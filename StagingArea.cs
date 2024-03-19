using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSimulation
{
    public class StagingArea
    {
        private List<string> archivosAgregados;

        public StagingArea()
        {
            archivosAgregados = new List<string>();
        }

        public void AddFile(string nombreArchivo)
        {
            archivosAgregados.Add(nombreArchivo);
        }

        public List<string> GetArchivosAgregados()
        {
            return archivosAgregados;
        }

        public void Clear()
        {
            archivosAgregados.Clear();
        }

        public List<string> GetArchivosNoComprometidos()
        {
            // Obtener todos los archivos del sistema de archivos
            List<string> archivosEnDirectorio = Directory.GetFiles(Directory.GetCurrentDirectory()).Select(Path.GetFileName).ToList();
            // Calcular la diferencia entre todos los archivos y los agregados al área de preparación
            List<string> archivosNoComprometidos = archivosEnDirectorio.Except(archivosAgregados).ToList();
            return archivosNoComprometidos;
        }
    }
}
