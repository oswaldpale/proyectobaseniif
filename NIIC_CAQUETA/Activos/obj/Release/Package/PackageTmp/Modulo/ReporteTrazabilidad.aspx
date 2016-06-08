<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteTrazabilidad.aspx.cs" Inherits="Activos.Modulo.Reporte.ReporteNiif" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="text/javascript">
        var prepare = function (grid, command, record, row, col, value) {

            if (record.get('norma') == 2 && command.command == "btnDetalleDepreciacion") {
                command.hidden = true;
                command.hideMode = "visibility";
            }

        };
    </script>
    <title></title>
</head>
<body>

    <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="true" Theme="Neptune" />

    <form id="form1" runat="server">
        <ext:Hidden ID="Hidden1" runat="server" />

        <ext:Store ID="StoreActivos" runat="server" AutoDataBind="false" PageSize="150">
            <Model>
                <ext:Model ID="Model3" runat="server">
                </ext:Model>
            </Model>
        </ext:Store>

        <ext:Store ID="Store1" runat="server" PageSize="150">
            <Model>
                <ext:Model ID="Model1" runat="server">
                </ext:Model>
            </Model>
        </ext:Store>


        <ext:Viewport ID="Viewport1" runat="server">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
            </LayoutConfig>
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Width="1300" Height="580" Border="true"
                    TitleAlign="Center" BodyPadding="8" AutoScroll="true">
                    <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                    <Items>
                        <ext:FieldSet ID="FieldSet1" runat="server" DefaultWidth="1250" Title="Filtros"
                            Height="70">
                            <Items>
                                <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Label ID="Label3" runat="server" Html="SubClase<font color='red'>*</font>" Width="600" />
                                        <ext:Label ID="Label1" runat="server" Width="150" Html="Estado <font color='red'>*</font>" Margins="0 0 0 5" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField runat="server" ID="txt_codigo_clase" Hidden="true" />
                                        <ext:TextField runat="server" ID="txt_codigo_subclase" Hidden="true" />
                                        <ext:DropDownField ID="drop_subclase" runat="server" FieldStyle="text-align: center;"
                                            AllowBlank="false" Editable="false" TriggerIcon="Search" MatchFieldWidth="false"
                                            Width="600" Margins="0 0 0 0">
                                            <Listeners>
                                                <Expand Handler="App.direct.CargarSubClases();" />
                                            </Listeners>
                                            <Component>
                                                <ext:Window ID="win_subclases" runat="server" Title="SubClases" CloseAction="Hide" Hidden="true"
                                                    Closable="true" Layout="FitLayout" Width="600" Height="400" Resizable="false">
                                                    <Items>
                                                        <ext:GridPanel ID="grilla_subclases" runat="server" Height="250">
                                                            <Store>
                                                                <ext:Store ID="Store_subclase" runat="server" PageSize="9">
                                                                    <Model>
                                                                        <ext:Model ID="Model4" runat="server" IDProperty="Codigo">
                                                                            <Fields>
                                                                                <ext:ModelField Name="Codigo" />
                                                                                <ext:ModelField Name="IdClase" />
                                                                                <ext:ModelField Name="Clase" />
                                                                                <ext:ModelField Name="Nombre" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                            <ColumnModel ID="ColumnModel2" runat="server">
                                                                <Columns>
                                                                    <ext:Column ID="Column3" runat="server" DataIndex="Codigo" Text="Código" Align="Center"
                                                                        Width="60">
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column4" runat="server" DataIndex="Clase" Text="Clase" Flex="1">
                                                                        <HeaderItems>
                                                                            <ext:TextField ID="TextField2" runat="server" FieldStyle="text-align: left;" EnableKeyEvents="true">
                                                                                <Listeners>
                                                                                    <Change Handler="this.up('grid').applyFilter();" Buffer="500" />
                                                                                </Listeners>
                                                                            </ext:TextField>
                                                                        </HeaderItems>
                                                                    </ext:Column>
                                                                    <ext:Column ID="Column5" runat="server" DataIndex="Nombre" Text="SubClase" Flex="1">
                                                                        <HeaderItems>
                                                                            <ext:TextField ID="TextField3" runat="server" EnableKeyEvents="true">
                                                                                <Listeners>
                                                                                    <Change Handler="this.up('grid').applyFilter();" Buffer="500" />
                                                                                </Listeners>
                                                                            </ext:TextField>
                                                                        </HeaderItems>
                                                                    </ext:Column>
                                                                </Columns>
                                                            </ColumnModel>
                                                            <SelectionModel>
                                                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                                                                    <Listeners>
                                                                        <Select Handler="App.drop_subclase.setValue('(' +record.data['Clase']+') '+  record.data['Nombre']);
                                                                                                    App.txt_codigo_clase.setValue(record.data['IdClase']);
                                                                                                    App.txt_codigo_subclase.setValue(record.data['Codigo']);" />
                                                                    </Listeners>
                                                                </ext:RowSelectionModel>
                                                            </SelectionModel>
                                                            <BottomBar>
                                                                <ext:PagingToolbar ID="PagingToolbar2" runat="server" HideRefresh="true" />
                                                            </BottomBar>
                                                        </ext:GridPanel>
                                                    </Items>
                                                </ext:Window>
                                            </Component>
                                        </ext:DropDownField>
                                        <ext:ComboBox runat="server" ID="cbx_estado" Width="150" Margins="0 0 0 5">
                                            <Items>
                                                <ext:ListItem Text="Inactivo" Value="I" />
                                                <ext:ListItem Text="Activo" Value="A" />
                                            </Items>
                                        </ext:ComboBox>

                                        <ext:Button runat="server" ID="btn_buscar" Width="100" Text="Buscar" Icon="Zoom" Margins="0 0 0 5">
                                            <Listeners>
                                                <Click Handler="if(App.FormPanel1.isValid()){
                                                                    App.direct.BuscarFiltro(
                                                                        App.txt_codigo_subclase.getValue(),
                                                                        App.cbx_estado.getValue()
                                                                    );
                                                                }else{true}
                                                                " />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet2" runat="server" DefaultWidth="1260" Height="450" Title="Activos Fijos">
                            <Items>
                                <ext:Container ID="Container3" runat="server">
                                    <Items>
                                        <ext:GridPanel ID="GrillaActivos" runat="server" Height="400" Border="true">
                                            <Store>
                                                <ext:Store ID="store_reportes" runat="server">
                                                    <Model>
                                                        <ext:Model ID="Model19" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="FechaEntrada" />
                                                                <ext:ModelField Name="idActivo" />
                                                                <ext:ModelField Name="NControl" />
                                                                <ext:ModelField Name="placa" />
                                                                <ext:ModelField Name="Nombre" />
                                                                <ext:ModelField Name="norma" />
                                                                <ext:ModelField Name="Proveedor" />
                                                                <ext:ModelField Name="CostoInicial" />
                                                                <ext:ModelField Name="vr_razonable" />
                                                                <ext:ModelField Name="vr_deterioro" />
                                                                <ext:ModelField Name="vr_residual" />
                                                                <ext:ModelField Name="ajus_dep_acum" />
                                                                <ext:ModelField Name="vr_Depreciacion" />
                                                                <ext:ModelField Name="Importe_libros" />
                                                                <ext:ModelField Name="CodEmpleado" />
                                                                <ext:ModelField Name="EstadoBaja" />
                                                                <ext:ModelField Name="Estado" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel1" runat="server">
                                                <Columns>
                                                    <ext:Column ID="Column1" runat="server" DataIndex="FechaEntrada" Header="Fecha Ingreso" Width="100">
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
                                                    <ext:Column ID="Column2" runat="server" DataIndex="NControl" Header="N Control" Flex="1" Hidden="true" />
                                                    <ext:Column ID="Column6" runat="server" DataIndex="placa" Header="Placa" Width="90">
                                                        <HeaderItems>
                                                            <ext:TextField ID="TextField6" runat="server" FieldCls="text-center">
                                                                <Listeners>
                                                                    <Change Handler="this.up('grid').applyFilter();" />
                                                                </Listeners>
                                                                <Plugins>
                                                                    <ext:ClearButton ID="ClearButton6" runat="server" />
                                                                </Plugins>
                                                            </ext:TextField>
                                                        </HeaderItems>
                                                    </ext:Column>
                                                    <ext:Column ID="Column7" runat="server" DataIndex="Nombre" Header="Nombre" Flex="2" />
                                                    <ext:Column ID="Column8" runat="server" DataIndex="CostoInicial" Header="Costo Inicial" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column9" runat="server" DataIndex="vr_razonable" Header="Vr Revaluación" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column10" runat="server" DataIndex="vr_razonable" Header="Vr Deterioro" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column11" runat="server" DataIndex="vr_residual" Header="Vr Residual" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column12" runat="server" DataIndex="ajus_dep_acum" Header="Ajuste Dep Acum" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column13" runat="server" DataIndex="vr_Depreciacion" Header="Dep Acum" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column14" runat="server" DataIndex="Importe_libros" Header="Importe Libros" Width="110">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column88" runat="server" Align="Center" Width="150">
                                                        <Commands>
                                                            <ext:ImageCommand CommandName="btnDetalleDepreciacion" Icon="Report" ToolTip-Text="Reporte Depreciación">
                                                            </ext:ImageCommand>
                                                            <ext:ImageCommand CommandName="btnDetalleTrazabilidad" Icon="Report" ToolTip-Text="Reporte Trazabilidad" />
                                                        </Commands>
                                                        <PrepareCommand Fn="prepare" />

                                                        <Listeners>

                                                            <Command Handler="if(command=='btnDetalleDepreciacion'){ 
                                                                                                      App.direct.TrazabilidadNormaInternacional(record.data['idActivo'],record.data['placa'],record.data['Nombre']);}
                                                                                 " />
                                                        </Listeners>
                                                    </ext:Column>

                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" HideRefresh="true" />
                                            </BottomBar>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                    <FooterBar>
                        <ext:Toolbar runat="server" ID="asdas">
                            <Items>
                                <%--   <ext:Button runat="server" ID="btn_exportar" Text="EXPORTAR" Icon="PageExcel" Scale="Small"
                                    Hidden="false" Disabled="true" OnClick="ToExcel2" AutoPostBack="true">
                                   
                                </ext:Button>--%>
                            </Items>
                        </ext:Toolbar>
                    </FooterBar>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>

        <ext:ComboBox
            ID="cbx_tipoactivo"
            runat="server"
            DisplayField="Nombre"
            ValueField="ID"
            Editable="false"
            QueryMode="Local" ForceSelection="true"
            SelectOnTab="true">
            <Store>
                <ext:Store ID="StoreTipoActivo" runat="server">
                    <Model>
                        <ext:Model runat="server" IDProperty="ID">
                            <Fields>
                                <ext:ModelField Name="ID" />
                                <ext:ModelField Name="Nombre" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
        </ext:ComboBox>
        <ext:ComboBox
            ID="cbx_propiedad"
            runat="server"
            DisplayField="Nombre"
            ValueField="ID"
            Editable="false"
            QueryMode="Local" ForceSelection="true"
            SelectOnTab="true">
            <Store>
                <ext:Store ID="StorePropiedad" runat="server">
                    <Model>
                        <ext:Model runat="server" IDProperty="ID">
                            <Fields>
                                <ext:ModelField Name="ID" />
                                <ext:ModelField Name="Nombre" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
        </ext:ComboBox>
        <ext:ComboBox
            ID="cbx_proveedor"
            runat="server"
            DisplayField="Nombre"
            ValueField="ID"
            QueryMode="Local" ForceSelection="true"
            SelectOnTab="true">
            <Store>
                <ext:Store ID="StoreProveedor" runat="server">
                    <Model>
                        <ext:Model runat="server" IDProperty="ID">
                            <Fields>
                                <ext:ModelField Name="ID" />
                                <ext:ModelField Name="Nombre" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
        </ext:ComboBox>

    </form>
</body>

</html>
