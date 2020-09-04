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
        string fName;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)
            {
                fName = o.FileName;
                t(0);
                foreach (var item in p.StarsName) comboBox1.Items.Add(item.Replace("_", " "));
                comboBox1.SelectedIndex = 0;
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
            for (int i = 0; i < 8; i++)
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
                        switch (e.ColumnIndex)
                        {
                            case 0: p.Stats = writeData(0); break;
                            case 1: p.MaxStats = writeData(1); break;
                        }
                    };
                };
            }
        void t(uint a)
        {
            dataGridView1.Rows.Clear();
            p = (a > 0) ? new Player(fName, a) : new Player(fName);
            for (int i = 0; i < p.Stats.Length; i++)
                dataGridView1.Rows.Add($"{p.Stats[i]}", $"{p.MaxStats[i]}");
            textBox1.Text = p.NameText;
            textBox2.Text = p.HUDText;
            textBox3.Text = p.Nickname;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)=> t(p.Stars[comboBox1.SelectedIndex]);
    }
    }

