using AgencyTemplate.Models.Base;

namespace AgencyTemplate.Models
{
    public class Employee:BaseNameableEntity
    {
        public double Salary { get; set; }
        public string ImageUrl { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Linkedn { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
