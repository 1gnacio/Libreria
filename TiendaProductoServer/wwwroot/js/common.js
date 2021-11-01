window.MostrarToastr = (tipo, mensaje) => {
    if (tipo === "success") {
        toastr.success(mensaje, "Operacion exitosa");
    }

    if (tipo === "error") {
        toastr.error(mensaje, "Operacion fallida");
    }
}

window.MostrarSual = (tipo, mensaje) => {
    if (tipo === "success") {
        Swal.fire(
            'Notificacion exitosa!',
            mensaje,
            'success'
        )
    }

    if (tipo === "error") {
        Swal.fire(
            'Notificacion fallida!',
            mensaje,
            'error'
        )
    }
}

function MostrarModalConfirmacionEliminar() {
    $('#confirmacionEliminarModal').modal('show');
}

function OcultarModalConfirmacionEliminar() {
    $('#confirmacionEliminarModal').modal('hide');
}