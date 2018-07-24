using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecordRetentionApp.Models
{
    public class retention_schedule
    {
        [Key]
        public int RecordID { get; set; }

        [Required(ErrorMessage = "File Number is required.")]
        [StringLength(200)]
        [DisplayName("File Number")]
        public string File_Number { get; set; }

        [Required(ErrorMessage = "Office Of Record is required.")]
        [StringLength(250)]
        [DisplayName("Office Of Record")]
        public string Office_Of_Record { get; set; }

        [StringLength(255)]
        [DisplayName("Record Description")]
        public string Record_Description { get; set; }

        [Required(ErrorMessage = "Folder Name is required.")]
        [StringLength(250)]
        [DisplayName("Folder Name")]
        public string Folder_Name { get; set; }

        //[Required(ErrorMessage = "Retention Period is required.")]
        [Required(ErrorMessage = "Retention Period (Month) is required.")]
        [RegularExpression(@"^\d+$")]
        [DisplayName("Retention Period (Month)")]
        public int retention_period_Month { get; set; }

        //public string record_status { get; set; }

        //public DateTime create_date { get; set; }

        //public DateTime? modified_date { get; set; }

        //public string username { get; set; }

        [DisplayName("Retention Description")]
        public string Retention_Description { get; set; }

        [DisplayName("Is Laserfiche ready?  ")]
        public Boolean Laserfiche { get; set; }
    }
}