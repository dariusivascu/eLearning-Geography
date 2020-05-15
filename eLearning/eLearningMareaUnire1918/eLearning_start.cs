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
using System.Data.SqlClient;
using System.Globalization;

namespace eLearningMareaUnire1918
{
    public partial class eLearning_start : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader cmdR;

        int i,n;
        string[] path = Directory.GetFiles(@"..\..\..\..\Resurse\imaginislideshow");
        string email, pw;
        public string caleMDF, clasaUser, nume;
        public int idUser;
        Introducere parent;

        public eLearning_start(Introducere x)
        {
            InitializeComponent();
            parent = x;
            pictureBox1.ImageLocation = path[0];
            FileInfo BazaDate = new FileInfo(@"..\..\eLearning.mdf"); 
            caleMDF = BazaDate.FullName;
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='"+caleMDF+"';Integrated Security=True");
            StreamReader fin = new StreamReader(@"..\..\..\..\Resurse\date.txt");
            con.Open();
            cmd = new SqlCommand("TRUNCATE TABLE Evaluari; TRUNCATE TABLE Itemi; TRUNCATE TABLE Utilizatori",con);
            cmd.ExecuteNonQuery();
            con.Close();
            string tabela = "";
            string sir = "";
            string[] aux = new string[1];
            string[] valori = new string[10];
            while(!fin.EndOfStream)
            {
                sir = fin.ReadLine();
                if(sir.EndsWith(":"))
                {
                    aux = sir.Split(':');
                    tabela = aux[0];
                }
                else
                {
                    valori = sir.Split(';');
                    con.Open();
                    if(tabela=="Utilizatori")
                    {
                        cmd = new SqlCommand("INSERT INTO Utilizatori (NumePrenumeUtilizator,ParolaUtilizator,EmailUtilizator,ClasaUtilizator) VALUES ('" +valori[0]+ "','" + valori[1]+"','" + valori[2] + "','" + valori[3] + "')",con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if(tabela=="Itemi")
                    {
                        cmd = new SqlCommand("INSERT INTO Itemi VALUES ('" + Convert.ToInt32(valori[0]) + "','" + valori[1] + "','" + valori[2] + "','" + valori[3] + "','" + valori[4] + "','" + valori[5] + "','" + valori[6]+ "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if(tabela=="Evaluari")
                    {
                        DateTime d = new DateTime();
                        d = DateTime.ParseExact(valori[1], "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                        cmd = new SqlCommand("INSERT INTO Evaluari (IdElev, DataEvaluare, NotaEvaluare) VALUES(@b,@a,@c)",con);
                        cmd.Parameters.Add("@a", SqlDbType.DateTime);
                        cmd.Parameters.Add("@b", SqlDbType.Int);
                        cmd.Parameters.Add("@c", SqlDbType.Int);
                        cmd.Parameters["@b"].Value = valori[0];
                        cmd.Parameters["@c"].Value = valori[2];
                        cmd.Parameters["@a"].Value = d;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    pictureBox2.ImageLocation = @"..\..\..\..\Resurse\user.bmp";
                    n = path.Length - 1;
                    progressBar1.Maximum = n;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Manual")
            {
                button2.Text = "Auto";
                button1.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button2.Text = "Manual";
                button1.Enabled = false;
                button3.Enabled = false;
            }
                
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (i < n)
            {
                i++;
                pictureBox1.ImageLocation = path[i];
                progressBar1.Increment(1);
            }
        }

        private void ELearning_start_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (i > 0)
            {
                i--;
                pictureBox1.ImageLocation = path[i];
                progressBar1.Increment(-1);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            email = textBox1.Text;
            pw = textBox2.Text;
            con.Open();
            cmd = new SqlCommand("SELECT IdUtilizator, ClasaUtilizator, NumePrenumeUtilizator From Utilizatori Where EmailUtilizator='" +email+ "' AND ParolaUtilizator='" + pw + "'", con);
            cmdR = cmd.ExecuteReader();
            if (cmdR.Read())
            {
                idUser = Convert.ToInt32(cmdR[0]);
                clasaUser = cmdR[1].ToString();
                nume = cmdR[2].ToString();
                eLearning_Elev f = new eLearning_Elev(this);
                f.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Cont inexistent!");
            con.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(button2.Text=="Manual")
            {
                if (i < n)
                {
                    i++;
                    progressBar1.Increment(1);
                }    
                else
                {
                    i = 0;
                    progressBar1.Value = progressBar1.Minimum;
                }
                pictureBox1.ImageLocation = path[i];
            }
        }
    }
}