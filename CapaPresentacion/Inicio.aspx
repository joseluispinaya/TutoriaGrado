<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="CapaPresentacion.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    Bienvenido...
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title text-dark m-0">Card Default</h3>
                </div>
                <div class="card-body">

                    <div class="form-row align-items-end" id="loadda">
                        <div class="form-group col-sm-3">
                            <label for="cboCarrera">Carreras</label>
                            <select class="form-control form-control-sm" id="cboCarrera">
                                <option value="1">Ing. Sistemas</option>
                                <option value="2">Ing. Comercial</option>
                            </select>
                        </div>
                        <div class="form-group col-sm-7">
                            <label for="txtTitulo">Titulo de Proyecto</label>
                            <input type="text" class="form-control input-sm" id="txtTitulo">
                        </div>
                        <div class="form-group col-sm-2">
                            <button class="btn btn-info btn-block btn-sm" type="button" id="btnBuscar">
                                <i class="fas fa-search mr-2"></i>Consultar</button>
                        </div>
                    </div>
                    <hr />

                    <div class="row">
                        <div class="col-sm-12">

                            <h4 class="m-t-0 m-b-10">Preguntas para evaluar</h4>
                            <div class="form-group">
                                <label for="txtobservacion">Observacion General</label>
                                <textarea class="form-control" rows="4" id="txtobservacion"></textarea>
                            </div>

                            <div class="card m-b-2">
                                <div class="card-body p-2">
                                    <p class="fw-bold">¿Qué actividades disfrutas más en tu tiempo libre?</p>
                                    <input type="text" class="form-control input-sm">
                                </div>
                            </div>
                            <div class="card m-b-2">
                                <div class="card-body">
                                    <p class="fw-bold">¿Prefieres trabajar con personas, datos o máquinas?</p>
                                    <input type="text" class="form-control input-sm">
                                </div>
                            </div>
                            <div class="card m-b-2">
                                <div class="card-body">
                                    <p class="fw-bold">¿Te resulta satisfactorio ayudar a otras personas a resolver problemas?</p>
                                    <input type="text" class="form-control input-sm">
                                </div>
                            </div>
                            <div class="card m-b-2">
                                <div class="card-body p-2">
                                    <div class="form-group">
                                        <label class="fw-bold">¿Te resulta satisfactorio ayudar a otras personas a resolver problemas?</label>
                                        <input type="text" class="form-control input-sm">
                                    </div>
                                </div>
                            </div>

                            <%--<table id="tbResult" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>Docente</th>
                                        <th>Puntos</th>
                                        <th>Justificacion</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
    <script src="jsdev/Inicio.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
    <%--<script src="jsdev/Masterpa.js" type="text/javascript"></script>--%>
</asp:Content>
