using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TODOLIST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            check();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source= DESKTOP-NVG7K5K\\SQLEXPRESS;Initial Catalog=To Do List;Integrated Security=True;Pooling=False");
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO [Table]([Entry Number],Activity,Date) VALUES (@EntryNumber,@Activity,@Date)", con);
            cmd.Parameters.AddWithValue("@EntryNumber", int.Parse(txtEnt.Text));
            cmd.Parameters.AddWithValue("@Activity", txtAct.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("SELECT * FROM [Table]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            txtEnt.Text = "";
            txtAct.Text = "";
            txtDate.Text = "";
           
            
           
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source= DESKTOP-NVG7K5K\\SQLEXPRESS;Initial Catalog=To Do List;Integrated Security=True;Pooling=False");
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [Table] SET Activity=@Activity,Date=@Date WHERE [Entry Number]=@EntryNumber", con);
            cmd.Parameters.AddWithValue("@EntryNumber", int.Parse(txtEnt.Text));
            cmd.Parameters.AddWithValue("@Activity", txtAct.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("SELECT * FROM [Table]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            txtEnt.Text = "";
            txtAct.Text = "";
            txtDate.Text = "";


           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source= DESKTOP-NVG7K5K\\SQLEXPRESS;Initial Catalog=To Do List;Integrated Security=True;Pooling=False");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE [Table] WHERE [Entry Number]=@EntryNumber", con);
            cmd.Parameters.AddWithValue("@EntryNumber", int.Parse(txtEnt.Text));
            cmd.Parameters.AddWithValue("@Activity", txtAct.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("SELECT * FROM [Table]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            txtEnt.Text = "";
            txtAct.Text = "";
            txtDate.Text = "";


            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source= DESKTOP-NVG7K5K\\SQLEXPRESS;Initial Catalog=To Do List;Integrated Security=True;Pooling=False");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Table]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source= DESKTOP-NVG7K5K\\SQLEXPRESS;Initial Catalog=To Do List;Integrated Security=True;Pooling=False");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Table] WHERE [Entry Number]=@EntryNumber", con);
            cmd.Parameters.AddWithValue("EntryNumber",int.Parse(txtEnt.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void check()
        {
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Completed";
            chk.Name = "Checkbox";
            // associate a method with the click event
            dataGridView1.Columns.Add(chk);

        }
        private void txtDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // check you clicked on the first column (index 0)
            if (this.dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                int entryNmbr = int.Parse(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString());
                // this is a checked task so delete here
                SqlConnection con = new SqlConnection("Data Source= DESKTOP-NVG7K5K\\SQLEXPRESS;Initial Catalog=To Do List;Integrated Security=True;Pooling=False");
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE [Table] WHERE [Entry Number]=@EntryNumber", con);
                cmd.Parameters.AddWithValue("@EntryNumber", entryNmbr);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM [Table]", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

                txtEnt.Text = "";
                txtAct.Text = "";
                txtDate.Text = "";
            }
            else
            {
                // any other click on any other column
            }
        }

   

       
        private void btnClear_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt != null)
                dt.Clear();
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source= DESKTOP-NVG7K5K\\SQLEXPRESS;Initial Catalog=To Do List;Integrated Security=True;Pooling=False");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Table]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
