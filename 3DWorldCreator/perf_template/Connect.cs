using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Animations;



namespace perf_template
{
    public class Connect_
    {
        public MySqlConnection connection;

        public Connect_()
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


        public int insert_new_performance(int user_created, string name)
        {
            string sql_request = "INSERT INTO performance (user_created, name) VALUES('" + user_created.ToString() + "', '" + name + "');";

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
            return 0;
        }

        public int insert_perf_phrase(int num_in_perf,string text_russian,string text_english,int perf_id,string author_name)
        {
            string sql_request = string.Format("INSERT INTO single_perf_phrase (num_in_perf,text_russian,text_english,performance_idperformance,actor_name) VALUES('{0}','{1}','{2}','{3}','{4}');", num_in_perf, text_russian, text_english, perf_id, author_name);

            MySqlCommand command = new MySqlCommand(sql_request, connection);

            command.ExecuteNonQuery();
            return 0;
        }


        public List<SinglePerfPhrase> get_all_phrases_()
        {
            List<SinglePerfPhrase> result = new List<SinglePerfPhrase>();
            string sql = "SELECT * FROM single_perf_phrase;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                string id_single_perf_phrase = reader["idsingle_perf_phrase"].ToString();
                int num_in_perf_ = (int)reader["num_in_perf"];
                string text_russian = reader["text_russian"].ToString();
                string text_english = reader["text_english"].ToString();
                int performance_idperformance = (int)reader["performance_idperformance"];
                string actor_name = reader["actor_name"].ToString();
                string animation_idanimation = reader["animation_idanimation"].ToString();

                int animation_idanimation_ = 0;
                try
                {
                    animation_idanimation_ = Int32.Parse(animation_idanimation);
                }
                catch
                {

                }

                SinglePerfPhrase single_perf_phrase = new SinglePerfPhrase(Int32.Parse(id_single_perf_phrase), num_in_perf_, text_russian, text_english, performance_idperformance, actor_name, animation_idanimation_);

                result.Add(single_perf_phrase);
            }

            reader.Close();
            //SinglePerfPhrase result_ = result[0];
            foreach (SinglePerfPhrase res in result)
            {

                string sql1 = "SELECT CONVERT(text_russian USING utf8) FROM single_perf_phrase WHERE idsingle_perf_phrase = '" + res.id_single_perf_phrase.ToString() + "';";
                MySqlCommand command1 = new MySqlCommand(sql1, connection);
                res.text_russian = command1.ExecuteScalar().ToString();
            }

            return result;
        }

        public List<SinglePerfPhrase> get_all_phrases(int max_index)
        {
            List<SinglePerfPhrase> result = new List<SinglePerfPhrase>();
            string sql = "SELECT * FROM single_perf_phrase WHERE idsingle_perf_phrase <='" + max_index.ToString() + "';";
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                string id_single_perf_phrase = reader["idsingle_perf_phrase"].ToString();
                int num_in_perf_ = (int)reader["num_in_perf"];
                string text_russian = reader["text_russian"].ToString();
                string text_english = reader["text_english"].ToString();
                int performance_idperformance = (int)reader["performance_idperformance"];
                string actor_name = reader["actor_name"].ToString();
                string animation_idanimation = reader["animation_idanimation"].ToString();

                int animation_idanimation_ = 0;
                try
                {
                    animation_idanimation_ = Int32.Parse(animation_idanimation);
                }
                catch
                {

                }

                SinglePerfPhrase single_perf_phrase = new SinglePerfPhrase(Int32.Parse(id_single_perf_phrase), num_in_perf_, text_russian, text_english, performance_idperformance, actor_name, animation_idanimation_);

                result.Add(single_perf_phrase);
            }

            reader.Close();
            //SinglePerfPhrase result_ = result[0];
            foreach (SinglePerfPhrase res in result)
            {

                string sql1 = "SELECT CONVERT(text_russian USING utf8) FROM single_perf_phrase WHERE idsingle_perf_phrase = '" + res.id_single_perf_phrase.ToString() + "';";
                MySqlCommand command1 = new MySqlCommand(sql1, connection);
                res.text_russian = command1.ExecuteScalar().ToString();
            }

            return result;
        }


        public SinglePerfPhrase get_current_phrase(int num_in_perf,int performance_id)
        {
            List<SinglePerfPhrase> result= new List<SinglePerfPhrase>();

            string sql = "SELECT * FROM single_perf_phrase WHERE num_in_perf='"+num_in_perf.ToString()+ "' AND performance_idperformance='1'";// WHERE idanimation='70'";
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                string id_single_perf_phrase = reader["idsingle_perf_phrase"].ToString();
                int num_in_perf_ = (int)reader["num_in_perf"];
                string text_russian = reader["text_russian"].ToString();
                string text_english = reader["text_english"].ToString();
                int performance_idperformance = (int)reader["performance_idperformance"];
                string actor_name= reader["actor_name"].ToString();
                string animation_idanimation = reader["animation_idanimation"].ToString();

                int animation_idanimation_ = 0;
                try
                {
                    animation_idanimation_ = Int32.Parse(animation_idanimation);
                }
                catch
                {

                }
              
                SinglePerfPhrase single_perf_phrase = new SinglePerfPhrase(Int32.Parse(id_single_perf_phrase), num_in_perf_, text_russian, text_english, performance_idperformance, actor_name, animation_idanimation_);
                
                result.Add(single_perf_phrase);
            }
            
            reader.Close();
            SinglePerfPhrase result_ = result[0];

            string sql1 = "SELECT CONVERT(text_russian USING utf8) FROM single_perf_phrase WHERE idsingle_perf_phrase = '" + result_.id_single_perf_phrase.ToString() + "';";
            MySqlCommand command1 = new MySqlCommand(sql1, connection);
            result_.text_russian = command1.ExecuteScalar().ToString();

            return result_;
        }
        public void add_animation_to_phrase(int id_single_perf_phrase, int animation_idanimation)
        {
            string sql = "UPDATE single_perf_phrase SET animation_idanimation = '"+ animation_idanimation.ToString() + "' WHERE idsingle_perf_phrase = '" + id_single_perf_phrase.ToString() + "';";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

    }
}
