using System;
using System.ComponentModel;
using System.Reflection;

namespace Rajpal
{
    public class EnumValue
    {
        public enum PropertyType { [Description("Residential")]Residential, [Description("Commercial")] Commercial, [Description("Condo")] Condo }
        public enum EmailType { [Description("Appointment")]Appointment, [Description("ContactUs")] ContactUs }
        
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}