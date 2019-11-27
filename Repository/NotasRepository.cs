using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Repository
{
    public class NotasRepository
    {
        public List<NotaModulo> ObtenerNotaPorModulo(int Modulo)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Curso> notas = new List<Curso>();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlConnection command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT dn.*, nm.Nota " +
                                         "FROM DesgloseDeNotas dn " +
                                        "INNER JOIN NotasModulo nm " +
                                        "ON dn.IdNotas = nm.IdNotas " +
                                        "WHERE IdModulo = @Id";

                    command.Parameters.AddWithValue("@Id", modulo);

                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        notas.Add(new nota()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"])

                        });
                    }
                }
            }
            return notas;
        }
    }
}
