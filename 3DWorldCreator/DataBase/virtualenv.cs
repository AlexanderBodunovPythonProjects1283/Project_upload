using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class virtualenv
    {
        public virtualenv(string name_, int python_interpreter_idPython_interpreter_, string verbal_)
        {
            name= name_;
            python_interpreter_idPython_interpreter= python_interpreter_idPython_interpreter_;
            verbal= verbal_;
        }
        public string name;
        public int python_interpreter_idPython_interpreter;
        public string verbal;
    }
}
