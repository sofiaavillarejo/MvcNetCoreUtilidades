using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemoriaPersonalizada(int? tiempo)
        {
            if (tiempo == null)
            {
                tiempo = 60;
            }
            string fecha = DateTime.Now.ToLongDateString() + "--" + DateTime.Now.ToLongTimeString();
            //debemos preguntar si existe algo en cache o no
            if (this.memoryCache.Get("FECHA") == null) //preguntamos si existe fecha
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
                //no existe en cache todavia, asi que lo almacenamos
                this.memoryCache.Set("FECHA", fecha, options);
                ViewData["MENSAJE"] = "Fecha almacenada";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA"); //guardamos la fecha para ver por pantalla si ha cambiado
            }
            else
            {
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Fecha recuperada";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }

        [ResponseCache(Duration =60, Location =ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida() {
            string fecha = DateTime.Now.ToLongDateString() + "--" + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
    }
}
