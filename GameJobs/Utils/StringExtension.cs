namespace FT.NH88.GameJobs.Utils
{
   public static   class   StringExtension
    {
      
            public static string TrimIfNotNull(this string value)
            {
                if (value != null)
                {
                    return value.Trim();
                }
                return null;
            }
        }
    
}
