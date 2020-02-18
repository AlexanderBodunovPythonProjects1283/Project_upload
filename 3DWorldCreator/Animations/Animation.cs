using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animations
{
    public class Animation
    {
        public Animation(int idanimation_, string animation_name_,int animation_number_,string russian_description1_)
        {
            idanimation = idanimation_;
            animation_name = animation_name_;
            animation_number = animation_number_;
            russian_description1 = russian_description1_;
        }
        public int idanimation;
        public string animation_name;
        public int animation_number;
        public string russian_description1;
    }
}
