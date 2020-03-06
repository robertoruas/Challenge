using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain
{
	public enum LoanStatus
	{
		Processing = 0,
		Completed = 1
	}

	public enum LoanResult
	{
		Approved = 0,
		Refused = 1
	}

	public enum PoliticsStatus
	{
		Blocked = 0,
		Active = 1
	}
}
