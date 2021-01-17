using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_CustomQueue_Generics
{
    public class Program
    {
        static void exercice1()
        {
            Console.WriteLine("--- Generic String Queue ---");
            CustomQueue<string> myQueue = new CustomQueue<string>();

            Node<string> lastDequeue = new Node<string>("None");


            while (true)
            {
                Console.WriteLine("\n\nMy Queue : ");
                myQueue.displayQueue();

                Console.Write("\n\nEnqueue an element (Type exit to stop) >>> ");
                string temp = Console.ReadLine();
                if (temp == "exit") { break; }
                myQueue.enQueue(new Node<string>(temp));

                Console.Clear();
            }

            Console.Clear();

            while (true)
            {
                Console.WriteLine("\nDequeued element : " + lastDequeue);

                Console.WriteLine("\n\nMy Queue : ");
                myQueue.displayQueue();

                Console.WriteLine("\nDequeue an element : (Click Enter to Dequeue | Type exit to stop)");
                string temp = Console.ReadLine();
                if (temp == "exit") { break; }

                lastDequeue = myQueue.deQueue();

                Console.Clear();
            }
        }

        public static void test()
        {

        }

        static void Main(string[] args)
        {
            exercice1();
            Console.ReadLine();
        }
    }
}
