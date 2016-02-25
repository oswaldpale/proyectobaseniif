<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubComponente.aspx.cs" Inherits="Activos.Modulo.SubComponente" %>

<!DOCTYPE html>

<ext:XScript ID="XScript1" runat="server">
    <script type="text/javascript" >
        //******** filtros6*****************
    
        var filterStore = [];

        var applyFilter6 = function (field) {                
            var store = #{GridPanel6}.getStore();
            store.filterBy(getRecordFilter6());    
            store.applyPaging();                                            
        };
        var borrarFiltro6 = function () {
           #{txt_filtrocodigoclase}.reset();
           #{txt_filtrodescripcionclase}.reset();
          
           #{Store_clase}.clearFilter();


        }
        

        var filterNumber6 = function (value, dataIndex, record) {
            var val = record.get(dataIndex);                
 
            if (!Ext.isEmpty(value, false) && val != value) {
                return false;
            }
                
            return true;
        };

        var filterString6 = function (value, dataIndex, record) {
            var val = record.get(dataIndex);
                
            if (typeof val != "string") {
                return value.length == 0;
            }
                
            return val.toLowerCase().indexOf(value.toLowerCase()) > -1;
        };
                         
        var getRecordFilter6 = function () {
            var f = [];
            filterStore = []; 
               
            f.push({
                filter: function (record) {                         
                    return filterNumber6(#{txt_filtrocodigoclase}.getValue(), "codigoclase", record);
                }
            });
                 
            f.push({
                filter: function (record) {                         
                    return filterString6(#{txt_filtrodescripcionclase}.getValue() || "", "descripcionclase", record);
                }
            });
                                     
 
            var len = f.length;
                 
            return function (record) {
                for (var i = 0; i < len; i++) {
                    if (!f[i].filter(record)) {
                        return false;
                    }
                }
                filterStore.push(record.data);
                return true;
            };
        };
            
        //*************************

        //******** filtros7*****************
    
        var filterStore = [];

        var applyFilter7 = function (field) {                
            var store = #{GridPanel7}.getStore();
            store.filterBy(getRecordFilter7());    
            store.applyPaging();                                            
        };
        var borrarFiltro7 = function () {
           #{txt_filtrocodigosubclase}.reset();
           #{txt_filtrodescripcionsubclase}.reset();
              
           #{Store_subclase}.clearFilter();
        }
        

        var filterNumber7 = function (value, dataIndex, record) {
            var val = record.get(dataIndex);                
 
            if (!Ext.isEmpty(value, false) && val != value) {
                return false;
            }
                
            return true;
        };

        var filterString7 = function (value, dataIndex, record) {
            var val = record.get(dataIndex);
                
            if (typeof val != "string") {
                return value.length == 0;
            }
                
            return val.toLowerCase().indexOf(value.toLowerCase()) > -1;
        };
                         
        var getRecordFilter7 = function () {
            var f = [];
            filterStore = []; 
               
            f.push({
                filter: function (record) {                         
                    return filterNumber7(#{txt_filtrocodigosubclase}.getValue(), "codigosubclase", record);
                }
            });
                 
            f.push({
                filter: function (record) {                         
                    return filterString6(#{txt_filtrodescripcionsubclase}.getValue() || "", "descripcionsubclase", record);
                }
            });
                                     
 
            var len = f.length;
                 
            return function (record) {
                for (var i = 0; i < len; i++) {
                    if (!f[i].filter(record)) {
                        return false;
                    }
                }
                filterStore.push(record.data);
                return true;
            };
        };
            
        //*************************

        //******** filtros caracteristica*****************
    
        var filterStore = [];

        var applyFilter8 = function (field) {                
            var store = #{GridPanel1}.getStore();
            store.filterBy(getRecordFilter8());    
            store.applyPaging();                                            
        };
        var borrarFiltro8 = function () {
           #{txt_filtrocodigo}.reset();
           #{txt_filtrocaracteristica}.reset();
           #{txt_filtrodescripcion}.reset();   
           #{Store_subclase}.clearFilter();
        }
       

        var filterNumber8 = function (value, dataIndex, record) {
            var val = record.get(dataIndex);                
            if (!Ext.isEmpty(value, false) && val != value) {
                return false;
            }
            return true;
        };

        var filterString8 = function (value, dataIndex, record) {
            var val = record.get(dataIndex);
                
            if (typeof val != "string") {
                return value.length == 0;
            }
                
            return val.toLowerCase().indexOf(value.toLowerCase()) > -1;
        };
                         
        var getRecordFilter8 = function () {
            var f = [];
            filterStore = []; 
               
            f.push({
                filter: function (record) {                         
                    return filterNumber8(#{txt_filtrocodigo}.getValue(), "codigo", record);
                }
            });
                 
            f.push({
                filter: function (record) {                         
                    return filterString8(#{txt_filtrodescripcion}.getValue() || "", "descripcion", record);
                }
            });
            f.push({
                filter: function (record) {                         
                    return filterString8(#{txt_filtrocaracteristica}.getValue() || "", "caracteristica", record);
                }
            });
            var len = f.length;
                 
            return function (record) {
                for (var i = 0; i < len; i++) {
                    if (!f[i].filter(record)) {
                        return false;
                    }
                }
                filterStore.push(record.data);
                return true;
            };
        };
            
        //*************************
    </script>
</ext:XScript>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Subcomponente y Caracteristicas</title>
    <link href="../Estilos/extjs-extension.css" rel="stylesheet" />
   <script src="http://gascaqueta.net/sigcweb/scripts/extjs-extension.js" >
    </script>
 
     <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune"  />
    
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
    <ext:Store ID="Store_subclase" runat="server" PageSize="8">
        <Sorters>
            <ext:DataSorter Property="codigosubclase" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model3" runat="server" IDProperty="codigosubclase">
                <Fields>
                    <ext:ModelField Name="codigosubclase" />
                    <ext:ModelField Name="descripcionsubclase" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="Store_tbcaracteristica" runat="server" PageSize="8">
        <Sorters>
            <ext:DataSorter Property="codigo" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model4" runat="server" IDProperty="codigo">
                <Fields>
                    <ext:ModelField Name="codigo" />
                    <ext:ModelField Name="caracteristica" />
                    <ext:ModelField Name="descripcion" />
                    <ext:ModelField Name="tipo" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <form id="form1" runat="server">
     <ext:Viewport ID="Viewport1" runat="server" >
        <Items>
             <ext:Panel ID="Panel3" runat="server" Region="Center"  BodyPaddingSummary="0 2 0 0" UI="Primary" >
                    <Items>
                     <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Height="70">
                
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:Label ID="lbl_clase" runat="server" Text="Clase" Flex="1" Enabled="true" Margins="10 0 0 10" />
                            <ext:Label ID="lbl_subclase" runat="server" Text="Subclase" Flex="1" Enabled="true"
                                Margins="10 0 0 10" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Middle" DefaultMargins="5" />
                        </LayoutConfig>
                        <Items>
                             <ext:Hidden ID="idclase" runat="server" />
                             <ext:Hidden ID="clase" runat ="server" />
                             <ext:Hidden ID="idsubclase" runat="server" />
                            <ext:Hidden ID="subclase" runat="server" />
                            
                            
                            <%--************ Filtro de clase***************--%>
                            <%-- <ext:DropDownField ID="Dropclase" runat="server" FieldStyle="text-align: center;"
                                  Editable="false" TriggerIcon="SimpleArrowDown" MatchFieldWidth="true" Flex="3"
                                  Hidden="false" Margins="0 0 0 10" AllowBlank="false">
                                  <Component>
                                      <ext:GridPanel ID="GridPanel6" runat="server" Height="250" StoreID="Store_clase" Scala="Small">
                                          <ColumnModel ID="ColumnModel7" runat="server" >
                                              <Columns>
                                                  <ext:Column ID="Column16" runat="server" DataIndex="codigoclase" Text="Codigo" Width="100"
                                                      Align="Center">
                                                      <HeaderItems>
                                                          <ext:TextField ID="txt_filtrocodigoclase" runat="server" FieldStyle="text-transform: uppercase;"
                                                              EnableKeyEvents="true" EnforceMaxLength="true">
                                                              <Listeners>
                                                                  <Change Handler="applyFilter6(this);" Buffer="100" />
                                                              </Listeners>
                                                          </ext:TextField>
                                                      </HeaderItems>
                                                  </ext:Column>
                                                  <ext:Column ID="Column17" runat="server" DataIndex="descripcionclase" Text="Descripcion"
                                                      Align="Center" Width="200">
                                                      <HeaderItems>
                                                          <ext:TextField ID="txt_filtrodescripcionclase" runat="server" FieldStyle="text-align: center;"
                                                              EnableKeyEvents="true">
                                                              <Listeners>
                                                                  <Change Handler="applyFilter6(this);" Buffer="100" />
                                                              </Listeners>
                                                          </ext:TextField>
                                                      </HeaderItems>
                                                  </ext:Column>
                                                  <ext:CommandColumn ID="CommandColumn6" runat="server" Align="Center" Width="27">
                                                      <HeaderItems>
                                                          <ext:Button ID="Button6" runat="server" Icon="Cancel" ToolTip="Limpiar Filtro">
                                                              <Listeners>
                                                                  <Click Handler="borrarFiltro6();" />
                                                              </Listeners>
                                                          </ext:Button>
                                                      </HeaderItems>
                                                  </ext:CommandColumn>
                                                   <ext:CommandColumn ID="CommandColumn9" runat="server" Align="Center" Width="27">
                                                    <HeaderItems>
                                                        <ext:Button ID="Button8" runat="server" Icon="Add" ToolTip="Adicionar Clase">
                                                            <Listeners>
                                                                <Click Handler="App.direct.AddClase();" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </HeaderItems>
                                                </ext:CommandColumn>
                                                 <ext:CommandColumn ID="CommandColumn1" runat="server" Align="Center" Width="27" >
                                                    <HeaderItems>
                                                        <ext:Button ID="Button2" runat="server" Icon="ApplicationEdit" ToolTip="Modificar Clase" Disabled="true">
                                                            <Listeners>
                                                                <Click Handler="App.direct.EditClase();" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </HeaderItems>
                                                </ext:CommandColumn>
                                              </Columns>
                                          </ColumnModel>
                                               <Listeners>
                                                        <Select Handler="#{Dropclase}.setValue(record.data['descripcionclase']);  
                                              #{idclase}.setValue(record.data['codigoclase']); 
                                              #{clase}.setValue(record.data['descripcionclase']);
                                              App.direct.select_subclase();" />
                                                </Listeners>
                                          <BottomBar>
                                              <ext:PagingToolbar ID="PagingToolbar6" runat="server" HideRefresh="true" />
                                          </BottomBar>
                                      </ext:GridPanel>
                                  </Component>
                                  
                              </ext:DropDownField>--%>
                            <ext:TriggerField ID="TriggerField2" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="2">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerField1_Click1" After="#{Window5}.show();">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window5" UseMsg="false" />
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <%--********* Filtro de subclase*************** --%>
                             <ext:TriggerField ID="TriggerField3" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="2">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="TriggerField1_Click2" After="#{Window6}.show();">
                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window6" UseMsg="false" />
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:TriggerField>
                            <%--<ext:DropDownField ID="Dropsubclase" runat="server" FieldStyle="text-align: center;" Disabled="true"
                                Editable="false" TriggerIcon="SimpleArrowDown" MatchFieldWidth="true" Flex="3"
                                Hidden="false" Margins="0 10 0 10" AllowBlank="false">
                                <Component>
                                    <ext:GridPanel ID="GridPanel7" runat="server" Height="250" StoreID="Store_subclase">
                                        <ColumnModel ID="ColumnModel8" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column20" runat="server" DataIndex="codigosubclase" Text="Codigo"
                                                    Width="100" Align="Center">
                                                    <HeaderItems>
                                                        <ext:TextField ID="txt_filtrocodigosubclase" runat="server" FieldStyle="text-transform: uppercase;"
                                                            EnableKeyEvents="true" EnforceMaxLength="true">
                                                            <Listeners>
                                                                <Change Handler="applyFilter7(this);" Buffer="100" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </HeaderItems>
                                                </ext:Column>
                                                <ext:Column ID="Column21" runat="server" DataIndex="descripcionsubclase" Text="Descripcion"
                                                    Align="Center" Width="200">
                                                    <HeaderItems>
                                                        <ext:TextField ID="txt_filtrodescripcionsubclase" runat="server" FieldStyle="text-align: center;"
                                                            EnableKeyEvents="true">
                                                            <Listeners>
                                                                <Change Handler="applyFilter7(this);" Buffer="100" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </HeaderItems>
                                                </ext:Column>
                                                <ext:CommandColumn ID="CommandColumn7" runat="server" Align="Center" Width="27">
                                                    <HeaderItems>
                                                        <ext:Button ID="Button7" runat="server" Icon="Cancel" ToolTip="Limpiar Filtro">
                                                            <Listeners>
                                                                <Click Handler="borrarFiltro7();" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </HeaderItems>
                                                </ext:CommandColumn>
                                                <ext:CommandColumn ID="CommandColumn2" runat="server" Align="Center" Width="27">
                                                    <HeaderItems>
                                                        <ext:Button ID="Button3" runat="server" Icon="Add" ToolTip="Adicionar Subclase">
                                                            <Listeners>
                                                                <Click Handler="App.direct.AddSubclase();" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </HeaderItems>
                                                </ext:CommandColumn>
                                                 <ext:CommandColumn ID="CommandColumn10" runat="server" Align="Center" Width="27" >
                                                    <HeaderItems>
                                                        <ext:Button ID="Button9" runat="server" Icon="ApplicationEdit" ToolTip="Modificar Subclase" Disabled="true">
                                                            <Listeners>
                                                                <Click Handler="App.direct.EditSubClase();" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </HeaderItems>
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                        <Listeners>
                                            <Select Handler="#{Dropsubclase}.setValue(record.data['descripcionsubclase']);
                                          #{idsubclase}.setValue(record.data['codigosubclase']); 
                                          #{subclase}.setValue(record.data['descripcionsubclase']);
                                          App.direct.consultarSubclase();
                                          " />
                                        </Listeners>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar7" runat="server" HideRefresh="true" />
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Component>
                              
                            </ext:DropDownField>--%>
                        </Items>
                    </ext:Container>
                   
                </Items>
            </ext:FormPanel>
            <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" Plain="True" PaddingSpec="10 0 0" Hidden="false" UI="Primary">
                            <Items>

                                <ext:Panel ID="Panel11" runat="server" Title="Componente" UI="Info" >
                                    <Items>
                                        <ext:FormPanel ID="FormPanel2" runat="server" ButtonAlign="Center" Disabled="true" UI="Primary">
                                            <Items>
                                                <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Label ID="lbl_nombre" runat="server" Text="Nombre" Flex="4" Enabled="true" Margins="5 0 0 10" />
                                                         <ext:Label ID="Label5" runat="server" Text="Norma" Flex="2" Enabled="true" Margins="5 15 0 10" />
                                                        <ext:Label ID="lbl_porcentaje" runat="server" Text="Porcentaje " Flex="1" Enabled="true" Margins="5 0 0 10" />
                                                        <ext:Label ID="lbl_tipo" runat="server" Text="Tipo Depreciacion" Flex="3" Enabled="true" Margins="5 0 0 10" />
                                                        <ext:Label ID="lbl_vidautil" runat="server" Text="Vida Util" Flex="1" Enabled="true" Margins="5 0 0 10" />
                                                       
                                                        <ext:Label ID="lbl_medida" runat="server" Text="Medida" Flex="4" Enabled="true" Margins="5 15 0 10" />
                                                       
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:TextField ID="txt_nombre" runat="server" Flex="4" Margins="0 0 15 10" AllowBlank="false" />
                                                          <ext:ComboBox runat="server" ID="cbx_norma" Icon="SectionCollapsed"
                                                            ForceSelection="true" Margins="0 0 15 10" DisplayField="norma" ValueField="codigo"
                                                            Flex="2" AllowBlank="false">
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_norma">
                                                                    <Model>
                                                                        <ext:Model ID="Model7" runat="server" IDProperty="codigo">
                                                                            <Fields>
                                                                                <ext:ModelField Name="codigo" />
                                                                                <ext:ModelField Name="norma" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                        </ext:ComboBox>
                                                        <ext:TextField ID="txt_porcentajeComponente" runat="server" Flex="1"  MaskRe="/[0-9]/"
                                                            Margins="0 0 15 10" AllowBlank="false" />
                                                        <ext:ComboBox runat="server" ID="cbx_tipo_depreciacion" Icon="SectionCollapsed" EmptyText="<<seleccione>>"
                                                            ForceSelection="true" Margins="0 0 15 10" DisplayField="Depreciacion" ValueField="id_tipo"
                                                            Flex="3" AllowBlank="false">
                                                            <Store>
                                                                <ext:Store runat="server" ID="store15">
                                                                    <Model>
                                                                        <ext:Model ID="Model16" runat="server" IDProperty="id_tipo">
                                                                            <Fields>
                                                                                <ext:ModelField Name="id_tipo" />
                                                                                <ext:ModelField Name="Depreciacion" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                            <%-- <Listeners>
                                      <Select Handler="App.direct.formato_VidaUtil(#{cbx_tipo_depreciacion}.getValue())" />
                                  </Listeners>--%>
                                                        </ext:ComboBox>
                                                        <ext:TextField ID="txt_vidautil" runat="server" Flex="1" Margins="0 0 15 10" AllowBlank="false"  MaskRe="/[0-9]/" />
                                                       
                                                        <ext:ComboBox runat="server" ID="cbx_medida" Icon="SectionCollapsed"
                                                            ForceSelection="true" Margins="0 0 15 10" DisplayField="UnidadMedida" ValueField="codigo"
                                                            Flex="2" AllowBlank="false">
                                                            <Store>
                                                                <ext:Store runat="server" ID="store_medida">
                                                                    <Model>
                                                                        <ext:Model ID="Model1" runat="server" IDProperty="codigo">
                                                                            <Fields>
                                                                                <ext:ModelField Name="codigo" />
                                                                                <ext:ModelField Name="UnidadMedida" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                </ext:Store>
                                                            </Store>
                                                        </ext:ComboBox>
                                                        <ext:Button ID="btn_agregar" FormBind="true" runat="server" Flex="2" Margins="0 10 15 10" Text="Agregar" Icon="Add" UI="Primary">
                                                            <Listeners>
                                                                <Click Handler="App.direct.GuardarComponente([
                                                                  App.idclase.getValue(), 
                                                                  App.idsubclase.getValue(),                                         
                                                                  App.txt_nombre.getValue(),
                                                                  App.txt_porcentajeComponente.getValue(),
                                                                  App.txt_vidautil.getValue(),
                                                                  App.cbx_tipo_depreciacion.getValue(),                                                                  
                                                                  App.cbx_medida.getValue(),
                                                                  App.cbx_norma.getValue()  
                                                                    ])" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FormPanel>
                                    </Items>
                                    <Items>
                                        <ext:GridPanel ID="GridPanel3" runat="server" Flex="1" Height="250">
                                            <Store>
                                                <ext:Store ID="store_componente" runat="server" GroupField="subclase">
                                                    <Model>
                                                        <ext:Model ID="Model5" runat="server" IDProperty="codigo">
                                                            <Fields>
                                                                <ext:ModelField Name="clase" />
                                                                <ext:ModelField Name="subclase" />
                                                                <ext:ModelField Name="componente" />
                                                                <ext:ModelField Name="porc" />
                                                                <ext:ModelField Name="vidautil" />
                                                                <ext:ModelField Name="unidad" />
                                                                <ext:ModelField Name="tipo" />
                                                                <ext:ModelField Name="codigo"/>
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel2" runat="server">
                                                <Columns>
                                                    <ext:Column ID="Column1" runat="server" DataIndex="componente" Header="Componente"
                                                        Flex="4">
                                                    </ext:Column>
                                                    <ext:Column ID="Column22" runat="server" DataIndex="subclase" Header="Subclase" Width="100">
                                                    </ext:Column>
                                                    <ext:Column ID="Column4" runat="server" DataIndex="porc" Header="Porcentaje" Flex="1">
                                                        <Renderer Handler="return value +'%';" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column6" runat="server" DataIndex="tipo" Header="Tipo Depreciacion"
                                                        Flex="2">
                                                    </ext:Column>
                                                    <ext:Column ID="Column3" runat="server" DataIndex="vidautil" Header="Vida Util" Flex="1">
                                                    </ext:Column>
                                                    <ext:Column ID="Column5" runat="server" DataIndex="unidad" Header="Medida" Flex="1">
                                                    </ext:Column>
                                                    <ext:CommandColumn ID="CommandColumn8" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel3.getStore().remove(record);App.direct.EliminarComponente(record.data.codigo)" />
                                                        </Listeners>
                                                    </ext:CommandColumn>
                                                </Columns>
                                            </ColumnModel>
                                            <Features>
                                                <ext:Grouping ID="Group1" runat="server" GroupHeaderTplString="{name}" HideGroupedHeader="true"
                                                    EnableGroupingMenu="false" />
                                            </Features>
                                             <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar2" runat="server" HideRefresh="true" />
                                            </BottomBar>
                                        </ext:GridPanel>
                                    </Items>

                                </ext:Panel>
                                <ext:Panel ID="Panel4" runat="server" Title="Caracteristicas" UI="Info">
                                    <Items>
                                        <ext:FormPanel ID="FormPanel3" runat="server" ButtonAlign="Center" Disabled="true">
                                            <Items>
                                                <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Label ID="Label1" runat="server" Text="Caracteristica" Flex="4" Enabled="true"
                                                            Margins="5 0 0 10" />
                                                        <ext:Label ID="lbl_obli" runat="server" Text="Obligatorio " Flex="2" Enabled="true"
                                                            Margins="5 15 0 10" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Hidden ID="idcaracteristica" runat="server" />
                                                        <ext:Hidden ID="caracteristica" runat="server" />
                                                        <ext:Hidden ID="descaracteristica" runat="server" />
                                                        <ext:Hidden ID="tipo" runat="server" />
                                                        <%--<ext:DropDownField ID="DroptbCaracteristica" runat="server" FieldStyle="text-align: center;"
                                                            Editable="false" TriggerIcon="SimpleArrowDown" MatchFieldWidth="true" Flex="4"
                                                            Hidden="false" Margins="0 0 0 10" AllowBlank="false" >
                                                            <Component>
                                                                <ext:GridPanel ID="GridPanel1" runat="server" Height="250" StoreID="Store_tbcaracteristica">
                                                                    <ColumnModel ID="ColumnModel1" runat="server">
                                                                        <Columns>
                                                                            <ext:Column ID="Column10" runat="server" DataIndex="codigo" Text="Codigo" Width="50"
                                                                                Align="Center">
                                                                                <HeaderItems>
                                                                                    <ext:TextField ID="txt_filtrocodigo" runat="server" FieldStyle="text-transform: uppercase;"
                                                                                        EnableKeyEvents="true" EnforceMaxLength="true">
                                                                                        <Listeners>
                                                                                            <Change Handler="applyFilter8(this);" Buffer="100" />
                                                                                        </Listeners>
                                                                                    </ext:TextField>
                                                                                </HeaderItems>
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column11" runat="server" DataIndex="caracteristica" Text="Caracteristica"
                                                                                Align="Center" Width="120">
                                                                                <HeaderItems>
                                                                                    <ext:TextField ID="txt_filtrocaracteristica" runat="server" FieldStyle="text-align: center;"
                                                                                        EnableKeyEvents="true">
                                                                                        <Listeners>
                                                                                            <Change Handler="applyFilter8(this);" Buffer="100" />
                                                                                        </Listeners>
                                                                                    </ext:TextField>
                                                                                </HeaderItems>
                                                                            </ext:Column>
                                                                            <ext:Column ID="Column12" runat="server" DataIndex="descripcion" Text="Descripcion"
                                                                                Align="Center" Width="230">
                                                                                <HeaderItems>
                                                                                    <ext:TextField ID="txt_filtrodescripcion" runat="server" FieldStyle="text-align: center;"
                                                                                        EnableKeyEvents="true">
                                                                                        <Listeners>
                                                                                            <Change Handler="applyFilter8(this);" Buffer="100" />
                                                                                        </Listeners>
                                                                                    </ext:TextField>
                                                                                </HeaderItems>
                                                                            </ext:Column>
                                                                             <ext:Column ID="Column8" runat="server" DataIndex="tipo" Text="Tipo Dato" Hidden="true"
                                                                                Align="Center" Width="100">
                                                                               
                                                                            </ext:Column>
                                                                            <ext:CommandColumn ID="CommandColumn3" runat="server" Align="Center" Width="27">
                                                                                <HeaderItems>
                                                                                    <ext:Button ID="Button4" runat="server" Icon="Cancel" ToolTip="Limpiar Filtro">
                                                                                        <Listeners>
                                                                                            <Click Handler="borrarFiltro8();" />
                                                                                        </Listeners>
                                                                                    </ext:Button>
                                                                                </HeaderItems>
                                                                            </ext:CommandColumn>
                                                                            <ext:CommandColumn ID="CommandColumn4" runat="server" Align="Center" Width="27" >
                                                                                <HeaderItems>
                                                                                    <ext:Button ID="Button5" runat="server" Icon="Add" ToolTip="Adicionar Caracteristica" FormBind="false">
                                                                                        <Listeners>
                                                                                            <Click Handler="App.direct.AddCaracteristica()" />
                                                                                        </Listeners>
                                                                                    </ext:Button>
                                                                                </HeaderItems>
                                                                            </ext:CommandColumn>
                                                                            <ext:CommandColumn ID="CommandColumn11" runat="server" Align="Center" Width="27" >
                                                                                <HeaderItems>
                                                                                    <ext:Button ID="Button10" runat="server" Icon="ApplicationEdit" ToolTip="Modificar Caracteristica" FormBind="false" Disabled="true">
                                                                                        <Listeners>
                                                                                            <Click Handler="App.direct.EditCaracteristica()" />
                                                                                        </Listeners>
                                                                                    </ext:Button>
                                                                                </HeaderItems>
                                                                            </ext:CommandColumn>
                                                                        </Columns>
                                                                    </ColumnModel>
                                                                    <Listeners>
                                                                        <Select Handler="#{DroptbCaracteristica}.setValue(record.data['caracteristica']);
                                                                          #{idcaracteristica}.setValue(record.data['codigo']); 
                                                                          #{caracteristica}.setValue(record.data['caracteristica']);  
                                                                          #{descaracteristica}.setValue(record.data['descripcion']); 
                                                                          #{tipo}.setValue(record.data['tipo']);   
                                                                          App.direct.HabilitarEditCaract();
                                                                            " />
                                                                    </Listeners>
                                                                    <BottomBar>
                                                                        <ext:PagingToolbar ID="PagingToolbar1" runat="server" HideRefresh="true" />
                                                                    </BottomBar>
                                                                </ext:GridPanel>
                                                            </Component>
                                                        </ext:DropDownField>--%>
                                                        <ext:TriggerField ID="TriggerField1" runat="server" Name="Buscar" TextAlign="Center" AllowBlank="false" Editable="false" FieldCls="text-center" Flex="4">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Search" />
                                                            </Triggers>
                                                            <DirectEvents>
                                                                <TriggerClick OnEvent="TriggerField1_Click" After="#{Window4}.show();">
                                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="Window4" UseMsg="false" />
                                                                </TriggerClick>
                                                            </DirectEvents>
                                                        </ext:TriggerField>
                                                        <ext:ComboBox ID="cbx_obligatorio" runat="server" Editable="false" Margins="0 0 15 10" AllowBlank="false" ForceSelection="true"
                                                            Flex="1">
                                                            <Items>
                                                                <ext:ListItem Text="NO" Value="0" />
                                                                <ext:ListItem Text="SI" Value="1" />
                                                            </Items>
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                            </Triggers>
                                                        </ext:ComboBox>
                                                       
                                                        <ext:Button ID="Button1" FormBind="true" runat="server" Flex="1" Margins="0 10 15 10" Scale="small" UI="Primary"
                                                            Text="Agregar" Icon="Add" >
                                                            <Listeners>
                                                            <Click Handler="App.direct.GuardarCaracteristica([
                                                              App.idclase.getValue(), 
                                                              App.idsubclase.getValue(),                                         
                                                              #{idcaracteristica}.getValue(),                        
                                                              App.cbx_obligatorio.getValue()
                                                            ])" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FormPanel>
                                        <ext:GridPanel ID="GridPanel2" runat="server" Flex="1" Height="250">
                                            <Store>
                                                <ext:Store ID="store_caracteristica" runat="server" GroupField="subclase" >
                                                    <Model>
                                                        <ext:Model ID="Model2" runat="server" IDProperty="codigo">
                                                            <Fields>
                                                                <ext:ModelField Name="subclase" />
                                                                <ext:ModelField Name="caracteristica" />
                                                                <ext:ModelField Name="obligatorio" Type="Boolean" />
                                                                <ext:ModelField Name="codigo" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel3" runat="server">
                                                <Columns>
                                                    <ext:Column ID="Column2" runat="server" DataIndex="caracteristica" Header="Caracteristica"
                                                        Flex="4">
                                                    </ext:Column>
                                                    <ext:Column ID="Column7" runat="server" DataIndex="subclase" Header="Subclase" Flex="2">
                                                    </ext:Column>
                                                    <ext:CheckColumn ID="CheckColumn1" runat="server" DataIndex="obligatorio" Header="Obligatorio"  Flex="1" />
                                                    <ext:CommandColumn ID="CommandColumn5" runat="server" Align="Center" Width="27">
                                                        <Commands>
                                                            <ext:GridCommand CommandName="btnEliminar" Icon="Delete" ToolTip-Text="Eliminar" />
                                                        </Commands>
                                                        <Listeners>
                                                            <Command Handler="App.GridPanel2.getStore().remove(record);App.direct.EliminarCaracteristica(record.data.codigo) " />
                                                        </Listeners>
                                                    </ext:CommandColumn>
                                                </Columns>
                                            </ColumnModel>
                                              <Features>
                                                <ext:Grouping ID="Grouping1" runat="server" GroupHeaderTplString="{name}" HideGroupedHeader="true"
                                                    EnableGroupingMenu="false" />
                                            </Features>
                                             <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" Mode="Single" />
                                            </SelectionModel>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar3" runat="server" HideRefresh="true" />
                                            </BottomBar>
                                        </ext:GridPanel>

                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                   </Items>
             </ext:Panel>
             <ext:Window ID="window1" runat="server" StyleSpec="border: 0;" Modal="true" Hidden="true" Closable="true" Width="400" Height="120">

            <Items>
                <ext:FormPanel ID="FormPanel4" runat="server" ButtonAlign="Center" Frame="true" Height="100" >
                    <Items>
                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:Label ID="Label3" runat="server" Text="CLASE:" Width="80" Enabled="true"
                                    Margins="10 0 0 10" />
                                <ext:TextField ID="txt_clase" runat="server" Flex="4" Disabled="false" Margins="10 0 0 10"
                                    EmptyText="" />
                            </Items>
                        </ext:Container>
                         <ext:Container ID="botones" runat="server" Layout="HBoxLayout" >
                            <Items>
                                <ext:Button ID="btn_crearClase" runat="server" Flex="1" Icon="Add" Text="Agregar" Margins="10 10 0 10">
                                    <Listeners>
                                        <Click Handler="App.direct.GuardarClase(
                                            App.txt_clase.getValue()
                                        )" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="btn_modificarclase" runat="server" Flex="1" Icon="ApplicationEdit" Text="Editar" Margins="10 10 0 10" Hidden="true">
                                    <Listeners>
                                        <Click Handler="App.direct.ModificarClase([
                                            App.txt_clase.getValue(), #{idclase}.getValue()]);App.window1.hide();
                                        " />
                                    </Listeners>
                                </ext:Button>
                                 <ext:Button ID="btn_eliminarclase" runat="server" Flex="1" Icon="ApplicationDelete" Text="Eliminar" Margins="10 10 0 10" Hidden="true">
                                    <Listeners>
                                        <Click Handler="App.direct.EliminarClase(
                                            #{idclase}.getValue());App.window1.hide();
                                        " />
                                    </Listeners>
                                </ext:Button>
                               

                            </Items>
                        </ext:Container>
                    </Items>
                </ext:FormPanel>
            </Items>

        </ext:Window>
             <ext:Window ID="window2" runat="server" StyleSpec="border: 0;" Modal="true" Hidden="true" Closable="true" Width="400" Height="120">

            <Items>
                <ext:FormPanel ID="FormPanel5" runat="server" ButtonAlign="Center" Frame="true" Height="100" >
                    <Items>
                        <ext:Container ID="Container7" runat="server" Layout="HBoxLayout">
                        <Items>
                           
                            <ext:Label ID="Label4" runat="server" Text="Subclase:" Width="60" Enabled="true"
                                Margins="10 0 10 10" />
                            <ext:TextField ID="txt_subclase" runat="server" Flex="2" Disabled="false" Margins="10 0 10 10"
                                EmptyText="" />
                           
                            
                        </Items>
                    </ext:Container>
                     <ext:Container ID="Container10" runat="server" Layout="HBoxLayout">
                         <Items>
                             <ext:Button ID="btn_crearSubclase" runat="server" Flex="1" Icon="Add" Text="Agregar" Margins="0 0 0 10">
                                <Listeners>
                                    <Click Handler="App.direct.GuardarSubclase(                                   
                                    App.txt_subclase.getValue()
                                    )" />
                                </Listeners>
                            </ext:Button>
                             <ext:Button ID="btn_modificarsubclase" runat="server" Flex="1" Icon="ApplicationEdit" Text="Editar" Margins="0 0 0 10" Hidden="true">
                                    <Listeners>
                                        <Click Handler="App.direct.ModificarSubclase([
                                            App.txt_subclase.getValue(), #{idsubclase}.getValue()]);App.window2.hide();
                                        " />
                                    </Listeners>
                                </ext:Button>
                                 <ext:Button ID="btn_eliminarsubclase" runat="server" Flex="1" Icon="ApplicationDelete" Text="Eliminar" Margins="0 0 0 10" Hidden="true">
                                    <Listeners>
                                        <Click Handler="App.direct.EliminarSubclase(
                                            #{idsubclase}.getValue());App.window2.hide();
                                        " />
                                    </Listeners>
                                </ext:Button>

                         </Items>
                    </ext:Container>
                    </Items>
                </ext:FormPanel>
            </Items>

        </ext:Window>
             <ext:Window ID="window3" runat="server" StyleSpec="border: 0;" Modal="true" Hidden="true" Closable="true" Width="600" Height="120">

            <Items>
                <ext:FormPanel ID="FormPanel6" runat="server" ButtonAlign="Center" Frame="true" Height="100" >
                    <Items>
                        <ext:Container ID="Container8" runat="server" Layout="HBoxLayout">
                        <Items>
                             <ext:Label ID="lbl_caracteristica" runat="server" Text="Caracteristica:"  Enabled="true"  Flex="2"
                                Margins="0 0 0 10" />
                             <ext:Label ID="lbl_descripcion" runat="server" Text="Descripcion:" Flex="3" 
                                Margins="0 0 0 10" />
                            <ext:Label ID="Label2" runat="server" Text="Tipo Dato" Flex="2" Enabled="true"
                                Margins="0 0 0 10" />
                        </Items>
                     </ext:Container>
                    <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                        <Items>                      

                            <ext:TextField ID="txt_caracteristica" runat="server" Flex="2" Disabled="false" Margins="0 0 0 10"
                                EmptyText="" />
                            <ext:TextField ID="txt_descripcion" runat="server" Flex="3" Disabled="false" Margins="0 0 0 10"
                                EmptyText="" />
                            <ext:ComboBox ID="cbx_tipo" runat="server" ForceSelection="true" Margins="0 0 0 10"
                                DisplayField="tipo" ValueField="codigo" Flex="2">
                                <Store>
                                    <ext:Store runat="server" ID="store_tipo">
                                        <Model>
                                            <ext:Model ID="Model6" runat="server" IDProperty="codigo">
                                                <Fields>
                                                    <ext:ModelField Name="codigo" />
                                                    <ext:ModelField Name="tipo" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                            </ext:ComboBox>

                           
                        </Items>
                    </ext:Container>
                     <ext:Container ID="Container11" runat="server" Layout="HBoxLayout">
                        <Items>    
                             <ext:Button ID="btn_crearCaracteristica" runat="server" Flex="1" Text="Agregar" Icon="Add" FormBind="true" Margins="10 0 0 10">
                                <Listeners>
                                    <Click Handler="App.direct.Guardar_tbCaracteristica([
                                    App.txt_caracteristica.getValue(),
                                    App.txt_descripcion.getValue(),
                                    App.cbx_tipo.getValue(),
                                    ])" />
                                </Listeners>
                            </ext:Button>
                             <ext:Button ID="btn_ModCaracteristica" runat="server" Flex="1" Icon="ApplicationEdit" Text="Editar" Margins="10 0 0 10" Hidden="true">
                                    <Listeners>
                                        <Click Handler="App.direct.Modificar_tbCaracteristica([
                                            App.txt_caracteristica.getValue(), 
                                            App.txt_descripcion.getValue(), 
                                            App.cbx_tipo.getValue(), 
                                            #{idcaracteristica}.getValue()]);App.window3.hide();
                                        " />
                                    </Listeners>
                                </ext:Button>
                             <ext:Button ID="btn_ElimCaracteristica" runat="server" Flex="1" Icon="ApplicationDelete" Text="Eliminar" Margins="10 0 0 10" Hidden="true">
                                    <Listeners>
                                        <Click Handler="App.direct.Eliminar_tbCaracteristica(
                                            #{idcaracteristica}.getValue());App.window3.hide();
                                        " />
                                    </Listeners>
                                </ext:Button>
                        </Items>   
                     </ext:Container>
                    </Items>
                </ext:FormPanel>
            </Items>

        </ext:Window>
       </Items>
    </ext:Viewport>
    </form>
    <%-- caracteristica --%>
    <ext:Window ID="Window4" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="600">
            <Items>
                <ext:GridPanel ID="GridCaracteristica" runat="server" Title="Caracteristica" UI="Info" Border="true" Height="435" StoreID="Store_tbcaracteristica">
                    <Tools>
                        <ext:Tool ID="Tool2" Type="Close">
                            <Listeners>
                                <Click Handler="#{Window4}.hide();" />
                            </Listeners>
                        </ext:Tool>
                    </Tools>
                    <ColumnModel ID="ColumnModel4" runat="server">
                        <Columns>
                            <ext:Column ID="Column9" runat="server" Text="Codigo" DataIndex="codigo" Align="Center" Width="70">
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
                           
                            <ext:Column ID="Column13" runat="server" Text="Caracteristica" DataIndex="caracteristica" Align="Center" Flex="1">
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
                            <ext:Column ID="Column14" runat="server" Text="Descripcion" DataIndex="descripcion" Flex="1">
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
                            <ext:Column ID="Column15" runat="server" DataIndex="tipo" Text="Tipo Dato" Hidden="true"  Align="Center" Width="100"/>
                            <ext:CommandColumn ID="CommandColumn12" runat="server" Align="Center" Width="27">
                                <HeaderItems>
                                    <ext:Button ID="Button11" runat="server" Icon="Add" ToolTip="Adicionar Caracteristica" FormBind="false">
                                        <Listeners>
                                            <Click Handler="App.direct.AddCaracteristica()" />
                                        </Listeners>
                                    </ext:Button>
                                </HeaderItems>
                            </ext:CommandColumn>
                            <ext:CommandColumn ID="CommandColumn13" runat="server" Align="Center" Width="27">
                                <HeaderItems>
                                    <ext:Button ID="Button12" runat="server" Icon="ApplicationEdit" ToolTip="Modificar Caracteristica" FormBind="false" Disabled="true">
                                        <Listeners>
                                            <Click Handler="App.direct.EditCaracteristica()" />
                                        </Listeners>
                                    </ext:Button>
                                </HeaderItems>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <ItemDblClick OnEvent="GridComodato_SelectRow2" After="#{Tool2}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="Codigo" Value="record.get('codigo')" Mode="Raw" />
                                <ext:Parameter Name ="Caracteristica"   Value="record.get('caracteristica')" Mode="Raw" />
                                <ext:Parameter Name ="Descripcion"   Value="record.get('descripcion')" Mode="Raw" />
                                <ext:Parameter Name ="Tipo"   Value="record.get('tipo')" Mode="Raw" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Target="Page" UseMsg="false" />
                        </ItemDblClick>
                    </DirectEvents>
                    <BottomBar>
                        <ext:PagingToolbar ID="PagingToolbar4" runat="server" HideRefresh="true" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Window>
    <%-- clase --%>
     <ext:Window ID="Window5" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="500">
            <Items>
                <ext:GridPanel ID="GridClase" runat="server" Title="Clase" UI="Info" Border="true" Height="435" StoreID="Store_clase">
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
                            <ext:CommandColumn ID="CommandColumn3" runat="server" Align="Center" Width="27">
                                <HeaderItems>
                                    <ext:Button ID="Button4" runat="server" Icon="Add" ToolTip="Adicionar Clase">
                                        <Listeners>
                                            <Click Handler="App.direct.AddClase();" />
                                        </Listeners>
                                    </ext:Button>
                                </HeaderItems>
                            </ext:CommandColumn>
                            <ext:CommandColumn ID="CommandColumn4" runat="server" Align="Center" Width="27">
                                <HeaderItems>
                                    <ext:Button ID="Button5" runat="server" Icon="ApplicationEdit" ToolTip="Modificar Clase" Disabled="true">
                                        <Listeners>
                                            <Click Handler="App.direct.EditClase();" />
                                        </Listeners>
                                    </ext:Button>
                                </HeaderItems>
                            </ext:CommandColumn>
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
                    <BottomBar>
                        <ext:PagingToolbar ID="PagingToolbar1" runat="server" HideRefresh="true" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Window>
       <ext:Window ID="Window6" runat="server" Header="false" Shadow="false" StyleSpec="border: 0;" Modal="true" Hidden="true" Width="500">
            <Items>
                <ext:GridPanel ID="GridSubclase" runat="server" Title="Clase" UI="Info" Border="true" Height="435" StoreID="Store_subclase">
                    <Tools>
                        <ext:Tool ID="Tool3" Type="Close">
                            <Listeners>
                                <Click Handler="#{Window6}.hide();" />
                            </Listeners>
                        </ext:Tool>
                    </Tools>
                    <ColumnModel ID="ColumnModel5" runat="server">
                        <Columns>
                            <ext:Column ID="Column11" runat="server" Text="Codigo" DataIndex="codigosubclase" Align="Center" Width="70">
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
                            <ext:Column ID="Column12" runat="server" Text="Subclase" DataIndex="descripcionsubclase" Align="Center" Flex="1">
                                <HeaderItems>
                                    <ext:TextField ID="TextField7" runat="server" FieldCls="text-center">
                                        <Listeners>
                                            <Change Handler="this.up('grid').applyFilter();" />
                                        </Listeners>
                                        <Plugins>
                                            <ext:ClearButton ID="ClearButton7" runat="server" />
                                        </Plugins>
                                    </ext:TextField>
                                </HeaderItems>
                            </ext:Column>
                            <ext:CommandColumn ID="CommandColumn2" runat="server" Align="Center" Width="27">
                                <HeaderItems>
                                    <ext:Button ID="Button3" runat="server" Icon="Add" ToolTip="Adicionar Subclase">
                                        <Listeners>
                                            <Click Handler="App.direct.AddSubclase();" />
                                        </Listeners>
                                    </ext:Button>
                                </HeaderItems>
                            </ext:CommandColumn>
                            <ext:CommandColumn ID="CommandColumn10" runat="server" Align="Center" Width="27">
                                <HeaderItems>
                                    <ext:Button ID="Button9" runat="server" Icon="ApplicationEdit" ToolTip="Modificar Subclase" Disabled="true">
                                        <Listeners>
                                            <Click Handler="App.direct.EditSubClase();" />
                                        </Listeners>
                                    </ext:Button>
                                </HeaderItems>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <ItemDblClick OnEvent="GridComodato_SelectRow1" After="#{Tool3}.fireEvent('click');">
                            <ExtraParams>
                                <ext:Parameter Name="Codigo" Value="record.get('codigosubclase')" Mode="Raw" />
                                <ext:Parameter Name ="Nombre"   Value="record.get('descripcionsubclase')" Mode="Raw" />
                            </ExtraParams>
                            <EventMask ShowMask="true" Target="Page" UseMsg="false" />
                        </ItemDblClick>
                    </DirectEvents>
                    <BottomBar>
                        <ext:PagingToolbar ID="PagingToolbar5" runat="server" HideRefresh="true" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Window>
</body>
</html>
