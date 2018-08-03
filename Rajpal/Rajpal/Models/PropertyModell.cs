using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rajpal.Models
{
    public partial class PropertyModell
    {
       

     
        private string _Address = "";
        public string Address { get { return (_Address == null ? "" : _Address); } set { this._Address = value; } }
        public string AirConditioning { get; set; }
        public string ApproxSquareFootage { get; set; }
        public string Area { get; set; }
        public string Basement1 { get; set; }
        private string _Bedrooms = "";
        public string Bedrooms { get { return (_Bedrooms == null ? "" : _Bedrooms); } set { this._Bedrooms = value; } }

        public string Community { get; set; }
        public string DirectionsCrossStreets { get; set; }
        public List<PropertyModell> FeaturedProperties { get; set; }
        public string GarageType { get; set; }
        public string IdxUpdtedDt { get; set; }
        public string Kitchens { get; set; }
        public string ListBrokerage { get; set; }
        public string ListPrice { get; set; }
    
        private string _MLS = "";
        public string MLS { get { return (_MLS == null ? "" : _MLS); } set { this._MLS = value; } }

        public string Municipality { get; set; }
        private string _MunicipalityDistrict = "";
        public string MunicipalityDistrict { get { return (_MunicipalityDistrict == null ? "" : _MunicipalityDistrict); } set { this._MunicipalityDistrict = value; } }
        public string ParkingSpaces { get; set; }
        public string pImage { get; set; }
        public string Pool { get; set; }
        private string _PostalCode = "";
        public string PostalCode { get { return (_PostalCode == null ? "" : _PostalCode); } set { this._PostalCode = value; } }
       // public List<PropertyImages> PropertyImages { get; set; }
  
        private string _Province = "";
        public string Province { get { return (_Province == null ? "" : _Province); } set { this._Province = value; } }
        public string RemarksForClients { get; set; }
        public string Rooms { get; set; }
        public string SaleLease { get; set; }
        public string serverimagepath { get; set; }
        private string _Status = "";
        public string Status { get { return (_Status == null ? "" : _Status); } set { this._Status = value; } }
       
        public string Street { get; set; }
        public string StreetAbbreviation { get; set; }
        public string StreetDirection { get; set; }
        public string StreetName { get; set; }
        public string Style { get; set; }
        public string Taxes { get; set; }
        public string TypeOwn1Out { get; set; }
        public string UtilitiesCable { get; set; }
        public string UtilitiesGas { get; set; }
        public bool VOX { get; set; }
        private int _Washrooms = 0;
        public int Washrooms { get { return (_Washrooms == null ? 0 : _Washrooms); } set { this._Washrooms = value; } }
       
        public string Water { get; set; }
        public string Zoning { get; set; }
        public string exist { get; set; }
    }

    
}