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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static kurs.LogWind;

namespace kurs.Views
{
    /// <summary>
    /// Логика взаимодействия для infoSubbotStudent.xaml
    /// </summary>
    public partial class infoSubbotStudent : ContentControl
    {
        public static int userId { get; set; }
        public infoSubbotStudent()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select l.Id from Libing l join Students s on (l.Id = s.Id) where s.login = '" + userInfo.login + "'";
                cmd.Connection = con;
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                    userId = Convert.ToInt32(da.GetValue(0));

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

        DataTable datatable = new DataTable("infoSub");
        DataTable table = new DataTable("Sub");
        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select  rtrim(s.Date) as Дата, substring(cast(s.Time as varchar(10)), 1, 5) as Время, s.Descript as Описание, Admin as Принимающий from SubbotForStud s join Admin a on (s.Admin = a.L_Name) ";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(datatable);
                info.ItemsSource = datatable.DefaultView;


                cmd.CommandText = "Select  rtrim(s.Date) as Дата, substring(cast(s.Time as varchar(10)), 1, 5) as Время, s.Descript as Описание, s.Admin as Принимающий from SubbotForStud s join SubbotZapis z on (s.Descript = z.Descript) where z.Id = '"+ userId+"' and z.Status = 'Нет'";
                cmd.Connection = con;
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                d.Fill(table);
                infost.ItemsSource = table.DefaultView;
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

        public static string row { get; set; }
        private void info_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)info.SelectedItem;
            if (dataRow == null) return;
            else
            {
                row = dataRow.Row.ItemArray[2].ToString();
                addItem.IsEnabled = true;
            }
            
        }
        private void infost_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)infost.SelectedItem;
            if (dataRow == null) return;
            else
            {
                row = dataRow.Row.ItemArray[2].ToString();
                delItem.IsEnabled = true;
            }
            
        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Insert into SubbotZapis(Descript, Id, Status) values ('"+row+"', '"+userId+"', 'Нет')";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Субботник добавлен");
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

        private void delItem_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Delete from SubbotZapis where Descript = '" + row + "' ";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Субботник удалён из списка");
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

        private void refItem_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select  rtrim(s.Date) as Дата, substring(cast(s.Time as varchar(10)), 1, 5) as Время, s.Descript as Описание, s.Admin as Принимающий from SubbotForStud s join SubbotZapis z on (s.Descript = z.Descript) where z.Id = '" + userId + "' and z.Status = 'Нет'";
                cmd.Connection = con;
                SqlDataAdapter d = new SqlDataAdapter(cmd);
                table.Clear();
                d.Fill(table);
                infost.ItemsSource = table.DefaultView;
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
