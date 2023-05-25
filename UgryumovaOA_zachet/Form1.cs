using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace UgryumovaOA_zachet
{
    public partial class Form1 :Form
    {
        string fl;
        List<Word> words = new List<Word>( );
        public Form1 ()
        {
            InitializeComponent( );
            
        }
        private bool CheckFile (string file) //проверка файла
        {
            if ( File.Exists(file) )
            {
                if(File.ReadAllText(file) == "" )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private void button1_Click (object sender, EventArgs e) //кнопка вставить файл
        {
            fl = textBox1.Text;
            if ( CheckFile(fl) )
            {
                string [ ] lines = File.ReadAllLines(fl);
                foreach ( string i in lines )
                {
                    listBox1.Items.Add(i);
                    Word w = new Word( );
                    w.Name = i;
                    words.Add(w);
                }
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("такого файла нет");
            }
        }

        private void Form1_FormClosing (object sender, FormClosingEventArgs e)
        {
            try
            {
                StreamWriter sw = File.CreateText(fl);
                string [ ] lines = new string [ listBox1.Items.Count ];
                for ( int i = 0; i < lines.Length; i++ )
                {
                    lines [ i ] = listBox1.Items [ i ].ToString( );
                }
                foreach ( string line in lines )
                {
                    sw.WriteLine(line);
                }
                sw.Close( );
                Application.Exit( );
            }
            catch
            {
                Application.Exit( );
            }
        }

        private void button2_Click (object sender, EventArgs e)//добавить слово
        {
            string word = textBox3.Text;
            Word w = new Word( );
            w.Name = word;
            words.Add(w);
            listBox1.Items.Add(w.Name);
        }

        private void button3_Click (object sender, EventArgs e)//кнопка удалить
        {
            string worddel = textBox2.Text;
            if ( worddel.Contains(' ') ) { MessageBox.Show("в слове есть иные символы"); }
            else
            {
                Queue<string> ww = new Queue<string>(words.Count);

                foreach ( Word w in words )
                {
                    ww.Enqueue(w.Name);
                }
                var newlist = ww.GroupBy(x => x).Where(x => x.Contains(worddel) == false).Select(x=>x.Key).ToList( );

                listBox1.Items.Clear( );
                words.Clear( );
                foreach ( var i in newlist )
                {
                    listBox1.Items.Add(i);
                    Word word = new Word( );
                    word.Name = i.ToString();
                }
            }
        }

        private void button4_Click (object sender, EventArgs e) //сортировка
        {
            try
            {
                listBox1.Items.Clear();
                words.Sort( );
                foreach ( Word i in words )
                {
                    listBox1.Items.Add(i.Name);
                }
            }
            catch
            {
                MessageBox.Show("уберите для начала дубликаты");
            }
        }

        private void button5_Click (object sender, EventArgs e) //убрать дубликаты
        {
            listBox1.Items.Clear( );
            Queue <string> ww = new Queue<string>(words.Count);

            foreach ( Word w in words )
            {
                ww.Enqueue(w.Name);
            }
            var newlist = ww.GroupBy(x => x).Where(x => x.Count( ) == 1).Select(x => x.Key).ToList( );
            words.Clear( );
            foreach ( var i in newlist )
            {
                listBox1.Items.Add(i);
                Word word = new Word( );
                word.Name = i;
            }
        }
    }
}
