<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargarAltaActivo.aspx.cs" Inherits="Activos.Modulo.CargarAltaActivo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
        <ext:Viewport ID="Viewport1" runat="server">
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
            </LayoutConfig>
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Width="450" Height="500" Frame="true"
                    TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                    <FieldDefaults LabelAlign="Right" LabelWidth="115" MsgTarget="Side" />
                    <Items>
                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:Label ID="lbl_archivo" runat="server" Html="Archivo <font color ='red'>*</font>" Width="55" Enabled="true" Margins="10 0 0 10" />
                                <ext:TextField ID="TextField1" runat="server" Flex="6" FieldCls="text-center" Margins="10 0 0 10"/>
                                <ext:Button ID="btnCargar" runat="server" Width="30" Icon="Attach" Margins="10 0 0 10"  UI="Default"    />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:Label ID="Label1" runat="server" Text="Columnas separadas por:" Flex="1" Enabled="true" Margins="10 0 0 10" />
                                <ext:TextField ID="TextField2" runat="server"  Text=";" Width="30" FieldCls="text-center" Margins="10 0 0 10"/>
                                <ext:Label ID="Label2" runat="server" Text="Filas separadas por:" Flex="1" Enabled="true" Margins="10 0 0 10" />
                                <ext:TextField ID="TextField3" runat="server"  Text=";" Width="30" FieldCls="text-center" Margins="10 0 0 10"/>
                            </Items>
                        </ext:Container>
                         <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Panel ID="Panel1" runat="server"  Flex="1">
                                        <Items>
                                            <ext:GridPanel ID="GridPanel1" runat="server" Height="200" Margins="10 0 10 10" >
                                                <ColumnModel ID="ColumnModel1" runat="server">
                                                    <Columns>
                                                        <ext:CheckColumn ID="Column2" runat="server" DataIndex="estado" Flex="1" />
                                                        <ext:Column ID="Column22" runat="server" DataIndex="nombre" Header="NOMBRE" Flex="2" />
                                                        <ext:Column ID="Column1" runat="server" DataIndex="obligatorio" Header="OBLIGATORIO" Flex="2" />
                                                       
                                                    </Columns>
                                                </ColumnModel>
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
    <ext:Window ID="win_subirplano" runat="server" Title="ENTRADA DE ACTIVOS FIJOS POR ARCHIVOS PLANOS" CloseAction="Hide"
            Hidden="true" Closable="true" Layout="FitLayout" Width="500" Height="450" Resizable="false" Draggable="false">
            <Items>
                <ext:FormPanel
                    ID="BasicForm"
                    runat="server"
                    Width="500">
                    <Defaults>              
                         <ext:Parameter Name="anchor" Value="95%" Mode="Value" />          
                        <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
                        <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
                    </Defaults>
                    <FieldDefaults LabelAlign="Right" LabelWidth="50" MsgTarget="Side" />
                    <Items>

                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" Width="400">
                            <Items>
                                <ext:FileUploadField
                                    ID="FileUploadField1"
                                    runat="server" AllowBlank="false"
                                    EmptyText="Seleccione un archivo..."
                                    FieldLabel="Archivo"
                                    ButtonText="" AnchorHorizontal="100%" Width="450"
                                    Icon="PageGo" Margins="10 0 0 10">
                                    <Listeners>
                                        <Change Handler="if(/.txt/.test(this.getValue()) || /.csv/.test(this.getValue()) ){}else {alert('Archino no válido, solo se perimiten archivos .txt o .csv'); #{BasicForm}.getForm().reset(); App.SaveButton.setDisabled(true);}" />
                                    </Listeners>

                                </ext:FileUploadField>

                            </Items>
                        </ext:Container>

                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" Width="400">
                            <Items>
                                <ext:Label runat="server" Text="Columnas separadas por:"  Width="160" Margins="12 0 0 10"/>
                                <ext:TextField runat="server" ID="txt_separador_c" MaxLengthText="1" MaxLength="1" Text=";" EnforceMaxLength="true" Width="30" Margins="10 0 0 0"/>
                            </Items>
                        </ext:Container>
                         <ext:Container ID="Container7" runat="server" Layout="HBoxLayout" Width="400">
                            <Items>
                                <ext:Label runat="server" Text="Filas separadas por:"  Width="160" Margins="12 0 0 10"/>
                                <ext:TextField runat="server" ID="txt_separador_f" MaxLengthText="2" MaxLength="2" Text="\n" ReadOnly="true" Selectable="false"
                                     EnforceMaxLength="true" Width="30" Margins="10 0 0 0"/>
                            </Items>
                         </ext:Container>

                        <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" Width="400">
                            <Items>
                                <ext:GridPanel
                                    ID="GrillaAtributos" MultiSelect="true"
                                    runat="server"
                                    Title="Caracteristicas del activo"                                    
                                     Flex="2" Margins="10 0 0 10"  Border="true" Height="260">
                                    <Store>
                                        <ext:Store ID="StoreAtributos" runat="server">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
                                                         <ext:ModelField Name="Id" />
                                                         <ext:ModelField Name="Nombre" />
                                                         <ext:ModelField Name="Obligatorio" />                                                         
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel runat="server">
                                        <Columns>
                                             <ext:Column runat="server" Text="Nombre"  Flex="3" DataIndex="Nombre"/>                   
                                            <ext:Column runat="server" Text="Obligatorio"  Flex="1" DataIndex="Obligatorio">                                            
                                            </ext:Column>   
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel runat="server" Mode="Multi" CheckOnly="true" >
                                            <Listeners>
                                                <BeforeDeselect Handler="if(record.data['Obligatorio']=='SI'){ return false;}" />
                                            </Listeners>
                                        </ext:CheckboxSelectionModel>
                                    </SelectionModel>
                                    
                                </ext:GridPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                    <Listeners>
                        <ValidityChange Handler="#{SaveButton}.setDisabled(!valid);" />
                    </Listeners>
                    <Buttons>
                        <ext:Button ID="SaveButton" runat="server" Text="Guardar" Disabled="true" FormBind="true">
                            <Listeners>
                                <Click Handler=" var s=App.GrillaAtributos.getSelectionModel(); App.direct.CargarArchivo(App.GrillaAtributos.getRowsValues({selectedOnly : true}));" />
                            </Listeners>
                        
                        </ext:Button>
                        <ext:Button runat="server" Text="Limpiar">
                            <Listeners>
                                <Click Handler="#{BasicForm}.getForm().reset(); App.SaveButton.setDisabled(true);" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
</body>
</html>
