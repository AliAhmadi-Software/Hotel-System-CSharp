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
    public partial class frmListMenu : Form
    {
        public frmListMenu()
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
            adp.SelectCommand.CommandText = "select * from Menu";
            adp.Fill(ds, "Menu");
            dgvMenu.DataSource = ds;
            dgvMenu.DataMember = "Menu";
        }

        private void frmListMenu_Load(object sender, EventArgs e)
        {
            Display();
            dgvMenu.Columns[0].HeaderText = "کد";
            dgvMenu.Columns[1].HeaderText = "نام منو";
            dgvMenu.Columns[2].HeaderText = "قیمت منو";
            dgvMenu.Columns[3].HeaderText = "توضیحات";
        }

        private void txtNameMenu_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Menu where NameMenu like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameMenu.Text + "%");
            adp.Fill(ds, "Menu");
            dgvMenu.DataSource = ds;
            dgvMenu.DataMember = "Menu";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvMenu.SelectedCells[0].Value);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Delete from Menu where IdMenu=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف با موفقیت انجام شد");
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            frm.lblId.Text = dgvMenu[0, dgvMenu.CurrentRow.Index].Value.ToString();
            frm.txtNameMenu.Text = dgvMenu[1, dgvMenu.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvMenu[2, dgvMenu.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvMenu[3, dgvMenu.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptMenu.mrt");
            Report.Compile();
            Report.ShowWithRibbonGUI();
        }
    }
}
