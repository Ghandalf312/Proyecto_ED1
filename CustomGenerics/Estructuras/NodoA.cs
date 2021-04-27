using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Threading.Tasks;


namespace CustomGenerics.Estructuras
{
    public class NodoA<T, K> where T : IComparable<T> where K : IComparable<K>
    {
        public NodoA<T, K> Izquierdo { get; set; }
        public NodoA<T, K> Derecho { get; set; }
        public T Valor { get; set; }
        public K Llave { get; set; }
    }
}
