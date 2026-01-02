<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="ModeloLm.aspx.cs" Inherits="CapaPresentacion.ModeloLm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .sin-margin-bottom {
            margin-bottom: 0;
        }

        .titull {
            margin-top: 10px;
            margin-bottom: 5px;
        }

        .respuesta-error {
            border: 2px solid #dc3545 !important;
            background-color: #f8d7da !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    Modelo de Vocacion
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row" id="loaddet">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <%--<h4 class="m-t-0 m-b-15">Detalle del Estudiante</h4>--%>
                    <input id="txtIdEstudiante" value="0" type="hidden" />
                    <div class="row">
                        <div class="col-lg-7">
                            <div class="form-inline m-b-10">
                                <div class="form-group">
                                    <label class="sr-only" for="textlab">Estudiante</label>
                                    <label class="fw-bold" id="textlab">Estudiante</label>
                                </div>

                                <div class="form-group m-l-10">
                                    <label class="sr-only" for="txtNroci">Nro CI</label>
                                    <input type="text" class="form-control input-sm" id="txtNroci" placeholder="Nro de CI">
                                </div>
                                <button type="button" class="btn btn-success btn-sm m-l-10" id="btnBuscar"><i class="fas fa-search mr-2"></i>Consultar</button>
                            </div>
                            <%--<h4 class="m-t-0 m-b-10">Detalle del Estudiante</h4>--%>
                            <div class="row">
                                <div class="col-sm-6">
                                    <strong>Nombres:</strong>
                                    <label class="sin-margin-bottom" id="texnomn"></label>
                                </div>
                                <div class="col-sm-6">
                                    <strong>Correo:</strong>
                                    <label class="sin-margin-bottom" id="texcorreo"></label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="form-row align-items-end">
                                <div class="form-group col-sm-4">
                                    <button class="btn btn-info btn-block btn-sm" type="button" id="btnGargar">
                                        <i class="fas fa-address-book mr-2"></i>Iniciar Test</button>
                                </div>
                                <div class="form-group col-sm-4">
                                    <button class="btn btn-success btn-block btn-sm" type="button" id="btnNuevo">
                                        <i class="fas fa-address-book mr-2"></i>Nuevo Test</button>
                                </div>
                                <div class="form-group col-sm-4">
                                    <button class="btn btn-danger btn-block btn-sm" type="button" id="btnImprim">
                                        <i class="fas fa-print mr-2"></i>Exportar Test</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <hr />

                    <div class="row">
                        <div class="col-sm-12">

                            <div id="bloquePreguntas">
                                <h4 class="m-t-0 m-b-10">Preguntas para evaluar</h4>
                                <div id="contenedorPreguntas"></div>
                                <button id="btnEnviar" class="btn btn-primary btn-sm m-t-3">Enviar respuestas</button>
                            </div>

                            <div id="bloqueResultados" style="display: none;">
                                <h4 class="m-t-0 m-b-10">Resultados Vocacional Generado por el Modelo Inteligente</h4>

                                <div class="form-group">
                                    <label for="txtobservacion">Observacion General</label>
                                    <textarea class="form-control" rows="4" id="txtobservacion"></textarea>
                                </div>
                                <hr />
                                <table id="tbResults" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr>
                                            <th>Carreras</th>
                                            <th>Puntos</th>
                                            <th>Justificación</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
    <script src="jsdev/ModeloLm.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
</asp:Content>
