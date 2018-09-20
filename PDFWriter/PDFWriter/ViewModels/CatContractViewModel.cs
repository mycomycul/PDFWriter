using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDFWriter.ViewModels
{
    public class CatContractViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string OwnerName { get; set; }
        [Required]
        [Display(Name = "Cat's Name")]
        public string CatName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string OwnerName2 { get; set; }

        [Required]
        string _day;
        public string Day
        {
                get { return this._day; }

                set {
                    if (int.TryParse(value, out int a))
                    {
                        _day = value;

                        switch (value[value.Length - 1].ToString())
                        {
                            case "1":
                                _day += "st";
                                break;
                            case "2":
                                _day += "nd";
                                break;
                            case "3":
                                _day += "rd";
                                break;
                            default:
                                _day += "th";
                                break;
                        }
                    }
                }
            }
            [Required]
            public int Month { get; set; }
        [Required]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string OwnerName3 { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        public LunarState State { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "You must enter a valid email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Cat's Name")]
        public string CatName2 { get; set; }

        [Required(ErrorMessage = "You must provide a weight in terrestrial pounds")]
        [RegularExpression(@"^[0-9]+$")]
        public float Weight { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string Color { get; set; }

        public bool Fixed { get; set; }

        public Gender Gender { get; set; }

        [Required(ErrorMessage = "All lunar cats must be chipped")]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Chip number only includes numbers")]
        [Range(111111111, 999999999, ErrorMessage = "Chip number must have 9 digits")]
        public int ChipNumber { get; set; }

        [Display(Name = "Veterination Name")]
        public string VetName { get; set; }
        [Display(Name = "Vet Address")]
        public string VetAddress { get; set; }
        [Display(Name = "Vet Phone")]
        [DataType(DataType.PhoneNumber)]
        public string VetPhone { get; set; }
        [Required]
        [Display(Name = "Digital Signature")]
        public string OwnerSignature { get; set; }

        [Display(Name = "Second Page Notes")]
        public string SecondPageData { get; set; }
    }
    public enum LunarState
    {
        Crateriton,
        MoonRock,
        Farside,
        Lunida,
        Terran,
        Crescent,
        Eclipse,
        Solaris,
        Nebulon,
        Kepler,
        Daedalus,
        Nectaris,
        Imbrium,
        Gibbous,
        Sinus,
        Breccia,
        MilkyWay
    }
    public enum Gender
    {
        Female,
        Male
    }
}