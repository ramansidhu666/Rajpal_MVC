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
    
    public partial class Registration
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string UserType { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> Role { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> Birthday_Greeting { get; set; }
        public Nullable<bool> seasonal_Greeting { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
