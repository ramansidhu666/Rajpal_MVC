//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rajpal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_UserInfo
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNo { get; set; }
        public byte[] Image { get; set; }
        public string WebsiteURL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Role { get; set; }
    }
}