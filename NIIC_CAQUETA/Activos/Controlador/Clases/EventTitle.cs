using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace Activos.Clases
{
    public class EventTitle : PdfPageEventHelper
    {
        private List<IElement> _objHeader;

        public EventTitle(string pagEncabezado)
        {
            _objHeader = HTMLWorker.ParseToList(new StringReader(pagEncabezado), new StyleSheet());
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            for (int k = 0; k < _objHeader.Count; k++)
            {
                document.Add((IElement)_objHeader[k]);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
        }
    }
}