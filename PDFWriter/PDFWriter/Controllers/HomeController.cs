using Newtonsoft.Json.Linq;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PDFWriter.Models;
using PDFWriter.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;

using System.Web;
using System.Web.Mvc;

namespace PDFWriter.Controllers
{

    
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult CatContract()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="submit">For </param>
        /// <param name="vm">ViewModel containing properties that match those specified in Json Coordinates</param>
        /// <returns></returns>
        //POST: Cat Contract Data and save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CatContract(string submit, CatContractViewModel vm)
        {

            if (ModelState.IsValid)
            {
                //Where to save the completed document locally.  Uncomment to enable
                //string savePath = @"C:/temp/";
                string savePath = null;

                //Where to get the PDF to print on
                string sourcePath = @"~/App_data/Cat Contract.pdf";
                //JSON coordinates of where to print on the new PDF
                string JSONPath = @"~/App_Data/Cat Contract Coordinates.json";

                //Name of new document when saved and emailed
                string pdfName = vm.OwnerName.Replace(" ", "") + "-CatContract-" + DateTime.Today.ToString("MMddyyyy") + ".pdf";
                //Create the document and save it to the specified location
                PdfDocument newCatContract = PrintPdfFromViewModel(vm, savePath, sourcePath, JSONPath);

                if (submit == "Submit and Email")
                {
                    EmailAgreement(newCatContract, vm.Email, pdfName);
                    ViewBag.Email = vm.Email;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    newCatContract.Save(stream, false);
                    return File(stream, "application/pdf", pdfName);
                }

                return RedirectToAction("success", new { email = vm.Email });
            }

            return View(vm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm">View model that matches json properties</param>
        /// <param name="savePath">Where to save locally. Uncomment code</param>
        /// <param name="sourcePath"></param>
        /// <param name="JSONPath"></param>
        /// <returns></returns>
        public PdfDocument PrintPdfFromViewModel(dynamic vm, string savePath, string sourcePath, string JSONPath)
        {
            /* Load PDF original into a new PDFDocument so that it can be printed to*/
            PdfDocument originalPDF = PdfReader.Open(Server.MapPath(sourcePath), PdfDocumentOpenMode.Import);
            PdfDocument newPDF = new PdfDocument();
            newPDF.Info.Author = "Lunar Animal Control";
            newPDF.Info.Keywords = "Cat Contract";
            newPDF.Info.Title = "Cat Contract";
            for (int p = 0; p < originalPDF.PageCount; p++)
            {
                PdfPage page = newPDF.AddPage(originalPDF.Pages[p]);
                page.Size = PageSize.A4;
            }
            //Load vm properties so that they can be looped through
            Type type = vm.GetType();
            PropertyInfo[] VMProperties = type.GetProperties();

            //Load JSON Data for printing values and create dynamic variable to hold values
            JObject jsonPageFields = JObject.Parse(System.IO.File.ReadAllText(Server.MapPath(JSONPath))) as JObject;
            dynamic pages = jsonPageFields.Values();

            //Printing Defaults
            var fontFamilyDefault = "Arial";
            var fontSizeDefault = 14;
            var fontStyleDefault = "Regular";//Regular,BoldItalic,Italic,Bold,Underline,Strikeout

            /*Loop through JSON Data and look for key values with corresponding Class properties and if
             * found, get the values from the ViewModel and JSON data send to print method*/

            foreach (PropertyInfo prop in VMProperties)
            {
                foreach (JToken page in pages)
                {
                    var pageNumber = Convert.ToInt16(page.Path.Remove(0, 4));
                    foreach (JObject inputs in page["inputs"])
                    {
                        double top;
                        double left;
                        string textToPrint;
                        if (inputs.Value<string>("field") == prop.Name && prop.GetValue(vm) != null)
                        {
                            //SAMPLE RadioButton printing of possible options on to a pdf with evenly spaced bubbles
                            //Set each input field in HTML to consecutive number.  THe below prints to 45 values, 15 tall and 3 wide.
                            //left2 and left3 represent the left location of each row.  
                            //THis could be switched to left and right and calculate middle locations accoridngly if properly spaced
                            if (prop.Name == "Weeks")
                            {

                                int fieldValue = Convert.ToInt16(prop.GetValue(vm));
                                int elements = 45;
                                int columns = 3;
                                int rows = elements / columns;
                                int rowSpaces = rows - 1;

                                //Check which column the selected radio button is in and adjust the top and left accordingly
                                //First Column
                                if (fieldValue <= rows)
                                {
                                    top = inputs.Value<double>("top") + (inputs.Value<double>("bottom") - inputs.Value<double>("top")) / rowSpaces * fieldValue;
                                    left = inputs.Value<double>("left");
                                }
                                //Second Column
                                else if (fieldValue > rows && fieldValue <= rows * 2)
                                {
                                    top = inputs.Value<double>("top") + (inputs.Value<double>("bottom") - inputs.Value<double>("top")) / rowSpaces * fieldValue;
                                    left = inputs.Value<double>("left2");
                                }
                                //Third Column
                                else
                                {
                                    top = inputs.Value<double>("top") + (inputs.Value<double>("bottom") - inputs.Value<double>("top")) / rowSpaces * fieldValue;
                                    left = inputs.Value<double>("left3");
                                }
                                textToPrint = "X ";
                            }

                            else
                            {
                                top = inputs.Value<double>("top");
                                left = inputs.Value<double>("left");
                                textToPrint = prop.GetValue(vm).ToString();
                            }
                            string fontStyle = inputs.Value<string>("fontstyle") != null ? (string)inputs.Value<string>("fontstyle") : fontStyleDefault;//Set Default fault in case none specified
                            string fontFamily = inputs.Value<string>("fontfamily") != null ? (string)inputs.Value<string>("fontfamily") : fontFamilyDefault;//Set Default fault in case none specified
                            double fontSize = inputs.Value<string>("fontsize") != null ? Convert.ToDouble(inputs.Value<string>("fontsize")) : fontSizeDefault;//Set Default fault in case none specified
                            string propValue = prop.GetValue(vm).ToString();
                            PrintElement(newPDF, pageNumber, top, left, fontFamily, fontSize, fontStyle, textToPrint);
                        }
                    }
                }

            }
            //Build savePath with applicant's name
            if (savePath != null)
            {
                string name = vm.OwnerName != null ? vm.OwnerName.Replace(" ", "") : "Unknown";
                savePath += "CatContract-" + name + DateTime.Today.ToString("MMddyyyy") + ".pdf";
                //Save to Local Directory and open file
                try
                {
                    
                    newPDF.Save(savePath);
                    Process.Start(savePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return newPDF;




            void PrintElement(PdfDocument newPDFdouble, int pageNumber, double top, double left, string fontFamily, double fontSize, string fontStyle, string textToPrint)
            {
                XGraphics gfx = XGraphics.FromPdfPage(newPDF.Pages[pageNumber - 1]);

                XFont font = new XFont(fontFamily, fontSize, (XFontStyle)Enum.Parse(typeof(XFontStyle), fontStyle));

                XPoint xTL = new XPoint(left, top);
                gfx.DrawString(textToPrint, font, XBrushes.Black, new XRect(xTL, xTL));
                gfx.Dispose();
            }

        }

        /// <summary>
        /// Returns view for creating grid
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateGrid()
        {
            return View();
        }

        /// <summary>
        /// Use createGrid to print the target document with a grid of numbers so that input box locations can be determined
        /// Grids can be a single vertical or horizontal line or a full page grid
        /// Change the locations in the function call to change you filepaths and uncomment the appropriate loop sections
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>      
        //POST CreateGrid
        [HttpPost]
        public ActionResult CreateGrid(string f)
        {
            HttpPostedFileBase file = Request.Files["file"];
            var contentType = file.ContentType;
            if (file.ContentType == "application/pdf")
            {
                try
                {
                    Stream document = file.InputStream;
                    PdfDocument originalPDF = PdfReader.Open(document, PdfDocumentOpenMode.Import);
                    PdfDocument newPDF = new PdfDocument();

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
                    MemoryStream stream = new MemoryStream();
                    newPDF.Save(stream, false);
                    return File(stream, "application/pdf", file.FileName + "-Grid");
                }
                catch (Exception)
                {

                    ViewBag.Message = "Something went wrong, plase try again";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "The file you uploaded doesn't seem to be a PDF, please try again.");
                return View();
            }
        }
        /// <summary>
        ///Gets data from input boxes on form and corresponding coordinates in JSON and prints them to a new copy of a PDF*/
        ///This a deprecated version using a string array of identically named inputs as values and identically ordered JSON coordinates
        /// </summary>
        /// <param name="inputs"></param>
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
             **/
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

        /// <summary>
        /// Send a pdf with the currently specified credentials
        /// </summary>
        /// <param name="pdfToEmail">Pdf to send</param>
        /// <param name="emailTarget">What email address to send to</param>
        /// <param name="pdfName">Name of pdf for attaching to email</param>
        public void EmailAgreement(PdfDocument pdfToEmail, string emailTarget, string pdfName)
        {
            if (pdfToEmail.PageCount > 0)
            {

                Credentials c = new Credentials("username@gmail.com", "password", "hostAddress", 587);

                //Prepare Message
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(c.Email),
                    Subject = "New Cat Agreement",
                    Body = "Congratulations on registering your cat.  Included in your copy of the contract and your official Cat Registration"
                };
                message.To.Add(new MailAddress(emailTarget));


                //Add Attachment
                ContentType ct = new ContentType();
                ct.MediaType = MediaTypeNames.Application.Pdf;
                ct.Name = pdfName;
                MemoryStream stream = new MemoryStream();
                pdfToEmail.Save(stream, false);
                message.Attachments.Add(new System.Net.Mail.Attachment(stream, ct));

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        //Set proper credentials
                        UserName = c.Email,
                        Password = c.Password
                    };
                    smtp.Credentials = credential;
                    smtp.Host = c.HostAddress;
                    smtp.Port = c.HostPort;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    try {
                        smtp.Send(message);
                    }
                    catch (SmtpFailedRecipientException ex)
                    {
                        System.Web.HttpContext.Current.Response.Write(ex.Message);
                    }

                }
            }
        }

        public ActionResult success(string email)
        {
            ViewBag.Email = email;
            return View();
        }


        //Get Strunktech
        public ActionResult StrunkTechHome()
        {
            return Redirect("http://www.strunktech.com");
        }



    }
}