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
    /// Логика взаимодействия для infoOtrAndSub.xaml
    /// </summary>
    public partial class infoOtrAndSub : ContentControl
    {
        DataTable tablOtr = new DataTable("infoOtr");
        DataTable tablSub = new DataTable("infoSub");

        public infoOtrAndSub()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select  rtrim(ot.Date) as Дата, o.Descript as Описание from OtrZapis o join Students s on (o.Id = s.Id) join OtrForStud ot on (ot.Descript = o.Descript) " +
                    "where Status = 'Да' and s.Login = '"+userInfo.login+"'";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tablOtr);
                otr.ItemsSource = tablOtr.DefaultView;


                cmd.CommandText = "Select  rtrim(ot.Date) as Дата, o.Descript as Описание from SubbotZapis o join Students s on (o.Id = s.Id) join SubbotForStud ot on (ot.Descript = o.Descript) " +
                    "where Status = 'Да' and s.Login = '"+userInfo.login+"'";
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                da.Fill(tablSub);
                sub.ItemsSource = tablSub.DefaultView;


                cmd.CommandText = "Select count(*) Status from OtrZapis ot join Students st on (st.Id = ot.Id) where Status = 'Да' and st.Login = '" + userInfo.login + "'";
                cmd.Connection = con;
                SqlDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    countOtr.Text = d.GetValue(0).ToString();
                }
                con.Close();


                con.Open();
                cmd.CommandText = "Select count(*) Status from SubbotZapis s join Students st on (st.Id = s.Id) where Status = 'Да' and st.Login = '" + userInfo.login + "'";
                cmd.Connection = con;
                SqlDataReader a = cmd.ExecuteReader();
                while (a.Read())
                {
                    countSub.Text = a.GetValue(0).ToString();
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
