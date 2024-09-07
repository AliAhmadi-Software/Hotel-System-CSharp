using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Stimulsoft.Report;

namespace Hoteldari
{
    public partial class frmListChekP : Form
    {
        public frmListChekP()
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
            adp.SelectCommand.CommandText = "Select * from ChekP where SarResid Between '" + mskSarResid1.Text + "' AND '" + mskSarResid2.Text + "' ";
            adp.Fill(ds,"ChekP");
            dgvChek.DataSource = ds;
            dgvChek.DataMember = "ChekP";

            dgvChek.Columns[0].HeaderText = "کد";
            dgvChek.Columns[1].HeaderText = "شماره حساب";
            dgvChek.Columns[2].HeaderText = "تاریخ ثبت";
            dgvChek.Columns[3].HeaderText = "سررسید";
            dgvChek.Columns[4].HeaderText = "مبلغ چک";
            dgvChek.Columns[5].HeaderText = "وضعیت";
            dgvChek.Columns[6].HeaderText = "در وجه";
            dgvChek.Columns[7].HeaderText = "توضیحات";
        }

        private void frmListChekP_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskSarResid1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            mskSarResid2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfYear(DateTime.Now).ToString("0#");
            Display();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvChek.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from ChekP where IdChekP=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmChekP frm = new frmChekP();
            frm.lblId.Text = dgvChek[0, dgvChek.CurrentRow.Index].Value.ToString();
            frm.txtShomareHesab.Text = dgvChek[1, dgvChek.CurrentRow.Index].Value.ToString();
            frm.mskTarikh.Text = dgvChek[2, dgvChek.CurrentRow.Index].Value.ToString();
            frm.mskSarResid.Text = dgvChek[3, dgvChek.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvChek[4, dgvChek.CurrentRow.Index].Value.ToString();
            frm.cmbVaziyat.Text = dgvChek[5, dgvChek.CurrentRow.Index].Value.ToString();
            frm.txtName.Text = dgvChek[6, dgvChek.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvChek[7, dgvChek.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void mskSarResid1_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void mskSarResid2_TextChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptChekP.mrt");
            Report.Compile();
            Report["strTarikh1"] = mskSarResid1.Text;
            Report["strTarikh2"] = mskSarResid2.Text;
            Report.ShowWithRibbonGUI();
        }

        private void btnVosol_Click(object sender, EventArgs e)
        {
            string str;
            int str1;

            con.Open();
            SqlCommand sqlcmd = new SqlCommand("select mablagh from Hesab where ShomareHesab ='" + Convert.ToInt32(dgvChek.SelectedCells[1].Value) + "'", con);
            str = Convert.ToString((int)sqlcmd.ExecuteScalar());
            str1 = Convert.ToInt32(dgvChek.SelectedCells[4].Value);
            if (str1 > Convert.ToInt32(str))
            {
                MessageBox.Show("موجودی حساب برای پرداخت این چک کافی نمی باشد");
            }
            else
            {
                int b = int.Parse(str) - str1;
                string update = "update Hesab set Mablagh='" + b + "' where ShomareHesab ='" + Convert.ToInt32(dgvChek.SelectedCells[1].Value) + "'";
                SqlCommand com = new SqlCommand(update, con);
                com.ExecuteNonQuery();
                //***************************************
                string updateVaziyat = "update chekP set Vaziyat='" + "وصول شده" + "' where IdChekP='" + Convert.ToInt32(dgvChek.SelectedCells[0].Value) + "'";
                SqlCommand cmd = new SqlCommand(updateVaziyat, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("مبلغ چک از حساب مورد نظر پرداخت شد و چک مورد نظر وصول شد");
            }
            con.Close();
        
        }
    }
}
