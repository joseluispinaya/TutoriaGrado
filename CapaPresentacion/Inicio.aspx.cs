using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;

namespace CapaPresentacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Respuesta<List<DocenteModelDTO>> ListaDocentes(int IdCarrera)
        {
            try
            {
                Respuesta<List<DocenteModelDTO>> Lista = NDocente.GetInstance().ObtenerDocentesProyectos(IdCarrera);
                //var listaDocentes = Lista.Data;
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<DocenteModelDTO>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener las propiedades: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<List<TutorRecomendadoDTO>> ListaRecomendados(int IdCarrera, string Titulo)
        {
            try
            {
                Respuesta<List<DocenteModelDTO>> Lista = NDocente.GetInstance().ObtenerDocentesProyectos(IdCarrera);
                var listaDocentes = Lista.Data;
                if (!Lista.Estado || listaDocentes.Count == 0)
                {
                    return new Respuesta<List<TutorRecomendadoDTO>>()
                    {
                        Estado = false,
                        Mensaje = "No se encontraron docentes para la carrera seleccionada."
                    };
                }
                var recomendacion = Utilidadesj.GetInstance().GenerarRecomendacion(Titulo, listaDocentes);

                if (!recomendacion.Estado)
                {
                    return new Respuesta<List<TutorRecomendadoDTO>>()
                    {
                        Estado = false,
                        Mensaje = recomendacion.Mensaje
                    };
                }

                return recomendacion;
            }
            catch (Exception ex)
            {
                return new Respuesta<List<TutorRecomendadoDTO>>()
                {
                    Estado = false,
                    Mensaje = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

    }
}