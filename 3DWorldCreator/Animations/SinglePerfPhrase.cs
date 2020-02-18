using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animations
{
    public class SinglePerfPhrase
    {
        public SinglePerfPhrase(int id_single_perf_phrase_, int num_in_perf_, string text_russian_, string text_english_,int performance_idperformance_,string actor_name_,int animation_idanimation_)
        {
            id_single_perf_phrase = id_single_perf_phrase_;
            num_in_perf = num_in_perf_;
            text_russian = text_russian_;
            text_english = text_english_;
            performance_idperformance = performance_idperformance_;
            actor_name = actor_name_;
            animation_idanimation = animation_idanimation_;
        }
        public int id_single_perf_phrase;
        public int num_in_perf;
        public string text_russian;
        public string text_english;
        public int performance_idperformance;
        public string actor_name;
        public int animation_idanimation;
    }
}
