using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        [HttpGet]
        public ActionResult AlumnoGetAll()
        {
            ML.Result resultAlumnos = BL.Alumno.GetAll();
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            if (resultAlumnos.Correct)
            {
                alumnoMateria.Alumno = new ML.Alumno(); //Instanciamos tanto la llave foránea como la nueva lista
                alumnoMateria.Alumno.Alumnos = new List<object>();
                alumnoMateria.Alumno.Alumnos = resultAlumnos.Objects;
            }
            return View(alumnoMateria);
        }

        [HttpGet]
        public ActionResult MateriaAsignada(int? IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
              
            ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno.Value);
            //Session["Alumno"] = resultAlumno; //Guardamos los datos del alumno en la sesion

            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno = (ML.Alumno)resultAlumno.Object;

            ML.Result resultMaterias = BL.AlumnoMateria.MateriasGetAsignadas(IdAlumno.Value);
            //Session["Materias"] = resultMaterias; 
            alumnoMateria.Materia = new ML.Materia();
            alumnoMateria.Materia.Materias = new List<object>();
            alumnoMateria.Materia.Materias = resultMaterias.Objects;

            return View(alumnoMateria);                    
        }


        [HttpGet]
        public ActionResult MateriasNoAsignadas(int? IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            ML.Result result = new ML.Result();
            result = BL.Alumno.GetById(IdAlumno.Value);
            //result = (ML.Result)Session["Alumno"];

            if(result.Object != null)
            {
                alumnoMateria.Alumno = new ML.Alumno();
                alumnoMateria.Alumno = (ML.Alumno)result.Object;

                result = BL.AlumnoMateria.AlumnoMateriasNoAsignadas(IdAlumno.Value);
                alumnoMateria.Materia = new ML.Materia();
                alumnoMateria.Materia.Materias = result.Objects;

                return View(alumnoMateria);
            }
            else
            {
                return View(alumnoMateria);
            }           
        }

        [HttpPost]
        public ActionResult MateriasNoAsignadas(ML.AlumnoMateria alumnoMateria)
        {

            if(alumnoMateria.AlumnoMaterias != null)
            {
                foreach(string IdMateria in alumnoMateria.AlumnoMaterias)
                {
                    ML.AlumnoMateria alumnomateria = new ML.AlumnoMateria();

                    alumnomateria.Alumno = new ML.Alumno();
                    alumnomateria.Alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;

                    alumnomateria.Materia = new ML.Materia();
                    alumnomateria.Materia.IdMateria = int.Parse(IdMateria);
                    ML.Result result = BL.AlumnoMateria.MateriaAdd(alumnomateria);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Se asignó la materia correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "Hubo un problema al asignar la materia: " + result.ErrorMessage;
                    }
                }
            }
            else
            {
                return RedirectToAction("MateriaAsignada", "AlumnoMateria", new { IdAlumno = alumnoMateria.Alumno.IdAlumno }); //No aparecerá modal
            }
            //return PartialView("ValidationModal", alumnoMateria); Aparecera Modal
            return RedirectToAction("MateriaAsignada", "AlumnoMateria", new { IdAlumno = alumnoMateria.Alumno.IdAlumno }); //No aparecerá modal
        }

        [HttpGet]
        public ActionResult Delete(int IdAlumno, int IdAlumnoMateria)
        {
            ML.Result result = new ML.Result();
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.IdAlumnoMateria = IdAlumnoMateria;
            alumnoMateria.Alumno.IdAlumno = IdAlumno;
            result = BL.AlumnoMateria.AlumnoDeleteMateria(alumnoMateria);

            if (result.Correct)
            {
                ViewBag.Message = "Se eliminó la materia del alumno!!!";
            }
            else
            {
                ViewBag.Message = "No se eliminó la materia, ocurrió el siguiente error: " + result.ErrorMessage;
            }

            return PartialView("ValidationModal", alumnoMateria);
        }
    }
}