using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecordRetentionApp.Models;
using RecordRetentionApp.Managers;

namespace RecordRetentionApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private RecordRetentionContext db;
        private AccountManager amgr;



        public HomeController()
        {
            amgr = new AccountManager();
            db = new RecordRetentionContext();
        }

        public ActionResult Index()
        {
            HomeViewModel Hm = new HomeViewModel();
            Hm.retention_schedule_List = db.retention_schedule.ToList();

            Hm.Office_Of_Records = amgr.getOffice_Of_Record();

            //Hm.folderNameDropdown = amgr.getFolderNames();

            TempData["name"] = amgr.getUserNames(User.Identity.Name);
            //string temp = temp.

            //Hm.retentionDropdown = new List<int> { 1, 2, 3, 4, 5, 6, 7, 500 };
            //var Office_Of_Record = amgr.getOffice_Of_Record().Select(rr =>
            //new SelectListItem { Value = , Text = Hm.ri.department }).ToList();
            //Hm.Office_Of_Record = Office_Of_Record;


            //foreach (var record in db.retention_schedule.ToList())
            //{
            //    amgr.updateRetentionPeriodDisplay(record.retention_period_Month);
            //}

            return View(Hm);
        }

        [Authorize(Roles = "ADMIN, RecordRetentionApp(RW)")]
        [HttpPost]
        public ActionResult Index(HomeViewModel HomeViewModels)
        {
            if (ModelState.IsValid)
            {
                int recordCount = db.retention_schedule.Where(x => x.File_Number == HomeViewModels.ri.File_Number).Count();
                if (recordCount == 0)
                {


                    retention_schedule newRecord = new retention_schedule();

                    newRecord.Office_Of_Record = HomeViewModels.ri.Office_Of_Record;
                    newRecord.File_Number = HomeViewModels.ri.File_Number;
                    newRecord.retention_period_Month = HomeViewModels.ri.retention_period_Month;
                    newRecord.Folder_Name = HomeViewModels.ri.Folder_Name;
                    newRecord.Record_Description = HomeViewModels.ri.Record_Description;
                    newRecord.Retention_Description = HomeViewModels.ri.Retention_Description;
                    newRecord.Laserfiche = HomeViewModels.ri.Laserfiche;
                    //newRecord.record_status = "Created";
                    //newRecord.create_date = DateTime.Today;
                    //newRecord.username = amgr.getUserNames(User.Identity.Name);
                    //newRecord.retentionPeriodDisplay = amgr.updateRetentionPeriodDisplay(HomeViewModels.ri.retention_period_Month);

                    db.retention_schedule.Add(newRecord);
                    db.SaveChanges();

                    ModelState.Clear();

                    ViewBag.SuccessMessage = "Record Added";
                }
                else
                    ModelState.AddModelError(string.Empty, "File Number already exists");
            }

            HomeViewModel hm = new HomeViewModel();
            hm.retention_schedule_List = db.retention_schedule.ToList();
            hm.Office_Of_Records = amgr.getOffice_Of_Record();
            //hm.folderNameDropdown = amgr.getFolderNames();

            TempData["name"] = amgr.getUserNames(User.Identity.Name);
            return View(hm);
        }

        [Authorize(Roles = "ADMIN, RecordRetentionApp(RW)")]
        [HttpPost]
        public ActionResult DeleteRecord(int RecordID)
        {
            retention_schedule record = db.retention_schedule.Find(RecordID);
            db.retention_schedule.Remove(record);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public PartialViewResult EditRecord(int RecordID)
        {
            retention_schedule record = db.retention_schedule.Find(RecordID);
            //record.record_status = "Modified";
            //record.modified_date = DateTime.Today;

            HomeViewModel hm = new HomeViewModel();
            hm.ri = record;

            hm.retention_schedule_List = db.retention_schedule.ToList();
            hm.Office_Of_Records = amgr.getOffice_Of_Record();
            //hm.folderNameDropdown = amgr.getFolderNames();
            //hm.retentionDropdown = db.retention_period_Month.ToList();

            return PartialView("_Edit", hm);
        }

        [Authorize(Roles = "ADMIN, RecordRetentionApp(RW)")]
        [HttpPost]
        //public JsonResult EditRecord(HomeViewModel model, int retentionperiod)
        public ActionResult EditRecord(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Boolean fileExisted = true;
                int indexCounter = db.retention_schedule.Where(x => x.File_Number == model.ri.File_Number).Count();
                if (  indexCounter < 2)
                {
                    //if (db.retention_schedule.Where(x => x.File_Number == model.ri.File_Number).Count() == 0)
                    //{
                    int recordID = 0;
                    if(indexCounter != 0) 
                        recordID = db.retention_schedule.First(x => x.File_Number == model.ri.File_Number).RecordID;

                    if (indexCounter == 0 || (indexCounter == 1 && model.ri.RecordID == recordID))
                    {
                        retention_schedule record = db.retention_schedule.Find(model.ri.RecordID);
                        record.File_Number = model.ri.File_Number;
                        record.Office_Of_Record = model.ri.Office_Of_Record;
                        record.Record_Description = model.ri.Record_Description;
                        record.Folder_Name = model.ri.Folder_Name;
                        record.retention_period_Month = model.ri.retention_period_Month;
                        record.Retention_Description = model.ri.Retention_Description;
                        record.Laserfiche = model.ri.Laserfiche;

                        db.SaveChanges();
                        fileExisted = false;
                        return Json(new { Data = model, success = true }, JsonRequestBehavior.AllowGet);                        
                    }
                }


                if (fileExisted)
                {
                    ModelState.AddModelError(model.ri.File_Number, "File Number Existed");
                }
            }

            model.retention_schedule_List = db.retention_schedule.ToList();
            model.Office_Of_Records = amgr.getOffice_Of_Record();
            //model.folderNameDropdown = amgr.getFolderNames();

            return PartialView("_Edit", model);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Create()
        //{
        //    return View();
        //}
    }
}