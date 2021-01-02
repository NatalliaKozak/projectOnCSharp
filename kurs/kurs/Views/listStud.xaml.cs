using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Diagnostics;

namespace kurs.Views
{
    /// <summary>
    /// Логика взаимодействия для listStud.xaml
    /// </summary>
    public partial class listStud : ContentControl
    {
        
        public listStud()
        {
            InitializeComponent();
        }

        DataTable datatable = new DataTable("ListStudents");

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select distinct l.Room as Комната, l.L_Name as Фамилия, l.F_Name as Имя,  l.M_Name as Отчество, l.Id as Студенческий, " +
                    "(select count(*) from SubbotZapis s  where s.Status = 'Да' and s.Id = l.Id) as Субботники," +
                    "(select count(*) from OtrZapis s  where s.Status = 'Да' and s.Id = l.Id) as Отработки from Libing l ";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = datatable.DefaultView;
            if (numb.IsChecked == true)
                dv.Sort = "Комната";
            if (fam.IsChecked == true)
                dv.Sort = "Фамилия";
            if (name.IsChecked == true)
                dv.Sort = "Имя";
            if (mname.IsChecked == true)
                dv.Sort = "Отчество";
            if (id.IsChecked == true)
                dv.Sort = "Студенческий";
            if (otr.IsChecked == true)
                dv.Sort = "Отработки";
            if (sub.IsChecked == true)
                dv.Sort = "Субботники";
            info.ItemsSource = dv;
        } 

       


        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            datatable.DefaultView.RowFilter = "Фамилия like '" + search_fam.Text + "%' and Имя like '" + search_nam.Text + "%' and Отчество like '" + search_mnam.Text + "%'" +
                "and Convert(Комната, System.String) like '%" + search_room.Text + "%' and Convert(Отработки, System.String) like '%" + search_otr.Text + "%'" +
                "and Convert(Субботники, System.String) like '%" + search_sub.Text + "%'";
            info.ItemsSource = datatable.DefaultView;
        }

    }
}
