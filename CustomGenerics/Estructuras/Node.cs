using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Estructuras
{
    public class Node<T>
    {
        public Node<T> Father;
        public Node<T> RightSon;
        public Node<T> LeftSon;

        public string Key;
        public int Priority;
        public int Age;

        //Constructor
        public Node(string key, int priority, int age)
        {
            Key = key;
            Priority = priority;
            Age = age;
            
        }
    }
}
