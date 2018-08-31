using Newtonsoft.Json.Linq;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace GoogleDrive.Controllers
{

    //Need somewehere to store locations to print at, currently in JSON
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            createGrid("c:/temp/Agreement.pdf", @"c:\temp\SignedAgreementGrid.pdf");
            return View("Index");
        }
        [HttpPost]
        public ActionResult Index(List<string> inputs)
        {
            CreateAgreement(inputs);
            return View("Index");
        }

        public ActionResult MapPDF()
        {
            return View("");
        }

        /*Method will pull the first page off a document, print data from input boxes to corresponding coordinates and save it to a new location*/
        public void CreateAgreement(List<string> inputs)
        {
            //Where to save the completed document
            string targetPath = @"c:\temp\SignedAgreement.pdf";
            //Where to get the PDF to print on
            string sourcePath = @"C:/temp/Agreement.pdf";
            //Source location of JSON printing coordinates
            string JSONPath = @"C:\Users\Michael\Desktop\Programming\Projects\PDFWriter\PDFWriter\PDFWriter\App_Data\SeattleData.json";

            PdfDocument PDFDoc = PdfReader.Open(sourcePath, PdfDocumentOpenMode.Import);
            PdfDocument document = new PdfDocument();
            document.Info.Author = "Author";
            document.Info.Keywords = "Keywords";
            document.Info.Title = "Document Title";

            PdfPage page = document.AddPage(PDFDoc.Pages[0]);
            page.Size = PageSize.A4;
            /*Create a JSON file that holds the input value locations to be printed on the PDF and update file paths.  See sample in AppData
             *Input locations need to match the order of the input boxes on the submitted form*/
            JObject jsonFields = JObject.Parse(System.IO.File.ReadAllText(JSONPath)) as JObject;
            dynamic pageFields = jsonFields["inputs"];

            /*Loop through all elements and find the match coordinates for where to print*/
            for (int i = 0; i < inputs.Count; i++)
            {
                dynamic f = pageFields[i];
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    XFont font = new XFont("Times New Roman", 20, XFontStyle.BoldItalic);
                    XPoint xTL = new XPoint(Convert.ToDouble(f["left"]), Convert.ToDouble(f["top"]));
                    XPoint xBR = new XPoint(Convert.ToDouble(f["left"]), Convert.ToDouble(f["top"]));
                    gfx.DrawString(inputs[i], font, XBrushes.Black, new XRect(xTL, xBR));
                }
            }
            
            document.Save(targetPath);
            Process.Start(targetPath);

        }

        /*Use createGrid to print the target document with a grid of numbers so that input box locations can be determined
         * Change the locations in the function call to change you filepaths and uncomment on of the loop section*/
        public void createGrid(string source, string target)
        {
            PdfDocument PDFDoc = PdfReader.Open(source, PdfDocumentOpenMode.Import);
            PdfDocument document = new PdfDocument();
            document.Info.Author = "Author";
            document.Info.Keywords = "Enrollment";
            document.Info.Title = "Document Title";

            PdfPage page = document.AddPage(PDFDoc.Pages[0]);
            page.Size = PageSize.A4;

            /*Create one vertical line of coordinates. Change left to move line horizontally. Coordinates in (left,top) format*/
            //double left = 260;
            //for (int top = 0; top < 36; top++)
            //{
            //    using (XGraphics gfx = XGraphics.FromPdfPage(page))
            //    {
            //        //XGraphics gfx = XGraphics.FromPdfPage(page);
            //        XFont font = new XFont("Times New Roman", 6, XFontStyle.BoldItalic);
            //        XPoint xTL = new XPoint(left, top * 25);
            //        XPoint xBR = new XPoint(left, top * 25);
            //        gfx.DrawString((left).ToString() + "," + (top * 25).ToString(), font, XBrushes.Black, new XRect(xTL, xBR));
            //    }
            //}

            /*Create one horizontal line of coordinates. Change top to move line vertically. Coordinates in (left,top) format*/
            //double top = 450;
            //for (int left = 0; left < 25; left++)
            //{
            //    using (XGraphics gfx = XGraphics.FromPdfPage(page))
            //    {
            //        //XGraphics gfx = XGraphics.FromPdfPage(page);
            //        XFont font = new XFont("Times New Roman", 6, XFontStyle.BoldItalic);
            //        XPoint xTL = new XPoint(left *25, top);
            //        XPoint xBR = new XPoint(left *25, top);
            //        gfx.DrawString((left * 25).ToString() + "," + (top).ToString(), font, XBrushes.Black, new XRect(xTL, xBR));
            //    }
            //}

            //Create entire page of gridding

            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 36; y++)
                {
                    using (XGraphics gfx = XGraphics.FromPdfPage(page))
                    {
                        //XGraphics gfx = XGraphics.FromPdfPage(page);
                        XFont font = new XFont("Times New Roman", 6, XFontStyle.BoldItalic);
                        XPoint xTL = new XPoint(x * 25, y * 25);
                        XPoint xBR = new XPoint(x * 25, y * 25);
                        gfx.DrawString((x * 25).ToString() + "," + (y * 25).ToString(), font, XBrushes.Black, new XRect(xTL, xBR));
                    }
                }
            }

            //Save Location for the Grid File
            string filename = target;
            document.Save(filename);
            Process.Start(filename);
        }
    }
}