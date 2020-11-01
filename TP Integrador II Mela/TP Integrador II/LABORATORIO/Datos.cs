using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LABORATORIO
{
    class Datos
    {
        SqlConnection cnn;
        SqlCommand cmd;
        string cadenaConexion;
        public Datos(string cadenaConexion)
        {
            this.cnn = new SqlConnection();
            this.cmd = new SqlCommand();
            this.cadenaConexion = cadenaConexion;
        }
        public void conectar()
        {
            cnn.ConnectionString = cadenaConexion;
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
        }
        public void desconectar()
        {
            cnn.Close();
        }
        public DataTable consultarTabla(string consultaSQL)
        {
            conectar();
            DataTable dt = new DataTable();
            cmd.CommandText = consultaSQL;
            dt.Load(cmd.ExecuteReader());
            desconectar();
            return dt;
        }
    }
}
