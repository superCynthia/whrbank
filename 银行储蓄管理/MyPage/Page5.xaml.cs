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
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Page5 : Page
    {
        Costomer costomer = new Costomer();
        public Page5()
        {
            InitializeComponent();
            btn_5_1.Click += btn_5_1_Click;
            btn_5_2.Click += btn_5_2_Click;
            btn_6_1.Click += btn_6_1_Click;
            btn_6_2.Click += btn_6_2_Click;
        }

        void btn_6_2_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            using (var context = new BankDbEntities2())
            {
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("操作成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败：" + ex.Message);
                }
            }
        }

        void btn_6_1_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            using (var context = new BankDbEntities2())
            {
                var q = from t in context.Costomer
                        select t;
                datagrid5.ItemsSource = q.ToList();
            }
        }

        void btn_5_2_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string id = tb_5_5.Text.Trim();
            string pwd = tb_5_6.Password;
            using (var context = new BankDbEntities2())
            {
                var q = from t in context.Costomer
                        where t.身份证号码 == id
                        select t;
                foreach (var v in q)
                {
                    if (v.密码 == pwd)
                    {
                        context.Costomer.Remove(v); 
                    }
                    else
                    {
                        MessageBox.Show("无匹配客户！");
                    }
                }
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("操作成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("销户失败：" + ex.Message);
                }
            }
        }

        void btn_5_1_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (rb5_1.IsChecked == true)
            {
                costomer.性别 = "男";
            }
            else if (rb5_2.IsChecked == true)
            {
                costomer.性别 = "女";
            }
            if (tb_5_3.Password == tb_5_4.Password) 
            {
                costomer.密码 = tb_5_3.Password;
            }
            costomer.姓名 = tb_5_1.Text.Trim();
            costomer.身份证号码 = tb_5_2.Text.Trim();
            costomer.余额 = 0;
            costomer.账号 = 1;
            using (var context = new BankDbEntities2())
            {
                try
                {
                    context.Costomer.Add(costomer);
                    context.SaveChanges();
                    MessageBox.Show("操作成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("开户失败：" + ex.Message);
                }
            }
        }
    }
}
