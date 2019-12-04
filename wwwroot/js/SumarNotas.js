/* Funcion suma. */
function SumarAutomatico() {

    var asistencia = parseInt(document.getElementById('asistencia').value);
    var cotidiano = parseInt(document.getElementById('cotidiano').value);
    var proyecto1 = parseInt(document.getElementById('proyecto1').value);
    var proyecto2 = parseInt(document.getElementById('proyecto2').value);
    var proyectoFinal = parseInt(document.getElementById('proyectoFinal').value);

    document.getElementById('notaTotal').value = asistencia+ cotidiano+ proyecto1 +proyecto2+ proyectoFinal;

    
}
