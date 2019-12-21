using FVSystem.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Repository
{
    public class NotasRepository
    {

        private IConfiguration configuration;
        private string connectionString;

        public NotasRepository(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DesgloseNotas DesgloseNotasPorModulo(string moduloId, string estudianteId)
        {
            var desglose = new DesgloseNotas();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT dn.*, nm.Nota " +
                                            "FROM DesgloseDeNotas dn " +
                                             "INNER JOIN NotasModulos nm " +
                                               "ON dn.IdNota = nm.iD " +
                                                    "WHERE nm.IdModulo = @IdModulo " +
                                                    "AND nm.IdEstudiante = @IdEstudiante";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@IdModulo", moduloId);
                    command.Parameters.AddWithValue("@IdEstudiante", estudianteId);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        desglose = new DesgloseNotas()
                        {
                            Asistencia = Convert.ToInt32(reader["Asistencia"]),
                            AsistenciaTotal = Convert.ToInt32(reader["AsistenciaTotal"]),
                            Cotidiano = Convert.ToInt32(reader["Cotidiano"]),
                            CotidianoTotal = Convert.ToInt32(reader["CotidianoTotal"]),
                            Proyecto1 = Convert.ToInt32(reader["Proyecto1"]),
                            Proyecto1Total = Convert.ToInt32(reader["Proyecto1Total"]),
                            Proyecto2 = Convert.ToInt32(reader["Proyecto2"]),
                            Proyecto2Total = Convert.ToInt32(reader["Proyecto2Total"]),
                            ProyectoFinal= Convert.ToInt32(reader["ProyectoFinal"]),
                            ProyectoFinalTotal = Convert.ToInt32(reader["ProyectoFinalTotal"]),

                        };
                    }
                }
            }

            return desglose;
        }
        public bool ActualizarNotasEstudiante(DesgloseNotas desgloseDeNotas)
        {
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (var command = new MySqlCommand(
                                                        "UPDATE DesgloseDeNotas " +
                                                        "SET Asistencia = @Asistencia, Cotidiano = @Cotidiano, Proyecto1 = @Proyecto1, Proyecto2 = @Proyecto2, ProyectoFinal= @ProyectoFinal  " +
                                                        "WHERE IdNota=@IdNota ", connect))
                {

                    command.Parameters.AddWithValue("@IdNota", desgloseDeNotas.IdNota);
                    command.Parameters.AddWithValue("@Asistencia", desgloseDeNotas.Asistencia);
                    command.Parameters.AddWithValue("@Cotidiano", desgloseDeNotas.Cotidiano);
                    command.Parameters.AddWithValue("@Proyecto1", desgloseDeNotas.Proyecto1);
                    command.Parameters.AddWithValue("@Proyecto2", desgloseDeNotas.Proyecto2);
                    command.Parameters.AddWithValue("@ProyectoFinal", desgloseDeNotas.ProyectoFinal);
   
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
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
