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

                ML.Result resultMaterias = BL.Materia.MateriasGetAsignadas(IdAlumno.Value);
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
                ML.Result resultMaterias = BL.Materia.MateriasGetAsignadas(alumnoMateria.Alumno.IdAlumno);               

                alumnoMateria.Materia = new ML.Materia();
                alumnoMateria.Materia.Materias = new List<object>();
                alumnoMateria.Materia.Materias = resultMaterias.Objects;

                return View(alumnoMateria);
            }

            
        }

        [HttpGet]
        public ActionResult Form()
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            ML.Result result = new ML.Result();
            result = (ML.Result)Session["Alumno"];

            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno = (ML.Alumno)result.Object;

            return View(alumnoMateria);
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {


            return PartialView("ValidationModal");
        }
    }
}