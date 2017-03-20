using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Relacion2Clases
{
    class Alumno
    {
        private string nombre;
        private int edad;
        private decimal calificacion;
        
        public Alumno(string nombre, int edad, decimal calificacion)
        {
            if (nombre == "")
            {
                throw new Exception("El nombre no puede estar vacio");
            }
            else
            {
                this.nombre = nombre;
            }
            if (edad < 17 || edad > 100)
            {
                throw new Exception("La edad tiene que estar entre 17 años y 100 años");
            }
            else
            {
                this.edad = edad;
            }
            if (calificacion < 0 || calificacion > 10)
            {
                throw new Exception("La nota tiene que estar entre 0 y 10");
            }
            else
            {
                this.calificacion = calificacion;
            }
        }
        public string Imprime()
        {
            string impresion;
            impresion = nombre.PadRight(40) + edad.ToString().PadRight(10) + calificacion.ToString().PadRight(10);
            return impresion;
        }
        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                if (value == "")
                {
                    throw new Exception("El nombre no puede estar vacio");
                }
                else
                {
                    this.nombre = value;
                }
            }
            
        }
        public decimal Calificacion
        {
            get
            {
                return calificacion;
            }

            set
            {
                if (value <0||value>10)
                {
                    throw new Exception("La nota es invalida");
                }
                else
                {
                    this.calificacion = value;
                }
            }

        }
        public int Edad
        {
            get
            {
                return edad;
            }
            set
            {
                if(value < 17 || value > 100)
                {
                    throw new Exception("La edad es invalida");
                }
                else
                {
                    this.calificacion = value;
                }
            }

        }

        public override string ToString()
        {
            return Imprime();
        }
    }
    class Grupo
    {
        public List<Alumno> lista_alumnos;

        public Grupo()
        {
            lista_alumnos = new List<Alumno>( );
        }
        public void InsertaAlumnoLista(Alumno a)
        {
            lista_alumnos.Add(a);
        }
        public void InsertaAlumnoLista(string nombre, int edad, decimal calificacion)
        {
            Alumno a = new Alumno(nombre, edad, calificacion);
            lista_alumnos.Add(a);
        }
        public string Imprime()
        {
            string lista = "";
           
            for (int i = 0; i < lista_alumnos.Count; i++)
            {
                lista = lista + lista_alumnos[i] + "\n";
            }
            return lista;
        }
        public void EscribeFicheroAlumnos(List<Alumno> lista_alumnos, string fichero="alumnos.bin")
        {
            FileStream fs = new FileStream(fichero, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            int i;
            bw.Write(lista_alumnos.Count);
            for (i = 0; i < lista_alumnos.Count; i++)
            {
                bw.Write(lista_alumnos[i].Nombre);
                bw.Write(lista_alumnos[i].Edad);
                bw.Write(lista_alumnos[i].Calificacion);
            }
            bw.Close();
            fs.Close();
        }
        public void LeeFicheroAlumnos(List<Alumno> lista_alumnos, string fichero="alumnos.bin")
        {
            FileStream fs = new FileStream(fichero, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            string nombre;
            int edad;
            decimal calificacion;
            while (fs.Position < fs.Length)
            {
                nombre = br.ReadString();
                
                edad = br.ReadInt32();
                
                calificacion = br.ReadDecimal();

                Alumno a = new Alumno(nombre, edad, calificacion);
                lista_alumnos.Add(a);
            }
            br.Close();
            fs.Close();
        }
        static void EscribeFicheroAlumnosTXT(List<Alumno> lista_alumnos, string fichero="alumnos.txt")
        {
            StreamWriter sw = new StreamWriter(fichero, false, Encoding.Default);
            int i;

            sw.Write(lista_alumnos.Count);
            for (i = 0; i < lista_alumnos.Count; i++)
            {
                sw.WriteLine(lista_alumnos[i].Nombre);
                sw.WriteLine(lista_alumnos[i].Edad);
                sw.WriteLine(lista_alumnos[i].Calificacion);
            }
            sw.Close();
            Console.WriteLine("Proceso finalizado");
        }
        public void LeeFicheroAlumnosTXT(List<Alumno> lista_alumnos, string fichero="alumnos.txt")
        {
            StreamReader sr = new StreamReader(fichero, Encoding.Default);
            string copia;
            sr.ReadLine();
            lista_alumnos.Clear();
            while (!sr.EndOfStream)
            {
                copia = sr.ReadLine();
                Console.WriteLine("Nombre: " + copia);
                copia = sr.ReadLine();
                Console.WriteLine("Edad: " + copia);
                copia = sr.ReadLine();
                Console.WriteLine("Calificacion: " + copia);
            }
            sr.Close();
        }
        public void ImprimeColores()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Nombre                                 ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Edad     ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Calificacion");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(Imprime());
        }

        


    }
    class Program
    {
        static void Main(string[] args)
        {
            Alumno a = new Alumno("Pepe", 56, 4);
            //Console.WriteLine(a);
            Alumno b = new Alumno("Segismundo", 69, 4.9m);
            //Console.WriteLine(b);
            Grupo g = new Grupo();
            g.InsertaAlumnoLista(a);
            g.InsertaAlumnoLista(b);
            g.ImprimeColores();
            //Console.WriteLine(g.Imprime());
            Console.ReadKey();
        }
    }
}
