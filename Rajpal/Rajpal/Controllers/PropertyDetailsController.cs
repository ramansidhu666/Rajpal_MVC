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
       

        public ActionResult Index(string Type,string MLS)
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
            DataTable dtRooms = new DataTable();
            dtRooms.Columns.Add("Room", typeof(string));
            dtRooms.Columns.Add("Level", typeof(string));
            dtRooms.Columns.Add("RoomDim", typeof(string));
            dtRooms.Columns.Add("RoomDesc", typeof(string));

            for (int i = 0; i < NoOfRoom; i++)
            {
                int RowIndex = i + 1;
                string vRoom =CheckNullOrEmptyvalue( result.Rooms + RowIndex); //CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + ""]));
                string vLevel = CheckNullOrEmptyvalue(result.Level + RowIndex) != "" ? result.Level + RowIndex : "0";// CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Level" + RowIndex + ""])) != "" ? Convert.ToString(dt.Rows[0]["Level" + RowIndex + ""]) : "0";
                string vRoomDim = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Length"])) != "" ? (Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Length"])) : "0") + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Width"]) != "" ? ("x" + Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Width"])) : "");// Convert.ToString(dt.Rows[0]["Room1Length"]) + "x" + Convert.ToString(dt.Rows[0]["Room1Width"]);
                string vRoomDesc = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc1"])) != "" ? (Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc1"])) : "----") + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc2"]) != "" ? ("," + Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc2"])) : "");

                DataRow dr = dtRooms.NewRow();
                dr["Room"] = vRoom;
                dr["Level"] = vLevel;
                dr["RoomDim"] = vRoomDim;
                dr["RoomDesc"] = vRoomDesc;
                dtRooms.Rows.Add(dr);
                LVroom.DataSource = dtRooms;
                LVroom.DataBind();
            }
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