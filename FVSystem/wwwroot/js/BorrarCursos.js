
function BorrarCurso(id) {
    var resultado = confirm("Esta seguro que desea borrar?");
    if (resultado) {
        $.post("/Curso/Eliminar/" + id, function (data) {
            window.location.reload(false);
        });
    }

}
