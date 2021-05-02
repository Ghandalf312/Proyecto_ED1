using System;
using System.Collections.Generic;
using System.Text;


using CustomGenerics.Estructuras;
using System.Linq;
using System.Threading.Tasks;



namespace CustomGenerics.Estructuras
{
    public class Hash<T> where T : IComparable<T>
    {
        /// <summary>
        /// Declaración de variables
        /// </summary>
        public int Length;
        public HashNode<T>[] HashTable;

        /// <summary>
        /// Constructor, establece el tamaño del arreglo en la hash
        /// </summary>
        /// <param name="length"></param> Tamaño del arreglo
        public Hash(int length)
        {
            Length = length;
            HashTable = new HashNode<T>[Length];
        }

        /// <summary>
        /// Iserta un nuevo nodo en la tabla hash
        /// </summary>
        /// <param name="InsertV"></param> El valor del nodo
        /// <param name="key"></param> Llave utilizada para agregar a la tabla hash
        public void Insert(T InsertV, string key)
        {
            HashNode<T> T1 = new HashNode<T>();
            T1.Value = InsertV;
            T1.Key = key;
            int code = GetCode(T1.Key);
            if (HashTable[code] != null)
            {
                HashNode<T> Aux = HashTable[code];
                while (Aux.Next != null)
                {
                    Aux = Aux.Next;
                }
                Aux.Next = T1;
                T1.Previous = Aux;
            }
            else
            {
                HashTable[code] = T1;
            }
        }
        /// <summary>
        /// Segundo tipo de inserción
        /// </summary>
        /// <param name="InsertV"></param> Valor del nodo.
        /// <param name="key"></param> Llave utilizada para insertar el nodo
        /// <param name="multiplier"></param> Número utilizado para establecer el rango utilizado para la serie.
        public void Insert(T InsertV, string key, int multiplier)
        {
            HashNode<T> T1 = new HashNode<T>();
            T1.Value = InsertV;
            T1.Key = key;
            int Originalcode = GetCode(T1.Value ,T1.Key, multiplier);
            int code = Originalcode;
            if (HashTable[code] != null)
            {
                while (HashTable[code] != null)
                {
                    if (code >= (multiplier + 1) * 10)
                    {
                        code = multiplier * 10;
                    }
                    else
                    {
                        code += 1;
                    }
                }
                if (HashTable[code] == null)
                {
                    HashTable[code] = T1;
                }
            }
            else
            {
                HashTable[code] = T1;
            }
        }

        /// <summary>
        /// Función que busca un nodo en la tabla hash
        /// </summary>
        /// <param name="searchedKey"></param> Llave utilizada para buscar el elemento.
        /// <param name="multiplier"></param> Número utilizado para establecer el rango utilizado para la serie.
        /// <returns></returns>
        public HashNode<T> Search(string searchedKey, int multiplier)
        {
            int Originalcode = GetCode(searchedKey, multiplier);
            int code = Originalcode;
            bool Isfound = false;
            while (!Isfound)
            {
                if (HashTable[code] != null)
                {
                    if (searchedKey != HashTable[code].Key)
                    {
                        if (code >= (multiplier + 1) * 10)
                        {
                            code = multiplier * 10;
                        }
                        else
                        {
                            code += 1;
                        }
                        if (code == Originalcode)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        Isfound = true;
                    }

                }
                else
                {
                    code += 1;
                    if (code == Originalcode)
                    {
                        return null;
                    }
                }
            }
            return HashTable[code];

        }

        /// <summary>
        /// Otro tipo de buscar
        /// </summary>
        /// <param name="searchedKey"></param> Llave que se quire buscar
        /// <returns></returns>
        public HashNode<T> Search(string searchedKey)
        {
            int code = GetCode(searchedKey);

            if (HashTable[code] != null)
            {

                if (HashTable[code].Key != searchedKey)
                {
                    HashNode<T> Aux = HashTable[code];
                    while (Aux.Key != searchedKey && Aux.Next != null)
                    {
                        Aux = Aux.Next;
                    }
                    if (Aux.Key == searchedKey)
                    {
                        return Aux;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return HashTable[code];
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Función de elimnar
        /// </summary>
        /// <param name="searchedKey"></param> Llave necesaria para poder eliminar el nodo.
        /// <param name="multiplier"></param> Número utilizado para establecer el rango utilizado para la serie.
        public void Delete(T value, string searchedKey, int multiplier)
        {
            int code = GetCode(value, searchedKey, multiplier);
            bool found = false;

            while (HashTable[code] == null)
            {
                code++;
            }
            while (found == false)
            {
                if (HashTable[code].Key != null)
                {
                    if (HashTable[code].Key != searchedKey)
                    {
                        if (code >= (multiplier + 1) * 1)
                        {
                            code = multiplier * 1;
                        }
                        else
                        {
                            code += 1;
                        }
                    }
                    else
                    {
                        HashTable[code] = null;
                        found = true;
                    }
                }
                else
                {
                    if (code >= (multiplier + 1) * 1)
                    {
                        code = multiplier * 1;
                    }
                    else
                    {
                        code += 1;
                    }
                }
            }
        }
        /// <summary>
        /// Consigue la posición para insertar el nodo a la tabla hash
        /// </summary>
        /// <param name="Key"></param> Llave utilizada para conseguir la posición.
        /// <returns></returns>
        private int GetCode(string Key)
        {
            int length = Key.Length;
            int code = 0;
            for (int i = 0; i < length; i++)
            {
                code += Convert.ToInt32(Key.Substring(i, 1));
            }
            code = (code * 7) % Length;
            return code;
        }

        /// <summary>
        /// Segunda versión de consguir la posición.
        /// </summary>
        /// <param name="Key"></param> Llave utilizada para conseguir la posición.
        /// <param name="Multiplier"></param> Número utilizado para establecer el rango utilizado para la serie
        private int GetCode(string Key, int Multiplier)
        {
            int code = Key.Length * 11 % (Multiplier * 10);
            while (code < Multiplier * 10)
            {
                if (code >= (Multiplier * 10))
                {
                    code -= 10;
                }
                else
                {
                    if (HashTable[code] != null)
                    {
                        code += 1;
                    }
                    else
                    {
                        return code;
                    }
                }
            }
            return code;
        }

        /// <summary>
        /// Consigue la posición para insertar el nodo a la tabla hash
        /// </summary>
        /// <param name="value"></param> Es el valor buscado.
        /// <param name="Key"></param> Representa la clave que da acceso a una posición en eñ arreglo en la que debería estar el valor.
        /// <param name="Multiplier"></param> Reduce la sección en la que se busca el valor en toda la matriz.

        /// <returns></returns>
        private int GetCode(T value, string Key, int Multiplier)
        {
            int code = Key.Length * 11 % (Multiplier * 10);
            while (code < Multiplier * 10)
            {
                if (code >= (Multiplier * 10))
                {
                    code -= 10;
                }
                else
                {
                    if (HashTable[code] != null)
                    {
                        if (HashTable[code].Value.CompareTo(value) == 0)
                        {
                            return code;
                        }
                        else
                        {
                            code += 1;
                        }
                    }
                    else
                    {
                        return code;
                    }
                }
            }
            return code;
        }


        /// <summary>
        /// Devuelve una lista de nodos de todo los elementos de la tabla hash.
        /// </summary>
        public List<HashNode<T>> GetAsNodes()
        {
            var returnList = new List<HashNode<T>>();
            var currentNode = new HashNode<T>();
            foreach (var task in HashTable)
            {
                currentNode = task;
                while (currentNode != null)
                {
                    returnList.Add(currentNode);
                    currentNode = currentNode.Next;
                }
            }
            return returnList;
        }
    }
}

