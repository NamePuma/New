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
    /// Логика взаимодействия для Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public ObservableCollection<Specialities> Speciality { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public Page4()
        {
         

            InitializeComponent();
            InitializeComponent();

            Connect("10.14.206.27", "5432", "student", "1234", "mmm");

            DataContext = this;

            Speciality = new ObservableCollection<Specialities>();
            Groups = new ObservableCollection<Group>();


            LoadSpett();
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
        private void AddGroup(object sender, RoutedEventArgs e)
        {
            string groupid = groupID.Text.Trim();
            var groupSpeciality = (takeSpecialityName.SelectedItem as Specialities).Id;
            int course = int.Parse(courseGroup.Text.Trim());

            if (groupid == null || course == null) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO study_group (number, course, specialty) VALUES(@groupid, @course, @groupSpeciality)";
            command.Parameters.AddWithValue("@groupid", NpgsqlDbType.Varchar, groupid);
            command.Parameters.AddWithValue("@course", NpgsqlDbType.Integer, course);
            command.Parameters.AddWithValue("@groupSpeciality", NpgsqlDbType.Integer, groupSpeciality);
            var result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Группа добавлена");
                LoadGroup();
            }

            groupID.Clear();
            courseGroup.Clear();
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
    }
}
