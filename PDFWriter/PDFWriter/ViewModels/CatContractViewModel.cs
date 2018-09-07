using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDFWriter.ViewModels
{
    public class CatContractViewModel
    {
        [Required(ErrorMessage = "Owner Name must be specified")]
        public string OwnerName1 { get; set; }
        public string CatName { get; set; }
        public string Name2 { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string OwnerName2 { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public LunarState State { get; set; }
        public int PhoneNumber { get; set; }
        public int Email { get; set; }
        public string CatName2 { get; set; }
        public float Weight { get; set; }
        public string Color { get; set; }
        public bool Fixed { get; set; }
        public int ChipNumber { get; set; }
        public string VetName { get; set; }
        public string VetAddress { get; set; }
        public string VetPhone { get; set; }
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
}