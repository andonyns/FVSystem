function borrarModulo(id) {
    var resultado = confirm("Esta seguro que desea borrar?");
    if (resultado) {
        $.post("/Modulo/Eliminar/" + id, function (data) {
            window.location.reload(false);
        });
    }

}
