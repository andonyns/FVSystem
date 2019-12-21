
function borrarEstudiante(id) {
    var resultado = confirm("Esta seguro que desea borrar?");
    if (resultado) {
        $.post("/Estudiantes/Eliminar/" + id, function (data) {
            window.location.reload(false); 
        });
    }
    
}
