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
using MvcPaging;
using System.Web.Caching;
namespace Rajpal.Controllers
{
    public class PropertyController : Controller
    {
        private const int defaultPageSize = 10;
        private IList<PropertyModell> allEmployee = new List<PropertyModell>();
        private IIdxCommercialService _CommercialService { get; set; }
        private IIdxResidentialService _ResidentialService { get; set; }
        private IIdxCondoService _CondoService { get; set; }
        public PropertyController(IIdxCommercialService CommercialService, IIdxResidentialService ResidentialService, IIdxCondoService CondoService)
        {
            this._CommercialService = CommercialService;
            this._ResidentialService = ResidentialService;
            this._CondoService = CondoService;
        }
        public ActionResult GetPropertyTypes(string Type)
        {
            var PropertyType = new List<PropertyType>();
            if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Residential))
            {
                PropertyType = _ResidentialService.GetPropertyTypes();
            }
            else if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Commercial))
            {
                PropertyType = _CommercialService.GetPropertyTypes_Comm();
            }
            else if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Condo))
            {
                PropertyType = _CondoService.GetPropertyTypes_Condo();
            }
            else
            {
                PropertyType = _ResidentialService.GetPropertyTypes();
            }
            List<SelectListItem> PropertyTypes = new List<SelectListItem>();
            foreach (var item in PropertyType)
            {
                PropertyTypes.Add(new SelectListItem
                {
                    Value = item.typeown1out,
                    Text = item.typeown1out
                });
            }

            return Json(PropertyTypes, JsonRequestBehavior.AllowGet);
        }

        private IList<PropertyModell> GetData(string Type)
        {
            List<PropertyModel> PropertList = new List<PropertyModel>();
            if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Residential))
            {
                PropertList = _ResidentialService.GetResidentials();
            }
            else if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Commercial))
            {
                PropertList = _CommercialService.GetCommercials();
            }
            else if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Condo))
            {
                PropertList = _ResidentialService.GetResidentials();
            }
            else
            {
                PropertList = _ResidentialService.GetResidentials();
            }
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PropertyModel, PropertyModell>();
            });

            IMapper mapper = config.CreateMapper();
            foreach (var item in PropertList)
            {
                var dest = mapper.Map<PropertyModel, PropertyModell>(item);
                allEmployee.Add(dest);
            }
            return allEmployee;

        }

        public ActionResult Index(string Type, string IsList, int? page,bool Ishome=false, string HomeType = "", string Keyword = "", string Status = "", string MinPrice = "0", string MaxPrice = "0", int Bedroom = 0, int Bathroom = 0,string Sort="")
        {

            var StaticPropertyType = "Residential";
            var people = new PagedData<PropertyModell>();
            try
            {
                if (Type != "" && Type != null)
                {
                    TempData["PropertyType"] = Type;
                }
                Type = TempData["PropertyType"].ToString();
                TempData.Keep("PropertyType");

                if (Sort != "")
                {
                    TempData["Sort"] = Sort;
                }
                Sort = TempData["Sort"] == null ? "" : TempData["Sort"].ToString();
                TempData.Keep("Sort");

                if (MinPrice != "0" )
                {
                    TempData["MinPrice"] = MinPrice;
                }
                MinPrice = TempData["MinPrice"]==null?"0":TempData["MinPrice"].ToString();
                TempData.Keep("MinPrice");

                if (MaxPrice != "0")
                {
                    TempData["MaxPrice"] = MaxPrice;
                }
                MaxPrice = TempData["MaxPrice"] == null ? "0" : TempData["MaxPrice"].ToString();
                TempData.Keep("MaxPrice");

                if (HomeType != "" && HomeType != "All Home Types" && HomeType != "0")
                {
                    TempData["HomeType"] = HomeType;
                }
                HomeType = TempData["HomeType"] == null ? "" : TempData["HomeType"].ToString();
                TempData.Keep("HomeType");

                if (Status != "" && Status != "All")
                {
                    TempData["Status"] = Status;
                }
                Status = TempData["Status"] == null ? "" : TempData["Status"].ToString();
                TempData.Keep("Status");

                if (Bedroom != 0)
                {
                    TempData["Bedroom"] = Bedroom;
                }
                Bedroom = TempData["Bedroom"] == null ? 0 :Convert.ToInt32( TempData["Bedroom"]);
                TempData.Keep("Bedroom");

                if (Bathroom != 0)
                {
                    TempData["Bathroom"] = Bathroom;
                }
                Bathroom = TempData["Bathroom"] == null ? 0 : Convert.ToInt32(TempData["Bathroom"]);
                TempData.Keep("Bathroom");

            
                var PropertList = HttpContext.Cache.Get(Type) as IList<PropertyModell>;

                if (PropertList == null)
                {
                    PropertList = this.GetData(Type);
                    HttpContext.Cache.Insert(Type, PropertList, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
                }
                if (HomeType != "" && HomeType != "All Home Types" && HomeType != "0")
                {
                    PropertList = PropertList.Where(c => c.TypeOwn1Out.ToLower() == HomeType.ToLower()).ToList();
                }
                if (Status != "" && Status != "All")
                {
                    PropertList = PropertList.Where(c => c.SaleLease.ToLower() == Status.ToLower()).ToList();
                }
                if (Bedroom != 0)
                {
                    PropertList = PropertList.Where(c => Convert.ToInt32(c.Bedrooms) >= Bedroom && c.Bedrooms != null).ToList();
                }
                if (Bathroom != 0)
                {
                    PropertList = PropertList.Where(c => Convert.ToInt32(c.Washrooms) >= Bathroom && c.Washrooms != null).ToList();
                }
                if (MinPrice != "0" && MaxPrice != "0")
                {
                    PropertList = PropertList.Where(c => decimal.Parse(c.ListPrice) >= decimal.Parse(MinPrice) && decimal.Parse(c.ListPrice) <= decimal.Parse(MaxPrice)).ToList();
                }
                if (Keyword != "")
                {
                    PropertList = PropertList.Where(c => c.MLS.ToLower() == Keyword.ToLower() || c.Address.ToLower().Contains(Keyword.ToLower()) || c.Province.ToLower().Contains(Keyword.ToLower()) || c.PostalCode.ToLower().Contains(Keyword.ToLower()) || c.MunicipalityDistrict.ToLower().Contains(Keyword.ToLower())).ToList();
                }
                if (Sort == "high-price")
                {
                    PropertList = PropertList.OrderByDescending(c => decimal.Parse(c.ListPrice)).ToList();
                }
                else if (Sort == "low-price")
                {
                    PropertList = PropertList.OrderBy(c => decimal.Parse(c.ListPrice)).ToList();
                }
              
                ViewBag.TotalData = PropertList.Count();
                // List<PropertyModel> PropertList = new List<PropertyModel>();
                List<PropertyModell> PropertyModel = new List<PropertyModell>();


                ViewBag.ListOrGrid = (IsList == "" || IsList == null ? "List" : IsList);


                // ViewData["employee_name"] = employee_name;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                //IList<PropertyModell> employees = this.allEmployee;


                PropertList = PropertList.ToPagedList(currentPageIndex, defaultPageSize);

                //if (page == 0)
                //{
                //    people.Data = PropertyModel.Take(PageSize).ToList();
                //}
                //else
                //{
                //    people.Data = PropertyModel.Skip(PageSize * (page - 1)).Take(PageSize).ToList();
                //    people.CurrentPage = page;
                //}

                //people.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)PropertyModel.Count() / PageSize));
                ViewBag.Type = Type;
                if (Request.IsAjaxRequest()&&!Ishome)
                {
                    if (IsList == "List")
                    {
                        return PartialView("~/Views/Partial/PropertyList.cshtml", PropertList);
                    }
                    else
                    {
                        return PartialView("~/Views/Partial/PropertyGrid.cshtml", PropertList);
                    }
                }

                else
                {
                   
                        return View(PropertList);
                   
                }
                   
                ////if (IsList == "" || IsList == null)
                ////{
                ////    return View(employees);
                ////}
                ////else
                ////{
                ////    if (IsList == "List")
                ////    {
                ////        return PartialView("~/Views/Partial/PropertyList.cshtml", employees);
                ////    }
                ////    else
                ////    {
                ////        return PartialView("~/Views/Partial/PropertyGrid.cshtml", employees);
                ////    }

                ////}
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