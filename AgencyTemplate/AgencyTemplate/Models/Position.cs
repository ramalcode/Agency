using AgencyTemplate.Models.Base;

namespace AgencyTemplate.Models
{
    public class Position:BaseNameableEntity
    {
        public ICollection<Employee> Employees { get; set; }
    }
}
