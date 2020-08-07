using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.Tests
{
    [TestClass()]
    public class ProducerTests
    {
        [TestMethod()]
        public void MainTest()
        {
            Producer.Main(new string[0]);

        }
    }
}