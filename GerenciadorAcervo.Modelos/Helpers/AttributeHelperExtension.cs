using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Helpers
{
    public static class AttributeHelperExtension
    {
        public static string ToDescription(this Enum value)
        {
            var atributo = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return atributo.Length > 0 ? atributo[0].Description : value.ToString();
        }
    }
}
