using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DocenteModelDTO
    {
        public int IdDocente { get; set; }
        public string NombreCompleto { get; set; }
        public string ResumenPerfil { get; set; }
        public List<ProyectoSimpleDTO> Proyectos { get; set; }
    }
}
