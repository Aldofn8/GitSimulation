using GitSimulation;

public class Program
{
    static StagingArea stagingArea = new StagingArea();
    static List<Commit> historialCommits = new List<Commit>();
    static void Main(string[] args)
    {

        // Cargar los commits desde el archivo al iniciar el programa
        if (File.Exists("commits.txt"))
        {
            string[] lines = File.ReadAllLines("commits.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string mensaje = parts[0];
                DateTime fecha = DateTime.Parse(parts[1]);
                List<string> archivosModificados = new List<string>(parts.Skip(2));
                Commit commit = new Commit(mensaje, fecha, archivosModificados);
                historialCommits.Add(commit);
            }
        }

        while (true)
        {


            Console.Write("git> ");
            string input = Console.ReadLine();

            string[] parts = input.Split(' ');
            string command = parts[0];

            switch (command)
            {
                case "add":
                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Uso incorrecto: add <nombre_archivo>");
                        break;
                    }
                    stagingArea.AddFile(parts[1]);
                    Console.WriteLine($"Se ha agregado el archivo '{parts[1]}' al stage.");
                    break;
                case "commit":
                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Uso incorrecto: commit <mensaje>");
                        break;
                    }
                    string mensajeCommit = string.Join(" ", parts.Skip(1));
                    Commit(mensajeCommit);
                    break;
                case "push":
                    Push();
                    break;
                case "help":
                    Console.WriteLine("Comandos disponibles:");
                    Console.WriteLine("  add <nombre_archivo> - Agregar un archivo al stage.");
                    Console.WriteLine("  commit - Realizar un commit de los archivos en el área de preparación.");
                    Console.WriteLine("  push - Enviar los commits locales al servidor remoto.");
                    break;
                default:
                    Console.WriteLine("Comando no reconocido. Escribe 'help' para ver la lista de comandos disponibles.");
                    break;
                case "status":
                    Console.WriteLine("Archivos en el área de preparación:");
                    foreach (string archivo in stagingArea.GetArchivosAgregados())
                    {
                        Console.WriteLine(archivo);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Archivos no comprometidos:");
                    foreach (string archivo in stagingArea.GetArchivosNoComprometidos())
                    {
                        Console.WriteLine(archivo);
                    }
                    break;
                case "log":
                    Log();
                    break;
            }
        }
    }

    static void Commit(string mensaje)
    {

        // Obtener archivos modificados en el área de preparación
        List<string> archivosModificados = stagingArea.GetArchivosAgregados();
        if (archivosModificados.Count == 0)
        {
            Console.WriteLine("No hay archivos en el área de preparación para realizar el commit.");
            return;
        }

        // Crear una cadena que representa el commit
        string commitString = $"{mensaje},{DateTime.Now}";
        foreach (string archivo in archivosModificados)
        {
            commitString += $",{archivo}";
        }

        // Guardar la cadena del commit en un archivo de texto
        File.AppendAllText("commits.txt", commitString + Environment.NewLine);

        // Limpiar el área de preparación después de realizar un commit
        stagingArea.Clear();

        // Mostrar mensaje de éxito
        Console.WriteLine("Se ha realizado el commit con éxito.");
    }

    static void Push()
    {
        Console.WriteLine("Los commits locales han sido enviados al servidor remoto.");
    }

    static void Log()
    {
        // Verificar si el archivo de commits existe
        if (File.Exists("commits.txt"))
        {
            // Leer todas las líneas del archivo de commits
            string[] lines = File.ReadAllLines("commits.txt");

            if (lines.Length == 0)
            {
                Console.WriteLine("No hay commits realizados hasta el momento.");
                return;
            }

            // Mostrar los detalles de cada commit
            Console.WriteLine("Historial de commits:");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string mensaje = parts[0];
                DateTime fecha = DateTime.Parse(parts[1]);
                List<string> archivosModificados = new List<string>(parts.Skip(2));

                // Mostrar detalles del commit
                Console.WriteLine($"Mensaje: {mensaje}");
                Console.WriteLine($"Fecha: {fecha}");
                Console.WriteLine("Archivos modificados:");
                foreach (string archivo in archivosModificados)
                {
                    Console.WriteLine($"  - {archivo}");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No se encontró el archivo de commits.");
        }
    }
}