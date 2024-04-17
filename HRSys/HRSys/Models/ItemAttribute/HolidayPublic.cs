using System;
using System.ComponentModel.DataAnnotations;

namespace HRSys.Models.ItemAttribute
{
	public class HolidayPublicAttribute
	{
		[Key]
		[DataType(DataType.Date)]
		[Display(Name = "法定休假日")]
		public DateTime PDate;
	}
}
