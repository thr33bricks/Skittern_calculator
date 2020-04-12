using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sreden_uspeh_10
{
    public partial class Form1 : Form
    {
        //Yordan Yordanov, April 2020
        //Images designed by "Freepik" from Flaticon (Flaticon Basic License)

        char separator, wrongSeparator;
        int lang = 0;

        #region Movable
        //====================================================================================
        //Declarations used for making the form movable
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //====================================================================================
        //Events to make the form movable

        private void CheckBeingMoved(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panelUpper_MouseMove(object sender, MouseEventArgs e)
        {
            CheckBeingMoved(e);
        }

        private void lblFormHeading_MouseMove(object sender, MouseEventArgs e)
        {
            CheckBeingMoved(e);
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            CheckBeingMoved(e);
        }

        private void pbPattern_MouseMove(object sender, MouseEventArgs e)
        {
            CheckBeingMoved(e);
        }

        private void lblQu_MouseMove(object sender, MouseEventArgs e)
        {
            CheckBeingMoved(e);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            CheckBeingMoved(e);
        }

        //====================================================================================
        #endregion

        #region Snappable
        private const int SnapDist = 100;
        private bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SnapDist;
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            Screen scn = Screen.FromPoint(this.Location);
            if (DoSnap(this.Left, scn.WorkingArea.Left)) this.Left = scn.WorkingArea.Left;
            if (DoSnap(this.Top, scn.WorkingArea.Top)) this.Top = scn.WorkingArea.Top;
            if (DoSnap(scn.WorkingArea.Right, this.Right)) this.Left = scn.WorkingArea.Right - this.Width;
            if (DoSnap(scn.WorkingArea.Bottom, this.Bottom)) this.Top = scn.WorkingArea.Bottom - this.Height;
        }
        #endregion

        public Form1()
        {
            InitializeComponent();

            separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            if (separator == ',')
                wrongSeparator = '.';
            else if (separator == '.')
                wrongSeparator = ',';

            changeLang(lang);
        }

        #region Title bar buttons
        private void form_mini_MouseEnter(object sender, EventArgs e)
        {
            form_mini.BackColor = ColorTranslator.FromHtml("#272a2e");
        }

        private void form_mini_MouseLeave(object sender, EventArgs e)
        {
            form_mini.BackColor = ColorTranslator.FromHtml("#202225");
        }

        private void form_mini_MouseDown(object sender, MouseEventArgs e)
        {
            form_mini.BackColor = ColorTranslator.FromHtml("#2b2e32");
        }

        private void form_mini_MouseUp(object sender, MouseEventArgs e)
        {
            form_mini.BackColor = ColorTranslator.FromHtml("#272a2e");
        }

        private void formClose_MouseEnter(object sender, EventArgs e)
        {
            Image close_clicked = Properties.Resources.close_clicked;
            formClose.BackColor = ColorTranslator.FromHtml("#f04747");
            pbClose.Image = (Image)(new Bitmap(close_clicked));
        }

        private void formClose_MouseLeave(object sender, EventArgs e)
        {
            Image close = Properties.Resources.close;
            formClose.BackColor = ColorTranslator.FromHtml("#202225");
            pbClose.Image = (Image)(new Bitmap(close));
        }

        private void form_mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void formClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Button About
        private void btnAboutOut_Click(object sender, EventArgs e)
        {
            btnAbout_Click();
        }

        private void btnAboutOut_MouseEnter(object sender, EventArgs e)
        {
            btnAbout_MouseEnter();
        }

        private void btnAboutOut_MouseLeave(object sender, EventArgs e)
        {
            btnAbout_MouseLeave();
        }

        private void btnAboutIn_Click(object sender, EventArgs e)
        {
            btnAbout_Click();
        }

        private void btnAboutIn_MouseEnter(object sender, EventArgs e)
        {
            btnAbout_MouseEnter();
        }

        private void btnAboutIn_MouseLeave(object sender, EventArgs e)
        {
            btnAbout_MouseLeave();
        }

        private void lblBtnAbout_Click(object sender, EventArgs e)
        {
            btnAbout_Click();
        }

        private void lblBtnAbout_MouseEnter(object sender, EventArgs e)
        {
            btnAbout_MouseEnter();
        }

        private void lblBtnAbout_MouseLeave(object sender, EventArgs e)
        {
            btnAbout_MouseLeave();
        }

        private void btnAbout_Click()
        {
            frmAbout about = new frmAbout(lang);
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog(this);
        }

        private void btnAbout_MouseEnter()
        {
            btnAboutOut.BackColor = Color.FromArgb(0, 122, 204);
            btnAboutIn.BackColor = Color.LightGray;
        }

        private void btnAbout_MouseLeave()
        {
            btnAboutOut.BackColor = Color.DimGray;
            btnAboutIn.BackColor = Color.Gainsboro;
        }
        #endregion

        #region Button Result
        private void btnResOut_Click(object sender, EventArgs e)
        {
            btnRes_Click();
        }

        private void btnResOut_MouseEnter(object sender, EventArgs e)
        {
            btnRes_MouseEnter();
        }

        private void btnResOut_MouseLeave(object sender, EventArgs e)
        {
            btnRes_MouseLeave();
        }

        private void btnResIn_Click(object sender, EventArgs e)
        {
            btnRes_Click();
        }

        private void btnResIn_MouseEnter(object sender, EventArgs e)
        {
            btnRes_MouseEnter();
        }

        private void btnResIn_MouseLeave(object sender, EventArgs e)
        {
            btnRes_MouseLeave();
        }

        private void lblBtnRes_Click(object sender, EventArgs e)
        {
            btnRes_Click();
        }

        private void lblBtnRes_MouseEnter(object sender, EventArgs e)
        {
            btnRes_MouseEnter();
        }

        private void lblBtnRes_MouseLeave(object sender, EventArgs e)
        {
            btnRes_MouseLeave();
        }

        private void btnRes_Click()
        {
            calculateResult();
        }

        private void btnRes_MouseEnter()
        {
            btnResOut.BackColor = Color.FromArgb(0, 122, 204);
            btnResIn.BackColor = Color.LightGray;
        }

        private void btnRes_MouseLeave()
        {
            btnResOut.BackColor = Color.DimGray;
            btnResIn.BackColor = Color.Gainsboro;
        }
        #endregion

        #region Button Flag
        private void btnFlagOut_Click(object sender, EventArgs e)
        {
            btnFlag_Click();
        }

        private void btnFlagOut_MouseEnter(object sender, EventArgs e)
        {
            btnFlag_MouseEnter();
        }

        private void btnFlagOut_MouseLeave(object sender, EventArgs e)
        {
            btnFlag_MouseLeave();
        }

        private void pbFlagIn_Click(object sender, EventArgs e)
        {
            btnFlag_Click();
        }

        private void pbFlagIn_MouseEnter(object sender, EventArgs e)
        {
            btnFlag_MouseEnter();
        }

        private void pbFlagIn_MouseLeave(object sender, EventArgs e)
        {
            btnFlag_MouseLeave();
        }

        private void btnFlag_Click()
        {
            lang = lang ^ 1;
            changeLang(lang);
        }

        private void btnFlag_MouseEnter()
        {
            btnFlagOut.BackColor = Color.FromArgb(0, 122, 204);
        }

        private void btnFlag_MouseLeave()
        {
            btnFlagOut.BackColor = Color.DimGray;
        }
        #endregion

        #region TextBox events
        private void tbWaist_TextChanged(object sender, EventArgs e)
        {
            lblRes.Text = "?";
            lblOB.Text = "?";
        }

        private void tbAngle_TextChanged(object sender, EventArgs e)
        {
            lblRes.Text = "?";
            lblOB.Text = "?";
        }

        private void tbLength_TextChanged(object sender, EventArgs e)
        {
            lblRes.Text = "?";
            lblOB.Text = "?";
        }

        private void tbWaist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbAngle.Focus();
            else if (e.KeyCode == Keys.Down)
                tbAngle.Focus();
            else if (e.KeyCode == Keys.Up)
                tbLength.Focus();
        }

        private void tbAngle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbLength.Focus();
            else if (e.KeyCode == Keys.Down)
                tbLength.Focus();
            else if (e.KeyCode == Keys.Up)
                tbWaist.Focus();
        }

        private void tbLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnlMain.Focus();
                calculateResult();
            }
            else if (e.KeyCode == Keys.Down)
                tbWaist.Focus();
            else if (e.KeyCode == Keys.Up)
                tbAngle.Focus();
        }
        #endregion

        #region Misc
        private void pnlMain_Click(object sender, EventArgs e)
        {
            pnlMain.Focus();
        }

        private void pbPattern_Click(object sender, EventArgs e)
        {
            pnlMain.Focus();
        }

        private void pnlMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                calculateResult();
            }
        }

        private string fixSep(string text)
        {
            string res = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == wrongSeparator)
                    res += separator;
                else
                    res += text[i];
            }
            return res;
        }

        private void changeLang(int lang)
        {
            if (lang == 1)
            {
                lblBtnAbout.Text = "About";
                lblWaist.Text = "Waist";
                lblAngle.Text = "Angle";
                lblLength.Text = "Length";
                lblRadius.Text = "Radius";
                lblResult.Text = "Result";

                pbFlag.Image = Properties.Resources.bg;
            }
            else if (lang == 0)
            {
                lblBtnAbout.Text = "Относно";
                lblWaist.Text = "Талия";
                lblAngle.Text = "Ъгъл";
                lblLength.Text = "Дължина";
                lblRadius.Text = "Радиус";
                lblResult.Text = "Резултат";

                pbFlag.Image = Properties.Resources.us;
            }
        }
        #endregion

        private double sin(double angleDeg)
        {
            return Math.Sin(angleDeg * Math.PI / 180);
        }       

        private void calculateResult()
        {
            double waist, angle, length;
            try
            {
                waist = double.Parse(fixSep(tbWaist.Text));
                angle = double.Parse(fixSep(tbAngle.Text));
                length = double.Parse(fixSep(tbLength.Text));
            }
            catch{ return; }
            if (angle > 90)
                return;

            double oa = Math.Round((waist / (2 * Math.PI)) / sin(angle), 2);
            lblRes.Text = oa.ToString();
            lblOB.Text = Math.Round((oa + length), 2).ToString();
        }
    }
}
