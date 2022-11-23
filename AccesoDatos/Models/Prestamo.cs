using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos.Models
{
    public class Prestamo
    {
        public Libro libro;
        public int id { get; set; }
        public Ejemplar ejemplar = new Ejemplar();
        //public Lector lector;
        public DateTime fecha_prestamo;
        public DateTime fecha_devolucion;
        public Lector lector;

        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public static List<Prestamo> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            List<Prestamo> lista = new List<Prestamo>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT id, fecha_de_prestamo, fecha_de_devolucion FROM Prestamo";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prestamo e = new Prestamo()
                            {
                                id = reader.GetInt32(0),
                                fecha_prestamo = reader.GetDateTime(1),
                                fecha_devolucion = reader.GetDateTime(2)
                            };
                            lista.Add(e);
                        }
                    }
                }
            }
            foreach(var p in lista){
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT Ejemplar.Codigo FROM Ejemplar JOIN Prestamo ON Prestamo.Ejemplar = Ejemplar.Codigo WHERE Prestamo.id =" + p.id;

                    using (SqlCommand c = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader r = c.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                p.ejemplar = new Ejemplar()
                                {
                                    codigo = r.GetString(0)
                                };
                            }
                        }
                    }
                }
            }
            foreach(var p in lista)
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT Lector.ID, Lector.Nombre, Lector.Apellidos FROM Lector JOIN Prestamo ON Prestamo.id = Lector.ID WHERE Prestamo.id = " + p.id;

                    using (SqlCommand c = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader r = c.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                p.lector = new Lector()
                                {
                                    id = r.GetInt32(0),
                                    nombre = r.GetString(1),
                                    apellidos = r.GetString(2),
                                    curso_departamento = Prestamo.getCursoOrDepartamento()
                                };
                            }
                        }
                    }
                }
            }
            foreach (var p in lista)
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT Libro.ISBN, Libro.Nombre FROM Prestamo JOIN Ejemplar ON Ejemplar.Codigo = Prestamo.Ejemplar JOIN Libro ON Ejemplar.ISBN = Libro.ISBN WHERE Prestamo.id="+ p.id;

                    using (SqlCommand c = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader re = c.ExecuteReader())
                        {
                            while (re.Read())
                            {
                                p.libro = new Libro()
                                {
                                    isbn = re.GetString(0),
                                    nombre = re.GetString(1)
                                };
                            }
                        }
                    }
                }
            }

            return lista;
        }

        public static string getCursoOrDepartamento()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var value = random.Next(0, 5);
            switch (value)
            {
                case 0:
                    return "INFORMATICA";
                case 1:
                    return "2º MULWEB";
                case 2:
                    return "MATEMATICAS";
                case 3:
                    return "1º MULWEB";
                case 4:
                    return "2º DAM";
                case 5:
                    return "LENGUA";
            }
            return "1º SMR";
        }

        public static bool Delete(string id)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";


            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("DELETE FROM Prestamo WHERE Ejemplar='{0}'", id);

                using (SqlCommand c = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        c.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return false;
        }
    }
}
