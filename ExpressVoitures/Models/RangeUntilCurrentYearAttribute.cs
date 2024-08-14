﻿using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class RangeUntilCurrentYearAttribute : RangeAttribute
	{
		public RangeUntilCurrentYearAttribute(int minimum) : base(minimum, DateTime.Now.Year)
		{
		}
	}
}
