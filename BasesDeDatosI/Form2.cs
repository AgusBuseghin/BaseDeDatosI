using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasesDeDatosI
{
    public partial class frmGeneros : Form
    {
        string CadCon = "Server=127.0.0.1; User id=root; password=VectorLord; port=3306; database=trabajo";
        string genero = "";
        public frmGeneros()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (btnCerrar.Text == "Cancelar")
            {
                txtID.Text = "";
                txtGenero.Text = "";
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCerrar.Text = "Cerrar";
                btnGuardar.Text = "Guardar";
                txtGenero.Enabled = false;
                ActualizarDatos();
                dgvGeneros.Enabled = true;
            }
            else
            {
                Close();
            }
        }

        public DataSet Listar()
        {
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "Select * from generos order by id;";
            MySqlDataAdapter adaptador;
            DataSet conjunto = new DataSet();
            adaptador = new MySqlDataAdapter(query, micon);
            adaptador.Fill(conjunto, "tbl");
            micon.Close();
            return conjunto;
        }

        private void frmGeneros_Load(object sender, EventArgs e)
        {
            ActualizarDatos();
            txtID.Enabled = false;
            txtGenero.Enabled = false;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void frmGeneros_Activated(object sender, EventArgs e)
        {
            ActualizarDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtGenero.Text = "";
            txtGenero.Enabled = true;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
            btnEliminar.Enabled = false;
            btnCerrar.Text = "Cancelar";
            btnGuardar.Text = "Guardar";
        }

        public bool ValidarDatos()
        {
            bool res = true;
            if (genero == String.Empty)
            {
                res = false;
                MessageBox.Show("El genero es obligatorio");
            }
            return res;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Modificar")
            {
                txtGenero.Enabled = true;
                btnGuardar.Text = "Guardar";
                btnCerrar.Text = "Cancelar";
                btnEliminar.Enabled = false;
                dgvGeneros.Enabled = false;
            }
            else
            {
                bool resultado;
                genero = txtGenero.Text;
                resultado = ValidarDatos();
                if (resultado == false)
                {
                    return;
                }
                if (txtID.Text == "")
                {
                    Crear();
                }
                else
                {
                    Editar();
                }
                dgvGeneros.Enabled=true;
            }
        }

        public void Crear()
        {
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            try
            {
            string query = "insert into generos(genero) values ('" + genero + "');";
            MySqlCommand com = new MySqlCommand(query, micon);
            com.ExecuteNonQuery();
            micon.Close();
            MessageBox.Show("Registro creado!");
            ActualizarDatos();
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = true;
            btnEliminar.Enabled = false;
            txtID.Text = "";
            txtGenero.Text = "";
            txtGenero.Enabled = false;
            btnCerrar.Text = "Cerrar";
            }
            catch (MySqlException)
            {
                MessageBox.Show("Registro duplicado");
                micon.Close();
            }
        }

        public void Editar()
        {
            int id = int.Parse(txtID.Text);
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "update generos set genero='" + genero + "' where id = " + id + ";";
            MySqlCommand com = new MySqlCommand(query, micon);
            com.ExecuteNonQuery();
            micon.Close();
            MessageBox.Show("Registro actualizado!");
            ActualizarDatos();
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = true;
            btnEliminar.Enabled = false;
            txtID.Text = "";
            txtGenero.Text = "";
            txtGenero.Enabled = false;
            btnCerrar.Text = "Cerrar";
        }

        private void dgvGeneros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvGeneros.CurrentRow.Cells["id"].Value.ToString();
            txtGenero.Text = dgvGeneros.CurrentRow.Cells["genero"].Value.ToString();
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = true;
            btnGuardar.Text = "Modificar";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                return;
            }
            if (MessageBox.Show("¿Deseas eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = int.Parse(txtID.Text);
                Eliminar();
                ActualizarDatos();
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                txtGenero.Enabled = false;
                txtID.Text = "";
                txtGenero.Text = "";
                btnCerrar.Text = "Cerrar";
            }
        }

        public void ActualizarDatos()
        {
            dgvGeneros.DataSource = Listar().Tables["tbl"];
        }

        public void Eliminar()
        {
            int id = int.Parse(txtID.Text);
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "delete from generos where id=" + id + ";";
            MySqlCommand com = new MySqlCommand(query, micon);
            com.ExecuteNonQuery();
            micon.Close();
            MessageBox.Show("Registro Eliminado!");
        }
    }
}
