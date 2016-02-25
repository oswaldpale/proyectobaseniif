<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DarAltaActivoEmpleado.aspx.cs" Inherits="Activos.Modulo.CargarAltaActivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DAR ALTA ACTIVO</title>
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>
    <link href="../Estilos/Estilo.css" rel="stylesheet" />
    <script type="text/javascript">
        var showResult = function (btn) {
            if (btn == "yes") {
                App.win_centroCosto.hide();
                App.TriggerCentro.setValue();
            };
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" AjaxTimeout="56000" />

        <ext:Store ID="Store_empleado" runat="server" PageSize="8">
            <Sorters>
                <ext:DataSorter Property="id_empleado" Direction="ASC" />
            </Sorters>
            <Model>
                <ext:Model ID="Model13" runat="server" IDProperty="id_empleado">
                    <Fields>
                        <ext:ModelField Name="id_empleado" />
                        <ext:ModelField Name="NombreEmpleado" />
                        <ext:ModelField Name="No_documento" />
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>

        <ext:FormPanel ID="FormPanel1" runat="server" Width="800" Frame="true" Height="685"
            TitleAlign="Center" BodyPadding="13">
            <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
            <Items>
                <ext:Hidden ID="FechaCompra" runat="server" />
                <ext:Hidden ID="FechaAlta" runat="server" />
                <ext:Hidden ID="idActivo" runat="server" />
                <ext:Hidden ID="FechaDepreciacion" runat="server" />
                <ext:Hidden ID="idresponsable" runat="server" />
                <ext:Hidden ID="idfuncion" runat="server" />
                <ext:Hidden ID="PlacaActivo" runat="server" />
                <ext:Hidden ID="NombreActivo" runat="server" />
                <ext:Hidden ID="NFactura" runat="server" />
                <ext:Hidden ID="NCompra" runat="server" />
                <ext:Hidden ID="idSubclase" runat="server" />
                <ext:Hidden ID="idClase" runat="server" />
                <ext:Hidden ID="CodigoCosto" runat="server" />
                <ext:Hidden ID="ConcatenadorCCosto" runat="server" />
                <ext:Hidden ID="NombreFuncionario" runat="server" />
                <ext:Hidden ID="CodigoAlta" runat="server" />
                <ext:Hidden ID="NAlta" runat="server" />
                <ext:Hidden ID="HCodigoGrupo" runat="server" />
                <%--***************************--%>
                <ext:FieldSet ID="FRECUPERAR" runat="server" Disabled="false" Title="Recuperar" Collapsed="true" Collapsible="true">
                    <Items>
                        <ext:FormPanel runat="server" Padding="3">
                            <Items>
                                <ext:Label ID="LRECUPERAR" runat="server" Text="No. Alta" Width="100" Margins="10 0 0 10" />
                            </Items>
                            <Items>
                                <ext:TextField ID="TRECUPERARALTA" runat="server" Width="120" ReadOnly="true" FieldStyle="text-align:right;" Margins="10 0 0 10"
                                    Selectable="false" Icon="Zoom" FieldCls="CampoReservado">
                                    <Listeners>
                                        <IconClick Handler="App.direct.RecuperarNoAlta();App.WRECUPERARALTA.show();" />
                                    </Listeners>
                                </ext:TextField>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:FieldSet>
                <ext:FieldSet ID="FieldSet2" runat="server" Disabled="false" Title="Informacion General">
                    <Items>
                        <ext:FormPanel runat="server" ID="FpanelGeneral">
                            <Items>
                                <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Label ID="lbl_responsable" runat="server" Html="Empleado<font color ='red'>*</font>" Flex="3" Enabled="true" Width="350" Margins="5 0 0 5" />
                                        <ext:Label ID="lbl_centrocosto" runat="server" Html="Centro Costo<font color ='red'>*</font>" Flex="3" Enabled="true" Margins="5 0 0 5" />
                                        <ext:Label ID="lbl_nfactura" runat="server" Text="No. Factura" Width="100" Margins="5 0 0 5" />
                                        <ext:Label ID="lbl_fecha" runat="server" Html="Fecha Alta<font color ='red'>*</font>" Width="140" Margins="5 0 0 5" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TriggerField ID="TriggerField2" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="3" Margins="5 0 0 5">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <DirectEvents>
                                                <TriggerClick OnEvent="TriggerField2_Click" After="#{Window2}.show();">
                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window2" UseMsg="false" />
                                                </TriggerClick>
                                            </DirectEvents>
                                        </ext:TriggerField>

                                        <ext:TriggerField ID="TriggerCentro" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="3" Margins="5 0 0 5">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <DirectEvents>
                                                <TriggerClick OnEvent="TriggerField2_Click" After="#{win_centroCosto}.show();">
                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="win_centroCosto" UseMsg="false" />
                                                </TriggerClick>
                                            </DirectEvents>
                                        </ext:TriggerField>
                                        <ext:TextField ID="txt_nfactura" runat="server" Width="100" ReadOnly="true" FieldStyle="text-align:right;" Margins="5 0 0 5"
                                            Selectable="false" Icon="Zoom" FieldCls="CampoReservado">
                                            <Listeners>
                                                <IconClick Handler="App.direct.CargarFacturas();App.win_Nfactura.show();" />
                                            </Listeners>
                                        </ext:TextField>
                                        <ext:DateField ID="dfd_fecha" runat="server" Editable="false" FieldStyle="text-align: center;" Margins="5 0 0 5"
                                            Width="140" Format="yyyy-MM-dd" AllowBlank="false" EnableKeyEvents="true">
                                        </ext:DateField>
                                    </Items>
                                </ext:Container>

                                <ext:Container ID="Container31" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Label ID="lbl_uge" runat="server" Html="Unidad Generadora Efectivo<font color ='red'>*</font>" Flex="1"
                                            Enabled="true" Margins="5 0 0 5" />

                                        <ext:Label ID="lbl_centroeconomico" runat="server" Html="Centro Economico<font color ='red'>*</font>" Flex="1"
                                            Enabled="true" Margins="5 0 0 5" />

                                        <ext:Label ID="Label2" runat="server" Html="Motivo<font color ='red'>*</font>" Flex="1" Enabled="true"
                                            Margins="5 0 0 5" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:ComboBox runat="server" ID="cbx_uge" Icon="SectionCollapsed" EmptyText="Select" Disabled="false"
                                            ForceSelection="true" Margins="5 0 0 5" Flex="1"
                                            ValueField="codigo" DisplayField="uge" AllowBlank="false">
                                            <Store>
                                                <ext:Store runat="server" ID="store6">
                                                    <Model>
                                                        <ext:Model ID="Model7" runat="server" IDProperty="codigo">
                                                            <Fields>
                                                                <ext:ModelField Name="codigo" />
                                                                <ext:ModelField Name="uge" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>


                                        <ext:ComboBox runat="server" ID="cbx_economico" Icon="SectionCollapsed" EmptyText="Select"
                                            ForceSelection="true" Margins="5 0 0 5" Flex="1"
                                            ValueField="codigo" DisplayField="economico" AllowBlank="false">
                                            <Store>
                                                <ext:Store runat="server" ID="store3">
                                                    <Model>
                                                        <ext:Model ID="Model4" runat="server" IDProperty="codigo">
                                                            <Fields>
                                                                <ext:ModelField Name="codigo" />
                                                                <ext:ModelField Name="economico" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>

                                        <ext:ComboBox runat="server" ID="cbx_motivo" Icon="SectionCollapsed" EmptyText="Select"
                                            ForceSelection="true" Margins="5 0 0 5" Flex="1"
                                            ValueField="codigo" DisplayField="motivo" AllowBlank="false">
                                            <Store>
                                                <ext:Store runat="server" ID="store5">
                                                    <Model>
                                                        <ext:Model ID="Model6" runat="server" IDProperty="codigo">
                                                            <Fields>
                                                                <ext:ModelField Name="codigo" />
                                                                <ext:ModelField Name="motivo" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>

                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Label ID="lbl_concepto" runat="server" Html="Observación<font color ='green'>*</font>" Flex="1" Enabled="true"
                                            Margins="5 0 0 5" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextArea ID="txt_conceptos" runat="server" Margins="5 0 15 5" AllowBlank="true"
                                            Height="40" Flex="1" MaxLength="1999" EnforceMaxLength="true">
                                        </ext:TextArea>
                                    </Items>
                                </ext:Container>

                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:FieldSet>
                <ext:FieldSet ID="FielSet33" runat="server" Title="Detalle">
                    <Items>
                        <ext:Container ID="Container34" runat="server" Layout="HBoxLayout">
                            <Items>

                                <ext:Label ID="Label1" runat="server" Html="Item<font color ='red'>*</font>" Flex="1" Enabled="true"
                                    Margins="5 0 0 5" />
                                <ext:Label ID="lbl_ubicacion" runat="server" Html="Dependencia<font color ='red'>*</font>" Flex="1" Enabled="true"
                                    Margins="5 0 0 5" />
                                <ext:Label ID="lbl_funcion" runat="server" Html="Función<font color ='red'>*</font>" Flex="1" Enabled="true"
                                    Margins="5 0 0 5" />
                                <ext:Label ID="Label3" runat="server" Width="80" Enabled="true"
                                    Margins="5 0 0 5" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                            <Items>

                                <ext:TriggerField ID="TriggerField1" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="1" Margins="0 0 15 5">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <DirectEvents>
                                        <TriggerClick OnEvent="TriggerField1_Click" After="#{Window1}.show();">
                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window1" UseMsg="false" />
                                        </TriggerClick>
                                    </DirectEvents>
                                </ext:TriggerField>

                                <ext:ComboBox runat="server" ID="cbx_dependencia" Icon="SectionCollapsed" EmptyText="Select"
                                    ForceSelection="true" Margins="0 0 0 5" Flex="1"
                                    ValueField="codigo" DisplayField="dependencia" AllowBlank="false">
                                    <Store>
                                        <ext:Store runat="server" ID="store1">
                                            <Model>
                                                <ext:Model ID="Model3" runat="server" IDProperty="codigo">
                                                    <Fields>
                                                        <ext:ModelField Name="codigo" />
                                                        <ext:ModelField Name="dependencia" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>
                                <ext:ComboBox runat="server"
                                    ID="cbxFuncion"
                                    Icon="SectionCollapsed"
                                    Flex="1"
                                    ValueField="codigo"
                                    DisplayField="funcion"
                                    AllowBlank="false"
                                    EmptyText="Select"
                                    Margins="0 0 0 5">
                                    <Store>
                                        <ext:Store ID="Store_funcion" runat="server" PageSize="8">
                                            <Sorters>
                                                <ext:DataSorter Property="codigo" Direction="ASC" />
                                            </Sorters>
                                            <Model>
                                                <ext:Model ID="Model9" runat="server" IDProperty="codigo">
                                                    <Fields>
                                                        <ext:ModelField Name="codigo" />
                                                        <ext:ModelField Name="funcion" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>
                                <ext:Button ID="BtnIngresar" runat="server" Text="Ingresar" Icon="Add" Width="80" Margins="0 0 0 5">

                                    <Listeners>
                                        <Click Handler=" 
                                                           
                                                             if(App.FormPanel1.isValid()){ App.direct.ingresarItems( 
                                                                                                #{GridPanel3}.getStore().getRecordsValues(),                      
                                                                                                [
                                                                                                App.cbx_dependencia.getValue(),
                                                                                                App.cbx_dependencia.getDisplayValue(),
                                                                                                App.cbxFuncion.getValue(),
                                                                                                App.cbxFuncion.getDisplayValue()
                                                                                                ]
                                                                                                );
                                                                        }else{
                                                                                        Ext.Msg.show({
                                                                                            title: 'Falta Campos por rellenar',
                                                                                            msg: 'Por Favor, rellene los campos Faltantes',
                                                                                            width: 270,
                                                                                            buttons: Ext.Msg.OK,
                                                                                            icon: Ext.window.MessageBox.INFO
                                                                                        });
                                                            
                                                                        true;
                                                                        }
                                                             " />
                                    </Listeners>
                                </ext:Button>

                            </Items>
                        </ext:Container>
                        <ext:Container runat="server">
                            <Items>
                                <ext:GridPanel ID="GridPanel3" runat="server" Height="300" Margins="0 0 15 0" Border="true">
                                    <Store>
                                        <ext:Store ID="store_asignaciones" runat="server">
                                            <Model>

                                                <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                    <Fields>
                                                        <ext:ModelField Name="idactivo" />
                                                        <ext:ModelField Name="ncompra" />
                                                        <ext:ModelField Name="nfactura" />
                                                        <ext:ModelField Name="placa" />
                                                        <ext:ModelField Name="activo" />
                                                        <ext:ModelField Name="iddependecia" />
                                                        <ext:ModelField Name="dependencia" />
                                                        <ext:ModelField Name="idfuncion" />
                                                        <ext:ModelField Name="activo" Type="String" />
                                                        <ext:ModelField Name="funcion" />
                                                        <ext:ModelField Name="idsubclase" />

                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel4" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn runat="server" Width="40" />
                                            <ext:Column ID="Column13" runat="server" DataIndex="nfactura" Header="No. FACTURA" Width="100">
                                                <HeaderItems>
                                                    <ext:TextField ID="TextField7" runat="server" FieldCls="text-center">
                                                        <Listeners>
                                                            <Change Handler="this.up('grid').applyFilter();" />
                                                        </Listeners>
                                                        <Plugins>
                                                            <ext:ClearButton ID="ClearButton7" runat="server" />
                                                        </Plugins>
                                                    </ext:TextField>
                                                </HeaderItems>
                                            </ext:Column>
                                            <ext:Column ID="Column8" runat="server" DataIndex="placa" Header="PLACA" Width="90">
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
                                            <ext:Column ID="Column6" runat="server" DataIndex="activo" Header="ACTIVO" Width="150" />
                                            <ext:Column ID="Column12" runat="server" DataIndex="dependencia" Header="DEPENDENCIA" Width="155" />
                                            <ext:Column ID="Column7" runat="server" DataIndex="funcion" Header="FUNCION" Width="160" />
                                            <ext:CommandColumn ID="CommandColumn1" runat="server" Align="Center" Width="27">
                                                <Commands>
                                                    <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                </Commands>
                                                <Listeners>
                                                    <Command Handler="App.GridPanel3.getStore().remove(record);App.direct.RestaurarItems(record.data.idactivo,record.data.placa,record.data.activo,record.data.idsubclase,record.data.ncompra,record.data.nfactura);" />
                                                </Listeners>
                                            </ext:CommandColumn>
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                                    </SelectionModel>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar3" runat="server" HideRefresh="true" />
                                    </BottomBar>

                                </ext:GridPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:FieldSet>

            </Items>

            <%-- <%--*************************Botones Generales************************--%>
            <FooterBar>
                <ext:Toolbar runat="server" ID="asdas">
                    <Items>
                        <ext:Button runat="server" ID="btn_reporte" Text="REPORTE..." Icon="Printer" Scale="Small">
                            <Listeners>
                                <Click Handler="
                                                  
                                                    App.direct.Informe_AltaActivo_RptDetalle(  
                                                    #{GridPanel3}.getStore().getRecordsValues(),
                                                    #{GridPanel1}.getStore().getRecordsValues(),
                                                     [App.cbx_uge.getDisplayValue(),
                                                     App.cbx_economico.getDisplayValue()]);
                                                 
                                                    " />
                            </Listeners>
                        </ext:Button>

                        <ext:ToolbarFill />
                        <ext:Button ID="BTNACTUALIZAR" runat="server" Text="ACTUALIZAR" Width="100" Hidden="true">
                            <Listeners>
                                <Click Handler=" var r = App.GridPanel3.getStore().count();
                                             if(r != 0 ) {
                                                if(App.FpanelGeneral.isValid()){ 
                                                  App.direct.ActualizarAlta(#{GridPanel1}.getStore().getRecordsValues());
                                                }else{true} 
                                            }else{
                                                Ext.Msg.show({
                                                title: 'No existe Activos',
                                                msg: 'Por Favor, Ingrese Activos..',
                                                width: 120,
                                                buttons: Ext.Msg.OK,
                                                icon: Ext.window.MessageBox.INFO
                                                });
                                            } " />

                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btn_grabar" runat="server" Text="GRABAR" Icon="Disk" Width="100"
                            ToolTip="Agregar Asignacion." Margins="0 0 15 10">
                            <Listeners>
                                <Click Handler="
                                             var r = App.GridPanel3.getStore().count();
                                             if(r != 0 ) {
                                                if(App.FpanelGeneral.isValid()){ App.direct.grabarAsignaciones(
                                                #{GridPanel3}.getStore().getRecordsValues(),
                                                #{GridPanel1}.getStore().getRecordsValues()
                                                );
                                                }else{true} 
                                            }else{
                                             
                                            Ext.Msg.show({
                                            title: 'No existe Activos',
                                            msg: 'Por Favor, Ingrese Activos..',
                                            width: 120,
                                            buttons: Ext.Msg.OK,
                                            icon: Ext.window.MessageBox.INFO
                                            });
                                            }

                                            " />

                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button1" runat="server" Text="LIMPIAR" Disabled="false">

                            <Listeners>
                                <Click Handler="App.GridPanel3.getStore().removeAll();
                                                        App.GridPanel1.getStore().removeAll();
                                                        App.FormPanel1.reset();
                                                        App.btn_grabar.enable();
                                                        App.direct.limpiarFormulario();
                                        " />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </FooterBar>
            <%--*************************************************--%>
        </ext:FormPanel>



    </form>
    <ext:Window ID="Window1" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="500">
        <Items>
            <ext:GridPanel ID="GridActivo" runat="server" Title="Comodatos" UI="Info" Border="true" Height="360">
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
                            <ext:Model ID="Model2" runat="server" IDProperty="codigo">
                                <Fields>
                                    <ext:ModelField Name="codigo" />
                                    <ext:ModelField Name="NoCompra" />
                                    <ext:ModelField Name="nombre" />
                                    <ext:ModelField Name="placa" />
                                    <ext:ModelField Name="IdSubClase" />
                                    <ext:ModelField Name="NFactura" />
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
                        <ext:Column ID="Column5" runat="server" Text="No Factura" DataIndex="NFactura" Align="Center" Width="120">
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

                        <ext:Column ID="Column2" runat="server" Text="Placa" DataIndex="placa" Align="Center" Width="120">
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
                        <ext:Column ID="Column10" runat="server" Text="Activo" DataIndex="nombre" Flex="1">
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
                    <ItemDblClick OnEvent="GridComodato_SelectRow" After="#{Tool2}.fireEvent('click');">
                        <ExtraParams>
                            <ext:Parameter Name="NComodato" Value="record.get('codigo')" Mode="Raw" />
                            <ext:Parameter Name="Nombre" Value="record.get('nombre')" Mode="Raw" />
                            <ext:Parameter Name="Placa" Value="record.get('placa')" Mode="Raw" />
                            <ext:Parameter Name="NoCompra" Value="record.get('NoCompra')" Mode="Raw" />
                            <ext:Parameter Name="idSubclase" Value="record.get('IdSubClase')" Mode="Raw" />
                            <ext:Parameter Name="NoFactura" Value="record.get('NFactura')" Mode="Raw" />
                        </ExtraParams>
                        <EventMask ShowMask="true" Target="Page" UseMsg="false" />
                    </ItemDblClick>
                </DirectEvents>
                <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar2" runat="server" HideRefresh="true" />
                </BottomBar>
            </ext:GridPanel>
        </Items>
    </ext:Window>
    <ext:Window ID="Window2" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="500">
        <Items>
            <ext:GridPanel ID="GridPanel2" runat="server" Title="Comodatos" UI="Info" Border="true" Height="360" StoreID="Store_empleado">
                <Tools>
                    <ext:Tool ID="Tool1" Type="Close">
                        <Listeners>
                            <Click Handler="#{Window2}.hide();" />
                        </Listeners>
                    </ext:Tool>
                </Tools>

                <ColumnModel ID="ColumnModel3" runat="server">
                    <Columns>
                        <ext:Column ID="Column3" runat="server" Text="Codigo" DataIndex="id_empleado" Align="Center" Width="120">
                            <HeaderItems>
                                <ext:TextField ID="TextField4" runat="server" FieldCls="text-center">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton4" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>

                        <ext:Column ID="Column4" runat="server" Text="Empleado" DataIndex="NombreEmpleado" Align="Left" Flex="1">
                            <HeaderItems>
                                <ext:TextField ID="TextField5" runat="server" FieldCls="text-center">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton5" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <DirectEvents>
                    <ItemDblClick OnEvent="GridDatoEmp_SelectRow" Before="#{FormPanel1}.getForm().reset();" After="#{Tool1}.fireEvent('click');">
                        <ExtraParams>
                            <ext:Parameter Name="No_documento" Value="record.get('No_documento')" Mode="Raw" />
                            <ext:Parameter Name="Codigo" Value="record.get('id_empleado')" Mode="Raw" />
                            <ext:Parameter Name="Nombre" Value="record.get('NombreEmpleado')" Mode="Raw" />
                        </ExtraParams>
                        <EventMask ShowMask="true" Target="Page" UseMsg="false" />
                    </ItemDblClick>
                </DirectEvents>
                <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" HideRefresh="true" />
                </BottomBar>
            </ext:GridPanel>
        </Items>
    </ext:Window>
    <ext:Window ID="win_centroCosto" runat="server" Hidden="true" Width="500" Title="Detalle Centro Costo">
        <Items>
            <ext:FormPanel ID="FrmCentrocosto" runat="server" Border="true" BodyPadding="13">
                <Items>
                    <ext:Container runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:Label ID="lblcentrocosto" runat="server" Flex="5" Html="Centro Costo<font color ='red'>*</font>" />
                            <ext:Label ID="lblporcentaje" runat="server" Flex="4" Html="Porcentaje<font color ='red'>*</font>" />

                        </Items>
                    </ext:Container>
                    <ext:Container runat="server" Layout="HBoxLayout">
                        <Items>

                            <ext:DropDownField ID="Drop2" runat="server" FieldStyle="text-align: center;" Editable="false"
                                Flex="5" TriggerIcon="SimpleArrowDown" MatchFieldWidth="false" Width="255" Hidden="false">

                                <Component>
                                    <ext:GridPanel ID="grilla_recuperar" runat="server" Frame="true" Padding="5" Height="250">
                                        <Store>
                                            <ext:Store runat="server" ID="storeScosto">
                                                <Model>
                                                    <ext:Model ID="Model12" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="codigo" />
                                                            <ext:ModelField Name="idcosto" />
                                                            <ext:ModelField Name="costo" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel6" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column16" runat="server" DataIndex="codigo" Text="Código" Align="Center"
                                                    Width="100">
                                                    <HeaderItems>
                                                        <ext:TextField ID="TextField13" runat="server">
                                                            <Listeners>
                                                                <Change Handler="this.up('grid').applyFilter();" />
                                                            </Listeners>
                                                            <Plugins>
                                                                <ext:ClearButton ID="ClearButton12" runat="server" />
                                                            </Plugins>
                                                        </ext:TextField>
                                                    </HeaderItems>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <ColumnModel ID="ColumnModel7" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column18" runat="server" DataIndex="costo" Text="Centro Costo" Align="Center"
                                                    Width="262">
                                                    <HeaderItems>
                                                        <ext:TextField ID="TextField11" runat="server">
                                                            <Listeners>
                                                                <Change Handler="this.up('grid').applyFilter();" />
                                                            </Listeners>
                                                            <Plugins>
                                                                <ext:ClearButton ID="ClearButton13" runat="server" />
                                                            </Plugins>
                                                        </ext:TextField>
                                                    </HeaderItems>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" Mode="Single">
                                                <DirectEvents>
                                                    <Select OnEvent="Select_CentroCosto">
                                                        <ExtraParams>
                                                            <ext:Parameter Name="CodigoCosto" Value="record.data['idcosto']" Mode="Raw" />
                                                            <ext:Parameter Name="NombreCosto" Value="record.data['costo']" Mode="Raw" />
                                                        </ExtraParams>
                                                        <EventMask ShowMask="true" Msg="Cargando ..." Target="Page" />
                                                    </Select>
                                                </DirectEvents>
                                            </ext:RowSelectionModel>
                                        </SelectionModel>

                                    </ext:GridPanel>
                                </Component>
                                <Listeners>
                                    <Expand Handler="this.picker.setWidth(410);" />
                                </Listeners>
                            </ext:DropDownField>

                            <ext:TextField ID="txtporcentaje" EmptyText="%" runat="server" Flex="2" Margins="0 0 10 10" AllowBlank="false" MaskRe="/[0-9]/" />
                            <ext:Button runat="server" Text="Ingresar" Icon="Add" Flex="2" Margins="0 0 10 10">
                                <Listeners>
                                    <Click Handler=" if(App.FrmCentrocosto.isValid()){App.direct.AgregarCentroCosto(
                                        #{GridPanel1}.getStore().getRecordsValues(),
                                            [
                                             App.CodigoCosto.getValue(),
                                             App.Drop2.getValue(),
                                             App.txtporcentaje.getValue()
                                        ])}
                                        else{true}" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Container>
                    <ext:GridPanel ID="GridPanel1" runat="server" Height="280" Margins="0 0 15 0" Border="true">
                        <Store>
                            <ext:Store ID="store_cCosto" runat="server">
                                <Model>
                                    <ext:Model ID="Model19" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="idcentro" />
                                            <ext:ModelField Name="codigo" />
                                            <ext:ModelField Name="centroCosto" />
                                            <ext:ModelField Name="porcentaje" Type="Int" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn runat="server" />
                                <ext:Column ID="Column11" runat="server" DataIndex="codigo" Header="CODIGO" Flex="1" />
                                <ext:Column ID="Column9" runat="server" DataIndex="centroCosto" Header="CENTRO COSTO" Flex="3" />
                                <ext:Column ID="Column1" runat="server" DataIndex="porcentaje" Header="PORCENTAJE"
                                    Width="110">
                                    <Renderer Handler="return value +'%';" />
                                </ext:Column>
                                <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                    <Commands>
                                        <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="App.GridPanel1.getStore().remove(record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                        </SelectionModel>

                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar5" runat="server" HideRefresh="true" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:FormPanel>
        </Items>
        <Listeners>
            <BeforeClose Handler="App.direct.ValidarPorcentajesCcosto(#{GridPanel1}.getStore().getRecordsValues()); " />
        </Listeners>
    </ext:Window>
    <ext:Window ID="win_Nfactura" runat="server" Title="Entradas realizadas" Modal="true" Hidden="true" Header="false"
        Layout="FitLayout" Width="610" Height="400" Resizable="false" Shadow="false" StyleSpec="border-width: 2px ;">
        <Items>
            <ext:GridPanel ID="grilla_factura" runat="server" Height="250">
                <Tools>
                    <ext:Tool ID="ToolwinFactura" Type="Close">
                        <Listeners>
                            <Click Handler="#{win_Nfactura}.hide();" />
                        </Listeners>
                    </ext:Tool>
                </Tools>
                <Store>
                    <ext:Store ID="Store_factura" runat="server" PageSize="12">
                        <Model>
                            <ext:Model ID="Model8" runat="server">
                                <Fields>
                                    <ext:ModelField Name="NCompra" />
                                    <ext:ModelField Name="Fecha" />
                                    <ext:ModelField Name="NFactura" />
                                    <ext:ModelField Name="Items" />
                                    <ext:ModelField Name="IdClase" />
                                    <ext:ModelField Name="IdSubClase" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel5" runat="server">
                    <Columns>
                        <ext:Column ID="Column17" runat="server" DataIndex="NCompra" Text="No. Compra" Align="Center"
                            Width="100">
                            <HeaderItems>
                                <ext:TextField ID="TextField10" runat="server">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton8" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                        <ext:DateColumn runat="server" ID="Column88" DataIndex="Fecha" Text="Fecha" Width="100"
                            Format="yyyy-MM-dd">
                            <HeaderItems>
                                <ext:TextField ID="TextField16" runat="server">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton9" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:DateColumn>
                        <ext:Column ID="Column14" runat="server" DataIndex="NFactura" Text="Factura" Width="100">
                            <HeaderItems>
                                <ext:TextField ID="TextField8" runat="server">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton10" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                        <ext:Column ID="Column15" runat="server" DataIndex="Items" Text="Subclase" Width="300">
                            <HeaderItems>
                                <ext:TextField ID="TextField9" runat="server">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton11" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <DirectEvents>
                    <ItemDblClick OnEvent="FacturaSelect" Before="App.win_items_Dep_Funcion.show();" After="#{ToolwinFactura}.fireEvent('click');">
                        <ExtraParams>
                            <ext:Parameter Name="Nfactura" Value="record.get('NFactura')" Mode="Raw" />
                            <ext:Parameter Name="Ncompra" Value="record.get('NCompra')" Mode="Raw" />
                            <ext:Parameter Name="Items" Value="record.get('Items')" Mode="Raw" />
                            <ext:Parameter Name="IdSubClase" Value="record.get('IdSubClase')" Mode="Raw" />
                            <ext:Parameter Name="IdClase" Value="record.get('IdClase')" Mode="Raw" />
                            <ext:Parameter Name="FechaIngreso" Value="record.get('Fecha')" Mode="Raw" />
                        </ExtraParams>
                        <EventMask ShowMask="true" Target="Page" UseMsg="false" />
                    </ItemDblClick>
                </DirectEvents>
                <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar4" runat="server" HideRefresh="true" />
                </BottomBar>
            </ext:GridPanel>
        </Items>
    </ext:Window>
    <ext:Window runat="server" ID="win_items_Dep_Funcion" Width="360" Title="ingreso Activo Detalle" Hidden="true">
        <Items>
            <ext:FormPanel runat="server">
                <Items>
                    <ext:Container runat="server" Layout="FormLayout" Padding="5">
                        <Items>
                            <ext:ComboBox runat="server" ID="cbxFact_dep"
                                Icon="SectionCollapsed"
                                EmptyText="Select"
                                ForceSelection="true"
                                Margins="0 0 0 5"
                                Flex="1"
                                LabelWidth="80"
                                FieldLabel="Dependencia"
                                ValueField="codigo"
                                DisplayField="dependencia"
                                AllowBlank="false">
                                <Store>
                                    <ext:Store runat="server" ID="store_fact_dep">
                                        <Model>
                                            <ext:Model ID="Model10" runat="server" IDProperty="codigo">
                                                <Fields>
                                                    <ext:ModelField Name="codigo" />
                                                    <ext:ModelField Name="dependencia" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server"
                                ID="cbxFact_fun"
                                Icon="SectionCollapsed"
                                Flex="1"
                                FieldLabel="Funcion"
                                ValueField="codigo"
                                DisplayField="funcion"
                                AllowBlank="false"
                                EmptyText="Select"
                                LabelWidth="80"
                                Margins="0 0 15 5">
                                <Store>
                                    <ext:Store ID="store_fact_func" runat="server" PageSize="8">
                                        <Sorters>
                                            <ext:DataSorter Property="codigo" Direction="ASC" />
                                        </Sorters>
                                        <Model>
                                            <ext:Model ID="Model11" runat="server" IDProperty="codigo">
                                                <Fields>
                                                    <ext:ModelField Name="codigo" />
                                                    <ext:ModelField Name="funcion" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
                <FooterBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" Text="ACEPTAR">
                                <Listeners>
                                    <Click Handler="App.win_items_Dep_Funcion.hide();
                                            App.direct.AgregarItemsPorFactura([
                                                               App.cbxFact_dep.getValue(),
                                                               App.cbxFact_dep.getDisplayValue(),
                                                               App.cbxFact_fun.getValue(),
                                                               App.cbxFact_fun.getDisplayValue()]
                                                               );" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </FooterBar>
            </ext:FormPanel>
        </Items>
    </ext:Window>
    <ext:Window ID="WRECUPERARALTA" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="650">
        <Items>
            <ext:GridPanel ID="GRECUPERACION" runat="server" Title="RECUPERAR ALTA" UI="Info" Border="true" Height="360">
                <Tools>
                    <ext:Tool ID="TRECUPERACION" Type="Close">
                        <Listeners>
                            <Click Handler=" #{WRECUPERARALTA}.hide(); " />
                        </Listeners>
                    </ext:Tool>
                </Tools>
                <Store>
                    <ext:Store ID="SRECUPERACION" runat="server" PageSize="12">
                        <Model>
                            <ext:Model ID="MRECUPERACION" runat="server">
                                <Fields>
                                    <ext:ModelField Name="Codigo" />
                                    <ext:ModelField Name="Rcontrol" />
                                    <ext:ModelField Name="FechaRegistro" />
                                    <ext:ModelField Name="Nombre" />
                                    <ext:ModelField Name="Observacion" />
                                    <ext:ModelField Name="idcentroeconomico" />
                                    <ext:ModelField Name="Idmotivo" />
                                    <ext:ModelField Name="IdUge" />
                                    <ext:ModelField Name="Idempleado" />
                                </Fields>
                            </ext:Model>
                        </Model>
                        <Sorters>
                            <ext:DataSorter Property="Rcontrol" Direction="DESC" />
                        </Sorters>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel8" runat="server">
                    <Columns>
                        <ext:Column ID="CRcontrol" runat="server" Text="No Ingreso" DataIndex="Rcontrol" Align="Center" Width="120">
                            <HeaderItems>
                                <ext:TextField ID="TextField123" runat="server" FieldCls="text-center">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" Buffer="100" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton14" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                        <ext:Column ID="CFecha" runat="server" Text="Fecha" DataIndex="FechaRegistro" Align="Center" Width="100">
                            <HeaderItems>
                                <ext:TextField ID="TextField14" runat="server" FieldCls="text-center">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" Buffer="100" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton17" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>

                        <ext:Column ID="CNombre" runat="server" Text="Empleado" DataIndex="Nombre" Align="Center" Flex="1">
                            <HeaderItems>
                                <ext:TextField ID="TextField12" runat="server" FieldCls="text-center">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" Buffer="100" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton15" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                        <ext:Column ID="CObservación" runat="server" Text="Observación" DataIndex="Observacion" Flex="1">
                            <HeaderItems>
                                <ext:TextField ID="TextField15" runat="server">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" />
                                    </Listeners>
                                    <Plugins>
                                        <ext:ClearButton ID="ClearButton16" runat="server" />
                                    </Plugins>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <DirectEvents>
                    <ItemDblClick OnEvent="GRRECUPERACIONALTA" After="#{TRECUPERACION}.fireEvent('click');">
                        <ExtraParams>
                            <ext:Parameter Name="HRcontrol" Value="record.get('Rcontrol')" Mode="Raw" />
                            <ext:Parameter Name="HFechaRegistro" Value="record.get('FechaRegistro')" Mode="Raw" />
                            <ext:Parameter Name="HObservacion" Value="record.get('Observacion')" Mode="Raw" />
                            <ext:Parameter Name="HNombre" Value="record.get('Nombre')" Mode="Raw" />
                            <ext:Parameter Name="HIdempleado" Value="record.get('Idempleado')" Mode="Raw" />
                            <ext:Parameter Name="HUge" Value="record.get('IdUge')" Mode="Raw" />
                            <ext:Parameter Name="HCodigo" Value="record.get('Codigo')" Mode="Raw" />
                            <ext:Parameter Name="HRcontrol" Value="record.get('Rcontrol')" Mode="Raw" />
                            <ext:Parameter Name="HIdmotivo" Value="record.get('Idmotivo')" Mode="Raw" />
                            <ext:Parameter Name="HIdcentroeconomico" Value="record.get('idcentroeconomico')" Mode="Raw" />
                        </ExtraParams>
                        <EventMask ShowMask="true" Target="Page" UseMsg="true" Msg="Consultando.." />
                    </ItemDblClick>
                </DirectEvents>
                <BottomBar>
                    <ext:PagingToolbar ID="PAGRECUPERACION" runat="server" HideRefresh="true" />
                </BottomBar>
            </ext:GridPanel>
        </Items>
    </ext:Window>
</body>
</html>
