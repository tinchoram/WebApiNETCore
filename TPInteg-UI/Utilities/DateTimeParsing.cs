using System;

namespace TPInteg_UI.Utilities
{
    public static class DateTimeParsing
    {
        public static DateTime DateTimeFromString(string dataString) 
        {
            if (!string.IsNullOrEmpty(dataString))
            {
                string[] fecha = dataString.Contains("-") ? dataString.Split('-') :
                    dataString.Contains("/") ? dataString.Split('/') :
                    dataString.Split('.');

                return new DateTime(int.Parse(fecha[2]), int.Parse(fecha[1]), int.Parse(fecha[0]));
            }
            return DateTime.Now;
        }
    }
}