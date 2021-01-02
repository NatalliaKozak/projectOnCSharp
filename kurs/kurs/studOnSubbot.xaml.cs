using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using static kurs.Views.addInfoSub;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для studOnSubbot.xaml
    /// </summary>
    public partial class studOnSubbot : Window
    {
        public studOnSubbot()
        {
            InitializeComponent();

            
        }

        DataTable datatable = new DataTable("infoSub");
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select s.Id as Студенческий, l.L_Name as Фамилия, l.Room as Комната from SubbotZapis s join Libing l on (s.Id = l.Id) where s.Descript = '" + selectetRow.row + "' and Status = 'Нет'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(datatable);
                info.ItemsSource = datatable.DefaultView;
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





        private void sendRes(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-EV5QNO6; Initial Catalog=Dorm; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;

                try
                {
                    cmd.CommandText = "Update SubbotZapis set Status = 'Да' where Descript = '" + selectetRow.row + "' ";
                    cmd.Connection = connection;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Insert into Status values('" + selectetRow.row + "', 'Отработано') ";
                    cmd.Connection = connection;
                    SqlDataAdapter d = new SqlDataAdapter(cmd);
                    d.SelectCommand.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Суботник выставлен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
                finally
                {
                    drop.IsEnabled = false;
                    send.IsEnabled = false;
                }


            }

        }
        private void del(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Delete from SubbotZapis where Descript = '" + selectetRow.row + "' and Id = '" + sel + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Студент удалён из списка");
                cmd.CommandText = "Select s.Id as Студенческий, l.L_Name as Фамилия, l.Room as Комната from SubbotZapis s join Libing l on (s.Id = l.Id) where s.Descript = '" + selectetRow.row + "'";
                cmd.Connection = con;
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                datatable.Clear();
                d.Fill(datatable);
                info.ItemsSource = datatable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                drop.IsEnabled = false;
                send.IsEnabled = false;
            }
        }


         public static string sel { get; set; }
        private void info_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)info.SelectedItem;
            if (dataRow == null) return;
            else
            {
                sel = dataRow.Row.ItemArray[0].ToString(); // студ
                drop.IsEnabled = true;
                send.IsEnabled = true;
            }
        }
    }
}
