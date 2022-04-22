using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlumnoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlumnoService.svc or AlumnoService.svc.cs at the Solution Explorer and start debugging.
    public class AlumnoService : IAlumnoService
    {
        public SL_WCF.Result Add(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);

            return new Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public SL_WCF.Result Update(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Update(alumno);

            return new Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public SL_WCF.Result Delete(int IdAlumno)
        {
            ML.Result result = BL.Alumno.Delete(IdAlumno);

            return new Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Objects = result.Objects,
                Object = result.Object
            };
        }

        public SL_WCF.Result GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();

            return new Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }

        public SL_WCF.Result GetById(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);

            return new Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }
    }
}
