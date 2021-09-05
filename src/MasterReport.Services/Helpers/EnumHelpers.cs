using System;

namespace MasterReport.Services.Helpers
{
    public static class EnumHelpers
    {
        /*
         Color colorEnum = "Red".ToEnum<Color>();

        string color = "Red";
        var colorEnum = color.ToEnum<Color>();
        */
        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }
    }
}