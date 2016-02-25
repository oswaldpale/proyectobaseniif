using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Ext.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace Activos.Clases
{
    public class ReportePDF
    {
        private string FilePath;
        private List<string> template;
        private string init;

        #region Secciones del Documento
        public List<string> Template
        {
            get
            {
                return this.template;
            }
        }

        /// <summary>
        /// Finaliza el cuerpo del documento HTML
        /// </summary>
        public string Fin
        {
            get
            {
                return "</body></html>";
            }
        }

        /// <summary>
        /// Inicializa el cuerpo del documento HTML
        /// </summary>
        public string Inicio
        {
            get
            {
                return this.init;
            }
        }
        #endregion

        public ReportePDF(string fileName)
        {
            this.template = this.LoadTemplate(fileName);
        }

        private List<string> LoadTemplate(string fileName)
        {
            //throw new Exception(Global.path + "/Templates/" + fileName + ".htm");
            string url = Global.path2 + "/Templates/" + fileName + ".htm";
            Uri targetUri = new Uri(Global.path2+"/Templates/" + fileName + ".htm");


            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(targetUri);
            StreamReader sr = new StreamReader(http.GetResponse().GetResponseStream());
            string html = sr.ReadToEnd();
            template = new List<string>();

            /* Barra Vertical partida - Codigo ASCII 221 - ( ¦ ) : Representa un indicador de separación de secciones del docuemnto HTML */
            int k = 0;
            while (true)
            {
                k = html.IndexOf('¦');
                if (k.Equals(-1))
                {
                    template.Add(html.Substring(0, html.Length).Trim());
                    break;
                }
                template.Add(html.Substring(0, k));
                html = html.Remove(0, k + 1);
            }

            // Inicio del documento HTML
            this.init = template[0];

            // Encabezado de Página
            template[1] = this.Inicio + template[1] + this.Fin;

            return template;
        }

        public void createPDF(string html, string pageHeader, string fileName, Document doc)
        {
            try
            {
                html = html.Replace("¦", "");
                string File = fileName + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".pdf";
                FilePath = HttpRuntime.AppDomainAppPath + @"Reportes\" + File;
                string direcccion = @"C:\" + File;

                Document document = doc;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.OpenOrCreate));
                EventTitle ev = new EventTitle(pageHeader);
                writer.PageEvent = ev;

                List<IElement> element = HTMLWorker.ParseToList(new StringReader(html), null);
                document.Open();

                for (int k = 0; k < element.Count; k++)
                    document.Add((IElement)element[k]);

                document.Close();
                string s = Global.path2.ToString();
                string f = s.Replace("/Modulo","");
                X.Js.Call("window.open", f + "/Reportes/" + File, fileName);
            }
            catch (Exception exc)
            {
                throw new Exception("No se pudo generar el Archivo PDF. " + exc.Message + exc.StackTrace);
            }
        }

        private StyleSheet _LoadStyle(string FileStyle)
        {
            StyleSheet style = new StyleSheet();
            //string path = contex.Server.MapPath("../EstiloCSS/StylePDF/StyleStandar.css");
            string path = Global.path2 + "/EstiloCSS/" + FileStyle + ".css";
            path = HttpRuntime.AppDomainAppPath + @"EstiloCSS\" + FileStyle + ".css";

            if (File.Exists(path))
            {
                string content = CleanUp(File.ReadAllText(path));
                string[] parts = content.Split('}');
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    string[] parts1 = parts[i].Split('{');
                    string str = parts1[0].ToString().Trim().StartsWith("}") ? parts1[0].ToString().Trim().Remove(0, 1) : parts1[0].ToString().Trim();

                    foreach (string nombreEstilo in str.Split(','))
                    {
                        foreach (string detalleEstilo in parts1[1].ToString().Trim().Split(';'))
                        {
                            string[] valorDetalleEstilo = detalleEstilo.Split(':');
                            if (valorDetalleEstilo.Length >= 2)
                            {
                                style.LoadTagStyle(nombreEstilo, valorDetalleEstilo[0].Trim(), valorDetalleEstilo[1].Trim());
                            }
                        }
                    }
                }
            }

            return style;
        }


        private string CleanUp(string s)
        {
            string temp = s;
            string reg = "(/\\*(.|[\r\n])*?\\*/)|(//.*)";
            Regex r = new Regex(reg);
            temp = r.Replace(temp, "");
            temp = temp.Replace("\r", "").Replace("\n", "");
            return temp;
        }

        public Document getPageConfig(string PageType, bool Rotation)
        {
            iTextSharp.text.Document doc = new Document();

            switch (PageType.ToUpper())
            {
                case "LETTER":
                    doc = !Rotation ? new Document(iTextSharp.text.PageSize.LETTER, 50f, 50f, 50f, 50f) : new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 50f, 50f, 50f, 50f);
                    break;
                case "LEGAL":
                    doc = !Rotation ? new Document(iTextSharp.text.PageSize.LEGAL, 50f, 50f, 50f, 50f) : new Document(iTextSharp.text.PageSize.LEGAL.Rotate(), 50f, 50f, 50f, 50f);
                    break;
                case "A4":
                    doc = !Rotation ? new Document(iTextSharp.text.PageSize.A4, 50f, 50f, 50f, 50f) : new Document(iTextSharp.text.PageSize.A4.Rotate(), 50f, 50f, 50f, 50f);
                    break;
                case "POST":
                    doc = new Document(new iTextSharp.text.Rectangle(216, 792), 5f, 5f, 5f, 5f);
                    break;
                default:
                    string str = "El tipo de Papel {0} no posee una configuración.";
                    throw new Exception(String.Format(str, PageType));
            }

            return doc;
        }
    }

}