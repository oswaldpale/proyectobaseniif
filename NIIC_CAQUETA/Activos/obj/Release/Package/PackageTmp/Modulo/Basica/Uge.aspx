<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Uge.aspx.cs" Inherits="Activos.Modulo.Uge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UNIDAD GENERADORA EFECTIVO</title>

     <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
    <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script>

    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
    <style>
  
     .x-grid-body .x-grid-cell-Cost {
            background-color : #f1f2f4;
        }
         
        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner{
            background-color : #e1e2e4;
        }    

        .task .x-grid-cell-inner {
            padding-left: 15px;
        }

        .x-grid-row-summary .x-grid-cell-inner {
            font-weight: bold;
            font-size: 11px;
            background-color : #f1f2f4;
        }         
        
            .cbStates-list {
            width : 280px;
            font  : 11px tahoma,arial,helvetica,sans-serif;
        }
        
        .cbStates-list th {
            font-weight : bold;
        }
        
        .cbStates-list td, .cbStates-list th {
            padding : 3px;
        }

        .list-item {
            cursor : pointer;
        }
    </style>
     <script>

         var EditarGrillaDetalle = function (editor, e) {

             if (!(e.value === e.originalValue || (Ext.isDate(e.value) && Ext.Date.isEqual(e.value, e.originalValue)))) {
                 Detalle.Editar(e.record.data.codigo, e.record.data.idmunicipio, e.value, e.field);
             }

         };

         var MunicipioRenderer = function (value) {
             var r = App.store_gridMunicipio.getById(value);

             if (Ext.isEmpty(r)) {
                 return "";
             }

             return r.data.municipio;
         };

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
    <ext:Hidden ID="iduge" runat="server" />
        
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
                            <ext:Label ID="lbl_costo" runat="server" Html="Unidad Generadora Efectivo<font color ='red'>*</font>" Flex="3" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="Label1" runat="server" Html=" Estado<font color ='red'>*</font>" Width="215" Enabled="true"
                                Margins="5 0 0 5" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                             <ext:TextField ID="txt_codigo" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" />
                            <ext:TextField ID="txt_uge" runat="server" Flex="3" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
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
                            
                            <ext:Button ID="btn_guardar" runat="server" Width="110" Icon="Disk" Text="Guardar" Margins="5 0 0 5" >
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
                             <ext:Panel ID="Paneluge" runat="server" Flex="1" Margins="5 0 0 5">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="330" Border="true">
                                        <Store>
                                            <ext:Store ID="store_uge" runat="server" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                        <Fields>   
                                                            <ext:ModelField Name="iduge" />
                                                            <ext:ModelField Name="codigo" />                                                     
                                                            <ext:ModelField Name="uge" />  
                                                            <ext:ModelField Name="estado" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>        
                                                <ext:Column ID="Column2" runat="server" DataIndex="codigo" Text="CODIGO" />                                           
                                                <ext:Column ID="Column1" runat="server" DataIndex="uge" Text="UNIDAD GENERADORA EFECTIVO"
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
                                                
                                              <%-- <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel1.getStore().remove(record);App.direct.Eliminarcosto(record.data.codigo)" />
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
                                                    <Select Handler="App.direct.CargarDatos(record.data['iduge']);">
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