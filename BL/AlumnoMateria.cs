using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result MateriasGetAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriasGetAsignadas(IdAlumno);
                    result.Objects = new List<object>();

                    if (procedure != null)
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

        public static ML.Result AlumnoDeleteMateria(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.AlumnoMateriaDelete(alumnoMateria.IdAlumnoMateria);

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

        public static ML.Result AlumnoMateriasNoAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.MateriaGetNoAsignada(IdAlumno).ToList();
                    result.Objects = new List<object>();

                    if(procedure != null)
                    {
                        foreach(var obj in procedure)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = obj.IdMateria;
                            alumnoMateria.Materia.Nombre = obj.Nombre;
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

        public static ML.Result MateriaAdd(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ATranquilinoControlEscolarEntities context = new DL.ATranquilinoControlEscolarEntities())
                {
                    var procedure = context.AlumnoMateriaAdd(alumnoMateria.Alumno.IdAlumno, alumnoMateria.Materia.IdMateria);

                    if (procedure >= 1)
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
    }
}
