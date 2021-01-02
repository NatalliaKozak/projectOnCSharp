using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace kurs.Views
{
    /// <summary>
    /// Логика взаимодействия для addNewStudents.xaml
    /// </summary>
    public partial class addNewStudents : ContentControl
    {
        public addNewStudents()
        {
            InitializeComponent();
        }

        /*void clearTextBox(StackPanel Name)
        {
            foreach (Control txtBox in Name.Children)
            {
                if (txtBox.GetType() == typeof(TextBox))
                    ((TextBox)txtBox).Text = string.Empty;
            }
        }*/

        private void addNewStudentToList(object sender, RoutedEventArgs e)
        {
            //this.clearTextBox(grid);

            if (checkId() == 1)
            {
                if (checkFIO(L_Name.Text) == 0)
                {
                    if (checkFIO(F_Name.Text) == 0)
                    {
                        if (checkFIO(M_Name.Text) == 0)
                        {
                            SqlConnection con = new SqlConnection();
                            con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;

                            try
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = "Insert into Libing(Id, L_Name, F_Name, M_Name, Faculti, Curs, Gr, Room ) values ( '" + Convert.ToInt32(Id.Text) + "' , '" + L_Name.Text + "' , '" + F_Name.Text + "', '" + M_Name.Text + "', '" + Facult.Text + "', '" + Convert.ToInt32(Curs.Text) + "', '" + Convert.ToInt32(Group.Text) + "', '" + Convert.ToInt32(Room.Text) + "') ";
                                cmd.Connection = con;
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                da.SelectCommand.ExecuteNonQuery();
                                MessageBox.Show("Данные добавлены.");


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
                        else
                            MessageBox.Show("Заполните поле 'Отчество'");
                    }
                    else
                        MessageBox.Show("Заполните поле 'Имя'");
                }
                else
                    MessageBox.Show("Заполните поле 'Фамилия'");
            }
            else if (checkId() == 0)
                MessageBox.Show("Номер студенческого введён неверно.");
            else
                MessageBox.Show("Студент с данным студенческим билетом уже есть в базе данных.");

        }

        private void onlyNumb(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private int checkId()
        {
            int rez = 0;
            
            if (Id.Text.Length == 9)
            {
                rez++;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["connList"].ConnectionString;
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select * from Libing where Id = '" + Convert.ToInt32(Id.Text) + "'";
                    cmd.Connection = con;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable check = new DataTable();
                    sqlDataAdapter.Fill(check);
                    if (check.Rows.Count == 1)
                        rez++;
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
            return rez;
        }
        private int checkFIO(string info)
        {
            int rez = 0;
            if (info == "") rez++;
            return rez;
        }

        private void onlyLett(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^А-яЁё]+").IsMatch(e.Text);
        }
    }
}
