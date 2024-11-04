using MySql.Data.MySqlClient;

namespace Comandos
{
    public class comandos
    {
        string CadCon = "Server=127.0.0.1; User id=root; password=VectorLord; port=3306; database=trabajo";
        public void conexion()
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
            micon.Close();
        }
    }
}