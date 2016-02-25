<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValorResidual.aspx.cs" Inherits="Activos.Modulo.ValorResidual" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VALOR RESIDUAL</title>
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>
    <script src="JS/FormatoPeso.js"></script>
    <style type="text/css">
        /**/
        #unlicensed {
            display: none !important;
        }
    </style>

    <style>
        .x-grid-body .x-grid-cell-valorResidualActual {
            background-color: #f1f2f4;
            font-weight: bolder;
            font-size: 11px;
            background-color: #f1f2f4;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
        <ext:Hidden ID="codigo" runat="server" />
        <ext:Hidden ID="idempleado" runat="server" />
        <ext:Hidden ID="nombreActivo" runat="server" />
        <ext:Hidden ID="idplaca" runat="server" />
        <ext:Hidden ID="FechaUltimoDoc" runat="server" />
        <ext:Hidden ID="FechaUResidual" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
            </LayoutConfig>
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Width="720" Height="570" Border="true"
                    TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                    <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                    <Items>

                        <ext:Container runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:Label runat="server" Text="No. Residual" Width="100" Margins="5 0 0 5" />
                                <ext:Label runat="server" Html="Fecha<font color ='red'>*</font>" Flex="1" Margins="5 0 0 5" />
                                <ext:Label runat="server" Text="Archivo:" Width="80" Flex="3" Margins="5 0 0 5" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="TRECUPERARA" runat="server" Width="100" ReadOnly="true" FieldStyle="text-align:right;" Margins="5 0 0 5"
                                    Selectable="false" Icon="Zoom" FieldCls="CampoReservado">
                                    <Listeners>
                                        <IconClick Handler=" App.WRECUPERARRESIDUAL.show();" />
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
                        
                        <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextArea ID="txt_conceptos" runat="server" Margins="5 0 10 5" Flex="1" EmptyText="Observación."
                                    MaxLength="1999" EnforceMaxLength="true">
                                </ext:TextArea>
                            </Items>
                        </ext:Container>
                        <%--- Detalle Componentes -----%>
                        <ext:Container ID="Container18" runat="server">
                            <Items>
                                <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Margins="10 0 0 5" Height="350" Border="true">
                                            <Store>
                                                <ext:Store ID="store_grilla_valorResidual" runat="server" GroupField="Componente">
                                                    <Model>
                                                        <ext:Model ID="Model19" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="Componente" />
                                                                <ext:ModelField Name="costo_inicial" />
                                                                <ext:ModelField Name="Porcentaje_ci" />
                                                                <ext:ModelField Name="valorResidualAnterior" />
                                                                <ext:ModelField Name="valorResidualActual" />
                                                                <ext:ModelField Name="ajusteValorResidual" />
                                                                <ext:ModelField Name="fecha" Type="Date" />
                                                            </Fields>
                                                        </ext:Model>

                                                    </Model>
                                                    <Sorters>
                                                        <ext:DataSorter Property="fecha" Direction="DESC" />
                                                    </Sorters>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel1" runat="server">
                                                <Columns>
                                                    <ext:Column ID="Column8" runat="server" DataIndex="fecha" Header="Fecha Revisión" Flex="1">
                                                        <Renderer Format="Date" FormatArgs="'Y-m-d'" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column1" runat="server" DataIndex="Componente" Header="Componente" Flex="4" />
                                                    <ext:Column ID="Column9" runat="server" DataIndex="costo_inicial" Header="Costo Inicial" Flex="1">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column13" runat="server" DataIndex="Porcentaje_ci" Header="Porcentaje" Flex="1">
                                                        <Renderer Handler="return value +'%';" />
                                                    </ext:Column>

                                                    <ext:Column ID="Column3" runat="server" DataIndex="valorResidualAnterior" Header="Valor Anterior" Flex="1">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>
                                                    <ext:Column ID="valorResidualActual" runat="server" DataIndex="valorResidualActual" Header="Valor Actual" Flex="1">
                                                        <Renderer Handler="return Ext.util.Format.number(value, '0,0.00');" />
                                                    </ext:Column>

                                                </Columns>
                                            </ColumnModel>
                                            <Features>
                                                <ext:Grouping ID="Group1" runat="server" GroupHeaderTplString="{name}" HideGroupedHeader="true"
                                                    EnableGroupingMenu="false" />
                                            </Features>
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
                        </ext:Container>
                    </Items>
                    <FooterBar>
                        <ext:Toolbar runat="server" ID="asdas">
                            <Items>
                                <ext:Button ID="btn_guardar" runat="server" Text="GUARDAR" Disabled="false" Width="100" Margins="5 0 0 5"
                                    Icon="Disk" ToolTip="Guardar Deterioro.">
                                    <Listeners>
                                        <Click Handler="
                                        var r = App.txt_valornuevo.getValue();
                                        r= r.replace(/[\.]/g, '');
                                        if (#{FormPanel1}.getForm().isValid()) { App.direct.GrabarValorResidual(
                                                    [
                                                    App.dfd_fecha.getValue(),
                                                    App.idempleado.getValue(), 
                                                    r,
                                                    App.txt_conceptos.getValue()
                                                    ]); 
                                                    }else{'true'}" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="btn_limpiar" runat="server" Text="LIMPIAR" Width="100" Margins="5 0 0 5" Icon="Erase">
                                    <Listeners>
                                        <Click Handler="App.FormPanel1.reset();Ext.ComponentQuery.query('#GridPanel1')[0].getStore().removeAll();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>

                        </ext:Toolbar>
                    </FooterBar>
                    <%--*************************************************--%>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
        <ext:Window ID="WRECUPERARRESIDUAL" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="500">
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
