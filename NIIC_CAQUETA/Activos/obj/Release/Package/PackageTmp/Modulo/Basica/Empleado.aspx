<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="Activos.Modulo.Empleado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FUNCIONARIO</title>

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

         var EditarFila = function (editor,e) {
             App.direct.EditarEmpleado(e.record.data.codigo);
         }

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
     <ext:Hidden ID ="idempleado" runat="server" Name="idempleado" />
     
        
    <ext:Viewport ID="Viewport1" runat="server">
        <LayoutConfig>
            <ext:VBoxLayoutConfig Align="Center" Pack="Center" />
        </LayoutConfig>
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Width="950" Height="590" Frame="true" 
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">

                <FieldDefaults LabelAlign="Top" LabelWidth="11" MsgTarget="Side" />
                
                <Items>
                     
                    <ext:FieldSet runat="server">
                        <Items>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                        <Items>
                            

                             <ext:Label ID="Label3" runat="server" Html="Cedula<font color ='red'>*</font>" Width="90" Enabled="true"
                                Margins="5 0 0 5" />

                            <ext:Label ID="lbl_nombre" runat="server" Html="Nombre<font color ='red'>*</font>" Flex="3" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="lbl_apellido1" runat="server" Html=" Primer Apellido<font color ='red'>*</font>" Flex="2" Enabled="true"
                                Margins="5 0 0 5" />

                            <ext:Label ID="Label1" runat="server" Html=" Segundo Apellido<font color ='green'>*</font>" Flex="2" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="Label2" runat="server" Html="Cargo<font color ='red'>*</font>" Width="200" Enabled="true"
                                Margins="5 0 0 5" />
                             

                             <ext:Label ID="lbl_telefono" runat="server" Html=" Telefono<font color ='green'>*</font>" Width="90" Enabled="true"
                                Margins="5 0 0 5" />
                             
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>

                            <ext:TextField ID="txt_identificacion" runat="server" Width="90" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" Name="idempleado" />

                            <ext:TextField ID="txt_nombre" runat="server" Flex="3" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" Name="nombre1" />
                             <ext:TextField ID="txt_apellido1" runat="server" Flex="2" Disabled="false" Margins="5 0 0 5" AllowBlank="false"
                                EmptyText="" Name="apellido1" />
                             <ext:TextField ID="txt_apellido2" runat="server" Flex="2" Disabled="false" Margins="5 0 0 5" 
                                EmptyText="" Name="apellido2" />

                             <ext:ComboBox runat="server" ID="cbx_tipo" Icon="SectionCollapsed"
                                ForceSelection="true" Margins="5 0 0 5" DisplayField="tipo" ValueField="idcargo" Cls="true"
                                Width="200" AllowBlank="false" Name="idtipo" >
                                <Store>
                                    <ext:Store runat="server" ID="store_tipo">
                                        <Model>
                                            <ext:Model ID="Model2" runat="server" IDProperty="idcargo">
                                                <Fields>
                                                    <ext:ModelField Name="idcargo" />
                                                    <ext:ModelField Name="codigo" />
                                                    <ext:ModelField Name="tipo" />
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
                                                     <td><b><font size=1>{codigo}</font></b></td>                                             
							                            <td><font size=1>{tipo}</font></td>
                                                         
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                </Html>
                                            </Tpl>
                                        </ListConfig>
                            </ext:ComboBox>
                            <ext:TextField ID="txt_telefono" runat="server" Width="90" Disabled="false" MaskRe="/[0-9]/" Margins="5 0 15 5" AllowBlank="true"
                                EmptyText="" Name="telefono" />
                            

                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                        <Items>
                           

                            <ext:Label ID="lbl_celular" runat="server" Html=" Celular<font color ='green'>*</font>" Width="90" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="lbl_Direccion" runat="server" Html=" Dirección<font color ='green'>*</font>" Width="205" Enabled="true"
                                Margins="5 0 0 5" />

                            <ext:Label ID="Label4" runat="server" Html=" Centro Costo<font color ='red'>*</font>" Flex="2" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="Label5" runat="server" Html=" Centro Economico<font color ='red'>*</font>" Flex="2" Enabled="true"
                                Margins="5 0 0 5" />

                             <ext:Label ID="Label7" runat="server" Html=" Estado<font color ='red'>*</font>" Width="100" Enabled="true"
                                Margins="5 0 0 5" />

                         </Items>
                    </ext:Container>
                    
                    <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                        <Items>
                            
                            <ext:TextField ID="txt_celular" runat="server" Width="90" Disabled="false" MaskRe="/[0-9]/" Margins="5 0 15 5" AllowBlank="true"
                                EmptyText=""  Name="celular" />
                            <ext:TextField ID="txt_direccion" runat="server" Width="205" Disabled="false" Margins="5 0 15 5" AllowBlank="true"
                                EmptyText="" Name="direccion" />

                             <ext:ComboBox runat="server" ID="cbx_costo" Icon="SectionCollapsed"
                                ForceSelection="true" Margins="5 0 15 5" DisplayField="costo" ValueField="idcosto" Cls="true"
                                Flex="2" AllowBlank="false" Name="idcosto">
                                <Store>
                                    <ext:Store runat="server" ID="store_costo">
                                        <Model>
                                            <ext:Model ID="Model3" runat="server" IDProperty="idcosto">
                                                <Fields>
                                                    <ext:ModelField Name="codigo" />
                                                    <ext:ModelField Name="idcosto" />
                                                    <ext:ModelField Name="costo" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                 <ListConfig Width="350" Height="300" ItemSelector=".x-boundlist-item">
                                            <Tpl ID="Tpl2" runat="server">
                                                <Html>
                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                           
						                            </tpl>
						                            <tr class="x-boundlist-item">      
                                                     <td><b><font size=1>{codigo}</font></b></td>                                             
							                            <td><font size=1>{costo}</font></td>
                                                         
						                            </tr>
						                            <tpl if="[xcount-xindex]==0">
							                            </table>
						                            </tpl>
					                            </tpl>
                                                </Html>
                                            </Tpl>
                                        </ListConfig>
                            </ext:ComboBox>

                             <ext:ComboBox runat="server" ID="cbx_economico" Icon="SectionCollapsed"
                                ForceSelection="true" Margins="5 0 15 5" DisplayField="economico" ValueField="ideconomico" Cls="true"
                                Flex="2" AllowBlank="false" Name="ideconomico">
                                <Store>
                                    <ext:Store runat="server" ID="store_economico">
                                        <Model>
                                            <ext:Model ID="Model4" runat="server" IDProperty="codigo">
                                                <Fields>
                                                    <ext:ModelField Name="ideconomico" />
                                                    <ext:ModelField Name="codigo" />
                                                    <ext:ModelField Name="economico" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                 <ListConfig Width="350" Height="300" ItemSelector=".x-boundlist-item">
                                            <Tpl ID="Tpl1" runat="server">
                                                <Html>
                                                    <tpl for=".">
						                               <tpl if="[xindex] == 1">
							                          <table class="cbStates-list">
								                           
						                            </tpl>
						                            <tr class="x-boundlist-item">      
                                                     <td><b><font size=1>{codigo}</font></b></td>                                             
							                            <td><font size=1>{economico}</font></td>
                                                         
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
                                    <ext:Menu ID="Menu3" runat="server" >
                                        <Items>
                                            <ext:CheckMenuItem ID="CheckMenuItem_Activo" runat="server" Icon="Accept" Text="Activo" 
                                                Checked="true" />
                                            <ext:CheckMenuItem ID="CheckMenuItem_Inactivo" runat="server" Icon="Delete" Text="Inactivo" />
                                        </Items>
                                       
                                    </ext:Menu>
                                </Menu>
                            </ext:CycleButton>

                        </Items>
                    </ext:Container>

                    <ext:Container ID="Container2" runat="server">
                        <Items>
                             <ext:TabPanel runat="server" ID="TabPanel1" Flex="1"  Frame="false"
                                        BodyStyle="background:transparent;" Margins="5 10 10 5" Plain="true">
                                <TabBar>
                                      <ext:ToolbarFill/>

                                    <ext:Button runat="server" ID="Button1" Text="ACTUALIZAR" Icon="Reload" Scale="Small" Disabled="true" 
                                        Hidden="false">
                                        <Listeners>
                                            <Click Handler="if (#{FormPanel1}.getForm().isValid()) {App.direct.Actualizar([
                                                 App.txt_nombre.getValue(),
                                                 App.txt_apellido1.getValue(),
                                                 App.txt_apellido2.getValue(),
                                                 App.txt_direccion.getValue(),
                                                 App.txt_telefono.getValue(),
                                                 App.txt_celular.getValue(),
                                                 App.cbx_tipo.getValue(),
                                                 App.cbx_costo.getValue(),
                                                 App.cbx_economico.getValue(),
                                                 App.txt_identificacion.getValue(),
                                                
                                                ]);
                                                }else{'true'}
                                                " />
                                        </Listeners>
                                    </ext:Button>

                                        <ext:Button runat="server" ID="Button2" Text="GUARDAR" Icon="Disk"  Scale="Small" 
                                            >
                                            <Listeners>
                                                <Click Handler="if (#{FormPanel1}.getForm().isValid()) { App.direct.Guardar(
                                                [App.txt_identificacion.getValue(),                    
                                                App.txt_nombre.getValue(),
                                                App.txt_apellido1.getValue(),
                                                App.txt_apellido2.getValue(),
                                                App.cbx_tipo.getValue(),
                                                App.txt_telefono.getValue(),
                                                App.txt_celular.getValue(),
                                                App.txt_direccion.getValue(),
                                                App.cbx_costo.getValue(),
                                                App.cbx_economico.getValue(),
                                                ]
                                                );
                                                }else{'true'}" />
                                                
                                            </Listeners>
                                        </ext:Button>
                                </TabBar>
                                 <Items>
                                     <ext:Panel ID="Paneleconomico" runat="server" Flex="1" Title="EMPLEADOS" Margins="10 0 10 5">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="350" Border="true">
                                        <Store>
                                            <ext:Store ID="store_empleados" runat="server" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                        <Fields>   
                                                            <ext:ModelField Name="idempleado" />  
                                                            <ext:ModelField Name="cedula" />                                                   
                                                            <ext:ModelField Name="nombre" />  
                                                            <ext:ModelField Name ="telefono" />
                                                            <ext:ModelField Name ="direccion" />
                                                            <ext:ModelField Name ="ccosto" />
                                                            <ext:ModelField Name ="ceconomico" />
                                                            <ext:ModelField Name ="celular" />
                                                            <ext:ModelField Name="idcosto" />
                                                            <ext:ModelField Name="ideconomico" />
                                                            <ext:ModelField Name="nombre1" />
                                                            <ext:ModelField Name="apellido1" />
                                                            <ext:ModelField Name="apellido2" />
                                                            <ext:ModelField Name="idtipo" />
                                                            <ext:ModelField Name="eliminado" Type="Boolean">
                                                                <Convert Handler="return value === 'A';" />
                                                            </ext:ModelField>
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>                                                
                                                <ext:Column ID="Column1" runat="server" DataIndex="cedula" Text="Codigo"
                                                    Width="90">
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
                                                 <ext:Column ID="Column2" runat="server" DataIndex="nombre" Text="Nombre"
                                                    Flex="3">
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
                                                <ext:Column ID="Column3" runat="server" DataIndex="telefono" Text="Telefono"
                                                    Width="80">
                                                </ext:Column>
                                                
                                                <ext:Column ID="Column5" runat="server" DataIndex="direccion" Text="Dirección"
                                                    Flex="2">
                                                </ext:Column>
                                                <ext:Column ID="Column6" runat="server" DataIndex="ccosto" Text="Centro Costo"
                                                    Flex="2">
                                                </ext:Column>
                                                 <ext:Column ID="Column7" runat="server" DataIndex="ceconomico" Text="Centro Economico"
                                                    Flex="2">
                                                </ext:Column>
                                                 <ext:Column ID="Column4" runat="server" DataIndex="ceconomico" Text="Centro Economico"
                                                    Flex="2">
                                                </ext:Column>
                                                <ext:CheckColumn runat="server" Text="Estado" Width="60" DataIndex="eliminado" />
                                               <%--<ext:CommandColumn ID="CommandColumn8" runat="server" DataIndex="eliminado"  Align="Center" Width="27" >
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnDesactivar"  ToolTip-Text="Desactivar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.direct.DesactivarEmpleado(record.data.idempleado)" />
                                                        </Listeners>
                                                    </ext:CommandColumn>--%>
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                                        </SelectionModel>
                                      
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar3" runat="server" />
                                        </BottomBar>

                                        <Listeners>
                                            <%--<SelectionChange Handler="App.direct.EditarEmpleado(selected[0].data.idempleado);" />--%>
                                            <SelectionChange Handler="var d=selected[0].data,f=#{FormPanel1}.getForm();f.setValues(d);App.Button1.setDisabled(false);;App.Button2.setDisabled(true);App.direct.QuitarSeleccion(selected[0].data.eliminado);" />
                                            <%--<Select Handler="var d=records[0].data,f=#{FormPanel1}.getForm();f.setValues(d);f.setValues({destino:d.Cod_Destino+' - '+d.Destino1});" />--%>
                                        </Listeners>

                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                                 </Items>
                                  </ext:TabPanel>
                             
                        </Items>
                    </ext:Container>
                 </Items>
                   </ext:FieldSet>
                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Viewport>    
    </form>
</body>
</html>
