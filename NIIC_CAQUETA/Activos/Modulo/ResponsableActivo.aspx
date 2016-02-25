<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponsableActivo.aspx.cs" Inherits="Activos.Modulo.ResponsableActivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ASIGNAR RESPONSABLE</title>
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
   <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js" >
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
    
    <ext:Store ID="Store_empleado" runat="server" PageSize="8">
        <Sorters>
            <ext:DataSorter Property="id_empleado" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model13" runat="server" IDProperty="id_empleado">
                <Fields>
                    <ext:ModelField Name="id_empleado" />
                    <ext:ModelField Name="NombreEmpleado" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="680" Height="550" Frame="true"
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                <Items>
                    <ext:Hidden ID="FechaCompra" runat="server" />
                    <ext:Hidden ID="FechaAlta" runat="server" />
                    <ext:Hidden ID="codigo" runat="server" />
                    <ext:Hidden ID="FechaDepreciacion" runat="server" />
                    <%--***************************--%>
                    <ext:FieldSet ID="FieldSet2" runat="server" Disabled="false">
                        <Items>
                            <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Label ID="lbl_activo" runat="server" Html ="Activo<font color ='red'>*</font>"  Flex="3" Enabled="true" Margins="10 0 0 10" />
                                    <ext:Label ID="lbl_fecha" runat="server" Html ="Fecha Asignación<font color ='red'>*</font>" Flex="1" Margins="10 0 0 10" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:TriggerField ID="TriggerField1" runat="server" Name="Buscar" TextAlign="Center"  AllowBlank="false" Editable="false" FieldCls="text-center" Flex="3" Margins="0 0 0 10">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <DirectEvents>
                                                        <TriggerClick OnEvent="TriggerField1_Click" After="#{Window1}.show();">
                                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window1" UseMsg="false" />
                                                        </TriggerClick>
                                                    </DirectEvents>
                                                </ext:TriggerField>
                                   <%-- <ext:DateField ID="dfd_inicio" runat="server" Editable="false" FieldStyle="text-align: center; " EndDateField="dfd_fecha" Text="2014-09-16" Hidden="true"
                                        Margins="0 0 0 10" Flex="1" Format="yyyy-MM-dd" AllowBlank="false" Vtype="daterange" EnableKeyEvents="false"/> --%>
                                    <ext:DateField ID="dfd_fecha" runat="server" Editable="false" FieldStyle="text-align: center;"  
                                        Margins="0 0 0 10" Flex="1" Format="yyyy-MM-dd" AllowBlank="false"  EnableKeyEvents="true" >
                                    </ext:DateField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container7" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Label ID="lbl_responsable" runat="server" Html ="Empleado<font color ='red'>*</font>" Flex="3" Enabled="true"
                                        Margins="10 0 0 10" />
                                    <ext:Label ID="lbl_motivo" runat="server"  Html ="Motivo<font color ='red'>*</font>" Flex="1" Enabled="true"
                                        Margins="10 0 0 10" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                <Items>
                                   <ext:TriggerField ID="TriggerField2" runat="server" Name="Buscar" TextAlign="Center"  AllowBlank="false" Editable="false" FieldCls="text-center" Flex="3" Margins="0 0 0 10">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <DirectEvents>
                                                        <TriggerClick OnEvent="TriggerField2_Click" After="#{Window2}.show();">
                                                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window2" UseMsg="false" />
                                                        </TriggerClick>
                                                    </DirectEvents>
                                                </ext:TriggerField>
                                    <ext:TextField ID="txt_codigo" runat="server" Flex="1" Disabled="true" Margins=" 0 0 10"
                                        EmptyText="" FieldStyle="text-align: center;" Hidden="true" />
                                    <ext:ComboBox runat="server" ID="cbx_motivo" Icon="SectionCollapsed" EmptyText="<<seleccione>>"
                                        ForceSelection="true" Margins="0 0 0 10" Flex="1"
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
                            <ext:Container ID="Container31" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Label ID="lbl_centrocosto" runat="server" Text="Centro Costo" Flex="1" Enabled="true"
                                        Margins="5 0 0 10" />
                                    <ext:Label ID="lbl_centroeconomico" runat="server" Text="Centro Economico" Flex="1"
                                        Enabled="true" Margins="5 0 0 10" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:TextField ID="txt_ce" runat="server" Flex="1" Margins="0 0 0 10" ReadOnly="true" />
                                    <ext:TextField ID="txt_cc" runat="server" Flex="1" Margins="0 0 0 10" ReadOnly="true" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Label ID="lbl_concepto" runat="server" Text="Observación" Flex="1" Enabled="true"
                                        Margins="0 0 0 10" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:TextArea ID="txt_conceptos" runat="server" Margins="0 0 15 10" AllowBlank="true"
                                        Height="60" Flex="1" MaxLength="1999" EnforceMaxLength="true">
                                    </ext:TextArea>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet ID="FieldSet3" runat="server" Disabled="false">
                        <Items>
                            <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Panel ID="Panel1" runat="server"  Flex="1">
                                        <Items>
                                            <ext:GridPanel ID="GridPanel1" runat="server" Height="200" Margins="15 0 15 10" >
                                                <Store>
                                                    <ext:Store ID="store_asignaciones" runat="server">
                                                        <Model>
                                                            <ext:Model ID="Model19" runat="server" IDProperty="codigo">
                                                                <Fields>
                                                                    <ext:ModelField Name="codigo" />
                                                                    <ext:ModelField Name="nombre" Type="String" />
                                                                    <ext:ModelField Name="centro" />
                                                                    <ext:ModelField Name="fecha" Type="Date" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ColumnModel ID="ColumnModel1" runat="server">
                                                    <Columns>
                                                        <ext:Column ID="Column22" runat="server" DataIndex="nombre" Header="EMPLEADO" Flex="3" />
                                                        <ext:Column ID="Column1" runat="server" DataIndex="centro" Header="CENTRO COSTO"
                                                            Flex="2" />
                                                        <ext:DateColumn ID="DateColumn1" runat="server" DataIndex="fecha" Header="FECHA ASIGNADO" Format="dd/MM/yyyy"
                                                            Width="130" />
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
                                    </ext:Panel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FieldSet>
                </Items>
               <%-- <%--*************************Botones del formulario agregar Componentes************************--%>
                <Buttons>
                    <ext:Button ID="Button3" runat="server" Text="Actualizar" Hidden="true" Icon="DiskEdit"
                        ToolTip="Guardar los cambios.">
                    </ext:Button>
                    <ext:Button ID="btn_guardarAsignacion" runat="server" Text="Guardar" Disabled="false"
                        FormBind="true" Icon="Disk" ToolTip="Guardar Asignacion.">
                        <Listeners>
                            <Click Handler="App.direct.Guardar(#{GridPanel1}.getStore().getRecordsValues(),                            
                            [App.txt_codigo.getValue(),
                            App.txt_conceptos.getValue(),
                            App.dfd_fecha.getValue(),
                            App.cbx_motivo.getValue()
                            ]
                            );" />
                        </Listeners>
                    </ext:Button>
                    
                </Buttons>
                <%--*************************************************--%>
            </ext:FormPanel>

        </Items>
    </ext:Viewport>
    </form>
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
                        <ItemDblClick OnEvent="GridComodato_SelectRow" Before="#{FormPanel1}.getForm().reset();" After="#{Tool2}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="NComodato" Value="record.get('codigo')" Mode="Raw" />
                                <ext:Parameter Name ="Nombre"   Value="record.get('nombre')" Mode="Raw" />
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
                <ext:GridPanel ID="GridPanel2" runat="server" Title="Comodatos" UI="Info" Border="true" Height="435" StoreID="Store_empleado">
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
                           
                            <ext:Column ID="Column4" runat="server" Text="Empleado" DataIndex="NombreEmpleado" Align="Center" Flex="1">
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
                        <ItemDblClick OnEvent="GridDatoEmp_SelectRow" After="#{Tool1}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="Codigo" Value="record.get('id_empleado')" Mode="Raw" />
                                <ext:Parameter Name ="Nombre"   Value="record.get('NombreEmpleado')" Mode="Raw" />
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
</body>
</html>

