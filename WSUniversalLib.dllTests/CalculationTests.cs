using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSUniversalLib.dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSUniversalLib.dll.Tests
{
    [TestClass()]
    public class CalculationTests
    {
        [TestMethod()]
        public void Check_CorectData()
        {
            //Arrage
            int expected = 114148;
            //Act
            Calculation calc = new Calculation();
            int actual = calc.GetQuantityForProduct(3, 1, 15, 20, 45);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_NoRightTypeProduct()
        {
            //Arrage
            int expected = -1;
            //Act
            Calculation calc = new Calculation();
            int actual = calc.GetQuantityForProduct(7, 1, 15, 20, 45);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_NoRightTypeMaterial()
        {
            //Arrage
            int expected = -1;
            //Act
            Calculation calc = new Calculation();
            int actual = calc.GetQuantityForProduct(3, 7, 15, 20, 45);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_CountUnder_1()
        {
            //Arrage
            int expected = -1;
            //Act
            Calculation calc = new Calculation();
            int actual = calc.GetQuantityForProduct(3, 1, -15, 20, 45);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_WidthUnder_1()
        {
            //Arrage
            int expected = -1;
            //Act
            Calculation calc = new Calculation();
            int actual = calc.GetQuantityForProduct(3, 1, 15, -20, 45);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void Check_LengthUnder_1()
        {
            //Arrage
            int expected = -1;
            //Act
            Calculation calc = new Calculation();
            int actual = calc.GetQuantityForProduct(3, 1, 15, 20, -45);
            //Assert
            Assert.AreEqual(expected, actual);
        }

    }
}