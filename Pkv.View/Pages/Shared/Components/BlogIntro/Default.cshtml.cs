
using Microsoft.AspNetCore.Mvc;

using Pkv.View.Models;

namespace Pkv.View.Components
{
    public class BlogIntroViewComponent : ViewComponent
    {
        public BlogIntroViewComponent()
        {

        }
        public IViewComponentResult Invoke(BlogIntroModel blogIntroModel)
        {
            return View(blogIntroModel);
        }
    }
}
