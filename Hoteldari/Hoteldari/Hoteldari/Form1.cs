using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hoteldari
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            new frmInfo().ShowDialog();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            new frmKarbar().ShowDialog();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            new frmGroup().ShowDialog();
        }

        private void btnOtag_Click(object sender, EventArgs e)
        {
            new frmOtag().ShowDialog();
        }

        private void btnListOtag_Click(object sender, EventArgs e)
        {
            new frmListOtag().ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnService_Click(object sender, EventArgs e)
        {
            new frmService().ShowDialog();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            new frmMenu().ShowDialog();
        }

        private void btnHesab_Click(object sender, EventArgs e)
        {
            new frmHesab().ShowDialog();
        }

        private void btnListHesab_Click(object sender, EventArgs e)
        {
            new frmListHesab().ShowDialog();
        }

        private void btnChekP_Click(object sender, EventArgs e)
        {
            new frmChekP().ShowDialog();
        }

        private void btnListChekP_Click(object sender, EventArgs e)
        {
            new frmListChekP().ShowDialog();
        }

        private void btnChekD_Click(object sender, EventArgs e)
        {
            new frmChekD().ShowDialog();
        }

        private void btnListChekD_Click(object sender, EventArgs e)
        {
            new frmListChekD().ShowDialog();
        }

        private void btnPardakht_Click(object sender, EventArgs e)
        {
            new frmPardakht().ShowDialog();
        }

        private void btnDaryaft_Click(object sender, EventArgs e)
        {
            new frmVariz().ShowDialog();
        }

        private void btnTax_Click(object sender, EventArgs e)
        {
            new frmTax().Show();
        }

        private void btnHazineh_Click(object sender, EventArgs e)
        {
            new frmHazineh().ShowDialog();
        }

        private void btnListHazineh_Click(object sender, EventArgs e)
        {
            new frmListHazineh().ShowDialog();
        }

        private void btnPaziresh_Click(object sender, EventArgs e)
        {
            new frmPaziresh().ShowDialog();
        }

        private void btnListPaziresh_Click(object sender, EventArgs e)
        {
            new frmlistPaziresh().ShowDialog();
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("wordpad.exe");
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.Hour.ToString("0#");
            lblTime.Text += " : ";
            lblTime.Text += DateTime.Now.Minute.ToString("0#");
            lblTime.Text += " : ";
            lblTime.Text += DateTime.Now.Second.ToString("0#");
  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            lblDate.Text = p.GetYear(DateTime.Now).ToString()+ "/" + p.GetMonth(DateTime.Now).ToString("0#")+ "/" + p.GetDayOfYear(DateTime.Now).ToString("0#");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new frmPaziresh().ShowDialog();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            new frmListHesab().ShowDialog();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            new frmListChekP().ShowDialog();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            new frmListChekD();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            new frmPardakht().ShowDialog();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            new frmVariz().ShowDialog();
        } 
    }
}
