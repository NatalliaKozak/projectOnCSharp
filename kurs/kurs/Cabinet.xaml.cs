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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using kurs.Views;
using kurs.ViewModels;
using static kurs.LogWind;
using System.IO;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для Cabinet.xaml
    /// </summary>
    public partial class Cabinet : Window
    {


        public Cabinet()
        {
            InitializeComponent();
            DataContext = new cabinetViewModel();


            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select F_Name, M_Name, Photo, Vacancy from Admin where login = '" + userInfo.login + "'";
                cmd.Connection = con;
                SqlDataReader da = cmd.ExecuteReader();
                while(da.Read())
                {
                    NameOfPerson.Text = da.GetValue(0).ToString();
                    NameOfPerson.Text += " " + da.GetValue(1).ToString();
                    byte[] img = (byte[])da.GetValue(2);

                    
                    using (var ms = new MemoryStream(img))
                    {
                        userImg.ImageSource = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }

                    StatusOfPerson.Text = da.GetValue(3).ToString();
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
    }
}

