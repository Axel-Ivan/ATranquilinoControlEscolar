using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Configuration;
//using Microsoft.Extensions.Configuration;
//using System.Configuration.ConfigurationManager;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result resultMaterias = new ML.Result();
            resultMaterias.Objects = new List<object>();
            ML.Materia resultMateriaItem = new ML.Materia();

            string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:2211/api/");
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("Materia");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var resultItem in readTask.Result.Objects)
                    {
                        resultMateriaItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        resultMaterias.Objects.Add(resultMateriaItem);
                        resultMateriaItem.Materias = resultMaterias.Objects;
                    }
                }
            }

            return View(resultMateriaItem);
        }
        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();

            if(IdMateria == null) //Add
            { 
                return View(materia);
            } // Update
            else
            {
                ML.Materia materiaItem = new ML.Materia();
                ML.Result result = new ML.Result();
                using(var client = new HttpClient())
                {
                    string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
                    //client.BaseAddress = new Uri("http://localhost:2211/api/");
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.GetAsync("Materia/" + IdMateria);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        materiaItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());

                        result.Object = materiaItem;
                        result.Correct = true;                       
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Materia";
                    }
                }
                if (result.Correct)
                {
                    return View(materiaItem);
                }
            }

            return PartialView("ValidationModal");
        }


        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if(ModelState.IsValid)
            {
                ML.Result result = new ML.Result();

                //Pruebas AppSettings
                char[] validacionNombreMateria = ConfigurationManager.AppSettings["ValidacionNombreMateria"].ToCharArray();

                foreach(char caracter in validacionNombreMateria)
                {
                    materia.Nombre = materia.Nombre.Replace(caracter.ToString(), "");
                }

                if(materia.IdMateria == null || materia.IdMateria == 0) //Comienza Add
                {
                    using (var client = new HttpClient())
                    {
                        string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
                        //client.BaseAddress = new Uri("http://localhost:2211/api/");
                        client.BaseAddress = new Uri(urlAPI);

                        var postTask = client.PostAsJsonAsync<ML.Materia>("Materia", materia);
                        postTask.Wait();

                        var resultAdd = postTask.Result;
                        if (resultAdd.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La materia se ingresó correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrió un error al momento de ingresar la materia: " + result.ErrorMessage;
                        }
                    }
                }
                else //Comienza Update
                {
                    using(var client = new HttpClient())
                    {
                        string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
                        //client.BaseAddress = new Uri("http://localhost:2211/api/");
                        client.BaseAddress = new Uri(urlAPI);

                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<ML.Materia>("Materia", materia);
                        postTask.Wait();

                        var resultUpdate = postTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "La materia se actualizó correctamente!!!";
                        }
                        else
                        {
                            ViewBag.Message = "La materia no se actualizó, ocurrió el siguiente error: " + result.ErrorMessage;
                        }
                    }
                }
                return PartialView("ValidationModal");
            }

            else
            {
                return View(materia);
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdMateria)
        {
            ML.Result result = new ML.Result();

            using(var client = new HttpClient())
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
                //client.BaseAddress = new Uri("http://localhost:2211/api/");
                client.BaseAddress = new Uri(urlAPI);

                var postTask = client.DeleteAsync("Materia/" + IdMateria);
                postTask.Wait();

                var resultDelete = postTask.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El registro de materia se eliminó correctamente!!!";
                }
                else
                {
                    ViewBag.Message = "El registro no se eliminó, ocurrió el error: " + result.ErrorMessage;
                }
            }

            return PartialView("ValidationModal");
        }

    }
}