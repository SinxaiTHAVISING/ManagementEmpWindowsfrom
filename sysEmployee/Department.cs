using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sysEmployee
{
    public partial class Department : Form
    {
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=sys_emp;";
        public Department()
        {
            InitializeComponent();
        }

        private void Department_Load(object sender, EventArgs e)
        {
            this.Size = new Size(950, 520);

            string cmd = "SELECT * FROM db_depart";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmd, databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            int no = 1;
            while (reader.Read())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = no++.ToString();
                listViewItem.SubItems.Add(reader["id_dep"].ToString());
                listViewItem.SubItems.Add(reader["dep_name"].ToString());
                listView1.Items.Add(listViewItem);
                
            }

            btn_update.Enabled = false;
            btn_del.Enabled = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ");
            }
            else
            {
                string query = "INSERT INTO `db_depart` (`id_dep`, `dep_name`) VALUES (NULL, '" + textBox1.Text + "')";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    MessageBox.Show("Create succesfully");
                    databaseConnection.Close();
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmdSql = "SELECT dep_name FROM db_depart";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmdSql, databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            ListViewItem listViewItem = new ListViewItem();

            if (listView1.SelectedItems.Count > 0&&reader.Read())
            {
                ListViewItem item = listView1.SelectedItems[0];
                //dep_name
                textBox1.Text = item.SubItems[2].Text;
                label2.Text = item.SubItems[1].Text;
            };
            btn_save.Enabled = false;
            btn_update.Enabled = true;
            btn_del.Enabled = true;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string cmdSql = "UPDATE db_depart SET dep_name='"+textBox1.Text+"' WHERE id_dep='"+label2.Text+"'";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmdSql, databaseConnection);
            commandDatabase.Parameters.AddWithValue("dep_name", textBox1);
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                MessageBox.Show("Update succesfully");
                databaseConnection.Close();
                textBox1.Text = "";

            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message, cmdSql);

            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure Delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand("DELETE FROM db_depart WHERE id_dep=" + label2.Text, databaseConnection);
                try
                {
                    databaseConnection.Open();
                    commandDatabase.ExecuteNonQuery();
                    MessageBox.Show("Delete succesfully");
                    databaseConnection.Close();
                    textBox1.Text = "";
                    Console.WriteLine(commandDatabase);
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                    Console.WriteLine(commandDatabase);
                }
            }
            else
            {
                MessageBox.Show("You are pressed No!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                databaseConnection.Open();
                this.Refresh();
                Department Check = new Department();
                Check.Show();
                Hide();
        }
    }
}
