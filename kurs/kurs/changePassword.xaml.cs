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
using System.Windows.Shapes;
using static kurs.LogWind;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для changePassword.xaml
    /// </summary>
    public partial class changePassword : Window
    {
        public static string pass { get; set; }
        public changePassword()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select s.Password from  Students s where s.login = '" + userInfo.login + "'";
            cmd.Connection = con;
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                pass = (string)da.GetValue(0).ToString();
            }
        }

        private void rest(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void change(object sender, RoutedEventArgs e)
        {

            if (spass.Password == pass)
            {
                if (npass.Password == nreppass.Password)
                {
                    try
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
                        con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "Update Students set Password = '" + npass.Password + "' where login = '" + userInfo.login + "' ";
                        cmd.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.SelectCommand.ExecuteNonQuery();
                        MessageBox.Show("Пароль изменён.");
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show("Проверьте правильность повтора нового пароля.");

                
            }
            else
                MessageBox.Show("Текущий пароль введён неверно.");

            
        }
    }
}
