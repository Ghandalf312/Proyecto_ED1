using System;
using System.Collections.Generic;
using System.Text;
using CustomGenerics.Interfaces;
namespace CustomGenerics.Estructuras
{
    public class AVLTree<T, K> : NonLinearDataStructureBase<T, K> where T : IComparable<T> where K : IComparable<K>
    {
        private NodeA<T, K> Raiz = new NodeA<T, K>();
        private NodeA<T, K> temp = new NodeA<T, K>();
        private List<T> listaOrdenada = new List<T>();
        private List<T> ListaBusqueda = new List<T>();

        /// <summary>
        /// Recibe un valor y una llave
        /// </summary>
        /// <param name="value">Valor de tipo T</param>
        /// <param name="key">Llave de tipo K</param>
        public void Add(T value, K key)
        {
            Insert(Raiz, value, key);
        }
        public void Remove(K deleted)
        {
            NodeA<T, K> busc = new NodeA<T, K>();
            busc = Get(Raiz, deleted);
            if (busc != null)
            {
                Delete(busc);
            }
        }
        public T Buscar(K buscado)
        {
            NodeA<T, K> busc = new NodeA<T, K>();
            busc = Get(Raiz, buscado);
            if (busc != null)
            {
                return busc.Valor;
            }
            return default(T);
        }

        public List<T> ObtenerLista()
        {
            listaOrdenada.Clear();
            InOrder(Raiz);
            return listaOrdenada;
        }
        public List<T> ObtenerListaPost()
        {
            listaOrdenada.Clear();
            PostOrder(Raiz);
            return listaOrdenada;
        }
        public List<T> ObtenerListaPre()
        {
            listaOrdenada.Clear();
            PreOrder(Raiz);
            return listaOrdenada;
        }

        // tres casos para eliminacion
        protected override void Delete(NodeA<T, K> nodo)
        {
            if (nodo.Izquierdo.Valor == null && nodo.Derecho.Valor == null) // Caso 1
            {
                nodo.Valor = nodo.Derecho.Valor;
                nodo.Llave = nodo.Derecho.Llave;
            }
            else if (nodo.Derecho.Valor == null) // Caso 2
            {
                nodo.Valor = nodo.Izquierdo.Valor;
                nodo.Llave = nodo.Izquierdo.Llave;
                nodo.Derecho = nodo.Izquierdo.Derecho;
                nodo.Izquierdo = nodo.Izquierdo.Izquierdo;
            }
            else // Caso 3
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    temp = Derecha(nodo.Izquierdo);
                }
                else
                {
                    temp = Derecha(nodo);
                }
                nodo.Valor = temp.Valor;
                nodo.Llave = temp.Llave;
            }
            BalanceoPost(Raiz);
        }
        // Metodo ayuda para el caso 3 de eliminacion
        private NodeA<T, K> Derecha(NodeA<T, K> nodo)
        {
            if (nodo.Derecho.Valor == null)
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    return Derecha(nodo.Izquierdo);
                }
                else
                {
                    NodeA<T, K> temporal = new NodeA<T, K>();
                    temporal.Valor = nodo.Valor;
                    temporal.Llave = nodo.Llave;
                    nodo.Valor = nodo.Derecho.Valor;
                    return temporal;
                }
            }
            else
            {
                return Derecha(nodo.Derecho);
            }
        }

        // Busqueda recursiva de un valor dentro del arbol por llave del valor
        protected override NodeA<T, K> Get(NodeA<T, K> nodo, K value)
        {
            if (value.CompareTo(nodo.Llave) == 0)
            {
                return nodo;
            }
            else if (value.CompareTo(nodo.Llave) == -1)
            {
                if (nodo.Izquierdo == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Izquierdo, value);
                }
            }
            else
            {
                if (nodo.Derecho == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Derecho, value);
                }
            }
        }

        // Metodo recursivo para incertar segun orden alfabetico

        protected override NodeA<T, K> Insert(NodeA<T, K> nodo, T value, K key)
        {
            try
            {
                if (nodo.Valor == null)
                {
                    nodo.Valor = value;
                    nodo.Llave = key;
                    nodo.Derecho = new NodeA<T, K>();
                    nodo.Izquierdo = new NodeA<T, K>();
                }
                else if (key.CompareTo(nodo.Llave) == -1)
                {
                    nodo.Izquierdo = Insert(nodo.Izquierdo, value, key);
                    nodo = Balancear(nodo);
                }
                else if (key.CompareTo(nodo.Llave) == 1)
                {
                    nodo.Derecho = Insert(nodo.Derecho, value, key);
                    nodo = Balancear(nodo);
                }
                return nodo;
            }
            catch
            {
                throw;
            }
        }

        // Recorre la lista en orden y agrega los valores a la listaOrdenada
        private void InOrder(NodeA<T, K> nodo)
        {
            if (nodo.Valor != null)
            {
                InOrder(nodo.Izquierdo);
                listaOrdenada.Add(nodo.Valor);
                InOrder(nodo.Derecho);
            }
        }
        private void PostOrder(NodeA<T, K> nodo)
        {
            if (nodo.Valor != null)
            {
                PostOrder(nodo.Izquierdo);
                PostOrder(nodo.Derecho);
                listaOrdenada.Add(nodo.Valor);
            }
        }
        private void PreOrder(NodeA<T, K> nodo)
        {
            if (nodo.Valor != null)
            {
                listaOrdenada.Add(nodo.Valor);
                PreOrder(nodo.Izquierdo);
                PreOrder(nodo.Derecho);
            }
        }
        private void BalanceoPost(NodeA<T, K> nodo)
        {
            if (nodo.Valor != null)
            {
                BalanceoPost(nodo.Izquierdo);
                BalanceoPost(nodo.Derecho);
                Balancear(nodo);
            }
        }


        private int getHeight(NodeA<T, K> node)
        {
            if (node.Valor == null) return -1;
            var IzquierdoH = getHeight(node.Izquierdo);
            var rightH = getHeight(node.Derecho);
            return Math.Max(IzquierdoH, rightH) + 1;
        }

        private int FactorEquilibrio(NodeA<T, K> nodoActual)
        {
            int iz = getHeight(nodoActual.Izquierdo);
            int der = getHeight(nodoActual.Derecho);
            int FactorE = iz - der;
            return FactorE;
        }

        private NodeA<T, K> Balancear(NodeA<T, K> nodoActual)
        {
            int factorE = FactorEquilibrio(nodoActual);
            if (factorE > 1)
            {
                if (FactorEquilibrio(nodoActual.Izquierdo) > 0)
                {
                    nodoActual = RotacionDer(nodoActual);
                }
                else
                {
                    nodoActual = RotacionDobDer(nodoActual);
                }
            }
            else if (factorE < -1)
            {
                if (FactorEquilibrio(nodoActual.Derecho) > 0)
                {
                    nodoActual = RotacionDobIzq(nodoActual);
                }
                else
                {
                    nodoActual = RotacionIzq(nodoActual);
                }
            }
            return nodoActual;
        }

        private NodeA<T, K> RotacionIzq(NodeA<T, K> nodoActual)
        {
            var temp = new NodeA<T, K>
            {
                Valor = nodoActual.Derecho.Valor,
                Llave = nodoActual.Derecho.Llave,
                Izquierdo = nodoActual.Derecho.Izquierdo,
                Derecho = nodoActual.Derecho.Derecho
            };
            nodoActual.Derecho = temp.Izquierdo;
            temp.Izquierdo = nodoActual;

            if (nodoActual.Valor.CompareTo(Raiz.Valor) == 0)
            {
                Raiz = temp;
            }
            return temp;
        }
        private NodeA<T, K> RotacionDer(NodeA<T, K> nodoActual)
        {
            var temp = new NodeA<T, K>
            {
                Valor = nodoActual.Izquierdo.Valor,
                Llave = nodoActual.Izquierdo.Llave,
                Izquierdo = nodoActual.Izquierdo.Izquierdo,
                Derecho = nodoActual.Izquierdo.Derecho
            };
            nodoActual.Izquierdo = temp.Derecho;
            temp.Derecho = nodoActual;

            if (nodoActual.Valor.CompareTo(Raiz.Valor) == 0)
            {
                Raiz = temp;
            }
            return temp;
        }
        private NodeA<T, K> RotacionDobDer(NodeA<T, K> nodoActual)
        {
            var temp = new NodeA<T, K>
            {
                Valor = nodoActual.Izquierdo.Valor,
                Llave = nodoActual.Izquierdo.Llave,
                Izquierdo = nodoActual.Izquierdo.Izquierdo,
                Derecho = nodoActual.Izquierdo.Derecho
            };
            nodoActual.Izquierdo = RotacionIzq(temp);
            return RotacionDer(nodoActual);
        }
        private NodeA<T, K> RotacionDobIzq(NodeA<T, K> nodoActual)
        {
            var temp = new NodeA<T, K>
            {
                Valor = nodoActual.Derecho.Valor,
                Llave = nodoActual.Derecho.Llave,
                Izquierdo = nodoActual.Derecho.Izquierdo,
                Derecho = nodoActual.Derecho.Derecho
            };
            nodoActual.Derecho = RotacionDer(temp);
            return RotacionIzq(nodoActual);
        }

    }
}
