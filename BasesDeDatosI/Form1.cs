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
    public partial class Form1 : Form
    {
        string CadCon = "Server=127.0.0.1; User id=root; password=VectorLord; port=3306; database=trabajo";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGeneros_Click(object sender, EventArgs e)
        {
            frmGeneros gen = new frmGeneros();
            gen.Show();
        }

        private void conexion()
        {
            MySqlConnection micon = new MySqlConnection(CadCon);
            try
            {
                micon.Open();
                MessageBox.Show("Conectado");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                micon.Close();
            }
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            frmLibros lib = new frmLibros();
            lib.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios us = new frmUsuarios();
            us.Show();
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            frmPrestamos pres = new frmPrestamos();
            pres.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
