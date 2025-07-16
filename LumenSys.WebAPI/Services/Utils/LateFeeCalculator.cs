namespace LumenSys.WebAPI.Services.Utils
{
    public static class LateFeeCalculator
    {
        public static double CalculateLateFee(DateTime dueDate, DateTime today, double installmentValue, double monthlyRatePercent)
        {
            if (today <= dueDate) return 0;

            int monthsLate = ((today.Year - dueDate.Year) * 12) + (today.Month - dueDate.Month);
            if (today.Day < dueDate.Day)
                monthsLate--;

            monthsLate = Math.Max(monthsLate, 1); 

            double rateDecimal = monthlyRatePercent / 100.0;
            return monthsLate * installmentValue * rateDecimal;
        }
    }
}
