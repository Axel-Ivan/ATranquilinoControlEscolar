using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriaGetAll().ToList();
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        foreach(var obj in procedure)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Costo = obj.Costo.Value;
                            result.Objects.Add(materia);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriaAdd(materia.Nombre, materia.Costo);

                    if(procedure >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result GetById(int IdMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriaGetById(IdMateria).FirstOrDefault();
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = procedure.IdMateria;
                        materia.Nombre = procedure.Nombre;
                        materia.Costo = procedure.Costo.Value;
                        result.Object = materia;

                        result.Correct = true;
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

        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriaUpdate(materia.IdMateria, materia.Nombre, materia.Costo);

                    if(procedure >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result Delete(int IdMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriaDelete(IdMateria);

                    if(procedure >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result MateriasGetAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriasGetAsignadas(IdAlumno);
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        foreach (var obj in procedure)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.IdAlumnoMateria = obj.IdAlumnoMateria;
                            alumnoMateria.Alumno = new ML.Alumno();
                            alumnoMateria.Alumno.IdAlumno = obj.IdAlumno;
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = obj.IdMateria;
                            alumnoMateria.Materia.Nombre = obj.MateriaNombre;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;
                            result.Objects.Add(alumnoMateria);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                    }
                    result.Correct = true;
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
