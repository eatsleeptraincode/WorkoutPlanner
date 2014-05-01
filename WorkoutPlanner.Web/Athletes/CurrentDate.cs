using System;

namespace WorkoutPlanner.Web.Athletes
{
    public class CurrentDate
    {
        private static readonly Func<DateTime> _originalValue = () => DateTime.Now;
        
        public static Func<DateTime> Now = _originalValue;
        public static Func<DateTime> Today = _originalValue;

        public static void Reset()
        {
            Now = _originalValue;
        }
    }
}