using HRSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRSys.Models.ItemAttribute;

namespace HRSys.Controllers
{
    public class USRFORMController : Controller
    {
		HRSysContext dbo =new HRSysContext();
		// GET: USRFORM/USRFORM
		public ActionResult Index()
        {
            var _usrform = dbo.USRFORM.ToList();
            return View(_usrform);
        }       

        // GET: USRFORM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USRFORM/Create
        [HttpPost]
        public ActionResult Create(USRFORM _usrform)
        {
            try
            {
                var chk_usrform = dbo.USRFORM.Where(f => f.UsrId == _usrform.UsrId).FirstOrDefault();
                if (chk_usrform != null) 
                {
                    TempData["ErrorMessage"] = "此帳號已存在";
					return RedirectToAction("Create");
				}                
                else
                {
                    USRFORM new_usrform = new USRFORM();
					new_usrform = _usrform;
                    new_usrform.Employed = true;
                    dbo.USRFORM.Add(new_usrform);
                    dbo.SaveChanges();
					TempData["ErrorMessage"] = "帳號新增成功";
					return RedirectToAction("Index");
				}
            }
            catch
            {
                return View();
            }
        }

        // GET: USRFORM/Edit/5
        public ActionResult Edit(string usrid)
        {
            return View();
        }

        // POST: USRFORM/Edit/5
        [HttpPost]
        public ActionResult Edit(USRFORM _usrform)
        {
			var chk_usrform = dbo.USRFORM.Where(f => f.UsrId == _usrform.UsrId).FirstOrDefault();
			try
            {
                if (chk_usrform == null)
                {
					TempData["ErrorMessage"] = "此帳號不存在";
					return RedirectToAction("Index");
                }
                var edit_usrform = dbo.USRFORM.Where(f => f.UsrId == _usrform.UsrId).FirstOrDefault();
				edit_usrform.UsrPSW = _usrform.UsrPSW;
				edit_usrform.Employed= _usrform.Employed;
                dbo.SaveChanges();
				TempData["ErrorMessage"] = "修改成功";
				return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: USRFORM/Delete/5
        public ActionResult Delete(string usrid)
        {
			var chk_usrform = dbo.USRFORM.Where(f => f.UsrId == usrid).FirstOrDefault();
			if (chk_usrform == null)
			{
				TempData["ErrorMessage"] = "此帳號不存在";
				return RedirectToAction("Index");
			}
			else
			{
                var del_usrform = dbo.USRFORM.Where(f => f.UsrId == chk_usrform.UsrId).FirstOrDefault();
				dbo.USRFORM.Remove(del_usrform);
				dbo.SaveChanges();
				TempData["ErrorMessage"] = "帳號已刪除";
				return RedirectToAction("Index");
			}			
        }
    }
}
