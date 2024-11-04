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
    public partial class frmUsuarios : Form
    {
        string CadCon = "Server=127.0.0.1; User id=root; password=VectorLord; port=3306; database=trabajo";
        string Nombre, Apellido, DNI, Telefono, Email;

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (btnCerrar.Text == "Cancelar")
            {
                txtID.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDNI.Text = "";
                txtTelefono.Text = "";
                txtEmail.Text = "";
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnPrestamo.Enabled = false;
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtDNI.Enabled = false;
                txtTelefono.Enabled = false;
                txtEmail.Enabled = false;
                btnCerrar.Text = "Cerrar";
                btnGuardar.Text = "Guardar";
                ActualizarDatos();
                dgvUsuarios.Enabled = true;
            }
            else
            {
                Close();
            }
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvUsuarios.CurrentRow.Cells["id"].Value.ToString();
            txtNombre.Text = dgvUsuarios.CurrentRow.Cells["nombre"].Value.ToString();
            txtApellido.Text = dgvUsuarios.CurrentRow.Cells["apellido"].Value.ToString();
            txtDNI.Text = dgvUsuarios.CurrentRow.Cells["dni"].Value.ToString();
            txtTelefono.Text = dgvUsuarios.CurrentRow.Cells["telefono"].Value.ToString();
            txtEmail.Text = dgvUsuarios.CurrentRow.Cells["email"].Value.ToString();
            btnGuardar.Enabled = true;
            btnPrestamo.Enabled = true;
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
                btnNuevo.Enabled = true;
                btnPrestamo.Enabled = false;
                btnGuardar.Enabled = false;
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtDNI.Enabled = false;
                txtTelefono.Enabled = false;
                txtEmail.Enabled = false;
                txtID.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDNI.Text = "";
                txtTelefono.Text = "";
                txtEmail.Text = "";
                btnCerrar.Text = "Cerrar";
                dgvUsuarios.Enabled = true;
            }
        }

        public void Eliminar()
        {
            int id = int.Parse(txtID.Text);
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "update usuarios set estado = 0 where id=" + id + ";";
            MySqlCommand com = new MySqlCommand(query, micon);
            com.ExecuteNonQuery();
            micon.Close();
            MessageBox.Show("Prestamo realizado!");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Modificar")
            {
                txtNombre.Enabled = true;
                txtApellido.Enabled = true;
                txtDNI.Enabled = true;
                txtTelefono.Enabled = true;
                txtEmail.Enabled = true;
                btnGuardar.Text = "Guardar";
                btnCerrar.Text = "Cancelar";
                dgvUsuarios.Enabled = false;
                btnPrestamo.Enabled = false;
            }
            else
            {
                bool res;
                Nombre = txtNombre.Text;
                Apellido = txtApellido.Text;
                DNI = txtDNI.Text;
                Telefono = txtTelefono.Text;
                Email = txtEmail.Text;
                res = ValidarDatos();
                if (res == false)
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
                dgvUsuarios.Enabled = true;
            }
        }

        public void Crear()
        {
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            try
            {
                string query = "insert into usuarios(nombre, apellido, dni, telefono, email) values ('" + Nombre + "', '" + Apellido + "', '" + DNI + "', '" + Telefono + "', '" + Email + "');";
                MySqlCommand com = new MySqlCommand(query, micon);
                com.ExecuteNonQuery();
                micon.Close();
                MessageBox.Show("Registro creado!");
                ActualizarDatos();
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnPrestamo.Enabled = false;
                btnCerrar.Text = "Cerrar";
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtDNI.Enabled = false;
                txtTelefono.Enabled = false;
                txtEmail.Enabled = false;
                txtID.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDNI.Text = "";
                txtTelefono.Text = "";
                txtEmail.Text = "";
            } catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
                micon.Close();
            }
        }

        public void Editar()
        {
            int id = int.Parse(txtID.Text);
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "update usuarios set nombre = '" + Nombre + "', apellido = '" + Apellido + "', dni = '" + DNI + "', telefono = '" + Telefono + "', email = '" + Email + "', actualizado_el = now() where id = " + id + ";";
            MySqlCommand com = new MySqlCommand(query, micon);
            com.ExecuteNonQuery();
            micon.Close();
            MessageBox.Show("Registro actualizado!");
            ActualizarDatos();
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnPrestamo.Enabled = false;
            btnCerrar.Text = "Cerrar";
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDNI.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtID.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
        }

        public bool ValidarDatos()
        {
            bool res = true;
            if (Nombre == String.Empty)
            {
                res = false;
                MessageBox.Show("Ingrese un nombre");
            }
            if (Apellido == String.Empty)
            {
                res = false;
                MessageBox.Show("ingrese un apellido");
            }
            if (DNI == String.Empty)
            {
                res = false;
                MessageBox.Show("Ingrese un DNI");
            }
            if (Telefono == String.Empty)
            {
                res = false;
                MessageBox.Show("Ingrese un telefono");
            }
            if (Email == String.Empty)
            {
                res = false;
                MessageBox.Show("Ingrese un email");
            }
            return res;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnPrestamo.Enabled = false;
            txtID.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDNI.Enabled = true;
            txtTelefono.Enabled = true;
            txtEmail.Enabled = true;
            btnCerrar.Text = "Cancelar";
            btnGuardar.Text = "Guardar";
            txtNombre.Focus();
        }

        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            ActualizarDatos();
            txtID.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDNI.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            btnGuardar.Enabled = false;
            btnPrestamo.Enabled = false;
        }

        public void ActualizarDatos()
        {
            dgvUsuarios.DataSource = Listar().Tables["tbl"];
        }

        public DataSet Listar()
        {
            MySqlConnection micon = new MySqlConnection(CadCon);
            micon.Open();
            string query = "select * from usuarios order by id;";
            MySqlDataAdapter adaptador;
            DataSet conjunto = new DataSet();
            adaptador = new MySqlDataAdapter(query, micon);
            adaptador.Fill(conjunto, "tbl");
            micon.Close();
            return conjunto;
        }
    }
}
