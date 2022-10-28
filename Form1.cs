using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int i = 0;
        public Form1()
        {
            InitializeComponent();
        }


        public void DB_Connect()
        {
            String oradb = "DATA SOURCE=172.16.54.24:1521/ictorcl;USER ID=IT178;Password=student";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DB_Connect();
            comm = new OracleCommand();
            comm.CommandText = "select regno from STUDENT";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "Tbl_STUDENT");
            dt = ds.Tables["Tbl_STUDENT"];
            int t = dt.Rows.Count;
            
            comboBox1.DataSource = dt.DefaultView;
            comboBox1.DisplayMember = "REGNO";
            conn.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DB_Connect();
            comm = new OracleCommand();
            comm.CommandText = "select * from STUDENT";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "Tbl_STUDENT");
            dt = ds.Tables["Tbl_STUDENT"];
            int t = dt.Rows.Count;
            MessageBox.Show(t.ToString());
            dr = dt.Rows[i];
            textBox1.Text = dr["REGNO"].ToString();
            textBox2.Text = dr["NAME"].ToString();
            textBox3.Text = dr["MAJOR"].ToString();
            textBox4.Text = dr["BDATE"].ToString();
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            i--;
            if (i < 0)
                i = dt.Rows.Count - 1;
            dr = dt.Rows[i];
            textBox1.Text = dr["REGNO"].ToString();
            textBox2.Text = dr["NAME"].ToString();
            textBox3.Text = dr["MAJOR"].ToString();
            textBox4.Text = dr["BDATE"].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i++;
            if (i >= dt.Rows.Count)
                i = 0;
            dr = dt.Rows[i];
            textBox1.Text = dr["REGNO"].ToString();
            textBox2.Text = dr["NAME"].ToString();
            textBox3.Text = dr["MAJOR"].ToString();
            textBox4.Text = dr["BDATE"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB_Connect();
             //DataSetDateTime  v = DateTime.Parse(textBox4.Text);
            OracleCommand cm = new OracleCommand();
            cm.Connection = conn;
            cm.CommandText = "insert into Student values (:n1,:n2,:n3,:n4)";
            cm.CommandType = CommandType.Text;
            //Uses OracleParameter to read the parameter from the GUI 
            OracleParameter pa1 = new OracleParameter();
            pa1.ParameterName = "n1";
            pa1.DbType = DbType.Int32;
            pa1.Value = int.Parse(textBox1.Text);

            OracleParameter pa2 = new OracleParameter();
            pa2.ParameterName = "n2";
            pa2.DbType = DbType.String;
            pa2.Value = textBox2.Text;

            OracleParameter pa3 = new OracleParameter();
            pa3.ParameterName = "n3";
            pa3.DbType = DbType.String;
            pa3.Value = textBox3.Text;

            OracleParameter pa4 = new OracleParameter();
            pa4.ParameterName = "n4";
            pa4.DbType = DbType.DateTime;
            pa4.Value = DateTime.Parse(textBox4.Text);

            

            cm.Parameters.Add(pa1);
            cm.Parameters.Add(pa2);
            cm.Parameters.Add(pa3);
            cm.Parameters.Add(pa4);
            cm.ExecuteNonQuery();
            MessageBox.Show("Inserted");
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB_Connect();
           // int v = int.Parse(textBox3.Text);
            OracleCommand cm = new OracleCommand();
            cm.Connection = conn;
            cm.CommandText = "update STUDENT set major=:pb where regno =:pdn";
            cm.CommandType = CommandType.Text;
            //Uses OracleParameter to read the parameter from the GUI 
            OracleParameter pa1 = new OracleParameter();
            pa1.ParameterName = "pb";
            pa1.DbType = DbType.String;
            pa1.Value = textBox3.Text;
            OracleParameter pa2 = new OracleParameter();
            pa2.ParameterName = "pdn";
            pa2.DbType = DbType.Int32;
            pa2.Value = int.Parse(textBox1.Text);
            cm.Parameters.Add(pa1);
            cm.Parameters.Add(pa2);
            cm.ExecuteNonQuery();
            MessageBox.Show("updated");
            conn.Close();

        }

        private void Gridview_Click(object sender, EventArgs e)
        {
            DB_Connect();
            comm = new OracleCommand();
            comm.CommandText = "select * from STUDENT";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "student");
            dataGridView1.DataSource = ds.Tables["Student"].DefaultView;
            /*dt = ds.Tables["Tbl_instructor"];
            int t = dt.Rows.Count;
            MessageBox.Show(t.ToString());
            dr = dt.Rows[i];
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "instructor"; // Database Table name
            conn.Close();*/
        }
    }
}
