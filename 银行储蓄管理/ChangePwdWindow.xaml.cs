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
using System.Windows.Shapes;

namespace 银行储蓄管理
{
    /// <summary>
    /// ChangePwdWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePwdWindow : Window
    {
        public int Num { get; set; }
        public ChangePwdWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            btn.Click += btn_Click;
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string pwd1 = pb1.Password;
            string pwd2 = pb2.Password;
            string pwd3 = pb3.Password;
            using (var context = new BankDbEntities2())
            {
                var q = from t in context.Staff
                        where t.员工编号 == this.Num
                        select t;
                foreach (var v in q)
                {
                    if (v.密码 == pwd1 && pwd2 == pwd3)
                    {
                        v.密码 = pwd3;
                        
                    }
                    else
                    {
                        MessageBox.Show("密码错误或新密码不一致！");
                    }
                }
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("修改成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败：" + ex.Message);
                }
            }
        }
    }
}
