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
    /// Логика взаимодействия для Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        public ObservableCollection<Specialities> Speciality { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public Page7()
        {


            InitializeComponent();
            InitializeComponent();

            Connect("10.14.206.27", "5432", "student", "1234", "mmm");

            DataContext = this;

            Speciality = new ObservableCollection<Specialities>();
            Groups = new ObservableCollection<Group>();


            
            LoadGroup();
        }
        private NpgsqlConnection connection;
        public void Connect(string host, string port, string username, string password, string database)
        {
            connection = new NpgsqlConnection(string.Format(
                "Server={0};Port={1};User Id={2};Password={3};Database={4};",
                host, port, username, password, database));
            connection.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadGroup()
        {
            Groups.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT number, course, specialty, caption FROM study_group  Join specialty ON specialty.id = study_group.specialty";
            var result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Groups.Add(
                        new Group(result.GetString(0), result.GetInt32(1), result.GetInt32(2), result.GetString(3)));
                }
            }
            result.Close();


        }
         private void LoadSpett()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT id, code, caption, qualification FROM specialty";
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

            var a = mama1.Text.Trim();
            var b = mama2.Text.Trim().ToString();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Update study_group set number =@a,  course =@b  where number = @a ";
            cmd.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, a);
            cmd.Parameters.AddWithValue("@b", NpgsqlDbType.Integer, b);
            cmd.ExecuteNonQuery();
        }
    }
}
