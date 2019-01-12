using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BegoSys.TO;

namespace BegoSys.Core.Facturacion
{
    public class ImprimirFactura
    {
        public static void PrintReceiptForTransaction(DatosLMTO DatosFactura)
        {
            /*Debe contener el nombre o razón social de la empresa.
            NIT de la persona o empresa que vende un producto o servicio.
            Fecha en la que se expide la factura.
            El documento debe decir claramente “Factura de Venta”.
            Nombre o razón del cliente.
            NIT o identificación del cliente.
            Descripción de los servicios o productos vendidos.
            Consecutivo de la factura (según la resolución de la DIAN).
            El valor total de la factura, con el discriminado de la tarifa y el valor de IVA pagado.
            Y finalmente la resolución de facturación por computador autorizada por la DIAN.*/

            PrintDocument recordDoc = new PrintDocument();

            recordDoc.DocumentName = "Customer Receipt";
            //recordDoc.PrintPage += new PrintPageEventHandler(PrintReceiptPage()); // function below
            recordDoc.PrintController = new StandardPrintController(); // hides status dialog popup
                                                                       // Comment if debugging 
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = "EPSON TM-T20II Receipt";
            recordDoc.PrinterSettings = ps;
            recordDoc.Print();
            // --------------------------------------

            // Uncomment if debugging - shows dialog instead
            //PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            //printPrvDlg.Document = recordDoc;
            //printPrvDlg.Width = 1200;
            //printPrvDlg.Height = 800;
            //printPrvDlg.ShowDialog();
            // --------------------------------------

            recordDoc.Dispose();
        }

        private static void PrintReceiptPage(DatosLMTO DatFac, object sender, PrintPageEventArgs e)
        {
            float x = 10;
            float y = 5;
            float width = 270.0F; // max width I found through trial and error
            float height = 0F;

            Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            StringFormat drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            StringFormat drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            // Draw string to screen.
            string text = "JuiceBar.co Unicentro";
            e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

            text = "Tel: 3192319827";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Calle 34B 66 A 02";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Bego Inversiones S.A.S";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "NIT 901.226.468-2";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Resolución DIAN # 18762011519624 del 2018/12/01 desde el 0000001 hasta el 7499962 Regimen Común";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Medellín, Colombia";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Factura de Venta # 0000001 01/12/2018 09:32:05 AM";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            text = "Cant.   Producto  Subtotal";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

            //foreach (var elemFact in DatFac)
            //{
            //    text = elemFact. ;
            //    e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            //    y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
            //}

        }
    }
}
