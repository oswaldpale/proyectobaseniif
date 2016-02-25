<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MotivoAlta.aspx.cs" Inherits="Activos.Modulo.MotivoAlta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MOTIVO ALTA</title>
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
    <ext:Hidden ID="idmotivo" runat="server" />
        
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="510" Height="370" Frame="true" 
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Top" LabelWidth="11" MsgTarget="Side" />
                <Items>
                   
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                           
                            <ext:Label ID="lbl_motivo" runat="server" Html ="Motivo<font color ='red'>*</font>"  Width="60" Enabled="true"
                                Margins="10 0 10 10" />
                            <ext:TextField ID="txt_motivo" runat="server" Flex="2" Disabled="false" Margins="10 0 10 10" AllowBlank="false"
                                EmptyText="" />
                           
                            <ext:Button ID="btn_crearmotivo" runat="server" Flex="1" Icon="Disk" Text="Guardar" Margins="10 0 0 10" >
                                <Listeners>
                                    <Click Handler="if(App.FormPanel1.isValid()){ App.direct.Guardar(
                                    App.GridPanel1.getStore().getRecordsValues(),                                    
                                    App.txt_motivo.getValue()
                                    );
                                    }else{true}
                                    " />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                        <Items>
                             <ext:Panel ID="Panelmotivo" runat="server" Flex="1" Margins="10 0 0 10">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="250" Border="true">
                                        <Store>
                                            <ext:Store ID="store_motivo" runat="server" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                        <Fields>   
                                                            <ext:ModelField Name="codigo" />                                                     
                                                            <ext:ModelField Name="motivo" />  
                                                                                                                                                                            
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>    
                                                   <ext:Column ID="Column2" runat="server" DataIndex="codigo" Text="CODIGO"
                                                    Flex="1">
                                                   
                                                </ext:Column>                                         
                                                <ext:Column ID="Column1" runat="server" DataIndex="motivo" Text="MOTIVO ALTA"
                                                    Flex="4">
                                                    <Editor>
                                                        <ext:TextField ID="TextField1" runat="server" />
                                                    </Editor>
                                                </ext:Column>
                                               <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel1.getStore().remove(record);App.direct.Eliminarmotivo(record.data.codigo)" />
                                                        </Listeners>
                                                    </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                                        </SelectionModel>

                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar3" runat="server" HideRefresh="true" />
                                        </BottomBar>
                                        <Plugins>
                                            <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1">
                                                <Listeners>
                                                    <Edit Fn="EditarGrillaDetalle" />
                                                </Listeners>
                                            </ext:CellEditing>
                                       </Plugins>
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
