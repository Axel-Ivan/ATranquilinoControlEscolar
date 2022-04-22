using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string GetConnectionString()
        {
            //string Conexion = ConfigurationManager.ConnectionStrings["ATranquilinoControlEscolar"].ConnectionString;
            string Conexion = "Data Source=.;Initial Catalog=ATranquilinoControlEscolar;Persist Security Info=True;User ID=sa;Password=pass@word1";
            return Conexion;
        }
    }
}
