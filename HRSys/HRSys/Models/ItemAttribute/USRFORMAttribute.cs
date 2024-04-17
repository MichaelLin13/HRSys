using System;
using System.ComponentModel.DataAnnotations;

namespace HRSys.Models.ItemAttribute
{
	public class USRFORMItemAttributes
	{
		[Key]
		[StringLength(10)]
		[Display(Name = "使用者帳號")]
		public string UsrId;

		[StringLength(10)]
		[RegularExpression(@"^[a-zA-Z0-9_]{5,10}$", ErrorMessage = "允許5-10字，允許字母、數字、_")]
		[Display(Name = "使用者密碼")]
		public string UsrPSW;

		[Display(Name = "在職")]
		public bool Employed;
	}
}