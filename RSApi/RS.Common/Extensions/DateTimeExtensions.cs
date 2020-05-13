using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The date time extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this object value)
        {
            if (value == null)
                return null;
            if (value is DateTime)
                return (DateTime) value;

            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            return null;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this object value, string format)
        {
            if (value == null)
                return null;
            if (value is DateTime)
                return (DateTime) value;
            if (String.IsNullOrEmpty(format))
                return value.ToDateTime();

            DateTime result;
            if (DateTime.TryParseExact(
                value.ToString(),
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result))
                return result;

            return null;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeZoneId">The time zone identifier.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateTime value, string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(value, timeZone);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value, string format, DateTime defaultValue)
        {
            var result = value.ToDateTime(format);
            return result ?? defaultValue;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value, DateTime defaultValue)
        {
            var result = value.ToDateTime();
            return result ?? defaultValue;
        }

        /// <summary>
        /// To the short date string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTime? value, string defaultValue = "")
        {
            return !value.HasValue ? defaultValue : value.Value.ToShortDateString();
        }

        /// <summary>
        /// To the short time string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string ToShortTimeString(this DateTime? value, string defaultValue = "")
        {
            return !value.HasValue ? defaultValue : value.Value.ToShortTimeString();
        }

        /// <summary>
        /// To the short date time string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToShortDateTimeString(this DateTime value)
        {
            return String.Concat(value.ToShortDateString(), " ", value.ToShortTimeString());
        }

        /// <summary>
        /// To the short date time string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToShortDateTimeString(this DateTime? value)
        {
            return !value.HasValue ? String.Empty : value.Value.ToShortDateTimeString();
        }

        /// <summary>
        /// To the short day string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToShortDayString(this DateTime value)
        {
            return String.Format("{0:ddd}", value);
        }

        /// <summary>
        /// To the short date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToShortDateTime(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
        }

        /// <summary>
        /// To the begin of the day.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToBeginOfTheDay(this DateTime value)
        {
            return value.Date;
        }

        /// <summary>
        /// To the start of week.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToStartOfWeek(this DateTime value)
        {
            return value.AddDays(-(value.DayOfWeek - System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
        }

        /// <summary>
        /// To the current hour.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToCurrentHour(this DateTime value)
        {
            return value.Date.AddHours(DateTime.Now.Hour);
        }

        /// <summary>
        /// To the end of the day.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToEndOfTheDay(this DateTime value)
        {
            return value.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// Adds the weeks.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="numberOfWeeks">The number of weeks.</param>
        /// <returns></returns>
        public static DateTime AddWeeks(this DateTime value, int numberOfWeeks)
        {
            return value.AddDays(numberOfWeeks * 7);
        }

        /// <summary>
        /// Gets the week date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="weekDay">The week day.</param>
        /// <returns></returns>
        public static DateTime GetWeekDate(this DateTime value, DayOfWeek weekDay)
        {
            while (value.DayOfWeek != weekDay)
                value = value.AddDays(-1);

            return value;
        }

        /// <summary>
        /// Gets the week first date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDate(this DateTime value)
        {
            return value.Date.GetWeekDate(DayOfWeek.Sunday);
        }

        /// <summary>
        /// Gets the week last date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetWeekLastDate(this DateTime value)
        {
            return GetWeekFirstDate(value).AddDays(7).AddSeconds(-1);
        }

        /// <summary>
        /// Gets the work week.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IList<DateTime> GetWorkWeek(this DateTime value)
        {
            var workWeek = new List<DateTime>();
            var date = value.GetWeekDate(DayOfWeek.Monday);
            for (var i = 0; i < 5; i++)
            {
                workWeek.Add(date);
                date = date.AddDays(1);
            }

            return workWeek;
        }

        /// <summary>
        /// Gets the work week.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IList<DateTime> GetWorkWeek(this DateTime? value)
        {
            return !value.HasValue ? new List<DateTime>() : value.Value.GetWorkWeek();
        }

        /// <summary>
        /// Gets the month dates.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IList<DateTime> GetMonthDates(this DateTime value)
        {
            var monthDates = new List<DateTime>();
            var date = value.GetMonthFirstDate();
            for (var i = 0; i < value.GetMonthLastDate().Day; i++)
            {
                monthDates.Add(date);
                date = date.AddDays(1);
            }

            return monthDates;
        }

        /// <summary>
        /// Gets the month dates.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IList<DateTime> GetMonthDates(this DateTime? value)
        {
            return !value.HasValue ? new List<DateTime>() : value.Value.GetMonthDates();
        }

        /// <summary>
        /// Gets the month first date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetMonthFirstDate(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        /// <summary>
        /// Gets the month last date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetMonthLastDate(this DateTime value)
        {
            return GetMonthFirstDate(value).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// Gets the week of year.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.Int32.</returns>
        public static int GetWeekOfYear(this DateTime date)
        {
            return DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        /// <summary>
        /// Gets the year months.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isFromJanuary">If set to <c>true</c> then is from january.</param>
        /// <returns></returns>
        public static IList<DateTime> GetYearMonths(this DateTime value, bool isFromJanuary = true)
        {
            var yearMonths = new List<DateTime>();
            var date = isFromJanuary ? value.GetYearFirstDate() : value.GetMonthFirstDate();
            for (var i = 0; i < 12; i++)
            {
                yearMonths.Add(date);
                date = date.AddMonths(1);
            }

            return yearMonths;
        }

        /// <summary>
        /// Gets the year first date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetYearFirstDate(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1);
        }

        /// <summary>
        /// Gets the year last date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetYearLastDate(this DateTime value)
        {
            return GetYearFirstDate(value).AddYears(1).AddSeconds(-1);
        }

        /// <summary>
        /// Gets the age.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns></returns>
        public static int GetAge(this DateTime dateOfBirth)
        {
            var timeSpan = DateTime.Now.Date - dateOfBirth.Date;
            return Math.Truncate(timeSpan.TotalDays / 365.25).ToInt(0);
        }

        /// <summary>
        /// Gets the age.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns></returns>
        public static string GetAge(this DateTime? dateOfBirth)
        {
            return
                !dateOfBirth.HasValue
                    ? String.Empty
                    : GetAge(dateOfBirth.Value).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Determines whether birth date is valid.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns></returns>
        public static bool IsValidBirthDate(this DateTime? dateOfBirth)
        {
            return
                dateOfBirth.HasValue &&
                dateOfBirth.Value != DateTime.MinValue &&
                dateOfBirth.Value < DateTime.Now.Date;
        }

        /// <summary>
        /// Gets the business minutes.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        public static int GetBusinessMinutes(this int days)
        {
            return days * 8 * 60;
        }

        /// <summary>
        /// Sets the date time kind local.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime SetDateTimeKindLocal(this DateTime value)
        {
            return new DateTime(value.ToUniversalTime().Ticks, DateTimeKind.Local);
        }

        /// <summary>
        /// Gets the hours.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public static decimal GetHours(this int minutes)
        {
            return ((decimal)minutes / 60).ToRounded(2).ToDecimal(0);
        }

        /// <summary>
        /// Gets the hours.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public static decimal GetHours(this decimal minutes)
        {
            return (minutes / 60).ToRounded(2).ToDecimal(0);
        }

        /// <summary>
        /// Gets the service date hour.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetServiceDateHour(this DateTime date)
        {
            return date.ToString("hh").ToInt(0);
        }

        /// <summary>
        /// Gets the service date hour.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetServiceDateMinute(this DateTime date)
        {
            return date.ToString("mm").ToInt(0);
        }

        /// <summary>
        /// Gets the service date hour.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int GetServiceDateMeridiem(this DateTime date)
        {
            return date.ToString("tt").Equals("AM", StringComparison.CurrentCultureIgnoreCase) ? 1 : 2;
        }

        /// <summary>
        /// Determines whether date is week start.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsWeekStart(this DateTime? date)
        {
            return date.HasValue && date.Value.DayOfWeek == DayOfWeek.Monday;
        }

        /// <summary>
        /// Determines whether date is work week end.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsWorkWeekEnd(this DateTime? date)
        {
            return date.HasValue && date.Value.DayOfWeek == DayOfWeek.Friday;
        }

        /// <summary>
        /// Ins the range.
        /// </summary>
        /// <param name="dateToCheck">The date to check.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }

        /// <summary>
        /// Determines whether Is week end.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsWeekEnd(this DateTime date)
        {
            return
                date.DayOfWeek == DayOfWeek.Sunday ||
                date.DayOfWeek == DayOfWeek.Saturday;
        }

        /// <summary>
        /// Determines whether Is outside SQL date range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsOutsideSqlDateRange(this DateTime date)
        {
            return (DateTime) SqlDateTime.MinValue > date ||
                   (DateTime) SqlDateTime.MaxValue < date;
        }

        /// <summary>
        /// Determines whether Is outside SQL date range.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool IsOutsideSqlDateRange(this DateTime? date)
        {
            if (!date.HasValue)
                return false;

            return date.Value.IsOutsideSqlDateRange();
        }
    }
}