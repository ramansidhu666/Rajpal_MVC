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
       

        public ActionResult Index()
        {
            return View();
        }
    }
}