namespace RestaurantReview.Web.Constants
{
    public class Application
    {
        public static readonly string Name = AppSettings.ReadSetting("Application.Name");
        public static readonly string Logo = AppSettings.ReadSetting("Application.Logo");
        public static readonly string CompanyName = AppSettings.ReadSetting("Application.CompanyName");
        public static readonly string AdminLink = AppSettings.ReadSetting("Application.AdminLink");

        public static readonly string LDAPUsername = AppSettings.ReadSetting("strLDAPUserName");
        public static readonly string LDAPPassword = AppSettings.ReadSetting("strLDAPPassword");
        public static readonly string LDAPPath = AppSettings.ReadSetting("strLDAPPath");
    }
}