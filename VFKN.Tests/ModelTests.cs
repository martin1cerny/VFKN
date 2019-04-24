using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFKN.Entities;

namespace VFKN.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        [DeploymentItem("Files\\Export_vse.vfk")]
        public void ReadBuildingPolygon()
        {
            var model = Model.Open("Files\\Export_vse.vfk");
            var building = model.Get<Budova>(b => b.ID == "293210306").First();
            Assert.IsNotNull(building.Polygon);

            building = model.Get<Budova>(b => b.ID == "323701306").First();
            Assert.IsNotNull(building.Address.FirstOrDefault());
        }
    }
}
