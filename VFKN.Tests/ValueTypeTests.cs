using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VFKN.Extensions;

namespace VFKN.Tests
{
    [TestClass]
    public class ValueTypeTests
    {
        [TestMethod]
        public void DateTimeTest()
        {
            var value = "30.12.1985 11:55:59";
            var dt = value.VfkToDateTime();
            Assert.IsTrue(dt.HasValue);

            var date = dt.Value;
            Assert.AreEqual(30, date.Day);
            Assert.AreEqual(12, date.Month);
            Assert.AreEqual(1985, date.Year);
            Assert.AreEqual(11, date.Hour);
            Assert.AreEqual(55, date.Minute);
            Assert.AreEqual(59, date.Second);

        }
    }
}
