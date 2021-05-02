using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Threading.Tasks;



namespace CustomGenerics.Estructuras
{
    public class HashNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// Declaración de variables
        /// </summary>
        public HashNode<T> Previous { get; set; }
        public HashNode<T> Next { get; set; }
        public T Value { get; set; }
        public string Key { get; set; }
    }
}
