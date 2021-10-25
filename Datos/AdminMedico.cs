using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AdminMedico
    {
        public static List<Medico> Listar()
        {
            string query = "SELECT Id,Nombre,Apellido,NroMatricula,EspecialidadId FROM dbo.Medico";

            SqlCommand command = new SqlCommand(query, AdminDB.ConectarBD());

            SqlDataReader reader;
            reader = command.ExecuteReader();

            List<Medico> medicos = new List<Medico>();

            while (reader.Read())
            {
                medicos.Add(new Medico((int)reader["Id"], (string)reader["Nombre"], (string)reader["Apellido"], (int)reader["NroMatricula"], (int)reader["EspecialidadId"]));
            }

            reader.Close();
            AdminDB.ConectarBD().Close();

            return medicos;
        }

        public static DataTable ListarOffline()
        {
            string query = "SELECT Id,Nombre,Apellido,NroMatricula,EspecialidadId FROM dbo.Medico";

            SqlDataAdapter adapter = new SqlDataAdapter(query, AdminDB.ConectarBD());

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Medicos");

            return ds.Tables["Medicos"];
        }
        
        public static DataTable Listar(int EspecialidadID)
        {
            string query = "SELECT Id,Nombre,Apellido,NroMatricula,EspecialidadId FROM dbo.Medico WHERE EspecialidadId = @EspecialidadId";

            SqlDataAdapter adapter = new SqlDataAdapter(query, AdminDB.ConectarBD());

            adapter.SelectCommand.Parameters.Add("@EspecialidadId", SqlDbType.Int).Value = EspecialidadID;

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Medicos");

            return ds.Tables["Medicos"];
        }

        public static DataTable TraerUno(int Id)
        {
            string query = "SELECT Id,Nombre,Apellido,NroMatricula,EspecialidadId FROM dbo.Medico WHERE Id = @Id";

            SqlDataAdapter adapter = new SqlDataAdapter(query, AdminDB.ConectarBD());

            adapter.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            DataSet ds = new DataSet();

            adapter.Fill(ds, "Medico");

            return ds.Tables["Medico"];
        }

        public static int Insertar(Medico medico)
        {
            string query = "INSERT dbo.Medico(Nombre,Apellido,NroMatricula,EspecialidadId) VALUES (@Nombre,@Apellido,@NroMatricula,@EspecialidadId)";

            SqlCommand command = new SqlCommand(query, AdminDB.ConectarBD());

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = medico.Nombre;
            command.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = medico.Apellido;
            command.Parameters.Add("@NroMatricula", SqlDbType.Int).Value = medico.NroMatricula;
            command.Parameters.Add("@EspecialidadId", SqlDbType.Int).Value = medico.EspecialidadId;

            int filasAfectadas = command.ExecuteNonQuery();

            return filasAfectadas;
        }

        public static int Eliminar(int Id)
        {
            string query = "DELETE FROM dbo.Medico WHERE Id = @Id";

            SqlCommand command = new SqlCommand(query, AdminDB.ConectarBD());

            command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            int filasAfectadas = command.ExecuteNonQuery();

            AdminDB.ConectarBD().Close();

            return filasAfectadas;
        }

        public static int Modificar(Medico medico)
        {
            string query = "UPDATE dbo.Medico SET Nombre=@Nombre,Apellido=@Apellido,NroMatricula=@NroMatricula,EspecialidadId=@EspecialidadId WHERE Id=@Id";

            SqlCommand command = new SqlCommand(query, AdminDB.ConectarBD());

            command.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = medico.Nombre;
            command.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = medico.Apellido;
            command.Parameters.Add("@NroMatricula", SqlDbType.Int).Value = medico.NroMatricula;
            command.Parameters.Add("@EspecialidadId", SqlDbType.Int).Value = medico.EspecialidadId;
            command.Parameters.Add("@Id", SqlDbType.Int).Value = medico.Id;

            int filasAfectadas = command.ExecuteNonQuery();
            AdminDB.ConectarBD().Close();
            return filasAfectadas;
        }

    }
}
