# PDFWriter
This .NET C# MVC Project takes HTML input and prints it, using the PDFSharp Library, on a pre-existing PDF using printing coordinates and styling saved in a JSON file. This version prints a lunar cat ownership agreement and e-mails or downloads it. 

## Motivation
The original intent was to provide for filling out extensive digital contracts online, printing the data on a preexisting contracts in PDF format, and e-mailing them to the legal department to be countersigned. The company wanted a solution that translated responsive web styling into a reliable paper formatting. All of the solutions were either overly complicated or required another licensing fee for an API so I wanted to build something that could be reused without too much setup or overhead.  This is a basic sample of the deployed solution.

## Getting Started

After cloning the repository, open the sln and execute. Fill out the form and choose "Submit and Download". You will recieve a copy of the contract and a certificate of registration.
To enable emailing, proper email credentials and account information need to be set for the source account in EmailAgreement().

### To create a new project
- Run the program and navigate to "Create Grid" to upload a pdf and download it with coordinates printed on it.
- Coordinates of each intended printing location on the page needs to be saved as JSON as shown below. 
- Update the pdf and json file location specified in CatContract().
- Make a view with inputs and a viewmodel with properties that match the names of the inputs specified in the JSON file and use them to replace CatContract.cshtml and CatContractViewModel
### JSON Sample
```
{
	"page1": {
		"inputs": [{
				"field": "OwnerName",
				"left": 120,
				"top": 285,
				"fontfamily": "Console",
				"fontsize": 14,
				"fontstyle": "Italic"

			},
			{
				"field": "CatName",
				"left": 460,
				"top": 320
			}
		]
	},
	"page2": {"inputs": [{}]
	}
}
```
Optional: “fontfamily”, “fontsize”, “fontstyle”, bottom, left2
Required: page#,inputs,field,left,top

The solution currently iterates through a viewmodel of properties passed from the form on Catcontract.cshtml and searches for a corresponding coordinate set in the JSON by the “field” key that matches each property and prints the viewmodel’s value with the coordinate set’s styling. An additional method is included that uses an array of identically named elements instead that are mapped to JSON coordinate set that is only paired based on a corresponding index.

### Samples
[Form Screenshot](FormScreenShot.jpg)

[Completed Form](PrintedCatContract.pdf)

### Prerequisite
C# 7.0 or greater

Visual Studio 2017



## Built With

[PdfSharp 1.50.4845-RC2a](http://www.pdfsharp.net)

Visual Studio 2017

## In Development
Add API functionality

## Author

* **Michael Strunk** - [Strunktech](https://github.com/mycomycul)




