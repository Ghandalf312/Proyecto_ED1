using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomGenerics.Estructuras;


namespace CustomGenerics.Interfaces
{
    public abstract class NonLinearDataStructureBase<T, K> where T : IComparable<T> where K : IComparable<K>
    {
        protected abstract NodeA<T, K> Insert(NodeA<T, K> nodo, T value, K key);
        protected abstract void Delete(NodeA<T, K> nodo);
        protected abstract NodeA<T, K> Get(NodeA<T, K> nodo, K value);
    }
}
