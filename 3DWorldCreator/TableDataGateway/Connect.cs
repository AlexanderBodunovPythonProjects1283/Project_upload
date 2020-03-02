using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataBase;
using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDataGateway;
using DataBase;
using System.Diagnostics;
using System.IO;

namespace TableDataGateway
{

    public class MyLocalDB
    {
        public  MySqlConnection connection1;

        public MyLocalDB()
        {
            //public MySqlConnection connection;
            string host = "localhost";
            int port = 3306;
            string database = "local_db";
            string username = "root";
            string password = "EfbgUvrq";

            String connString = "Server=" + host  //+";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            connection1 = new MySqlConnection(connString);//"datasourse=localhost;port=3306;username=root;password=EfbgUvrq");
            connection1.Open();
            //return connection;
            MySqlCommand cmd;

            string s0 = "CREATE DATABASE IF NOT EXISTS `local_db`;";
            cmd = new MySqlCommand(s0, connection1);
            cmd.ExecuteNonQuery();
            connection1.Close();

            String connString1 = "Server=" + host +";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            connection1 = new MySqlConnection(connString1);//"datasourse=localhost;port=3306;username=root;password=EfbgUvrq");
            connection1.Open();
            //return connection;

            // "package", "virtualenv_has_package", "virtualenv", "python_interpreter"
            string[] tables = { "User", "user_multicript", "user_input_script", "user_graph", "user_output_script", "user_graph_has_user_input_script", "user_graph_has_user_output_script", "performance", "single_perf_phrase", "animation" };


            foreach (string table in tables)
            {
                s0 = @"CREATE TABLE IF NOT EXISTS local_db."+ table + @" LIKE mydb."+ table + ";";
                cmd = new MySqlCommand(s0, connection1);
                cmd.ExecuteNonQuery();

                s0 = @"DELETE FROM local_db." + table + ";";
                cmd = new MySqlCommand(s0, connection1);
                cmd.ExecuteNonQuery();

                s0 = @"INSERT INTO local_db."+ table + " SELECT * FROM mydb."+ table + ";";
                cmd = new MySqlCommand(s0, connection1);
                cmd.ExecuteNonQuery();
            }
            connection1.Close();

        }



    }


    public class My_MySQLDB
    {

        public MySqlConnection connection;


        


        public My_MySQLDB()
        {
            //public MySqlConnection connection;
            string host = "localhost";
            int port = 3306;
            string database = "mydb";
            string username = "root";
            string password = "EfbgUvrq";

            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            connection = new MySqlConnection(connString);//"datasourse=localhost;port=3306;username=root;password=EfbgUvrq");
            connection.Open();
            //return connection;
        }


        


        public string get_file_content(int idUser_script,string type)
        {
            //idUser_script = 27;
            string sql = "SELECT CONVERT(script_file USING utf8) FROM User_"+ type + "_script WHERE idUser_script = " + idUser_script + ";";
            MySqlCommand command = new MySqlCommand(sql, connection);
            string result = command.ExecuteScalar().ToString();
            return result;
        }

        
        public string get_graph_content(string graph_name)
        {
            //idUser_script = 27;
            string sql = "SELECT CONVERT(graph_file USING utf8) FROM User_graph WHERE graph_name = '" + graph_name + "';";
            MySqlCommand command = new MySqlCommand(sql, connection);
            string result = command.ExecuteScalar().ToString();
            return result;
        }


        public List<User_input_script> get_users_input_scripts()
        {
            List<User_input_script> users = new List<User_input_script>();
            string sql = "SELECT * FROM User_input_script;";// WHERE user_name='"+ username + "' AND user_password ='"+ password + "';";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            //User_input_script script = new User_input_script(1, 1, "", "");

            //users.Add(script);
            
            
            while (reader.Read())
            {

                string idUser_script = reader["idUser_script"].ToString();
                int User_idUser = (int)reader["User_idUser"];
                string script_name = reader["script_name"].ToString();
                string script_file = reader["script_file"].ToString();

                //idUser_script="7";
                User_input_script script = new User_input_script(Convert.ToInt32(idUser_script), User_idUser, script_name, script_file);

                users.Add(script);
            }
            reader.Close();
            return users;
        }

        public List<User_input_script> get_users_output_scripts()
        {
            List<User_input_script> users = new List<User_input_script>();
            string sql = "SELECT * FROM User_output_script";// WHERE user_name='"+ username + "' AND user_password ='"+ password + "';";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string idUser_script = reader["idUser_script"].ToString();
                int User_idUser = (int)reader["User_idUser"];
                string script_name = reader["script_name"].ToString();
                string script_file = reader["script_file"].ToString();

                User_input_script script = new User_input_script(Convert.ToInt32(idUser_script),User_idUser, script_name, script_file);

                users.Add(script);
            }
            reader.Close();
            return users;
        }

        public List<User_graph> get_users_graphs()
        {
            List<User_graph> users = new List<User_graph>();
            string sql = "SELECT * FROM User_graph";// WHERE user_name='"+ username + "' AND user_password ='"+ password + "';";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string idUser_graph = reader["idUser_graph"].ToString();
                int User_idUser = (int)reader["User_idUser"];
                //string graph_file = reader["graph_file"].ToString();
                string graph_name = reader["graph_name"].ToString();

                User_graph script = new User_graph(Convert.ToInt32(idUser_graph), User_idUser, "", graph_name);

                users.Add(script);
            }
            reader.Close();

            foreach (User_graph user__ in users)
            {
                string graph_file = get_graph_content(user__.graph_name);
                    user__.graph_file = graph_file;
            }
            return users;
        }


        public List<User_graph_has_script> get_user_graphs_has_input_scripts(string type_)
        {
            List<User_graph_has_script> user_graphs_has_input_scripts=new List<User_graph_has_script>();

            string sql = "SELECT * FROM User_graph_has_user_"+ type_ + "_script";// WHERE user_name='"+ username + "' AND user_password ='"+ password + "';";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string User_graph_idUser_graph = reader["User_graph_idUser_graph"].ToString();
                string User_input_script_idUser_script = reader["User_"+ type_ + "_script_idUser_script"].ToString();

                int User_graph_idUser_graph_ = Int32.Parse(User_graph_idUser_graph);
                int User_input_script_idUser_script_ = Int32.Parse(User_input_script_idUser_script);

                //string script_name = get_input_script_name_by_id(User_input_script_idUser_script);
                User_graph_has_script graph_script = new User_graph_has_script(User_graph_idUser_graph_, User_input_script_idUser_script_, "1");
                user_graphs_has_input_scripts.Add(graph_script);
            }
            reader.Close();

            foreach (User_graph_has_script graph_script in user_graphs_has_input_scripts)
            {
                string script_name = get_input_script_name_by_id(graph_script.User_script_idUser_script, type_);
                graph_script.script_name = script_name;
            }

            

            return user_graphs_has_input_scripts;

        }

        public List<virtualenv> get_virtualenvs()
        {
            List<virtualenv> virtualenvs = new List<virtualenv>();

            string sql = "SELECT * FROM virtualenv";
            MySqlCommand command = new MySqlCommand(sql, connection);
           
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string name = reader["name"].ToString();
                string python_interpreter_idPython_interpreter = reader["python_interpreter_idPython_interpreter"].ToString();
                string verbal=reader["verbal"].ToString();

                int python_interpreter_idPython_interpreter_ = Int32.Parse(python_interpreter_idPython_interpreter);


                //string script_name = get_input_script_name_by_id(User_input_script_idUser_script);
                virtualenv env = new virtualenv(name, python_interpreter_idPython_interpreter_, verbal);
                virtualenvs.Add(env); 
            }
            reader.Close();
            return virtualenvs;

        }

        public string get_input_script_name_by_id(int id__, string type_)
        {
            //idUser_script = 27;
            string sql = "SELECT script_name FROM User_"+ type_ + "_script WHERE idUser_script = " + id__.ToString() + ";";
            MySqlCommand command = new MySqlCommand(sql, connection);
            string result = command.ExecuteScalar().ToString();
            return result;
        }

        public List<User_graph_has_script> get_user_graphs_has_output_scripts()
        {
            List<User_graph_has_script> user_graphs_has_output_scripts;

            return user_graphs_has_output_scripts= new List<User_graph_has_script>();
        }

        public void insert_user_graph_has_output_script(int User_graph_idUser_graph, int User_output_script_idUser_script)
        {
            string sql_request = "INSERT INTO User_graph_has_user_output_script (User_graph_idUser_graph, User_output_script_idUser_script) VALUES('" + User_graph_idUser_graph + "', '" + User_output_script_idUser_script + "');";

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
            //return 0;
        }

        public void insert_user_graph_has_input_script(int User_graph_idUser_graph, int User_input_script_idUser_script)
        {
            string sql_request = "INSERT INTO User_graph_has_user_input_script (User_graph_idUser_graph, User_input_script_idUser_script) VALUES('" + User_graph_idUser_graph + "', '" + User_input_script_idUser_script + "');";

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
            //return 0;
        }

        public void insert_virt_env(string python_interpreter_idPython_interpreter,string name, string verbal)
        {
            string sql_request = "INSERT INTO virtualenv (name, python_interpreter_idPython_interpreter,verbal) VALUES('" + name + "', '" + 1 + "','" + verbal + "');";

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
        }




        public int insert_user_graph_script(string User_idUser, string graph_name, string graph_file)
        {
            string sql_request = "INSERT INTO User_graph (User_idUser, graph_file, graph_name) VALUES('" + User_idUser + "', '" + graph_file + "', '" + graph_name + "');";

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
            return 0;
        }

        public int insert_user_input_script(string User_idUser, string script_name_, string script_file_)
        {
            string path_ = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "\"") + "code.txt";
            string sql_request = "INSERT INTO User_input_script (User_idUser, script_name, script_file) VALUES('" + User_idUser + "', '" + script_name_ + "', '" + script_file_ + "');";

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
            return 0;
        }

        public int insert_user_output_script(string User_idUser, string script_name_, string script_file_)
        {
            string sql_request = "INSERT INTO User_output_script (User_idUser, script_name, script_file) VALUES('" + User_idUser + "', '" + script_name_ + "', '" + script_file_ + "');";

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
            return 0;
        }


    }
}
