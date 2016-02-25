
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargarPlanoNiif.aspx.cs" Inherits="Activos.Modulo.CargarValorResidualPlano" %>

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
                <ext:Window ID="win_cargarplano" runat="server" Title="ENTRADA DE ACTIVOS FIJOS POR ARCHIVOS PLANOS" CloseAction="Hide" Hidden="true" Width="500" Height="450" >
                 <Items>
                 <ext:FormPanel
                    ID="BasicForm"
                    Frame="true"
                    runat="server"
                    Width="490"
                   
                    AutoScroll="true"
                    Height="450">
                    <Defaults>              
                         <ext:Parameter Name="anchor" Value="95%" Mode="Value" />          
                        <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
                        <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
                    </Defaults>
                  
                    <Items>
                         
                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" Width="400">
                            <Items>
                                <ext:FileUploadField
                                    ID="FileUploadField1"
                                    runat="server" AllowBlank="false"
                                    EmptyText="Seleccione un archivo..."
                                    FieldLabel="Archivo"
                                    ButtonText="" AnchorHorizontal="100%" Width="440"
                                    Icon="PageGo" Margins="10 0 0 10">
                                    <Listeners>
                                        <Change Handler="if(/.txt/.test(this.getValue()) || /.csv/.test(this.getValue()) ){}else {alert('Archino no válido, solo se perimiten archivos .txt o .csv'); #{BasicForm}.getForm().reset(); App.SaveButton.setDisabled(true);}" />
                                    </Listeners>

                                </ext:FileUploadField>

                            </Items>
                        </ext:Container>

                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" Width="400">
                            <Items>
                                <ext:Label runat="server" Text="Columnas separadas por:"  Flex="1" Margins="12 0 0 10"/>
                                <ext:TextField runat="server" ID="txt_separador_c" MaxLengthText="1" MaxLength="1" Text=";" EnforceMaxLength="true" Width="30" Margins="10 0 0 0"/>
                                 <ext:Label runat="server" Text="Filas separadas por:"  Flex="1" Margins="12 0 0 10"/>
                                <ext:TextField runat="server" ID="txt_separador_f" MaxLengthText="2" MaxLength="2" Text="\n" ReadOnly="true" Selectable="false"
                                     EnforceMaxLength="true" Width="30" Margins="10 0 0 0"/>
                                 </Items>
                        </ext:Container>

                        <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" Width="400">
                            <Items>
                                <ext:GridPanel
                                    ID="GrillaAtributos" 
                                    runat="server"
                                    AutoLoad="false"
                                    Title="Caracteristica"                                    
                                     Flex="2" Margins="10 0 0 10"  Border="true" Height="260">
                                    <Store>
                                        <ext:Store ID="StoreAtributos" runat="server">
                                            <Model>
                                                <ext:Model runat="server">
                                                    <Fields>
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
                                <Click Handler=" var s=App.GrillaAtributos.getSelectionModel(); App.direct.CargarArchivo(App.GrillaAtributos.getRowsValues());" />
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
            </Items>
           </ext:Viewport>
    </form>
    
</body>
</html>
