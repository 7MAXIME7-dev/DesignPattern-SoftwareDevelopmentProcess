using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_CustomQueue_Generics
{
    public class Node<T>
    {
        public T data { get; set; }
        public Node<T> next { get; set; }

        public Node(T val)
        {
            data = val;
            next = null;
        }

        public Node()
        {

        }

        public override string ToString()
        {
            return Convert.ToString(this.data);
        }

    }
}
