using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Estructuras   
{
    public class PriorityQueue<T> : ICloneable, IEnumerable<T>
    {
        /// <summary>
        /// Variable declaration.
        /// </summary>
        public Node<T> Root;
        public int PatientsNumber;
        public PriorityQueue<T> queueCopy;

        /// <summary>
        /// Constructor, estableciendo el numero de pacientes como 0.
        /// </summary>
        public PriorityQueue()
        {
            PatientsNumber = 0;
        }

        /// <summary>
        /// Retorna true si la raiz esta vacia.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return Root == null ? true : false;
        }

        /// <summary>
        /// Retorna true si el numero de paciente es de 10.
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return PatientsNumber == 10 ? true : false;
        }

        /// <summary>
        /// Agrega un nuevo paciente a la cola de prioridad.
        /// </summary>
        /// <param name="key"></param> Representa la llave del nodo
        /// <param name="age"></param> Representa la edad como segundo criterio de la prioridad.
        /// <param name="patient"></param> Representa el valor insertado a la cola.
        /// <param name="priority"></param> Representa la prioridad del paciente.
        public void AddPatient(string key, int age, T patient, int priority)
        {
            var newNode = new Node<T>(key, age, patient, priority);
            if (IsEmpty())
            {
                Root = newNode;
                PatientsNumber = 1;
            }
            else
            {
                PatientsNumber++;
                var NewNodeFather = SearchLastNode(Root, 1);
                if (NewNodeFather.LeftSon != null)
                {
                    NewNodeFather.RightSon = newNode;
                    newNode.Father = NewNodeFather;
                    OrderDowntoUp(newNode);
                }
                else
                {
                    NewNodeFather.LeftSon = newNode;
                    newNode.Father = NewNodeFather;
                    OrderDowntoUp(newNode);
                }

            }
        }
        /// <summary>
        /// Compara prioridades y cambia si es necesario, desde el nodo a evaluar hacia la raiz
        /// </summary>
        /// <param name="current"></param> Es el nodo a evaluar
        private void OrderDowntoUp(Node<T> current)
        {
            if (current.Father != null)
            {
                if (current.Priority < current.Father.Priority)
                {
                    ChangeNodes(current);
                }
                else if (current.Priority == current.Father.Priority)
                {
                    if (current.AgePriority > current.Father.AgePriority)
                    {
                        ChangeNodes(current);
                    }
                }
                OrderDowntoUp(current.Father);
            }
        }
        /// <summary>
        /// Compara prioridades y cambia si es necesario, desde la raiz hasta las hojas
        /// </summary>
        /// <param name="current"></param> Es el nodo a evaluar
        private void OrderUptoDown(Node<T> current)
        {
            if (current.RightSon != null && current.LeftSon != null)
            {
                if (current.LeftSon.Priority > current.RightSon.Priority)
                {
                    if (current.Priority > current.RightSon.Priority)
                    {
                        ChangeNodes(current.RightSon);
                        OrderUptoDown(current.RightSon);
                    }
                    else if (current.Priority == current.RightSon.Priority)
                    {
                        if (current.AgePriority < current.RightSon.AgePriority)
                        {
                            ChangeNodes(current.RightSon);
                            OrderUptoDown(current.RightSon);
                        }
                    }
                }
                else if (current.LeftSon.Priority < current.RightSon.Priority)
                {
                    if (current.Priority > current.LeftSon.Priority)
                    {
                        ChangeNodes(current.LeftSon);
                        OrderUptoDown(current.LeftSon);
                    }
                    else if (current.Priority == current.LeftSon.Priority)
                    {
                        if (current.AgePriority < current.LeftSon.AgePriority)
                        {
                            ChangeNodes(current.LeftSon);
                            OrderUptoDown(current.LeftSon);
                        }
                    }
                }
                else
                {
                    if (current.LeftSon.AgePriority < current.RightSon.AgePriority)
                    {
                        if (current.Priority > current.RightSon.Priority)
                        {
                            ChangeNodes(current.RightSon);
                            OrderUptoDown(current.RightSon);
                        }
                        else if (current.Priority == current.RightSon.Priority)
                        {
                            if (current.AgePriority < current.RightSon.AgePriority)
                            {
                                ChangeNodes(current.RightSon);
                                OrderUptoDown(current.RightSon);
                            }
                        }
                    }
                    else
                    {
                        if (current.Priority > current.LeftSon.Priority)
                        {
                            ChangeNodes(current.LeftSon);
                            OrderUptoDown(current.LeftSon);
                        }
                        else if (current.Priority == current.LeftSon.Priority)
                        {
                            if (current.AgePriority < current.LeftSon.AgePriority)
                            {
                                ChangeNodes(current.LeftSon);
                                OrderUptoDown(current.LeftSon);
                            }
                        }
                    }
                }
            }
            else if (current.RightSon != null)
            {
                if (current.Priority > current.RightSon.Priority)
                {
                    ChangeNodes(current.RightSon);
                    OrderUptoDown(current.RightSon);
                }
                else if (current.Priority == current.RightSon.Priority)
                {
                    if (current.AgePriority < current.RightSon.AgePriority)
                    {
                        ChangeNodes(current.RightSon);
                        OrderUptoDown(current.RightSon);
                    }
                }
            }
            else if (current.LeftSon != null)
            {
                if (current.Priority > current.LeftSon.Priority)
                {
                    ChangeNodes(current.LeftSon);
                    OrderUptoDown(current.LeftSon);
                }
                else if (current.Priority == current.LeftSon.Priority)
                {
                    if (current.AgePriority < current.LeftSon.AgePriority)
                    {
                        ChangeNodes(current.LeftSon);
                        OrderUptoDown(current.LeftSon);
                    }
                }
            }
        }
        /// <summary>
        /// Intercambia el nodo actual con el padre.
        /// </summary>
        /// <param name="node"></param> The node being exchangedEs el nodo que se intercambia
        private void ChangeNodes(Node<T> node)
        {
            var Priority1 = node.Priority;
            var Key1 = node.Key;
            var Age1 = node.AgePriority;
            var Patient1 = node.Patient;
            node.Priority = node.Father.Priority;
            node.Key = node.Father.Key;
            node.AgePriority = node.Father.AgePriority;
            node.Patient = node.Father.Patient;
            node.Father.Priority = Priority1;
            node.Father.Key = Key1;
            node.Father.AgePriority = Age1;
            node.Father.Patient = Patient1;

        }
        /// <summary>
        /// Remueve el primer valor de la cola.
        /// </summary>
        /// <returns></returns>
        public Node<T> GetFirst()
        {
            if (Root == null)
            {
                return null;
            }
            Node<T> LastNode = SearchLastNode(Root, 1);
            Node<T> FirstNode = (Node<T>)Root.Clone();
            var LastNodeCopy = (Node<T>)LastNode.Clone();
            Root.Key = LastNodeCopy.Key;
            Root.Priority = LastNodeCopy.Priority;
            Root.Patient = LastNodeCopy.Patient;
            Root.AgePriority = LastNodeCopy.AgePriority;
            if (LastNode.Father == null)
            {
                Root = null;
                PatientsNumber--;
                return LastNode;
            }
            else
            {
                if (LastNode.Father.LeftSon == LastNode)
                {
                    LastNode.Father.LeftSon = null;
                }
                else
                {
                    LastNode.Father.RightSon = null;
                }
            }
            OrderUptoDown(Root);
            PatientsNumber--;
            return Root;
        }
        /// <summary>
        /// Busca el ultimo insertado en la cola de prioridad.
        /// </summary>
        /// <param name="current"></param> Es el nodo actual a evaluar.
        /// <param name="number"></param> Numero total de elementos.
        /// <returns></returns>
        private Node<T> SearchLastNode(Node<T> current, int number)
        {
            try
            {
                int previousn = PatientsNumber;
                if (previousn == number)
                {
                    return current;
                }
                else
                {
                    while (previousn / 2 != number)
                    {
                        previousn = previousn / 2;
                    }
                    if (previousn % 2 == 0)
                    {
                        if (current.LeftSon != null)
                        {
                            return SearchLastNode(current.LeftSon, previousn);
                        }
                        else
                        {
                            return current;
                        }
                    }
                    else
                    {
                        if (current.RightSon != null)
                        {
                            return SearchLastNode(current.RightSon, previousn);
                        }
                        else
                        {
                            return current;
                        }
                    }
                }
            }
            catch
            {
                return current;
            }

        }
        /// <summary>
        /// Implementacion del Icloneable, clona la cola de prioridad
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Implementacion IEnumerator 
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            var queueCopy = new PriorityQueue<T>() { Root = this.Root, PatientsNumber = this.PatientsNumber };
            var current = queueCopy.Root;
            while (current != null)
            {
                yield return current.Patient;
                current = queueCopy.GetFirst();
            }
        }
        /// <summary>
        /// IEnumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
