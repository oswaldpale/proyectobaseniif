<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambioMetodoDepreciacion.aspx.cs" Inherits="Activos.Modulo.Ajustes.CambioMetodoDepreciacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>CAMBIO DE MÉTODO DE LA DEPRECIACIÓN</title>

     <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="idclase" runat="server" />
        <ext:Hidden ID="clase" runat="server" />
        <ext:Hidden ID="idsubclase" runat="server" />
        <ext:Hidden ID="subclase" runat="server" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
        <ext:Viewport ID="Viewport1" runat="server">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
            </LayoutConfig>
            
            <Items>
                <ext:FormPanel
                    ID="FormCambioMetodoDepreciacion"
                    Width="1150"
                    Title="Cambio De Método Depreciación"
                    Border="true"
                    Frame="true"
                    runat="server">
                    <FieldDefaults LabelAlign="Right" LabelWidth="50" MsgTarget="Side" />
                    <Items>
                        <ext:Panel ID="Panel1" runat="server" Flex="1" Padding="5">
                            <Items>
                                <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:Label ID="Label4" runat="server" Text="Fecha" Width="130" Enabled="true" Margins="10 0 0 10" />
                                        <ext:Label ID="lbl_subclase" runat="server" Text="Subclase" Flex="1" Enabled="true"
                                            Margins="10 0 0 10" />
                                        <ext:Label ID="Label1" runat="server" Text="Componente" Flex="1" Enabled="true" Margins="10 0 0 10" />
                                        <ext:Label ID="Label3" runat="server" Text="Tipo Método Depreciación" Flex="1" Enabled="true" Margins="10 0 0 10" />
                                        <ext:Label ID="Label2" runat="server" Text="Vida Util" Width="80" Enabled="true" Margins="10 0 0 10" />
                                        <ext:Label ID="Label5" runat="server" Text="Medida" Width="210" Enabled="true" Margins="10 0 0 10" />
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                     <Items>
                                         <ext:DateField ID="FechaCambio" runat="server" Editable="false" FieldStyle="text-align: center; "
                                            Margins="5 0 5 5" Width="130" Format="yyyy-MM-dd" AllowBlank="false" />
                                         <ext:TriggerField ID="TriggerListaSubclase" runat="server" Name="Buscar" TextAlign="Center" Disabled="false" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="1" Hidden="false"  Margins="5 0 5 5">
                                             <Triggers>
                                                 <ext:FieldTrigger Icon="Search" />
                                             </Triggers>
                                             <Listeners>
                                                 <TriggerClick Handler="
                                                                    App.direct.CargarSubClases();
                                                                    App.Windows_subclase.show();
                                                               " />
                                             </Listeners>
                                         </ext:TriggerField>
                                          <ext:ComboBox runat="server" ID="cbx_componente" Icon="SectionCollapsed" AllowBlank="false" 
                                            ForceSelection="true" Margins="5 0 5 5" Flex="1" DisplayField="nombre_componente" ValueField="IdComponente">
                                            <Store>
                                                <ext:Store runat="server" ID="store_Componente">
                                                    <Model>
                                                        <ext:Model ID="Model3" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="IdComponente" />
                                                                <ext:ModelField Name="nombre_componente" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>
                                            <ext:ComboBox runat="server" ID="cbx_Metodo" Icon="SectionCollapsed" AllowBlank="false" 
                                            ForceSelection="true" Margins="5 0 5 5" Flex="1" DisplayField="MetodoDepreciacion" ValueField="IdMetodoDepreciacion">
                                            <Store>
                                                <ext:Store runat="server" ID="store_metodo">
                                                    <Model>
                                                        <ext:Model ID="Model2" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="IdMetodoDepreciacion" />
                                                                <ext:ModelField Name="MetodoDepreciacion" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>
                                         <ext:TextField ID="CantVidaUtil" runat="server" Width="80"  Margins="5 0 5 5"  AllowBlank="false" MaskRe="/[0-9]/" />

                                         <ext:ComboBox runat="server" ID="cbx_medida" Icon="SectionCollapsed"
                                             ForceSelection="true" Margins="5 0 5 5" DisplayField="UnidadMedida" ValueField="codigo"
                                             Width="150" AllowBlank="false">
                                             <Store>
                                                 <ext:Store runat="server" ID="store_medida">
                                                     <Model>
                                                         <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                             <Fields>
                                                                 <ext:ModelField Name="codigo" />
                                                                 <ext:ModelField Name="UnidadMedida" />
                                                             </Fields>
                                                         </ext:Model>
                                                     </Model>
                                                 </ext:Store>
                                             </Store>
                                         </ext:ComboBox>

                                         <ext:Button runat="server" ID="btnAjustar" Text="Aplicar" Width="60" Margins="5 0 5 5"  FormBind="true">
                                             <Listeners>
                                                 <Click Handler="if(App.FormCambioMetodoDepreciacion.isValid())
                                                                 {
                                                                    
                                                                      App.direct.AplicarAjuste(App.cbx_componente.getValue(),
                                                                                              App.cbx_Metodo.getValue(),
                                                                                              App.cbx_Metodo.displayField,
                                                                                              App.CantVidaUtil.getValue())
                                                     
                                                                 }else{
                                                                    return true; 
                                                                }           
                                                     
                                                     " />
                                             </Listeners>
                                         </ext:Button>
                                     </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="PanelCambioVidaUtil" runat="server" Flex="1" Padding="5">
                            <Items>
                                <ext:GridPanel ID="gridCambioMetodo" runat="server" Flex="1"  Border="true" Height="400">
                                    <Store>
                                        <ext:Store ID="store_CambioMetodo" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="IdActivo" />
                                                        <ext:ModelField Name="Activo" />
                                                        <ext:ModelField Name="IdComponente" />
                                                        <ext:ModelField Name="IdsubComponente" />
                                                        <ext:ModelField Name="nombre_componente" />
                                                        <ext:ModelField Name="Porcentaje_ci" />
                                                        <ext:ModelField Name="IDdepreciacion" />
                                                        <ext:ModelField Name="Depreciacion" />
                                                        <ext:ModelField Name="vida_util" Type="Float" />
                                                        <ext:ModelField Name="vida_util_remanente" Type="Float" />
                                                        <ext:ModelField Name="vida_util_utilizado" Type="Float" />
                                                        <ext:ModelField Name="AjusteDepreciacionAcumulada" Type="Float" />
                                                        <ext:ModelField Name="AjusteResidual" Type="Float" />
                                                        <ext:ModelField Name="AjusteDeterioro" Type="Float" />
                                                        <ext:ModelField Name="AjusteRazonable" Type="Float" />
                                                        <ext:ModelField Name="BaseDepreciable" Type="Float" />
                                                        <ext:ModelField Name="DepreciacionMes" Type="Float" />
                                                        <ext:ModelField Name="DepreciacionAcumulada" Type="Float" />
                                                        
                                                        <ext:ModelField Name="ImporteLibros" Type="Float" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel>
                                        <Columns>
                                            
                                            <ext:Column ID="Column1" runat="server" Text="Activo" DataIndex="Activo"
                                                Width="200">
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
                                            <ext:Column ID="Column26" runat="server" Text="Componentes" DataIndex="nombre_componente"
                                                Width="180" />
                                            <ext:Column ID="Column27" runat="server" Text="Tipo Depreciacion" Width="180" DataIndex="Depreciacion">
                                            </ext:Column>
                                            <ext:Column ID="Column28" runat="server" DataIndex="vida_util" Header="Vida Util" Hidden="false"
                                                Width="70" />
                                            <ext:Column ID="Column25" runat="server" DataIndex="vida_util_utilizado" Header="Ejecutada" Hidden="false"
                                                Width="90" />
                                            <ext:Column ID="Column10" runat="server" DataIndex="vida_util_remanente" Header="Remanente" Hidden="false"
                                                Width="90" />
                                            
                                             <ext:Column ID="Column3" runat="server" DataIndex="AjusteResidual" Header="Residual" Hidden="true"
                                                Width="140">
                                                    <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                </ext:Column>
                                              <ext:Column ID="Column4" runat="server" DataIndex="AjusteDeterioro" Header="Deterioro" Hidden="true"
                                                Width="140" >
                                                  <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                             </ext:Column>
                                             <ext:Column ID="Column5" runat="server" DataIndex="BaseDepreciable" Header="Base Depreciable" Hidden="false"
                                                Width="140">
                                                 <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                            </ext:Column>
                                            <ext:Column ID="Column6" runat="server" DataIndex="DepreciacionMes" Header="Depreciación Mes" Hidden="false"
                                                Width="140">
                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                            </ext:Column>
                                            <ext:Column ID="Column7" runat="server" DataIndex="DepreciacionAcumulada" Header="Depreciación Acum" Hidden="false"
                                                Width="140">
                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                            </ext:Column>
                                       
                                            <ext:Column ID="Column9" runat="server" DataIndex="ImporteLibros" Header="Importe Libros" Hidden="false"
                                                Width="140" >
                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                            </ext:Column>

                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" Mode="Single" />
                                    </SelectionModel>
                                    <Features>
                                        <ext:Grouping ID="Group1" runat="server" GroupHeaderTplString="{name}" HideGroupedHeader="true"
                                            EnableGroupingMenu="false" />
                                    </Features>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="10" HideRefresh="true">
                                        </ext:PagingToolbar>
                                    </BottomBar>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <FooterBar>
                        <ext:Toolbar runat="server" ID="Toolbar2" Border="true">
                            <Items>
                                <ext:ToolbarFill />
                                 <ext:Button runat="server" ID="BtnGuardar" Text="GUARDAR">
                                    <Listeners>
                                        <Click Handler="App.direct.GrabarCambiosAplicados(
                                                                    App.cbx_componente.getValue(),
                                                                    App.cbx_Metodo.getValue(),
                                                                    App.cbx_medida.getValue(),
                                                                    App.CantVidaUtil.getValue()
                                                                                       );"
                                         />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" ID="BtnLimpiar" Text="LIMPIAR">
                                    <Listeners>
                                        <Click Handler="App.FormCambioMetodoDepreciacion.reset();
                                                        App.gridCambioMetodo.getStore().removeAll();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </FooterBar>
                </ext:FormPanel>
                <ext:Window ID="Windows_subclase" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="650">
        <Items>
            <ext:GridPanel ID="grilla_subclases" runat="server" Height="400" Border="true" Title="Subclase">
                <Tools>
                    <ext:Tool ID="Tool4" Type="Close">
                        <Listeners>
                            <Click Handler="#{Windows_subclase}.hide();" />
                        </Listeners>
                    </ext:Tool>
                </Tools>
                <Store>
                    <ext:Store ID="StoreSubclase" runat="server" PageSize="9">
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
                        <ext:Column ID="Column42" runat="server" DataIndex="Codigo" Text="Código" Align="Center"
                            Width="60">
                        </ext:Column>
                        <ext:Column ID="Column43" runat="server" DataIndex="Clase" Text="Clase" Flex="1">
                            <HeaderItems>
                                <ext:TextField ID="TextField3" runat="server" FieldStyle="text-align: left;" EnableKeyEvents="true">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" Buffer="500" />
                                    </Listeners>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                        <ext:Column ID="Column44" runat="server" DataIndex="Nombre" Text="SubClase" Flex="1">
                            <HeaderItems>
                                <ext:TextField ID="TextField8" runat="server" EnableKeyEvents="true">
                                    <Listeners>
                                        <Change Handler="this.up('grid').applyFilter();" Buffer="500" />
                                    </Listeners>
                                </ext:TextField>
                            </HeaderItems>
                        </ext:Column>
                    </Columns>
                </ColumnModel>

                <DirectEvents>
                    <ItemDblClick OnEvent="GridComodato_SelectSubclase" After="#{Tool4}.fireEvent('click');">
                        <ExtraParams>
                            <ext:Parameter Name="Codigo" Value="record.get('Codigo')" Mode="Raw" />
                            <ext:Parameter Name="Nombre" Value="record.get('Nombre')" Mode="Raw" />
                            <ext:Parameter Name="Clase" Value="record.get('Clase')" Mode="Raw" />
                            <ext:Parameter Name="IdClase" Value="record.get('IdClase')" Mode="Raw" />
                        </ExtraParams>
                        <EventMask ShowMask="true" Target="Page" Msg="Cargandoo" />
                    </ItemDblClick>
                </DirectEvents>
                <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar6" runat="server" HideRefresh="true" />
                </BottomBar>
            </ext:GridPanel>
        </Items>
    </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>