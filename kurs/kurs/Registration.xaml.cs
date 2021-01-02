using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Pass_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabPass.Visibility = Visibility.Hidden;
        }

        private void registration(object sender, RoutedEventArgs e)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Libing where Id = '" +Convert.ToInt32(Id.Text) + "'";
            cmd.Connection = con;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable check = new DataTable();
            sqlDataAdapter.Fill(check);
            if (check.Rows.Count == 1)
            {
                cmd.CommandText = "Select * from Students where Id = '" + Convert.ToInt32(Id.Text) + "'";
                cmd.Connection = con;
                sqlDataAdapter = new SqlDataAdapter(cmd);
                check = new DataTable();
                sqlDataAdapter.Fill(check);
                if (check.Rows.Count == 0)
                {
                    cmd.CommandText = "Select * from Students where Login = '" + Log.Text + "'";
                    cmd.Connection = con;
                    sqlDataAdapter = new SqlDataAdapter(cmd);
                    check = new DataTable();
                    sqlDataAdapter.Fill(check);
                    if (check.Rows.Count == 0)
                    {
                        if (Regex.IsMatch(Em.Text, pattern, RegexOptions.IgnoreCase))
                        {
                            try
                            {
                                cmd = new SqlCommand();
                                cmd.CommandText = "Insert into Students (Login, Password, Id, Email) " +
                                    "values ('" + Log.Text + "', '" + Convert.ToInt32(Pass.Password) + "', '" + Convert.ToInt32(Id.Text) + "', '" + Em.Text + "' )";
                                cmd.Connection = con;
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                da.SelectCommand.ExecuteNonQuery();

                                MessageBox.Show("Учётная запись создана");
                                LogWind logWind = new LogWind();
                                logWind.Show();
                                this.Close();
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
                        else
                            MessageBox.Show("Email введён некорректно.");
                    }
                    else
                        MessageBox.Show("Пользователь с таким логином уже сужествует.");
                }
                else
                    MessageBox.Show("У Вас уже есть учётная запись.");
                   
            }
            else
                MessageBox.Show("Студенческий введён некорректно.");
        }

        private void Log_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Log.Text == "Логин")
                Log.Text = "";
        }
        private void Id_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Id.Text == "Студенческий")
                Id.Text = "";
        }
        private void Em_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Em.Text == "Email")
                Em.Text = "";
        }

        private void back(object sender, MouseButtonEventArgs e)
        {
            LogWind logWind = new LogWind();
            logWind.Show();
            this.Close();
        }

        private void onlyNumb(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
