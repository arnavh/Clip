using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Windows;

namespace Clip
{
    public partial class frmClip : Form
    {
        object[] Clipboards = new object[4];
        KeyHook Hook = new KeyHook();
        Keys[] Binds = { Keys.F6, Keys.F10, Keys.F9, Keys.F8, Keys.F7 };
        Color Blue = Color.FromArgb(132, 189, 182),
              Yellow = Color.FromArgb(246, 235, 187),
              Red = Color.FromArgb(221, 158, 136),
              Orange = Color.FromArgb(238, 190, 129);

        public frmClip()
        {
            InitializeComponent();

            Hook.hook();
            Hook.KeyDown += new KeyEventHandler(Gkh_KeyDown);
            foreach (Keys key in Binds)
            {
                Hook.HookedKeys.Add(key);
            }

            if (Clipboard.ContainsText())
            {
                for (int i = 0; i < 4; i++)
                {
                    Clipboards[i] = Clipboard.GetText().Replace(System.Environment.NewLine, " ");
                }
            }
            else if (Clipboard.ContainsImage())
            {
                for (int i = 0; i < 4; i++)
                {
                    Clipboards[i] = Clipboard.GetImage();
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Clipboards[i] = "null";
                }
            }

            Fade("out");
        }

        Point ScreenSize()
        {
            return new Point((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
        }

        void ShowClipboards()
        {
            this.Location = new Point(ScreenSize().X - 268, ScreenSize().Y - 500);
            this.TopMost = true;

            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        if (Clipboards[i].ToString() == "System.Drawing.Bitmap")
                        {
                            pnlClipboard1.BackgroundImage = (Bitmap)Clipboards[i];
                            lblClip1.Text = "";
                        }
                        else
                        {
                            lblClip1.Text = Clipboards[i].ToString();
                            pnlClipboard1.BackgroundImage = null;
                        }
                        break;
                    case 1:
                        if (Clipboards[i].ToString() == "System.Drawing.Bitmap")
                        {
                            pnlClipboard2.BackgroundImage = (Bitmap)Clipboards[i];
                            lblClip2.Text = "";
                        }
                        else
                        {
                            lblClip2.Text = Clipboards[i].ToString();
                            pnlClipboard2.BackgroundImage = null;
                        }
                        break;
                    case 2:
                        if (Clipboards[i].ToString() == "System.Drawing.Bitmap")
                        {
                            pnlClipboard3.BackgroundImage = (Bitmap)Clipboards[i];
                            lblClip3.Text = "";
                        }
                        else
                        {
                            lblClip3.Text = Clipboards[i].ToString();
                            pnlClipboard3.BackgroundImage = null;
                        }
                        break;
                    case 3:
                        if (Clipboards[i].ToString() == "System.Drawing.Bitmap")
                        {
                            pnlClipboard4.BackgroundImage = (Bitmap)Clipboards[i];
                            lblClip4.Text = "";
                        }
                        else
                        {
                            lblClip4.Text = Clipboards[i].ToString();
                            pnlClipboard4.BackgroundImage = null;
                        }
                        break;
                    case 4:
                        if (Clipboard.ContainsImage())
                        {
                            pnlClipboard.BackgroundImage = Clipboard.GetImage();
                            lblClip.Text = "";
                        }
                        else
                        {
                            lblClip.Text = Clipboard.GetText();
                            pnlClipboard.BackgroundImage = null;
                        }
                        break;
                }
            }

            Fade("in");
        }

        void Fade(string type)
        {
            if (type == "in")
            {
                while (this.Opacity != 1)
                {
                    this.Opacity += 0.01;
                    Thread.Sleep(2);
                }
            }
            else
            {
                while (this.Opacity != 0)
                {
                    this.Opacity -= 0.01;
                    Thread.Sleep(2);
                }
            }
        }

        void Gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Binds[0])
            {
                if (this.Opacity == 1)
                {
                    Fade("out");
                }
                else if (this.Opacity == 0)
                {
                    ShowClipboards();
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (e.KeyCode == Binds[i])
                {
                    if (Clipboard.ContainsText())
                        Clipboards[i - 1] = Clipboard.GetText();
                    else
                        Clipboards[i - 1] = Clipboard.GetImage();
                }
            }
        }

        #region Panel Colour Changes
        private void pnlClipboard1_MouseMove(object sender, MouseEventArgs e)
        {
            pnlClipboard1.BackColor = Color.FromArgb(Blue.R - 20, Blue.G - 20, Blue.B - 20); ;
        }

        private void pnlClipboard2_MouseMove(object sender, MouseEventArgs e)
        {
            pnlClipboard2.BackColor = Color.FromArgb(Yellow.R - 20, Yellow.G - 20, Yellow.B - 20); ;
        }

        private void pnlClipboard3_MouseMove(object sender, MouseEventArgs e)
        {
            pnlClipboard3.BackColor = Color.FromArgb(Red.R - 20, Red.G - 20, Red.B - 20); ;
        }

        private void pnlClipboard4_MouseMove(object sender, MouseEventArgs e)
        {
            pnlClipboard4.BackColor = Color.FromArgb(Orange.R - 20, Orange.G - 20, Orange.B - 20); ;
        }

        private void pnlClipboard1_MouseLeave(object sender, EventArgs e)
        {
            pnlClipboard1.BackColor = Blue;
        }

        private void pnlClipboard2_MouseLeave(object sender, EventArgs e)
        {
            pnlClipboard2.BackColor = Yellow;
        }

        private void pnlClipboard3_MouseLeave(object sender, EventArgs e)
        {
            pnlClipboard3.BackColor = Red;
        }

        private void pnlClipboard4_MouseLeave(object sender, EventArgs e)
        {
            pnlClipboard4.BackColor = Orange;
        }
        #endregion

        #region Panel Clicks
        private void pnlClipboard1_Click(object sender, EventArgs e)
        {
            if (Clipboards[0].ToString() != "System.Drawing.Bitmap")
                Clipboard.SetText(lblClip1.Text);
            else
                Clipboard.SetImage(pnlClipboard1.BackgroundImage);
        }

        private void pnlClipboard2_Click(object sender, EventArgs e)
        {
            if (Clipboards[1].ToString() != "System.Drawing.Bitmap")
                Clipboard.SetText(lblClip2.Text);
            else
                Clipboard.SetImage(pnlClipboard2.BackgroundImage);
        }

        private void pnlClipboard3_Click(object sender, EventArgs e)
        {
            if (Clipboards[2].ToString() != "System.Drawing.Bitmap")
                Clipboard.SetText(lblClip3.Text);
            else
                Clipboard.SetImage(pnlClipboard3.BackgroundImage);
        }

        private void pnlClipboard4_Click(object sender, EventArgs e)
        {
            if (Clipboards[3].ToString() != "System.Drawing.Bitmap")
                Clipboard.SetText(lblClip4.Text);
            else
                Clipboard.SetImage(pnlClipboard4.BackgroundImage);
        }
        #endregion

    }
}
