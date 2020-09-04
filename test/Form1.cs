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
            f = true;
        }
        bool f;
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
        byte[] writeData(int index)
        {
            byte[] res = new byte[8];
            for (int i = 0; i < 7; i++)
                res[i] = Convert.ToByte(dataGridView1.Rows[i].Cells[index].Value);
            return res;
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (f)
            {
                int? rowIdx = e?.RowIndex;
                int? colIdx = e?.ColumnIndex;
                if (rowIdx.HasValue && colIdx.HasValue)
                {
                    var dgv = (DataGridView)sender;
                    var cell = dgv?.Rows?[rowIdx.Value]?.Cells?[colIdx.Value]?.Value;
                    if (!string.IsNullOrEmpty(cell?.ToString()))
                        p.Stats = writeData(e.ColumnIndex);
                    };
                };
            }
        }
    }

