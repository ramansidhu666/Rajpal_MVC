using Property.Entity;
using Property.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rajpal.Controllers
{
    public class PropertyController : Controller
    {
//        public PropertyController()
//{
//}
        private IIdxCommercialService _CommercialService { get; set; }
        private IIdxResidentialService _ResidentialService { get; set; }
        private IIdxCondoService _CondoService { get; set; }
        public PropertyController(IIdxCommercialService CommercialService, IIdxResidentialService ResidentialService, IIdxCondoService CondoService)
        {
            this._CommercialService = CommercialService;
            this._ResidentialService = ResidentialService;
            this._CondoService = CondoService;
        }
       
        public ActionResult Index(string Type)
        {
            var StaticPropertyType = "Residential";
            try
            {
                List<PropertyModel> PropertList = new List<PropertyModel>();
                if (Type ==EnumValue.GetEnumDescription( EnumValue.PropertyType.Residential))
                {
                    PropertList = _ResidentialService.GetResidentials().Skip(1).Take(14).ToList();
                }
                else if (Type == EnumValue.GetEnumDescription( EnumValue.PropertyType.Commercial))
                {
                    PropertList = _CommercialService.GetCommercials();
                }
                else if (Type == EnumValue.GetEnumDescription( EnumValue.PropertyType.Condo))
                {
                    PropertList = _ResidentialService.GetResidentials();
                }
                else
                {
                    PropertList = null;
                }
                return View(PropertList);
            }
            catch (Exception ex)
            {

                throw;
                return View();
            }


        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}