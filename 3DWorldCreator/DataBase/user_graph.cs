using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
   
        public class User_graph
        {
            public User_graph(int idUser_graph_, int User_idUser_, string graph_file_, string graph_name_)
            {
                idUser_graph = idUser_graph_;
                User_idUser = User_idUser_;
                graph_file = graph_file_;
                graph_name = graph_name_;
            }
            public int idUser_graph;
            public int User_idUser;
            public string graph_file;
            public string graph_name;
        
    }
}
