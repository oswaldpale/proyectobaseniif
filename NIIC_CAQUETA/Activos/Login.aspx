<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Activos.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="Estilos/styleSheet.css" rel="stylesheet" type="text/css" />
    <script src="JS/JS_Inicio.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            background-image: url(Imagenes/wallpapers/logon.jpg);
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-size: 100% 100%;
        }
    </style>
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Gray">
    </ext:ResourceManager>

    <div>
        <ext:Window ID="Window1" runat="server" Title="Sistema de Información Gas Caqueta .Net -- Sigc_net" Icon="User" Closable="false" 
            Resizable="false" Draggable="false" Height="310" Width="500">
           <Content>
                <div align="center">
                    <div id="logo_top"></div>

                    <ext:FormPanel ID="FormPanel1" runat="server" Border="false" HideLabels="true" BodyStyle="background:transparent;">
                        <Defaults>
                            <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                        </Defaults>
                        <Content>
                            <div>
                                <ext:TextField ID="txtUsuario" runat="server" Icon="User" EnableKeyEvents="true" BlankText="Ingrese su Usuario." FieldStyle="text-transform: uppercase; text-align: center;" AnchorHorizontal="45%" Height="25">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="Ingrese su Usuario." Anchor="right" Target="txtUsuario" />
                                    </ToolTips>
                                    <Listeners>
                                        <KeyPress Fn="EnterKey" Buffer="100" />
                                    </Listeners>
                                </ext:TextField>
                                <ext:TextField ID="txtPassword" runat="server" Icon="Lock" EnableKeyEvents="true" BlankText="Ingrese su Contraseña." InputType="Password" FieldStyle="text-align: center;" AnchorHorizontal="45%" Height="25">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="Ingrese su Contraseña." Anchor="right" Target="txtPassword" />
                                    </ToolTips>
                                    <Listeners>
                                        <KeyPress Fn="EnterKey" Buffer="100" />
                                    </Listeners>
                                </ext:TextField>
                            </div>
                        </Content>
                    </ext:FormPanel>
                    <div id="logo_bottom"></div>
                </div>
           </Content>
           <Buttons>
                <ext:Button ID="btn_Ingresar" runat="server" Text="Ingresar" Icon="Key" Scale="Medium">
                    <Listeners>
                        <Click Handler="if(App.FormPanel1.isValid()){ App.direct.btnIngresar_Click(); }" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>

