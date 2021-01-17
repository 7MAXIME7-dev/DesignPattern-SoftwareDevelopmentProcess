using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1_CustomQueue_Generics.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void MyTests()
        {

            CustomQueue<string> myQueue = new CustomQueue<string>();

            Assert.IsTrue(myQueue.isEmptyQueue());

            myQueue.enQueue(new Node<string>("Bonjour"));
            myQueue.enQueue(new Node<string>("Monsieur"));

            Assert.AreEqual("Bonjour", myQueue.deQueue().data);
            Assert.AreEqual("Monsieur", myQueue.deQueue().data);

            Assert.IsTrue(myQueue.isEmptyQueue());

        }
    }
}

