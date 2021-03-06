﻿namespace NCldr.Types
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Calendar is a CLDR calendar
    /// </summary>
    /// <remarks>CLDR reference: http://www.unicode.org/reports/tr35/#Calendar_Elements </remarks>
    [Serializable]
    public class Calendar
    {
        /// <summary>
        /// Gets or sets the calendar's identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the calendar's date formats
        /// </summary>
        public DateFormat[] DateFormats { get; set; }

        /// <summary>
        /// Gets or sets the calendar's sets of day names
        /// </summary>
        public DayNameSet[] DayNameSets { get; set; }

        /// <summary>
        /// Gets or sets the calendar's sets of day period names
        /// </summary>
        public DayPeriodNameSet[] DayPeriodNameSets { get; set; }

        /// <summary>
        /// Gets or sets the calendar's sets of era names
        /// </summary>
        public EraNameSet[] EraNameSets { get; set; }

        /// <summary>
        /// Gets or sets the calendar's sets of month names
        /// </summary>
        public MonthNameSet[] MonthNameSets { get; set; }

        /// <summary>
        /// Gets or sets the calendar's time formats
        /// </summary>
        public TimeFormat[] TimeFormats { get; set; }

        /// <summary>
        /// Gets the calendar's CalendarType (corresponding to the calendar's Id)
        /// </summary>
        public CalendarType CalendarType
        {
            get
            {
                if (NCldr.CalendarTypes == null)
                {
                    return null;
                }

                return (from ct in NCldr.CalendarTypes
                        where string.Compare(ct.Id, this.Id, StringComparison.InvariantCulture) == 0
                        select ct).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets an array of localized abbreviated day names
        /// </summary>
        public string[] AbbreviatedDayNames
        {
            get
            {
                return this.GetDayNames("abbreviated", new string[] { "stand-alone", "format" });
            }
        }

        /// <summary>
        /// Gets an array of localized day names
        /// </summary>
        public string[] DayNames
        {
            get
            {
                return this.GetDayNames("wide", new string[] { "stand-alone", "format" });
            }
        }

        /// <summary>
        /// Gets an array of localized shortest day names
        /// </summary>
        public string[] ShortestDayNames
        {
            get
            {
                return this.GetDayNames("narrow", new string[] { "stand-alone", "format" });
            }
        }

        /// <summary>
        /// Gets an array of localized abbreviated genitive month names
        /// </summary>
        public string[] AbbreviatedMonthGenitiveNames
        {
            get
            {
                return this.GetMonthNames("abbreviated", new string[] { "format", "stand-alone" });
            }
        }

        /// <summary>
        /// Gets an array of localized genitive month names
        /// </summary>
        public string[] MonthGenitiveNames
        {
            get
            {
                return this.GetMonthNames("wide", new string[] { "format", "stand-alone" });
            }
        }

        /// <summary>
        /// Gets an array of localized abbreviated month names
        /// </summary>
        public string[] AbbreviatedMonthNames
        {
            get
            {
                return this.GetMonthNames("abbreviated", new string[] { "stand-alone", "format" });
            }
        }

        /// <summary>
        /// Gets an array of localized month names
        /// </summary>
        public string[] MonthNames
        {
            get
            {
                return this.GetMonthNames("wide", new string[] { "stand-alone", "format" });
            }
        }

        /// <summary>
        /// Gets an array of localized shortest month names
        /// </summary>
        public string[] ShortestMonthNames
        {
            get
            {
                return this.GetMonthNames("narrow", new string[] { "stand-alone", "format" });
            }
        }

        /// <summary>
        /// Gets the localized AM designator
        /// </summary>
        public string AMDesignator
        {
            get
            {
                DayPeriodName[] dayPeriodNames = this.GetDayPeriods("wide");
                if (dayPeriodNames != null)
                {
                    return (from dpn in dayPeriodNames
                            where string.Compare(dpn.Id, "am", StringComparison.InvariantCulture) == 0
                            select dpn.Name).FirstOrDefault();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the localized PM designator
        /// </summary>
        public string PMDesignator
        {
            get
            {
                DayPeriodName[] dayPeriodNames = this.GetDayPeriods("wide");
                if (dayPeriodNames != null)
                {
                    return (from dpn in dayPeriodNames
                            where string.Compare(dpn.Id, "pm", StringComparison.InvariantCulture) == 0
                            select dpn.Name).FirstOrDefault();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the CLDR full date pattern
        /// </summary>
        public string FullDatePattern
        {
            get
            {
                return this.GetDateFormatPattern("full");
            }
        }

        /// <summary>
        /// Gets the .NET full date pattern
        /// </summary>
        public string FullDotNetDatePattern
        {
            get
            {
                string dateFormatPattern = this.FullDatePattern;
                if (dateFormatPattern == null)
                {
                    return null;
                }

                return DateTimeFormat.GetDotNetFormat(dateFormatPattern);
            }
        }

        /// <summary>
        /// Gets the CLDR long date pattern
        /// </summary>
        public string LongDatePattern
        {
            get
            {
                return this.GetDateFormatPattern("long");
            }
        }

        /// <summary>
        /// Gets the .NET long date pattern
        /// </summary>
        public string LongDotNetDatePattern
        {
            get
            {
                string dateFormatPattern = this.LongDatePattern;
                if (dateFormatPattern == null)
                {
                    return null;
                }

                return DateTimeFormat.GetDotNetFormat(dateFormatPattern);
            }
        }

        /// <summary>
        /// Gets the CLDR short date pattern
        /// </summary>
        public string ShortDatePattern
        {
            get
            {
                return this.GetDateFormatPattern("short");
            }
        }

        /// <summary>
        /// Gets the .NET short date pattern
        /// </summary>
        public string ShortDotNetDatePattern
        {
            get
            {
                string dateFormatPattern = this.ShortDatePattern;
                if (dateFormatPattern == null)
                {
                    return null;
                }

                return DateTimeFormat.GetDotNetFormat(dateFormatPattern);
            }
        }

        /// <summary>
        /// Gets the CLDR long time pattern
        /// </summary>
        public string LongTimePattern
        {
            get
            {
                return this.GetTimeFormatPattern("long");
            }
        }

        /// <summary>
        /// Gets the .NET long time pattern
        /// </summary>
        public string LongDotNetTimePattern
        {
            get
            {
                string timeFormatPattern = this.LongTimePattern;
                if (timeFormatPattern == null)
                {
                    return null;
                }

                return DateTimeFormat.GetDotNetFormat(timeFormatPattern);
            }
        }

        /// <summary>
        /// Gets the CLDR short time pattern
        /// </summary>
        public string ShortTimePattern
        {
            get
            {
                return this.GetTimeFormatPattern("short");
            }
        }

        /// <summary>
        /// Gets the .NET short time pattern
        /// </summary>
        public string ShortDotNetTimePattern
        {
            get
            {
                string timeFormatPattern = this.ShortTimePattern;
                if (timeFormatPattern == null)
                {
                    return null;
                }

                return DateTimeFormat.GetDotNetFormat(timeFormatPattern);
            }
        }

        /// <summary>
        /// Gets the date separator
        /// </summary>
        /// <remarks>CLDR does not have a specific value for a date separator so this algorithm makes
        /// a guess based on the short date format</remarks>
        public string DateSeparator
        {
            get
            {
                string shortDotNetDatePattern = this.ShortDotNetDatePattern;
                if (string.IsNullOrEmpty(shortDotNetDatePattern))
                {
                    return "-";
                }

                // strip away everything that isn't a date separator
                shortDotNetDatePattern = shortDotNetDatePattern.Replace("y", string.Empty);
                shortDotNetDatePattern = shortDotNetDatePattern.Replace("M", string.Empty);
                shortDotNetDatePattern = shortDotNetDatePattern.Replace("d", string.Empty);
                shortDotNetDatePattern = shortDotNetDatePattern.Replace(" ", string.Empty);
                shortDotNetDatePattern = shortDotNetDatePattern.Replace("e", string.Empty);

                if (shortDotNetDatePattern.Length > 0)
                {
                    // all that should be left now is one or more date separators
                    char dateSeparator = shortDotNetDatePattern[0];
                    if (dateSeparator == '-' || dateSeparator == '/')
                    {
                        // it is a known date separator
                        return dateSeparator.ToString();
                    }
                }

                return "-";
            }
        }

        /// <summary>
        /// Gets the time separator
        /// </summary>
        /// <remarks>CLDR does not have a specific setting for a time separator so this is a guess</remarks>
        public string TimeSeparator
        {
            get
            {
                return ":";
            }
        }

        /// <summary>
        /// Combine combines a child with a parent as necessary and returns the combined object
        /// </summary>
        /// <param name="combinedCalendar">The child object</param>
        /// <param name="parentCalendar">The parent object</param>
        /// <returns>The combined object</returns>
        public static Calendar Combine(Calendar combinedCalendar, Calendar parentCalendar)
        {
            if (combinedCalendar.DateFormats == null || combinedCalendar.DateFormats.GetLength(0) == 0)
            {
                combinedCalendar.DateFormats = parentCalendar.DateFormats;
            }

            combinedCalendar.DayNameSets = CalendarNameSet<DayName>.Combine<DayNameSet>(
                combinedCalendar.DayNameSets, 
                parentCalendar.DayNameSets);

            if (combinedCalendar.DayPeriodNameSets == null || combinedCalendar.DayPeriodNameSets.GetLength(0) == 0)
            {
                combinedCalendar.DayPeriodNameSets = parentCalendar.DayPeriodNameSets;
            }

            if (combinedCalendar.EraNameSets == null || combinedCalendar.EraNameSets.GetLength(0) == 0)
            {
                combinedCalendar.EraNameSets = parentCalendar.EraNameSets;
            }

            if (combinedCalendar.MonthNameSets == null || combinedCalendar.MonthNameSets.GetLength(0) == 0)
            {
                combinedCalendar.MonthNameSets = parentCalendar.MonthNameSets;
            }
            else
            {
                combinedCalendar.MonthNameSets =
                    CombineMonthNameSets(combinedCalendar.MonthNameSets, parentCalendar.MonthNameSets);
            }

            if (combinedCalendar.TimeFormats == null || combinedCalendar.TimeFormats.GetLength(0) == 0)
            {
                combinedCalendar.TimeFormats = parentCalendar.TimeFormats;
            }

            return combinedCalendar;
        }

        /// <summary>
        /// CombineMonthNameSets combines a child with a parent as necessary and returns the combined object
        /// </summary>
        /// <param name="combinedMonthNameSets">The child object</param>
        /// <param name="parentMonthNameSets">The parent object</param>
        /// <returns>The combined object</returns>
        private static MonthNameSet[] CombineMonthNameSets(MonthNameSet[] combinedMonthNameSets, MonthNameSet[] parentMonthNameSets)
        {
            List<MonthNameSet> combinedMonthNameSetList = new List<MonthNameSet>(combinedMonthNameSets);
            if (parentMonthNameSets != null)
            {
                foreach (MonthNameSet parentMonthNameSet in parentMonthNameSets)
                {
                    MonthNameSet combinedMonthNameSet = (from cmns in combinedMonthNameSetList
                                                         where string.Compare(cmns.Id, parentMonthNameSet.Id, StringComparison.InvariantCulture) == 0
                                                         select cmns).FirstOrDefault();
                    if (combinedMonthNameSet == null)
                    {
                        // the combined set does not have the parent set so add it
                        combinedMonthNameSetList.Add(parentMonthNameSet);
                    }
                    else
                    {
                        // combine the two lists
                        List<MonthName> combinedMonthNames = new List<MonthName>(combinedMonthNameSet.Names);
                        foreach (MonthName parentMonthName in parentMonthNameSet.Names)
                        {
                            if (!(from cmn in combinedMonthNames
                                  where string.Compare(cmn.Id, parentMonthName.Id, StringComparison.InvariantCulture) == 0
                                  select cmn).Any())
                            {
                                // the parent month name does not exist in the combined month names
                                combinedMonthNames.Add(parentMonthName);
                            }
                        }

                        combinedMonthNameSet.Names = combinedMonthNames.OrderBy(monthName => int.Parse(monthName.Id)).ToArray();
                    }
                }
            }

            return combinedMonthNameSetList.ToArray();
        }

        /// <summary>
        /// GetDayNames gets an array of day names for the given DayNameSet Id
        /// </summary>
        /// <param name="id">The Id of the DayNameSet</param>
        /// <param name="contexts">An array of context names representing the order in which the contexts should be searched</param>
        /// <returns>An array of day names for the given DayNameSet Id</returns>
        private string[] GetDayNames(string id, string[] contexts)
        {
            if (this.DayNameSets == null)
            {
                return null;
            }

            DayName[] dayNames = null;

            // look for a day name set in the order in which the contexts are listed
            foreach (string context in contexts)
            {
                dayNames = (from dns in this.DayNameSets
                            where string.Compare(dns.Id, id, StringComparison.InvariantCulture) == 0
                            && string.Compare(dns.Context, context, StringComparison.InvariantCulture) == 0
                            select dns.Names).FirstOrDefault();
            }

            if (dayNames == null)
            {
                // there are no day name sets for the given context names so get the first day name set that matches the id
                dayNames = (from dns in this.DayNameSets
                            where string.Compare(dns.Id, id, StringComparison.InvariantCulture) == 0
                            select dns.Names).FirstOrDefault();
            }

            if (dayNames != null)
            {
                return (from dn in dayNames
                        select dn.Name).ToArray();
            }

            return null;
        }

        /// <summary>
        /// GetMonthNames gets an array of month names for the given MonthNameSet Id
        /// </summary>
        /// <param name="id">The Id of the MonthNameSet</param>
        /// <param name="contexts">An array of context names representing the order in which the contexts should be searched</param>
        /// <returns>An array of month names for the given MonthNameSet Id</returns>
        private string[] GetMonthNames(string id, string[] contexts)
        {
            if (this.MonthNameSets == null)
            {
                return null;
            }

            MonthName[] monthNames = null;

            // look for a month name set in the order in which the contexts are listed
            foreach (string context in contexts)
            {
                monthNames = (from dns in this.MonthNameSets
                              where string.Compare(dns.Id, id, StringComparison.InvariantCulture) == 0
                              && string.Compare(dns.Context, context, StringComparison.InvariantCulture) == 0
                              select dns.Names).FirstOrDefault();
            }

            if (monthNames == null)
            {
                // there are no month name sets for the given context names so get the first month name set that matches the id
                monthNames = (from dns in this.MonthNameSets
                              where string.Compare(dns.Id, id, StringComparison.InvariantCulture) == 0
                              select dns.Names).FirstOrDefault();
            }

            if (monthNames != null)
            {
                List<string> monthNamesList = (from dn in monthNames
                                               select dn.Name).ToList();

                // the list of month names must be 13 names long
                while (monthNamesList.Count < 13)
                {
                    monthNamesList.Add(string.Empty);
                }

                return monthNamesList.ToArray();
            }

            return null;
        }

        /// <summary>
        /// GetDayPeriods gets an array of day period names for the given DayPeriodNameSet Id
        /// </summary>
        /// <param name="id">The Id of the DayNameSet</param>
        /// <returns>An array of day names for the given DayNameSet Id</returns>
        private DayPeriodName[] GetDayPeriods(string id)
        {
            if (this.DayPeriodNameSets == null)
            {
                return null;
            }

            DayPeriodName[] dayPeriodNames = (from dns in this.DayPeriodNameSets
                                              where string.Compare(dns.Id, id, StringComparison.InvariantCulture) == 0
                                              select dns.Names).FirstOrDefault();
            if (dayPeriodNames != null)
            {
                return (from dn in dayPeriodNames
                        select dn).ToArray();
            }

            return null;
        }

        /// <summary>
        /// GetDateFormatPattern gets the CLDR date format pattern for the given date format Id
        /// </summary>
        /// <param name="dateFormatId">The Id of the date format pattern to get</param>
        /// <returns>The CLDR date format pattern for the given date format Id</returns>
        private string GetDateFormatPattern(string dateFormatId)
        {
            if (this.DateFormats == null)
            {
                return null;
            }

            return (from df in this.DateFormats
                    where string.Compare(df.Id, dateFormatId, StringComparison.InvariantCulture) == 0
                    select df.Pattern).FirstOrDefault();
        }

        /// <summary>
        /// GetTimeFormatPattern gets the CLDR time format pattern for the given time format Id
        /// </summary>
        /// <param name="timeFormatId">The Id of the time format pattern to get</param>
        /// <returns>The CLDR time format pattern for the given time format Id</returns>
        private string GetTimeFormatPattern(string timeFormatId)
        {
            if (this.TimeFormats == null)
            {
                return null;
            }

            return (from tf in this.TimeFormats
                    where string.Compare(tf.Id, timeFormatId, StringComparison.InvariantCulture) == 0
                    select tf.Pattern).FirstOrDefault();
        }
    }
}
