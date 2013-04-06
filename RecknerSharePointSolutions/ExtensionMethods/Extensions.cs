using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;

namespace RecknerSharePointSolutions.ExtensionMethods
{
      public static partial class Extensions
    {
        private static string Separate(string first, string second, string seperator)
        {
            return first + seperator + second;
        }
         
        public static IEnumerable<string> EnumarateWeekNumbers(this DateTime startDate,  DateTime endDate)
        {
            var endWeek = GetWeekNumber(endDate);
            var startWeek = GetWeekNumber(startDate);

            if (startDate.Year < endDate.Year)
            {
                //Generate weeks of the first year.
                for (var week = startWeek; week < 53; week++)
                {
                    yield return Separate(startDate.Year.ToString(),  week.ToString(), "-");
                
                }

                //Generate in between
                for (var year = startDate.Year + 1; year < endDate.Year; year++)
                {
                    for (var week = 1; week < 53; week++)
                    {
                        yield return Separate(startDate.Year.ToString(), week.ToString(), "-");
                    }
                }

                //Generate weeks of the end date
                for (var week = 1; week < endWeek + 1; week++)
                {
                    yield return Separate(endDate.Year.ToString(), week.ToString(), "-");
                }

            }
            else
            {
                //Generate the weeek numbers if the timespan is less then  or equal to one year
             //   startWeek = GetWeekNumber(startDate);

                for (var week = startWeek; week < GetWeekNumber(endDate) + 1; week++)
                {
                    yield return Separate(startDate.Year.ToString(), week.ToString(), "-") ;
                }

            }

           
        }

        public static int GetWeekNumber(this DateTime dtPassed)
        {
            var ciCurr = CultureInfo.CurrentCulture;
            var weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static void Compute(this DataRow row, string expression, string columnName, int fromColumn, int toColumn, int round)
         {
             var columnValue = 0.0;
             var result = 0.0;
            
             for (var i = fromColumn; i < toColumn; i++)
             {
                 double.TryParse(row[i].ToString(), out columnValue);
                 result += columnValue;

             }

             switch (expression.ToUpper())
             {
                 case "SUM":
                   row[columnName] = result;
                   break;
                 case "AVG":
                     row[columnName] = Math.Round(result/(toColumn - fromColumn), round);
                   break;
                 default:
                     row[columnName] = 0;
                     break;
             }
             
         }
    }
}
