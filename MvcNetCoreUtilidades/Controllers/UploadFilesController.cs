using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        //private IWebHostEnvironment hostEnvironment;
        private HelperPathProvider helper;
        //public UploadFilesController(IWebHostEnvironment hostEnvironment)
        //{
        //    //this.hostEnvironment = hostEnvironment;
        //}

        public UploadFilesController(HelperPathProvider helper)
        {
            this.helper = helper;
        }
        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            //NECESITAMOS LA RUTA A NUESTRO WWWROOT DEL SERVER
            //string rootFolder = this.hostEnvironment.WebRootPath;
            //almacenamos el fichero en los elementos temporales
            //string tempFolder = Path.GetTempPath(); //ruta temporal del sistema (CON LA RUTA DEL SERVIDOR YA NO NOS HACE FALTA)
            //nombre del fichero
            string fileName = fichero.FileName;

            //cuando habamos de ficheros y de rutas de sistema, pensamos en -> C:\miruta\...
            //pero estamos en net core -> podemos montar el servidor donde queramos
            //las rutas de ficheros no debemos escribirlas, tenemos que gerenarlas con el sistema donde estoy trabajando
            string path = this.helper.MapPath(fileName, Folders.Images);
            string urlPath = this.helper.MapUrlPath(fileName, Folders.Images);

            //string path = Path.Combine(rootFolder, "uploads", fileName);//quitamos la ruta temporal y metemmos la del servdior
            //para subir el fichero, se usa Stream con IFormFile
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["URL"] = urlPath;
            return View();
        }
    }
}
