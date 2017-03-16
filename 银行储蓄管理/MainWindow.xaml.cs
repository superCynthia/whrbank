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
using System.Windows.Threading;

namespace 银行储蓄管理
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        LoginWindow w = new LoginWindow();
        
        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.WindowStyle = System.Windows.WindowStyle.None;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Start();
            btn_changepwd.Click += btn_leftitem_Click;
            btn_changeuser.Click += btn_leftitem_Click;
            btn_exit.Click += btn_leftitem_Click;
        }

        void MainWindow_SourceInitialized(object sender, EventArgs e)
        {     
            w.ShowDialog();
            lb_no.Content = w.UserName;
            lb_name.Content = w.NName;
            lb_rank.Content = w.Rank;
        }

        void btn_leftitem_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            switch (btn.Name.Trim())
            {
                case "btn_changepwd":
                    ChangePwdWindow cpw = new ChangePwdWindow();
                    cpw.Num = w.UserName;
                    cpw.ShowDialog();
                    break;
                case "btn_changeuser":
                    this.Close();
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    break;
                case "btn_exit":
                    App.Current.Shutdown();
                    break;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lb_timer.Content = string.Concat(DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            if (btn != null && "管理员".Equals(w.Rank))
            {
                mainframe.Source = new Uri("MyPage/" + btn.Tag + ".xaml",UriKind.Relative);
            }
            else if (btn != null && "操作员".Equals(w.Rank))
            {
                switch (btn.Tag.ToString())
                {
                    case "Page1":
                        MessageBox.Show("无权限进行此操作！","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
                        break;
                    case "Page2":
                        mainframe.Source = new Uri("MyPage/" + btn.Tag + ".xaml", UriKind.Relative);
                        break;
                    case "Page3":
                        mainframe.Source = new Uri("MyPage/" + btn.Tag + ".xaml", UriKind.Relative);
                        break;
                    case "Page4":
                        MessageBox.Show("无权限进行此操作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case "Page5":
                        mainframe.Source = new Uri("MyPage/" + btn.Tag + ".xaml", UriKind.Relative);
                        break;
                    case "Page6":
                        mainframe.Source = new Uri("MyPage/" + btn.Tag + ".xaml", UriKind.Relative);
                        break;
                }
            }
        }
        
    }
}
 