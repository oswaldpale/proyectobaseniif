<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subclase.aspx.cs" Inherits="Activos.Modulo.CrearSubclase" %>

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
                 Detalle.Editar(e.record.data.idsubclase, e.record.data.subclase, e.record.data.depreciable);
             }
         
     };
    </script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SUBCLASE</title>
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
    <ext:Hidden ID="idclase" runat="server" />
     <ext:Store ID="Store_clase" runat="server" PageSize="8">
        <Sorters>
            <ext:DataSorter Property="codigoclase" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model15" runat="server" IDProperty="codigoclase">
                <Fields>
                    <ext:ModelField Name="codigoclase" />
                    <ext:ModelField Name="descripcionclase" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>   
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="510" Height="430" Frame="true" 
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Top" LabelWidth="11" MsgTarget="Side" />
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                    <Items>
                         <ext:Label ID="lbl_clase" runat="server" Html ="Clase<font color ='red'>*</font>" Width="60" Enabled="true"
                                Margins="5 0 0 5" />                         
                         <ext:TriggerField ID="TriggerField2" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="2" Margins="5 0 0 5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerField1_Click1" After="#{Window5}.show();">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window5" UseMsg="false" />
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                    </Items>
                    </ext:Container> 
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                           
                            <ext:Label ID="lbl_subclase" runat="server" Html ="Subclase<font color ='red'>*</font>" Width="60" Enabled="true"
                                Margins="5 0 0 5" />
                            <ext:TextField ID="txt_subclase" runat="server" Flex="2" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" />
                             <ext:Label ID="Label1" runat="server" Html ="Depreciable<font color ='red'>*</font>" Width="80" Enabled="true"
                                Margins="5 0 0 5" />
                             <ext:Checkbox ID="CDEPRECIABLE" runat="server" Width="60" Margins="0 0 15 10" AllowBlank="false" >
                                                        </ext:Checkbox>
                           
                            <ext:Button ID="btn_crearSubclase" runat="server" Flex="1" Icon="Disk" Text="Guardar" Margins="5 0 0 5">
                                <Listeners>
                                    <Click Handler="if(App.FormPanel1.isValid()){ App.direct.Guardar(
                                    App.GridPanel1.getStore().getRecordsValues(),                                    
                                    App.txt_subclase.getValue()
                                    );
                                    }else{true}
                                    " />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                        <Items>
                             <ext:Panel ID="PanelClase" runat="server" Flex="1" Margins="10 0 0 10">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="300" Border="true">
                                        <Store>
                                            <ext:Store ID="store_hclase" runat="server" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="idsubclase">
                                                        <Fields>   
                                                            <ext:ModelField Name="idsubclase" />                                                     
                                                            <ext:ModelField Name="subclase" />  
                                                            <ext:ModelField Name="depreciable" Type="Boolean" />                                                                                                                
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>                                                
                                                <ext:Column ID="Column2" runat="server" DataIndex="idsubclase" Text="Codigo"
                                                    Flex="1">
                                                  
                                                </ext:Column>
                                                <ext:Column ID="Column1" runat="server" DataIndex="subclase" Text="Subclase"
                                                    Flex="4">
                                                    <Editor>
                                                        <ext:TextField ID="TextField1" runat="server" />
                                                    </Editor>
                                                </ext:Column>
                                                <ext:CheckColumn runat="server" DataIndex="depreciable" Width="100" Text="Depreciable" Editable="true" >
                                                </ext:CheckColumn>
                                               <%-- <ext:Column ID="Column3" runat="server" DataIndex="depreciable" Text="Depreciable"
                                                    Flex="1">
                                                    <Editor>
                                                        <ext:Checkbox ID="Chekcbox1" runat="server">
                                                           
                                                        </ext:Checkbox>
                                                    </Editor>
                                                    
                                                </ext:Column>--%>
                                               <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel1.getStore().remove(record);App.direct.EliminarSubclase(record.data.idsubclase)" />
                                                        </Listeners>
                                                    </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                         <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar2" runat="server" HideRefresh="true" />
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
            <ext:Window ID="Window5" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="400">
            <Items>
                <ext:GridPanel ID="GridClase" runat="server" Title="Clase" UI="Info" Border="true" Height="320" StoreID="Store_clase">
                    <Tools>
                        <ext:Tool ID="Tool1" Type="Close">
                            <Listeners>
                                <Click Handler="#{Window5}.hide();" />
                            </Listeners>
                        </ext:Tool>
                    </Tools>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column ID="Column8" runat="server" Text="Codigo" DataIndex="codigoclase" Align="Center" Width="70">
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
                           
                            <ext:Column ID="Column10" runat="server" Text="Clase" DataIndex="descripcionclase" Align="Center" Flex="1">
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
                        <ItemDblClick OnEvent="GridComodato_SelectRow" After="#{Tool1}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="Codigo" Value="record.get('codigoclase')" Mode="Raw" />
                                <ext:Parameter Name ="Nombre"   Value="record.get('descripcionclase')" Mode="Raw" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Target="Page" UseMsg="false" />
                        </ItemDblClick>
                    </DirectEvents>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                    </SelectionModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="PagingToolbar5" runat="server" HideRefresh="true" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Window>
        </Items>
    </ext:Viewport>    
    </form>
</body>
</html>
