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
    public partial class frmListService : Form
    {
        public frmListService()
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
            adp.SelectCommand.CommandText = "select * from Service";
            adp.Fill(ds, "Service");
            dgvService.DataSource = ds;
            dgvService.DataMember = "Service";
        }
        private void frmListService_Load(object sender, EventArgs e)
        {
            Display();
            dgvService.Columns[0].HeaderText = "کد";
            dgvService.Columns[1].HeaderText = "نام سرویس";
            dgvService.Columns[2].HeaderText = "قیمت سرویس";
            dgvService.Columns[3].HeaderText = "توضیحات";
            dgvService.Columns[3].Width = 150;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvService.SelectedCells[0].Value);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "Delete from Service where IdService=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف با موفقیت انجام شد");
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmService frm = new frmService();
            frm.lblId.Text = dgvService[0, dgvService.CurrentRow.Index].Value.ToString();
            frm.txtNameService.Text = dgvService[1, dgvService.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvService[2, dgvService.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvService[3, dgvService.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNameService_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Service where NameService like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameService.Text + "%");
            adp.Fill(ds, "Service");
            dgvService.DataSource = ds;
            dgvService.DataMember = "Service";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptService.mrt");
            Report.Compile();
            Report.ShowWithRibbonGUI();
        }
    }
}
