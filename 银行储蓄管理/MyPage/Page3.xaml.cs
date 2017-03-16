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
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        Deposit deposit = new Deposit();
        public Page3()
        {
            InitializeComponent();
            rb1_1.Checked += rb1_Checked;
            rb1_2.Checked += rb1_Checked;
            rb1_3.Checked += rb1_Checked;
            btn3.Click += btn3_Click;
            rb2_1.Checked += rb2_Checked;
            rb2_2.Checked += rb2_Checked;
            rb2_3.Checked += rb2_Checked;
        }

        void rb2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = e.Source as RadioButton;
            switch (rb.Name.Trim())
            {
                case "rb2_1":
                    deposit.期限 = 1;
                    break;
                case "rb2_2":
                    deposit.期限 = 3;
                    break;
                case "rb2_3":
                    deposit.期限 = 5;
                    break;
            }
        }

        void btn3_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int num = int.Parse(tb_3_1.Text);
            DateTime time = DateTime.Now;
            decimal price = Decimal.Parse(tb_3_2.Text.Trim());
            using (var context = new BankDbEntities2())
            {
                deposit.编号 = 100;  //标记
                deposit.账号 = num;
                deposit.日期 = time;
                deposit.金额 = price;
                try
                {
                    context.Deposit.Add(deposit);
                    Summary sum = new Summary();
                    sum.编号 = 120;   //标记
                    sum.账号 = deposit.账号;
                    sum.金额 = deposit.金额;
                    sum.时间 = DateTime.Now;
                    sum.业务类型 = deposit.存款类型;
                    context.Summary.Add(sum);
                    var q = from t in context.Costomer
                            where t.账号 == deposit.账号
                            select t;
                    foreach (var v in q)
                    {
                        v.余额 += deposit.金额; 
                    }
                    context.SaveChanges();
                    MessageBox.Show("操作成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败: " + ex.Message);
                }
            }
        }

        void rb1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = e.Source as RadioButton;
            switch (rb.Name.Trim())
            {
                case "rb1_2":
                    lb_trans.Visibility = System.Windows.Visibility.Visible;
                    rb2_1.Visibility = System.Windows.Visibility.Visible;
                    rb2_2.Visibility = System.Windows.Visibility.Visible;
                    rb2_3.Visibility = System.Windows.Visibility.Visible;
                    deposit.存款类型 = rb.Content.ToString();
                    break;
                case "rb1_3":
                    lb_trans.Visibility = System.Windows.Visibility.Visible;
                    rb2_1.Visibility = System.Windows.Visibility.Visible;
                    rb2_2.Visibility = System.Windows.Visibility.Visible;
                    rb2_3.Visibility = System.Windows.Visibility.Visible;
                    deposit.存款类型 = rb.Content.ToString();
                    break;
                default:
                    lb_trans.Visibility = System.Windows.Visibility.Hidden;
                    rb2_1.Visibility = System.Windows.Visibility.Hidden;
                    rb2_2.Visibility = System.Windows.Visibility.Hidden;
                    rb2_3.Visibility = System.Windows.Visibility.Hidden;
                    deposit.存款类型 = rb.Content.ToString();
                    deposit.期限 = 0;
                    break;
            }
        }
    }
}
