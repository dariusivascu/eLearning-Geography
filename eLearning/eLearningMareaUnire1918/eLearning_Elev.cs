using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace eLearningMareaUnire1918
{
    public partial class eLearning_Elev : Form
    {
        eLearning_start parent;
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader cmdR;
        int ct = 1,punctaj=1;
        int[] itemiA = new int[11];
        int count;
        string raspuns;
        int raspuns2;
        CheckBox[] check;
        int[] adev = new int[5];
        int VAdev;

        public eLearning_Elev(eLearning_start f)
        {
            InitializeComponent();
            parent = f;
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + parent.caleMDF + "';Integrated Security=True");
            check = new CheckBox[] { null, checkBox1, checkBox2, checkBox3, checkBox4 };
            adev[0] = adev[1] = adev[2] = adev[3] = adev[4] = 0;
            for (int i = 0; i <= 9; i++)
                itemiA[i] = 0;
            label2.Visible = label5.Visible = false;
        }

        private void eLearning1918_Elev_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Show();
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void carnetDeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void graficNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sem;
            if(panel1.Visible==true)
            {
                raspuns = editWord(raspuns);
                string raspUser;
                raspUser = editWord(textBox1.Text);
                if(raspUser==raspuns)
                {
                    MessageBox.Show("Corect!");
                    punctaj++;
                    label2.Text = punctaj.ToString();
                }
                else
                    MessageBox.Show("Gresit!");
                panel1.Visible = false;
            }
            if(panel2.Visible==true)
            {
                sem = 0;
                if (raspuns2 == 1 && radioButton1.Checked == true)
                {
                    MessageBox.Show("Correct!");
                    punctaj++;
                    label2.Text = punctaj.ToString();
                    sem = 1;
                }
                if (raspuns2 == 2 && radioButton2.Checked == true)
                {
                    MessageBox.Show("Correct!");
                    punctaj++;
                    label2.Text = punctaj.ToString();
                    sem = 1;
                }
                if (raspuns2 == 3 && radioButton3.Checked == true)
                {
                    MessageBox.Show("Correct!");
                    punctaj++;
                    label2.Text = punctaj.ToString();
                    sem = 1;
                }
                if (raspuns2 == 4 && radioButton4.Checked == true)
                {
                    MessageBox.Show("Correct!");
                    punctaj++;
                    label2.Text = punctaj.ToString();
                    sem = 1;
                }
                if (sem == 0)
                    MessageBox.Show("Gresit!");
                panel2.Visible = false;
            }
            if (panel3.Visible == true)
            {
                sem = 1;
                for(int i=1;i<=4;i++)
                {
                    if (adev[i] == 1 && check[i].Checked == false)
                        sem = 0;
                }
                if (sem == 0)
                    MessageBox.Show("Gresit!");
                else
                {
                    MessageBox.Show("Corect!");
                    punctaj++;
                    label2.Text = punctaj.ToString();
                }
                for (int i = 0; i < 5; i++)
                    adev[i] = 0;
                panel3.Visible = false;
            }
            if(panel4.Visible == true)
            {
                if ((VAdev == 1 && radioButton5.Checked == true) || (VAdev == 0 && radioButton6.Checked == true))
                {
                    MessageBox.Show("Corect!");
                    punctaj++;
                    label2.Text = punctaj.ToString();
                }
                else
                    MessageBox.Show("Gresit!");
                panel4.Visible = false;
            }
            ct++;
            button3.Enabled = true;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button3.Enabled = false;
            button2.Visible = true;
            con.Open();
            ct = 1; punctaj = 1;
            label2.Visible = label5.Visible = true;
            label2.Text = punctaj.ToString();
            label5.Text = "1";
            cmd = new SqlCommand("SELECT COUNT(*) FROM Itemi",con);
            cmdR = cmd.ExecuteReader();
            if (cmdR.Read())
                count = Convert.ToInt32(cmdR[0]);
            con.Close();
            con.Open();
            cmd = new SqlCommand("SELECT TOP 1 IdItem, EnuntItem, RaspunsCorectItem FROM Itemi WHERE TipItem=1 ORDER BY NewId()",con);
            cmdR = cmd.ExecuteReader();
            if (cmdR.Read())
            {
                itemiA[ct]=Convert.ToInt32(cmdR[0]);
                enunt.Text = cmdR[1].ToString();
                panel1.Visible = true;
                raspuns = cmdR[2].ToString();
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = false;
            textBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            for (int i = 1; i <= 4; i++)
                check[i].Checked = false;
            if (ct==2)
            {
                con.Open();
                cmd = new SqlCommand("SELECT TOP 1 IdItem, EnuntItem, Raspuns1Item, Raspuns2Item, Raspuns3Item, Raspuns4Item, RaspunsCorectItem FROM Itemi WHERE TipItem=2 ORDER BY NewId()", con);
                cmdR = cmd.ExecuteReader();
                label5.Text = "2";
                if (cmdR.Read())
                {
                    itemiA[ct] = Convert.ToInt32(cmdR[0]);
                    enunt.Text = cmdR[1].ToString();
                    panel2.Visible = true;
                    radioButton1.Text = cmdR[2].ToString();
                    radioButton2.Text = cmdR[3].ToString();
                    radioButton3.Text = cmdR[4].ToString();
                    radioButton4.Text = cmdR[5].ToString();
                    raspuns2 = Convert.ToInt32(cmdR[6]);
                }
                con.Close();
            }
            if(ct==3)
            {
                con.Open();
                cmd = new SqlCommand("SELECT TOP 1 IdItem, EnuntItem, Raspuns1Item, Raspuns2Item, Raspuns3Item, Raspuns4Item, RaspunsCorectItem FROM Itemi Where TipItem=3 ORDER BY NewId()", con);
                cmdR = cmd.ExecuteReader();
                label5.Text = "3";
                if(cmdR.Read())
                {
                    itemiA[ct] = Convert.ToInt32(cmdR[0]);
                    enunt.Text = cmdR[1].ToString();
                    panel3.Visible = true ;
                    check[1].Text = cmdR[2].ToString();
                    check[2].Text = cmdR[3].ToString();
                    check[3].Text = cmdR[4].ToString();
                    check[4].Text = cmdR[5].ToString();
                    string val = cmdR[6].ToString();
                    for (int i = 0; i < val.Length; i++)
                        adev[Convert.ToInt32(val[i].ToString())] = 1;
                }
                con.Close();
            }
            if(ct==4)
            {
                con.Open();
                cmd = new SqlCommand("SELECT TOP 1 IdItem, EnuntItem, RaspunsCorectItem FROM Itemi WHERE TipItem=4 ORDER BY NewId()", con);
                cmdR = cmd.ExecuteReader();
                label5.Text = "4";
                if(cmdR.Read())
                {
                    itemiA[ct] = Convert.ToInt32(cmdR[0]);
                    enunt.Text = cmdR[1].ToString();
                    panel4.Visible = true;
                    VAdev = Convert.ToInt32(cmdR[2]);
                }
                con.Close();
            }
            if(ct>4 && ct<10)
            {
                con.Open();
                cmd = new SqlCommand("SELECT TOP 1 * FROM Itemi WHERE IdItem NOT IN ('"+ itemiA[1]+"','"+itemiA[2] + "','" + itemiA[3] + "','" + itemiA[4]+ "','" + itemiA[5] + "','" + itemiA[6] + "','" + itemiA[7] + "','" + itemiA[8] + "','" + itemiA[9] + "') ORDER BY NewId()",con);
                cmdR = cmd.ExecuteReader();
                if(cmdR.Read())
                {
                    itemiA[ct] = Convert.ToInt32(cmdR[0]);
                    label5.Text = cmdR[1].ToString();
                    enunt.Text = cmdR[2].ToString();
                    if(Convert.ToInt32(cmdR[1])==1)
                    {
                        raspuns = cmdR[7].ToString();
                        panel1.Visible = true;
                    }
                    if(Convert.ToInt32(cmdR[1])==2)
                    {
                        radioButton1.Text = cmdR[3].ToString();
                        radioButton2.Text = cmdR[4].ToString();
                        radioButton3.Text = cmdR[5].ToString();
                        radioButton4.Text = cmdR[6].ToString();
                        raspuns2 = Convert.ToInt32(cmdR[7]);
                        panel2.Visible = true;
                    }
                    if(Convert.ToInt32(cmdR[1])==3)
                    {
                        check[1].Text=cmdR[3].ToString();
                        check[2].Text = cmdR[4].ToString();
                        check[3].Text = cmdR[5].ToString();
                        check[4].Text = cmdR[6].ToString();
                        string val = cmdR[7].ToString();
                        for (int i = 0; i < val.Length; i++)
                            adev[Convert.ToInt32(val[i].ToString())] = 1;
                        panel3.Visible = true;
                    }
                    if(Convert.ToInt32(cmdR[1])==4)
                    {
                        VAdev = Convert.ToInt32(cmdR[7]);
                        panel4.Visible=true;
                    }
                }
                con.Close();
            }
            if(ct==10)
            {
                MessageBox.Show("Felicitari! Ai acumulat: " + punctaj + " puncte!");
                button2.Visible = button3.Visible = false;
                con.Open();
                cmd = new SqlCommand("INSERT INTO Evaluari (IdElev, DataEvaluare, NotaEvaluare) VALUES ('" + parent.idUser + "', SysDateTime(),'" + punctaj + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                enunt.Text = "";
                label2.Visible = label5.Visible = false;
                
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==1)
            {
                label10.Text = "Carnetul de note al elevului "+parent.nume;
                dataGridView1.Rows.Clear();
                con.Open();
                cmd = new SqlCommand("SELECT NotaEvaluare, DataEvaluare FROM Evaluari Where IdElev='" + parent.idUser + "'", con);
                cmdR = cmd.ExecuteReader();
                while(cmdR.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[1].Value = cmdR[0].ToString();
                    row.Cells[0].Value = cmdR[1].ToString();
                    dataGridView1.Rows.Add(row);
                    row.Dispose();
                }
                cmdR.Close();
                con.Close();
            }
            if(tabControl1.SelectedIndex==2)
            {
                int x = 1;
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                con.Open();
                cmd = new SqlCommand("Select NotaEvaluare FROM Evaluari WHERE IdElev='" + parent.idUser + "'", con);
                cmdR = cmd.ExecuteReader();
                while(cmdR.Read())
                {
                    chart1.Series[0].Points.AddXY(x,cmdR[0]);
                    x++;
                }
                con.Close();
                con.Open();
                cmd = new SqlCommand("SELECT AVG(NotaEvaluare),COUNT(NotaEvaluare) FROM Evaluari WHERE IdElev IN (SELECT IdElev FROM Utilizatori WHERE ClasaUtilizator = '" + parent.clasaUser + "')", con);
                cmdR = cmd.ExecuteReader();
                if(cmdR.Read())
                {
                    chart1.Series[1].Points.AddXY(1, cmdR[0]);
                    chart1.Series[1].Points.AddXY(cmdR[1], cmdR[0]);
                }
                    
                con.Close();
            }
        }

        Bitmap bmp;

        private void button4_Click(object sender, EventArgs e)
        {
            int height = dataGridView1.Height;
            dataGridView1.Height = dataGridView1.Height * dataGridView1.RowCount *2;
            bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            dataGridView1.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0,0);
        }

        string editWord(string a)
        {
            string[] aux;
            a=a.ToLower();
            aux = a.Split(' ');
            a = "";
            foreach(string s in aux)
            {
                a = a + s + " ";
            }
            a=a.Trim();
            return a;
        }

    }
}
