using Home.Source.Common;
using Home.Source.Extensions;

namespace Home.Source.Helpers
{
    public class EnumHelper
    {
        public static List<string> GetAppRoleNames()
        {
            return Enum.GetValues(typeof(AppRole)).Cast<AppRole>().Select(p => p.GetBasicName()).ToList();
        }
    }
}
