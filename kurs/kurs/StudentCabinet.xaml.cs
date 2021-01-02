using kurs.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для StudentCabinet.xaml
    /// </summary>
    public partial class StudentCabinet : Window
    {
        public static ImageSource source;

        public StudentCabinet()
        {
            InitializeComponent();
            DataContext = new StudentCabinetViewModel();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select l.F_Name, l.L_Name, s.Photo, l.Id from Libing l join Students s on (l.Id = s.Id) where s.login = '" + userInfo.login + "'";
                cmd.Connection = con;
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    NameOfPerson.Text = da.GetValue(0).ToString();
                    NameOfPerson.Text += " " + da.GetValue(1).ToString();
                    StatusOfPerson.Text = da.GetValue(3).ToString();

                    if (da.GetValue(2).GetType().ToString() == "System.DBNull")
                        return;
                    else
                    {
                        byte[] img = (byte[])da.GetValue(2);


                        using (var ms = new MemoryStream(img))
                        {
                            userImg.ImageSource = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        }

                    }
                    

                    
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
        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            LogWind main = new LogWind();
            main.Show();
            this.Close();
        }

        private void refresh(object sender, MouseButtonEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select l.F_Name, l.L_Name, s.Photo, l.Id from Libing l join Students s on (l.Id = s.Id) where s.login = '" + userInfo.login + "'";
                cmd.Connection = con;
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    NameOfPerson.Text = da.GetValue(0).ToString();
                    NameOfPerson.Text += " " + da.GetValue(1).ToString();
                    StatusOfPerson.Text = da.GetValue(3).ToString();

                    if (da.GetValue(2).GetType().ToString() == "System.DBNull")
                        return;
                    else
                    {
                        byte[] img = (byte[])da.GetValue(2);


                        using (var ms = new MemoryStream(img))
                        {
                            userImg.ImageSource = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        }

                    }



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
    }
}
