using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDFWriter.ViewModels
{
    public interface IEAViewModel
    {

    }
    public class SeattleEAViewModel:IEAViewModel
    {

        /*Needs clientside validation.  All types should be string because these replicate written values and should be printed as such or 
        *display formatting will need to be implemented in the print methods eliminate default formatting*/
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string StartDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string Initials1 { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Veteran { get; set; }
        public string Disabilities { get; set; }
        public string Race { get; set; }
        public string LastHighSchool { get; set; }
        public string HighSchoolGraduate { get; set; }
        public string GraduateYear { get; set; }
        public string GED { get; set; }
        public string GEDYear { get; set; }
        public string Initials2 { get; set; }
        public string Initials3 { get; set; }
        public string Initials4 { get; set; }
        public string Initials5 { get; set; }
        public string Initials6 { get; set; }
        public string Initials7 { get; set; }
        public string Initials8 { get; set; }
        public string Initials9 { get; set; }
        public string Initials10 { get; set; }
        public string Initials11 { get; set; }
        public string Initials12 { get; set; }
        public string Initials13 { get; set; }
        public string StudentName13 { get; set; }
        public string Initials14 { get; set; }
        public string Initials15 { get; set; }
        public string StudentPrintedName15 { get; set; }
        public string StudentSignature15 { get; set; }
        public string StudentDate15 { get; set; }
        public string Initials16 { get; set; }
        public string StudentPrintedName17 { get; set; }
        public string StudentSignature17 { get; set; }
        public string StudentDate17 { get; set; }
        public string Initials17 { get; set; }



    }
    public class PortlandEAViewModel:IEAViewModel
    {

        /*Needs clientside validation.  All types should be string because these replicate written values and should be printed as such or 
        *display formatting will need to be implemented in the print methods eliminate default formatting*/
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string StartDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string Initials1 { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Veteran { get; set; }
        public string Disabilities { get; set; }
        public string Race { get; set; }
        public string LastHighSchool { get; set; }
        public string HighSchoolGraduate { get; set; }
        public string GraduateYear { get; set; }
        public string GED { get; set; }
        public string GEDYear { get; set; }
        public string Initials2 { get; set; }
        public string Initials3 { get; set; }
        public string Initials4 { get; set; }
        public string Initials5 { get; set; }
        public string Initials6 { get; set; }
        public string Initials7 { get; set; }
        public string Initials8 { get; set; }
        public string Initials9 { get; set; }
        public string Initials10 { get; set; }
        public string Initials11 { get; set; }
        public string Initials12 { get; set; }
        public string Initials13 { get; set; }
        public string StudentName13 { get; set; }
        public string Initials14 { get; set; }
        public string Initials15 { get; set; }
        public string StudentPrintedName16 { get; set; }
        public string StudentSignature16 { get; set; }
        public string StudentDate16 { get; set; }
        public string Initials16 { get; set; }
        public string StudentPrintedName17 { get; set; }
        public string StudentSignature17 { get; set; }
        public string StudentDate17 { get; set; }
        public string Initials17 { get; set; }
    }
    public class ConcordiaEAViewModel : IEAViewModel
    {

        /*Needs clientside validation.  All types should be string because these replicate written values and should be printed as such or 
        *display formatting will need to be implemented in the print methods eliminate default formatting*/
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string StartDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string Initials1 { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Veteran { get; set; }
        public string Disabilities { get; set; }
        public string Race { get; set; }
        public string LastHighSchool { get; set; }
        public string HighSchoolGraduate { get; set; }
        public string GraduateYear { get; set; }
        public string GED { get; set; }
        public string GEDYear { get; set; }
        public string Initials2 { get; set; }
        public string Initials3 { get; set; }
        public string StudentName4 { get; set; }
        public string Initials4 { get; set; }
        public string Initials5 { get; set; }
        public string StudentPrintedName6 { get; set; }
        public string StudentDate6 { get; set; }
        public string Initials6 { get; set; }
        public string StudentSignature7 { get; set; }
        public string Initials7 { get; set; }
        public string StudentPrintedName8 { get; set; }
        public string StudentSignature8 { get; set; }
        public string StudentDate8 { get; set; }
        public string Initials8 { get; set; }







    }
    public class DenverEAViewModel : IEAViewModel
    {

        /*Needs clientside validation.  All types should be string because these replicate written values and should be printed as such or 
        *display formatting will need to be implemented in the print methods eliminate default formatting*/
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string StartDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string Initials1 { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string Veteran { get; set; }
        public string Disabilities { get; set; }
        public string Race { get; set; }
        public string LastHighSchool { get; set; }
        public string HighSchoolGraduate { get; set; }
        public string GraduateYear { get; set; }
        public string GED { get; set; }
        public string GEDYear { get; set; }
        public string Initials2 { get; set; }
        public string Initials3 { get; set; }
        public string Initials4 { get; set; }
        public string Initials5 { get; set; }
        public string Initials6 { get; set; }
        public string Initials7 { get; set; }
        public string Initials8 { get; set; }
        public string Initials9 { get; set; }
        public string Initials10 { get; set; }
        public string Initials11 { get; set; }
        public string Initials12 { get; set; }
        public string StudentName12 { get; set; }
        public string Initials13 { get; set; }
        public string Initials14 { get; set; }
        public string StudentPrintedName15 { get; set; }
        public string StudentSignature15 { get; set; }
        public string StudentDate15 { get; set; }
        public string Initials15 { get; set; }
        public string StudentPrintedName16 { get; set; }
        public string StudentSignature16 { get; set; }
        public string StudentDate16 { get; set; }
        public string Initials16 { get; set; }



    }
}