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
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public int UserName { get; set; }
        public string Rank { get; set; }
        public string NName { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            btn_login.Click += btn_login_Click;
            btn_exit.Click += btn_exit_Click;
        }

        void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            int num = int.Parse(tb_log1.Text.Trim());
            string pwd = tb_log2.Password;
            using (var context = new BankDbEntities2())
            {
                var q = from t in context.Staff
                        where t.员工编号 == num
                        select t;
                foreach (var v in q)
                {
                    if (v.密码 == pwd)
                    {
                        this.NName = v.姓名;
                        this.Rank = v.身份;
                        this.UserName = v.员工编号;
                        this.Close();
                        break;
                    }
                    else
                    {
                        MessageBox.Show("密码错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        //MessageBoxResult res = MessageBox.Show("密码错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        //if (res == MessageBoxResult.OK)
                        //{
                        //    //this.Close();
                        //    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        //}
                    }
                }             
            }
        }
    }
}
