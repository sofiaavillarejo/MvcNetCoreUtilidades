using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CifradoController : Controller
    {

        public IActionResult CifradoBasico()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoBasico(string contenido, string resultado, string accion)
        {
            //ciframos el contenido
            string response = HelperCryptography.EncriptarTextoBase(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            }else if (accion.ToLower() == "comparar")
            {
                //si el usuario quiere comparar, nos estará enviando el resultado para compararlo
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "Los datos no coinciden";
                }
                else
                {
                    ViewData["MENSAJE"] = "Contenidos iguales";
                }
            }
            return View();
        }
        public IActionResult CifradoEficiente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoEficiente(string contenido, string resultado, string accion)
        {
            if (accion.ToLower() == "cifrar")
            {
                string response = HelperCryptography.CifrarContenido(contenido, false); //con false le decimos que no queremos comparar, que queremos generar nuevo salt
                ViewData["TEXTOCIFRADO"] = response; 
                ViewData["SALT"] = HelperCryptography.Salt;//como hemos puesto publiv al metodo, aqui por curiosidad lo podemos ver
            }
            else if (accion.ToLower() == "comparar")
            {
                string respons = HelperCryptography.CifrarContenido(contenido, true); //coge el mismo salt que antes
                if (respons != resultado)
                {
                    ViewData["MENSAJE"] = "Los datos no son correctos";
                }else
                {
                    ViewData["MENSAJE"] = "Los datos son CORRECTOS";

                }
            }
            return View();
        }
    }
}
