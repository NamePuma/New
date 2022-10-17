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
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public ObservableCollection<Specialities> Speciality { get; set; }
        public Page3()
        {
            InitializeComponent();
         
            Connect("10.14.206.27", "5432", "student", "1234", "mmm");
            DataContext = this;
            Speciality = new ObservableCollection<Specialities>();
            LoadSpeciality();
            

        }
        private NpgsqlConnection connection;
        public void Connect(string host, string port, string username, string password, string database)
        {
            connection = new NpgsqlConnection(string.Format(
                "Server={0};Port={1};User Id={2};Password={3};Database={4};",
                host, port, username, password, database));
            connection.Open();
        }
        private void AddSpeciality(object sender, RoutedEventArgs e)
        {
            string specID = specialityID.Text.Trim();
            string specName = specialityName.Text.Trim();
            string qual = qualification.Text.Trim();

            if (specID == null || specName == null || qual == null) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO specialty (code, caption, qualification) VALUES(@specID, @specName, @qual)";
            command.Parameters.AddWithValue("@specID", NpgsqlDbType.Varchar, specID);
            command.Parameters.AddWithValue("@specName", NpgsqlDbType.Varchar, specName);
            command.Parameters.AddWithValue("@qual", NpgsqlDbType.Varchar, qual);
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность добавлена");
                LoadSpeciality();
            }

            specialityID.Clear();
            specialityName.Clear();
            qualification.Clear();
        }
        private void LoadSpeciality()
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

        private void buttonForRed_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate( new Page6());
        }
    }
}
