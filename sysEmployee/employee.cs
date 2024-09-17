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
using Mysqlx.Crud;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sysEmployee
{
    public partial class employee : Form
    {
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=sys_emp;";
        public employee()
        {
            InitializeComponent();
        }

        private void employee_Load(object sender, EventArgs e)
        {
            //ID.Text = "12";
            this.Size = new Size(950, 520);
            string cmdShow = "SELECT * FROM db_emp emp INNER JOIN db_posit pos INNER JOIN db_depart dp INNER JOIN db_edu brn ON emp.id_pos=pos.id_pos AND emp.id_dep=dp.id_dep AND emp.id_branc=brn.id_branc;";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmdShow, databaseConnection);
           // MySqlCommand commandDatabase = new MySqlCommand("SELECT emp.id_emp,emp.emp_name,emp.emp_age,emp.emp_gender,emp.emp_salary,emp.emp_tel,emp.emp_email,pos.id_pos,pos.pos FROM db_emp emp INNER JOIN db_posit pos on emp.id_pos=pos.id_pos;", databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            var num = 1;
            //if reader have data return true and reader=null return flase
             while (reader.Read())
              {
             ListViewItem listViewItem= new ListViewItem();
                listViewItem.Text =num++.ToString();
                listViewItem.SubItems.Add( reader["id_emp"].ToString());
                listViewItem.SubItems.Add(reader["emp_name"].ToString());
                listViewItem.SubItems.Add(reader["emp_gender"].ToString());
                listViewItem.SubItems.Add (reader["emp_age"].ToString());
                listViewItem.SubItems.Add(reader["id_branc"] + "_" + reader.GetString("branc"));
                listViewItem.SubItems.Add(reader["id_dep"] +"_"+ reader.GetString("dep_name"));
                listViewItem.SubItems.Add(reader["id_pos"] +"_"+ reader.GetString("pos"));
                listViewItem.SubItems.Add(reader["emp_salary"].ToString());
                listViewItem.SubItems.Add(reader["emp_tel"].ToString());
                listViewItem.SubItems.Add(reader["emp_email"].ToString());
                listView1.Items.Add(listViewItem);
               
              }
            //Run on load Department combobox function
            loadDep();
            //Run on load position combobox function
            loadPos();
            //Run on load branch to combobox function
            loadBranch();

            //set Don't Button 
            update.Enabled = false;
            delete.Enabled = false;
        }
        //----------------------------------------------------------------------------------------
        //function load Department to Combobox
        private void loadDep()
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand("SELECT id_dep,dep_name FROM db_depart",databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = command.ExecuteReader();
             while (reader.Read())
            {
               comboBox_dep.Items.Add(reader["id_dep"] + reader.GetString("dep_name"));

             }
            //foreach (var item in reader)
            //{comboBox_dep.Items.Add(reader["id_dep"] + reader.GetString("dep_name"));  }
           
        }
        //function load position to Combobox
        private void loadPos()
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("SELECT id_pos,pos FROM db_posit;", databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            
            while (reader.Read())
            {
                //comboBox2.Items.Add(reader.GetInt32("id_pos"));
                //comboBox2.Items.Add(reader.GetString("pos"));
                comboBox2.Items.Add(reader["id_pos"]+reader.GetString("pos"));
            }
            
            
        }//-----------------------------------------------------------------------------------
        //Funtion load branch to comboBox
        private void loadBranch()
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("SELECT id_branc,branc FROM db_edu;", databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();

            while (reader.Read())
            {
               
                branch.Items.Add(reader["id_branc"] + reader.GetString("branc"));
            }


        }
        //-------------------------------------------------------------------------------
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {  
           
        }

        //save Function
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""||textBox2.Text==""||textBox4.Text==""||comboBox1.Text==""||comboBox2.Text==""||textBox5.Text==""|| comboBox_dep.Text==""||branch.Text=="")
            {
                MessageBox.Show("ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ!");
            }
            else
            {
                string query = "INSERT INTO `db_emp` (`id_emp`, `emp_name`, `emp_gender`, `emp_salary`, `id_pos`, `emp_age`,`emp_tel`,`emp_email`,`id_dep`,`id_branc`) VALUES (NULL, '" + textBox1.Text + "', '" + comboBox1.Text + "', '" + textBox4.Text +",00" +"','" + comboBox2.Text + "','" + textBox2.Text + "','"+textBox5.Text+"','"+textBox6.Text+"','"+comboBox_dep.Text+"','"+branch.Text+"')";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    MessageBox.Show("Create succesfully");
                    databaseConnection.Close();
                    textBox1.Text = "";//name
                    comboBox1.Text = "";//gender
                    textBox2.Text = "";//age
                    comboBox_dep.Text = "";//dep
                    comboBox2.Text = "";//pos
                    textBox4.Text = "";//salary
                    textBox5.Text = "";//tel
                    textBox6.Text = "";//email
                    branch.Text = "";//branch
                    //Console.WriteLine(query);
                }
                catch (Exception ex)
                {
                    // Show any error message.
                    MessageBox.Show(ex.Message);
                    
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        //click for Show on From and for update,delete
        private void listView1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM db_emp emp INNER JOIN db_posit pos INNER JOIN db_depart dep INNER JOIN db_edu brn ON pos.id_pos=emp.id_pos AND dep.id_dep=emp.id_dep AND brn.id_branc=emp.id_branc ORDER BY emp.id_emp DESC;", databaseConnection);
            databaseConnection.Open();
            MySqlDataReader reader = commandDatabase.ExecuteReader();
            ListViewItem listViewItem = new ListViewItem();
            //click and show in text Box
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView1.SelectedItems[0];
                //id Emp
                    ID.Text = item.SubItems[1].Text;
                //name
                    textBox1.Text = item.SubItems[2].Text;
                //Gender
                comboBox1.Text = item.SubItems[3].Text;
                //age
                textBox2.Text = item.SubItems[4].Text; 
                //School
                branch.Text = item.SubItems[5].Text;
                //department
                comboBox_dep.Text = item.SubItems[6].Text;
                //Position
                    comboBox2.Text= item.SubItems[7].Text;
                //Salary
                textBox4.Text = item.SubItems[8].Text;
                //tel
                textBox5.Text= item.SubItems[1].Text;
                //email
                textBox6.Text = item.SubItems[9].Text;
                

        };

            //set for don't click Save Btn
            save.Enabled = false;
            //-----------------------------------------------
            update.Enabled = true;
            delete.Enabled = true;

        }

        private void update_Click(object sender, EventArgs e)
        {
            string cmdUpdate= "UPDATE db_emp SET id_emp='" + ID.Text + "',emp_name='" + textBox1.Text + "',emp_gender='" + comboBox1.Text + "',emp_salary='" + textBox4.Text + "',id_pos='" + comboBox2.Text + "', emp_age='" + textBox2.Text + "',emp_tel='" + textBox5.Text + "',emp_email='" + textBox6.Text + "',id_dep='"+comboBox_dep.Text+"',id_branc='"+branch.Text+"' WHERE id_emp='"+ID.Text+"' ";
            
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(cmdUpdate, databaseConnection);
            commandDatabase.Parameters.AddWithValue("id_emp",ID);
            commandDatabase.Parameters.AddWithValue("emp_name",textBox1);
            commandDatabase.Parameters.AddWithValue("emp_gender",comboBox1);
            commandDatabase.Parameters.AddWithValue("emp_age",textBox2);
            commandDatabase.Parameters.AddWithValue("id_dep", comboBox_dep);
            commandDatabase.Parameters.AddWithValue("ip_pos", comboBox2);
            commandDatabase.Parameters.AddWithValue("emp_salary", textBox4);
            commandDatabase.Parameters.AddWithValue("emp_tel", textBox5);
            commandDatabase.Parameters.AddWithValue("emp_email", textBox6);
            commandDatabase.Parameters.AddWithValue("id_branc", branch);
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                MessageBox.Show("Update succesfully");
                databaseConnection.Close();
                textBox1.Text = "";//name
                comboBox1.Text = "";//gender
                textBox2.Text = "";//age
                comboBox_dep.Text = "";//dep
                comboBox2.Text = "";//pos
                textBox4.Text = "";//salary
                textBox5.Text = "";//tel
                textBox6.Text = "";//email
                branch.Text = "";//branch

            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
                Console.WriteLine(cmdUpdate);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure Delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand("DELETE FROM db_emp WHERE id_emp=" + ID.Text, databaseConnection);
                try
                {

                    databaseConnection.Open();
                    commandDatabase.ExecuteNonQuery();
                    MessageBox.Show("Delete succesfully");
                    databaseConnection.Close();
                    textBox1.Text = "";//name
                    comboBox1.Text = "";//gender
                    textBox2.Text = "";//age
                    comboBox_dep.Text = "";//dep
                    comboBox2.Text = "";//pos
                    textBox4.Text = "";//salary
                    textBox5.Text = "";//tel
                    textBox6.Text = "";//email
                    branch.Text = "";//branch
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

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            this.Refresh();
            employee Check = new employee();
            Check.Show();
            Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //if (!textBox4.Text.EndsWith(",") && textBox4.Text.Length > 3){
            //    textBox4.Text+= ",";
            //    textBox4.SelectionStart=textBox4.Text.Length;
           // }
        }
    }

    internal class comboBox
    {
    }
}
