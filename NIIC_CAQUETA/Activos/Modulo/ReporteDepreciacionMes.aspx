<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteDepreciacionMes.aspx.cs" Inherits="Activos.Modulo.Reporte.ReporteNiif" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>

    <script src="JS/Blob.js"></script>
    <script src="JS/FileSaver.js"></script>
    <script src="JS/papaparse.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <style type="text/css">
        /**/
        #unlicensed {
            display: none !important;
        }
    </style>
    <script type="text/javascript">

        //************GENERADOR DE CSV***************
        var descargaCSV = function (url) {
            App.w1.show();
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    // data = xmlhttp.responseText;
                    //var myArr = JSON.parse(xmlhttp.responseText);
                    myFunction(xmlhttp.responseText);
                }
            }
            xmlhttp.open("GET", url, true);
            xmlhttp.send();
        };

        function myFunction(arr) {
            var BOM = "\uFEFF";
            
            var csv = BOM + arr;// Papa.unparse(arr, { delimiter: "," });
            var blob = new Blob([csv], { type: "text/csv;charset=UTF-8" });
            var date = new Date();

            saveAs(blob, "_" + App.CDEPRECIACION.rawValue + ".csv");
            App.w1.hide();
        };
        //*******************************************

    </script>
    <title></title>
</head>
<body>

    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />

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
                <ext:VBoxLayoutConfig Align="Center" Pack="Center"  />
            </LayoutConfig>
            <Items>
                <ext:Window ID="w1" runat="server" Closable="false" Draggable="false" Resizable="false" Width="270" Hidden="true" Border="false"
                    Frame="false" Height="150" Modal="true" BodyPadding="10">
                    <Items>
                        <ext:Image runat="server" ImageUrl="../Estilos/loading110.gif" Width="80" Height="80" MarginSpec="5 0 0 80" />
                        <ext:Label runat="server" Html="<P ALIGN=center> Generando archivo, por favor este proceso puede tardar varios minutos...</P>" Width="120" MarginSpec="10 0 0 0 30" />
                    </Items>
                </ext:Window>
                <ext:FormPanel ID="FPRINCIPAL" runat="server" Width="550"  Border="true"
                    TitleAlign="Center" BodyPadding="8" AutoScroll="true">
                    <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                    <Items>
                        <ext:FieldSet ID="FSPRINCIPAL" runat="server" DefaultWidth="980"  Height="80">

                            <Items>
                                <ext:Container ID="CTIPOFILTRO" runat="server" Layout="HBoxLayout" Padding="10">
                                    <Items>
                                        <ext:Label  runat="server" Html="DEPRECIACIONES REALIZADAS<font color='red'>*</font>" Width="600"  />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:ComboBox runat="server" ID="CDEPRECIACION" Icon="SectionCollapsed" EmptyText="Select" Disabled="false"
                                            ForceSelection="true"  Width="350"
                                            ValueField="CODIGO" DisplayField="DESCRIPCION" AllowBlank="false">
                                            <Store>
                                                <ext:Store runat="server" ID="store6">
                                                    <Model>
                                                        <ext:Model ID="Model7" runat="server" IDProperty="codigo">
                                                            <Fields>
                                                                <ext:ModelField Name="CODIGO" />
                                                                <ext:ModelField Name="DESCRIPCION" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>
                                     
                                        <ext:Button runat="server" ID="btn_buscar" Width="150" Text="Generar Reporte" Icon="PageExcel" Margins="0 0 0 5">
                                            <Listeners>
                                                <Click Handler="if(App.FPRINCIPAL.isValid()){
                                                                    App.direct.Excel(App.CDEPRECIACION.getValue());
                                                                    
                                                                }else{true}
                                                                " />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                     <%--   <ext:FieldSet runat="server" ID="FDETALLEFILTRO">
                            <Items>
                                <ext:Container runat="server" ID="CFILTROACTIVO" Hidden="true" >
                                    <Items>
                                        <ext:DropDownField runat="server">

                                        </ext:DropDownField>
                                    </Items>
                                </ext:Container>
                                <ext:Container runat="server" ID="CFILTROMES" Height="300" Hidden="true">
                                    <Items>
                                        <ext:DateField runat="server" ID="DFECHAINICIO" Type="Month" />
                                        <ext:DateField runat="server" ID="DFECHAFIN" />
                                        <ext:MonthPicker runat="server" />
                                    </Items>
                                </ext:Container>
                             </Items>

                        </ext:FieldSet>--%>

                        <ext:FieldSet ID="FieldSet2" runat="server" DefaultWidth="960" Height="400" Title="RESUMEN" Collapsed="false" Collapsible="true" Hidden="true">
                            <Items>
                                <ext:Container ID="Container3" runat="server">
                                    <Items>
                                        <ext:GridPanel ID="GrillaActivos" runat="server" Height="370" Border="true" Split="true" ColumnWidth="1">
                                            <Store>
                                                <ext:Store ID="STOREMES" runat="server">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                              <ext:ModelField Name="CODIGO" />
                                                             <ext:ModelField Name="CANT" />
                                                             <ext:ModelField Name="NORMA" />
                                                             <ext:ModelField Name="DEPMES" />
                                                             <ext:ModelField Name="DEPACUMULADA" />
                                                             <ext:ModelField Name="IMPORTEDEP" />
                                                             <ext:ModelField Name="IMPORTELIBROS" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel runat="server">
                                                <Columns>
                                                    <ext:Column ID="CCODIGO" runat="server" DataIndex="CODIGO" Header="CODIGO" Width="100" />
                                                    <ext:Column ID="CCANT" runat="server" DataIndex="CANTI" Header="CANTIDAD ACTIVOS" Width="150" Hidden="true" />
                                                    <ext:Column ID="CNORMA" runat="server" DataIndex="NORMA" Header="NORMA" Width="200" />
                                                    <ext:Column ID="CDEPMES" runat="server" DataIndex="DEPMES" Header="DEP MES" Width="150" >
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="CDEPACUMULADA" runat="server" DataIndex="DEPACUMULADA" Header="DEP ACUMULADA" Width="150">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="CIMPORTEDEP" runat="server" DataIndex="IMPORTEDEP" Header="IMPORTE DEPRECIABLE" Width="170">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="CIMPORTELIBRO" runat="server" DataIndex="IMPORTELIBROS" Header="IMPORTE LIBROS" Width="170">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar runat="server" HideRefresh="true">
                                                    <Items>
                                                        <ext:Toolbar runat="server" ID="asdas">
                                                            <Items>
                                                                <ext:Button runat="server" ID="btn_exportar" Text="EXPORTAR" Icon="PageExcel" Scale="Small"
                                                                    Hidden="false" Disabled="true" AutoPostBack="true">
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </Items>
                                                </ext:PagingToolbar>
                                            </BottomBar>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                  
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
