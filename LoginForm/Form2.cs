using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, System.EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value<100)
            {
                progressBar1.Value = progressBar1.Value + 2;
           
            }
            else if(progressBar1.Value>=100)
            {
                timer1.Enabled = false;
                this.Hide();
                MainForm mf = new MainForm();
                mf.Show();
            }
        }
    }
    }

