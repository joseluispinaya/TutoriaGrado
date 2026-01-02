
var table;

$('#btnBuscar').on('click', function () {
    buscarEst();
});

$('#btnGargar').on('click', function () {
    if (parseInt($("#txtIdEstudiante").val()) === 0) {
        toastr.warning("", "Debe buscar un estudiante");
        return;
    }
    cargarPreguntas();
});

$('#btnNuevo').on('click', function () {
    location.reload();
})

$('#btnImprim').on('click', function () {
    swal("Mensaje", "Falta implementar Imprimir.", "warning");
})

function buscarEst() {

    var nroCi = $("#txtNroci").val().trim();

    if (nroCi === "") {
        swal("Mensaje", "Ingrese número de CI", "warning");
        return;
    }

    $.ajax({
        type: "GET",
        url: "https://localhost:7214/api/Preguntas/" + nroCi,
        dataType: "json",
        beforeSend: function () {
            $("#loaddet").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loaddet").LoadingOverlay("hide");

            //console.log("Respuesta completa:", response);
            //console.log("Estudiante:", response.result);

            if (response.wasSuccess) {
                $("#txtIdEstudiante").val(response.result.id);
                $("#texnomn").text(response.result.nombreCompleto);
                $("#texcorreo").text(response.result.correo);
                //console.log("Unidad:", response.result.unidadEducativa);
            } else {
                swal("Mensaje", response.message, "warning");
            }
        },
        error: function (xhr) {
            $("#loaddet").LoadingOverlay("hide");
            console.log("Error:", xhr.status, xhr.responseText);
        }
    });
}

let listaPreguntas = [];

function cargarPreguntas() {

    $.ajax({
        type: "GET",
        url: "https://localhost:7214/api/Preguntas/preguntas",
        dataType: "json",
        success: function (response) {

            if (!response.wasSuccess) {
                swal("Mensaje", response.message, "warning");
                return;
            }

            listaPreguntas = response.result;
            renderizarPreguntas();
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function renderizarPreguntas() {

    let contenedor = $("#contenedorPreguntas");
    contenedor.empty();

    let fila = $('<div class="row"></div>');

    listaPreguntas.forEach((item, index) => {

        let columna = $(`
            <div class="col-md-6">
                <div class="card m-b-2">
                    <div class="card-body p-2">

                        <div class="form-group">
                            <label class="fw-bold">
                                ${index + 1}. ${item.texto}
                            </label>
                            <input type="text" class="form-control input-sm respuesta" data-id="${item.id}">
                        </div>

                    </div>
                </div>
            </div>
        `);

        fila.append(columna);
    });

    contenedor.append(fila);
}

function obtenerRespuestas() {
    let respuestas = [];

    $(".respuesta").each(function () {
        respuestas.push({
            preguntaId: $(this).data("id"),
            textoRespuesta: $(this).val()
        });
    });

    return respuestas;
}

$("#btnEnviar").on("click", function () {

    $('#btnEnviar').prop('disabled', true);

    if (parseInt($("#txtIdEstudiante").val()) === 0) {
        toastr.warning("", "Debe buscar un estudiante");
        $('#btnEnviar').prop('disabled', false);
        return;
    }

    let valido = true;

    $(".respuesta").each(function () {
        let valor = $(this).val().trim();

        if (valor === "") {
            valido = false;
            $(this).addClass("respuesta-error");
        } else {
            $(this).removeClass("respuesta-error");
        }
    });

    if (!valido) {
        swal("Mensaje", "Debe responder todas las preguntas antes de continuar.", "warning");
        $('#btnEnviar').prop('disabled', false);
        return;
    }

    let payload = {
        estudianteId: parseInt($("#txtIdEstudiante").val(), 10),
        respuestas: obtenerRespuestas()
    };

    $.ajax({
        type: "POST",
        url: "https://localhost:7214/api/Respuestas/prueba",
        data: JSON.stringify(payload),
        contentType: "application/json",
        beforeSend: function () {
            $("#loaddet").LoadingOverlay("show");
        },
        success: function (response) {
            $("#loaddet").LoadingOverlay("hide");

            if (!response.wasSuccess) {
                swal("Mensaje", response.message, "warning");
                return;
            }
            $("#txtIdEstudiante").val("0");
            mostrarResultados(response.result);
            swal("Mensaje", response.message, "success");
        },
        error: function (err) {
            $("#loaddet").LoadingOverlay("hide");
            console.log(err);
            swal("Error", "Ocurrió un problema intente mas tarde.", "error");
        },
        complete: function () {
            $('#btnEnviar').prop('disabled', false);
        }
    });
});

function mostrarResultadosVer(data) {

    $("#bloquePreguntas").hide();
    $("#bloqueResultados").show();
    console.log("Resultado IA:", data);

}

function mostrarResultados(data) {

    $("#bloquePreguntas").hide();
    $("#bloqueResultados").show();

    if ($.fn.DataTable.isDataTable("#tbResults")) {
        $("#tbResults").DataTable().clear().rows.add(data.recomendaciones).draw();
        return;
    }

    $("#txtobservacion").val(data.observacionGeneral);

    $("#tbResults").DataTable({
        responsive: true,
        data: data.recomendaciones,
        columns: [
            { data: "carrera" },
            {
                data: "puntuacion",
                render: function (data, type, row) {
                    return data + " %";
                }
            },
            { data: "justificacion" }
        ],
        dom: "rt",
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

// fin