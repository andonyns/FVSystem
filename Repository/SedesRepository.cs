using FVSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace FVSystem.Repository
{
    public class SedesRepository
    {
        public List<Sede> GetSedes()
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Sede> sedes = new List<Sede>();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Sedes";
                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sedes.Add(new Sede()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Direccion = Convert.ToString(reader["Direccion"])
                        });
                    }
                }
            }
            return sedes;
        }

        public bool InsertarSede(Sede sede)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();


                using (SQLiteCommand command = new SQLiteCommand(
                                                        "INSERT INTO Sedes(Id,Nombre,Direccion) " +
                                                        "VALUES(@Id,@Nombre,@Direccion)", connect))
                {
                    command.Parameters.AddWithValue("@Id", sede.Id);
                    command.Parameters.AddWithValue("@Nombre", sede.Nombre);
                    command.Parameters.AddWithValue("@Direccion", sede.Direccion);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }
    }
}