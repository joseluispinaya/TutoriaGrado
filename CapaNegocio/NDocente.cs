using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NDocente
    {
        #region "PATRON SINGLETON"
        private static NDocente daoEmpleado = null;
        private NDocente() { }
        public static NDocente GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NDocente();
            }
            return daoEmpleado;
        }
        #endregion

        public Respuesta<List<DocenteModelDTO>> ObtenerDocentesProyectos(int IdCarrera)
        {
            return DDocente.GetInstance().ObtenerDocentesProyectos(IdCarrera);
        }
    }
}
