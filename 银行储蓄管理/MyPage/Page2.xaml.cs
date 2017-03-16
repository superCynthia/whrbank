using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 银行储蓄管理.MyPage
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        Deposit dep = new Deposit();
        public Page2()
        {
            InitializeComponent();
            btn_2_1.Click += btn_2_1_Click;
            btn_2_2.Click += btn_2_2_Click;
        }

        void btn_2_2_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int num = int.Parse(tb_2_5.Text);
            using (var context = new BankDbEntities2())
            {
                var q = from t in context.Deposit
                        where t.编号 == num
                        select t;
                foreach (var v in q)
                {
                    decimal money = addInterestRate(v);
                    context.Deposit.Remove(v);
                    Summary sum = new Summary();
                    sum.编号 = 20;
                    sum.账号 = v.账号;
                    sum.金额 = money;
                    sum.时间 = DateTime.Now;
                    sum.业务类型 = "取款";
                    context.Summary.Add(sum);
                    var qq = from t in context.Costomer
                             where t.账号 == v.账号
                             select t;
                    foreach (var vv in qq)
                    {
                        vv.余额 -= v.金额;
                    }
                    lb_money.Content = money.ToString();
                }             
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败：" + ex.Message);
                }
            }
        }

        void btn_2_1_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int num = int.Parse(tb_2_1.Text.Trim());
            string id = tb_2_3.Text.Trim();
            string pwd = tb_2_2.Password;
            using (var context = new BankDbEntities2())
            {
                var q1 = from t1 in context.Deposit
                        where t1.账号 == num
                        select t1;
                var q2 = from t2 in context.Costomer
                         where t2.账号 == num
                         select t2;
                foreach (var v in q2)
                {
                    if (v.身份证号码 == id && v.密码 == pwd)
                    {
                        datagrid3.ItemsSource = q1.ToList();
                    }
                    else
                    {
                        MessageBox.Show("无匹配客户！");
                    }
                }                
            }
        }
        private bool isExtended(Deposit dep)
        {
            if (DateTime.Now.AddYears(-dep.期限).CompareTo(dep.日期) > 0)
            {
                return true;
            }
            return false;
        }

        private decimal addInterestRate(Deposit dep)
        {
            decimal newMoney = 0;
            var context = new BankDbEntities2();
            var q = from t in context.InterestRate
                    select t;
            List<InterestRate> list = q.ToList();
            switch(dep.存款类型){
                case "活期存款":
                    newMoney = dep.金额 * (1 + list.ElementAt(5).利率); 
                    break;
                case "定期存款":
                    switch(dep.期限){
                        case 1:
                            if (isExtended(dep))
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(0).利率) * (1 + list.ElementAt(3).利率); 
                            }
                            else
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(4).利率);
                            }
                            break;
                        case 3:
                            if (isExtended(dep))
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(1).利率) * (1 + list.ElementAt(3).利率);
                            }
                            else
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(4).利率);
                            }
                            break;
                        case 5:
                            if (isExtended(dep))
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(2).利率) * (1 + list.ElementAt(3).利率);
                            }
                            else
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(4).利率);
                            }
                            break;
                    }
                    break;
                case "零存整取":
                    switch(dep.期限){
                        case 1:
                            if (isExtended(dep))
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(6).利率) * (1 + list.ElementAt(9).利率);
                            }
                            else
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(10).利率);
                            }
                            break;
                        case 3:
                            if (isExtended(dep))
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(7).利率) * (1 + list.ElementAt(9).利率);
                            }
                            else
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(10).利率);
                            }
                            break;
                        case 5:
                            if (isExtended(dep))
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(8).利率) * (1 + list.ElementAt(9).利率);
                            }
                            else
                            {
                                newMoney = dep.金额 * (1 + list.ElementAt(10).利率);
                            }
                            break;
                    }
                    break;
            }
            return newMoney;
        }
    }
}
