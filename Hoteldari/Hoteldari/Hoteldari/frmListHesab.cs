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
    public partial class frmListHesab : Form
    {
        public frmListHesab()
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
            adp.SelectCommand.CommandText = "select * from Hesab";
            adp.Fill(ds,"Hesab");
            dgvHesab.DataSource = ds;
            dgvHesab.DataMember = "Hesab";

            dgvHesab.Columns[0].HeaderText = "کد";
            dgvHesab.Columns[1].HeaderText = "نام حساب";
            dgvHesab.Columns[2].HeaderText = "شماره حساب";
            dgvHesab.Columns[3].HeaderText = "موجودی";
            dgvHesab.Columns[4].HeaderText = "توضیحات";
            dgvHesab.Columns[4].Width = 150;
        }
        private void frmListHesab_Load(object sender, EventArgs e)
        {
            try
            {
                Display();
            }
            catch (Exception)
            {
                
            }

        }

        private void txtNameHesab_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Hesab where NameHesab like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@s" , txtNameHesab.Text  + "%");
            adp.Fill(ds, "Hesab");
            dgvHesab.DataSource = ds;
            dgvHesab.DataMember = "Hesab";

            dgvHesab.Columns[0].HeaderText = "کد";
            dgvHesab.Columns[1].HeaderText = "نام حساب";
            dgvHesab.Columns[2].HeaderText = "شماره حساب";
            dgvHesab.Columns[3].HeaderText = "موجودی";
            dgvHesab.Columns[4].HeaderText = "توضیحات";
        }

        private void txtShomare_ValueChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Hesab where ShomareHesab like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@s", txtShomare.Text + "%");
            adp.Fill(ds, "Hesab");
            dgvHesab.DataSource = ds;
            dgvHesab.DataMember = "Hesab";

            dgvHesab.Columns[0].HeaderText = "کد";
            dgvHesab.Columns[1].HeaderText = "نام حساب";
            dgvHesab.Columns[2].HeaderText = "شماره حساب";
            dgvHesab.Columns[3].HeaderText = "موجودی";
            dgvHesab.Columns[4].HeaderText = "توضیحات";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            int x = Convert.ToInt32(dgvHesab.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "delete from Hesab where IdHesab=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmHesab frm = new frmHesab();
            frm.lblId.Text = dgvHesab[0, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtNameHesab.Text = dgvHesab[1, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtShomare.Text = dgvHesab[2, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvHesab[3, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvHesab[4, dgvHesab.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report=new StiReport();
            Report.Load("Report/rptHesab.mrt");
            Report.Compile();
            Report.ShowWithRibbonGUI();
        }
    }
}
