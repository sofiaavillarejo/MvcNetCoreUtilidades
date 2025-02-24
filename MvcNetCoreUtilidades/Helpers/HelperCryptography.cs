using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        //tendremos una nueva propiedad para almacenar el salt que hemos creado dinamicamente
        public static string Salt { get; set; }
        //cada vez que realicemos un cifrado, generamos un salt distinto
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i=1; i<=30; i++)
            {
                //generamos un numero aleatorio con codigos ascii
                int aleat = random.Next(1, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        //creamos emtodo para cifrar de forma eficiente
        public static string CifrarContenido(string contenido, bool comparar)
        {
            if (comparar == false)
            {
                //creamos un nuevo salt para el cifrado y lo alamcenamos en la propiedad
                Salt = GenerateSalt();
            }

            //el salt lo podemos usar en multiples lugares con insert
            string contenidoSalt = contenido + Salt;
            //cremaos un objeto grande para cifrar 
            SHA256 managed = SHA256.Create(); //creamos el objeto
            //declaramos nuestro byte de salida
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidoSalt);
            //ciframos el contenido con n iteraciones
            for (int i = 1; i <=22; i++)
            {
                //realizamos el cifrado sobre el cifrado
                salida = managed.ComputeHash(salida);
            }
            //debemos liberar la memoria para no tostar el equipo
            managed.Clear();
            string resultado = encoding.GetString(salida);//esto solo para ver como lo saca, nunca se pondría
            return resultado;
        }

        //creamos un metodo static para cifrar un contenido -> devolvemos el texto cifrado
        public static string EncriptarTextoBase(string contenido)
        {
            //necesitamos un array de bites para convertir el contenido de entrada a byte
            byte[] entrada;

            //al cifrar el contenido, nos devuelve bytes[] de salida
            byte[] salida;

            //Necesitamos una clase que nos permite convertir de string a byte[] y viceversa
            UnicodeEncoding encoding = new UnicodeEncoding();
            //necesitamos un objeto para cifrar el contenido.
            SHA1 managed = SHA1.Create();
            //convertimos el contenido de entrada a byte[]
            entrada = encoding.GetBytes(contenido);
            //los objetos para cifrar contienen un metodo llamado ComputedHash que reciben un array de bytes e internamente, hacen cosas y devuelven otro array de bytes
            salida = managed.ComputeHash(entrada);
            //convertimos salida a String
            string resultado = encoding.GetString(salida);
            return resultado;
        }

    }
}
