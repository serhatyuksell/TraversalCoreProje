using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.Default
{
    public class _Feature: ViewComponent
    {
        FeatureManager _featureManager=new FeatureManager(new EfFeatureDal());
        public IViewComponentResult Invoke()
        {
            
            return View();
          
        }
    }
}
