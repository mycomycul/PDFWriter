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
        public int Day { get; set; }
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

        [Required]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "cat's Name")]
        public string CatName2 { get; set; }

        public float Weight { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string Color { get; set; }

        public bool Fixed { get; set; }

        public Gender Gender { get; set; }

        [Required(ErrorMessage = "All lunar cats must be chipped")]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "Chip number only includes numbers")]
        [Range(111111111, 999999999, ErrorMessage = "Chip must be w number with 9 digits")]
        public int ChipNumber { get; set; }

        [Display(Name = "Veterination Name")]
        public string VetName { get; set; }
        [Display(Name = "Vet Address")]
        public string VetAddress { get; set; }
        [Display(Name = "Vet Phone")]
        [DataType(DataType.PhoneNumber)]
        public string VetPhone { get; set; }
        [Display(Name = "Insert text to demonstrate printing on the second page")]
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