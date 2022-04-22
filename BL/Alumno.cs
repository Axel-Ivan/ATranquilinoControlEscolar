using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string procedure = "AlumnoGetAll";
                    using(SqlCommand cmd = new SqlCommand(procedure, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        DataTable alumnoTable = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(alumnoTable);
                        if(alumnoTable.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach(DataRow row in alumnoTable.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();
                                alumno.IdAlumno = int.Parse(row[0].ToString());
                                alumno.Nombre = row[1].ToString();
                                alumno.ApellidoPaterno = row[2].ToString();
                                alumno.ApellidoMaterno = row[3].ToString();
                                result.Objects.Add(alumno);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al mostrar los datos de la tabla";
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string procedure = "AlumnoAdd";
                    using(SqlCommand cmd = new SqlCommand(procedure, context))
                    {
                        cmd.Connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter parametro;

                        parametro = cmd.Parameters.Add("Nombre", SqlDbType.VarChar);
                        parametro.Value = alumno.Nombre;

                        parametro = cmd.Parameters.Add("ApellidoPaterno", SqlDbType.VarChar);
                        parametro.Value = alumno.ApellidoPaterno;

                        parametro = cmd.Parameters.Add("ApellidoMaterno", SqlDbType.VarChar);
                        parametro.Value = alumno.ApellidoMaterno;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if(rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string procedure = "AlumnoGetById";
                    using (SqlCommand cmd = new SqlCommand(procedure, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;
                        cmd.Parameters.AddRange(collection);

                        DataTable alumnoTable = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(alumnoTable);

                        if(alumnoTable.Rows.Count > 0)
                        {
                            DataRow row = alumnoTable.Rows[0];
                            result.Object = new Object();

                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = int.Parse(row[0].ToString());
                            alumno.Nombre = row[1].ToString();
                            alumno.ApellidoPaterno = row[2].ToString();
                            alumno.ApellidoMaterno = row[3].ToString();
                            result.Object = alumno;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al tomar los datos de la tabla";
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string procedure = "AlumnoUpdate";
                    using(SqlCommand cmd = new SqlCommand(procedure, context))
                    {
                        cmd.Connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter parametro;

                        parametro = cmd.Parameters.Add("@IdAlumno", SqlDbType.Int);
                        parametro.Value = alumno.IdAlumno;

                        parametro = cmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
                        parametro.Value = alumno.Nombre;

                        parametro = cmd.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar);
                        parametro.Value = alumno.ApellidoPaterno;

                        parametro = cmd.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar);
                        parametro.Value = alumno.ApellidoMaterno;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if(rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string procedure = "AlumnoDelete";
                    using(SqlCommand cmd = new SqlCommand(procedure, context))
                    {
                        cmd.Connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter parametro;

                        parametro = cmd.Parameters.Add("@IdAlumno", SqlDbType.Int);
                        parametro.Value = IdAlumno;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if(rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
