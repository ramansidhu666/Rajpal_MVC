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
using System.Data;
using Dapper;
using System.Web.Configuration;
namespace Rajpal.Controllers
{
    public class PropertyController : Controller
    {
        IDbConnection db = CommonClass.OpenConnection();
        private const int defaultPageSize = 10;
        private IList<PropertyModell> allEmployee = new List<PropertyModell>();
        private IList<PropertyModell> allFav = new List<PropertyModell>();
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

        private IList<PropertyModell> GetData(string Type, string dbName="",string userid="")
        {
            List<PropertyModel> PropertList = new List<PropertyModel>();
            if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Residential))
            {
                PropertList = _ResidentialService.GetResidentials(dbName,userid);
            }
            else if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Commercial))
            {
                PropertList = _CommercialService.GetCommercials(dbName, userid);
            }
            else if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Condo))
            {
                PropertList = _CondoService.GetCondos(dbName, userid);
            }
            else
            {
                PropertList = _ResidentialService.GetResidentials(dbName, userid);
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
                Type = TempData["PropertyType"] == null ? StaticPropertyType : TempData["PropertyType"].ToString();
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
                string userid = "11";// Session["UserId"] == null ? "" : Session["UserId"].ToString();
                var dbname = WebConfigurationManager.AppSettings["dbname"];
                if (PropertList == null)
                {
                    PropertList = this.GetData(Type,dbname, userid);
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

        [HttpPost]
        public ActionResult SaveFavourite(string MLSID, int ID,string Type)
        {
            try
            {
                int rowsAffected = 0;
                string sqlQuery = @"Insert Into tbl_Favourite (MLSID,ID,Type) Values (@MLSID,@ID,@Type);SELECT CAST(SCOPE_IDENTITY() as int)";
                rowsAffected = db.Query<int>(sqlQuery, new
                {
                    MLSID,
                    ID,
                    Type
                }).SingleOrDefault();

                Clear(Type);
                return Json("Suceess", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
           
        }
        [HttpPost]
        public ActionResult RemoveFavourite(string MLSID, int ID, string Type)
        {
            try
            {
                int rowsAffected = 0;
                string sqlQuery = @"delete from tbl_Favourite where ID=@ID and MLSID=@MLSID";
                rowsAffected = db.Execute(sqlQuery, new { ID = ID, MLSID = MLSID });

                Clear(Type);
                return Json("Suceess", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }

        private IList<PropertyModell> GetAllFavourite(int ID)
        {
            List<PropertyModel> PropertList = new List<PropertyModel>();

            var query = "Select MLSID,Type from [tbl_Favourite] where ID=@ID  group by Type,MLSID";
            var result = db.Query<tbl_Favourite>(query, new { ID = ID }).ToList();
            List<string> Residentials = new List<string>();
            List<string> Commercials = new List<string>();
            List<string> Condos = new List<string>();
            foreach (var item in result)
            {
                if (item.Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Residential))
                {
                    Residentials.Add(item.MLSID);
                }
                else if (item.Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Commercial))
                {
                    Commercials.Add(item.MLSID);
                }
                else if (item.Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Condo))
                {
                    Condos.Add(item.MLSID);
                }
            }


            PropertList = _ResidentialService.GetResidentials().Where(c=>Residentials.Contains(c.MLS)).ToList();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PropertyModel, PropertyModell>();
            });

            IMapper mapper = config.CreateMapper();
            foreach (var item in PropertList)
            {
                var dest = mapper.Map<PropertyModel, PropertyModell>(item);
                allFav.Add(dest);
            }

            PropertList = _CommercialService.GetCommercials().Where(c => Commercials.Contains(c.MLS)).ToList();
           
            foreach (var item in PropertList)
            {
                var dest = mapper.Map<PropertyModel, PropertyModell>(item);
                allFav.Add(dest);
            }

            PropertList = _CondoService.GetCondos().Where(c => Condos.Contains(c.MLS)).ToList();
           
            foreach (var item in PropertList)
            {
                var dest = mapper.Map<PropertyModel, PropertyModell>(item);
                allFav.Add(dest);
            }
            return allFav;
        }
        public ActionResult MyFavourite(int ID, int? page)
        {
            try
            {
                IList<PropertyModell> PropertList = this.GetAllFavourite(ID);
                ViewBag.TotalData = PropertList.Count();
                int currentPageIndex = page.HasValue ? page.Value : 1;
                PropertList = PropertList.ToPagedList(currentPageIndex, defaultPageSize);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/Partial/Favourite.cshtml", PropertList);
                }
                else
                {
                    return View(PropertList);
                }
                
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void Clear(string key)
        {
            HttpContext.Cache.Remove(key);
        }  
    }
}