using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crtanje_električnih_šema
{
    public partial class Tekst : Form
    {

        
        
        public Tekst()
        {
            InitializeComponent();
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        { 
            Crtanje.sadrzajTeksta = textBox1.Text;
            this.Close();
            textBox1.Text = "";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            FontDialog fontDialog = new FontDialog();
            DialogResult result = fontDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                Crtanje.font = fontDialog.Font;
                textBox1.Font = Crtanje.font;
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult result = colorDialog.ShowDialog();
            
            if(result == DialogResult.OK)
            {
                Crtanje.boja = colorDialog.Color;
                textBox1.ForeColor = Crtanje.boja;
            }
            
        }
    }
}
