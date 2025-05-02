namespace FitnessPortalAPI.Utils;

public static class DateRangeHelper
{
	public static (DateTime startDate, DateTime endDate) CalculateDateRange(TrainingPeriod period)
	{
		DateTime endDate = DateTime.Now;
		DateTime startDate;

		switch (period)
		{
			case TrainingPeriod.Month:
				startDate = endDate.AddMonths(-1);
				break;
			case TrainingPeriod.Quarter:
				startDate = endDate.AddMonths(-3);
				break;
			case TrainingPeriod.HalfYear:
				startDate = endDate.AddMonths(-6);
				break;
			default:
				throw new BadRequestException("Invalid period value. Supported values are 'month', 'quarter', and 'halfyear'");
		}

		startDate = startDate.Date;
		endDate = endDate.Date;

		return (startDate, endDate);
	}
}
