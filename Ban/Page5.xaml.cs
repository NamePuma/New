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
using Npgsql;
using NpgsqlTypes;
using System.Collections.ObjectModel;

namespace Ban
{
    /// <summary>
    /// Логика взаимодействия для Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public Page5()
        {
            InitializeComponent();
            Connect("10.14.206.27", "5432", "student", "1234", "mmm");

            DataContext = this;

            Students = new ObservableCollection<Student>();
            Groups = new ObservableCollection<Group>();

            LoadStudent();
            LoadForStudent();


        }
        private NpgsqlConnection connection;
        public void Connect(string host, string port, string username, string password, string database)
        {
            connection = new NpgsqlConnection(string.Format(
                "Server={0};Port={1};User Id={2};Password={3};Database={4};",
                host, port, username, password, database));
            connection.Open();
        }
        private void AddStudent(object sender, RoutedEventArgs e)
        {
            string name = studentName.Text.Trim();
            string surname = Surname.Text.Trim();
            var getgroupid = takeGroupId.SelectedItem as Group;

            if (name == null || surname == null || getgroupid == null) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO student (first_name, last_name, study_group) VALUES(@name, @surname, @getgroupid)";

            command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, name);
            command.Parameters.AddWithValue("@surname", NpgsqlDbType.Varchar, surname);
            command.Parameters.AddWithValue("@getgroupid", NpgsqlDbType.Varchar, getgroupid.GroupID);
            var result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Студент добавлен");
                LoadStudent();
            }

           
        }
        private void LoadStudent()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT first_name, last_name, study_group  FROM student";
            var result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Students.Add(new Student(result.GetString(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();
        }
        private void LoadForStudent()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT number, course, specialty, caption FROM study_group  Join specialty ON specialty.id = study_group.specialty";
            var result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Groups.Add(new Group(result.GetString(0), result.GetInt32(1), result.GetInt32(2), result.GetString(3) ));
                }
            }
            result.Close();

        }
    }



}
