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
    public partial class frmLibros : Form
    {
        string CadCon = "Server=127.0.0.1; User id=root; password=VectorLord; port=3306; database=trabajo";
        string titulo, autor, año, pruebagenero;
        int genero;
        public frmLibros()
        {
            InitializeComponent();
        }

        private void frmLibros_Load(object sender, EventArgs e)
        {
            ActualizarDatos();
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            txtTitulo.Enabled = false;
            txtAutor.Enabled = false;
            txtGenero.Enabled = false;
            txtAño.Enabled = false;
            txtID.Enabled = false;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        public DataSet Listar()
        {
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "select * from libros order by id;";
            MySqlDataAdapter adaptador;
            DataSet conjunto = new DataSet();
            adaptador = new MySqlDataAdapter(query, micon);
            adaptador.Fill(conjunto, "tbl");
            micon.Close();
            return conjunto;
        }

        public void ActualizarDatos()
        {
            dgvLibros.DataSource = Listar().Tables["tbl"];
        }

        public DataSet Generos()
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

        public void MostrarGeneros()
        {
            dgvLibros.DataSource = Generos().Tables["tbl"];
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGeneros();
            lblinfo.ForeColor = Color.White;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            txtID.Text = "";
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtGenero.Text = "";
            txtAño.Text = "";
            txtTitulo.Enabled = true;
            txtAutor.Enabled = true;
            txtGenero.Enabled = true;
            txtAño.Enabled = true;
            btnCerrar.Text = "Cancelar";
            btnGuardar.Text = "Guardar";
            txtTitulo.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if(btnCerrar.Text == "Cancelar")
            {
                txtID.Text = "";
                txtTitulo.Text = "";
                txtAutor.Text = "";
                txtGenero.Text = "";
                txtAño.Text = "";
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                txtTitulo.Enabled = false;
                txtAutor.Enabled = false;
                txtGenero.Enabled = false;
                txtAño.Enabled = false;
                btnCerrar.Text = "Cerrar";
                btnGuardar.Text = "Guardar";
                lblinfo.ForeColor = Color.Black;
                ActualizarDatos();
            }
            else
            {
                Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Modificar")
            {
                txtTitulo.Enabled = true;
                txtAutor.Enabled = true;
                txtGenero.Enabled = true;
                txtAño.Enabled = true;
                btnGuardar.Text = "Guardar";
                btnCerrar.Text = "Cancelar";
                lblinfo.ForeColor = Color.White;
                MostrarGeneros();
                dgvLibros.Enabled = false;
                btnEliminar.Enabled = false;
            }
            else
            {
                bool res;
                titulo = txtTitulo.Text;
                autor = txtAutor.Text;
                pruebagenero = txtGenero.Text;
                año = txtAño.Text;
                res = ValidarDatos();
                if (res == false)
                {
                    return;
                }
                genero = int.Parse(txtGenero.Text);
                if (txtID.Text == "")
                {
                    Crear();
                }
                else
                {
                    Editar();
                }
                dgvLibros.Enabled = true;
            }
            
        }

        public void Crear()
        {
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            try
            {
                string query = "insert into libros(titulo, autor, genero_id, año) values ('" + titulo + "', '" + autor + "', " + genero + ", '" + año + "');";
                MySqlCommand com = new MySqlCommand(query, micon);
                com.ExecuteNonQuery();
                micon.Close();
                MessageBox.Show("Registro creado!");
                ActualizarDatos();
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCerrar.Text = "Cerrar";
                txtID.Text = "";
                txtTitulo.Text = "";
                txtAutor.Text = "";
                txtGenero.Text = "";
                txtAño.Text = "";
                txtTitulo.Enabled = false;
                txtAutor.Enabled = false;
                txtGenero.Enabled = false;
                txtAño.Enabled = false;
                lblinfo.ForeColor = Color.Black;
            } catch (MySqlException)
            {
                MessageBox.Show("Titulo duplicado");
                micon.Close();
            }
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
                btnNuevo.Enabled = true;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = false;
                txtTitulo.Enabled = false;
                txtAutor.Enabled = false;
                txtGenero.Enabled = false;
                txtAño.Enabled = false;
                txtID.Text = "";
                txtTitulo.Text = "";
                txtAutor.Text = "";
                txtGenero.Text = "";
                txtAño.Text = "";
                btnCerrar.Text = "Cerrar";
                dgvLibros.Enabled = true;
                lblinfo.ForeColor = Color.Black;
            }
        }

        public void Eliminar()
        {
            int id = int.Parse(txtID.Text);
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "delete from libros where id=" + id + ";";
            MySqlCommand com = new MySqlCommand(query, micon);
            com.ExecuteNonQuery();
            micon.Close();
            MessageBox.Show("Registro Eliminado!");
            lblinfo.ForeColor = Color.Black;
        }

        private void dgvLibros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvLibros.CurrentRow.Cells["id"].Value.ToString();
            txtTitulo.Text = dgvLibros.CurrentRow.Cells["titulo"].Value.ToString();
            txtAutor.Text = dgvLibros.CurrentRow.Cells["autor"].Value.ToString();
            txtGenero.Text = dgvLibros.CurrentRow.Cells["genero_id"].Value.ToString();
            txtAño.Text = dgvLibros.CurrentRow.Cells["año"].Value.ToString();
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = true;
            btnGuardar.Text = "Modificar";
        }

        public void Editar()
        {
            int id = int.Parse(txtID.Text);
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "update libros set titulo = '" + titulo + "', autor = '" + autor + "', genero_id = " + genero + ", año = '" + año + "', actualizado_el = now() where id = " + id + ";";
            MySqlCommand com = new MySqlCommand(query, micon);
            com.ExecuteNonQuery();
            micon.Close();
            MessageBox.Show("Registro actualizado!");
            ActualizarDatos();
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCerrar.Text = "Cerrar";
            txtID.Text = "";
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtGenero.Text = "";
            txtAño.Text = "";
            txtTitulo.Enabled = false;
            txtAutor.Enabled = false;
            txtGenero.Enabled = false;
            txtAño.Enabled = false;
            lblinfo.ForeColor = Color.Black;
        }

        public bool ValidarDatos()
        {
            bool res = true;
            if (titulo == String.Empty)
            {
                res = false;
                MessageBox.Show("El título es obligatorio");
            }
            if (autor == String.Empty)
            {
                res = false;
                MessageBox.Show("El autor es obligatorio");
            }
            if (pruebagenero == String.Empty)
            {
                res = false;
                MessageBox.Show("El género es obligatorio");
            }
            if (año == String.Empty)
            {
                res = false;
                MessageBox.Show("El año es obligatorio");
            }
            return res;
        }
    }
}
