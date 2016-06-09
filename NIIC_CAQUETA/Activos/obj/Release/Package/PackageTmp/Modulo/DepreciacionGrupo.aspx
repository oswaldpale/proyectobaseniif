<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepreciacionGrupo.aspx.cs" Inherits="Activos.Modulo.DepreciacionGrupo" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<script runat="server">
    
    
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DEPRECIACION POR SUBCLASE</title>
    <script src="JS/Blob.js"></script>
    <script src="JS/FileSaver.js"></script>
    <script src="JS/papaparse.js"></script>
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>

    <style type="text/css">
        /**/
        #unlicensed {
            display: none !important;
        }
    </style>
    <script type="text/javascript">
        
        var descargaCSV = function (url) {
            App.w1.show();            
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {                    
                    var myArr = JSON.parse(xmlhttp.responseText);
                    myFunction(myArr);                    
                }
            }
            xmlhttp.open("GET", url, true);
            xmlhttp.send();
        };

        function myFunction(arr) {
            var BOM = "\uFEFF";
            var csv = BOM + Papa.unparse(arr, { delimiter: ";" });
            var blob = new Blob([csv], { type: "text/csv;charset=UTF-8" });
            saveAs(blob, "Depreciacion.csv");
            App.w1.hide();
        }



        var saveData = function () {

            App.hidden1.setValue(Ext.encode(App.gridLista.getRowsValues({ selectedOnly: false })));
        };


        var prepare = function (grid, command, record, row, col, value) {

            if (record.get('tipodepreciacion') == 'LINEA RECTA' && command.command == "Cambiar") {
                command.hidden = true;
                command.hideMode = "visibility";
            }
        };

        var onCommand = function (column, command, record, recordIndex, cellIndex) {
            App.winDetalle.show();
            App.txtTipo.reset();
            App.txtComponente.reset();
            App.txtCantidad.reset();
            App.txtTipo.setValue(record.get('tipodepreciacion').toString());
            App.txtComponente.setValue(record.get('descripcioncomponente').toString());
            App.hiddencomponente.setValue(record.get('codigocomponente').toString());
            App.GridPanel1.getView().getSelectionModel().selectByPosition({ row: recordIndex }, false);
        }

        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value);
        };

        var pctChange = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value);
        };

    </script>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="95000000" />
    <ext:TaskManager ID="TaskManager1" runat="server">
        <Tasks>
            <ext:Task
                TaskID="longactionprogress"
                AutoRun="false"
                OnStart="#{BANULAR}.setDisabled(true);#{btn_estado}.setDisabled(true);#{btn_procesar}.setDisabled(true);#{btnCargarPlano}.setDisabled(true);#{gridLista}.setDisabled(true);#{Button1}.setDisabled(true);#{btnGrabar}.setDisabled(true);"
                OnStop="#{BANULAR}.setDisabled(false);#{btn_estado}.setDisabled(false);#{gridLista}.setDisabled(false);#{Button1}.setDisabled(false);#{btnGrabar}.setDisabled(false);#{Button3}.setDisabled(false);">

                <DirectEvents>
                    <Update OnEvent="RefreshProgress" />
                </DirectEvents>
              
            </ext:Task>
        </Tasks>
    </ext:TaskManager>
    <ext:Store ID="Store_clase" runat="server" PageSize="8">
        <Sorters>
            <ext:DataSorter Property="codigoclase" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model15" runat="server" IDProperty="codigoclase">
                <Fields>
                    <ext:ModelField Name="codigoclase" />
                    <ext:ModelField Name="descripcionclase" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="Store_subclase" runat="server" PageSize="8">
        <Sorters>
            <ext:DataSorter Property="codigosubclase" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model3" runat="server" IDProperty="codigosubclase">
                <Fields>
                    <ext:ModelField Name="codigosubclase" />
                    <ext:ModelField Name="descripcionsubclase" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>

    <form id="form1" runat="server">
        <ext:Hidden ID="HTIPOGUARDAR" Text="1" runat="server" />

        <ext:Viewport ID="Viewport1" runat="server">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Frame="true">

                    <Items>
                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:Label ID="Label1" runat="server" Text="Fecha Revision" Width="150" Enabled="true" Margins="10 0 0 10" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
                            </LayoutConfig>
                            <Items>
                                <ext:Hidden ID="hidden1" runat="server" />
                                <ext:Hidden ID="idclase" runat="server" />
                                <ext:Hidden ID="clase" runat="server" />
                                <ext:Hidden ID="idsubclase" runat="server" />
                                <ext:Hidden ID="subclase" runat="server" />
                                <ext:Hidden ID="codActivo" runat="server" />
                                <ext:Hidden ID="nombreActivo" runat="server" />
                                <ext:Hidden ID="idplaca" runat="server" />
                                <ext:Hidden ID="Cantidad" runat="server" />
                                <ext:Hidden ID="estadoPlano" runat="server" />
                                <ext:Hidden ID="cantPlano" runat="server" />
                                <ext:Hidden ID="HTIPOBAR" runat="server" Text="Registrar" />

                                <ext:Hidden ID="estadoProcDe" runat="server" />
                                <ext:Hidden ID="EstadoUnidadUtilizada" runat="server" />

                                <ext:DateField ID="dfd_fecha" runat="server" Width="150" AllowBlank="false">
                                    <DirectEvents>
                                        <Change OnEvent="GridComodato_SelectRow1">
                                            <EventMask ShowMask="true" Target="Page" Msg="Cargando" />
                                        </Change>
                                    </DirectEvents>
                                </ext:DateField>
                                <ext:CalendarPanel ID="CalendarPanel1" runat="server" ActiveIndex="2"
                                    Border="false" ShowDayView="false" ShowMonthView="true" />
                                <ext:Button runat="server" ID="btnCargarPlano" Icon="DiskDownload" Disabled="true">
                                    <Listeners>
                                        <Click Handler=" App.win_cargarplano.show();" />

                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Container>

                        <ext:FieldSet ID="FieldSet3" runat="server" Title="Detalle" Height="450">
                            <Items>
                                <ext:Container runat="server" ID="Container7">

                                    <Items>
                                        <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" Plain="True" PaddingSpec="10 0 0" Hidden="false" UI="Primary">
                                            <LayoutConfig>
                                                <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
                                            </LayoutConfig>
                                            <TabBar>

                                                <ext:ProgressBar ID="Progress1" runat="server" Flex="7" Margins="5 0 0 5" />

                                                <ext:Button runat="server"
                                                    ID="btn_procesar"
                                                    Icon="CogGo"
                                                    Text="PROCESAR DEPRECIACIÓN"
                                                    Disabled="true"
                                                    Width="210"
                                                    Margins="5 5 0 5"
                                                    OnDirectClick="IniciarProcesoEjecucion" />

                                            </TabBar>

                                            <Items>

                                                <ext:Panel ID="Panel11" runat="server" Title="Lista de Activos" UI="Info">

                                                    <Items>
                                                        <ext:GridPanel
                                                            runat="server"
                                                            Height="380"
                                                            Flex="1"
                                                            ID="gridLista"
                                                            Border="true">
                                                            <Store>
                                                                <ext:Store ID="store_listaActivos" runat="server">
                                                                    <Model>
                                                                        <ext:Model runat="server" IDProperty="idActivo">
                                                                            <Fields>
                                                                                <ext:ModelField Name="FechaUdep" />
                                                                                <ext:ModelField Name="idActivo" />
                                                                                <ext:ModelField Name="fechaUDep" />
                                                                                <ext:ModelField Name="NoCompra" />
                                                                                <ext:ModelField Name="Nombre" />
                                                                                <ext:ModelField Name="placa" />
                                                                                <ext:ModelField Name="CostoInicial" />
                                                                                <ext:ModelField Name="revaluacion" />
                                                                                <ext:ModelField Name="deterioro" />
                                                                                <ext:ModelField Name="residual" />
                                                                                <ext:ModelField Name="vr_dep_mes" />
                                                                                <ext:ModelField Name="DepAcum" />
                                                                                <ext:ModelField Name="baseDepreciacion" />
                                                                                <ext:ModelField Name="importe" />

                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                            <ColumnModel runat="server">
                                                                <Columns>
                                                                    <ext:Column ID="Column24" runat="server" DataIndex="FechaUdep" Header="Fecha Dep" Width="100">
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column4" runat="server" DataIndex="placa" Header="Placa" Flex="1">
                                                                        <HeaderItems>
                                                                            <ext:TextField ID="TextField1" runat="server" FieldCls="text-center">
                                                                                <Listeners>
                                                                                    <Change Handler="this.up('grid').applyFilter();" />
                                                                                </Listeners>
                                                                                <Plugins>
                                                                                    <ext:ClearButton ID="ClearButton1" runat="server" />
                                                                                </Plugins>
                                                                            </ext:TextField>
                                                                        </HeaderItems>
                                                                    </ext:Column>

                                                                    <ext:Column ID="Column1" runat="server" DataIndex="Nombre" Header="Activo"
                                                                        Flex="2">
                                                                        <HeaderItems>
                                                                            <ext:TextField ID="TextField2" runat="server" FieldCls="text-center">
                                                                                <Listeners>
                                                                                    <Change Handler="this.up('grid').applyFilter();" />
                                                                                </Listeners>
                                                                                <Plugins>
                                                                                    <ext:ClearButton ID="ClearButton2" runat="server" />
                                                                                </Plugins>
                                                                            </ext:TextField>
                                                                        </HeaderItems>
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column3" runat="server" DataIndex="CostoInicial" Header="Costo Inicial" Flex="1">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column5" runat="server" DataIndex="revaluacion" Header="Revaluación" Flex="1">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column2" runat="server" DataIndex="deterioro" Header="Deterioro" Flex="1">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column6" runat="server" DataIndex="residual" Header="Residual" Flex="1">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>

                                                                    <ext:Column ID="Column39" runat="server" DataIndex="vr_dep_mes" Header="Dep Mes" Flex="1">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column9" runat="server" DataIndex="DepAcum" Header="Dep Acumulada" Width="115">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column7" runat="server" DataIndex="baseDepreciacion" Header="Base Depreciable" Width="125">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column13" runat="server" DataIndex="importe" Header="Importe Libros" Flex="1">
                                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                    </ext:Column>
                                                                </Columns>
                                                            </ColumnModel>
                                                            <BottomBar>
                                                                <ext:PagingToolbar ID="PagingToolbar2" runat="server" HideRefresh="true" />
                                                            </BottomBar>
                                                        </ext:GridPanel>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:TabPanel>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                    <FooterBar>
                        <ext:Toolbar runat="server" ID="Toolbar1">
                            <Items>

                                <ext:Button runat="server" ID="BANULAR" Text="ANULAR" Icon="Delete" Scale="Small" Width="90">
                                    <Listeners>
                                        <Click Handler="App.WANULACIONMESDEPRECIACION.show();App.direct.ConsultarUltimaDepreciacion();" />
                                    </Listeners>
                                </ext:Button>

                                <ext:ToolbarFill />
                                    <ext:CycleButton Width="125" ID="btn_estado" runat="server" ShowText="true" Margins="5 0 0 5" Disabled="true"  Hidden="true" 
                                PrependText="">
                                <Menu>
                                    <ext:Menu ID="Menu3" runat="server" >
                                        <Items>
                                            <ext:CheckMenuItem ID="CheckMenuItem_Activo" runat="server" Icon="PageExcel" Text="EXPORTAR:" Checked="true" Border="true">
                                                </ext:CheckMenuItem>
                                            <ext:Button runat="server" ID="Button1" Text="REPORTE PARTE 1"  AutoPostBack="true"    Icon="PageExcel" Scale="Small" Hidden="false" />
                                              <ext:Button runat="server" ID="Button2" Text="REPORTE PARTE 2" AutoPostBack="true" Icon="PageExcel" Scale="Small" Hidden="false" />
                                           
                                        </Items>
                                       
                                    </ext:Menu>
                                </Menu>
                            </ext:CycleButton>

                                <ext:Button runat="server" ID="Button3" Text="REPORTE" Icon="PageExcel" Scale="Small" Hidden="false" Disabled="true">
                                    <Listeners>
                                        <Click Handler="App.direct.Excel();" />
                                    </Listeners>
                                </ext:Button>
                                  <ext:Button runat="server" ID="BGUARDARFALT" Text="CargarDatos" Icon="Disk" Scale="Small" Width="120" Hidden="true"  >
                                    <Listeners>
                                        <Click Handler=" App.direct.formarSql();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" ID="btnGrabar" Text="GUARDAR" Icon="Disk" Scale="Small" Width="120" Hidden="true">
                                    <Listeners>
                                        <Click Handler="App.direct.guardar();" />
                                    </Listeners>
                                </ext:Button>
                                 
                                                <ext:Button runat="server"
                                                    ID="Button5"
                                                    Icon="CogGo"
                                                    Text="GUARDAR"
                                                    Width="150"
                                                    Margins="5 5 0 5"
                                                    OnDirectClick="IniciarProcesoGuardar" />
                                

                            </Items>
                        </ext:Toolbar>
                    </FooterBar>

                </ext:FormPanel>
                <ext:Window ID="win_cargarplano" runat="server" Title="Ingreso De Unidades Utilizadas" Hidden="true" CloseAction="Hide" Width="650" Height="500">
                    <Items>
                        <ext:Panel runat="server" Padding="5">
                            <Items>
                                <ext:FormPanel
                                    ID="FormPanel3"
                                    runat="server">

                                    <Defaults>
                                        <ext:Parameter Name="anchor" Value="100%" Mode="Value" />
                                        <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
                                        <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
                                    </Defaults>

                                    <Items>
                                        <ext:FieldSet runat="server" Title="Cargar Archivo" Padding="5">
                                            <Items>
                                                <ext:Panel ID="Panel5" runat="server">
                                                    <Items>
                                                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" Width="500">
                                                            <Items>
                                                                <ext:FileUploadField
                                                                    ID="FileUploadField1"
                                                                    runat="server" AllowBlank="false"
                                                                    EmptyText="Seleccione un archivo..."
                                                                    FieldLabel="Archivo"
                                                                    ButtonText="" AnchorHorizontal="100%" Width="440"
                                                                    Icon="PageGo" Margins="10 0 0 10">
                                                                    <Listeners>
                                                                        <Change Handler="if(/.txt/.test(this.getValue()) || /.csv/.test(this.getValue()) ){ App.SaveButton.setDisabled(false);}else { App.SaveButton.setDisabled(true); alert('Archino no válido, solo se perimiten archivos .txt o .csv'); #{win_cargarplano..}.getForm().reset();}" />
                                                                    </Listeners>

                                                                </ext:FileUploadField>

                                                            </Items>
                                                        </ext:Container>
                                                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" Width="450">
                                                            <Items>
                                                                <ext:Label runat="server" Text="Columnas separadas por:" Flex="1" Margins="12 0 0 10" />
                                                                <ext:TextField runat="server" ID="txt_separador_c" MaxLengthText="1" MaxLength="1" Text=";" EnforceMaxLength="true" Width="30" Margins="10 0 0 0" />
                                                                <ext:Label runat="server" Text="Filas separadas por:" Flex="1" Margins="12 0 0 10" />
                                                                <ext:TextField runat="server" ID="txt_separador_f" MaxLengthText="2" MaxLength="2" Text="\n" ReadOnly="true" Selectable="false"
                                                                    EnforceMaxLength="true" Width="30" Margins="10 0 0 0" />
                                                                <ext:Button ID="SaveButton" runat="server" Disabled="true" Margins="10 0 0 10" Icon="PlayGreen">
                                                                    <Listeners>
                                                                        <Click Handler=" var s=App.GridPanelIngresoUnidades.getSelectionModel(); App.direct.CargarArchivoIngresoUnidades(App.GridPanelIngresoUnidades.getRowsValues());" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Container>

                                                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:Label runat="server" Html="<font size='2'>Observación: </font>" Width="100" Margins="12 0 0 10" />
                                                                <ext:Label runat="server" Html="<font color ='green'> El plano debe tener el siguiente orden: IDActivo, IDComponente, Unidades Utilizadas </font>" Flex="1" Margins="12 0 0 10" />

                                                            </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:Panel ID="Panel3" runat="server" Flex="1">
                                            <Items>
                                                <ext:GridPanel ID="GridPanelIngresoUnidades" runat="server" Flex="1" Margins="10 0 0 10" Border="true" Height="280">
                                                    <Store>
                                                        <ext:Store ID="storeIngresoUnidades" runat="server" GroupField="Activo">
                                                            <Model>
                                                                <ext:Model ID="Model5" runat="server" IDProperty="IdActivo">
                                                                    <Fields>
                                                                        <ext:ModelField Name="IdActivo" />
                                                                        <ext:ModelField Name="Activo" />
                                                                        <ext:ModelField Name="IdComponente" />
                                                                        <ext:ModelField Name="Componente" />
                                                                        <ext:ModelField Name="UnidadesDepreciadas" />
                                                                        <ext:ModelField Name="TipoDepreciacion" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel>
                                                        <Columns>

                                                            <ext:Column ID="Column46" runat="server" Text="" DataIndex="Activo"
                                                                Width="210" />
                                                            <ext:Column ID="Column37" runat="server" Text="Cod Componente" DataIndex="IdComponente"
                                                                Width="120" />
                                                            <ext:Column ID="Column47" runat="server" Text="Nombre Componente" Width="170" DataIndex="Componente">
                                                            </ext:Column>
                                                            <ext:Column ID="Column48" runat="server" Text="Tipo Depreciación" Width="200" DataIndex="TipoDepreciacion">
                                                            </ext:Column>
                                                            <ext:Column ID="Column49" runat="server" DataIndex="UnidadesDepreciadas" Header="Unidades Utilizadas"
                                                                Width="130">
                                                                <Editor>
                                                                    <ext:NumberField runat="server" AllowBlank="false" MinValue="0" />
                                                                </Editor>
                                                                <Renderer Fn="change" />
                                                            </ext:Column>
                                                        </Columns>
                                                    </ColumnModel>

                                                    <Features>
                                                        <ext:Grouping ID="Grouping1" runat="server" GroupHeaderTplString="{name}" HideGroupedHeader="true"
                                                            EnableGroupingMenu="false" />
                                                    </Features>
                                                    <SelectionModel>
                                                        <ext:CellSelectionModel runat="server" />
                                                    </SelectionModel>
                                                    <Plugins>
                                                        <ext:CellEditing runat="server">
                                                            <Listeners>
                                                                <Edit Handler="var r = App.GridPanelIngresoUnidades.getStore();
                                                                    r.commitChanges(); App.BtnGuardarUnidadesUtilizadas.setDisabled(false);" />
                                                            </Listeners>
                                                        </ext:CellEditing>
                                                    </Plugins>
                                                    <BottomBar>
                                                        <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="10" HideRefresh="true">
                                                        </ext:PagingToolbar>
                                                    </BottomBar>
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Panel>
                                    </Items>

                                    <Listeners>
                                        <ValidityChange Handler="#{SaveButton}.setDisabled(!valid);" />
                                    </Listeners>
                                    <Buttons>
                                        <ext:Button ID="BtnGuardarUnidadesUtilizadas" runat="server" Text="Guardar">
                                            <Listeners>
                                                <Click Handler="App.SaveButton.setDisabled(true);
                                                            App.direct.ValidarCamposUnidadesDepreciadas(App.GridPanelIngresoUnidades.getRowsValues());" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button runat="server" Text="Cerrar">
                                            <Listeners>
                                                <Click Handler="
                                                               if(App.BtnGuardarUnidadesUtilizadas.disabled)
                                                                  {#{win_cargarplano}.hide();}
                                                               else{
                                                                 Ext.Msg.show({
                                                                 title: 'Validar Cambios!!..',
                                                                 msg: 'Falta Guardar los cambios Realizados ',
                                                                 width: 300,
                                                                 buttons: Ext.Msg.OK,
                                                                 icon: Ext.window.MessageBox.INFO
                                                                 });
                                                                 }
                                                                 " />
                                            </Listeners>
                                        </ext:Button>
                                    </Buttons>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Window>

                <ext:Window ID="win_trazabilidad" runat="server" Title="TRAZABILIDAD"
                    Hidden="true" Closable="true" Layout="FitLayout" Width="1200" Height="550">
                    <Items>
                        <ext:FormPanel
                            ID="BasicForm"
                            runat="server">
                            <FieldDefaults LabelAlign="Right" LabelWidth="50" MsgTarget="Side" />
                            <Items>
                                <ext:Panel ID="Panel2" runat="server" Flex="1">
                                    <Items>
                                        <%--- grilla historial de la depreciacion-----%>
                                        <ext:GridPanel ID="GridPanel2" runat="server"
                                            Height="480" Margins="10 10 0 10" Border="true">
                                            <Store>
                                                <ext:Store ID="Store1" runat="server" GroupField="TipoNorma">

                                                    <Model>
                                                        <ext:Model ID="Model2" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="FechaRegistro" />
                                                                <ext:ModelField Name="TipoNorma" />
                                                                <ext:ModelField Name="TipoDocumento" />
                                                                <ext:ModelField Name="IdActivo" />
                                                                <ext:ModelField Name="IdComponente" />
                                                                <ext:ModelField Name="Componente" />
                                                                <ext:ModelField Name="idtipodeprecion" />
                                                                <ext:ModelField Name="VidaUtilmes" />
                                                                <ext:ModelField Name="VidaUtil" />
                                                                <ext:ModelField Name="VidaUtilizada" />
                                                                <ext:ModelField Name="VidaRemanente" />
                                                                <ext:ModelField Name="VrRevaluacion" />
                                                                <ext:ModelField Name="VrDeterioro" />
                                                                <ext:ModelField Name="VrResidual" />
                                                                <ext:ModelField Name="DepreciacionMes" />
                                                                <ext:ModelField Name="DepreciacionAcumulada" />
                                                                <ext:ModelField Name="BaseDepreciable" />
                                                                <ext:ModelField Name="ImporteLibros" />
                                                                <ext:ModelField Name="VidaUtil" />
                                                                <ext:ModelField Name="VidaUtilizada" />
                                                                <ext:ModelField Name="VidaRemanente" />
                                                                <ext:ModelField Name="TipoDocumento" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel3" runat="server">
                                                <Columns>

                                                    <ext:Column ID="Column14" runat="server" TdCls="task" Text="Componente: " Sortable="true"
                                                        DataIndex="Componente" Hideable="false" Width="200" />
                                                    <ext:Column ID="Column18" runat="server" TdCls="task" Text="Tipo Documento" Sortable="true"
                                                        DataIndex="TipoDocumento" Hideable="false" Width="150" />
                                                    <ext:DateColumn runat="server" ID="Column88" DataIndex="FechaRegistro" Text="Fecha Registro" Width="130"
                                                        Format="yyyy-MM-dd" />
                                                    <ext:Column ID="Column40" runat="server" Width="130" Text="Tipo Norma" Sortable="true"
                                                        DataIndex="TipoNorma">
                                                    </ext:Column>
                                                    <ext:Column ID="Column11" runat="server" Width="130" Text="Vida Util" Sortable="true"
                                                        DataIndex="VidaUtil">
                                                    </ext:Column>
                                                    <ext:Column ID="Column12" runat="server" Width="130" Text="Vida Utilizada" Sortable="true"
                                                        DataIndex="VidaUtilizada">
                                                    </ext:Column>
                                                    <ext:Column ID="Column16" runat="server" Width="130" Text="Vida Remanente" Sortable="true"
                                                        DataIndex="VidaRemanente">
                                                    </ext:Column>
                                                    <ext:Column runat="server" Width="120" ID="Column17" Text="Revaluación" Sortable="false"
                                                        Groupable="false" DataIndex="VrRevaluacion">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>

                                                    <ext:Column runat="server" Width="120" ID="Column19" Text="Deterioro" Sortable="false"
                                                        Groupable="false" DataIndex="VrDeterioro">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column runat="server" Width="120" ID="Column20" Text="Residual" Sortable="false"
                                                        Groupable="false" DataIndex="VrResidual">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column runat="server" Width="120" Text="Depreciación mes" Sortable="false"
                                                        Groupable="false" DataIndex="DepreciacionMes">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column runat="server" Width="130" ID="Column22" Text="Depreciación Acum" Sortable="false"
                                                        Groupable="false" DataIndex="DepreciacionAcumulada">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column21" runat="server" Width="130" Text="Base Depreciable" Sortable="true"
                                                        DataIndex="BaseDepreciable">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column runat="server" Width="130" ID="Column23" Text="Importe Libros" Sortable="false"
                                                        Groupable="false" DataIndex="ImporteLibros">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                </Columns>
                                            </ColumnModel>
                                            <Features>
                                                <ext:Grouping ID="Group1" runat="server" GroupHeaderTplString="{name}" HideGroupedHeader="true"
                                                    EnableGroupingMenu="false" />
                                            </Features>
                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar3" runat="server">
                                                </ext:PagingToolbar>
                                            </BottomBar>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <FooterBar>
                                <ext:Toolbar runat="server" ID="asdas">
                                    <Items>
                                        <%-- <ext:Button runat="server" ID="btn_reporte" Text="REPORTE..." Icon="Printer" Scale="Small">
                                <Listeners>
                                    <Click Handler="App.direct.Informe_TrazabilidaNiic_RptDetalle();" />
                                </Listeners>
                            </ext:Button>--%>
                                        <ext:ToolbarFill />
                                        <ext:Button runat="server" ID="btn_cerrar" Text="Cerrar" Scale="Small">
                                            <Listeners>
                                                <Click Handler="App.win_trazabilidad.close();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </FooterBar>
                        </ext:FormPanel>
                    </Items>
                </ext:Window>
                <%-- Depreciacion temporal por componentes --%>
                <ext:Window ID="win_componente" runat="server" Title="Detalle por Componentes "
                    Hidden="true" Closable="true" Layout="FitLayout" Width="1200" Height="390">
                    <Items>
                        <ext:FormPanel
                            ID="FormPanel2"
                            runat="server">
                            <FieldDefaults LabelAlign="Right" LabelWidth="50" MsgTarget="Side" />
                            <Items>
                                <ext:Panel ID="Panel1" runat="server" Flex="1">
                                    <Items>
                                        <ext:GridPanel ID="gridDepTemp" runat="server" Flex="1" Margins="10 0 0 10" Border="true" Height="300">
                                            <Store>
                                                <ext:Store ID="store_depTem" runat="server">
                                                    <Model>
                                                        <ext:Model ID="Model1" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="id_activo" />
                                                                <ext:ModelField Name="id_componente" />
                                                                <ext:ModelField Name="id_tipo_depreciacion" />
                                                                <ext:ModelField Name="vida_util" Type="Float" />
                                                                <ext:ModelField Name="vida_util_remanente" Type="Float" />
                                                                <ext:ModelField Name="vida_util_utilizado" Type="Float" />
                                                                <ext:ModelField Name="Porcentaje_ci" />
                                                                <ext:ModelField Name="ajust_vr_residual" />
                                                                <ext:ModelField Name="ajust_vr_razonable" />
                                                                <ext:ModelField Name="ajust_vr_deterioro" />
                                                                <ext:ModelField Name="costo_inicial" />
                                                                <ext:ModelField Name="vr_dep_acumulada" />
                                                                <ext:ModelField Name="deterioro" />
                                                                <ext:ModelField Name="importeDepreciable" />
                                                                <ext:ModelField Name="vr_importe_libros" />
                                                                <ext:ModelField Name="base_deprec" />
                                                                <ext:ModelField Name="id_tipo_depreciacion" />
                                                                <ext:ModelField Name="vr_dep_mes" />
                                                                <ext:ModelField Name="unidad_dep" />
                                                                <ext:ModelField Name="nombre_depreciacion" />
                                                                <ext:ModelField Name="nombre_componente" />
                                                                <ext:ModelField Name="NombreNorma" />
                                                                <ext:ModelField Name="cantidadDias" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Sorters>
                                                        <ext:DataSorter Property="tipodepreciacion" Direction="DESC" />
                                                    </Sorters>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel>
                                                <Columns>
                                                    <ext:Column ID="Column26" runat="server" Text="Componentes" DataIndex="nombre_componente"
                                                        Width="250" />
                                                    <ext:Column ID="Column29" runat="server" Text="Tipo Norma" DataIndex="NombreNorma"
                                                        Width="210" />
                                                    <ext:Column ID="Column27" runat="server" Text="Tipo Depreciacion" Width="200" DataIndex="nombre_depreciacion">
                                                    </ext:Column>
                                                    <ext:Column ID="Column28" runat="server" DataIndex="vida_util" Header="Vida Util" Hidden="false"
                                                        Width="70" />
                                                    <ext:Column ID="Column45" runat="server" DataIndex="unidad_dep" Header="Unidades Mes" Hidden="false"
                                                        Width="130" />
                                                    <ext:Column ID="Column25" runat="server" DataIndex="vida_util_utilizado" Header="Vida Util Ejecutada" Hidden="false"
                                                        Width="140" />

                                                    <ext:Column ID="Column10" runat="server" DataIndex="vida_util_remanente" Header="Vida Util Remanente" Hidden="false"
                                                        Width="140" />

                                                    <ext:Column ID="Column30" runat="server" DataIndex="costo_inicial" Header="Costo Inicial"
                                                        Width="120">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column31" runat="server" DataIndex="ajust_vr_residual" Header="Residual" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column32" runat="server" DataIndex="ajust_vr_deterioro" Header="Deterioro"
                                                        Width="130">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column33" runat="server" DataIndex="ajust_vr_razonable" Header="Revaluación"
                                                        Width="130">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column41" runat="server" DataIndex="base_deprec" Header="Base Depreciable "
                                                        Width="130">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column38" runat="server" DataIndex="vr_dep_mes" Header="Depreciación Mes"
                                                        Width="130">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column35" runat="server" DataIndex="vr_dep_acumulada" Header="Depreciación Acum"
                                                        Width="130">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column8" runat="server" DataIndex="vr_importe_libros" Header="Importe Libros"
                                                        Hidden="false" Width="140">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" Mode="Single" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="10">
                                                </ext:PagingToolbar>
                                            </BottomBar>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <FooterBar>
                                <ext:Toolbar runat="server" ID="Toolbar2">
                                    <Items>
                                        <ext:ToolbarFill />
                                        <ext:Button runat="server" ID="Button4" Text="Cerrar" Scale="Small">
                                            <Listeners>
                                                <Click Handler="App.win_componente.hide();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </FooterBar>
                        </ext:FormPanel>
                    </Items>
                </ext:Window>
                <ext:Window runat="server" ID="WANULACIONMESDEPRECIACION" Layout="FormLayout" Width="400" Height="250">
                    <Items>
                        <ext:FormPanel runat="server" Layout="FitLayout" Padding="5" >
                            <Items>
                                <ext:Container runat="server" >
                                    <Items>
                                        <ext:Label runat="server" Html="Concepto<font color ='green'>*</font>" Flex="1" Enabled="true" Width="350"
                                            Margins="5 0 0 5" />
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" >
                                    <Items>
                                        <ext:TextArea ID="TCONCEPTOANULAR" runat="server" Margins="5 5 15 5" AllowBlank="true" Width="380"
                                            Height="200" Flex="1" MaxLength="1999" EnforceMaxLength="true" Border="true">
                                        </ext:TextArea>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <FooterBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="BTNCANCELAR" runat="server" Text="CANCELAR">
                                    <Listeners>
                                        <Click Handler="App.WANULACIONMESDEPRECIACION.hide();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="BTNANULARMES" runat="server" Text="ANULAR" Icon="Delete" UI="Warning">
                                    <Listeners>
                                        <Click Handler="App.direct.AnularDepreciacionMes(App.TCONCEPTOANULAR.getValue());" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </FooterBar>
                </ext:Window>
                 <ext:Window ID="w1" runat="server" Closable="false" Draggable="false"  Resizable="false" Width="270" Hidden="true" Border="false" 
            Frame="false" Height="150" Modal="true" BodyPadding="10">
            <Items>
                <ext:Image runat="server" ImageUrl="../Estilos/loading110.gif" Width="80" Height="80" MarginSpec="5 0 0 80" />
                <ext:Label runat="server" Html="<P ALIGN=center> Generando archivo, por favor este proceso puede tardar varios minutos...</P>" Width="120" MarginSpec="10 0 0 0 30" />
            </Items>
        </ext:Window>
            </Items>
        </ext:Viewport>
        
    </form>
</body>
</html>
