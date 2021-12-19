using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoCliente.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jSRuntime, string mensaje)
        {
            await jSRuntime.InvokeVoidAsync("MostrarToastr", "success", mensaje);
        }

        public static async ValueTask ToastrError(this IJSRuntime jSRuntime, string mensaje)
        {
            await jSRuntime.InvokeVoidAsync("MostrarToastr", "error", mensaje);
        }
    }
}
