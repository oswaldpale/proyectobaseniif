<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Activos.InicioActivoFijos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="JS/JScript2.js" type="text/javascript"></script>
<script type="text/javascript">
    function AbrirVentana() {
        App.direct.AbrirVentana();       
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sistema Información de Normas Internacionales De Gas Caquetá .Net</title>
    <link rel="shortcut icon" href="../Imagenes/Porty.png" type="image/x-icon" /> 
     <script src="JS/JS_Inicio.js" type="text/javascript"></script>
    <link href="Estilos/tplChoose.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default">
    </ext:ResourceManager>

    <%--Desktop Manager--%>
    <ext:Desktop ID="Desktop1" runat="server">
        <StartMenu Title="Sistema Información de Normas Internacionales De Gas Caquetá -- Sigc.Net" Icon="User" Height="360" Width="360">
        </StartMenu>

        <%--** Barra de Tareas--%>
        <TaskBar Dock="None" HideTray="true" HideQuickStart="true" >
         <CustomConfig>
                <ext:ConfigItem Name="startBtnText" Value="Inicio" Mode="Value" />
            </CustomConfig>
            <Tray>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                    </Items>
                </ext:Toolbar>
            </Tray>
            <QuickStart>
                <ext:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                    </Items>
                </ext:Toolbar>
            </QuickStart>
            <TrayClock Dock="Left" TimeFormat="dddd, dd \de MMMM \de yyyy  hh:mm:ss tt" RefreshInterval="1">
            </TrayClock>
        </TaskBar>

        <%--** Entorno Desktop Config--%>
            <DesktopConfig Wallpaper="Imagenes/wallpapers/desktop.jpg" WallpaperStretch="true" ShortcutDragSelector="true">
            <ShortcutDefaults IconCls="x-default-shortcut" />
                
            <%--** Menu Destokp--%>
            <ContextMenu>
                <ext:Menu ID="Menu1" runat="server">
                    <Items>
                        <ext:MenuItem ID="MenuItem1" runat="server" Text="Administrar Permisos" Icon="Cog">
                            <Listeners>
                                <Click Handler="if(App.Hidden1.getValue() == 'ADMIN'){ DynamicWindow(#{Desktop1},'Admin','Administrar Permisos','Administrar.aspx',1200,600, false); }
                                                        else{ Ext.Msg.show({
                                                                    title:'Sigc. Info.',
                                                                    msg: 'No tienes habilitada la opcion para la gestion de permisos a usuarios.',
                                                                    buttons: Ext.Msg.OK,
                                                                    icon: Ext.Msg.INFO,
                                                                    closable: false,
                                                                    modal: true
                                                            }); }" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem2" runat="server" Text="Finalizar Sesión" Icon="Key">
                            <DirectEvents>
                                <Click OnEvent="btnLogout_Click">
                                    <EventMask ShowMask="true" Msg="Finalizando Sessión ..." Target="Page" />
                                </Click>
                            </DirectEvents>
                        </ext:MenuItem>
                        <ext:MenuSeparator />
                        <ext:MenuItem ID="MenuItem3" runat="server" Text="Panel Rápido" Icon="ApplicationCascade">
                            <Listeners>
                                <Click Handler="getPanelFast();" />
                            </Listeners>
                        </ext:MenuItem>
                    </Items>
                </ext:Menu>
            </ContextMenu>

            <Content>
                <%--** Logo--%>
                <ext:Image ID="Image1" runat="server" ImageUrl="Imagenes/logo.png" StyleSpec="position:absolute; top: 3%; left: 3%; width: 350px; height: 120px;" />
                <%--** Panel_Info--%>
                <ext:Panel ID="Panel2" runat="server" Frame="true" Title="Sistema Información Gas Caqueta .Net -- Sigc.Net" 
                    StyleSpec="position:absolute; top: 3%; left: 70%;" Height="100" Width="400">
                    <Items>
                        <ext:DisplayField ID="DisplayField1" runat="server">
                            <Renderer Handler="return render(this.getValue());" />
                        </ext:DisplayField>
                        <ext:Hidden ID="Hidden1" runat="server" />
                    </Items>
                    <BottomBar>
                        <ext:Toolbar ID="Toolbar4" runat="server" Border="false">
                            <Items>
                                <ext:Button ID="txtPanelFast" runat="server" Text="Panel Rápido" Icon="ApplicationCascade" Scale="Medium">
                                    <Listeners>
                                        <Click Handler="getPanelFast();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill />
                                <ext:Button ID="btnLogout" runat="server" Text="Finalizar Sesión" Icon="Key" Scale="Medium">
                                    <DirectEvents>
                                        <Click OnEvent="btnLogout_Click">
                                            <EventMask ShowMask="true" Msg="Finalizando Sessión ..." Target="Page" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </BottomBar>
                </ext:Panel>
            </Content>
        </DesktopConfig>
    </ext:Desktop>
    </form>
</body>
</html>
