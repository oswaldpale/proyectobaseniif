<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tplChoose.aspx.cs" Inherits="Activos.tplChoose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="EstiloCSS/tplChoose.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /**/
	    #unlicensed{
		    display: none !important;
	    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="v-wrap">
        <div id="v-view">
        </div>
        <div id="v-module">
            <div class="v-thumb-module">
                <asp:ImageButton ID="ImageButton1" runat="server" />
		        <img src=""  alt="Image1" height="32" width="32" />
                <div class="v-module-name">
                    <span>Image1</span>
                </div>
	        </div>
            <div class="v-thumb-module">
		        <img src="" alt="Image2" height="32" width="32" />
	            <div class="v-module-name">
                    <span>Image2</span>
                </div>
	        </div>
            <div class="v-thumb-module">
		        <img src="" alt="Image2" height="32" width="32" />
	            <div class="v-module-name">
                    <span>Image3</span>
                </div>
	        </div>
            <div class="v-thumb-module">
		        <img src="" alt="Image2" height="32" width="32" />
	            <div class="v-module-name">
                    <span>Image4</span>
                </div>
	        </div>
            <div class="v-thumb-module">
		        <img src="" alt="Image2" height="32" width="32" />
	            <div class="v-module-name">
                    <span>Image5</span>
                </div>
	        </div>
        </div>
    </div>
    </form>
</body>
</html>

