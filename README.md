# PDFWriter
This .NET NET C# MVC Project project takes HTML input and prints it on a pre-existing PDF using printing coordinates and styling saved in JSON format and the PDFSharp Library. This version prints a lunar cat ownership agreement and e-mails or downloads it. 

## Motivation
The original intent was to provide for filling out extensive digital contracts online, printing the data on a preexisting contracts in PDF format, and e-mailing them to the legal department to be countersigned. We wanted a solution that translated responsive web styling into a reliable paper formatting and also didn't require extensive API licensing fees. This is a basic sample of the deployed solution.

## Getting Started
After cloning the repository, the CatContract() needs to have the locations of the PDF, JSON and local save location updated.
To enable emailing, proper email credentials and account information needs to be set for the source account in EmailAgreement().

### To create a new project
Run the program and navigate to home/creategrid to upload a pdf and download it with coordinates printed on it.  Currently it prints a full grid with 25 point resolution. Single vertical or horizontal lines of coordinates can be created by commenting out the appropriate section in the creategrid method. Coordinates of each intended printing location on the page needs to be saved as JSON at the location specified in CatContract().  

### JSON SAMPLE
```
{
  "page1": {
    "inputs": [
      {
        "field": "OwnerName",
        "left": 120,
        "top": 285,
        "fontfamily": "Console",
        "fontsize":14,
        "fontstyle":"Italic"
        
      },
      {
        "field": "CatName",
        "left": 460,
        "top": 320
      },
      ]},
   “page2”{
      “inputs”:[]}
}
```
Optional: “fontfamily”, “fontsize”, “fontstyle”, bottom, left2
Required: page#,inputs,field,left,top

The solution currently iterates through a viewmodel of properties passed from the form on Catcontract.cshtml and searches for a corresponding coordinate set in the JSON by the “field” key that matches each properties. prints the viewmodel’s value with the coordinate set’s styling. An additional method is included that uses an array of identically named elements instead that are mapped to JSON coordinate set that is only paired based on a corresponding index.



### Prerequisite
C# 7.0 or greater

Visual Studio 2017



## Built With

PdfSharp 1.50.4845-RC2a

Visual Studio 2017


## Author

* **Michael Strunk** - [Strunktech](https://github.com/mycomycul)




