using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sysEmployee
{
    public partial class workF : Form
    {
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=sys_emp;";
        public workF()
        {
            InitializeComponent();
        }

        
        private void workF_Load_1(object sender, EventArgs e)
        {
            this.Size = new Size(950, 520);
            txt_id.ReadOnly = true;
            string cmd = "SELECT * FROM db_dayf df INNER JOIN db_emp emp on df.id_emp=emp.id_emp";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmd, databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            int no = 1;
            while (reader.Read())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = no++.ToString();
                //listViewItem.Text = reader["id_emp"].ToString();
                listViewItem.SubItems.Add(reader["id_emp"]+"_"+reader.GetString("emp_name"));
                listViewItem.SubItems.Add(reader["date"].ToString());
                listViewItem.SubItems.Add(reader["description"].ToString());
                listView1.Items.Add(listViewItem);
            }

            loadEmp();
            btn_update.Enabled = false;
            btn_del.Enabled = false;
        }

        //load Employee
        private void loadEmp()
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand("SELECT emp.id_emp,emp.emp_name FROM db_emp emp", databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["id_emp"] + reader.GetString("emp_name"));
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || txt_date.Text == "" || txt_desc.Text == "")
            {
                MessageBox.Show("ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ");
            }
            else
            {
                string query = "INSERT INTO `db_dayf` (`id_dayf`, `date`, `description`, `id_emp`) VALUES (NULL, '" + txt_date.Text + "', '" + txt_desc.Text + "', '" + comboBox1.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    MessageBox.Show("Create succesfully");
                    databaseConnection.Close();
                    txt_id.Text = "";
                    txt_date.Text = "";
                    txt_desc.Text = "";
                    comboBox1.Text = "";
                }
                catch(Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmdSql = "SELECT emp.id_emp,emp.emp_name,df.date,df.description FROM db_emp emp INNER JOIN db_dayf df;";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmdSql, databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            ListViewItem listViewItem = new ListViewItem();

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                //id Emp
                txt_id.Text = item.SubItems[0].Text;
                //emp_name
                comboBox1.Text = item.SubItems[1].Text;
                //date
                txt_date.Text = item.SubItems[2].Text;
                //description
                txt_desc.Text = item.SubItems[3].Text;

            };
            btn_save.Enabled = false;
            btn_update.Enabled = true;
            btn_del.Enabled = true;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string cmdSql="UPDATE db_dayf SET id_emp='"+comboBox1.Text+"',date='"+txt_date.Text+"',description='"+txt_desc.Text+"' WHERE id_emp='"+txt_id.Text+"'";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmdSql, databaseConnection);
            commandDatabase.Parameters.AddWithValue("id_dayf", txt_id);
            commandDatabase.Parameters.AddWithValue("id_emp", comboBox1);
            commandDatabase.Parameters.AddWithValue("date", txt_date);
            commandDatabase.Parameters.AddWithValue("description", txt_desc);
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                MessageBox.Show("Update succesfully");
                databaseConnection.Close();
                txt_id.Text = "";
                txt_date.Text = "";
                txt_desc.Text = "";
                comboBox1.Text = "";

            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message,cmdSql);

            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure Delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand("DELETE FROM db_dayf WHERE id_emp=" + txt_id.Text, databaseConnection);
                try
                {

                    databaseConnection.Open();
                    commandDatabase.ExecuteNonQuery();
                    MessageBox.Show("Delete succesfully");
                    databaseConnection.Close();
                    txt_id.Text = "";
                    txt_date.Text = "";
                    txt_desc.Text = "";
                    comboBox1.Text = "";

                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("You are pressed No!");
            }
        }
        private void refresh_Click(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            workF Check = new workF();
            Check.Show();
            Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
