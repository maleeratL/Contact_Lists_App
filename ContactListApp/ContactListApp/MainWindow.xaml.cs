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

        private void update_table(object name)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.Columns.Add("Mobile Number");
            dt.Columns.Add("Email Address");
            dt.Rows.Add(new object[] { name, "Krishna", "9000000000", "haripobbati@gmail.com" });
            dt.Rows.Add(new object[] { name, "Locke", "15173257153", "kate.locke@gmail.com" });
            contact_table.ItemsSource = dt.DefaultView;
        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            var isNumeric = int.TryParse(phone_tb.Text, out int n);
            if (isNumeric == true)
            {
                string query = "INSERT INTO contacts (name, surname,phone,email) VALUES(\'" + name_tb.Text + "\', \'" + surname_tb.Text
                    + "\'," + phone_tb.Text + ",\'" + email_tb.Text + "\')";


                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                }
                Initialize();
                name_tb.Clear();
                surname_tb.Clear();
                phone_tb.Clear();
                email_tb.Clear();

                MessageBox.Show("Contact details are saved sucessfully!");
            }
            else
            {
                MessageBox.Show("Your phone details have to be number only!");
            }
        }

        private void Update_Button(object sender, RoutedEventArgs e)
        {
            var isNumeric = int.TryParse(phone_tb.Text, out int n);
            if (isNumeric == true)
            {
                string query = "UPDATE contacts SET name= \'" + name_tb.Text + "\', surname=\'" + surname_tb.Text
                + "\', phone=" + phone_tb.Text + ", email=\'" + email_tb.Text + "\' WHERE id=\'" + contact_id + "\'";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand();

                    cmd.CommandText = query;

                    cmd.Connection = connection;

                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                }
                Initialize();
                name_tb.Clear();
                surname_tb.Clear();
                phone_tb.Clear();
                email_tb.Clear();

                MessageBox.Show("Contact details are updated!");
            }
            else
            {
                MessageBox.Show("Your phone details have to be number only!");
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {

            string query = "DELETE FROM contacts WHERE id=\'" + contact_id + "\'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
            Initialize();
            name_tb.Clear();
            surname_tb.Clear();
            phone_tb.Clear();
            email_tb.Clear();

            MessageBox.Show("Contact details has been deleted!");
        }

        private void search_table(object name)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("First Name");
            dt.Columns.Add("Last Name");
            dt.Columns.Add("Mobile Number");
            dt.Columns.Add("Email Address");
            dt.Rows.Add(new object[] { name, "Krishna", "9000000000", "haripobbati@gmail.com" });
            dt.Rows.Add(new object[] { name, "Locke", "15173257153", "kate.locke@gmail.com" });
            contact_table.ItemsSource = dt.DefaultView;


        }


        private void Search_Button(object sender, RoutedEventArgs e)
        {
            server = "localhost";
            database = "myContactList";
            uid = "myuser";
            password = "mypass";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            string query = "SELECT * FROM contacts WHERE id LIKE  \'%" + search_tb.Text + "%\' OR name LIKE\'%" + search_tb.Text + "%\' OR surname LIKE \'%" + search_tb.Text + "%\' OR phone LIKE \'%" + search_tb.Text + "%\' OR email LIKE \'%" + search_tb.Text + "%\'";
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
            search_tb.Clear();
        }

        private void ShowList_Button(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void Selected_item(object sender, MouseEventArgs e)
        {
            var dg = sender as DataGrid;
            DataRowView row = (DataRowView)dg.SelectedItems[0];
            name_tb.Text = row["name"].ToString();
            surname_tb.Text = row["surname"].ToString();
            phone_tb.Text = row["phone"].ToString();
            email_tb.Text = row["email"].ToString();
            contact_id = row["id"].ToString();
        }

        private void Clear_Button(object sender, RoutedEventArgs e)
        {
            name_tb.Clear();
            surname_tb.Clear();
            phone_tb.Clear();
            email_tb.Clear();
        }
    }
}
