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
    public class PropertyDetailsController : Controller
    {
        IDbConnection db = CommonClass.OpenConnection();
        private const int defaultPageSize = 10;
        private IList<PropertyModell> allEmployee = new List<PropertyModell>();
        private IList<PropertyModell> allFav = new List<PropertyModell>();
        private IIdxCommercialService _CommercialService { get; set; }
        private IIdxResidentialService _ResidentialService { get; set; }
        private IIdxCondoService _CondoService { get; set; }
        public PropertyDetailsController(IIdxCommercialService CommercialService, IIdxResidentialService ResidentialService, IIdxCondoService CondoService)
        {
            this._CommercialService = CommercialService;
            this._ResidentialService = ResidentialService;
            this._CondoService = CondoService;
        }
       

        public ActionResult Index(string Type="Residential",string MLS="W4194008")
        {
            var result = new PropertyModel();
            if(Type==EnumValue.GetEnumDescription(EnumValue.PropertyType.Residential))
            {
                 result = _ResidentialService.GetSingleProperty(MLS);
            }
            else if (Type == EnumValue.GetEnumDescription(EnumValue.PropertyType.Commercial))
            {
                result = _CommercialService.GetSingleProperty(MLS);
            }
            else
            {
                result =_CondoService.GetSingleProperty(MLS);
            }
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PropertyModel, PropertyModell>();
            });
            IMapper mapper = config.CreateMapper();
            var dest = mapper.Map<PropertyModel, PropertyModell>(result);
            int NoOfRoom =Convert.ToInt32( result.Rooms);
            List<RoomLevels> list = new List<RoomLevels>();

            int i = 0;
             if (NoOfRoom != i)
             {
            RoomLevels obj = new RoomLevels();
            obj.Level = result.Level1;
            obj.Room = result.Room1;
            obj.RoomDesc = result.Room1Desc1 + result.Room1Desc2;
            obj.RoomDim = result.Room1Length + "x" + result.Room1Width;
            list.Add(obj);
            i = i + 1;
           }
           
            if (NoOfRoom != i)
            {
                RoomLevels obj1 = new RoomLevels();
                obj1.Level = result.Level2;
                obj1.Room = result.Room2;
                obj1.RoomDesc = result.Room2Desc1 + result.Room2Desc2;
                obj1.RoomDim = result.Room2Length + "x" + result.Room2Width;
                list.Add(obj1);
                i = i + 1;
            }
           
            if (NoOfRoom != i)
            {
            RoomLevels obj2 = new RoomLevels();
            obj2.Level = result.Level3;
            obj2.Room = result.Room3;
            obj2.RoomDesc = result.Room3Desc1 + result.Room3Desc2;
            obj2.RoomDim = result.Room3Length + "x" + result.Room3Width;
            list.Add(obj2);
            i = i + 1;
            }
           
            if (NoOfRoom != i)
            {
            RoomLevels obj3 = new RoomLevels();
            obj3.Level = result.Level4;
            obj3.Room = result.Room4;
            obj3.RoomDesc = result.Room4Desc1 + result.Room4Desc2;
            obj3.RoomDim = result.Room4Length + "x" + result.Room4Width;
            list.Add(obj3);
            i = i + 1;
             }
            if (NoOfRoom != i)
            {
                RoomLevels obj4 = new RoomLevels();
                obj4.Level = result.Level5;
                obj4.Room = result.Room5;
                obj4.RoomDesc = result.Room5Desc1 + result.Room5Desc2;
                obj4.RoomDim = result.Room5Length + "x" + result.Room5Width;
                list.Add(obj4);
                i = i + 1;
            }
            if (NoOfRoom != i)
            {
                RoomLevels obj5 = new RoomLevels();
                obj5.Level = result.Level6;
                obj5.Room = result.Room6;
                obj5.RoomDesc = result.Room6Desc1 + result.Room6Desc2;
                obj5.RoomDim = result.Room6Length + "x" + result.Room6Width;
                list.Add(obj5);
                i = i + 1;
            }
            if (NoOfRoom != i)
            {
                RoomLevels obj6 = new RoomLevels();
                obj6.Level = result.Level7;
                obj6.Room = result.Room7;
                obj6.RoomDesc = result.Room7Desc1 + result.Room7Desc2;
                obj6.RoomDim = result.Room7Length + "x" + result.Room7Width;
                list.Add(obj6);
                i = i + 1;
            }
            if (NoOfRoom != i)
            {
                RoomLevels obj7 = new RoomLevels();
                obj7.Level = result.Level8;
                obj7.Room = result.Room8;
                obj7.RoomDesc = result.Room8Desc1 + result.Room8Desc2;
                obj7.RoomDim = result.Room8Length + "x" + result.Room8Width;
                list.Add(obj7);
                i = i + 1;
            }
            if (NoOfRoom != i)
            {
                RoomLevels obj8 = new RoomLevels();
                obj8.Level = result.Level9;
                obj8.Room = result.Room9;
                obj8.RoomDesc = result.Room9Desc1 + result.Room9Desc2;
                obj8.RoomDim = result.Room9Length + "x" + result.Room9Width;
                list.Add(obj8);
                i = i + 1;
            }
          var hgfh = list.GroupBy(c => c.Level)
                 .Select(group =>
                        new
                        {
                            Level = group.Key,
                            roomlevels=group.ToList()
                        })
                .ToList();
            return View(dest);
        }
        private string CheckNullOrEmptyvalue(string pValue)
        {
            string pval1 = "";
            if (pValue == "null" || pValue == "")
                pval1 = "";
            else
                pval1 = pValue;
            return pval1;
        }
    }
}