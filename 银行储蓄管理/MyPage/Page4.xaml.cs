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
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class Page4 : Page
    {
        BankDbEntities2 context1 = new BankDbEntities2();
        public Page4()
        {
            InitializeComponent();
            this.Unloaded += Page4_Unloaded;
            btn_4_1.Click += btn_4_1_Click;
            btn_4_2.Click += btn_4_2_Click;
            var q = from t in context1.Staff
                    select t;
            datagrid4.ItemsSource = q.ToList();
        }

        void Page4_Unloaded(object sender, RoutedEventArgs e)
        {
            context1.Dispose();
        }

        void btn_4_2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                context1.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败：" + ex.Message);
            }
        }

        void btn_4_1_Click(object sender, RoutedEventArgs e)
        {
            decimal salary1 = 0, salary2 = Decimal.Parse(tb_4_2.Text.Trim());
            Button btn = sender as Button;
            int num = int.Parse(tb_4_1.Text.Trim());
            using (var context = new BankDbEntities2())
            {
                var q = from t in context.Staff
                        where t.员工编号 == num
                        select t;
                foreach (var v in q)
                {
                    salary1 = v.工资;
                    v.工资 = salary2;              
                }
                MessageBoxResult res = MessageBox.Show("是否将工资从" + salary1.ToString() + "改为" + salary2.ToString() + "?", "确认信息", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (res == MessageBoxResult.OK)
                {
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("修改失败：" + ex.Message);
                    }
                }   
            }
        }
    }
}
