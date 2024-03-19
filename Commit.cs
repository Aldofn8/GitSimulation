using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSimulation
{
    internal class Commit
    {
        public string Mensaje { get; }
        public DateTime Fecha { get; }
        public List<string> ArchivosModificados { get; }

        public Commit(string mensaje, DateTime fecha, List<string> archivosModificados)
        {
            Mensaje = mensaje;
            Fecha = DateTime.Now;
            ArchivosModificados = archivosModificados;
        }
    }
}
