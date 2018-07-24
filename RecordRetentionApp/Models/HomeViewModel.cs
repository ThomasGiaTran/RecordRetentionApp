using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RecordRetentionApp.Models
{
    public class HomeViewModel
    {
        public List<retention_schedule> retention_schedule_List { get; set; }

        public retention_schedule ri { get; set; }

        public List<Office_Of_Record> Office_Of_Records { get; set; }

        //public List<retention_period_Month> retentionDropdown { get; set; }

        //public string selectedDepartment { get; set; }

        //public string filenumber { get; set; }

        //public string folderName { get; set; }

        public List<retention_schedule> folderNameDropdown { get; set; }

        //public List<string> retentionPeriodDisplay { get; set; }
    }

    public class Office_Of_Record
    {
        public string Office_Of_Record_Name { get; set; }
    }

    
}