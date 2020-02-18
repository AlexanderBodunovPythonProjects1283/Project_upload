using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class User
    {
        public User(string user_name_, string user_password_){
            user_name= user_name_;
            user_password= user_password_;
    }
        public string user_name;
        public string user_password;
    }
}
