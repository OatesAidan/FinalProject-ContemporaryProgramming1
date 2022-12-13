namespace FinalProjectContemporaryProgramming
{
    public static class DBContext
    {
        public static readonly Models.DataLayer.ContemporaryProgramming_Final_Actual_Context Context=new Models.DataLayer.ContemporaryProgramming_Final_Actual_Context();

        public static string MdfFileLocation { get; internal set; }
    }
}
