using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hoteldari
{
    public partial class frmGroup : Form
    {
        public frmGroup()
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
            adp.SelectCommand.CommandText = "select * from Groups";
            adp.Fill(ds, "Groups");
            dgvGroup.DataSource = ds;
            dgvGroup.DataMember = "Groups";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Groups(NameGroup)values(@a)";
            cmd.Parameters.AddWithValue("@a",txtGroup.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void frmGroup_Load(object sender, EventArgs e)
        {
            Display();

            dgvGroup.Columns[0].HeaderText = "کد گروه";
            dgvGroup.Columns[1].HeaderText = "نام گروه";
            dgvGroup.Columns[1].Width = 120;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvGroup.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Groups where IdGroup=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            MessageBox.Show("عملیات با موفقیت انجام شد");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
