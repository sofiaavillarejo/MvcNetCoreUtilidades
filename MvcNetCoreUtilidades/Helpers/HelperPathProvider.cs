using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcNetCoreUtilidades.Helpers
{
    //vamos a ofrecer en programacion una enumeracion con las carpetas de nuestr servidor
    public enum Folders
    {
        //aqui vamos incluyendo todas las caprtas que hayamos creado en nuestro servidor
        Images, facturas, Uploads, Temporal
    }
    
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        //sacar el url del server
        private IServer server;
        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.server = server;
        }

        public string MapPath(string FileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }else if (folder == Folders.facturas)
            {
                carpeta = "facturas";
            }else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";

            }else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }

            //cogemos la ruta de nuestro server
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, FileName);
            return path;
        }
        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            var adresses =
                this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = adresses.FirstOrDefault();
            string urlPath = serverUrl + "/" + carpeta + "/" + fileName;
            return urlPath;
        }

    }
}
