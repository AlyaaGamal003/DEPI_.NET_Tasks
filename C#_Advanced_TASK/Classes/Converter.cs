using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    //Create a generic method ConvertList<TSource, TTarget> that converts one list type to another using a converter function.
    internal class Converter
    {
        public static List<TTarget> ConvertList<TSource, TTarget>(List<TSource> Sourse,Func<TSource, TTarget> converter)
        {
            if (Sourse == null)
                        throw new ArgumentNullException(nameof(Sourse));
            List<TTarget> result = new List<TTarget>();
            foreach (var item in Sourse)
            {
                result.Add(converter(item));
            }
            return result;
        }
    }
}
