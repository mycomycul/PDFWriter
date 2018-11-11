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

        //[Required]
        //[Display(Name = "Full Name")]
        //public string OwnerName2 { get; set; }

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
            string _month { get; set; }
        public string Month
        {
            get { return this._month; }

            set
            {
                if (int.TryParse(value, out int a))
                {
                    _month = value;

                    switch (value[value.Length - 1].ToString())
                    {
                        case "1":
                            _month += "st";
                            break;
                        case "2":
                            _month += "nd";
                            break;
                        case "3":
                            _month += "rd";
                            break;
                        default:
                            _month += "th";
                            break;
                    }
                }
            }
        }
        [Required]

        public int Year { get; set; }

        //[Required]
        //[Display(Name = "Full Name")]
        //public string OwnerName3 { get; set; }

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

        //[Required]
        //[Display(Name = "Cat's Name")]
        //public string CatName2 { get; set; }

        [Required(ErrorMessage = "You must provide a weight in terrestrial pounds")]
        [RegularExpression(@"^[0-9]+$")]
        float _weight { get; set; }
        public float Weight {
            get { return this._weight; }

            set { _weight = value * .165F; }
        }

        [Required]
        [Display(Name = "Color")]
        public string Color { get; set; }

        public bool Fixed { get; set; }

        public Gender Gender { get; set; }

        [Required(ErrorMessage = "All lunar cats must be chipped")]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Chip number only includes numbers")]
        [Range(111111111, 999999999, ErrorMessage = "Chip number must have 9 digits")]
        public int ChipNumber { get; set; }

        //[Display(Name = "Veterination Name")]
        //public string VetName { get; set; }
        //[Display(Name = "Vet Address")]
        //public string VetAddress { get; set; }
        //[Display(Name = "Vet Phone")]
        //[DataType(DataType.PhoneNumber)]
        //public string VetPhone { get; set; }
        [Required]
        [Display(Name = "Digital Signature")]
        public string OwnerSignature { get; set; }

        static Random r = new Random();
        string _registrationNumber = r.Next(111111, 999999).ToString();
        public string RegistrationNumber {
            get
            {
                return _registrationNumber;
            }
            set
            {             
            }
        }

        public string DirectorSignature { get; } = "Michael Strunk";


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