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
        private int _Bedrooms = 0;
        public int Bedrooms { get { return (_Bedrooms == null ? 0 : _Bedrooms); } set { this._Bedrooms = value; } }

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


        public string Room1 { get; set; }
        public string Room1Desc1 { get; set; }
        public string Room1Desc2 { get; set; }
        public string Room1Length { get; set; }
        public string Room1Width { get; set; }
        public string Room10 { get; set; }
        public string Room10Desc1 { get; set; }
        public string Room10Desc2 { get; set; }
        public string Room10Length { get; set; }
        public string Room10Width { get; set; }
        public string Room11 { get; set; }
        public string Room11Desc1 { get; set; }
        public string Room11Desc2 { get; set; }
        public string Room11Length { get; set; }
        public string Room11Width { get; set; }
        public string Room12 { get; set; }
        public string Room12Desc1 { get; set; }
        public string Room12Desc2 { get; set; }
        public string Room12Length { get; set; }
        public string Room12Width { get; set; }
        public string Room2 { get; set; }
        public string Room2Desc1 { get; set; }
        public string Room2Desc2 { get; set; }
        public string Room2Length { get; set; }
        public string Room2Width { get; set; }
        public string Room3 { get; set; }
        public string Room3Desc1 { get; set; }
        public string Room3Desc2 { get; set; }
        public string Room3Length { get; set; }
        public string Room3Width { get; set; }
        public string Room4 { get; set; }
        public string Room4Desc1 { get; set; }
        public string Room4Desc2 { get; set; }
        public string Room4Length { get; set; }
        public string Room4Width { get; set; }
        public string Room5 { get; set; }
        public string Room5Desc1 { get; set; }
        public string Room5Desc2 { get; set; }
        public string Room5Length { get; set; }
        public string Room5Width { get; set; }
        public string Room6 { get; set; }
        public string Room6Desc1 { get; set; }
        public string Room6Desc2 { get; set; }
        public string Room6Length { get; set; }
        public string Room6Width { get; set; }
        public string Room7 { get; set; }
        public string Room7Desc1 { get; set; }
        public string Room7Desc2 { get; set; }
        public string Room7Length { get; set; }
        public string Room7Width { get; set; }
        public string Room8 { get; set; }
        public string Room8Desc1 { get; set; }
        public string Room8Desc2 { get; set; }
        public string Room8Length { get; set; }
        public string Room8Width { get; set; }
        public string Room9 { get; set; }
        public string Room9Desc1 { get; set; }
        public string Room9Desc2 { get; set; }
        public string Room9Length { get; set; }
        public string Room9Width { get; set; }

        public string RoomsPlus { get; set; }


        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string Level6 { get; set; }
        public string Level7 { get; set; }
        public string Level8 { get; set; }
        public string Level9 { get; set; }
        public string Level10 { get; set; }
        public string Level11 { get; set; }

        public RoomLevelList roomlevels { get; set; }
    }
    public class RoomLevelList{
        public string Level { get; set; }
        public RoomLevels roomlevel { get; set; }
}
    public class RoomLevels
    {
        public string Room { get; set; }
        public string Level { get; set; }
        public string RoomDim { get; set; }
        public string RoomDesc { get; set; }
    }
}