<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CentroEconomico.aspx.cs" Inherits="Activos.Modulo.CentroEconomico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CENTRO ECONOMICO</title>

     <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>

    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
     <script>

         var EditarGrillaDetalle = function (editor, e) {

             if (!(e.value === e.originalValue || (Ext.isDate(e.value) && Ext.Date.isEqual(e.value, e.originalValue)))) {
                 Detalle.Editar(e.record.data.codigo, e.value);
             }

         };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
    <ext:Hidden ID="idcentroEconomico" runat="server" />
        
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="550" Height="450" Frame="true" 
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Top" LabelWidth="11" MsgTarget="Side" />
                <Items>
                     <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                         <Items>
                              <ext:Label ID="Label2" runat="server" Html="Codigo<font color ='red'>*</font>" Flex="1" Enabled="true"
                                Margins="5 0 0 5" />

                               <ext:Label ID="lbl_economico" runat="server" Html ="Centro Economico<font color ='red'>*</font>"  Flex="3" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="Label1" runat="server" Html ="Estado<font color ='red'>*</font>"  Width="215" Enabled="true"
                                Margins="5 0 0 5" />

                         </Items>
                     </ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_codigo" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" />
                            <ext:TextField ID="txt_economico" runat="server" Flex="3" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" />
                            <ext:CycleButton Width="100" ID="btn_estado" runat="server" ShowText="true" Margins="5 0 0 5"
                                PrependText="">
                                <Menu>
                                    <ext:Menu ID="Menu3" runat="server">
                                        <Items>
                                            <ext:CheckMenuItem ID="CheckMenuItem_Activo" runat="server" Icon="Accept" Text="Activo"
                                                Checked="true" />
                                            <ext:CheckMenuItem ID="CheckMenuItem_Inactivo" runat="server" Icon="Delete" Text="Inactivo" />
                                        </Items>
                                    </ext:Menu>
                                </Menu>
                            </ext:CycleButton>
                           
                            <ext:Button ID="btn_guardar" runat="server" Width="110" Icon="Disk" Text="GUARDAR" Margins="5 0 0 5">
                                <Listeners>
                                    <Click Handler="if(App.FormPanel1.isValid()){App.direct.Guardar();}else{true}" />
                                </Listeners>
                            </ext:Button>
                             <ext:Button ID="btn_actualizar" runat="server" Width="110" Icon="Disk" Text="ACTUALIZAR" Margins="5 0 0 5" Hidden="true" >
                                <Listeners>
                                    <Click Handler="if(App.FormPanel1.isValid()){App.direct.Actualizar();}else{true}" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                        <Items>
                             <ext:Panel ID="Paneleconomico" runat="server" Flex="1" Margins="10 0 0 10">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="330" Border="true">
                                        <Store>
                                            <ext:Store ID="store_economico" runat="server" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                        <Fields> 
                                                            <ext:ModelField Name="idcentro" />  
                                                            <ext:ModelField Name="codigo" />                                                     
                                                            <ext:ModelField Name="economico" /> 
                                                            <ext:ModelField Name="estado" /> 
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>   
                                                <ext:Column ID="Column2" runat="server" DataIndex="codigo" Text="CODIGO" />                                             
                                                <ext:Column ID="Column1" runat="server" DataIndex="economico" Text="DESCRIPCION"
                                                    Flex="1">
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
                                                 <ext:Column ID="Column3" runat="server" Text="Estado" Width="100" DataIndex="estado"
                                                 Align="Center" />
                                             <%--   <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel1.getStore().remove(record);App.direct.Eliminareconomico(record.data.idcentro)" />
                                                        </Listeners>
                                                    </ext:CommandColumn>--%>
                                            </Columns>
                                        </ColumnModel>

                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar3" runat="server" HideRefresh="true" />
                                        </BottomBar>
                                        <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" Mode="Single">
                                                <Listeners>
                                                    <Select Handler="App.direct.CargarDatos(record.data['idcentro']);">
                                                    </Select>
                                                </Listeners>
                                            </ext:RowSelectionModel>
                                        </SelectionModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>    
    </form>
</body>
</html>
