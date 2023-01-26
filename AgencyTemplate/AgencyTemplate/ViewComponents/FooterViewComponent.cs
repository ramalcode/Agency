using AgencyTemplate.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AgencyTemplate.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        readonly AppDBContext _context;

        public FooterViewComponent(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.Settings.ToDictionary(s => s.Key, s => s.Value));
        }
    }
}
