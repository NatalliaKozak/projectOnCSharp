using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using static kurs.LogWind;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для addNewOtr.xaml
    /// </summary>
    public partial class addNewOtr : Window
    {
        List<string> emails = new List<string>();
        public static string admin { get; set; }
        public addNewOtr()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select a.L_Name from  Admin a where a.Login = '" + userInfo.login + "'";
            cmd.Connection = con;
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                admin = (string)da.GetValue(0).ToString();
            }
            con.Close();

            try
            {
                con.Open();
                cmd.CommandText = "Select Email from Students where Email!='Null'";
                cmd.Connection = con;
                da = cmd.ExecuteReader();
                while (da.Read())
                {
                    emails.Add(da.GetValue(0).ToString());
                }

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

        private void rest(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void change(object sender, RoutedEventArgs e)
        {
            string pattern = @"([0-2]\d|3[01])\.(0\d|1[012])\.(\d{4})";
            string timePattern = "^(([0,1][0-9])|(2[0-3])):[0-5][0-9]$";

            if (date.Text == "")
                MessageBox.Show("Введите дату");
            else
            {
                if (Regex.IsMatch(date.Text, pattern, RegexOptions.IgnoreCase))
                {
                    if (time.Text == "")
                        MessageBox.Show("Введите время");
                    else
                    {
                        if (Regex.IsMatch(time.Text, timePattern, RegexOptions.IgnoreCase))
                        {
                            if (disc.Text == "")
                                MessageBox.Show("Введите описание");
                            else
                            {
                                try
                                {
                                    SqlConnection con = new SqlConnection();
                                    con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.CommandText = "Insert into OtrForStud(Date, Time, Descript, Admin) values (  cast('" + MDYToDMY(date.Text) + "' as date), cast('" + time.Text + "' as time), '" + disc.Text + "', '" + admin + "') ";
                                    cmd.Connection = con;
                                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                                    da.SelectCommand.ExecuteNonQuery();
                                    MessageBox.Show("Данные добавлены.");
                                    this.Close();



                                    MailAddress from = new MailAddress("dormitory.belstu@gmail.com", "Общежитие");
                                    foreach (var em in emails)
                                    {
                                        MailAddress to = new MailAddress("" + em + "");
                                        // создаем объект сообщения
                                        MailMessage m = new MailMessage(from, to);
                                        // тема письма
                                        m.Subject = "Dormitory";
                                        // текст письма
                                        m.Body = "<h2>В личном кабинете доступны новые отработки</h2>";
                                        // письмо представляет код html
                                        m.IsBodyHtml = true;
                                        // адрес smtp-сервера и порт, с которого будем отправлять письмо
                                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                                        // логин и пароль
                                        smtp.Credentials = new NetworkCredential("dormitory.belstu@gmail.com", "1213141516am");
                                        smtp.EnableSsl = true;
                                        smtp.Send(m);
                                        Console.Read();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        else
                            MessageBox.Show("Неверный формат времени. Пример: hh:mm");
                    }
                }
                else
                    MessageBox.Show("Неверный формат даты. Пример: dd.mm.yyyy");

            }



        }

        static string MDYToDMY(string input)
        {
            try
            {
                return Regex.Replace(input,
                       @"\b(?<day>\d{1,2}).(?<month>\d{1,2}).(?<year>\d{2,4})\b",
                      "${month}.${day}.${year}", RegexOptions.None,
                      TimeSpan.FromMilliseconds(150));
            }
            catch (RegexMatchTimeoutException)
            {
                return input;
            }
        }
    }
}
