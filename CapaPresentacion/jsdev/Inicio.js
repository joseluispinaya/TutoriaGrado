
var table;

$('#btnBuscar').on('click', function () {
    cargarListaRecomen()
})

function cargarListaRecomen() {
    if ($.fn.DataTable.isDataTable("#tbResult")) {
        $("#tbResult").DataTable().destroy();
        $('#tbResult tbody').empty();
    }
    var request = {
        IdCarrera: parseInt($("#cboCarrera").val()),
        Titulo: $("#txtTitulo").val()
    };

    table = $("#tbResult").DataTable({
        responsive: true,
        processing: true,
        "ajax": {
            "url": 'Inicio.aspx/ListaRecomendados',
            "type": "POST", // Cambiado a POST
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            "data": function () {
                return JSON.stringify(request);
            },
            "dataSrc": function (json) {
                if (json.d.Estado) {
                    return json.d.Data; // Asegúrate de que esto apunta al array de datos
                } else {
                    swal("Mensaje", json.d.Mensaje, "warning");
                    return [];
                }
            },
            "beforeSend": function () {
                $("#loadda").LoadingOverlay("show");
            },
            "complete": function () {
                $("#loadda").LoadingOverlay("hide");
            }
        },
        "columns": [
            { "data": "NombreDocente" },
            { "data": "PuntajeAfinidad" },
            { "data": "Justificacion" }
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

function cargarListaRecomenori() {

    var request = {
        IdCarrera: parseInt($("#cboCarrera").val()),
        Titulo: $("#txtTitulo").val()
    };

    $.ajax({
        type: "POST",
        url: "Inicio.aspx/ListaRecomendados",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $("#loadda").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loadda").LoadingOverlay("hide");
            if (response.d.Estado) {
                var lista = response.d.Data;
                console.log(lista);

            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loadda").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
}

function cargarLista() {

    var request = {
        IdCarrera: parseInt($("#cboCarrera").val())
    };

    $.ajax({
        type: "POST",
        url: "Inicio.aspx/ListaDocentes",
        data: JSON.stringify(request),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function () {
            $("#loadda").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loadda").LoadingOverlay("hide");
            if (response.d.Estado) {
                var lista = response.d.Data;
                console.log(lista);

            } else {
                swal("Mensaje", response.d.Mensaje, "warning");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#loadda").LoadingOverlay("hide");
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
}

//fin