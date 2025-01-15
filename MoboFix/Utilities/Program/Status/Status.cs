namespace MoboFix.Utilities.Program.Status
{
    //Inner Program Status Codes
    public static class ProgramStatusCodes
    {
        public const int Accepted = 1100;
        public const int Rejected = 1101;
        public const int Pending = 1000;
        public const int Active = 2001;
        public const int Disable = 2000;
        public const int NA = 3000;
        public const int Invalid = 0;

        public static string StatusCodeDesc(int StatusCode)
        {
            var table = new Dictionary<int, string>()
            {
                {0,"Invalid" },
                {1100,"Accepted" } ,
                {1101,"Rejected" },
                {1000,"Pending "},
                {2001,"Active" },
                {2000,"Disable"},
                {3000,"Not Available" }
            };

            if (table[StatusCode] != null)
                return table[StatusCode];
            else return table[NA];
        }
    }
}
