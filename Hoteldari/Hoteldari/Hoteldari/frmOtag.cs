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
    public partial class frmOtag : Form
    {
        public frmOtag()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data source =(local);initial catalog =Hoteldari ; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        private void frmOtag_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataBase db = new DataBase();
            dt = db.MySelect("select NameGroup from Groups");
            cmbGroup.DataSource = dt;
            cmbGroup.DisplayMember=("NameGroup");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.CommandText = "insert into Otag (CodeOtag,NameGroup,Emkanat,Mablagh,Vaziyat,Tozih)values(@a,@b,@c,@d,@e,@f)";
            cmd.Parameters.AddWithValue("@a", txtcodeOtag.Text);
            cmd.Parameters.AddWithValue("@b", cmbGroup.Text);
            cmd.Parameters.AddWithValue("@c", txtEmkanat.Text);
            cmd.Parameters.AddWithValue("@d", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@e", cmbVaziyat.Text);
            cmd.Parameters.AddWithValue("@f", txtTozih.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("عملیات با موفقیت ثبت شد");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Update Otag set CodeOtag='"+txtcodeOtag.Text+"',NameGroup='"+cmbGroup.Text+"',Emkanat='"+txtEmkanat.Text+"',Mablagh='"+txtMablagh.Text+"',Vaziyat='"+cmbVaziyat.Text+"',Tozih='"+txtTozih.Text+"' where CodeOtag="+ txtcodeOtag.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("عملیات با موفقیت ثبت شد");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            new frmListOtag().ShowDialog();
        }
    }
}
