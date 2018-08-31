using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace GoogleDrive.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UpdatePDf("string");
            return View("Index");
        }
        [HttpPost]
        public ActionResult Index(string StudentName)
        {
            //UpdatePDf(StudentName);
            return View("Index");
        }

        public void UpdatePDf(string sName)
        {
            PdfDocument PDFDoc = PdfReader.Open(@"C:\temp\Agreement.pdf", PdfDocumentOpenMode.Import);
            PdfDocument document = new PdfDocument();
            document.Info.Author = "Author";
            document.Info.Keywords = "Enrollment";
            document.Info.Title = "Document Title";

            PdfPage page = document.AddPage(PDFDoc.Pages[0]);
            page.Size = PageSize.A4;

                //Add pagesize query and gridsize variable to compute necessary grid count
            for (int x = 0; x < 24; x++)
            {
                for (int y = 0; y < 36; y++)
                {               
                using(XGraphics gfx = XGraphics.FromPdfPage(page)) { 
                //XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Times New Roman", 6, XFontStyle.BoldItalic);
                XPoint xTL = new XPoint(x*25, y * 25);
                XPoint xBR = new XPoint(x*25, y * 25);
                gfx.DrawString((x*25).ToString() + "," + (y * 25).ToString(), font, XBrushes.Black, new XRect(xTL, xBR));
                }
                }
            }
            string filename = @"c:\temp\SignedAgreement.pdf";
            document.Save(filename);
            Process.Start(filename);
        }
    }
}