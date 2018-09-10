using Newtonsoft.Json.Linq;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PDFWriter.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace GoogleDrive.Controllers
{

    //Need somewehere to store locations to print at, currently in JSON
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult CatContract()
        {
            return View();
        }
        //POST: Cat Contract Data and save
        [HttpPost]
        public ActionResult CatContract(CatContractViewModel vm)
        {
            
            if (ModelState.IsValid) { 

            //Where to save the completed document
            string savePath = @"C:/temp/";
            //Where to get the PDF to print on
            string sourcePath = @"C:/temp/Cat Contract.pdf";
            //JSON coordinates of where to print on the new PDF
            string JSONPath = @"C:/temp/Cat Contract Coordinates.json";

            //Create the document and save it to the specified location
            PdfDocument newCatContract = PrintViewModel(vm, savePath, sourcePath, JSONPath);

            //Email the document to registration
            //EmailMethod()
            MemoryStream stream = new MemoryStream();
            newCatContract.Save(stream, false);
            return File(stream, "application/pdf","CatContract");
            }
            else
            {
                string validationErrors = string.Join(",",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());

                return View(vm);
            }
        }

        public PdfDocument PrintViewModel(dynamic vm, string savePath, string sourcePath, string JSONPath)
        {
            /* Load PDF original into a new PDFDocument so that it can be printed to*/

            PdfDocument originalPDF = PdfReader.Open(sourcePath, PdfDocumentOpenMode.Import);
            PdfDocument newPDF = new PdfDocument();
            newPDF.Info.Author = "Author";
            newPDF.Info.Keywords = "Enrollment";
            newPDF.Info.Title = "Document Title";
            for (int p = 0; p < originalPDF.PageCount; p++)
            {
                PdfPage page = newPDF.AddPage(originalPDF.Pages[p]);
                page.Size = PageSize.A4;
            }
            //Load vm properties so that they can be looped through
            Type type = vm.GetType();
            PropertyInfo[] VMProperties = type.GetProperties();

            //Load JSON Data for printing values and create dynamic variable to hold values
            JObject jsonPageFields = JObject.Parse(System.IO.File.ReadAllText(JSONPath)) as JObject;
            dynamic pages = jsonPageFields.Values();

            /*Loop through JSON Data and look for key values with corresponding Class properties and if
             * found, get the values from the ViewModel and JSON data send to print method*/

            foreach (PropertyInfo prop in VMProperties)
            {
                foreach (JToken page in pages)
                {
                    var pageNumber = Convert.ToInt16(page.Path.Remove(0, 4));
                    foreach (JObject inputs in page["inputs"])
                    {
                        if (inputs.Value<string>("field") == prop.Name && prop.GetValue(vm) != null)
                        {
                            double top = inputs.Value<double>("top");
                            double left = inputs.Value<double>("left");
                            string fontStyle = inputs.Value<string>("fontstyle") != null ? (string)inputs.Value<string>("fontstyle") : "Regular";//Set Default fault in case none specified
                            string fontFamily = inputs.Value<string>("font") != null ? (string)inputs.Value<string>("font") : "Arial";//Set Default fault in case none specified
                            double fontSize = inputs.Value<string>("fontsize") != null ? Convert.ToDouble(inputs.Value<string>("fontsize")) : 14;//Set Default fault in case none specified
                            string propValue = prop.GetValue(vm).ToString();
                            PrintElement(top, left, fontFamily, pageNumber, propValue, fontSize, fontStyle);
                        }
                    }
                }

            }
            //Build savePath with applicant's name
            string name = vm.OwnerName != null ? vm.OwnerName.Replace(" ", "") : "Unknown";
            savePath += "CatContract-" + name + DateTime.Today.ToString("MMddyyyy") + ".pdf";


            //Add try catch in case in case it's currently open
            //Save to Local Directory
            //newPDF.Save(savePath);
            //Process.Start(savePath);

            return newPDF;




            void PrintElement(double top, double left, string fontFamily, int pageNumber, string textToPrint, double fontSize, string fontStyle)
            {
                XGraphics gfx = XGraphics.FromPdfPage(newPDF.Pages[pageNumber - 1]);

                XFont font = new XFont(fontFamily, fontSize, (XFontStyle)Enum.Parse(typeof(XFontStyle),fontStyle));
                XPoint xTL = new XPoint(left, top);
                gfx.DrawString(textToPrint, font, XBrushes.Black, new XRect(xTL, xTL));
                gfx.Dispose();
            }

        }
        //GET CreateGrid
        public ActionResult CreateGrid()
        {
            return View();
        }

        /*Use createGrid to print the target document with a grid of numbers so that input box locations can be determined
         * Grids can be a single vertical or horizontal line or a full page grid
         * Change the locations in the function call to change you filepaths and uncomment the appropriate loop sections*/
         //POST CreateGrid
         [HttpPost]
        public ActionResult createGrid()
        {
            string path = "c:/temp/";
            string file = @"Cat Contract.pdf";

            try
            {
                PdfDocument originalPDF = PdfReader.Open(path + file, PdfDocumentOpenMode.Import);
                PdfDocument newPDF = new PdfDocument();
                newPDF.Info.Author = "Lunar Animal Control";
                newPDF.Info.Keywords = "Cat Contract Grid";
                newPDF.Info.Title = "Cat Contract Grid";
                for (int p = 0; p < originalPDF.PageCount; p++)
                {
                    PdfPage page = newPDF.AddPage(originalPDF.Pages[p]);
                    page.Size = PageSize.A4;
                }

                /*Loop 1: Create one vertical line of coordinates. Change left to move line horizontally. Coordinates in (left,top) format*/
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

                /*Loop 2: Create one horizontal line of coordinates. Change top to move line vertically. Coordinates in (left,top) format*/
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

                //Loop 3: Create entire page of gridding
                for (int p = 0; p < newPDF.PageCount; p++)
                {
                    for (int x = 0; x < 25; x++)
                    {
                        for (int y = 0; y < 36; y++)
                        {
                            using (XGraphics gfx = XGraphics.FromPdfPage(newPDF.Pages[p]))
                            {
                                //XGraphics gfx = XGraphics.FromPdfPage(page);
                                XFont font = new XFont("Times New Roman", 6, XFontStyle.BoldItalic);
                                XPoint xTL = new XPoint(x * 25, y * 25);
                                XPoint xBR = new XPoint(x * 25, y * 25);
                                gfx.DrawString((x * 25).ToString() + "," + (y * 25).ToString(), font, XBrushes.Black, new XRect(xTL, xBR));
                            }
                        }
                    }
                }
                //Save Location for the Grid File
                string filename = path + "Grid -" + file;
                newPDF.Save(filename);
                Process.Start(filename);
                ViewBag.Message = "Grid Created Succesfully";
                return View();
            }
            catch (Exception)
            {

                ViewBag.Message = "Something went wrong, plase try again";
                return View();
            }
        }




        /*Gets data from input boxes on form and corresponding coordinates in JSON and prints them to a new copy of a PDF*/
        //This a previous version using a string array of identically named inputs as values and identically ordered JSON coordinates
        public void Print(params string[] inputs)

        {
            //Where to save the completed document
            string targetPath = @"C:/.pdf";
            //Where to get the PDF to print on
            string sourcePath = @"C:/.pdf";
            //Source location of JSON printing coordinates
            string JSONPath = @"C:/.json";

            PdfDocument originalPDF = PdfReader.Open(sourcePath, PdfDocumentOpenMode.Import);
            PdfDocument newPDF = new PdfDocument();
            newPDF.Info.Author = "Author";
            newPDF.Info.Keywords = "Enrollment";
            newPDF.Info.Title = "Document Title";
            for (int p = 0; p < originalPDF.PageCount; p++)
            {
                PdfPage page = newPDF.AddPage(originalPDF.Pages[p]);
                page.Size = PageSize.A4;
            }

            /*Create a JSON file that holds the input value locations to be printed on the PDF and update file paths.  See sample in AppData
             *Input locations need to match the order of the input boxes on the submitted form*/
            JObject jsonPageFields = JObject.Parse(System.IO.File.ReadAllText(JSONPath)) as JObject;
            dynamic pages = jsonPageFields;

            /*Loop through all elements and find the matching coordinates for where to print*/
            var pageNumber = 1;
            string fontFamily;
            int JSONObjectNumber = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                //Determine which page in the JSON document to grab coordinates from
                var pageString = "page" + pageNumber.ToString();
                dynamic currentPage = pages[pageString];
                if ((currentPage["inputs"].Count) <= (i - JSONObjectNumber))
                {
                    pageNumber++;
                    pageString = "page" + pageNumber.ToString();
                    currentPage = pages[pageString];
                    JSONObjectNumber = i;
                };
                dynamic f = currentPage["inputs"][i - JSONObjectNumber];
                using (XGraphics gfx = XGraphics.FromPdfPage(newPDF.Pages[pageNumber - 1]))
                {
                    if (f.ContainsKey("font"))
                    {
                        fontFamily = f["font"];
                    }
                    else { fontFamily = "Arial"; }
                    XFont font = new XFont(fontFamily, 16, XFontStyle.BoldItalic);
                    XPoint xTL = new XPoint(Convert.ToDouble(f["left"]), Convert.ToDouble(f["top"]));
                    XPoint xBR = new XPoint(Convert.ToDouble(f["left"]), Convert.ToDouble(f["top"]));
                    gfx.DrawString(inputs[i], font, XBrushes.Black, new XRect(xTL, xBR));
                }
            }

            newPDF.Save(targetPath);
            Process.Start(targetPath);

        }



        

    }
}