using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static kurs.LogWind;
using static kurs.StudentCabinet;

namespace kurs.Views
{
    /// <summary>
    /// Логика взаимодействия для studentInfo.xaml
    /// </summary>
    public partial class studentInfo : ContentControl
    {
        public studentInfo()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select l.F_Name, l.L_Name, l.M_Name, s.Photo, l.Id, l.Room, l.Faculti, l.Curs, l.Gr, s.Email, s.Phone, s.Login from Libing l join Students s on (l.Id = s.Id) where s.login = '" + userInfo.login + "'";
                cmd.Connection = con;
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    studentMainInfo.Text = da.GetValue(1).ToString();
                    studentMainInfo.Text += " " + da.GetValue(0).ToString();
                    studentMainInfo.Text += " " + da.GetValue(2).ToString();

                    if (da.GetValue(3).GetType().ToString() != "System.DBNull")
                    {
                        byte[] img = (byte[])da.GetValue(3);


                        using (var ms = new MemoryStream(img))
                        {
                            user.ImageSource = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        }

                    }
                    

                    StatusOfPerson.Text = da.GetValue(4).ToString();
                    room.Text = da.GetValue(5).ToString();
                    facult.Text = da.GetValue(6).ToString();
                    curs.Text = da.GetValue(7).ToString();
                    group.Text = da.GetValue(8).ToString();
                    if (da.GetValue(9).ToString() != "NULL")
                        email.Text = da.GetValue(9).ToString();
                    if (da.GetValue(10).ToString() != "NULL")
                        phon.Text = da.GetValue(10).ToString();
                    login.Text = da.GetValue(11).ToString();
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



        private void sendValue(object sender, RoutedEventArgs e)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();

                if (email.Text != "")
                {
                    if (Regex.IsMatch(email.Text, pattern, RegexOptions.IgnoreCase))
                    {
                        cmd.CommandText = "Update Students set Email = '" + email.Text + "' where login = '" + userInfo.login + "' ";
                        cmd.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.SelectCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Email введён некорректно.");
                    }
                    
                    
                }

                if (phon.Text != "")
                {
                    if (phon.Text.Length == 9)
                    {
                        cmd.CommandText = "Update Students set Phone = '" + phon.Text + "' where login = '" + userInfo.login + "' ";
                        cmd.Connection = con;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.SelectCommand.ExecuteNonQuery();
                    }
                    else
                        MessageBox.Show("Номер телефона введён некорректно.");
                }

                if (login.Text != "")
                {
                    if (userInfo.login != login.Text)
                    {
                        cmd.CommandText = "Select * from Students where Login = '" + login.Text + "'";
                        cmd.Connection = con;
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                        DataTable check = new DataTable();
                        sqlDataAdapter.Fill(check);
                        if (check.Rows.Count == 0)
                        {
                            cmd.CommandText = "Update Students set Login = '" + login.Text + "' where login = '" + userInfo.login + "' ";
                            cmd.Connection = con;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.SelectCommand.ExecuteNonQuery();
                            
                        }
                        else
                            MessageBox.Show("Пользователь с таким логином уже сужествует.");
                    }

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void newImg(object sender, MouseButtonEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP; *.JPG; *.PNG)|*.BMP; *.JPG; *.PNG";
            if(dialog.ShowDialog() == true)
            {
                try
                {
                    userimg.Background = new ImageBrush(new BitmapImage(new Uri(dialog.FileName, UriKind.Relative)));

                    cmd.CommandText = "Update Students set Photo = (SELECT * FROM OPENROWSET(BULK '" + dialog.FileName + "', SINGLE_BLOB) AS image) where login = '" + userInfo.login + "' ";
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void changPassword(object sender, RoutedEventArgs e)
        {
            changePassword wind = new changePassword();
            wind.Show();
        }

        private void phon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
