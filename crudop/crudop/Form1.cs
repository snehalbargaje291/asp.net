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

namespace crudop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H5AOA7A\SQLEXPRESS;Initial Catalog=crudop;Integrated Security=True");
        bool Mode = true;
        string id;
        string query;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void clear_btn()
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtEmail.Clear();
            txtPhno.Clear();
            txtName.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clear_btn();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'crudopDataSet.register' table. You can move, or remove it, as needed.
            //this.registerTableAdapter.Fill(this.crudopDataSet.register);



        }

        //private void button1_Click(object sender, EventArgs e)
        private void button1_Click(object sender, EventArgs e)
        {
            string Name = txtName.Text;
            string Course = txtCourse.Text;
            string Fee = txtFee.Text;
            string Email = txtEmail.Text;
            string Phno = txtPhno.Text;
            SqlTransaction transaction = null;

            try
            {

                if (Mode == true) 
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    query = "Insert into register(sname,course,fee,email,phno) values('" + txtName.Text + "','" + txtCourse.Text + "','" + txtFee.Text + "','" + txtEmail.Text + "','" + txtPhno.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    transaction.Commit();
                    MessageBox.Show("Data Inserted SuccessFully");
                    conn.Close();
                    load();
                }
                else
                {
                    //id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    query = "Update register set sname='" + Name + "',course='" + Course + "',fee='" + Fee + "',email='" + Email + "',phno='" + Phno + "' where id ='" + id + "' ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Transaction = transaction;
                    button1.Text = "Submit";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    transaction.Commit();
                    MessageBox.Show("Data Updated SuccessFully");
                    conn.Close();
                    load();
                    clear_btn();
                    Mode = true;
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                conn.Close();
            }
            finally
            {

            }

        }

        public void load()
        {
            try
            {
                query = "SELECT * FROM register";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                dataGridView1.Rows.Clear();
                while(read.Read()){
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3], read[4], read[5]);
                }
                read.Dispose();
                conn.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }

        }

        public void getID(String id)
        {
            conn.Open();
            query = "SELECT * FROM register where id= '"+ id +"' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader read= cmd.ExecuteReader();

            while (read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFee.Text = read[3].ToString();
                txtEmail.Text = read[4].ToString();
                txtPhno.Text = read[5].ToString();
            }

            conn.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
                load();

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                query = "Delete from register where id = '" + id + "' ";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully!");
                conn.Close();
                load();
            }
        }

    }
}
