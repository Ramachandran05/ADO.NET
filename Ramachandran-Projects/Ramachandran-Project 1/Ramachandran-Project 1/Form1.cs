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

namespace Ramachandran_Project_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection con = new SqlConnection(@"Data source=(localdb)\v11.0;Initial Catalog=ajith;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool mode = true;
        string sql;



        public void Load()
        {
            try
            {
                sql = "select * from students";
                cmd = new SqlCommand(sql, con);
                con.Open();

                read = cmd.ExecuteReader();

                drr = new SqlDataAdapter(sql, con);
                dataGridView1.Rows.Clear();


                while(read.Read())
                {


                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public void getId(string Id)
        {
            sql = "select * from students where id = '" + Id + "'  ";

            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while(read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFee.Text = read[3].ToString();
            }
            con.Close();
        }





        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;

            if (mode == true)
            {
                sql = "insert into students(StudentName,Course,Fees) values(@StudentName,@Course,@Fees)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@StudentName", name);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@Fees", fee);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update students set StudentName = @StudentName, Course = @Course,Fees = @Fees where Id = @Id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@StudentName", name);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@Fees", fee);
                cmd.Parameters.AddWithValue("@Id", id);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
                button1.Text = "Save";
                mode = true;
                    


            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if(e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getId(id);
                button1.Text = "Edit";

            }
            else if(e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from students where id = @Id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id",id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deletee");
                con.Close();




            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtName.Focus();
            button1.Text = "Save";
            mode = true;
        }
    }
}
