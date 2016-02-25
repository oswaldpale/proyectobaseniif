<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatosActivo.aspx.cs" Inherits="Activos.Modulo.DatosActivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../JS/Site.js"></script>
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js" >
    </script>
    
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
    <form id="Form1" runat="server">
        <ext:Viewport ID="Viewport1" runat="server" >
             <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
            </LayoutConfig>
            <Items>
               
                <ext:Panel ID="Panel9" runat="server" Region="Center" Width="1150"   ButtonAlig="center" Title="Datos Del Activo"  Border="true" ButtonAlign="Center" UI="Info">
                    
                    <Items>
                        <ext:Container ID="Container2" runat="server"  >
                            <Items>
                                <ext:FormPanel ID="FormPanel1" runat="server" Padding="10" Border="true" Region="Center"  >
                                    <Items>
                                        <ext:Panel runat="server" Region="Center" Padding="10"  >
                                            <Items>
                                                <ext:FieldSet ID="FieldSet2" runat="server" Title="Información General" Collapsible="true" StyleSpec="padding:10px;">
                                                    <Items>
                                                        <ext:FieldContainer ID="FieldContainer15" runat="server"  Height="15">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Label ID="Label2" runat="server" Text="Placa" Flex="1" />
                                                                <ext:Label ID="Label3" runat="server" Text="Nombre" Flex="2" />
                                                                <ext:Label ID="Label14" runat="server" Text="Funcionario" Flex="3" />
                                                                <ext:Label ID="Label4" runat="server" Text="Proveedor" Flex="3" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                        <ext:Container ID="Container1" runat="server" >
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5"  />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:TriggerField ID="TriggerField1" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="1">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Search" />
                                                                    </Triggers>
                                                                    <DirectEvents>
                                                                        <TriggerClick OnEvent="TriggerField1_Click" After="#{Window1}.show();">
                                                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window1" UseMsg="false" />
                                                                        </TriggerClick>
                                                                    </DirectEvents>
                                                                </ext:TriggerField>
                                                                <ext:TextField ID="txtNombre" runat="server" ReadOnly="True" FieldCls="text-center" Flex="2" />
                                                                <ext:TextField ID="TextFuncionario" runat="server" ReadOnly="True" FieldCls="text-center" Flex="3" />
                                                                 <ext:TextField ID="TextProveedor" runat="server" ReadOnly="True" FieldCls="text-center" Flex="3" />
                                                            </Items>
                                                        </ext:Container>

                                                         <ext:FieldContainer ID="FieldContainer16" runat="server"  Height="15">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Label ID="Label26" runat="server" Text="Centro Costo" Flex="3" />
                                                                <ext:Label ID="Label27" runat="server" Text="Centro Economico" Flex="3" />
                                                                <ext:Label ID="Label28" runat="server" Text="Clase" Flex="3" />
                                                                <ext:Label ID="Label29" runat="server" Text="Subclase" Flex="3" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                        <ext:Container ID="Container7" runat="server" >
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:TextField ID="TextCentroCosto" runat="server" ReadOnly="True" FieldCls="text-center" Flex="3" />
                                                                <ext:TextField ID="TextCentroEconomico" runat="server" ReadOnly="True" FieldCls="text-center" Flex="3" />
                                                                 <ext:TextField ID="TextClase" runat="server" ReadOnly="True" FieldCls="text-center" Flex="3" />
                                                                 <ext:TextField ID="TextSubclase" runat="server" ReadOnly="True" FieldCls="text-center" Flex="3" />
                                                            </Items>
                                                        </ext:Container>

                                                          <ext:FieldContainer ID="FieldContainer18" runat="server"  Height="15">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Label ID="Label22" runat="server" Text="Unidad Generado De Efectivo" Flex="2" />
                                                                <ext:Label ID="Label7" runat="server" Text="Fecha Ingreso" Flex="1" />
                                                                <ext:Label ID="Label21" runat="server" Text="Fecha Alta" Flex="1" />
                                                               
                                         
                                                                <ext:Label ID="Label6" runat="server" Text="Fecha Depreciación" Flex="1" />
                                                                <ext:Label ID="Label25" runat="server" Text="Fecha Baja" Flex="1" />
                                                            
                                                            </Items>
                                                        </ext:FieldContainer>
                                                          <ext:Container ID="Container4" runat="server" Height="30">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5"   />
                                                            </LayoutConfig>
                                                              <Items>
                                                                   <ext:TextField ID="TextUge" runat="server"   ReadOnly="true" FieldCls="text-center" Flex="2"  />
                                                                   <ext:DateField ID="TextFechaIngreso" runat="server"  Format="yyyy-MM-dd" ReadOnly="true" FieldCls="text-center" Flex="1"  />
                                                                   <ext:DateField ID="TextFechaAlta" runat="server"  Format="yyyy-MM-dd" ReadOnly="true" FieldCls="text-center" Flex="1"  />
                                                                   <ext:DateField ID="TextFechaDepreciacion" runat="server"  Format="yyyy-MM-dd" ReadOnly="true" FieldCls="text-center" Flex="1"  />
                                                                   <ext:DateField ID="TextFechaBaja" runat="server" Format="yyyy-MM-dd" ReadOnly="true" FieldCls="text-center" Flex="1" />
                                                              </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:FieldSet>
                                                <ext:FieldSet ID="FieldSet3" runat="server" Title="Propiedades" Collapsible="true" Padding="5" Collapsed="true">
                                                    <Items>
                                                        <ext:FieldContainer ID="FieldContainer7" runat="server" Height="15">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Label ID="Label8" runat="server" Text="Funcion" Flex="1" />

                                                                <ext:Label ID="Label10" runat="server" Text="Ubicación" Flex="1" />
                                                                <ext:Label ID="Label17" runat="server" Text="Propiedad" Flex="1" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                        <ext:FieldContainer ID="FieldContainer8" runat="server">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:TextField ID="txtfuncion" runat="server" Name="funcion" Flex="1" ReadOnly="True" FieldCls="text-center" />

                                                                <ext:TextField ID="txtubicacion" runat="server" Name="ubicacion" Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtPropiedad" runat="server" Name="Propiedad" Flex="1" ReadOnly="True" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                        <ext:FieldContainer ID="FieldContainer9" runat="server" Height="15">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:Label ID="Label11" runat="server" Text="Tipo Activo" Flex="1" />
                                                                <ext:Label ID="Label12" runat="server" Text="Estado" Flex="1" />
                                                                <ext:Label ID="Label13" runat="server" Text="Condicion" Flex="1" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                        <ext:FieldContainer ID="FieldContainer10" runat="server">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:TextField ID="txtTipoActivo" runat="server" Name="TipoActivo" Flex="1" FieldCls="text-center" ReadOnly="True" />
                                                                <ext:TextField ID="txtEstado" runat="server" Name="Estado" Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtCondicion" runat="server" Name="Condicion" Flex="1" ReadOnly="True" FieldCls="text-center" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                       
                                                    </Items>
                                                </ext:FieldSet>
                                                <ext:FieldSet ID="FieldSet1" runat="server" Title="Activo por NIIF" Collapsible="true">
                                                    <Items>
                                                        <ext:FieldContainer ID="FieldContainer1" runat="server" Height="15">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                 <ext:Label ID="Label1" runat="server" Text="Costo Inicial" Flex="1" />
                                                                <ext:Label ID="Label5" runat="server" Text="Valor Residual" Flex="1" />
                                                                <ext:Label ID="Label9" runat="server" Text="Valor Deterioro" Flex="1" />
                                                                <ext:Label ID="Label15" runat="server" Text="Valor Razonable " Flex="1" />
                                                                <ext:Label ID="Label20" runat="server" Text="Ajuste Depreciación Acumulada " Flex="1" />
                                                                <ext:Label ID="Label16" runat="server" Text="Depreciación Acum" Flex="1" />
                                                                <ext:Label ID="Label18" runat="server" Text="Importe Depreciable" Flex="1" />
                                                                <ext:Label ID="Label19" runat="server" Text="Importe Libros" Flex="1" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                        <ext:FieldContainer ID="FieldContainer2" runat="server">
                                                            <LayoutConfig>
                                                                <ext:HBoxLayoutConfig DefaultMargins="1.5" />
                                                            </LayoutConfig>
                                                            <Items>
                                                                <ext:TextField ID="txtCostoInicial" runat="server" Name="CostoInicial" Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtresidual" runat="server" Name="vresidual" Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtdeterioro" runat="server"  Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtrazonable" runat="server" Name="vrazonable" Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtajusdepAcum" runat="server"  Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtdepreciado" runat="server" Name="vdepreciado" Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtImporteDepreciable" runat="server" Name="vimportedep" Flex="1" ReadOnly="True" />
                                                                <ext:TextField ID="txtimporte" runat="server" Name="vimporte" Flex="1" ReadOnly="True" />
                                                            </Items>
                                                        </ext:FieldContainer>
                                                    </Items>
                                                </ext:FieldSet>
                                                <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" Border="false" Plain="True" PaddingSpec="10 0 0" Hidden="false">
                                                    <Items>
                                                        <ext:Panel ID="Panel11" runat="server" Title="Caracteristicas" UI="Info">
                                                            <Items>
                                                                <ext:FormPanel ID="FormPanel2" runat="server" ButtonAlign="Center">
                                                                    <Items>
                                                                        <ext:GridPanel ID="GridPanel4" runat="server" Flex="1" Height="250" Padding="6">
                                                                            <Store>
                                                                                <ext:Store ID="store_caracteristica" runat="server">
                                                                                    <Model>
                                                                                        <ext:Model ID="Model1" runat="server" IDProperty="codigo">
                                                                                            <Fields>
                                                                                                <ext:ModelField Name="codigo" />
                                                                                                <ext:ModelField Name="caracteristica" />
                                                                                                <ext:ModelField Name="valor" />

                                                                                            </Fields>
                                                                                        </ext:Model>
                                                                                    </Model>
                                                                                </ext:Store>
                                                                            </Store>
                                                                            <ColumnModel ID="ColumnModel5" runat="server">
                                                                                <Columns>
                                                                                    <ext:Column ID="Column14" runat="server" DataIndex="caracteristica" Header="Caracteristica"
                                                                                        Flex="4">
                                                                                    </ext:Column>
                                                                                    <ext:Column ID="Column11" runat="server" DataIndex="valor" Header="Valor"
                                                                                        Flex="2">
                                                                                    </ext:Column>
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
                                                        </ext:Panel>
                                                        <ext:Panel ID="Panel4" runat="server" Title="NIIC por Componentes" UI="Info">
                                                            <Items>
                                                                <ext:FormPanel ID="FormPanel3" runat="server" ButtonAlign="Center" Disabled="true">
                                                                </ext:FormPanel>
                                                                <ext:GridPanel ID="GridPanel3" runat="server" Flex="1" Height="250" Padding="6" Border="true">
                                                                    <Store>
                                                                        <ext:Store ID="store_componente" runat="server">
                                                                            <Model>
                                                                                <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                                                    <Fields>
                                                                                        <ext:ModelField Name="descripcion" />
                                                                                        <ext:ModelField Name="TipoDepreciacion" />
                                                                                        <ext:ModelField Name ="vutil" />
                                                                                        <ext:ModelField Name="porcentaje" />
                                                                                        <ext:ModelField Name="costo" />
                                                                                        <ext:ModelField Name="Residual" />
                                                                                        <ext:ModelField Name="Deterioro" />
                                                                                        <ext:ModelField Name="Revaluacion" />
                                                                                        <ext:ModelField Name="AjusteDepreciacionAcumulada" />
                                                                                        <ext:ModelField Name="ReexpresionVidaUtil" />
                                                                                        <ext:ModelField Name="Basedepreciable" />
                                                                                        <ext:ModelField Name="DepreciacionMes" />
                                                                                        <ext:ModelField Name="DepreciacionAcumulada" />
                                                                                        <ext:ModelField Name="SaldoDepreciable" />
                                                                                        <ext:ModelField Name="ImporteLibros" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <ColumnModel ID="ColumnModel1" runat="server">
                                                                        <Columns>
                                                                            <ext:Column ID="Column2" runat="server" DataIndex="descripcion" Header="Descripcion"
                                                                                Width="250" />
                                                                             <ext:Column ID="Column30" runat="server" DataIndex="TipoDepreciacion" Header="Tipo Depreciación"
                                                                                Width="200" />
                                                                             <ext:Column ID="Column31" runat="server" DataIndex="vutil" Header="Vida Util"
                                                                                Width="100" />
                                                                            <ext:Column ID="Column22" runat="server" DataIndex="porcentaje" Header="Porcentaje"
                                                                                Width="80">
                                                                                <Renderer Handler="return value +'%';" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column3" runat="server" DataIndex="costo" Header="Costo Inicial"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column16" runat="server" DataIndex="Residual" Header="Valor Residual"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column6" runat="server" DataIndex="Deterioro" Header="Valor Deterioro"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column4" runat="server" DataIndex="Revaluacion" Header="Revaluación"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column7" runat="server" DataIndex="AjusteDepreciacionAcumulada" Header="Ajuste Depreciación Acum"
                                                                                Width="190">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column12" runat="server" DataIndex="reexpresionVidaUtil" Header="Reexpresión Vida Util"
                                                                                Width="160">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column15" runat="server" DataIndex="Basedepreciable" Header="Base Depreciable"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column13" runat="server" DataIndex="DepreciacionMes" Header="Depreciación Mes"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column8" runat="server" DataIndex="DepreciacionAcumulada" Header="Depreciación Acumulada"
                                                                                Width="180">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column32" runat="server" DataIndex="SaldoDepreciable" Header="Saldo Depreciable"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column9" runat="server" DataIndex="ImporteLibros" Header="Importe Libros"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                        </Columns>
                                                                    </ColumnModel>
                                                                    <SelectionModel>
                                                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                                                                    </SelectionModel>
                                                                    <BottomBar>
                                                                        <ext:PagingToolbar ID="PagingToolbar1" runat="server" HideRefresh="true" />
                                                                    </BottomBar>
                                                                </ext:GridPanel>

                                                            </Items>
                                                        </ext:Panel>
                                                         <ext:Panel ID="Panel1" runat="server" Title="Activo por Norma Tributaria" UI="Info">
                                                            <Items>
                                                                <ext:FormPanel ID="FormPanel4" runat="server" ButtonAlign="Center" Disabled="true">
                                                                </ext:FormPanel>
                                                                    <ext:GridPanel ID="GridPanel5" runat="server" Flex="1" Height="250" Padding="6" Border="true">
                                                                    <Store>
                                                                        <ext:Store ID="store1" runat="server">
                                                                            <Model>
                                                                                <ext:Model ID="Model3" runat="server" IDProperty="codigo">
                                                                                    <Fields> 
                                                                                        <ext:ModelField Name="descripcion" />
                                                                                        <ext:ModelField Name="TipoDepreciacion" />
                                                                                        <ext:ModelField Name ="vutil" />
                                                                                        <ext:ModelField Name="porcentaje" />
                                                                                        <ext:ModelField Name="costo" />
                                                                                        <ext:ModelField Name="Residual" />
                                                                                        <ext:ModelField Name="Deterioro" />
                                                                                        <ext:ModelField Name="Revaluacion" />
                                                                                        <ext:ModelField Name="AjusteDepreciacionAcumulada" />
                                                                                        <ext:ModelField Name="ReexpresionVidaUtil" />
                                                                                        <ext:ModelField Name="Basedepreciable" />
                                                                                        <ext:ModelField Name="DepreciacionMes" />
                                                                                        <ext:ModelField Name="DepreciacionAcumulada" />
                                                                                        <ext:ModelField Name="SaldoDepreciable" />
                                                                                        <ext:ModelField Name="ImporteLibros" />
                                                                                    </Fields>
                                                                                </ext:Model>
                                                                            </Model>
                                                                        </ext:Store>
                                                                    </Store>
                                                                    <ColumnModel ID="ColumnModel3" runat="server">
                                                                        <Columns>
                                                                            <ext:Column ID="Column17" runat="server" DataIndex="descripcion" Header="Descripcion"
                                                                                Width="250" />
                                                                             <ext:Column ID="Column18" runat="server" DataIndex="TipoDepreciacion" Header="Tipo Depreciación"
                                                                                Width="200" />
                                                                             <ext:Column ID="Column19" runat="server" DataIndex="vutil" Header="Vida Util"
                                                                                Width="100" />
                                                                            <ext:Column ID="Column20" runat="server" DataIndex="porcentaje" Header="Porcentaje"
                                                                                Width="80">
                                                                                <Renderer Handler="return value +'%';" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column21" runat="server" DataIndex="costo" Header="Costo Inicial"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column23" runat="server" DataIndex="Residual" Header="Valor Residual"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column24" runat="server" DataIndex="Deterioro" Header="Valor Deterioro"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column25" runat="server" DataIndex="Revaluacion" Header="Revaluación"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column26" runat="server" DataIndex="AjusteDepreciacionAcumulada" Header="Ajuste Depreciación Acum"
                                                                                Width="190">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column27" runat="server" DataIndex="reexpresionVidaUtil" Header="Reexpresión Vida Util"
                                                                                Width="160">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column28" runat="server" DataIndex="Basedepreciable" Header="Base Depreciable"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column29" runat="server" DataIndex="DepreciacionMes" Header="Depreciación Mes"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column33" runat="server" DataIndex="DepreciacionAcumulada" Header="Depreciación Acumulada"
                                                                                Width="180">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column34" runat="server" DataIndex="SaldoDepreciable" Header="Saldo Depreciable"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column35" runat="server" DataIndex="ImporteLibros" Header="Importe Libros"
                                                                                Width="140">
                                                                                <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                                            </ext:Column>
                                                                        </Columns>
                                                                    </ColumnModel>
                                                                    <SelectionModel>
                                                                        <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" Mode="Single" />
                                                                    </SelectionModel>
                                                                    <BottomBar>
                                                                        <ext:PagingToolbar ID="PagingToolbar3" runat="server" HideRefresh="true" />
                                                                    </BottomBar>
                                                                </ext:GridPanel>

                                                            </Items>
                                                        </ext:Panel>

                                                    </Items>
                                                </ext:TabPanel>
                                           </Items>
                                       </ext:Panel>
                                    </Items>
                                </ext:FormPanel>
                                
                             
                                    
                                
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
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
                                <ext:Model ID="Model2" runat="server" IDProperty="codigo">
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
                            <ext:Column ID="Column5" runat="server" Text="Codigo" DataIndex="codigo" Align="Center" Width="120">
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
                           
                            <ext:Column ID="Column1" runat="server" Text="Placa" DataIndex="placa" Align="Center" Width="120">
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
                        <ItemDblClick OnEvent="GridComodato_SelectRow" Before="#{FormPanel1}.getForm().reset();" After="#{Tool2}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="NComodato" Value="record.get('placa')" Mode="Raw" />
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

    </form>
</body>
</html>
