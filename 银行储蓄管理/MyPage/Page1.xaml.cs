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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        private BankDbEntities2 context2 = new BankDbEntities2();
        public Page1()
        {
            InitializeComponent();
            this.Unloaded += Page1_Unloaded;
            btn_1_1.Click += btn_1_Click;
            btn_1_2.Click += btn_1_Click;
            btn_2_1.Click += btn_2_1_Click;
            var q = from t in context2.InterestRate
                    select t;
            datagrid2.ItemsSource = q.ToList();
        }

        void btn_2_1_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            try
            {
                context2.SaveChanges();
                MessageBox.Show("修改成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"保存失败！");
            }  
        }

        void Page1_Unloaded(object sender, RoutedEventArgs e)
        {
            context2.Dispose();
        }

        void btn_1_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            switch (btn.Name.Trim())
            {
                case "btn_1_1":
                    DateTime tt = DateTime.Now.Date;
                    using (var context = new BankDbEntities2())
                    {
                        var q = from t in context.Summary
                                where t.时间 == tt
                                select t;
                        datagrid1.Visibility = System.Windows.Visibility.Visible;
                        datagrid1.ItemsSource = q.ToList();
                    };
                    break;
                case "btn_1_2":
                    string idcard = tb_1.Text.Trim();
                    using (var context = new BankDbEntities2())
                    {
                        var q = from t1 in context.Deposit
                                from t2 in context.Costomer
                                where t1.账号 == t2.账号 && t2.身份证号码 == idcard
                                select new
                                {
                                    存款类型 = t1.存款类型,
                                    金额 = t1.金额,
                                    期限 = t1.期限,
                                    交易日期 = t1.日期
                                };
                        datagrid1.Visibility = System.Windows.Visibility.Visible;
                        datagrid1.ItemsSource = q.ToList();
                    }
                    break;
            }
        }
    }
}
