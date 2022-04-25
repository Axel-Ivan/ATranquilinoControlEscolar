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

            if (IdAlumno != null)
            {               
                ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno.Value);
                Session["Alumno"] = resultAlumno; //Guardamos los datos del alumno en la sesion

                alumnoMateria.Alumno = new ML.Alumno();
                alumnoMateria.Alumno = (ML.Alumno)resultAlumno.Object;

                ML.Result resultMaterias = BL.AlumnoMateria.MateriasGetAsignadas(IdAlumno.Value);
                //Session["Materias"] = resultMaterias; 
                alumnoMateria.Materia = new ML.Materia();
                alumnoMateria.Materia.Materias = new List<object>();
                alumnoMateria.Materia.Materias = resultMaterias.Objects;

                return View(alumnoMateria);
            }
            else
            {
                ML.Result resultAlumno = new ML.Result();
                resultAlumno = (ML.Result)Session["Alumno"];

                alumnoMateria.Alumno = new ML.Alumno();
                alumnoMateria.Alumno = (ML.Alumno)resultAlumno.Object;

                //ML.Result resultMaterias = new ML.Result();
                //resultMaterias = (ML.Result)Session["Materias"];
                //No se pueden guardar las materias porque no se verian reflejadas las que se agreguen
                ML.Result resultMaterias = BL.AlumnoMateria.MateriasGetAsignadas(alumnoMateria.Alumno.IdAlumno);               

                alumnoMateria.Materia = new ML.Materia();
                alumnoMateria.Materia.Materias = new List<object>();
                alumnoMateria.Materia.Materias = resultMaterias.Objects;

                return View(alumnoMateria);
            }           
        }

        [HttpGet]
        public ActionResult MateriasNoAsignadas(int IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            ML.Result result = new ML.Result();
            result = (ML.Result)Session["Alumno"];

            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno = (ML.Alumno)result.Object;

            result = BL.AlumnoMateria.AlumnoMateriasNoAsignadas(IdAlumno);           
            alumnoMateria.Materia = new ML.Materia();
            alumnoMateria.Materia.Materias = result.Objects;

            return View(alumnoMateria);
        }

        [HttpPost]
        public ActionResult MateriasNoAsignadas(ML.AlumnoMateria alumnoMateria)
        {
            string[] arreglo = new string[6];
            arreglo[0] = Request.Form["Matematicas"];
            arreglo[1] = Request.Form["Ingles"];
            arreglo[2] = Request.Form["Biología"];
            arreglo[3] = Request.Form["Quimica"];
            arreglo[4] = Request.Form["Programacion"];
            arreglo[5] = Request.Form["Dibujo Técnico"];

            for(int i = 0; i < 6; i++)
            {
                arreglo[i] = (arreglo[i] == null) ? "0" : arreglo[i];

                if (arreglo[i] != "0")
                {
                    int IdMateria = Convert.ToInt32(arreglo[i]);
                    ML.Result result = BL.AlumnoMateria.MateriaAdd(alumnoMateria.Alumno.IdAlumno, IdMateria);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Se agreg+o el registro correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrió un error al ingresar la materia: " + result.ErrorMessage;
                    }
                }
            }

            return PartialView("ValidationModal");
        }

        [HttpGet]
        public ActionResult Delete(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();
            result = BL.AlumnoMateria.AlumnoDeleteMateria(alumnoMateria);

            if (result.Correct)
            {
                ViewBag.Message = "Se eliminó la materia del alumno!!!";
            }
            else
            {
                ViewBag.Message = "No se eliminó la materia, ocurrió el siguiente error: " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }
    }
}