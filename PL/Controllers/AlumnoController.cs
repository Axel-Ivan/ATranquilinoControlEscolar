using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();

            AlumnoService.AlumnoServiceClient alumnoClient = new AlumnoService.AlumnoServiceClient();
            var alumnoItem = alumnoClient.GetAll();

            alumno.Alumnos = alumnoItem.Objects.ToList();

            if (alumnoItem != null)
            {
                return View(alumno);
            }

            return View(alumno);
        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            if(IdAlumno == null || IdAlumno == 0)
            {
                return View(alumno);
            }
            else
            {
                AlumnoService.AlumnoServiceClient alumnoClient = new AlumnoService.AlumnoServiceClient();
                var alumnoItem = alumnoClient.GetById(IdAlumno.Value);

                alumno = (ML.Alumno)alumnoItem.Object;

                if (alumnoItem.Correct)
                {
                    return View(alumno);
                }
                else
                {
                    alumnoItem.Correct = false;
                    alumnoItem.ErrorMessage = "Ocurrió un error al cargar la informacion";
                }
                
            }

            return PartialView("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if(alumno.IdAlumno == 0)
                {
                    AlumnoService.AlumnoServiceClient alumnoClient = new AlumnoService.AlumnoServiceClient();
                    var alumnoItem = alumnoClient.Add(alumno);

                    if (alumnoItem.Correct)
                    {
                        ViewBag.Message = "El alumno se ingresó correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "El alumno no se ingresó, ocurrió el siguiente error: " + alumnoItem.ErrorMessage;
                    }
                }
                else
                {
                    AlumnoService.AlumnoServiceClient alumnoClient = new AlumnoService.AlumnoServiceClient();
                    var alumnoItem = alumnoClient.Update(alumno);

                    if (alumnoItem.Correct)
                    {
                        ViewBag.Message = "El alumnó se actualizó correctamente!!!";
                    }
                    else
                    {
                        ViewBag.Message = "El alumno no se actualñizó, ocurrió el siguiente error: " + alumnoItem.ErrorMessage;
                    }
                }
                return PartialView("ValidationModal");
            }
            else
            {
                return View(alumno);
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdAlumno)
        {
            AlumnoService.AlumnoServiceClient alumnoClient = new AlumnoService.AlumnoServiceClient();
            var alumnoItem = alumnoClient.Delete(IdAlumno);

            if (alumnoItem.Correct)
            {
                ViewBag.Message = "El alumno se eliminó correctamente!!!";
            }
            else
            {
                ViewBag.Message = "El alumnó no se eliminó, ocurrió el siguiente error: " + alumnoItem.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

    }
}