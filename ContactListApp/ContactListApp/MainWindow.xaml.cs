using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace ContactListApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string contact_id;
        private object datagrid_contact;

        private void Initialize()
        {
            server = "localhost";
            database = "myContactList";
            uid = "myuser";
            password = "mypass";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            string query = "SELECT * FROM contacts";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                da.Fill(ds);

                contact_table.ItemsSource = ds.Tables[0].DefaultView;

                this.CloseConnection();
            }
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
