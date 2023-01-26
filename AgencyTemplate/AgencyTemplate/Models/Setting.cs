using AgencyTemplate.Models.Base;

namespace AgencyTemplate.Models
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
