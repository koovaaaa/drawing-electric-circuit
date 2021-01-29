using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Imaging;
using System.IO;

namespace Crtanje_električnih_šema
{
    public partial class Crtanje : Form
    {
       
        int izabraniElementTag = 0;
        int lokacijaX;
        int lokacijaY;
        Point point1 =  new Point(0,0);
        Point point2 = new Point(0, 0);
        public static string sadrzajTeksta;
        public static Font font;
        public static Color boja;
        
        Tekst tekst = new Tekst();

        public Crtanje()
        {
            InitializeComponent();

             
            font = new Font("Arial", 12);
            boja = Color.Black;
            

            lijeviPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            centralniPanel.BackColor = Color.White;
            centralniPanel.BorderStyle = BorderStyle.Fixed3D;
            

            pictureBox1.Image = Properties.Resources.Resistor;
            pictureBox1.Tag = 1;
            pictureBox2.Image = Properties.Resources.Diode;
            pictureBox2.Tag = 2;
            pictureBox3.Image = Properties.Resources.AC_Source;
            pictureBox3.Tag = 3;
            pictureBox4.Image = Properties.Resources.DC_Source;
            pictureBox4.Tag = 4;
            pictureBox5.Image = Properties.Resources.Capacitor;
            pictureBox5.Tag = 5;
            pictureBox6.Image = Properties.Resources.Switch;
            pictureBox6.Tag = 6;
            pictureBox7.Image = Properties.Resources.Wire;
            pictureBox7.Tag = 7;
            pictureBox8.Image = Properties.Resources.Text;
            pictureBox8.Tag = 8;


        }

        private void centralniPanel_Paint(object sender, PaintEventArgs e)
        {
            
            
            Graphics g = e.Graphics;
            Pen olovka = new Pen(new SolidBrush(Color.LightGray));
            
            int sirina = centralniPanel.Width;
            int visina = centralniPanel.Height;
     
            //Crtanje horizontalnih linija
            for (int i = 0; i < visina; i+=10)
            {
                g.DrawLine(olovka, 0, i, sirina, i);
            }

            //Crtanje vertikalnih linija
            for (int i = 0; i < sirina; i += 10)
            {
                g.DrawLine(olovka, i, 0, i, visina);

            }
            

            
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

       

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            PictureBox kliknutiElement = (PictureBox)sender;
            izabraniElementTag = Convert.ToInt32(kliknutiElement.Tag);
            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox2.BorderStyle = BorderStyle.None;
            pictureBox3.BorderStyle = BorderStyle.None;
            pictureBox4.BorderStyle = BorderStyle.None;
            pictureBox5.BorderStyle = BorderStyle.None;
            pictureBox6.BorderStyle = BorderStyle.None;
            pictureBox7.BorderStyle = BorderStyle.None;
            pictureBox8.BorderStyle = BorderStyle.None;
            kliknutiElement.BorderStyle = BorderStyle.Fixed3D;
            
        }

        private void centralniPanel_MouseClick(object sender, MouseEventArgs e)
        {
            lokacijaX = e.Location.X;
            lokacijaY = e.Location.Y;
            

            if (izabraniElementTag == 0)
            {
                MessageBox.Show("Niste izabrali nijedan element!");
                
            }
            else if (izabraniElementTag == 1)
            {
                nacrtajOtpronik();
                Label label = new Label();
                label.Text = "Optornik\n";
                desniPanel.Controls.Add(label);

            }
            else if (izabraniElementTag == 2)
            {
                nacrtajDiodu();
                Label label = new Label();
                label.Text = "Dioda\n";
                desniPanel.Controls.Add(label);
            }
            else if (izabraniElementTag == 3)
            {
                nacrtajACSource();
                Label label = new Label();
                label.Text = "AC izvor\n";
                desniPanel.Controls.Add(label);
            }
            else if (izabraniElementTag == 4)
            {
                nacrtajDCSource();
                Label label = new Label();
                label.Text = "DC izvor\n";
                desniPanel.Controls.Add(label);
            }
            else if (izabraniElementTag == 5)
            {
                nacrtajKondenzator();
                Label label = new Label();
                label.Text = "Kondenzator\n";
                desniPanel.Controls.Add(label);
            }
            else if (izabraniElementTag == 6)
            {
                nacrtajPrekidac();
                Label label = new Label();
                label.Text = "Prekidac\n";
                desniPanel.Controls.Add(label);
            }   
            else if (izabraniElementTag == 7)
            {
                if (point1.X == 0 && point1.Y ==0)
                {
                    point1 = e.Location;
                }
                else if(point2.X == 0 && point2.Y == 0)
                {
                    point2 = e.Location;
                    nacrtajLiniju(point1, point2);
                    Label label = new Label();
                    label.Text = "Linija\n";
                    desniPanel.Controls.Add(label);
                    point1.X = 0;
                    point1.Y = 0;

                    point2.X = 0;
                    point2.Y = 0;
                    
                }
                
               
            }
            else if (izabraniElementTag == 8)
            {                 
                tekst.ShowDialog();

                if(sadrzajTeksta.Length > 0)
                {
                    ispisiTekst();
                    Label label = new Label();
                    label.Text = "Tekst\n";
                    desniPanel.Controls.Add(label);
                }
                
                    
                
                
                
          
            }
            
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            desniPanel.Controls.Clear();
            centralniPanel.Invalidate();
            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox2.BorderStyle = BorderStyle.None;
            pictureBox3.BorderStyle = BorderStyle.None;
            pictureBox4.BorderStyle = BorderStyle.None;
            pictureBox5.BorderStyle = BorderStyle.None;
            pictureBox6.BorderStyle = BorderStyle.None;
            pictureBox7.BorderStyle = BorderStyle.None;
            pictureBox8.BorderStyle = BorderStyle.None;
            izabraniElementTag = 0;
        }

        void nacrtajOtpronik()
        {
            Graphics g = centralniPanel.CreateGraphics();
            
            Pen olovka = new Pen(new SolidBrush(Color.Black));

            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(lokacijaX-40, lokacijaY, lokacijaX - 20, lokacijaY);
            gp.AddLine(lokacijaX - 20, lokacijaY, lokacijaX - 20, lokacijaY - 10);
            gp.AddLine(lokacijaX - 20, lokacijaY - 10, lokacijaX + 20, lokacijaY - 10);
            gp.AddLine(lokacijaX + 20, lokacijaY - 10, lokacijaX + 20, lokacijaY);
            gp.AddLine(lokacijaX + 20, lokacijaY, lokacijaX + 40, lokacijaY);
            gp.AddLine(lokacijaX + 20, lokacijaY, lokacijaX + 20, lokacijaY + 10);
            gp.AddLine(lokacijaX + 20, lokacijaY + 10, lokacijaX - 20, lokacijaY + 10);
            gp.AddLine(lokacijaX - 20, lokacijaY + 10, lokacijaX - 20, lokacijaY);
            g.DrawPath(olovka, gp);
            
        }

        void nacrtajDiodu()
        {
            Graphics g = centralniPanel.CreateGraphics();
            Pen olovka = new Pen(new SolidBrush(Color.Black));
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(lokacijaX - 30, lokacijaY, lokacijaX - 10, lokacijaY);
            gp.AddLine(lokacijaX - 10, lokacijaY, lokacijaX - 10, lokacijaY - 15);
            gp.AddLine(lokacijaX - 10, lokacijaY - 15, lokacijaX + 10, lokacijaY);
            gp.AddLine(lokacijaX + 10, lokacijaY, lokacijaX + 10, lokacijaY - 15);
            gp.AddLine(lokacijaX + 10, lokacijaY, lokacijaX + 30, lokacijaY);
            gp.AddLine(lokacijaX + 10, lokacijaY, lokacijaX + 10, lokacijaY + 15);
            gp.AddLine(lokacijaX + 10, lokacijaY, lokacijaX - 10, lokacijaY + 15);
            gp.AddLine(lokacijaX - 10, lokacijaY + 15, lokacijaX - 10, lokacijaY);

            g.DrawPath(olovka, gp);
        }

        void nacrtajACSource()
        {
            Graphics g = centralniPanel.CreateGraphics();
            Pen olovka = new Pen(new SolidBrush(Color.Black));
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(lokacijaX, lokacijaY-20, lokacijaX, lokacijaY-40);
            
            gp.AddEllipse(lokacijaX-20, lokacijaY-20, 40, 40);
            gp.AddLine(lokacijaX, lokacijaY + 20, lokacijaX, lokacijaY + 40);
            
            g.DrawPath(olovka, gp);

            g.DrawArc(olovka, lokacijaX - 15, lokacijaY - 7, 15, 15,0, -180);
            g.DrawArc(olovka, lokacijaX, lokacijaY-7, 15, 15, 180, -180);
        }

        void nacrtajDCSource()
        {
            Graphics g = centralniPanel.CreateGraphics();
            Pen olovka = new Pen(new SolidBrush(Color.Black));
            GraphicsPath gp = new GraphicsPath();

            

            gp.AddLine(lokacijaX, lokacijaY - 20, lokacijaX, lokacijaY - 40);
            gp.AddEllipse(lokacijaX - 20, lokacijaY - 20, 40, 40);
            gp.AddLine(lokacijaX, lokacijaY + 20, lokacijaX, lokacijaY + 40);
            g.DrawPath(olovka, gp);

            g.DrawLine(olovka, lokacijaX, lokacijaY - 5, lokacijaX, lokacijaY - 15);
            g.DrawLine(olovka, lokacijaX - 5, lokacijaY - 10, lokacijaX + 5, lokacijaY - 10);
            g.DrawLine(olovka, lokacijaX - 5, lokacijaY + 10, lokacijaX + 5, lokacijaY + 10);
        }

        void nacrtajKondenzator()
        {
            Graphics g = centralniPanel.CreateGraphics();
            Pen olovka = new Pen(new SolidBrush(Color.Black));
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(lokacijaX - 25, lokacijaY, lokacijaX - 5, lokacijaY);
            gp.AddLine(lokacijaX - 5, lokacijaY - 12, lokacijaX - 5, lokacijaY + 12);
            g.DrawPath(olovka, gp);

            GraphicsPath gp2 = new GraphicsPath();
            gp2.AddLine(lokacijaX + 5, lokacijaY - 12, lokacijaX +5, lokacijaY + 12);
            gp2.AddLine(lokacijaX + 5, lokacijaY, lokacijaX + 25, lokacijaY);
            g.DrawPath(olovka, gp2);
            
        }

        void nacrtajPrekidac()
        {
            Graphics g = centralniPanel.CreateGraphics();
            Pen olovka = new Pen(new SolidBrush(Color.Black));
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(lokacijaX - 45, lokacijaY, lokacijaX - 15, lokacijaY);
            gp.AddLine(lokacijaX - 15, lokacijaY, lokacijaX + 15, lokacijaY - 20);
            g.DrawPath(olovka, gp);

            g.DrawLine(olovka, lokacijaX + 15, lokacijaY, lokacijaX + 45, lokacijaY);
        }

        void ispisiTekst()
        {
            Graphics g = centralniPanel.CreateGraphics();
            
            Brush cetka = new SolidBrush(boja);

            g.DrawString(sadrzajTeksta, font, cetka, lokacijaX-sadrzajTeksta.Length, lokacijaY-sadrzajTeksta.Length);
            sadrzajTeksta = "";




        }

        int brojSlike;
        private void snapshootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brojSlike++;
            var form = Form.ActiveForm;

            Bitmap bitmapSave = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(bitmapSave, new Rectangle(0, 0, bitmapSave.Width, bitmapSave.Height));

            bitmapSave.Save(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\screenshot " + brojSlike + " .png", ImageFormat.Png);
        }


        private void nacrtajLiniju(Point prvaTacka, Point drugaTacka)
        {
            Graphics g = centralniPanel.CreateGraphics();
            Pen olovka = new Pen(new SolidBrush(Color.Black));

            g.DrawLine(olovka, prvaTacka, drugaTacka);

        }

        private void desniPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen olovka1 = new Pen(new SolidBrush(Color.Black), 15);
            int sirina = desniPanel.Width;
            int visina = desniPanel.Height;
            g.DrawLine(olovka1, 0, 0, 0, visina);
            Invalidate();
        }

        private void statusStrip1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
                       
            string podaci = "Milan_Kovačević_1548_3._Crtanje_Električnih_Šema_Računarska_Grafika";
            FontStyle stil = FontStyle.Italic | FontStyle.Bold;
            Font font = new Font("Arial", 12, stil);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;

            g.DrawString(podaci, font, new SolidBrush(Color.Black), 0, 0, format);
        }
    }
}