namespace Core
{
    public class Constants
    {
        /// <summary>
        /// Amount of social disciplines student can take
        /// </summary>
        public static readonly int AmountSocioDisciplines = 2;
        /// <summary>
        /// Amount of special disciplines student can take
        /// </summary>
        public static readonly int AmountSpecialDisciplines = 2;
        /// <summary>
        /// Program will order students of such courses: [1, MaxCourse]
        /// </summary>
        public static readonly int MaxCourse  = 4;
        /// <summary>
        /// Default group limit (use when string from web.config cant be parsed)
        /// </summary>
        internal static readonly int DefaultGroupLimit = 25;
    }
}
