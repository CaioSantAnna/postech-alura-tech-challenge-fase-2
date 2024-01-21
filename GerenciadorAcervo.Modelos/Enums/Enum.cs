using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorAcervo.Modelos.Enums
{
    public class Enum<T> where T : Enum
    {
        public static IEnumerable<T> BuscarValoresComoIEnumerable()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
