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
    public partial class frmListOtag : Form
    {
        public frmListOtag()
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
            adp.SelectCommand.CommandText = "select * from Otag";
            adp.Fill(ds, "Otag");
            dgvOtag.DataSource = ds;
            dgvOtag.DataMember = "Otag";
        }
        private void frmListOtag_Load(object sender, EventArgs e)
        {
            Display();

            dgvOtag.Columns[0].HeaderText = "ردیف";
            dgvOtag.Columns[1].HeaderText = "شماره اتاق";
            dgvOtag.Columns[2].HeaderText = "گروه";
            dgvOtag.Columns[3].HeaderText = "امکانات";
            dgvOtag.Columns[4].HeaderText = "هزینه هر روز";
            dgvOtag.Columns[5].HeaderText = "وضعیت";
            dgvOtag.Columns[6].HeaderText = "توضیحات";

            DataTable dt = new DataTable();
            DataBase db = new DataBase();
            dt = db.MySelect("select NameGroup from Groups");
            cmbGroup.DataSource = dt;
            cmbGroup.DisplayMember = ("NameGroup");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvOtag.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Otag where IdOtag=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("حذف با موفقیت انجام شد");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            frmOtag frm = new frmOtag();
            frm.txtcodeOtag.Text = dgvOtag[1, dgvOtag.CurrentRow.Index].Value.ToString();
            frm.cmbGroup.Text = dgvOtag[2, dgvOtag.CurrentRow.Index].Value.ToString();
            frm.txtEmkanat.Text = dgvOtag[3, dgvOtag.CurrentRow.Index].Value.ToString();
            frm.txtMablagh.Text = dgvOtag[4, dgvOtag.CurrentRow.Index].Value.ToString();
            frm.cmbVaziyat.Text = dgvOtag[5, dgvOtag.CurrentRow.Index].Value.ToString();
            frm.txtTozih.Text = dgvOtag[6, dgvOtag.CurrentRow.Index].Value.ToString();
            frm.ShowDialog();
        }

        private void cmbGroup_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Otag where NameGroup Like  '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", cmbGroup.Text + "%");
            adp.Fill(ds,"Otag");
            dgvOtag.DataSource = ds;
            dgvOtag.DataMember = "Otag";
        }

        private void cmbVaziyat_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Otag where Vaziyat Like  '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", cmbVaziyat.Text + "%");
            adp.Fill(ds, "Otag");
            dgvOtag.DataSource = ds;
            dgvOtag.DataMember = "Otag";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            StiReport Report = new StiReport();
            Report.Load("Report/rptOtag.mrt");
            Report.Compile();
            Report.ShowWithRibbonGUI();

        }
    }
}
