<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subclase.aspx.cs" Inherits="Activos.Modulo.CrearSubclase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<ext:XScript ID="XScript1" runat="server">
    <script type="text/javascript" >
 //******** filtros6*****************
    
             var filterStore = [];

             var applyFilter6 = function (field) {                
                var store = Ext.getCmp("GridPanel6").getStore();
                store.filterBy(getRecordFilter6());    
                store.applyPaging();                                            
            };
             var borrarFiltro6 = function () {
                Ext.getCmp("txt_filtrocodigoclase").reset();
                Ext.getCmp("txt_filtrodescripcionclase").reset();
              
                Ext.getCmp("Store_clase").clearFilter();
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
                        return filterNumber6(Ext.getCmp("txt_filtrocodigoclase").getValue(), "codigoclase", record);
                    }
                });
                 
               f.push({
                    filter: function (record) {                         
                        return filterString6(Ext.getCmp("txt_filtrodescripcionclase").getValue() || "", "descripcionclase", record);
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
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
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
            <ext:FormPanel ID="FormPanel1" runat="server" Width="510" Height="370" Frame="true" 
                TitleAlign="Center" BodyPadding="13" AutoScroll="true">
                <FieldDefaults LabelAlign="Top" LabelWidth="11" MsgTarget="Side" />
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                    <Items>
                         <ext:Label ID="lbl_clase" runat="server" Text="Clase:" Width="60" Enabled="true"
                                Margins="0 0 0 10" />                         
                           <%--************ Filtro de clase***************--%>
                         <ext:DropDownField ID="Dropclase" runat="server" FieldStyle="text-align: center;"
                                  Editable="false" TriggerIcon="SimpleArrowDown"  Flex="1"
                                  Hidden="false" Margins="0 0 0 10" AllowBlank="false">
                                  <Component>
                                      <ext:GridPanel ID="GridPanel6" runat="server" Height="250" StoreID="Store_clase" Flex="1">
                                          <ColumnModel ID="ColumnModel7" runat="server">
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
                                              </Columns>
                                          </ColumnModel>
                                               <Listeners>
                                                        <Select Handler="#{Dropclase}.setValue(record.data['descripcionclase']);
                                                                         #{idclase}.setValue(record.data['codigoclase']);  " />                                             
                                               </Listeners>
                                          <BottomBar>
                                              <ext:PagingToolbar ID="PagingToolbar6" runat="server" HideRefresh="true" />
                                          </BottomBar>
                                      </ext:GridPanel>
                                  </Component>                                  
                              </ext:DropDownField>
                    </Items>
                    </ext:Container> 
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                           
                            <ext:Label ID="lbl_subclase" runat="server" Text="Subclase:" Width="60" Enabled="true"
                                Margins="10 0 10 10" />
                            <ext:TextField ID="txt_subclase" runat="server" Flex="2" Disabled="false" Margins="10 0 10 10"
                                EmptyText="" />
                           
                            <ext:Button ID="btn_crearSubclase" runat="server" Flex="1" Icon="Add" Text="SUBCLASE" Margins="10 0 0 10">
                                <Listeners>
                                    <Click Handler="App.direct.Guardar(
                                    App.GridPanel1.getStore().getRecordsValues(),                                    
                                    App.txt_subclase.getValue()
                                    )" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                        <Items>
                             <ext:Panel ID="PanelClase" runat="server" Flex="1" Margins="10 0 0 10">
                                <Items>
                                    <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Height="250">
                                        <Store>
                                            <ext:Store ID="store_hclase" runat="server" GroupField="clase" >
                                                <Model>
                                                    <ext:Model ID="Model5" runat="server" IDProperty="subclase">
                                                        <Fields>   
                                                                                                                    
                                                            <ext:ModelField Name="clase" />  
                                                            <ext:ModelField Name="subclase" />                                                                                                                  
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>                                                
                                                 <ext:Column ID="Column3" runat="server" DataIndex="clase" Text="Clase"
                                                    Flex="1"> 
                                                </ext:Column>  
                                                <ext:Column ID="Column1" runat="server" DataIndex="subclase" Text="Subclase"
                                                    Flex="1">
                                                </ext:Column>                                               
                                            </Columns>
                                        </ColumnModel>
                                         <Features>
                                            <ext:Grouping ID="Group1" runat="server" GroupHeaderTplString="{name}" HideGroupedHeader="true"
                                                EnableGroupingMenu="false" />
                                        </Features>                                       
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
