using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_CustomQueue_Generics
{
    public class CustomQueue<T>
    {
        public Node<T> start { get; set; }
        public Node<T> end { get; set; }

        public bool isEmptyQueue()
        {
            if (this.start == null && this.end == null)
                return true;
            else
                return false;
        }

        public void enQueue(Node<T> data)
        {

            if (isEmptyQueue())
            {
                this.start = data;
            }
            else
            {
                this.end.next = data;
            }
            this.end = data;
        }

        public Node<T> deQueue()
        {
            Node<T> deQueued = new Node<T>();

            if (!isEmptyQueue())
            {
                deQueued = this.start;
                this.start = this.start.next;

                if (this.start == null)
                    this.end = null;
            }
            return deQueued;
        }

        public void displayQueue()
        {
            Node<T> theEnd = this.end;
            Node<T> theStart = this.start;

            if (!isEmptyQueue())
            {
                while (theStart != theEnd)
                {
                    Console.WriteLine(theStart);
                    theStart = theStart.next;
                }
                Console.WriteLine(theStart);
            }
        }

    }
}
