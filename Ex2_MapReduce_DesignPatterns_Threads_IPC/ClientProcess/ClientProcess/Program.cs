using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Communication;

namespace ClientProcess
{


    public class Program
    {
        public static ConcurrentDictionary<string, Stream> streams = new ConcurrentDictionary<string, Stream>();
        private static readonly object balanceLock = new object();


        public static List<Tuple<string, int>> Map(string line)
        {


            MyTask toDo = new MyTask("SPLIT", line);
            Console.WriteLine(toDo);

            var pipeClient = new NamedPipeClientStream(".", "mypipe", PipeDirection.InOut);
            pipeClient.Connect();


            DataExchange dataExt = new DataExchange(pipeClient);

            dataExt.writeStream(toDo);
            MyTask done = (MyTask)dataExt.readStream();

            pipeClient.Close();

            return (List<Tuple<string, int>>)done.outputData;
        }

        public static void Reduce(List<Tuple<string, int>> input)
        {


            foreach (var tuple in input)
            {
                MyTask task = new MyTask("SHUFFLE", tuple);
                Console.WriteLine(task);

                lock (balanceLock)
                {
                    if (!streams.ContainsKey(tuple.Item1))
                    {
                        var pipe = new NamedPipeClientStream(".", "mypipe", PipeDirection.InOut);
                        pipe.Connect();

                        streams.TryAdd(tuple.Item1, pipe);
                    }

                    Stream value;
                    streams.TryGetValue(tuple.Item1, out value);

                    DataExchange dataExt = new DataExchange(value);

                    dataExt.writeStream(task);
                    
                    //Thread.Sleep(50);
                }



            }
        }


        public static void MapReduceIntermediary(object line)
        {
            string line2 = (string)line;
            List<Tuple<string, int>> mapOutput = Map(line2);
            Reduce(mapOutput);

        }

        public static void MapReduce(string txtFileName)
        {
            string[] lines = File.ReadAllLines(txtFileName);
            List<Thread> requests = new List<Thread>();


            foreach (var line in lines)
            {
                string myLine = line.ToLower();
                Thread thread = new Thread(MapReduceIntermediary);
                requests.Add(thread);
                thread.Start(myLine);
            }


            Console.WriteLine("Thread Joining...");
            foreach (Thread req in requests)
            {
                req.Join();
            }

        }


        public static void GetFinalResult(bool displayResult)
        {

            MyTask endMessage = new MyTask("END", null);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("output.txt"))
            {
                foreach (var s in streams)
                {
                    DataExchange dataExt = new DataExchange(s.Value);
                    dataExt.writeStream(endMessage);
                    MyTask done = (MyTask)dataExt.readStream();

                    if (displayResult)
                    {
                        Console.WriteLine(done);
                    }

                    Tuple<string, int> res = (Tuple<string, int>)done.outputData;

                    file.WriteLine(res.Item1 + ", " + res.Item2);

                    s.Value.Close();
                }

                file.Close();
            }




        }


        public static void clearServerConsole()
        {

            MyTask clearTask = new MyTask("CLEAR", null);

            var pipe = new NamedPipeClientStream(".", "mypipe", PipeDirection.InOut);
            pipe.Connect();

            DataExchange dataExt = new DataExchange(pipe);

            dataExt.writeStream(clearTask);
        }

        public static void test()
        {

        }




        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Word Count Program");
            Console.Write("Enter the Name of an Input File >>> ");
            string filename = Console.ReadLine();

            MapReduce(filename);

            GetFinalResult(true);
            Console.WriteLine("---------------------------- WORK DONE ----------------------------");

            Console.WriteLine("Tap Enter to exit...");
            Console.ReadLine();
            clearServerConsole();
        }

    }
}
