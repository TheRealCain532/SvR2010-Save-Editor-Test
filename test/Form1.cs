using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using svr2010;

namespace test
{
    public partial class Form1 : Form
    {
        Player p;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            dataGridView1.Rows.Clear();
            if (o.ShowDialog() == DialogResult.OK)
            {
                p = new Player(o.FileName);
                for (int i = 0; i < p.Stats.Length; i++)
                    dataGridView1.Rows.Add(p.Stats[i].ToString(), p.MaxStats[i].ToString());
                textBox1.Text = p.NameText;
                textBox2.Text = p.HUDText;
                textBox3.Text = p.Nickname;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] 
                newStats = new byte[dataGridView1.Rows.Count-1],
                newMaxStats = new byte[dataGridView1.Rows.Count - 1];
            for (int i = 0; i < newStats.Length; i++)
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    newStats[i] = Convert.ToByte(dataGridView1.Rows[i].Cells[0].Value);
                    newMaxStats[i] = Convert.ToByte(dataGridView1.Rows[i].Cells[1].Value);
                }
            p.Stats = newStats;
            p.MaxStats = newMaxStats;
            p.NameText = textBox1.Text;
            p.HUDText = textBox2.Text;
            p.Nickname = textBox3.Text;
        }
    }
}
