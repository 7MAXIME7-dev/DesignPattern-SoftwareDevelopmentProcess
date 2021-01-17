using System;
using System.IO.Pipes;
using System.Threading;
using System.Collections.Generic;
using Communication;

namespace ServerProcess
{

    class Program
    {
        static int numberOfInstance = 254;
        private static readonly object balanceLock = new object();

        public static void runServer()
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("mypipe", PipeDirection.InOut, numberOfInstance);
            
            pipeServer.WaitForConnection();
            Console.WriteLine("Connected to Server.");


            DataExchange dataExt = new DataExchange(pipeServer);

            MyTask inProgress = (MyTask)dataExt.readStream();

            Console.WriteLine(inProgress);


            if (inProgress.taskName == "SPLIT") 
            {
                inProgress.outputData = Map((string)inProgress.inputData);
                inProgress.taskName = "SPLIT RESULT";
                dataExt.writeStream(inProgress);
            }

            else if(inProgress.taskName == "SHUFFLE") 
            {
                Reduce(inProgress, dataExt);
            }

            else if(inProgress.taskName == "CLEAR") 
            { 
                Console.Clear(); Console.WriteLine("\n\nwaiting for connection...");                               
            }


        }


        public static List<Tuple<string, int>> Map(string input)
        {
            string[] delimiterString = { ", ", ". ", ": ", ",", ".", ":", " (", ") ", "(", ")", "-", " " };
            List<Tuple<string, int>> output = new List<Tuple<string, int>>();
            string[] data = input.Split(delimiterString, StringSplitOptions.None);
            foreach(string d in data){ output.Add(new Tuple<string, int>(d,1)); }
            return output;
        }


        public static void Reduce(MyTask inProgress, DataExchange dataExt)
        {
            Tuple<string, int> newTuple = (Tuple<string, int>)inProgress.inputData;

            while (inProgress.taskName != "END")
            {

                inProgress = (MyTask)dataExt.readStream();
                Console.WriteLine(inProgress);

                if (inProgress.taskName != "END")
                {
                    newTuple = new Tuple<string, int>(newTuple.Item1, 1 + newTuple.Item2);
                }
            }

            MyTask output = new MyTask("RESULT", null);
            output.outputData = newTuple;
            Console.WriteLine(output);

            dataExt.writeStream(output);

        }





        public static void Main(string[] args)
        {

            Thread[] servers = new Thread[numberOfInstance];

            for (int i = 0; i < servers.Length; i++)
            {               
                Thread t_server = new Thread(runServer);
                t_server.Start();
                Console.WriteLine("Server n°{0} : Started", i+1);
            }

            Console.WriteLine("waiting for connection...");

            Console.ReadLine();
        }



    }

}
