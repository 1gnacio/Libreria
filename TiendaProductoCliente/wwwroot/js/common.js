window.MostrarToastr = (tipo, mensaje) => {
    if (tipo === "success") {
        toastr.success(mensaje, "Operacion exitosa");
    }

    if (tipo === "error") {
        toastr.error(mensaje, "Operacion fallida");
    }
}