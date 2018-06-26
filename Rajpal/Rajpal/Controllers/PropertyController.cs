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
namespace Rajpal.Controllers
{
    public class PropertyController : Controller
    {
        private const int defaultPageSize = 2;
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
       
     
        public ActionResult Index(string Type, string IsList, int? page )
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
                    PropertList = _ResidentialService.GetResidentials().Skip(1).Take(10).ToList();
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
                    allEmployee.Add(dest);
                }

               // ViewData["employee_name"] = employee_name;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                IList<PropertyModell> employees = this.allEmployee;


                employees = employees.ToPagedList(currentPageIndex, defaultPageSize);

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

                if (Request.IsAjaxRequest())
                {
                    if (IsList == "List")
                    {
                        return PartialView("~/Views/Partial/PropertyList.cshtml", employees);
                    }
                    else
                    {
                        return PartialView("~/Views/Partial/PropertyGrid.cshtml", employees);
                    }
                }
                   
                else
                    return View(employees);
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