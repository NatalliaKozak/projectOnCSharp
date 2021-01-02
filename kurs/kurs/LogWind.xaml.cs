using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LogWind : Window
    {
        public LogWind()
        {
            InitializeComponent();
        }

        public int count = 2;
        public static class userInfo
        {
            public static string login { get; set; }
        }

        private void login(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Admin where password = '" + Pass.Password.Trim() + "' and login = '" + Log.Text.Trim() + "'";
                cmd.Connection = con;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dbLogin = new DataTable();
                sqlDataAdapter.Fill(dbLogin);
                if (dbLogin.Rows.Count == 1)
                {
                    userInfo.login = Log.Text;
                    Cabinet cabinet = new Cabinet();
                    cabinet.Show();
                    this.Close();
                    count--;
                }
                else
                {
                    cmd.CommandText = "Select * from Students where Password = '" + Pass.Password.Trim() + "' and Login = '" + Log.Text.Trim() + "'";
                    SqlDataAdapter sqlDataAdapter_1 = new SqlDataAdapter(cmd);
                    DataTable dbLogin_1 = new DataTable();
                    sqlDataAdapter_1.Fill(dbLogin_1);
                    if (dbLogin_1.Rows.Count == 1)
                    {
                        userInfo.login = Log.Text;
                        StudentCabinet studentCabinet = new StudentCabinet();
                        studentCabinet.Show();
                        this.Close();
                        count--;
                    }
                }
                if (count == 2)
                    MessageBox.Show("Нверный логин или пароль");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Select_LogIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem.Content.ToString() == "Вход в учётную запись администрации")
                Log.Text = "Должность";
            else
                Log.Text = "Номер студенческого";
        }



        private void Log_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Log.Text == "Логин")
                Log.Text = "";
        }

        private void Pass_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabPass.Visibility = Visibility.Hidden;
        }

        private void regist(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();

            this.Close();
        }

        private void close(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void en(object sender, MouseButtonEventArgs e)
        {
            LabPass.Visibility = Visibility.Hidden;
        }
    }
}
