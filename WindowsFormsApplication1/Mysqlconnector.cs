using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Add MySql Library
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace MysqlConnector
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
            OpenConnection();
            
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "company_employee";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);

        }

        //open connection to database
        private bool OpenConnection()
        {

            try
            {
                connection.Open();
                MessageBox.Show("Connection Established");
                Console.WriteLine("connection established");
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Connection to database failed");
                return false;
            }

        }

        //Insert statement
        public void Insert(string name,int age,string address)
        {
            
            string cmdText = "INSERT INTO  employeinfo(name,age,address) VALUES (@person, @personage,@personaddress)";
            MySqlCommand cmd = new MySqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@person", name);
            cmd.Parameters.AddWithValue("@personage", age);
            cmd.Parameters.AddWithValue("@personaddress", address);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Close();
            }
            catch(System.InvalidOperationException)
            {
                MessageBox.Show("Invalid operation");
                
            }
            
            
            
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public List<string>GetCustomerInfo()
        {
            string query = "SELECT * FROM employeinfo";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
 
            list[1] = new List<string>();
            list[2] = new List<string>();
            List<string> datalist = new List<string>();
            
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
            
              
            
            
            
               
            
            
            
            
//Create a data reader and Execute the command
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    int i = 0;
                    //Read the dat/a and store them in the list
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["name"] + "");
                        list[1].Add(dataReader["age"] + "");
                        list[2].Add(dataReader["address"] + "");

                        datalist.Add(dataReader["name"] + "");

                        datalist.Add(dataReader["age"] + "");
                        datalist.Add(dataReader["address"] + "");
                    }

                    //close Data Reader
                    dataReader.Close();

                }
             catch(System.InvalidOperationException)
                {
                    MessageBox.Show("Invalid operation");
                   
             }
                //close Connection
                //this.CloseConnection();

                //return list to be displayed
                return datalist;
           

        }

        //Count statement
        // public int Count()
        //{
        //}

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }

}
