<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelFast.aspx.cs" Inherits="Activos.PanelFast" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Estilos/tplChoose.css" rel="stylesheet" type="text/css" />
    <script src="JS/JS_Inicio.js" type="text/javascript"></script>
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" action="tplChoose.aspx">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default">
    </ext:ResourceManager>
    <div>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Border">
            <Items>
                <ext:Panel ID="Panel2" runat="server" Border="false" Region="West" Title="Modulos" TitleAlign="Center" Layout="Fit" Margins="5" Collapsible="true" Split="true" MaxWidth="330" Width="330">
                    <Items>
                        <ext:GridPanel ID="data_Modulo" runat="server">
                            <Store>
                                <ext:Store ID="Store2" runat="server">
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
                                    <ext:Column ID="Column3" runat="server" Sortable="false" DataIndex="nombre" Flex="1">
                                        <Renderer Handler="return Ext.util.Format.uppercase(value);" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <Select Handler="App.direct.getFormModules(record.data);" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel9" runat="server" Margins="5" Region="Center" BodyPadding="10" BodyStyle="background: white;" Border="false">
                    <Items>
                        <ext:DataView ID="DataView1" runat="server" SingleSelect="true" Cls="img-chooser-view" OverItemCls="x-view-over" ItemSelector="div.thumb-wrap">
                            <Store>
                                <ext:Store ID="Store1" runat="server">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="codigo">
                                            <Fields>
                                                <ext:ModelField Name="codigo" />
                                                <ext:ModelField Name="nombre" />
                                                <ext:ModelField Name="url" />
                                                <ext:ModelField Name="ancho" Type="Int" />
                                                <ext:ModelField Name="alto" Type="Int" />
                                                <ext:ModelField Name="icon" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <Tpl ID="Tpl1" runat="server">
                                <Html>
                                    <tpl for=".">
                                        <div class="thumb-wrap">
                                            <div class="thumb">
                                                <tpl if="!Ext.isIE6">
                                                    <img src="{icon}" alt="{nombre}" height="105" width="105" />
                                                </tpl>
                                            </div>
                                            <span>{nombre}</span>
                                        </div>
                                    </tpl>
                                </Html>
                            </Tpl>
                            <Listeners>
                                <SelectionChange Fn="selectionChanged" />
                            </Listeners>
                        </ext:DataView>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
