using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoServer.Service.IService
{
    public interface IArchivoUpload
    {
        Task<string> UploadArchivo(IBrowserFile file);
        bool EliminarArchivo(string nombreArchivo);
    }
}
