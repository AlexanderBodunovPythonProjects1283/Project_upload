﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDataGateway;
using DataBase;
using perf_template;
using System.Diagnostics;
using System.IO;
using _3DWorldCreator;
using System.Text.RegularExpressions;
using System.Management.Automation;




namespace _3DWorldCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //MessageBox.Show("1");
            InitializeComponent();
            listView6.SelectedIndexChanged += new EventHandler(listView6_SelectedIndexChanged);
            listView5.SelectedIndexChanged += new EventHandler(listView5_SelectedIndexChanged);
            listView13.SelectedIndexChanged += new EventHandler(input_script_SelectedIndexChanged);
            listView11.SelectedIndexChanged += new EventHandler(graph_SelectedIndexChanged);
            listView10.SelectedIndexChanged += new EventHandler(output_script_SelectedIndexChanged);

            listView9.SelectedIndexChanged += new EventHandler(input_network_SelectedIndexChanged);
            
            listView8.SelectedIndexChanged += new EventHandler(output_network_SelectedIndexChanged);

            listView12.SelectedIndexChanged += new EventHandler(graph_graph_SelectedIndexChanged);
            listView19.SelectedIndexChanged += new EventHandler(virt_env_SelectedIndexChanged);

            timer1.Enabled = true;
            timer1.Tick += new System.EventHandler(timer1_handle);

            //listView19.Items.Add("tensorflow версии 1.13.1");
            //listView19.Items.Add("tensorflow версии 2.0");

            webBrowser1.ScriptErrorsSuppressed = false;
            webBrowser2.ScriptErrorsSuppressed = false;
            webBrowser3.ScriptErrorsSuppressed = false;
            webBrowser4.ScriptErrorsSuppressed = false;
            webBrowser5.ScriptErrorsSuppressed = false;
            webBrowser6.ScriptErrorsSuppressed = false;
            webBrowser7.ScriptErrorsSuppressed = false;
            webBrowser8.ScriptErrorsSuppressed = false;
            webBrowser9.ScriptErrorsSuppressed = false;

            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.button12, "My button1");

            richTextBox6.Text = "Подсказка:\n1)выберите прогу промежуточного слоя\n2) нажмите не нее ЛКМ\n3) выберите проги входного и выходного слоя\nРезультат:\nПроги входного и выходного слоя будут привязаны к графам";
        // groupBox7.toolTip.SetToolTip(this, "Всплывающая подсказка на форме");


            update_All();
            

        }
        List<User_input_script> user_input_script;
        List<User_input_script> user_output_script;
        List<User_graph> user_graph;
        List<User_graph_has_script> user_graphs_has_input_scripts;
        List<User_graph_has_script> user_graphs_has_output_scripts;
        List<virtualenv> virtualenvs;

        int selected_input_script = 0;
        int selected_graph = 1;
        int selected_output_script = 0;
        int selected_env = 0;

        string output = "";

        Python_colors py_col = new Python_colors();



        public void update_All()
        {
            
            listView6.Items.Clear();
            listView12.Items.Clear();
            listView11.Items.Clear();
            listView9.Items.Clear();
            listView8.Items.Clear();
            user_input_script = database_.get_users_input_scripts();

            //int is_connection = database_.check_conn();
            //MessageBox.Show(is_connection);

            foreach (User_input_script script in user_input_script)
            {
                listView13.Items.Add(script.script_name);
                listView6.Items.Add(script.script_name);
                listView9.Items.Add(script.script_name);

            }

            listView5.Items.Clear();
            user_output_script = database_.get_users_output_scripts();

            foreach (User_input_script script in user_output_script)
            {
                listView10.Items.Add(script.script_name);
                listView5.Items.Add(script.script_name);
                listView8.Items.Add(script.script_name);

            }

            user_graph= database_.get_users_graphs();
            foreach (User_graph graph in user_graph)
            {
                listView11.Items.Add(graph.graph_name);
                listView12.Items.Add(graph.graph_name);
            }
            virtualenvs = database_.get_virtualenvs();
            foreach (virtualenv env in virtualenvs)
            {
                listView19.Items.Add(env.verbal);
            }


        }

        private void output_text_with_sintax()
        {
            string[] templates_blue = new string[] { @"False", @"=True", @"=None", @" and ", @"with ", @" as ", @"assert ", @"break", @"class ", @"continue", @"def ", @"del", @"elif", @"else", @"except", @"finally", @"for ", @"from ", @"global ", @"if", @"import", @" in", @" is", @"lambda ", @"nonlocal", @"not ", @" or ", @"pass", @"raise ", @"return ", @"try", @"while ", @"yield ", @"print" };
            string temlpate_gray = @"#(\S+)\s?";

            foreach (string temp in templates_blue)
            {
                MatchCollection allIp = Regex.Matches(richTextBox1.Text, temp);
                foreach (Match ip in allIp)
                {
                    richTextBox1.SelectionStart = ip.Index;
                    richTextBox1.SelectionLength = ip.Length;
                    richTextBox1.SelectionColor = Color.FromArgb(0, 0, 255);
                }
            }

            MatchCollection allIp1 = Regex.Matches(richTextBox1.Text, temlpate_gray);
            foreach (Match ip in allIp1)
            {
                richTextBox1.SelectionStart = ip.Index;
                richTextBox1.SelectionLength = 10;// ip.Length;
                richTextBox1.SelectionColor = Color.FromArgb(100, 100, 100);
            }
        }


        private void output_text_with_sintax1()
        {
            string[] templates_blue = new string[] { @"False", @"=True", @"=None", @" and ", @"with ", @" as ", @"assert ", @"break", @"class ", @"continue", @"def ", @"del", @"elif", @"else", @"except", @"finally", @"for ", @"from ", @"global ", @"if", @"import", @" in", @" is", @"lambda ", @"nonlocal", @"not ", @" or ", @"pass", @"raise ", @"return ", @"try", @"while ", @"yield ", @"print" };
            string temlpate_gray = @"#(\S+)\s?";

            foreach (string temp in templates_blue)
            {
                MatchCollection allIp = Regex.Matches(richTextBox2.Text, temp);
                foreach (Match ip in allIp)
                {
                    richTextBox2.SelectionStart = ip.Index;
                    richTextBox2.SelectionLength = ip.Length;
                    richTextBox2.SelectionColor = Color.FromArgb(0, 0, 255);
                }
            }

            MatchCollection allIp1 = Regex.Matches(richTextBox2.Text, temlpate_gray);
            foreach (Match ip in allIp1)
            {
                richTextBox2.SelectionStart = ip.Index;
                richTextBox2.SelectionLength = 10;// ip.Length;
                richTextBox2.SelectionColor = Color.FromArgb(100, 100, 100);
            }
        }


        private void update_script_graph()
        {
            user_graphs_has_input_scripts = database_.get_user_graphs_has_input_scripts("input");
            user_graphs_has_output_scripts = database_.get_user_graphs_has_input_scripts("output");

            List<string> list_user_graphs_has_input_script;
            List<string> list_user_graphs_has_output_script;


            StreamWriter f = new StreamWriter("C:\\Temp\\PycharmProjects\\treehouse_os\\inputs.txt");
            int i__ = 1;
            foreach (User_graph_has_script user_graphs_has_input_script in user_graphs_has_input_scripts)
            {
                f.WriteLine(user_graphs_has_input_script.script_name);
                //f.WriteLine(i__.ToString());
                //i__++;
            }
            f.Close();

            int i___ = 4;
            StreamWriter f1 = new StreamWriter("C:\\Temp\\PycharmProjects\\treehouse_os\\outputs.txt");
            foreach (User_graph_has_script user_graphs_has_output_script in user_graphs_has_output_scripts)
            {
                f1.WriteLine(user_graphs_has_output_script.script_name);
                //f1.WriteLine(i___.ToString()+"o");
                //i___++;
            }
            
            f1.Close();

            
            Bitmap MyImage1;
            MyImage1 = LoadBitmap_("C:\\Temp\\PycharmProjects\\treehouse_os\\example2_graph1.png");
            pictureBox5.Image = MyImage1;
            System.IO.File.Delete("C:\\Temp\\PycharmProjects\\treehouse_os\\example2_graph.png");
            string command = "/C C:\\Temp\\PycharmProjects\\treehouse_system\\venv\\Scripts\\graph_build.bat";
            var process__=System.Diagnostics.Process.Start("cmd.exe", command);
            process__.WaitForExit();
            process__.Close();

            /*
            string command = "/C cd C:\\Temp\\PycharmProjects\\treehouse_system\\venv\\Scripts";
            System.Diagnostics.Process.Start("cmd.exe",   command);

            string command2 = "/C python C:\\Temp\\PycharmProjects\\treehouse_os\\build_graph_25_01_20.py";
            var process = System.Diagnostics.Process.Start("cmd.exe", command2);
            process.WaitForExit();
            string command1 = "ping google.com -t";
            //System.Diagnostics.Process.Start("cmd.exe", "/C " + command1);
            */

        }

        public Bitmap LoadBitmap_(string path)
        {
            if (File.Exists(path))
            {
                // open file in read only mode
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                // get a binary reader for the file stream
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // copy the content of the file into a memory stream
                    var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                    // make a new Bitmap object the owner of the MemoryStream
                    return new Bitmap(memoryStream);
                }
            }
            else
            {
                MessageBox.Show("Error Loading File.", "Error!", MessageBoxButtons.OK);
                return null;
            }
        }

        

        private void graph_graph_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            if (listView12.SelectedItems.Count > 0)
            {
                var2 = listView12.Items.IndexOf(listView12.SelectedItems[0]);
            }
            else
            {
                return;
            }
            var user_graph__ = user_graph[var2];


            //string file_content = database_.get_graph_content(user_output_script[var2].idUser_script, "output");
            string content = user_graph__.graph_file;
            File.WriteAllText("C:\\Temp\\PycharmProjects\\treehouse_system\\graph_current.py", content);
            richTextBox3.Text = content;

            //System.IO.File.Delete("C:\\Temp\\PycharmProjects\\treehouse_os\\example2_graph.png");
            string command = "/C C:\\Temp\\PycharmProjects\\treehouse_system\\venv\\Scripts\\graph_visual.bat";
            var process__ =System.Diagnostics.Process.Start("cmd.exe", command);
            process__.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process__.WaitForExit();
            process__.Close();

            Bitmap MyImage2;
            
                MyImage2 = LoadBitmap_("C:\\Temp\\PycharmProjects\\treehouse_system\\example2_graph1.png");
                pictureBox4.Image = MyImage2;
            
        }

        private void virt_env_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            if (listView19.SelectedItems.Count > 0)
            {
                var2 = listView19.Items.IndexOf(listView19.SelectedItems[0]);
            }
            else
            {
                return;
            }
            selected_env = var2;
            get_packages();

        }

        private void output_script_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            if (listView10.SelectedItems.Count > 0)
            {
                var2 = listView10.Items.IndexOf(listView10.SelectedItems[0]);
            }
            else
            {
                return;
            }
            var output_script__ = user_output_script[var2];
            var graph__ = user_graph[selected_graph];
            database_.insert_user_graph_has_output_script(graph__.idUser_graph, output_script__.idUser_script);
            //MessageBox.Show("sucsess_o");
            update_script_graph();
            show_graph();
        }

        private void graph_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int var2 = 0;
            if (listView11.SelectedItems.Count > 0)
            {
                selected_graph = listView11.Items.IndexOf(listView11.SelectedItems[0]);
            }
            else
            {
                return;
            }
        }


        private void input_script_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            if (listView13.SelectedItems.Count > 0)
            {
                var2 = listView13.Items.IndexOf(listView13.SelectedItems[0]);
            }
            else
            {
                return;
            }
            var input_script__ = user_input_script[var2];
            var graph__ = user_graph[selected_graph];
            database_.insert_user_graph_has_input_script(graph__.idUser_graph, input_script__.idUser_script);
            //MessageBox.Show("sucsess_i");
            update_script_graph();
            show_graph();

        }

        

        private void input_network_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            if (listView9.SelectedItems.Count > 0)
            {
                var2 = listView9.Items.IndexOf(listView9.SelectedItems[0]);
            }
            else
            {
                return;
            }
            string file_content = database_.get_file_content(user_input_script[var2].idUser_script, "input");
            //string s = System.IO.File.ReadAllText(@"C://Temp//Myscripts//1.txt");
            richTextBox2.Text = file_content;
            File.WriteAllText(@"C:\Users\alexa\source\repos\3DWorldCreator\current_network.py", file_content, Encoding.UTF8);
            output_text_with_sintax1();

        }

        private void output_network_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            if (listView9.SelectedItems.Count > 0)
            {
                var2 = listView9.Items.IndexOf(listView9.SelectedItems[0]);
            }
            else
            {
                return;
            }
            string file_content = database_.get_file_content(user_output_script[var2].idUser_script, "output");
            //string s = System.IO.File.ReadAllText(@"C://Temp//Myscripts//1.txt");
            richTextBox2.Text = file_content;

        }



        private void listView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int var2 = 0;
            if (listView5.SelectedItems.Count > 0)
            {
                var2 = listView5.Items.IndexOf(listView5.SelectedItems[0]);
            }
            else
            {
                return;
            }
            //MessageBox.Show(user_input_script[var2].idUser_script.ToString());
            string file_content = database_.get_file_content(user_output_script[var2].idUser_script, "output");
            //string s = System.IO.File.ReadAllText(@"C://Temp//Myscripts//1.txt");
            richTextBox1.Text = file_content;

            output_text_with_sintax();
        }

            private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Объявляем переменную lb и приводим компонент, вызвавший событие к типу ListBox
            //(у нас же событие сгененрировал ListBox1, верно? А он имеет тип ListBox.
            var lb = sender as ListView;
            
            int var2 = 0;
            if (listView6.SelectedItems.Count > 0)
            {
                var2 = listView6.Items.IndexOf(listView6.SelectedItems[0]);
            }
            else
            {
                return;
            }
            //MessageBox.Show(user_input_script[var2].idUser_script.ToString());
            string file_content = database_.get_file_content(user_input_script[var2].idUser_script,"input");

            File.WriteAllText(@"C:\Users\alexa\source\repos\3DWorldCreator\current_script.py", file_content, Encoding.UTF8);

            //string s = System.IO.File.ReadAllText(@"C://Temp//Myscripts//1.txt");
            richTextBox1.Text = file_content;

            output_text_with_sintax();

        }

        public My_MySQLDB database_ = new My_MySQLDB();

        private void listView18_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                string Path_ = ofd.FileName;
                string fileName = Path.GetFileName(Path_);


                string code = System.IO.File.ReadAllText(Path_);
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "code.txt", code);
                //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "code.txt");
                database_.insert_user_input_script(21.ToString(), fileName, code);
                update_All();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                string Path_ = ofd.FileName;
                string fileName = Path.GetFileName(Path_);


                string code = System.IO.File.ReadAllText(Path_);
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "code.txt", code);
                //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "code.txt");
                database_.insert_user_output_script(21.ToString(), fileName, code);
                update_All();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string to_file=richTextBox1.Text+ "\n import msvcrt \n msvcrt.getch()";
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "exec.py", to_file);
            string path_to_save = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "exec.py";
            string command_to_run = @"C:\Temp\Python.lnk "+AppDomain.CurrentDomain.BaseDirectory + "exec.py";
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory.Replace("\\","//") + "exec.py");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false

                }
            };
            process.Start();

            using (StreamWriter pWriter = process.StandardInput)
            {
                if (pWriter.BaseStream.CanWrite)
                {
                        pWriter.WriteLine(command_to_run);
                }
            }
        }

        private void show_graph()
        {
            Bitmap MyImage;
            MyImage = LoadBitmap_("C:\\Temp\\PycharmProjects\\treehouse_os\\example2_graph.png");
            int width = MyImage.Width;
            int height = MyImage.Height;

            if (width>855 || height > 586)
            {
                //pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                //pictureBox5.SizeMode = PictureBoxSizeMode.Normal;
            }


            pictureBox5.Image = MyImage;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string command = "/C C:\\Temp\\PycharmProjects\\treehouse_system\\venv\\Scripts\\graph_build.bat";
            System.Diagnostics.Process.Start("cmd.exe", command);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                string Path_ = ofd.FileName;
                string fileName = Path.GetFileName(Path_);


                string code = System.IO.File.ReadAllText(Path_);
                //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "code.txt", code);
                //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "//") + "code.txt");
                database_.insert_user_graph_script(21.ToString(), fileName, code);
                update_All();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            user_graphs_has_input_scripts = database_.get_user_graphs_has_input_scripts("input");
            user_graphs_has_output_scripts = database_.get_user_graphs_has_input_scripts("output");


            string string_standart_import = @"# -*- coding: utf-8 -*-
import pydot
import sys
sys.path.append('C:/Users/nick/Desktop/my/PycharmProjects/behavior_Neo4j/triggers/')
import cv2
import sys
import os
sys.path.append('C:/Users/nick/Desktop/my/PycharmProjects/behavior_Neo4j/triggers/')
sys.path.append('C:/Temp/PycharmProjects/treehouse_system/')
sys.path.append('C:/Temp/files/builded/')
os.environ['PATH'] += os.pathsep + 'C:/Program Files (x86)/Graphviz2.38/bin/'
from graph_current import links_sub
import time";
            string string_loop = System.IO.File.ReadAllText("C:\\Temp\\PycharmProjects\\treehouse_system\\short_func_caller.py");
            

            string string_import = "\n";


            string string_priority = "priority=[";
            string string_cals = "cals=[";
            string string_arr_func = "c=[";

            Random rnd = new Random();


            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //Для выделения пути к каталогу, воспользуйтесь `System.IO.Path`:
            string path_ex =AppDomain.CurrentDomain.BaseDirectory;
            string prg_name = textBox10.Text;
            string path = path_ex.Substring(0, path_ex.Length - 25)+@"Projects\"+ prg_name;
            MessageBox.Show(path);



            DirectoryInfo dirInfo = new DirectoryInfo(path);
            //if (dirInfo.Exists)
            //{
                
                MessageBox.Show(path);
                DirectoryInfo drInfo = new DirectoryInfo(path+ "\\code\\builded");
                drInfo.Create();

                //Directory.CreateDirectory(path + "\\" + prg_name + "\\code");
                //Directory.CreateDirectory(path + "\\" + prg_name + "\\code\\builded");
            //}
            //else
            //{
                //MessageBox.Show(":(");
            //}
            


            int i__ = 1;
            foreach (User_graph_has_script user_graphs_has_input_script in user_graphs_has_input_scripts)
            {
                string file_content_ = database_.get_file_content(user_graphs_has_input_script.User_script_idUser_script, "input");
                System.IO.File.WriteAllText(path+"\\code\\builded\\" +i__.ToString()+"i.py", file_content_);
                string_import += "from builded import i"+ i__.ToString() + "\n";
                string_priority += rnd.Next(1,10).ToString() + ",";
                string_cals += "0,";
                string_arr_func += "i" + i__.ToString() + ".i" + i__.ToString()+",";
                i__++;
            }

            string_priority += "0]\n";
            string_cals += "0]\n";
            string_arr_func += "0]\n";

            i__ = 1;
            //int i___ = 4;
            //StreamWriter f1 = new StreamWriter("C:\\Temp\\PycharmProjects\\treehouse_os\\outputs.txt");
            foreach (User_graph_has_script user_graphs_has_output_script in user_graphs_has_output_scripts)
            {
                string file_content__ = database_.get_file_content(user_graphs_has_output_script.User_script_idUser_script, "output");
                System.IO.File.WriteAllText(path + "\\code\\builded" + i__.ToString() + "o.py", file_content__);
                string_import += "from builded import o" + i__.ToString() + "\n";
                i__++;
            }
            System.IO.File.WriteAllText(path + "\\code\\builded\\main.py", string_standart_import + string_import + string_priority + string_cals+ string_arr_func+ string_loop);






        }

        private void button21_Click(object sender, EventArgs e)
        {
            perf_template.Form1 perf_template = new perf_template.Form1();
            perf_template.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //WinFormsServer.FrmServer formServer = new WinFormsServer.FrmServer();
            //formServer.Show();
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate(@"C:\Users\alexa\Desktop\diplom\333.html");
            //webBrowser1.Navigate(@"youtube.com");

            base.OnLoad(e);
            var embed = "<html><head>" +
            "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
            "</head><body>" +
            "<iframe  src=\"{0}\"" +
            "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
             "<iframe  src=\"{1}\"" +
            "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
            "</body></html>";
            var url = "https://www.youtube.com/embed/AV-H2TBG1Vc";
            var url2 = "https://www.youtube.com/embed/AV-H2TBG1Vc";
            this.webBrowser1.DocumentText = string.Format(embed, url,url2);


        }

        private void textBox6_Enter(object sender, EventArgs e)//происходит когда элемент стает активным
        {
            textBox6.Text = null;
            textBox6.ForeColor = Color.Black;
        }


        

        private void button37_Click(object sender, EventArgs e)
        {

            


            string to_home = @" cd %HOMEPATH%";

            string test = @" mkdir testtesttest";

            string script = @" C:\Users\alexa\source\repos\3DWorldCreator\create_virtualenv.ps1";

            string powershell = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";

            // System.Diagnostics.Process.Start(powershell+ script, " -executionpolicy Unrestricted -File ");
            //System.Diagnostics.Process.Start(powershell + test, " -executionpolicy Unrestricted -File ");

            //PowerShell ps = PowerShell.Create();
            //ps.AddCommand("Set−ExecutionPolicy RemoteSigned");
            //ps.AddCommand("Set-PSDebug -RemoteSigned\n" + script);
            //ps.Invoke();
            NoError();

        }

        void NoError()
        {
            string python_interpreter = textBox11.Text;
            string name__ = textBox9.Text;
            string verbal__= textBox16.Text;

            var ps1File = @"C:\Users\alexa\source\repos\3DWorldCreator\create_virtualenv.ps1";

            using (StreamWriter w = new StreamWriter(ps1File, false, Encoding.GetEncoding("utf-8")))
            {
                w.WriteLine(@"cd -Path ~");
                w.WriteLine(@"virtualenv source\repos\3DWorldCreator\virtualenvs\" + name__);
                w.WriteLine(@".\source\repos\3DWorldCreator\virtualenvs\" + name__ + @"\Scripts\activate.ps1");
                w.WriteLine(@"pip install jupyter");
                w.WriteLine(@"jupyter notebook");
                w.WriteLine(@"cd - Path ~");
                w.WriteLine(@".\virtualenvs\"+ name__ + @"\Scripts\deactivate");
            }


            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);

            database_.insert_virt_env(python_interpreter, name__, verbal__);
            update_All();
        }

        private void get_packages()
        {
            listView20.Items.Clear();
            //var output_path = @"C:\Users\alexa\source\repos\3DWorldCreator\list_packages.txt";

            string name__ =virtualenvs[selected_env].name ;//textBox9.Text;

            var ps1File = @"C:\Users\alexa\source\repos\3DWorldCreator\get_packages.ps1";
            var output_path = @"C:\Users\alexa\source\repos\3DWorldCreator\list_packages.txt";

            using (StreamWriter w = new StreamWriter(ps1File, false, Encoding.GetEncoding("utf-8")))
            {
                w.WriteLine(@"cd -Path ~");
                
                w.WriteLine(@".\source\repos\3DWorldCreator\virtualenvs\" + name__ + @"\Scripts\activate.ps1");
                w.WriteLine(@"pip freeze --local >>" + output_path);
                w.WriteLine(@"cd - Path ~");
                w.WriteLine(@".\virtualenvs\" + name__ + @"\Scripts\deactivate");
            }


            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
            

            string[] readText = File.ReadAllLines(output_path);
            foreach (string s in readText)
            {
                string[] mystring = s.Split('=');
                ListViewItem lvi = new ListViewItem(mystring[0]);
                //lvi.Text = mystring[0];
                lvi.SubItems.Add(mystring[2]);
               
                listView20.Items.Add(lvi);
                //Console.WriteLine(s);
            }

        }

        private void button39_Click(object sender, EventArgs e)
        {

            List<Package_> pack_to_install= new List<Package_>();
            if (richTextBox5.Text == "")
            {
                string package_name = textBox7.Text;
                string version__ = textBox8.Text;
                Package_ pkg = new Package_(package_name, version__);
                pack_to_install.Add(pkg);
            }
            else
            {
                string[] __text__ = richTextBox5.Text.Split(new[] {"\n" },StringSplitOptions.None);

                foreach (string s in __text__)
                {
                    string[] mystring = s.Split('=');
                    Package_ pkg = new Package_(mystring[0], mystring[2]);
                    MessageBox.Show(mystring[0]+ mystring[2]);
                    pack_to_install.Add(pkg);
                    //Console.WriteLine(s);
                }
            }

            if (checkBox2.Checked==true) {
                foreach (virtualenv venv in virtualenvs ) {
                    install__(venv.name, pack_to_install);
                }
            }
            else{
                string name__ = virtualenvs[selected_env].name;
                install__(name__, pack_to_install);
            }
            


        }

        private void install__(string virtualenv_name, List<Package_> pack_to_install)
        {

            var ps1File = @"C:\Users\alexa\source\repos\3DWorldCreator\add_ackages.ps1";


            using (StreamWriter w = new StreamWriter(ps1File, false, Encoding.GetEncoding("utf-8")))
            {
                w.WriteLine(@"cd -Path ~");

                w.WriteLine(@".\source\repos\3DWorldCreator\virtualenvs\" + virtualenv_name + @"\Scripts\activate.ps1");
                foreach (Package_ package_ in pack_to_install)
                {
                    w.WriteLine(@"pip install " + package_.name + "==" + package_.version + "");
                }
                w.WriteLine(@"cd - Path ~");
                w.WriteLine(@".\virtualenvs\" + virtualenv_name + @"\Scripts\deactivate");
            }

            /*
            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);*/

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"";
                process.StartInfo.UseShellExecute = true;

                process.Start();
                process.WaitForExit();
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                // Synchronously read the standard output of the spawned process. 
                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();

                // Write the redirected output to this application's window.


                process.WaitForExit();
            }
        }


        private void timer1_handle(object sender, System.EventArgs e)
        {
            richTextBox4.Text = output;
        }

            private void button42_Click(object sender, EventArgs e)
        {
            string package_name = textBox7.Text;
            string adress = "numpy.org";
            webBrowser5.Navigate(adress, false);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button43_Click(object sender, EventArgs e)
        {
            Process Proc = new Process();
            Proc.StartInfo.FileName = "explorer";
            Proc.StartInfo.Arguments = @"C:\Users\alexa\source\repos\3DWorldCreator\plugins";
            Proc.Start();
            Proc.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process Proc = new Process();
            Proc.StartInfo.FileName = "explorer";
            Proc.StartInfo.Arguments = @"C:\Users\alexa\source\repos\3DWorldCreator\mocap_animations";
            Proc.Start();
            Proc.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process Proc = new Process();
            Proc.StartInfo.FileName = "explorer";
            Proc.StartInfo.Arguments = @"C:\Users\alexa\source\repos\3DWorldCreator\3DCharacter";
            Proc.Start();
            Proc.Close();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            
        }

        private void button38_Click(object sender, EventArgs e)
        {
            string name__=virtualenvs[selected_env].name;//= textBox9.Text;
            

            var ps1File = @"C:\Users\alexa\source\repos\3DWorldCreator\choose_virtualenv.ps1";

            using (StreamWriter w = new StreamWriter(ps1File, false, Encoding.GetEncoding("utf-8")))
            {
                w.WriteLine(@"cd -Path ~");
                w.WriteLine(@".\source\repos\3DWorldCreator\virtualenvs\" + name__ + @"\Scripts\activate.ps1");
                w.WriteLine(@"jupyter notebook");
                w.WriteLine(@"cd - Path ~");
                w.WriteLine(@".\virtualenvs\" + name__ + @"\Scripts\deactivate");
            }


            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);

            update_All();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            string install__=@"C:\Users\alexa\source\repos\3DWorldCreator\installations\mysql-installer-web-community-8.0.19.0.msi";
            System.Diagnostics.Process.Start(install__);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            MyLocalDB database_1 = new MyLocalDB();


        }

        private void button26_Click(object sender, EventArgs e)
        {
            foreach (virtualenv virt_env in virtualenvs)
            {
                var ps1File = @"C:\Users\alexa\source\repos\3DWorldCreator\podbor_virtualenv.ps1";

                using (StreamWriter w = new StreamWriter(ps1File, false, Encoding.GetEncoding("utf-8")))
                {
                    w.WriteLine(@"cd -Path ~");
                    w.WriteLine(@".\source\repos\3DWorldCreator\virtualenvs\" + virt_env.name + @"\Scripts\activate.ps1");
                    w.WriteLine(@"python " + @"C:\Users\alexa\source\repos\3DWorldCreator\current_script.py");
                    w.WriteLine(@"cd - Path ~");
                    w.WriteLine(@".\virtualenvs\" + virt_env.name + @"\Scripts\deactivate");
                }

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "powershell.exe";
                    process.StartInfo.Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();

                    // Synchronously read the standard output of the spawned process. 
                    StreamReader reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    richTextBox1.Text += "\nvirtualenv:\n" + virt_env.verbal + "\n";
                    richTextBox1.Text += output;
                    // Write the redirected output to this application's window.


                    process.WaitForExit();
                }
                

                update_All();

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string path__= @"C:\Users\alexa\source\repos\3DWorldCreator\current_network.py";
            string path_to = @" C:\Users\alexa\source\repos\3DWorldCreator\current_network1.ipynb";


            var ps1File = @"C:\Users\alexa\source\repos\3DWorldCreator\py_to_ipynb.ps1";

            using (StreamWriter w = new StreamWriter(ps1File, false, Encoding.GetEncoding("utf-8")))
            {
                w.WriteLine(@"cd -Path ~");
                w.WriteLine(@".\source\repos\3DWorldCreator\virtualenvs\" + virtualenvs[selected_env].name + @"\Scripts\activate.ps1");
                w.WriteLine(@"ipynb-py-convert "+path__ + path_to);
                w.WriteLine("jupyter notebook " + path_to);
                w.WriteLine(@"cd - Path ~");
                w.WriteLine(@".\virtualenvs\" + virtualenvs[selected_env] + @"\Scripts\deactivate");
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file \"{ps1File}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                // Synchronously read the standard output of the spawned process. 
                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();
                
                richTextBox2.Text = output;
                // Write the redirected output to this application's window.


                process.WaitForExit();
            }


            update_All();
        }
    }
}
