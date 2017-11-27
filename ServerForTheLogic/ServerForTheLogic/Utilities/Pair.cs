using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Utilities
{
    /// <summary>
    /// Basic Pair class used in project.
    /// </summary>
    /// <para>Written by Connor Goudie </para>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public class Pair<T,R>
    {
        public T First { get; set; }
        public R Second { get; set; }
        public Pair(T first, R second)
        {
            First = first;
            Second = second;
        }
    }
}
