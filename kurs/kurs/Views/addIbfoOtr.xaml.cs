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
    /// Логика взаимодействия для addIbfoOtr.xaml
    /// </summary>
    public partial class addIbfoOtr : ContentControl
    {
        public addIbfoOtr()
        {
            InitializeComponent();
        }

        public class selectetRow
        {
            public static string row { get; set; }
        }

        DataTable datatable = new DataTable("infoSub");

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select  rtrim(s.Date) as Дата, substring(cast(s.Time as varchar(10)), 1, 5) as Время, s.Descript as Описание, (Select count(*) from OtrZapis where Descript = s.Descript and Status = 'Нет') as [Количество студентов],  Admin as Принимающий from OtrForStud s join Admin a on (s.Admin = a.L_Name) ";
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


        private void openWindowForAddNewOtr(object sender, RoutedEventArgs e)
        {
            addNewOtr addNewOtr = new addNewOtr();
            addNewOtr.Show();
        }

        private void refreshList(object sender, RoutedEventArgs e)
        {
            datatable.Clear();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select  rtrim(s.Date) as Дата, substring(cast(s.Time as varchar(10)), 1, 5) as Время, s.Descript as Описание, (Select count(*) from OtrZapis where Descript = s.Descript and Status = 'Нет') as [Количество студентов], Admin as Принимающий from OtrForStud s join Admin a on (s.Admin = a.L_Name)";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(datatable);
            info.ItemsSource = datatable.DefaultView;
        }



        private void delSub(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Delete from OtrForStud where Descript = '" + selectetRow.row + "'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Субботник удалён из списка");
                cmd.CommandText = "Select rtrim(s.Date) as Дата, substring(cast(s.Time as varchar(10)), 1, 5) as Время, s.Descript as Описание, (Select count(*) from OtrZapis where Descript = s.Descript and Status = 'Нет') as [Количество студентов],  Admin as Принимающий from OtrForStud s join Admin a on (s.Admin = a.L_Name)";
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
            }
        }

        private void SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)info.SelectedItem;
            if (dataRow == null) return;
            else
            {
                selectetRow.row = dataRow.Row.ItemArray[2].ToString();
                del.IsEnabled = true;
            }
        }

        private void info_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)info.SelectedItem;
            if (dataRow == null) return;
            else
            {
                selectetRow.row = dataRow.Row.ItemArray[2].ToString();

                studOnOtr wind = new studOnOtr();
                wind.Show();
            }
        }
    }
}
