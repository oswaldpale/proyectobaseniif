<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Caracteristica.aspx.cs" Inherits="Activos.Modulo.Caracteristica" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 

<script>

     var edit = function (editor, e) {
         /*
             "e" is an edit event with the following properties:

                 grid - The grid
                 record - The record that was edited
                 field - The field name that was edited
                 value - The value being set
                 originalValue - The original value for the field, before the edit.
                 row - The grid table row
                 column - The grid Column defining the column that was edited.
                 rowIdx - The row index that was edited
                 colIdx - The column index that was edited
         */

         // Call DirectMethod
         if (!(e.value === e.originalValue || (Ext.isDate(e.value) && Ext.Date.isEqual(e.value, e.originalValue)))) {
             SubclaseX.Edit(e.record.data.ID, e.field, e.originalValue, e.value, e.record.data);
         }
     };

     var EditarGrillaDetalle = function (editor, e) {

         if (!(e.value === e.originalValue || (Ext.isDate(e.value) && Ext.Date.isEqual(e.value, e.originalValue)))) {
             Detalle.Editar(e.record.data.idsubclase, e.value);
         }

     };
    </script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CARACTERISTICA</title>
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
     <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js">
    </script> 

</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
    <ext:Hidden ID="idcaracteristica" runat="server" />
    
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="800" Height="500" Frame="true" 
                 BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Top" LabelWidth="11" MsgTarget="Side" />
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>

                            <ext:Label ID="lbl_caracteristica" runat="server" Html="Caracteristica<font color ='red'>*</font>" Flex="3" Enabled="true"
                                Margins="5 0 0 5" />

                            <ext:Label ID="lbl_tipo" runat="server" Html="Tipo<font color ='red'>*</font>" Flex="2" Enabled="true"
                                Margins="5 0 0 5" />


                            <ext:Label ID="Label1" runat="server" Html="Descripcion<font color ='green'>*</font>" Flex="3" Enabled="true"
                                Margins="5 0 0 5" />

                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                <Items>
                                    
                                <ext:TextField ID="txt_caracteristica" runat="server" Flex="3" Disabled="false" Margins="5 0 15 5" AllowBlank="false" EmptyText="" />
                                <ext:ComboBox runat="server" ID="cbx_tipo" Icon="SectionCollapsed"
                                ForceSelection="true" Margins="5 0 15 5" DisplayField="TipoDato" ValueField="id_tipo"
                                Flex="2" AllowBlank="false">
                                <Store>
                                    <ext:Store runat="server" ID="store_tipoDato">
                                        <Model>
                                            <ext:Model ID="Model7" runat="server" IDProperty="codigo">
                                                <Fields>
                                                    <ext:ModelField Name="id_tipo" />
                                                    <ext:ModelField Name="TipoDato" />
                                                    <ext:ModelField Name ="Abreviatura" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ListConfig Width="350" Height="300" ItemSelector=".x-boundlist-item">
                                            <Tpl ID="Tpl3" runat="server">
                                                <Html>
                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                           
						                            </tpl>
						                            <tr class="x-boundlist-item">      
                                                     <td><b><font size=2>{id_tipo}</font></b></td>                                             
							                            <td><font size=2>{TipoDato}</font></td>
                                                        <td>{Abreviatura}</td> 
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                </Html>
                                            </Tpl>
                                        </ListConfig>
                            </ext:ComboBox>

                            <ext:TextField ID="txt_descripcion" runat="server" Flex="3" Disabled="false"  Margins="5 0 15 5" EmptyText="" />
                                       
                               
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                       <Items>
                            <ext:TabPanel runat="server" ID="TabPanel1" Flex="1" Frame="false"
                                        BodyStyle="background:transparent;" Margins="5 10 10 5" Plain="true">
                                        <TabBar>
                                            <ext:ToolbarFill />
                                                <ext:Button ID="btn_Guardar" runat="server" Width="100" Icon="Disk" Text="Guardar" Margins="5 0 0 5">
                                                    <Listeners>
                                                        <Click Handler="if(App.FormPanel1.isValid()){ App.direct.Guardar_tbCaracteristica(
                                                              [
                                                                App.cbx_tipo.getValue(),
                                                                App.txt_caracteristica.getValue(),
                                                                App.txt_descripcion.getValue()
                                                              ]                              
                                                            );
                                                            }else{true}
                                                            " />
                                                    </Listeners>
                                                </ext:Button>

                                                <ext:Button ID="btn_Actualizar" runat="server"  Width="100" Hidden="true" Text="Actualizar" Margins="5 0 0 5">
                                                    <Listeners>
                                                        <Click Handler="if(App.FormPanel1.isValid()){ App.direct.Modificar_tbCaracteristica(
                                                          [
                                                            App.txt_caracteristica.getValue(),
                                                            App.txt_descripcion.getValue(),
                                                            App.cbx_tipo.getValue()
                                                           ]                              
                                                        );
                                                        }else{true}
                                                        " />
                                                    </Listeners>
                                                </ext:Button>
                                           
                                        </TabBar>

                            <Items>
                                <ext:Panel ID="PanelCaracteristica" runat="server" Width="120" Margins="10 0 0 10" Title="Caracteristicas ">
                                 <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="340" Border="true">
                                        <Store>
                                            <ext:Store ID="store_caracteristica" runat="server" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="idcaracteristica">
                                                        <Fields>   
                                                            <ext:ModelField Name="idcaracteristica" />                                                     
                                                            <ext:ModelField Name="Caracteristica" />  
                                                            <ext:ModelField Name="Tipo" />   
                                                            <ext:ModelField Name="Descripcion" />                                                                                                            
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>                                                
                                               
                                                <ext:Column ID="Column1" runat="server" DataIndex="idcaracteristica" Text="Codigo"
                                                    Flex="1">
                                                </ext:Column>
                                                <ext:Column ID="Column2" runat="server" DataIndex="Caracteristica" Text="Caracteristica"
                                                    Flex="3">
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

                                                <ext:Column ID="Column3" runat="server" DataIndex="Tipo" Text="Tipo Dato"
                                                    Flex="2">
                                                </ext:Column>

                                                  <ext:Column ID="Column4" runat="server" DataIndex="Descripcion" Text="Descripcion"
                                                    Flex="3">
                                                </ext:Column>
                                               <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel1.getStore().remove(record);App.direct.Eliminar_tbCaracteristica(record.data.idcaracteristica)" />
                                                        </Listeners>
                                                    </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                        
                                        <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel6" runat="server" Mode="Single">
                                                <Listeners>
                                                    <Select Handler="App.direct.CargarDatos(record.data['idcaracteristica']);">
                                                    </Select>
                                                </Listeners>
                                            </ext:RowSelectionModel>
                                        </SelectionModel>

                                        <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar2" runat="server" HideRefresh="true" />
                                        </BottomBar>
                                    </ext:GridPanel>
                                 </Items>
                               </ext:Panel>
                            </Items>
                           </ext:TabPanel>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FormPanel>
        </Items>
    
    </ext:Viewport>    
    </form>
</body>
</html>

