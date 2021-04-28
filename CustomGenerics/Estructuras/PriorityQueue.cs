using CustomGenerics.Estructuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics    
{
    public class PriorityQueue<T> : ICloneable
    {
        //Interface referenciada de: https://stackoverflow.com/questions/6569486/creating-a-copy-of-an-object-in-c-sharp
        public Node<T> Root;
        public int tasksQuantity;
        public int PatientsNumber;

        public PriorityQueue()
        {
            tasksQuantity = 0;
        }

        public bool IsEmpty()
        {
            return Root == null ? true : false;
        }

        public bool IsFull()
        {
            return tasksQuantity == 10 ? true : false;
        }

        public void AddPatient(string key,  int priority, int age)
        {
            var newNode = new Node<T>(key, priority, age);
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

        private Node<T> SearchLastNode(Node<T> current, int number)
        {
            try
            {
                int previousn = tasksQuantity;
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
                    //Segundo criterio prioridad
                    if (current.Age < current.Father.Age)
                    {
                        ChangeNodes(current);
                    }
                }
                OrderDowntoUp(current.Father);
            }
        }
        private void ChangeNodes(Node<T> node)
        {
            var Priority1 = node.Priority;
            var Key1 = node.Key;
            var Date1 = node.Age;
            node.Priority = node.Father.Priority;
            node.Key = node.Father.Key;
            node.Age = node.Father.Age;
            node.Father.Priority = Priority1;
            node.Father.Key = Key1;
            node.Father.Age = Date1;
        }
        public Node<T> Delete()
        {
            Node<T> LastNode = SearchLastNode(Root, 1);
            Node<T> FirstNode = Root;
            Root.Key = LastNode.Key;
            Root.Priority = LastNode.Priority;
            if (LastNode.Father == null)
            {
                Root = null;
                tasksQuantity--;
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
            tasksQuantity--;
            return FirstNode;
        }

        private void OrderUptoDown(Node<T> current)
        {
            if (current.RightSon != null && current.LeftSon != null)
            {
                if (current.LeftSon.Priority > current.RightSon.Priority)
                {
                    if (current.Priority > current.RightSon.Priority)
                    {
                        ChangeNodes(current.RightSon);
                        OrderDowntoUp(current.RightSon);
                    }
                    else if (current.Priority == current.RightSon.Priority)
                    {
                        if (current.Age > current.RightSon.Age)
                        {
                            ChangeNodes(current.RightSon);
                            OrderDowntoUp(current.RightSon);
                        }
                    }
                }
                else if (current.LeftSon.Priority < current.RightSon.Priority)
                {
                    if (current.Priority > current.LeftSon.Priority)
                    {
                        ChangeNodes(current.LeftSon);
                        OrderDowntoUp(current.LeftSon);
                    }
                    else if (current.Priority == current.LeftSon.Priority)
                    {
                        if (current.Age > current.LeftSon.Age)
                        {
                            ChangeNodes(current.LeftSon);
                            OrderDowntoUp(current.LeftSon);
                        }
                    }
                }
                else
                {
                    if (current.LeftSon.Age > current.RightSon.Age)
                    {
                        if (current.Priority > current.RightSon.Priority)
                        {
                            ChangeNodes(current.RightSon);
                            OrderDowntoUp(current.RightSon);
                        }
                        else if (current.Priority == current.RightSon.Priority)
                        {
                            if (current.Age > current.RightSon.Age)
                            {
                                ChangeNodes(current.RightSon);
                                OrderDowntoUp(current.RightSon);
                            }
                        }
                    }
                    else
                    {
                        if (current.Priority > current.LeftSon.Priority)
                        {
                            ChangeNodes(current.LeftSon);
                            OrderDowntoUp(current.LeftSon);
                        }
                        else if (current.Priority == current.LeftSon.Priority)
                        {
                            if (current.Age > current.LeftSon.Age)
                            {
                                ChangeNodes(current.LeftSon);
                                OrderDowntoUp(current.LeftSon);
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
                    OrderDowntoUp(current.RightSon);
                }
                else if (current.Priority == current.RightSon.Priority)
                {
                    if (current.Age > current.RightSon.Age)
                    {
                        ChangeNodes(current.RightSon);
                        OrderDowntoUp(current.RightSon);
                    }
                }
            }
            else if (current.LeftSon != null)
            {
                if (current.Priority > current.LeftSon.Priority)
                {
                    ChangeNodes(current.LeftSon);
                    OrderDowntoUp(current.LeftSon);
                }
                else if (current.Priority == current.LeftSon.Priority)
                {
                    if (current.Age > current.LeftSon.Age)
                    {
                        ChangeNodes(current.LeftSon);
                        OrderDowntoUp(current.LeftSon);
                    }
                }
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
