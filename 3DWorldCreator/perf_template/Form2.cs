using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Animations;

namespace perf_template
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox2.TextChanged+= new EventHandler(textBox2_TextChanged);
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            get_current_phrase();
        }

        List<Animations.Animation> animations;
        List<Animations.Animation> animations_all;
        List<Animations.SinglePerfPhrase> single_perf_phrases;
        List<Animations.SinglePerfPhrase> single_perf_phrases_all;

        Animation animation;
        int id_animation;
        SinglePerfPhrase single_perf_phrase;
        int phrase_index=1;
        int max_id = 1;

        Animations.Connect_ anim_connect = new Animations.Connect_();
        perf_template.Connect_ scripts_connect = new perf_template.Connect_();
        Connect_ native_connect = new Connect_();



        private void get_current_phrase()
        {
            single_perf_phrase = native_connect.get_current_phrase(phrase_index,1);
            //MessageBox.Show(single_perf_phrase.id_single_perf_phrase.ToString());

            richTextBox1.Text = single_perf_phrase.text_russian;
            textBox1.Text = single_perf_phrase.actor_name;
            textBox3.Text = anim_connect.get_animation_name(single_perf_phrase.animation_idanimation);

            max_id = Math.Max(single_perf_phrase.id_single_perf_phrase,max_id);

            single_perf_phrases = native_connect.get_all_phrases(max_id);
            listView1.Items.Clear();
            foreach (SinglePerfPhrase spf in single_perf_phrases)
            {
                ListViewItem newitem = new ListViewItem(spf.text_russian);
                newitem.SubItems.Add(anim_connect.get_animation_name(spf.animation_idanimation));
                listView1.Items.Add(newitem);
            }

        }


        private void get_all_animations(object sender, EventArgs e)
        {
            //animations_all=anim_connect.get_all_animations()
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            var2 = comboBox1.SelectedIndex;


            animation = animations[var2];
            textBox3.Text = animation.animation_name;
            id_animation = animation.idanimation;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
             int var2 = 0;
            if (listView1.SelectedItems.Count > 0)
            {
                var2 = listView1.Items.IndexOf(listView1.SelectedItems[0]);
            }
            else
            {
                return;
            }
            var spf = single_perf_phrases[var2];
            phrase_index = spf.num_in_perf;
            get_current_phrase();

            }


private void textBox2_TextChanged(object sender, EventArgs e)
        {
            comboBox1.DroppedDown = true;
            comboBox1.Items.Clear();
            string search_phrase = textBox2.Text;
            animations = anim_connect.get_animations(search_phrase);

            //MessageBox.Show(animations.Count.ToString());
            foreach (Animations.Animation anim in animations)
            {
                string text__ = anim.animation_name;
                //Encoding utf8 = Encoding.GetEncoding("UTF-8");
                //Encoding win1251 = Encoding.GetEncoding("Windows-1251");

                //byte[] utf8Bytes = win1251.GetBytes(anim.russian_description1);
                // byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);

                //string text__= win1251.GetString(win1251Bytes);

                textBox3.Text = text__;
                comboBox1.Items.Add(text__);
            }
        }


            private void button1_Click(object sender, EventArgs e)
        {
            string search_phrase = textBox2.Text;
            animations =anim_connect.get_animations(search_phrase);

            MessageBox.Show(animations.Count.ToString());
            foreach(Animations.Animation anim in animations)
            {
                string text__ = anim.animation_name;
                //Encoding utf8 = Encoding.GetEncoding("UTF-8");
                //Encoding win1251 = Encoding.GetEncoding("Windows-1251");

                //byte[] utf8Bytes = win1251.GetBytes(anim.russian_description1);
                // byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);

                //string text__= win1251.GetString(win1251Bytes);

                textBox3.Text = text__;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            native_connect.add_animation_to_phrase(single_perf_phrase.id_single_perf_phrase, id_animation);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            phrase_index++;
            get_current_phrase();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            single_perf_phrases_all = native_connect.get_all_phrases_();
            build_graph_text();
        }

        private void build_graph_text()
        {
            string graph_ = "links_sub=[";
            graph_ += "{ 'from_':0,'to_':1,'label_':'q1','action_':1,'animation':'" + anim_connect.get_animation_number(single_perf_phrases_all[0].animation_idanimation) + "','wait':'3'},\n";
            for (int i = 0; i < single_perf_phrases_all.Count; i++)
            {
                string animation_number = anim_connect.get_animation_number(single_perf_phrases_all[i].animation_idanimation);
                graph_ += "{ 'from_':" + (i+1).ToString() + ",'to_':" + (i + 2).ToString() + ",'label_':'q1','action_':'" + (i + 2).ToString() + "','animation':'"+ animation_number + "','wait':'3'},\n";
            }
            graph_ += "]";
            System.IO.File.WriteAllText(@"C://Temp//files//perf//graph_current.py", graph_);
        }
    }
}
