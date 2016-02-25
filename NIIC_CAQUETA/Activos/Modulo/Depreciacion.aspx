
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Depreciacion.aspx.cs" Inherits="Activos.Modulo.Depreciacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>DEPRECIACION</title>
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
   <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js" >
    </script>
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
    <script type="text/javascript">

        var calcularFecha = function (e) {
            //alert('Hola Mundo');
            var grilla = App.GridPanel1.getRowsValues();

            //            for (var i = 0; i < grilla.length; i++) {
            //                var pregunta = grilla[i]['tipodepreciacion'];
            //                if (pregunta == 'LINEA RECTA') {
            //                    var posicionGrilla = grilla[i]['codigocomponente'];
            //                    var fechaActual = document.getElementById("dfd_fecha-inputEl");
            //                    var fechaAnterior = document.getElementById("txt_fecha_anterior-inputEl");

            //                    var fechaCalculada = daysBetween(fechaActual, fechaAnterior);
            //                    
            //                    
            //                    alert("Los Dias son: " + fechaCalculada);

            //                }
            //            }

            for (var i = 0; i < grilla.length; i++) {
                var pregunta = grilla[i]['tipodepreciacion'];
                if (pregunta == 'LINEA RECTA') {
                    var posicionGrilla = grilla[i]['codigocomponente'];
                    var fechaActual = document.getElementById("dfd_fecha-inputEl");
                    var fechaAnterior = document.getElementById("txt_fecha_anterior-inputEl");
                    var fechaCalculada = daysBetween(fechaActual, fechaAnterior);
                }
            }
            // alert("" + grilla[0]['costoinicial']);
        };

        function daysBetween(fechaActual, fechaAnterior) {
            var fechaA = fechaActual.value.split('-');
            var fechaF = fechaAnterior.value.split('-');

            var fActual = new Date(fechaA[2], parseFloat(fechaA[1]) - 1, parseFloat(fechaA[0]));
            var fAnterior = new Date(fechaF[2], parseFloat(fechaF[1]) - 1, parseFloat(fechaF[0]));

            var fin = fActual.getTime() - fAnterior.getTime();
            var dias = Math.floor(fin / (1000 * 60 * 60 * 24))

            return dias;
        }



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
    </script>
    <ext:Window ID="winDetalle" runat="server" Title="Ingresar  Cantidad" Width="400"
        Height="180" Modal="true" Hidden="true" Layout="Fit">
        <Items>
            <ext:FormPanel ID="CompanyInfoTab" runat="server"  Layout="TableLayout" 
                DefaultAnchor="100%" BodyPadding="5">
                <Items>
                    <ext:Hidden ID="hiddencomponente" runat="server" />
                    <ext:TextField ID="txtComponente" runat="server" FieldLabel="Componente" ReadOnly="true" Hidden="true" />
                   
                    
                    <ext:TextField ID="txtTipo" runat="server" FieldLabel="Tipo Depreciación" ReadOnly="true" />
                    <ext:TextField ID="txtCantidad" runat="server" FieldLabel="Cantidad" />
                </Items>
                <Buttons>
                    <ext:Button ID="btnSave" runat="server" Text="Save" Icon="Disk">
                        <Listeners>
                            <Click Handler=" var sel= App.GridPanel1.getView().getSelectionModel().getSelection()[0];
                            sel.data[sel.fields.items[3].name]=App.txtCantidad.getValue();
                            App.GridPanel1.getView().refresh();
                            App.GridPanel1.store.commitChanges();
                            App.winDetalle.hide();" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnCancel" runat="server" Text="Cancel" Icon="Cancel">
                        <Listeners>
                            <Click Handler="App.winDetalle.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Window>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
    <ext:Hidden ID="hiddenfechaant" runat="server" />
    <ext:Hidden ID="hiddenoidemp" runat="server" />
    <ext:Hidden ID="codigo" runat="server" />
    <ext:Hidden ID ="idempleado" runat="server" />
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="1000" Height="610" Frame="true"
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Right" MsgTarget="Side" />
                <Items>
                    <%--***************************--%>
                    <ext:FieldSet ID="FieldSet1" runat="server" Disabled="false">
                        <Items>
                            <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Label ID="lbl_activo" runat="server" Flex="3" Enabled="true" Html ="Activo<font color ='red'>*</font>" Margins="10 0 0 10" />
                                    <ext:Label ID="lbl_fecha" runat="server" Flex="1" Html ="Fecha<font color ='red'>*</font>" Margins="10 0 0 10" />
                                    
                                    <ext:Label ID="lbl_fechaAlta" runat="server" Text="Fecha Alta" Flex="1" Margins="10 0 0 10" />
                                     <ext:Label ID="lbl_fecha_anterior" runat="server" Text="Fecha Dep Ultima" Flex="1" Enabled="true" Margins="10 0 0 10" />
                                     
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:TextField ID="txt_descripcion" runat="server" Flex="3" Disabled="false" Margins="5 0 0 5" Hidden="true"
                                        EmptyText="" ReadOnly="true" />
                                    <ext:TriggerField ID="TriggerField1" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="3" Margins="5 0 0 5">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <DirectEvents>
                                            <TriggerClick OnEvent="TriggerField1_Click" After="#{Window1}.show();">
                                                <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window1" UseMsg="false" />
                                            </TriggerClick>
                                        </DirectEvents>
                                    </ext:TriggerField>

                                    <ext:DateField ID="dfd_fecha" runat="server" FieldStyle="text-align: center;" AllowBlank="false"
                                        Margins="5 0 0 5" Flex="1" Format="yyyy-MM-dd" EnableKeyEvents="true">
                                        <Listeners>
                                            <Select Handler="App.direct.restarfechas(App.GridPanel1.getStore().getRecordsValues());" />
                                        </Listeners>
                                    </ext:DateField>

                                   
                                    <ext:TextField ID="dfd_fechaAlta" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5" 
                                        ReadOnly="true" /> 

                                     <ext:TextField ID="txt_fecha_anterior" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true"/>                                              
                                     
                                    
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Label ID="lbl_responsable" runat="server" Text="Responsable" Flex="3" Margins="5 0 0 5" />
                                    <ext:Label ID="Label1" runat="server" Text="Placa" Flex="1" Enabled="true" Margins="5 0 0 5" />
                                    <ext:Label ID="Label8" runat="server" Text="Centro Costo" Flex="1" Enabled="true" Margins="5 0 0 5" />
                                    <ext:Label ID="Label9" runat="server" Text="Centro Economico" Flex="1" Enabled="true" Margins="5 0 0 5" />
                                   
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:TextField ID="txt_resp" runat="server" Flex="3" Disabled="false" Margins="5 0 10 5"
                                        ReadOnly="true" />
                                   
                                    <ext:TextField ID="txt_placa" runat="server" Flex="1" Disabled="false" Margins="5 0 10 5" 
                                        ReadOnly="true" /> 
                                    <ext:TextField ID="txt_ccosto" runat="server" Flex="1" Disabled="false" Margins="5 0 10 5" 
                                        ReadOnly="true" /> 
                                    <ext:TextField ID="txt_economico" runat="server" Flex="1" Disabled="false" Margins="5 0 10 5" 
                                        ReadOnly="true" /> 
                                        
                                   
                                </Items>
                            </ext:Container>
                             <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Label ID="Label3" runat="server" Text="Costo Inicial" Flex="1" Margins="5 0 0 5" />
                                    <ext:Label ID="Label4" runat="server" Text="Revaluación" Flex="1" Margins="5 0 0 5" />
                                    <ext:Label ID="Label5" runat="server" Text="Valor Deterioro" Flex="1" Margins="5 0 0 5" />
                                    <ext:Label ID="Label6" runat="server" Text="Valor Residual" Flex="1" Enabled="true" Margins="5 0 0 5" />
                                    <ext:Label ID="Label2" runat="server" Text="Ajuste Depreciación Acum" Flex="1" Margins="5 0 0 5" />
                                    <ext:Label ID="lbl_valor" runat="server" Text="Depreciación Acum" Flex="1" Margins="5 0 0 5" />
                                    <ext:Label ID="Label7" runat="server" Text="Importe Libros" Flex="1" Margins="5 0 0 5" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:TextField ID="txt_costo" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true" />
                                    <ext:TextField ID="txt_razonable" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true" />
                                    <ext:TextField ID="txt_deterioro" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true" />
                                    <ext:TextField ID="txt_residual" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true"/>
                                     <ext:TextField ID="txt_ajusAcum" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true" />
                                    <ext:TextField ID="txt_valor" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true" />
                                    <ext:TextField ID="txt_importe" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5"
                                        ReadOnly="true" />    
                                </Items>
                            </ext:Container>
                      <%--  </Items>
                    </ext:FieldSet>--%>
                    <%--***************************--%>
                    <%--Resumen Depreciacion x componentes--%>
                    <%--<ext:FieldSet ID="field_detalle" runat="server">
                        <Items>--%>
                            
                           
                            <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" Plain="True" PaddingSpec="10 0 0" Hidden="false" UI="Primary">
                                   <Items>
                               <ext:Panel ID="Panel1" Title="Componentes" runat="server" Flex="1" >
                                        <Items>
                                            <ext:GridPanel ID="GridPanel1" runat="server" Flex="1"   Margins="10 0 0 10" Border="true" Height="280">
                                                <Store>
                                                    <ext:Store ID="Store_componente" runat="server">
                                                        <Model>
                                                            <ext:Model ID="Model1" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="codigocomponente" />
                                                                    <ext:ModelField Name="descripcioncomponente" />
                                                                    <ext:ModelField Name="tipodepreciacion" />
                                                                    <ext:ModelField Name="vidautil" Type="Float" />
                                                                    <ext:ModelField Name="depreciacion" />
                                                                    <ext:ModelField Name="costoinicial" />
                                                                    <ext:ModelField Name="residual" />
                                                                    <ext:ModelField Name="deterioro" />
                                                                    <ext:ModelField Name="razonable" />
                                                                    <ext:ModelField Name="ajuste_dep_acum" />
                                                                    <ext:ModelField Name="depacumulada" />
                                                                    <ext:ModelField Name="porcentaje" />
                                                                    <ext:ModelField Name="vutil" />
                                                                    <ext:ModelField Name="vr_importe_libros" />
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
                                                        <ext:Column ID="Column1" runat="server" Text="Componentes" DataIndex="descripcioncomponente"
                                                            Width="210" />
                                                        <ext:Column ID="Column2" runat="server" Text="Tipo" Width="200" DataIndex="tipodepreciacion">
                                                            <Commands>
                                                                <ext:ImageCommand CommandName="Cambiar" Icon="NoteEdit" />
                                                            </Commands>
                                                            <PrepareCommand Fn="prepare" />
                                                            <Listeners>
                                                                <Command Fn="onCommand" />
                                                            </Listeners>
                                                        </ext:Column>
                                                        <ext:Column ID="Column3" runat="server" DataIndex="vutil" Header="Vida Util" Hidden="false"
                                                            Width="70" />
                                                        <ext:Column ID="Column8" runat="server" DataIndex="vidautil" Width="70" Text="Vida Dep ">
                                                            <%--<Renderer Handler="if(record.data.tipodepreciacion=='LINEA RECTA'){                                                            
                                                            return ;} "/> --%>
                                                        </ext:Column>
                                                        <ext:Column ID="Column10" runat="server" DataIndex="costoinicial" Header="Costo Inicial"
                                                            Width="110">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column ID="Column21" runat="server" DataIndex="residual" Header="V. Residual"
                                                            Width="110">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column ID="Column22" runat="server" DataIndex="deterioro" Header="V. Deterioro"
                                                            Width="110">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column ID="Column23" runat="server" DataIndex="razonable" Header="Ajuste. Razonable"
                                                            Width="120">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>

                                                        <ext:Column ID="Column24" runat="server" DataIndex="ajuste_dep_acum" Header="Ajuste Dep. Acum"
                                                            Width="120">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>

                                                        <ext:Column ID="Column5" runat="server" DataIndex="depacumulada" Header="Dep. Acum"
                                                            Width="110">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column ID="Column6" runat="server" DataIndex="vr_importe_libros" Header="Importe Dep"
                                                            Hidden="false" Width="110">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>
                                                <SelectionModel>
                                                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                                                </SelectionModel>
                                                <BottomBar>
                                                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="10" >
                                                    </ext:PagingToolbar>
                                                </BottomBar>

                                            </ext:GridPanel>
                                        </Items>
                                        <BottomBar>
                                            <ext:Toolbar ID="Toolbar1" runat="server">
                                                <Items>
                                                    <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                                    <ext:Button ID="Button1" runat="server" Text="Depreciar">
                                                        <Listeners>
                                                            <Click Handler="App.direct.Depreciar(#{GridPanel1}.getStore().getRecordsValues(),                            
                                                            [App.hiddenoidemp.getValue(),                                                           
                                                            App.dfd_fecha.getValue(),
                                                            ]
                                                            );" />
                                                        </Listeners>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </BottomBar>
                                    </ext:Panel>
                               <ext:Panel ID="Panel2" runat="server" Flex="1" Title="Trazabilidad">
                                        <Items>
                                            <%--- grilla historial de la depreciacion-----%>
                                            <ext:GridPanel ID="GridPanel2" runat="server"
                                                Flex="1" Height="320" Margins="10 10 0 10" Border="true">
                                                <Store>
                                                    <ext:Store ID="Store1" runat="server" GroupField="descripcion">
                                                        <Sorters>
                                                            <ext:DataSorter Property="fecha" Direction="ASC" />
                                                        </Sorters>
                                                        <Model>
                                                            <ext:Model ID="Model2" runat="server" IDProperty="codigo">
                                                                <Fields>
                                                                    <ext:ModelField Name="codigo" />
                                                                    <ext:ModelField Name="descripcion" />
                                                                    <ext:ModelField Name="fecha" Type="Date" />
                                                                    <ext:ModelField Name="base" />
                                                                    <ext:ModelField Name="dep_mes" />
                                                                    <ext:ModelField Name="cantdep" />
                                                                    <ext:ModelField Name="saldo_dep" />
                                                                    <ext:ModelField Name="depacumulada" />
                                                                    <ext:ModelField Name="ajrazonable" />
                                                                    <ext:ModelField Name="ajdepacumulada" />
                                                                    <ext:ModelField Name ="vrdeterioro" />
                                                                    <ext:ModelField Name ="vresidual" />
                                                                    <ext:ModelField Name="tipo_doc" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel1" runat="server">
                                                    <Columns>

                                                        <ext:Column ID="Column4" runat="server" TdCls="task" Text="Componente: " Sortable="true"
                                                            DataIndex="descripcion" Hideable="false" Flex="1" />
                                                        <ext:Column ID="Column7" runat="server" Width="85" Text="Fecha" Sortable="true"
                                                            DataIndex="fecha">
                                                            <Renderer Format="Date" FormatArgs="'Y-m-d'" />
                                                        </ext:Column>
                                                        <ext:Column ID="Column25" runat="server" Width="120" Text="Tipo Documento" Sortable="true"
                                                            DataIndex="tipo_doc">
                                                        </ext:Column>
                                                        <ext:Column runat="server" Width="110" ID="Column13" Text="Cant Depreciada" Sortable="false"
                                                            Groupable="false" DataIndex="cantdep">                                                         
                                                        </ext:Column>
                                                        <ext:Column runat="server" Width="120" ID="Column17" Text="Ajuste Razonable" Sortable="false"
                                                            Groupable="false" DataIndex="ajrazonable">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        
                                                        <ext:Column runat="server" Width="110" ID="Column19" Text="Deterioro" Sortable="false" 
                                                            Groupable="false" DataIndex="vrdeterioro">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                         <ext:Column runat="server" Width="110" ID="Column20" Text="Residual" Sortable="false"
                                                            Groupable="false" DataIndex="vresidual">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column runat="server" Width="120" ID="Column18" Text="Ajuste Dep Acum" Sortable="false"
                                                            Groupable="false" DataIndex="ajdepacumulada">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column ID="Column9" runat="server" Width="110" Text="Base Dep" Sortable="true"
                                                            DataIndex="base">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column runat="server" Width="110" ID="Cost" Text="Dep mes" Sortable="false"
                                                            Groupable="false" DataIndex="dep_mes">
                                                            <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column runat="server" Width="110" ID="Column11" Text="Dep Acum" Sortable="false"
                                                            Groupable="false" DataIndex="depacumulada">
                                                          <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                        </ext:Column>
                                                        <ext:Column runat="server" Width="120" ID="Column12" Text="Saldo Depreciable" Sortable="false"
                                                            Groupable="false" DataIndex="saldo_dep">
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
                                                    <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="10" >
                                                    </ext:PagingToolbar>
                                                </BottomBar>
                                            </ext:GridPanel>
                                        </Items>
                                    </ext:Panel>
                            </Items>  
                            </ext:TabPanel>
                        </Items>
                    </ext:FieldSet>
                   
                   
                    <%--*************************************************--%>
                </Items>
                <%--*************************Botones del formulario agregar Componentes************************--%>
                <Buttons>
                    <ext:Button ID="btn_CancelAsignacion" runat="server" Text="Cerrar" Disabled="false"
                        Icon="Lock">
                        <Listeners>
                            <Click Handler="
                          cerrarventana1();                           
                          ">
                            </Click>
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>
     <ext:Window ID="Window1" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="700">
            <Items>
                <ext:GridPanel ID="GridActivo" runat="server" Title="Comodatos" UI="Info" Border="true" Height="435">
                    <Tools>
                        <ext:Tool ID="Tool2" Type="Close">
                            <Listeners>
                                <Click Handler="#{Window1}.hide();" />
                            </Listeners>
                        </ext:Tool>
                    </Tools>
                    <Store>
                        <ext:Store ID="Store2" runat="server" PageSize="12">
                            <Model>
                                <ext:Model ID="Model3" runat="server" IDProperty="codigo">
                                    <Fields>
                                        <ext:ModelField Name="codigo" />
                                        <ext:ModelField Name="nombre" />
                                        <ext:ModelField Name="placa" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="codigo" Direction="DESC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel2" runat="server">
                        <Columns>
                            <ext:Column ID="Column14" runat="server" Text="Codigo" DataIndex="codigo" Align="Center" Width="120">
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
                           
                            <ext:Column ID="Column15" runat="server" Text="Placa" DataIndex="placa" Align="Center" Width="120">
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
                            <ext:Column ID="Column16" runat="server" Text="Activo" DataIndex="nombre" Flex="1">
                                <HeaderItems>
                                    <ext:TextField ID="TextField3" runat="server">
                                        <Listeners>
                                            <Change Handler="this.up('grid').applyFilter();" />
                                        </Listeners>
                                        <Plugins>
                                            <ext:ClearButton ID="ClearButton3" runat="server" />
                                        </Plugins>
                                    </ext:TextField>
                                </HeaderItems>
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <ItemDblClick OnEvent="GridComodato_SelectRow" Before="#{FormPanel1}.getForm().reset();" After="#{Tool2}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="NComodato" Value="record.get('codigo')" Mode="Raw" />
                                <ext:Parameter Name ="Nombre"   Value="record.get('nombre')" Mode="Raw" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Target="Page" UseMsg="false" />
                        </ItemDblClick>
                    </DirectEvents>
                    <BottomBar>
                        <ext:PagingToolbar ID="PagingToolbar3" runat="server" HideRefresh="true" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Window>
    </form>
</body>
</html>
