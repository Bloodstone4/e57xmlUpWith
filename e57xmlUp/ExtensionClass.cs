using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e57xmlUp
{
    public static class ExtensionClass
    {
        public static List<Station> DeleteDuplicates(this IEnumerable<Station> stations)
        {
            List<Station> uniqueCol = new List<Station>();                       
            foreach (var elem in stations)
            {
                if (uniqueCol.Where(x=>x.X==elem.X && x.Y==elem.Y && x.Z==elem.Z).Count()==0)
                {
                    uniqueCol.Add(elem);
                }
            }
            return uniqueCol;
        }

        public static List<Station> Enumerate(this List<Station> stations)
        {
            int count = 1;
            foreach (var elem in stations)
            {
                elem.Number = count;
                count++;
            }
            return stations;
        }
    }
}
