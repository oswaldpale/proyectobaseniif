<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar.aspx.cs" Inherits="Activos.Administrar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Administrar_Permisos</title>
    <script src="Scripts/JS_Global.js" type="text/javascript"></script>
    <script src="Scripts/JS_Administrar.js" type="text/javascript"></script>
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Hidden ID="varUser" runat="server" />

    <div>
        <ext:Viewport ID="Viewport1" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="Panel2" runat="server" Title="Modulos" Layout="FitLayout" Collapsible="true" Region="West" Split="true" Flex="1">
                    <Items>
                        <ext:GridPanel ID="data_Modulo" runat="server" Border="false">
                            <Store>
                                <ext:Store ID="Store2" runat="server" PageSize="25">
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" IDProperty="codigo">
                                            <Fields>
                                                <ext:ModelField Name="codigo" />
                                                <ext:ModelField Name="nombre" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" />
                                    <ext:Column ID="Column3" runat="server" DataIndex="nombre" Flex="1">
                                        <Renderer Handler="return Ext.util.Format.uppercase(value);" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <Select Handler="if(#{Panel3}.items.items.length > 0) { #{Panel3}.items.removeAll(false); } App.direct.data_Formularios_Load(record.data);" />
                            </Listeners>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar2" runat="server" HideRefresh="true" />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel4" runat="server" Title="Usuarios" Layout="FitLayout" Collapsible="true" Region="East" Split="true" Flex="1">
                    <Items>
                        <ext:GridPanel ID="data_User" runat="server" Border="false">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="25">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="login">
                                            <Fields>
                                                <ext:ModelField Name="login" />
                                                <ext:ModelField Name="nombre" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel3" runat="server">
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" DataIndex="nombre" Text="Usuario" Flex="2">
                                        <HeaderItems>
                                            <ext:TextField ID="txtUsuario" runat="server" FieldStyle="text-transform: uppercase;" EnableKeyEvents="true">
                                                <Listeners>
                                                    <Change Handler="_Filter(App.data_User, getRecordFilter());" />
                                                </Listeners>
                                            </ext:TextField>
                                        </HeaderItems>
                                    </ext:Column>
                                    <ext:Column ID="Column2" runat="server" DataIndex="login" Text="Login" Flex="1">
                                        <HeaderItems>
                                            <ext:TextField ID="txtLogin" runat="server" FieldStyle="text-transform: uppercase;" EnableKeyEvents="true">
                                                <Listeners>
                                                    <Change Handler="_Filter(App.data_User, getRecordFilter());" />
                                                </Listeners>
                                            </ext:TextField>
                                        </HeaderItems>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumn1" runat="server" Align="Center" Width="27">
                                        <HeaderItems>
                                            <ext:Button ID="ClearFilter" runat="server" Icon="Cancel" ToolTip="Limpiar Filtro">
                                                <Listeners>
                                                    <Click Handler="clearFilter();" />
                                                </Listeners>
                                            </ext:Button>
                                        </HeaderItems>
                                        <Renderer Handler="return null" />
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <Select Handler="App.direct.data_User_Load(record.data);" />
                            </Listeners>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" HideRefresh="true" />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel13" runat="server" Layout="Fit" Region="Center" Title="Formularios" Flex="1">
                    <Items>
                        <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="1" Border="false">
                            <Items>
                                <ext:Panel ID="Panel1" runat="server" Title="General" Layout="FitLayout" Border="false">
                                </ext:Panel>
                                <ext:Panel ID="Panel3" runat="server" Title="Usuario" Layout="FitLayout" Border="false">
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <%--<ext:Panel ID="Panel1" runat="server" Layout="HBoxLayout" Border="false" Height="500">
            <Items>
            </Items>
            <Plugins>
                <ext:BoxReorderer ID="BoxReorderer1" runat="server" />
            </Plugins>
        </ext:Panel>--%>
    </div>
    </form>
</body>
</html>
