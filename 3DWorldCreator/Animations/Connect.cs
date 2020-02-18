using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Animations
{
    public class Connect_
    {
        public MySqlConnection connection;
        public Connect_()
        {
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

        public List<Animation> get_animations(string search_phrase)
        {
            List<Animation> anims = new List<Animation>();
            string sql = "SELECT * FROM Animation WHERE MATCH (animation_name) AGAINST ('"+ search_phrase + "')";// WHERE idanimation='70'";
            MySqlCommand command = new MySqlCommand(sql, connection);
            //connection.Open();
            // выполняем запрос и получаем ответ
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int idanimation = (int)reader["idanimation"];
                string animation_name = reader["animation_name"].ToString();
                string animation_number = reader["animation_number"].ToString().Replace("_","");
                string russian_description1 = reader["russian_description_1"].ToString();

                Animation anim = new Animation(idanimation, animation_name, Int32.Parse(animation_number), russian_description1);

                anims.Add(anim);
            }
            reader.Close();
            return anims;
        }

        public string get_animation_name(int id_animation)
        {
            string result="";
            string sql = "SELECT animation_name FROM Animation where idanimation="+ id_animation.ToString()+ ";";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                result = command.ExecuteScalar().ToString();
            }
            catch
            {

            }
            return result;
        }


        public string get_animation_number(int id_animation)
        {
            string result = "";
            string sql = "SELECT animation_number FROM Animation where idanimation=" + id_animation.ToString() + ";";
            MySqlCommand command = new MySqlCommand(sql, connection);
            try
            {
                result = command.ExecuteScalar().ToString();
            }
            catch
            {

            }
            return result;
        }

    }
}
