using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace perf_template
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        Connect_ database_ = new Connect_();



        private void button2_Click(object sender, EventArgs e)
        {
            //database_.insert_new_performance(21, textBox1.Text);
            string actors_rus = richTextBox3.Text;
            string[] arr_actors_rus = Regex.Split(actors_rus, @"\n");

            string actors_en = richTextBox4.Text;
            string[] arr_actors_en = Regex.Split(actors_en, @"\n");

            string text_rus = richTextBox1.Text;
            string[] phrases_rus = Regex.Split(text_rus, @"\n");
            string text_en = richTextBox2.Text;
            string[] phrases_en = Regex.Split(text_en, @"\n");


            //List<Regex> templ_ru = new List<Regex>();
            //List<Regex> templ_en = new List<Regex>();
            List<string> templ_ru = new List<string>();
            List<string> templ_en = new List<string>();
            foreach (string phrase in phrases_rus)
            {
                //templ_ru.Add(new Regex(string.Concat(@"", phrase)));
                templ_ru.Add(phrase);
            }

            foreach (string phrase in phrases_rus)
            {
                //templ_en.Add(new Regex(string.Concat(@"", phrase)));
                templ_en.Add(phrase);
            }


            for (int i = 1; i < phrases_rus.Length; i++)
            {
                string phrase_rus = phrases_rus[i];
                string phrase_en = phrases_en[i];
                int matched_regex = 0;
                for (int j = 0; j < arr_actors_rus.Length; j++)
                {
                    //Match match = Regex.Match(phrase_rus, templ_ru[j]); //templ_ru[j].Match(phrase_rus);
                    //if (match.Success)
                    //{
                        //MessageBox.Show(arr_actors_rus[j].ToString());
                        //matched_regex = j;
                        //MessageBox.Show(i.ToString()+"  "+matched_regex.ToString());
                    //}
                    if (phrase_rus.StartsWith(arr_actors_rus[j]))
                    {
                       matched_regex = j;
                        //MessageBox.Show(i.ToString() + "  " + matched_regex.ToString());
                    }
                }
                
                string phrase_cut_ru = phrase_rus.Substring(arr_actors_rus[matched_regex].Length, phrase_rus.Length - arr_actors_rus[matched_regex].Length);
                //MessageBox.Show(phrase_cut);
                Match match1 = Regex.Match(phrase_en, templ_en[matched_regex]);//templ_en[matched_regex].Match(phrase_en);
                string phrase_cut_en = phrase_en.Substring(arr_actors_en[matched_regex].Length, phrase_en.Length - arr_actors_en[matched_regex].Length);
                //MessageBox.Show((match1.Index == 0).ToString());

                database_.insert_perf_phrase(i, phrase_cut_ru.Replace(@"'", ""), phrase_cut_en.Replace(@"'", ""), 1, arr_actors_en[matched_regex]);
                
            }
            build_graph_text(phrases_rus.Length);
        }
        private void build_graph_text(int length__)
        {
            string graph_ = "links_sub=[";
            for (int i = 1; i < length__; i++)
            {
                graph_ += "{ 'from_':"+i.ToString()+",'to_':"+ (i+1).ToString() + ",'label_':'q1','action_':'"+ (i + 1).ToString() + "','wait':'3'},\n";
            }
            graph_ += "]";
            System.IO.File.WriteAllText(@"C://Temp//files//perf//graph_current.py", graph_);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
