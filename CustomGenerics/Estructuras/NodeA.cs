using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Threading.Tasks;


namespace CustomGenerics.Estructuras
{
    public class NodeA<T, K> where T : IComparable<T> where K : IComparable<K>
    {
        public NodeA<T, K> Izquierdo { get; set; }
        public NodeA<T, K> Derecho { get; set; }
        public T Valor { get; set; }
        public K Llave { get; set; }
    }
}
