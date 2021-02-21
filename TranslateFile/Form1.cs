using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using System.Windows;
using System.Diagnostics;








namespace TranslateFile
{
    public partial class Form1 : Form
    {





        //---------------------

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            



        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if(FBD.ShowDialog() == DialogResult.OK)
            {
                
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                string[] dirs = Directory.GetDirectories(FBD.SelectedPath);

                
                //string[] allfiles = Directory.GetFiles("E:/AfterEffects/Ae/Presets/", "*.*", SearchOption.AllDirectories);
                if (checkBox1.Checked)
                {
                    // Subdirectory
                    string[] allfiles = Directory.GetFiles(FBD.SelectedPath, "*.*", SearchOption.AllDirectories);
                    foreach (var file in allfiles)
                    {
                        FileInfo info = new FileInfo(file);
                        //listBox1.Items.Add(file);
                        if (checkBox2.Checked){
                            listBox1.Items.Add(file);
                        }
                        else {
                            listBox1.Items.Add(Path.GetFileName(file));
                        }
                        
                        


                    }
                }
                else
                    foreach (string file in files)
                    {
                        if (checkBox2.Checked)
                        {
                            listBox1.Items.Add(file);
                        }
                        else
                        {
                            listBox1.Items.Add(Path.GetFileName(file));
                        }
                        //listBox1.Items.Add(Path.GetFileName(file));
                        //listBox1.Items.Add(file);
                    }

                /*
                foreach (string dirs in dirs)
                {
                    listBox1.Items.Add(dir);
                }
                */

            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++) {
                listBox2.Items.Add(Translate(listBox1.Items[i].ToString()));
                Debug.WriteLine(listBox1.Items[i].ToString());                        
            }
        }
        public String Translate(String word)
        {
            var toLanguage = "en";//English
            var fromLanguage = "ru";//Russia
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                //listBox2.Items.Add(result);
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return "Error";

                
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //Translate(listBox1.SelectedItem.ToString());
            //listBox2.Items.Add(listBox1.SelectedItem.ToString());
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

            /*
            foreach (var item in listBox1.Items) {
                
                Newtonsoft.Json.JsonConvert.SerializeObject(item.ToString());
            }
            */

        }
        private bool check = true;

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            

            if (check == true) {

                Debug.WriteLine("true");
                check = false;
            }
            else if (check == false)
            {
                Debug.WriteLine("false");
                check = true;

            }


            
        }
        //sample.json[]
    }
}
