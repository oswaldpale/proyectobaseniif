<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CentroCosto.aspx.cs" Inherits="Activos.Modulo.CentroCosto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CENTRO COSTO</title>

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
            width : 200px;
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
    <ext:Hidden ID="idcosto" runat="server" />
        
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="700" Height="450" Frame="true" 
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Top" LabelWidth="11" MsgTarget="Side" />
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:Label ID="Label2" runat="server" Html="Codigo<font color ='red'>*</font>" Flex="1" Enabled="true"
                                Margins="5 0 0 5" />
                            <ext:Label ID="lbl_costo" runat="server" Html="Centro Costo<font color ='red'>*</font>" Flex="3" Enabled="true"
                                Margins="5 0 0 5" />

                            <ext:Label ID="Label3" runat="server" Html="Grupo Centro Costo<font color ='red'>*</font>" Flex="3" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="Label1" runat="server" Html=" Estado<font color ='red'>*</font>" Width="215" Enabled="true"
                                Margins="5 0 0 5" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                             <ext:TextField ID="txt_codigo" runat="server" Flex="1" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" />
                            <ext:TextField ID="txt_costo" runat="server" Flex="3" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" />
                            <ext:ComboBox runat="server" ID="cbx_grupocc" Icon="SectionCollapsed"
                                ForceSelection="true" Margins="5 0 0 5" DisplayField="descripcion" ValueField="id_grupo" Cls="true"
                                Flex="3" AllowBlank="false" Name="id_grupo" >
                                <Store>
                                    <ext:Store runat="server" ID="store_grupocc">
                                        <Model>
                                            <ext:Model ID="Model2" runat="server" IDProperty="idcargo">
                                                <Fields>
                                                    <ext:ModelField Name="id_grupo" />
                                                    <ext:ModelField Name="descripcion" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ListConfig Width="100" Height="300" ItemSelector=".x-boundlist-item">
                                            <Tpl ID="Tpl3" runat="server">
                                                <Html>
                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                           
						                            </tpl>
						                            <tr class="x-boundlist-item">      
                                                     <td><b><font size=1>{id_grupo}</font></b></td>                                             
							                            <td><font size=1>{descripcion}</font></td>
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                </Html>
                                            </Tpl>
                                        </ListConfig>
                            </ext:ComboBox>
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
                            
                            <ext:Button ID="btn_guardar" runat="server" Width="110" Icon="Disk" Text="GUARDAR" Margins="5 0 0 5" >
                                <Listeners>
                                    <Click Handler="if(App.FormPanel1.isValid()){App.direct.Guardar(
                                        #{GridPanel1}.getStore().getRecordsValues());}
                                        else{true}" />
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
                             <ext:Panel ID="Paneleconomico" runat="server" Flex="1" Margins="5 0 0 5">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="330" Border="true">
                                        <Store>
                                            <ext:Store ID="store_costo" runat="server" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="idcosto">
                                                        <Fields>   
                                                            <ext:ModelField Name="idcosto" />
                                                            <ext:ModelField Name="codigo" />                                                     
                                                            <ext:ModelField Name="costo" />  
                                                            <ext:ModelField Name="estado" />
                                                            <ext:ModelField Name="id_grupo_CC" />
                                                            <ext:ModelField Name="grupo_Ccosto" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>        
                                                <ext:Column ID="Column2" runat="server" DataIndex="codigo" Text="CODIGO"> 
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
                                                <ext:Column ID="Column1" runat="server" DataIndex="costo" Text="CENTRO COSTO"
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
                                                <ext:Column ID="Column4" runat="server" DataIndex="grupo_Ccosto" Text="GRUPO CENTRO COSTO"
                                                    Flex="1">
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
                                                  <ext:Column ID="Column3" runat="server" Text="Estado" Width="100" DataIndex="estado"
                                                 Align="Center" />
                                                <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel1.getStore().remove(record);App.direct.Eliminarcosto(record.data.idcosto)" />
                                                        </Listeners>
                                                    </ext:CommandColumn>
                                             
                                            </Columns>
                                        </ColumnModel>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar3" runat="server" HideRefresh="true" />
                                        </BottomBar>
                                       <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" Mode="Single">
                                                <Listeners>
                                                    <Select Handler="App.direct.CargarDatos(record.data['idcosto']);">
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
