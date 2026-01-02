<%@ Page Title="" Language="C#" MasterPageFile="~/HomePage.Master" AutoEventWireup="true" CodeBehind="ReportesTest.aspx.cs" Inherits="CapaPresentacion.ReportesTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    Reportes de Test de Vocacion
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="card" id="loadda">
                <div class="card-header bg-primary py-2 px-4">
                    <h3 class="card-title m-0"><i class="fas fa-bars"></i> Reporte Test Estudiantes</h3>
                </div>
                <div class="card-body">
                    <div class="row justify-content-center mb-4">
                        <button type="button" id="btnReporte" class="btn btn-success btn-sm"><i class="fas fa-print mr-2"></i>Exportar Reporte</button>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">

                            <table id="tbReport" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>Estudiante</th>
                                        <th>Carrera</th>
                                        <th>Puntos</th>
                                        <th>Fecha</th>
                                        <th>Detalle</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
    <script src="jsdev/ReportesTest.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>
</asp:Content>
