using EnumsNET;
using HomeTask_TopGanLab.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HomeTask_TopGanLab
{
    public static class EnumExtensions
    {
        public static List<SelectListItem> GetEnumDescriptions<T>() where T : IConvertible
        {

            Type type = typeof(T);
            Array values = System.Enum.GetValues(type);

            List<SelectListItem> descriptions = new List<SelectListItem>();

            int i = 0;

            foreach (int val in values)
            {

                var memInfo = type.GetMember(type.GetEnumName(val));
                var attrs = memInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    descriptions.Add(new SelectListItem { Text = ((DescriptionAttribute)attrs[0]).Description, Value = i.ToString() });

                    i++;
                }


            }

            return descriptions;
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }


    }
}
