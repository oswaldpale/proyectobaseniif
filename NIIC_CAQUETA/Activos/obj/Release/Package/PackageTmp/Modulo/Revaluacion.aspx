<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Revaluacion.aspx.cs" Inherits="Activos.Modulo.Revaluacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Revaluacion</title>
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
    <link href="../Estilos/Estilo.css" rel="stylesheet" />
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>
    <script src="JS/FormatoPeso.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
        <ext:Hidden ID="codigo" runat="server" />
        <ext:Hidden ID="idresponsable" runat="server" />
        <ext:Hidden ID="FechaURevaluacion" runat="server" />
        <ext:Hidden ID="vrdeterioro" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
            </LayoutConfig>
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Width="720" Height="570" Border="true"
                    TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                    <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                    <Items>
                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:Label ID="LRECUPERAR" runat="server" Text="No. Revaluación" Width="100" Margins="5 0 0 5" />
                                <ext:Label ID="Label3" runat="server" Html="Fecha<font color ='red'>*</font>" Flex="1" Margins="5 0 0 5" />
                                <ext:Label runat="server" Text="Archivo:" Width="80" Flex="3" Margins="5 0 0 5" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="TRECUPERARA" runat="server" Width="100" ReadOnly="true" FieldStyle="text-align:right;" Margins="5 0 0 5"
                                    Selectable="false" Icon="Zoom" FieldCls="CampoReservado">
                                    <Listeners>
                                        <IconClick Handler=" App.WRECUPERARREVALUACION.show();" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:DateField ID="dfd_fecha" runat="server" Editable="false" FieldStyle="text-align: center; "
                                    Margins="5 0 0 5" Flex="1" Format="yyyy-MM-dd" AllowBlank="false" />


                                <ext:TextField runat="server" ID="txt_separador_c" MaxLengthText="1" MaxLength="1" Text=";" EnforceMaxLength="true" Width="30" Margins="10 0 0 0" Hidden="true" />
                                <ext:TextField runat="server" ID="txt_separador_f" MaxLengthText="2" MaxLength="2" Text="\n" ReadOnly="true" Selectable="false"
                                    EnforceMaxLength="true" Width="30" Margins="10 0 0 0" Hidden="true" />
                                <ext:FileUploadField
                                    ID="FileUploadField1"
                                    runat="server" AllowBlank="false"
                                    EmptyText="Seleccione un archivo..."
                                    ButtonText="" AnchorHorizontal="100%" Width="310"
                                    Icon="PageGo" Margins="5 0 5 5">
                                    <Listeners>
                                        <Change Handler="if(/.txt/.test(this.getValue()) || /.csv/.test(this.getValue()) ){ App.SaveButton.setDisabled(false);}else { App.SaveButton.setDisabled(true); alert('Archino no válido, solo se perimiten archivos .txt o .csv');}" />
                                    </Listeners>
                                </ext:FileUploadField>
                                <ext:Button runat="server" Width="120" Text="Ingresar" Icon="Add" Margins="5 0 0 5">
                                    <Listeners>
                                        <Click Handler=" App.direct.CargarArchivo();" />
                                    </Listeners>
                                </ext:Button>

                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextArea ID="txt_conceptos" runat="server" Margins="5 0 10 5" AllowBlank="true" Flex="1" EmptyText="Observaciones.."
                                    MaxLength="1999" EnforceMaxLength="true">
                                </ext:TextArea>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container4" runat="server">
                            <Items>
                                <ext:Panel ID="Panel1" runat="server" Flex="1" Padding="2">
                                    <Items>
                                        <ext:GridPanel ID="GREVALUACION" runat="server" Flex="1" Margins="5 0 0 5" Height="350" Border="true">
                                            <Store>
                                                <ext:Store ID="SREVALUACION" runat="server">
                                                    <Model>
                                                        <ext:Model runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="id" />
                                                                <ext:ModelField Name="valor" />
                                                                <ext:ModelField Name="placa" />
                                                                <ext:ModelField Name="nombre" />
                                                                <ext:ModelField Name="basedepreciable" />
                                                                <ext:ModelField Name="importelibros" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel1" runat="server">
                                                <Columns>

                                                    <ext:Column ID="Column8" runat="server" DataIndex="placa" Header="Placa" Width="80">
                                                    </ext:Column>
                                                    <ext:Column ID="Column1" runat="server" DataIndex="nombre" Header="Descripción" Flex="2" />
                                                    <ext:Column ID="Column9" runat="server" DataIndex="valor" Header="Revaluación" Width="120">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>

                                                    <ext:Column ID="Column13" runat="server" DataIndex="basedepreciable" Header="Base Depreciable" Width="125">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>

                                                    <ext:Column ID="Column4" runat="server" DataIndex="importelibros" Header="Importe Libros" Width="120">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server" />
                                            </SelectionModel>
                                            <Plugins>
                                                <ext:CellEditing ID="CellEditing1" runat="server">
                                                </ext:CellEditing>
                                            </Plugins>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar3" runat="server" />
                                            </BottomBar>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Container>
                    </Items>
                    <FooterBar>
                        <ext:Toolbar runat="server" ID="asdas">
                            <Items>
                                <ext:Button runat="server" ID="btn_reporte" Text="ANULAR" Icon="Delete" Scale="Small" UI="Default" Margins="5 0 0 5">
                                    <Listeners>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill />
                                <ext:Button ID="btn_guardar" runat="server" Text="GUARDAR" Disabled="false"
                                    Icon="Disk" ToolTip="Guardar Revaluacion.">
                                    <Listeners>
                                        <Click Handler="
                                              if (#{FormPanel1}.getForm().isValid()) { App.direct.GrabarRevaluacion ();}else{'true'} " />
                                    </Listeners>
                                </ext:Button>
                                <%--<ext:Button ID="btn_limpiar" runat="server" Text="LIMPIAR" Width="100" Margins="5 0 0 5" Icon="Erase">
                                    <Listeners>
                                        <Click Handler="App.FormPanel1.reset();Ext.ComponentQuery.query('#GridPanel1')[0].getStore().removeAll();" />
                                    </Listeners>
                                </ext:Button>--%>
                            </Items>
                        </ext:Toolbar>
                    </FooterBar>


                    <%--*************************************************--%>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
           <ext:Window ID="WRECUPERARREVALUACION" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="500">
            <Items>
                <ext:GridPanel ID="GRECUPERACION" runat="server" Title="RECUPERAR REVALUACIÓN" UI="Info" Border="true" Height="340">
                    <Tools>
                        <ext:Tool ID="TRECUPERACION" Type="Close">
                            <Listeners>
                                <Click Handler=" #{WRECUPERARREVALUACION}.hide(); " />
                            </Listeners>
                        </ext:Tool>
                    </Tools>
                    <Store>
                        <ext:Store ID="SRECUPERACION" runat="server" PageSize="12">
                            <Model>
                                <ext:Model ID="MRECUPERACION" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Ncontrol" />
                                        <ext:ModelField Name="Fecha" />
                                        <ext:ModelField Name="TipoDeterioro" />
                                        <ext:ModelField Name="MotivoDeterioro" />
                                        <ext:ModelField Name="Observacion" />
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
                            <ext:Column ID="CRcontrol" runat="server" Text="No Ingreso" DataIndex="Ncontrol" Align="Center" Width="120">
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
                            <ext:Column ID="CFecha" runat="server" Text="Fecha Ingreso" DataIndex="Fecha" Align="Center" Width="100">
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

                            <ext:Column ID="CObservacion" runat="server" Text="Observación" DataIndex="Observacion" Align="Center" Flex="1">
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
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                       <%-- <ItemDblClick OnEvent="GRRECUPERACIONREVALUACION" After="#{TRECUPERACION}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="HNcontrol" Value="record.get('Ncontrol')" Mode="Raw" />
                                <ext:Parameter Name="HFecha" Value="record.get('Fecha')" Mode="Raw" />
                                <ext:Parameter Name="HObservacion" Value="record.get('Observacion')" Mode="Raw" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Target="Page" UseMsg="true" Msg="Consultando.." />
                        </ItemDblClick>--%>
                    </DirectEvents>
                    <BottomBar>
                        <ext:PagingToolbar ID="PAGRECUPERACION" runat="server" HideRefresh="true" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Window>
    </form>
</body>
</html>

