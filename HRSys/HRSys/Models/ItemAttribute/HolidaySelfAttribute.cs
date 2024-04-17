using System;
using System.ComponentModel.DataAnnotations;

namespace HRSys.Models.ItemAttribute
{	
	public class HolidaySelfAttribute
	{
		[Required(ErrorMessage = "此欄位為必填")]
		[DataType(DataType.Date)]
		[Display(Name = "員工休假日")]
		public DateTime SDate { get; set; }

		[Required(ErrorMessage = "此欄位為必填")]
		[Display(Name = "員工編號")]
		public string UsrId { get; set; }
	}
}