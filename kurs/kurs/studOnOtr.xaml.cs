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
using static kurs.Views.addIbfoOtr;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для studOnOtr.xaml
    /// </summary>
    public partial class studOnOtr : Window
    {
        public studOnOtr()
        {
            InitializeComponent();
        }

        DataTable datatable = new DataTable("infoOtr");
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select s.Id as Студенческий, l.L_Name as Фамилия, l.Room as Комната from OtrZapis s join Libing l on (s.Id = l.Id) where s.Descript = '" + selectetRow.row + "' and Status = 'Нет'";
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
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update OtrZapis set Status = 'Да' where Descript = '" + selectetRow.row + "' ";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Отработка выставлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                send.IsEnabled = false;
                drop.IsEnabled = false;
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
                cmd.CommandText = "Delete from OtrZapis where Descript = '" + selectetRow.row + "' and Id = '" + sel + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Студент удалён из списка");
                cmd.CommandText = "Select s.Id as Студенческий, l.L_Name as Фамилия, l.Room as Комната from OtrZapis s join Libing l on (s.Id = l.Id) where s.Descript = '" + selectetRow.row + "'";
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
                send.IsEnabled = false;
                drop.IsEnabled = false;
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
                send.IsEnabled = true;
                drop.IsEnabled = true;
            }
            
        }
    }
}
