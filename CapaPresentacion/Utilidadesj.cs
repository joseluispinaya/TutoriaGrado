using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;

namespace CapaPresentacion
{
    public class Utilidadesj
    {
        private static readonly string OpenAIApiKey = ConfigurationManager.AppSettings["OpenAIApiKey"];

        #region "PATRON SINGLETON"
        public static Utilidadesj _instancia = null;

        private Utilidadesj()
        {

        }

        public static Utilidadesj GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new Utilidadesj();
            }
            return _instancia;
        }
        #endregion

        public Respuesta<List<TutorRecomendadoDTO>> GenerarRecomendacion(string tituloProyecto, List<DocenteModelDTO> docentes)
        {
            if (string.IsNullOrWhiteSpace(OpenAIApiKey))
            {
                return new Respuesta<List<TutorRecomendadoDTO>>()
                {
                    Estado = false,
                    Mensaje = "OpenAI API Key no configurada."
                };
            }

            var prompt = ConstruirPrompt(tituloProyecto, docentes);

            var requestBody = new
            {
                model = "gpt-4.1-mini",
                messages = new[]
                {
                    new { role = "system", content = "Eres un asesor académico universitario especializado en evaluación de perfiles docentes y asignación de tutores para trabajos de grado. Analiza afinidad temática, experiencia profesional y antecedentes de tutoría." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.3
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", OpenAIApiKey);

                // Llamada SIN async
                var response = http
                    .PostAsync("https://api.openai.com/v1/chat/completions", content)
                    .GetAwaiter()
                    .GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    return new Respuesta<List<TutorRecomendadoDTO>>()
                    {
                        Estado = false,
                        Mensaje = "Error al comunicarse con OpenAI."
                    };
                }

                var responseJson = response
                    .Content
                    .ReadAsStringAsync()
                    .GetAwaiter()
                    .GetResult();

                var resultado = ExtraerResultado(responseJson);

                return new Respuesta<List<TutorRecomendadoDTO>>()
                {
                    Estado = true,
                    Data = resultado,
                    Mensaje = "Recomendación generada correctamente"
                };
            }
        }

        private List<TutorRecomendadoDTO> ExtraerResultado(string json)
        {
            using (var doc = JsonDocument.Parse(json))
            {
                var content = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return JsonSerializer.Deserialize<List<TutorRecomendadoDTO>>(content);
            }
        }

        private string ConstruirPrompt(string titulo, List<DocenteModelDTO> docentes)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Recomienda los tutores más adecuados para el siguiente proyecto:");
            sb.AppendLine($"Proyecto: {titulo}");
            sb.AppendLine("Docentes disponibles:");

            foreach (var d in docentes)
            {
                sb.AppendLine($"Docente: {d.NombreCompleto}");
                sb.AppendLine($"Perfil: {d.ResumenPerfil}");

                if (d.Proyectos.Any())
                {
                    sb.AppendLine("Proyectos tutorizados:");
                    foreach (var p in d.Proyectos)
                        sb.AppendLine($"- {p.Titulo} ({p.Gestion})");
                }

                sb.AppendLine();
            }

            sb.AppendLine("Selecciona únicamente los docentes con afinidad REAL con el proyecto.");
            sb.AppendLine("Descarta completamente a los docentes cuya afinidad sea baja o irrelevante.");
            sb.AppendLine("Devuelve SOLO los mejores candidatos (máximo 3).");

            sb.AppendLine("Asigna PuntajeAfinidad de 0 a 100 según coincidencia temática, experiencia previa y proyectos tutorizados similares.");
            sb.AppendLine("Da mayor peso a los docentes que hayan tutorizado proyectos similares al proyecto propuesto.");

            sb.AppendLine("Responde exclusivamente con un JSON válido. No agregues texto adicional.");
            sb.AppendLine("Formato:");
            sb.AppendLine("[{\"NombreDocente\":\"\",\"PuntajeAfinidad\":0,\"Justificacion\":\"\"}]");

            return sb.ToString();
        }
    }
}