using AutoMapper;
using Property.Entity;
using Property.Service;
using Rajpal.Data;
using Rajpal.Helpers;
using Rajpal.Models;
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
        public const int PageSize = 5;

        //public ActionResult Index()
        //{
        //    var people = new Paging<PropertyModell>();

        //    using (var ctx = new AjaxPagingContext())
        //    {
        //        people.Data = ctx.People.OrderBy(p => p.Surname).Take(PageSize).ToList();
        //        people.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)ctx.People.Count() / PageSize));
        //    }

        //    return View(people);
        //}
        public ActionResult Index(string Type, string IsList, int page = 0)
        {
            var StaticPropertyType = "Residential";
            var people = new PagedData<PropertyModell>();
            try
            {
                if(Type!=""&& Type!=null)
                {
                    TempData["PropertyType"] = Type;
                }
                Type = TempData["PropertyType"].ToString();
                TempData.Keep("PropertyType");
                List<PropertyModel> PropertList = new List<PropertyModel>();
                List<PropertyModell> PropertyModel = new List<PropertyModell>();
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
                
                ViewBag.ListOrGrid= (IsList == "" || IsList == null ? "List" : IsList);
                
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PropertyModel, PropertyModell>();
                });

                IMapper mapper = config.CreateMapper();
                foreach (var item in PropertList)
                {
                    var dest = mapper.Map<PropertyModel, PropertyModell>(item);
                    PropertyModel.Add(dest);
                }
                if (page == 0)
                {
                    people.Data = PropertyModel.Take(PageSize).ToList();
                }
                else
                {
                    people.Data = PropertyModel.Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                    people.CurrentPage = page;
                }
               
                people.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)PropertyModel.Count() / PageSize));
                if (IsList == "" || IsList == null)
                {
                    return View(people);
                }
                else
                {
                    if (IsList == "List")
                    {
                        return PartialView("~/Views/Partial/PropertyList.cshtml", people);
                    }
                    else
                    {
                        return PartialView("~/Views/Partial/PropertyGrid.cshtml", people);
                    }

                }
                //Mapper.CreateMap<PropertyModel, PropertyModell>();
               
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