using System.Collections.Generic;

namespace UVA_Arena.Structures
{
    public class UserRanklist
    {
        /// <summary> Constructor </summary>
        public UserRanklist() { }

        /// <summary> The rank of the user </summary>
        public long rank { get; set; }
        /// <summary> Non zero if the user is an old UVa user that hasn't migrate </summary>
        public long old { get; set; }
        /// <summary> The name of the user </summary>
        public string name { get; set; }
        /// <summary> The username of the user </summary>    
        public string username { get; set; }
        /// <summary> The number of accepted problems </summary>    
        public long ac { get; set; }
        /// <summary> The number of submissions of the user </summary>
        public long nos { get; set; }
        /// <summary> The number of accepted problems of the user in 2 days, 7 days, 31 days, 3 months, and 1 year. </summary>
        public List<long> activity { get; set; }

        /// <summary> The number of accepted problems of the user in 2 days. </summary>
        public long day2 { get { return activity[0]; } }
        /// <summary> The number of accepted problems of the user in 7 days. </summary>
        public long day7 { get { return activity[1]; } }
        /// <summary> The number of accepted problems of the user in 31 days. </summary>
        public long day31 { get { return activity[2]; } }
        /// <summary> The number of accepted problems of the user in 3 months. </summary>
        public long month3 { get { return activity[3]; } }
        /// <summary> The number of accepted problems of the user in 1 year. </summary>
        public long year1 { get { return activity[4]; } }

    }
}
