using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;

namespace Datos
{
    public class AdminEspecialidad
    {
        public static DataTable Listar()
        {
            string query = "SELECT Id,Nombre FROM dbo.Especialidad";

            SqlDataAdapter adapter = new SqlDataAdapter(query, AdminDB.ConectarBD());

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Especialidades");

            return ds.Tables["Especialidades"];
        }

        public static DataTable TraerUno(int Id)
        {
            string query = "SELECT Id,Nombre FROM dbo.Especialidad WHERE Id = @Id";

            SqlDataAdapter adapter = new SqlDataAdapter(query, AdminDB.ConectarBD());

            adapter.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Especialidad");

            return ds.Tables["Especialidad"];

        }

        public static int Crear(string especialidad)
        {
            string query = "INSERT dbo.Especialidad (Nombre) VALUES (@Nombre)";

            SqlCommand command = new SqlCommand(query, AdminDB.ConectarBD());

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = especialidad;

            int filasAfectadas = command.ExecuteNonQuery();

            return filasAfectadas;
        }
    }
}
