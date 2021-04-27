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
        protected abstract NodoA<T, K> Insert(NodoA<T, K> nodo, T value, K key);
        protected abstract void Delete(NodoA<T, K> nodo);
        protected abstract NodoA<T, K> Get(NodoA<T, K> nodo, K value);
    }
}
