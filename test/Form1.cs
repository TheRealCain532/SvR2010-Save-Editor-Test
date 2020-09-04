using System;
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
                    dataGridView1.Rows.Add($"{p.Stats[i]}", $"{p.MaxStats[i]}");
                textBox1.Text = p.NameText;
                textBox2.Text = p.HUDText;
                textBox3.Text = p.Nickname;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] 
                newStats = new byte[dataGridView1.Rows.Count],
                newMaxStats = new byte[dataGridView1.Rows.Count];
            for (int i = 0; i < newStats.Length; i++)
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    newStats[i] = Convert.ToByte(dataGridView1.Rows[i].Cells[0].Value);
                    newMaxStats[i] = Convert.ToByte(dataGridView1.Rows[i].Cells[1].Value);
                }
            p.Stats = newStats;
            p.MaxStats = newMaxStats;
        }

        private void textChange(object sender, EventArgs e)
        {
            TextBox a = sender as TextBox;
            switch (a.Name)
            {
                case "textBox1": p.NameText = textBox1.Text; break;
                case "textBox2": p.HUDText = textBox2.Text; break;
                case "textBox3": p.Nickname = textBox3.Text; break;
            }
        }
    }
}
