using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stimulsoft.Report;
using System.Data.SqlClient;

namespace Hoteldari
{
    public partial class frmlistPaziresh : Form
    {
        public frmlistPaziresh()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = " select DISTINCT CodePaziresh,Tarikh,MablaghOtag,MablaghServiceMenu,Maliyat,MablaghNahaye from Paziresh where Tarikh between '"+AzTarikh.Text+"' AND '"+TaTarikh.Text+"'";
            adp.Fill(ds, "Paziresh");
            dgvFactor.DataSource = ds;
            dgvFactor.DataMember = "Paziresh";

            dgvFactor.Columns[0].HeaderText = "کد پذیرش";
            dgvFactor.Columns[1].HeaderText = "تاریخ پذیرش";
            dgvFactor.Columns[2].HeaderText = "مبلغ کل اتاق";
            dgvFactor.Columns[3].HeaderText = "مبلغ سرویس و تغذیه";
            dgvFactor.Columns[4].HeaderText = "مبلغ ارزش افزوده";
            dgvFactor.Columns[5].HeaderText = "مبلغ نهایی پذیرش";


            dgvFactor.Columns[2].Width = 120;
            dgvFactor.Columns[3].Width = 120;
            dgvFactor.Columns[4].Width = 130;
            dgvFactor.Columns[5].Width = 130;
        }

        private void frmlistPaziresh_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
           AzTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
           TaTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");

            Display();
        }

        private void AzTarikh_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void TaTarikh_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(dgvFactor.CurrentRow.Cells[0]);
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.CommandText = "Delete from Paziresh where CodePaziresh=@N";
                cmd.Parameters.AddWithValue("@N", x);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("حذف با موفقیت انجام شد");
            }
            catch
            {
                MessageBox.Show("مشکلی پیش آمده است");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptListPaziresh.mrt");
            Report.Compile();
            Report["strTarikh1"] = AzTarikh.Text;
            Report["strTarikh2"] = TaTarikh.Text;
            Report.ShowWithRibbonGUI();
        }
    }
}
