using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TiendaProductoServer.Service.IService;

namespace TiendaProductoServer.Service
{
    public class ArchivoUpload : IArchivoUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArchivoUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool EliminarArchivo(string nombreArchivo)
        {
            try
            {
                var ruta = $"{_webHostEnvironment.WebRootPath}\\ImagenesLibro\\{nombreArchivo}";
                if (File.Exists(ruta))
                {
                    File.Delete(ruta);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<string> UploadArchivo(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var nombreArchivo = Guid.NewGuid().ToString() + fileInfo.Extension;
                var carpetaDirectorio = $"{_webHostEnvironment.WebRootPath}\\ImagenesLibro";
                var ruta = Path.Combine(_webHostEnvironment.WebRootPath, "ImagenesLibro", nombreArchivo);
                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                if (!Directory.Exists(carpetaDirectorio))
                {
                    Directory.CreateDirectory(carpetaDirectorio);
                }
                await using(var fs = new FileStream(ruta, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }
                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                var rutaCompleta = $"{url}ImagenesLibro/{nombreArchivo}";
                return rutaCompleta;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
