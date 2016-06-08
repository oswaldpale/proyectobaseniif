//-- Formulario Login.aspx --//
function EnterKey(txt, key) {
    if (key.getKey() == 13) {
        if (Ext.isEmpty(txt.getValue())) return;
                
        switch (txt.id) {
            case "txtUsuario":
                if (!Ext.isEmpty(App.txtPassword.getValue())) {
                    App.direct.btnIngresar_Click();
                }
                else{
                    App.txtPassword.focus(true);
                }
                break;
            case "txtPassword":
                if (!Ext.isEmpty(App.txtUsuario.getValue())) {
                    App.direct.btnIngresar_Click();
                }
                else{
                    App.txtUsuario.focus(true);
                }
                break;
        }
    }
}

//-- Formulario Default.aspx --//
//** Instancia Ventanas Dinamicas para el Desktop
function DynamicWindow(app, pageId, pageName, pageUrl, pageWidth, pageHeight, pageModal) 
{
    var desk = app.getDesktop();
    var win = null;

    if (desk.getWindow(pageId)) 
    {
        desk.getWindow(pageId).show();
        desk.getWindow(pageId).reload();
    }
    else 
    {
        win = desk.createWindow({
            id: pageId,
            title: Ext.util.Format.uppercase(pageName)/* + " - " + pageUrl*/,
            titleAlign: "center",
            width: pageWidth,
            height: pageHeight,
            bodyPadding: 5,
            plain: true,
            resizable: false,
            maximizable: true,
            minimizable: true,
            closeAction: "destroy",
            modal: pageModal,
            loader:
            {
                url: pageUrl,
                renderer: "frame",
                loadMask:
                {
                    showMask: true,
                    msg: "Cargando Formu ..."
                }
            },
            tbar:
            {
                items:
                [{
                    text: "Recargar Formulario",
                    icon: "Reload",
                    handler: function () 
                    {
                        desk.getWindow(pageId).reload();
                    }
                }]
            }
        });
    }

    desk.getWindow(pageId).center();
    desk.getWindow(pageId).show();
}

//**
function render(value){
   var template = '<span style="color:{0};">{1}</span>';
   return Ext.String.format(template, "#0B610B", value);
}

//-- Formulario PanelFast.aspx --//
function selectionChanged(dv, nodes) {
    if (nodes.length > 0) {
        var item = nodes[0].data;
        var dir = window.location.href;
        dir = dir.substring(0, dir.lastIndexOf("/")) + item.url;
        parent.DynamicWindow(parent.App.Desktop1, "win_" + item.codigo, item.nombre, dir, item.ancho, item.alto, false);
        dv.deselectAll(false);
    }
};

function getPanelFast(){
    var desk = App.Desktop1.getDesktop();
    var win = desk.windows.get('Panel1');

    if (win) {
        win.isVisible() ? win.hide() : win.show();
    }
    else {
        win = desk.createWindow({
            id: "Panel1",
            title: Ext.util.Format.uppercase("Panel Rápido"),
            border: false,
            titleAlign: "center",
            width: 990,
            height: 400,
            plain: true,
            resizable: false,
            maximizable: false,
            minimizable: false,
            closable: false,
            draggable: false,
            bodyStyle: "background: transparent;",
            loader:
            {
                url: "PanelFast.aspx",
                renderer: "frame",
                loadMask:
                {
                    showMask: true,
                    msg: "Cargando Formulario ..."
                }
            }
        });

        desk.windows.get(win.id).center();
        desk.windows.get(win.id).show();
    }
}
