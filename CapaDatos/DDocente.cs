using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class DDocente
    {
        #region "PATRON SINGLETON"
        private static DDocente daoEmpleado = null;
        private DDocente() { }
        public static DDocente GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new DDocente();
            }
            return daoEmpleado;
        }
        #endregion

        public Respuesta<List<DocenteModelDTO>> ObtenerDocentesProyectos(int IdCarrera)
        {
            try
            {
                var docentes = new Dictionary<int, DocenteModelDTO>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                using (SqlCommand cmd = new SqlCommand("sp_DocentesConProyectosPorCarrera", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCarrera", IdCarrera);
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idDocente = Convert.ToInt32(dr["IdDocente"]);

                            if (!docentes.ContainsKey(idDocente))
                            {
                                docentes[idDocente] = new DocenteModelDTO
                                {
                                    IdDocente = idDocente,
                                    NombreCompleto = dr["NombreCompleto"].ToString(),
                                    ResumenPerfil = dr["ResumenPerfil"].ToString(),
                                    Proyectos = new List<ProyectoSimpleDTO>()
                                };
                            }

                            if (dr["IdProyecto"] != DBNull.Value)
                            {
                                docentes[idDocente].Proyectos.Add(new ProyectoSimpleDTO
                                {
                                    Titulo = dr["Titulo"].ToString(),
                                    Gestion = dr["Gestion"].ToString()
                                });
                            }
                        }
                    }
                }

                return new Respuesta<List<DocenteModelDTO>>
                {
                    Estado = true,
                    Data = docentes.Values.ToList(),
                    Mensaje = "Docentes y proyectos obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<DocenteModelDTO>>
                {
                    Estado = false,
                    Mensaje = "Error: " + ex.Message
                };
            }
        }

        public Respuesta<List<DocenteModelDTO>> ObtenerDocentesProyectosNew(int carreraId)
        {
            try
            {
                var docentes = new Dictionary<int, DocenteModelDTO>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                using (SqlCommand cmd = new SqlCommand("sp_ConsultaDocentesPorCarrera", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CarreraId", carreraId);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idDocente = Convert.ToInt32(dr["Id"]);

                            if (!docentes.ContainsKey(idDocente))
                            {
                                docentes[idDocente] = new DocenteModelDTO
                                {
                                    NombreCompleto = dr["NombreCompleto"].ToString(),
                                    ResumenPerfil = dr["ResumenPerfil"].ToString(),
                                    LineasInvestigacion = new List<string>(),
                                    ExperienciaRelevante = new List<string>(),
                                    Proyectos = new List<ProyectoSimpleDTO>()
                                };
                            }

                            var docente = docentes[idDocente];

                            // Línea de investigación
                            if (dr["LineaInvestigacion"] != DBNull.Value)
                            {
                                string linea = dr["LineaInvestigacion"].ToString();
                                if (!docente.LineasInvestigacion.Contains(linea))
                                    docente.LineasInvestigacion.Add(linea);
                            }

                            // Experiencia relevante
                            if (dr["ExperienciaTema"] != DBNull.Value)
                            {
                                string tema = dr["ExperienciaTema"].ToString();
                                if (!docente.ExperienciaRelevante.Contains(tema))
                                    docente.ExperienciaRelevante.Add(tema);
                            }

                            // Proyectos
                            if (dr["ProyectoId"] != DBNull.Value)
                            {
                                docente.Proyectos.Add(new ProyectoSimpleDTO
                                {
                                    Id = Convert.ToInt32(dr["ProyectoId"]),
                                    Titulo = dr["ProyectoTitulo"].ToString(),
                                    Gestion = dr["ProyectoGestion"].ToString()
                                });
                            }
                        }
                    }
                }

                return new Respuesta<List<DocenteModelDTO>>
                {
                    Estado = true,
                    Data = docentes.Values.ToList(),
                    Mensaje = "Docentes obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<DocenteModelDTO>>
                {
                    Estado = false,
                    Mensaje = "Error: " + ex.Message
                };
            }
        }

    }
}
