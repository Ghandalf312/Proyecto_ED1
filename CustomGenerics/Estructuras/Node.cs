using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Estructuras
{
    public class Node<T> : ICloneable
    {
        /// <summary>
        /// Declaración de variables
        /// </summary>
        public Node<T> Father;
        public Node<T> RightSon;
        public Node<T> LeftSon;
        public T Patient;
        public string Key;
        public int Priority;
        public int AgePriority;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Date"></param>
        /// <param name="patient"></param>
        /// <param name="priority"></param>
        public Node(string key, int age, T patient, int priority)
        {
            Key = key;
            AgePriority = age;
            Patient = patient;
            Priority = priority;
        }
        /// <summary>
        /// ICloneable implemetion
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
