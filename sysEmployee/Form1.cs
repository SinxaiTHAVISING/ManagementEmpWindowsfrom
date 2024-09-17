using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sysEmployee
{
   
    public partial class Form1 : Form
    {
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=sys_emp;";
        // Your query,
        //string query = "SELECT emp.id_emp,emp.emp_name,emp.emp_gender,emp.emp_salary FROM db_emp AS emp;";
        string query = "SELECT * FROM db_emp";
        public Form1()
        {
            InitializeComponent();

            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            // Let's do it !
            try
            {
                // Open the database
                databaseConnection.Open();

                // Execute the query
                //reader = commandDatabase.ExecuteReader();

                // Finally close the connection
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(950, 520);

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            //count All emp
            MySqlCommand emp = new MySqlCommand("SELECT COUNT(*) FROM db_emp", databaseConnection);
            Int32 resEmpcount = Convert.ToInt32(emp.ExecuteScalar());
            all.Text = resEmpcount.ToString();
            //---------------------------------------------------------------------
            //count male emp
            MySqlCommand Genmale = new MySqlCommand("SELECT COUNT(*) FROM db_emp  WHERE db_emp.emp_gender='ຊາຍ';", databaseConnection);
            string resmal = Convert.ToString(Genmale.ExecuteScalar());
            male.Text = resmal.ToString();

            //count female emp
            MySqlCommand Genfemale = new MySqlCommand("SELECT COUNT(*) FROM db_emp  WHERE db_emp.emp_gender='ຍິງ';", databaseConnection);
            string resfemal = Convert.ToString(Genfemale.ExecuteScalar());
            female.Text = resfemal.ToString();

            //count manage emp
            MySqlCommand empManage = new MySqlCommand("SELECT COUNT(pos) FROM db_emp as emp INNER JOIN db_posit as pos on emp.id_pos =pos.id_pos AND pos.pos='ພະນັກງານ (ສັນຍາຈ້າງ)';", databaseConnection);
            string resManage = Convert.ToString(empManage.ExecuteScalar());
            manage.Text = resManage.ToString();

            //count Off day
            MySqlCommand dayf = new MySqlCommand("SELECT COUNT(id_dayf) FROM db_dayf  df INNER JOIN db_emp  emp on emp.id_emp =df.id_emp;", databaseConnection);
            string dayoff = Convert.ToString(dayf.ExecuteScalar());
            wf.Text = dayoff.ToString();
            //MessageBox.Show(resManage.ToString());
        }

        private void ໜາທຳອດToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            Form1 Check = new Form1();
            Check.Show();
            Hide();
        }

        private void ພະນກງານທງໝດToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            employee Check = new employee();
            Check.Show();
        }

        private void ເພມຂມນToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            workF Check = new workF();
            Check.Show();
        }
        private void ພມລາຍງານພະນກງານToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            Department Check = new Department();
            Check.Show();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ພມລາຍງານພະນກງານຂາດToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            Position_tool Check = new Position_tool();
            Check.Show();
        }

        private void ຈດການຂມນສາຂາToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            Branch Check = new Branch();
            Check.Show();
        }

        private void ອອກຈາກToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }
    }
}
