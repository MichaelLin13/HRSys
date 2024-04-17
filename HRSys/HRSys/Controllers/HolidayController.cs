using HRSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRSys.Models.ItemAttribute;

namespace HRSys.Controllers
{
    public class HolidayController : Controller
    {
		HRSysContext dbo = new HRSysContext();
		// GET: Holiday/Index
		public ActionResult Index(string usrid)
        {
			var chk_usrform = dbo.USRFORM.Where(f => f.UsrId == usrid).FirstOrDefault();
			if (chk_usrform == null)
			{
				TempData["ErrorMessage"] = "此帳號不存在";
				return RedirectToAction("Index","USRFORM");
			}
			ViewBag.usrid = usrid;
			// 儲存每個月的週六和週日日期
			Dictionary<int, List<DateTime>> weekendDatesByMonth = new Dictionary<int, List<DateTime>>();
			DateTime newdateTime = new DateTime(2024, 7, 1);

			var _holiday_p = dbo.HolidayPublic.ToList();

			for (int month = 1; month <= 12; month++)
			{
				var _holiday_list = _holiday_p.Where(f => f.PDate.Month == (month)).Select(x => x.PDate).ToList();
				weekendDatesByMonth.Add(month, _holiday_list);
			}
			var _holidaySelf = dbo.HolidaySelf.Where(f => f.UsrId == usrid).Select(x => x.SDate).ToList();
			bool spBigThan = _holidaySelf.Count() > 0;
			ViewBag.WeekendDatesByMonth = spBigThan ? sortOut_Date_OrderByMonth(weekendDatesByMonth, _holidaySelf) : weekendDatesByMonth;
			return View();
		}

		// GET: Holiday/Create
		public ActionResult Create(string usrid)
		{
			var chk_usrform = dbo.USRFORM.Where(f => f.UsrId == usrid).FirstOrDefault();
			if (chk_usrform == null)
			{
				TempData["ErrorMessage"] = "此帳號不存在";
				return RedirectToAction("Index", "USRFORM");
			}
			ViewBag.usrid = chk_usrform.UsrId;
			return View();
		}

		// POST: Holiday/Create
		[HttpPost]
		public ActionResult Create(HolidaySelf _holidayself)
		{			
			if (_holidayself.SDate == null || _holidayself.SDate<=DateTime.Now)
			{
				TempData["ErrorMessage"] = "日期不能小於今天";
				return RedirectToAction("Index", "Holiday", new { usrid = _holidayself.UsrId });
			}
			else
			{
				HolidaySelf new_holiday_self =new HolidaySelf();
				new_holiday_self = _holidayself;
				dbo.HolidaySelf.Add(new_holiday_self);
				dbo.SaveChanges();
				TempData["ErrorMessage"] = "請假成功";
				return RedirectToAction("Index", "Holiday", new { usrid= new_holiday_self.UsrId});
			}			
		}

		// GET: Holiday/Delete_List
		public ActionResult Delete_List(string usrid)
		{
			var chk_usrform = dbo.USRFORM.Where(f => f.UsrId == usrid).FirstOrDefault();
			if (chk_usrform == null)
			{
				TempData["ErrorMessage"] = "此帳號不存在";
				return RedirectToAction("Index", "USRFORM");
			}
			ViewBag.usrid = chk_usrform.UsrId;
			var _holiday_self = dbo.HolidaySelf.Where(f => f.UsrId == usrid).ToList();
			return View(_holiday_self);
		}

		// GET: Holiday/Delete
		public ActionResult Delete(int? holiday_selfid)
		{						
			var _holiday_self = dbo.HolidaySelf.Where(f => f.HolidaySelfId == holiday_selfid).FirstOrDefault();
			string usrid = _holiday_self.UsrId;
			dbo.HolidaySelf.Remove(_holiday_self);
			dbo.SaveChanges();
			return RedirectToAction("Index", "Holiday", new { usrid = usrid });
		}

		// GET: Holiday/Save_Year
		public ActionResult Save_Year()
		{			
			Dictionary<int, List<DateTime>> weekendDatesByMonth = new Dictionary<int, List<DateTime>>();
						
			for (int month = 1; month <= 12; month++)
			{				
				List<DateTime> weekendDates = new List<DateTime>();
				
				DateTime firstDayOfMonth = new DateTime(2024, month, 1);
				DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
				
				for (DateTime currentDay = firstDayOfMonth; currentDay <= lastDayOfMonth; currentDay = currentDay.AddDays(1))
				{
					if (currentDay.DayOfWeek == DayOfWeek.Saturday || currentDay.DayOfWeek == DayOfWeek.Sunday)
					{
						weekendDates.Add(currentDay);
					}
				}
				weekendDatesByMonth.Add(month, weekendDates);
			}	
			
			ViewBag.WeekendDatesByMonth = weekendDatesByMonth;
			save_holiday(weekendDatesByMonth);
			return View();
		}
				
		void save_holiday(Dictionary<int, List<DateTime>> weekendDatesByMonth)
		{
			var _holidays = dbo.HolidayPublic.ToList();
			foreach (var item in weekendDatesByMonth)
			{
				List<HolidayPublic> _holiday_list = new List<HolidayPublic>();
				for (int i = 0; i < item.Value.Count(); i++)
				{
					var _holiday_public = new HolidayPublic();
					_holiday_public.PDate = item.Value[i];
					var _holiday_chk = _holidays.Where(f => f.PDate == _holiday_public.PDate).Count();
					if (_holiday_chk == 0)
					{
						_holiday_list.Add(_holiday_public);
					}
				}
				var _holidayPublic = new List<HolidayPublic>();
				if (_holiday_list.Count() > 0)
				{
					foreach (var _holiday in _holiday_list) 
					{
						dbo.HolidayPublic.Add(_holiday);
						dbo.SaveChanges();
					}
				}
			}
		}

		Dictionary<int, List<DateTime>> sortOut_Date_OrderByMonth(Dictionary<int, List<DateTime>> weekendDatesByMonth, List<DateTime> holidayself)
		{
			for (int i = 0; i < holidayself.Count(); i++)
			{
				weekendDatesByMonth[(holidayself[i].Month)].Add(holidayself[i]);
			}
			return weekendDatesByMonth;

		}
	}
}