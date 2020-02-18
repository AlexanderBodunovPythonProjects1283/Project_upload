using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class User_graph_has_script
    {
        public User_graph_has_script(int User_graph_idUser_graph_, int User_script_idUser_script_,string script_name_)
        {
            User_graph_idUser_graph = User_graph_idUser_graph_;
            User_script_idUser_script = User_script_idUser_script_;
            script_name = script_name_;
        }
        public int User_graph_idUser_graph;
        public int User_script_idUser_script;
        public string script_name;
    }
}
