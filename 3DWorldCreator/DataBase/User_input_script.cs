using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class User_input_script
    {
        public User_input_script(int idUser_script_,int User_idUser_, string script_name_, string script_file_)
        {
            idUser_script = idUser_script_;
            User_idUser = User_idUser_;
            script_name = script_name_;
            script_file = script_file_;
        }
        public int idUser_script;
        public int User_idUser;
        public string script_name;
        public string script_file;
    }
}