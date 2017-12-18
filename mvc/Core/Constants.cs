namespace Core
{
    public class Constants
    {
        /// <summary>
        /// Max discipline capacity
        /// </summary>
        public static readonly int DisciplineMaxCapacity = 200;
        /// <summary>
        /// Min Discipline capacity
        /// </summary>
        public static readonly int DisciplineMinCapacity = 25;
        /// <summary>
        /// Program will order students of such courses: [1, MaxCourse]
        /// </summary>
        public static readonly int MaxCourse  = 4;
        /// <summary>
        /// Default group limit (use when string from web.config cant be parsed)
        /// </summary>
        internal static readonly int DefaultGroupLimit = 25;
        /// <summary>
        /// Start date of students pick disciplines
        /// </summary>
        public const string StartDate = "StartDate";
        /// <summary>
        /// First deadline of students pick disciplines
        /// </summary>
        public const string FirstDeadline = "FirstDeadline";
        /// <summary>
        /// End date of students pick disciplines
        /// </summary>
        public const string SecondDeadline = "SecondDeadline";
    }
}
