using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CRUD_App_csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=students;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public int studentId;
        bool valideEmail = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllStudents();
        }

        private void GetAllStudents()
        {

            SqlCommand query = new SqlCommand("SELECT * From apprenant", conn);
            DataTable dt = new DataTable();

            conn.Open();
            SqlDataReader sdr = query.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            studentDataGridView.DataSource = dt;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid() && isValidEmail(email.Text))
            {
                SqlCommand query = new SqlCommand("INSERT INTO apprenant VALUES (@nom,@prenom,@email,@telephone,@adresse,@pays,@ville,@specialite)", conn);
                query.CommandType = CommandType.Text;

                query.Parameters.AddWithValue("@nom", nom.Text);
                query.Parameters.AddWithValue("@prenom",prenom.Text);
                query.Parameters.AddWithValue("@email", email.Text);
                query.Parameters.AddWithValue("@telephone", int.Parse(telephone.Text)) ;
                query.Parameters.AddWithValue("@adresse", adresse.Text);
                query.Parameters.AddWithValue("@pays",comboBox_pays.Text);
                query.Parameters.AddWithValue("@ville", comboBox_ville.Text);
                query.Parameters.AddWithValue("@specialite", comboBox_specialite.Text);


                conn.Open();
                query.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Nouveau aprenant a ete bien ajouter", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearAllFields();

                GetAllStudents();

            }
        }

        private void clearAllFields()
        {
            nom.Clear();
            prenom.Clear();
            email.Clear();
            telephone.Clear();
            adresse.Clear();
            comboBox_pays.ResetText();
            comboBox_ville.ResetText();
            comboBox_specialite.ResetText();
            nom.Focus();

        }

       

        private bool isValid()
        {
            if (nom.Text == string.Empty || prenom.Text==string.Empty ||email.Text==string.Empty || telephone.Text==string.Empty ||adresse.Text==string.Empty || comboBox_pays.Text==string.Empty ||comboBox_ville.Text==string.Empty ||  comboBox_specialite.Text==string.Empty)
            {
                MessageBox.Show("Veuillez Remplire tout les champs avant l\'insertion  ","Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;

        }

        private void comboBox_ville_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox_pays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_pays.Text == "Maroc")
            {
                comboBox_ville.Items.Clear();
                comboBox_ville.Items.Add("Safi");
                comboBox_ville.Items.Add("Agadir");
                comboBox_ville.Items.Add("Esaouira");
                comboBox_ville.Items.Add("Titouan");
            }
            else if (comboBox_pays.Text == "France")
            {
                comboBox_ville.Items.Clear();
                comboBox_ville.Items.Add("Paris");
                comboBox_ville.Items.Add("Lyon");
                comboBox_ville.Items.Add("Toulouse");
                comboBox_ville.Items.Add("Tours");
            }
            else if (comboBox_pays.Text == "Allemand")
            {
                comboBox_ville.Items.Clear();
                comboBox_ville.Items.Add("Bonn");
                comboBox_ville.Items.Add("Berlin");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearAllFields();
        }

        private void studentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
             

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (studentId > 0)
            {
                SqlCommand query = new SqlCommand("UPDATE apprenant SET nom=@nom,prenom=@prenom,email=@email,telephone=@telephone,adrese=@adresse,pays=@pays,ville=@ville,choixSpecialite=@specialite WHERE id=@ID", conn);
                query.CommandType = CommandType.Text;

                query.Parameters.AddWithValue("@nom", nom.Text);
                query.Parameters.AddWithValue("@prenom", prenom.Text);
                query.Parameters.AddWithValue("@email", email.Text);
                query.Parameters.AddWithValue("@telephone", telephone.Text);
                query.Parameters.AddWithValue("@adresse", adresse.Text);
                query.Parameters.AddWithValue("@pays", comboBox_pays.Text);
                query.Parameters.AddWithValue("@ville", comboBox_ville.Text);
                query.Parameters.AddWithValue("@specialite", comboBox_specialite.Text);
                query.Parameters.AddWithValue("@ID",this.studentId);


                conn.Open();
                query.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Mise a jour  a ete bien fait", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearAllFields();

                GetAllStudents();
            }
            else
            {
                MessageBox.Show("S\'il vous plait selectionnez un champs ", "Selectionne?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void studentDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void studentDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            studentId = Convert.ToInt32(studentDataGridView.SelectedRows[0].Cells[0].Value);
            nom.Text = studentDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            prenom.Text = studentDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            email.Text = studentDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            telephone.Text = studentDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            adresse.Text = studentDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            comboBox_pays.Text = studentDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            comboBox_ville.Text = studentDataGridView.SelectedRows[0].Cells[7].Value.ToString();
            comboBox_specialite.Text = studentDataGridView.SelectedRows[0].Cells[8].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (studentId > 0)
            {
                SqlCommand query = new SqlCommand("DELETE FROM apprenant WHERE id=@ID", conn);
                query.CommandType = CommandType.Text;

                query.Parameters.AddWithValue("@ID", this.studentId);


                conn.Open();
                query.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("un champs a ete bien supprimer", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clearAllFields();

                GetAllStudents();
            }
            else
            {
                MessageBox.Show("Selectionnez un  champs s\'il vous plait", "DELETE?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void telephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(this.telephone, "just numbers please");
                
            }
            else
            {
                errorProvider1.Clear();
            }
        }


        private void email_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.isValidEmail(nom.Text);
        }

        private bool isValidEmail(string email)
        {
            Regex check = new Regex(@"^\w+[\w-.]+@\w{5}.[a-z]{2,3}$");

            valideEmail = check.IsMatch(email);

            if (valideEmail == true)
            {
                errorProvider1.Clear();
                return true;
            }
            else
            {
                errorProvider1.SetError(this.email, "please provide valid email address");
                return false;
            }
        }

        private void telephone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
