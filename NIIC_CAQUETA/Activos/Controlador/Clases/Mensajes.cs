using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activos.Clases
{
    public class Mensajes
    {
        public void NotificacionArriba(string Title,string mensajeHtml) {
            Notification.Show(new NotificationConfig
            {
                Title = Title,
                AlignCfg = new NotificationAlignConfig
                {
                    ElementAnchor = AnchorPoint.TopRight,
                    TargetAnchor = AnchorPoint.TopRight,
                    OffsetX = -20,
                    OffsetY = 20
                },
                ShowFx = new SlideIn { Anchor = AnchorPoint.TopRight, Options = new FxConfig { Easing = Easing.BounceOut } },
                HideFx = new Ghost { Anchor = AnchorPoint.TopRight },
                Html = mensajeHtml
            });
        }
        public void CuadroMensaje(string Title, string mensajeHtml)
        {
            X.MessageBox.Show(new MessageBoxConfig
            {
                Title = Title,
                Message = mensajeHtml,
                Buttons = MessageBox.Button.OK,
                Icon = MessageBox.Icon.INFO,
                Closable = true
            });
        }
    }
}