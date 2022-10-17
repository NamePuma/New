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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Npgsql;
using NpgsqlTypes;


namespace Ban
{
    /// <summary>
    /// Логика взаимодействия для Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        public ObservableCollection<Specialities> Speciality { get; set; }
        public Page6()
        {
             
        InitializeComponent();


            Speciality = new ObservableCollection<Specialities>();
            Connect("10.14.206.27", "5432", "student", "1234", "mmm");
            AddSpeti();
            DataContext = this;

        }
        private NpgsqlConnection connection;
        public void Connect(string host, string port, string username, string password, string database)
        {
            connection = new NpgsqlConnection(string.Format(
                "Server={0};Port={1};User Id={2};Password={3};Database={4};",
                host, port, username, password, database));
            connection.Open();
        }
        private void AddSpeti()
        {
            Speciality.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT id, code, caption, qualification FROM specialty   ";
            var result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Speciality.Add(new Specialities(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetString(3)));
                }
            }
            result.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var spec = listiiiii.SelectedItem as Specialities;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;

            command.CommandText = "Delete FROM student where study_group in (select number from study_group where specialty = @iddd)";
            command.Parameters.AddWithValue("@iddd", spec.Id);
            command.ExecuteNonQuery();

            command.CommandText = "Delete FROM study_group where specialty =  @idd";
            command.Parameters.AddWithValue("@idd", spec.Id);
            command.ExecuteNonQuery();


            command.CommandText = "Delete FROM specialty where id =  @id";
            command.Parameters.AddWithValue("@id", spec.Id);
            command.ExecuteNonQuery();


            AddSpeti();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var spec = listiiiii.SelectedItem as Specialities;
            int t = spec.Id;
            string p1 = P1.Text.Trim();
            string p2 = P2.Text.Trim();
            string p3 = P3.Text.Trim();

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE specialty SET code = @a, caption = @b, qualification = @c where id = @t";
            command.Parameters.AddWithValue("@t", NpgsqlDbType.Integer, t);
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, p1);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, p2);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, p3);
            command.ExecuteNonQuery();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page7());
    }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
    
}
