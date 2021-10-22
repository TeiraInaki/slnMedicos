using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace Datos
{
    internal static class AdminDB
    {
        internal static SqlConnection ConectarBD()
        {
            string Key = Datos.Properties.Settings.Default.KeyDBMedicos;
            SqlConnection connection = new SqlConnection(Key);
            connection.Open();

            return connection;

        }
    }
}
