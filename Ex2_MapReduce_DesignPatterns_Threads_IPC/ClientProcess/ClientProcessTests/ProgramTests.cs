using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System;

namespace ClientProcess.Tests
{
    [TestClass()]
    public class ProgramTests
    {

        public static bool EqualTuple(List<Tuple<string, int>> t1, List<Tuple<string, int>> t2)
        {
            if(t1.Count != t2.Count) { return false; }
            else
            {
                for (int i = 0; i < t1.Count; i++)
                {
                    if ((t1[i].Item1 != t2[i].Item1) || (t1[i].Item2 != t2[i].Item2)) { return false; }
                }
            }

            return true;
        }

        [TestMethod()]

        // Load the Server for this Test case
        public void MapTest()
        {
            string line = "Bonjour je m'appelle";
            List<Tuple<string, int>> myTupleListExpected = new List<Tuple<string, int>>();

            var t1 = new Tuple<string, int>("Bonjour", 1);
            var t2 = new Tuple<string, int>("je", 1);
            var t3 = new Tuple<string, int>("m'appelle", 1);

            myTupleListExpected.Add(t1);
            myTupleListExpected.Add(t2);
            myTupleListExpected.Add(t3);

            List<Tuple<string, int>> myTupleListResult = Program.Map(line);

            Assert.IsTrue(EqualTuple(myTupleListExpected, myTupleListResult));

        }


        [TestMethod()]
        public void compareOutputFiles()
        {
            Assert.IsTrue(FileCompare("output1Expected.txt", "output.txt"));
        }

        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            if (file1 == file2)
            {
                return true;
            }

            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();
                return false;
            }

            do
            {
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }
    }
}