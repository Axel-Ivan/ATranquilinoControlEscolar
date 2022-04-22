using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    //[Route("api/[controller]/[action]")]
    public class MateriaController : ApiController
    {
        // GET: api/Materia
        [HttpGet]
        [Route("api/Materia")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Materia/5
        [HttpGet]
        [Route("api/Materia/{IdMateria}")]
        public IHttpActionResult GetById(int IdMateria)
        {
            ML.Result result = BL.Materia.GetById(IdMateria);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Materia
        [HttpPost]
        [Route("api/Materia")]
        public IHttpActionResult Add([FromBody]ML.Materia materia)
        {
            ML.Result result = BL.Materia.Add(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Materia/5
        [HttpPut]
        [Route("api/Materia")]
        public IHttpActionResult Update([FromBody]ML.Materia materia)
        {
            ML.Result result = BL.Materia.Update(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Materia/5
        [HttpDelete]
        [Route("api/Materia/{IdMateria}")]
        public IHttpActionResult Delete(int IdMateria)
        {
            ML.Result result = BL.Materia.Delete(IdMateria);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
