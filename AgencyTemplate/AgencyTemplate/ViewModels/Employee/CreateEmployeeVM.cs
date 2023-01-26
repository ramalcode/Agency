using AgencyTemplate.Models;
using AgencyTemplate.Models.Base;

namespace AgencyTemplate.ViewModels
{
    public class CreateEmployeeVM:BaseNameableEntity
    {
        public double Salary { get; set; }
        public string? ImageUrl { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Linkedn { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public IFormFile Image { get; set; }
    }
}
