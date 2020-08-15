using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class issue_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=C:\Users\Abdullah gulyani\Desktop\MyDatabase#1.sdf;Persist Security Info=True");
      
        public issue_books()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student_info where student_enrollment_no = '"+txt_enrollment.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 0)
            {

                MessageBox.Show("this enrollment no not found");

            }
            else
            {


                foreach (DataRow dr in dt.Rows)
                {

                    txt_studentname.Text = dr["student_name"].ToString();
                    txt_studentdpt.Text = dr["student_department"].ToString();
                    txt_studentsem.Text = dr["student_sem"].ToString();
                    txt_studentcontact.Text = dr["student_contact"].ToString();
                    txt_studentemail.Text = dr["student_email"].ToString();
                }

            }

        }

        private void issue_books_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            
            }
            con.Open();
        }

        private void txt_booksname_KeyUp(object sender, KeyEventArgs e)
        {
            int count =0;

            if (e.KeyCode != Keys.Enter)
            {

                listBox1.Items.Clear();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + txt_booksname.Text+ "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                 
                if(count >0)
                {
                listBox1.Visible = true;
                    foreach(DataRow dr in dt.Rows)
                    {
                    listBox1.Items.Add(dr["books_name"].ToString());
                    }
                }
            
            
            
            }

        }

        private void txt_booksname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                //you might need to select one value to allow arrow keys
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txt_booksname.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txt_booksname.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into issue_books value('"+txt_enrollment.Text+"','"+txt_studentname.Text+"','"+txt_studentdpt.Text+"','"+txt_studentsem.Text+"','"+txt_studentcontact.Text+"','"+txt_studentemail.Text+"','"+txt_booksname.Text+"','"+dateTimePicker1.Value.ToShortDateString()+"')";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Books issue successfully");
        }

    }
}
