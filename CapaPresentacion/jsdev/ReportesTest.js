
var table;

$(document).ready(function () {
    cargarListaReport();
});

function cargarListaReport() {

    if ($.fn.DataTable.isDataTable("#tbReport")) {
        $("#tbReport").DataTable().destroy();
        $('#tbReport tbody').empty();
    }

    table = $("#tbReport").DataTable({
        responsive: true,
        processing: true,
        ajax: {
            url: 'https://localhost:7214/api/Preguntas/reporte',
            type: "GET",
            dataType: "json",

            dataSrc: function (json) {
                if (json.wasSuccess) {
                    return json.result;
                } else {
                    swal("Mensaje", json.message, "warning");
                    return [];
                }
            },

            beforeSend: function () {
                $("#loadda").LoadingOverlay("show");
            },
            complete: function () {
                $("#loadda").LoadingOverlay("hide");
            }
        },
        columns: [
            { data: "nombreCompleto" },
            { data: "carreraRecomendada" },
            { data: "puntuacionString" },
            { data: "fechaString" },
            { data: "observacionGeneral" }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}