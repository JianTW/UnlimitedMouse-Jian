using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UnlimitedMouse
{
    public partial class frmMain : Form
    {
        private int Threshold = 2;
        private int TotalWidth = 0;
        private int TotalHeight = 0;

        //如果滑鼠落在左右邊界的話，就要交換
        private int LeftThreshold, RightThreshold;
        public frmMain()
        {
            InitializeComponent();
            //foreach (Screen screen in Screen.AllScreens)
            //    TotalWidth += screen.Bounds.Width;
            LeftThreshold = Screen.AllScreens[0].Bounds.X + Threshold;
            RightThreshold = Screen.AllScreens[Screen.AllScreens.Length - 1].Bounds.Right - Threshold;

            TotalHeight = Screen.PrimaryScreen.Bounds.Height;
            niMain.Icon = this.Icon;
            niMain.ShowBalloonTip(2000, "Unlimited Mouse Jian", "Starting...", ToolTipIcon.Info);
            miLoopVertically.Checked = Properties.Settings.Default.LoopVertically;
        }

        private void tmrMouse_Tick(object sender, EventArgs e)
        {
            Screen[] s = Screen.AllScreens;

            int X = System.Windows.Forms.Cursor.Position.X;
            int Y = System.Windows.Forms.Cursor.Position.Y;
            lblMsg.Text = System.Windows.Forms.Cursor.Position.X + ", " + System.Windows.Forms.Cursor.Position.Y;

            if (X < LeftThreshold)
                System.Windows.Forms.Cursor.Position = new Point(RightThreshold,Y);
            else if (X > RightThreshold)
                System.Windows.Forms.Cursor.Position = new Point(LeftThreshold,Y);
            
            if (!miLoopVertically.Checked)
                return;

            if (Y < Threshold)
                System.Windows.Forms.Cursor.Position = new Point(X, TotalHeight - Threshold);
            else if (Y > TotalHeight - Threshold)
                System.Windows.Forms.Cursor.Position = new Point(X, Threshold);
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void frmMain_VisibleChanged(object sender, EventArgs e)
        {
#if !DEBUG
            this.Hide();
#endif
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LoopVertically = miLoopVertically.Checked;
            Properties.Settings.Default.Save();
        }

        private void niMain_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
