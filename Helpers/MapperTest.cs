namespace BlissAPI.Helpers
{
    using API.Helpers;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class MapperTest
    {
        [Theory]
        [TestCase("test", 999999999.99, 4.5f, 321123)]
        [TestCase("test2", 888899.99, 2.7f, 1)]
        [TestCase("test3", -777888.21, -45654.654f, -200)]
        public void TestMapperBetweenObjectsSimple(string stringType, decimal decimalType, float floatType, int intType)
        {
            //Arrange
            ObjFromSimple objFromSimple = new ObjFromSimple()
            {
                StringType = stringType,
                DateType = DateTimeOffset.Now,
                DecimalType = decimalType,
                FloatType = floatType,
                IntType = intType
            };

            ObjDestSimple objDestSimple = new ObjDestSimple();

            //Act
            objDestSimple.CopyPropertiesFrom(objFromSimple);

            //Assert
            Assert.AreEqual(objDestSimple.StringType, objFromSimple.StringType);
            Assert.AreEqual(objDestSimple.DateType, objFromSimple.DateType);
            Assert.AreEqual(objDestSimple.DecimalType, objFromSimple.DecimalType);
            Assert.AreEqual(objDestSimple.FloatType, objFromSimple.FloatType);
            Assert.AreEqual(objDestSimple.IntType, objFromSimple.IntType);
        }

        [Theory]
        [TestCase("test", 999999999.99, 4.5f, 321123)]
        [TestCase("test2", 888888.9977, 2.7f, 1)]
        [TestCase("test3", -777888.456321, -45654.654f, -200)]
        public void TestMapperBetweenObjectsComplex(string stringType, decimal decimalType, float floatType, int intType)
        {
            //Arrange
            ObjFromComplex objFromComplex = new ObjFromComplex()
            {
                StringType = stringType,
                DateType = DateTimeOffset.Now,
                DecimalType = decimalType,
                FloatType = floatType,
                IntType = intType
            };

            ObjFromSimple objFromSimple = new ObjFromSimple()
            {
                StringType = stringType,
                DateType = DateTimeOffset.Now,
                DecimalType = decimalType,
                FloatType = floatType,
                IntType = intType
            };

            objFromComplex.ObjSimple = objFromSimple;

            objFromComplex.ListObjSimples.Add(objFromSimple);
            objFromComplex.ListObjSimples.Add(objFromSimple);

            ObjDestComplex objDestComplex = new ObjDestComplex();

            //Act
            objDestComplex.CopyPropertiesFrom(objFromComplex);

            Assert.AreEqual(objDestComplex.StringType, objFromComplex.StringType);
            Assert.AreEqual(objDestComplex.DateType, objFromComplex.DateType);
            Assert.AreEqual(objDestComplex.DecimalType, objFromComplex.DecimalType);
            Assert.AreEqual(objDestComplex.FloatType, objFromComplex.FloatType);
            Assert.AreEqual(objDestComplex.IntType, objFromComplex.IntType);

            Assert.AreEqual(objDestComplex.ObjSimple.StringType, objFromComplex.ObjSimple.StringType);
            Assert.AreEqual(objDestComplex.ObjSimple.DateType, objFromComplex.ObjSimple.DateType);
            Assert.AreEqual(objDestComplex.ObjSimple.DecimalType, objFromComplex.ObjSimple.DecimalType);
            Assert.AreEqual(objDestComplex.ObjSimple.FloatType, objFromComplex.ObjSimple.FloatType);
            Assert.AreEqual(objDestComplex.ObjSimple.IntType, objFromComplex.ObjSimple.IntType);

            for (int i = 0; i < objDestComplex.ListObjSimples.Count; i++)
            {
                Assert.AreEqual(objDestComplex.ListObjSimples[i].StringType, objFromComplex.ListObjSimples[i].StringType);
                Assert.AreEqual(objDestComplex.ListObjSimples[i].DateType, objFromComplex.ListObjSimples[i].DateType);
                Assert.AreEqual(objDestComplex.ListObjSimples[i].DecimalType, objFromComplex.ListObjSimples[i].DecimalType);
                Assert.AreEqual(objDestComplex.ListObjSimples[i].FloatType, objFromComplex.ListObjSimples[i].FloatType);
                Assert.AreEqual(objDestComplex.ListObjSimples[i].IntType, objFromComplex.ListObjSimples[i].IntType);
            }
        }

        [Theory]
        [TestCase("test", 999999999.99, 4.5f, 321123)]
        [TestCase("test2", 888888.9977, 2.7f, 1)]
        [TestCase("test3", -777888.456321, -45654.654f, -200)]
        public void TestMapperBetweenComplexObjectsNotOverride(string stringType, decimal decimalType, float floatType, int intType)
        {
            //Arrange
            ObjFromComplex objFromComplex = new ObjFromComplex()
            {
                StringType = stringType,
                DateType = DateTimeOffset.Now,
                DecimalType = decimalType,
                FloatType = floatType,
                IntType = intType
            };

            ObjFromSimple objFromSimple = new ObjFromSimple()
            {
                StringType = stringType,
                DateType = DateTimeOffset.Now,
                DecimalType = decimalType,
                FloatType = floatType,
                IntType = intType
            };

            objFromComplex.ObjSimple = objFromSimple;
            objFromComplex.ListObjSimples.Add(objFromSimple);

            DateTimeOffset testDateTimeOffset = DateTimeOffset.Now;
            ObjDestComplex objDestComplex = new ObjDestComplex()
            {
                IntType = 123,
                DateType = DateTimeOffset.MaxValue,
                DecimalType = decimal.MaxValue,
                ObjSimple = new ObjDestSimple
                {
                    StringType = "NotOverride",
                    DateType = testDateTimeOffset,
                    DecimalType = decimal.MinusOne
                },
                ListObjSimples = new List<ObjDestSimple>
                                         {
                                             new ObjDestSimple
                                                 {
                                                     IntType = 321,
                                                     StringType = "NotOverride2",
                                                     FloatType = float.MaxValue
                                                 }
                                         }
            };

            //Act
            objDestComplex.CopyPropertiesFrom(objFromComplex, false);

            //Assert
            Assert.AreEqual(objDestComplex.StringType, objFromComplex.StringType);
            Assert.AreEqual(objDestComplex.DateType, DateTimeOffset.MaxValue);
            Assert.AreEqual(objDestComplex.DecimalType, decimal.MaxValue);
            Assert.AreEqual(objDestComplex.FloatType, objFromComplex.FloatType);
            Assert.AreEqual(123, objDestComplex.IntType);

            Assert.AreEqual("NotOverride", objDestComplex.ObjSimple.StringType);
            Assert.AreEqual(objDestComplex.ObjSimple.DateType, testDateTimeOffset);
            Assert.AreEqual(objDestComplex.ObjSimple.DecimalType, decimal.MinusOne);
            Assert.AreEqual(objDestComplex.ObjSimple.FloatType, objFromComplex.ObjSimple.FloatType);
            Assert.AreEqual(objDestComplex.ObjSimple.IntType, objFromComplex.ObjSimple.IntType);

            for (int i = 0; i < objDestComplex.ListObjSimples.Count; i++)
            {
                Assert.AreEqual("NotOverride2", objDestComplex.ListObjSimples[i].StringType);
                Assert.AreEqual(objDestComplex.ListObjSimples[i].DateType, objFromComplex.ListObjSimples[i].DateType);
                Assert.AreEqual(objDestComplex.ListObjSimples[i].DecimalType, objFromComplex.ListObjSimples[i].DecimalType);
                Assert.AreEqual(objDestComplex.ListObjSimples[i].FloatType, float.MaxValue);
                Assert.AreEqual(321, objDestComplex.ListObjSimples[i].IntType);
            }
        }
    }


    internal class ObjFromComplex
    {
        public string StringType { get; set; }

        public int? IntType { get; set; }

        public DateTimeOffset? DateType { get; set; }

        public float? FloatType { get; set; }

        public decimal DecimalType { get; set; }

        public ObjFromSimple ObjSimple { get; set; } = new ObjFromSimple();

        public List<ObjFromSimple> ListObjSimples { get; set; } = new List<ObjFromSimple>();
    }

    internal class ObjFromSimple
    {
        public string StringType { get; set; }

        public int? IntType { get; set; }

        public DateTimeOffset DateType { get; set; }

        public float? FloatType { get; set; }

        public decimal DecimalType { get; set; }
    }

    internal class ObjDestComplex
    {
        public string StringType { get; set; }

        public int IntType { get; set; }

        public DateTimeOffset DateType { get; set; }

        public float FloatType { get; set; }

        public decimal? DecimalType { get; set; }

        public ObjDestSimple ObjSimple { get; set; } = new ObjDestSimple();

        public List<ObjDestSimple> ListObjSimples { get; set; } = new List<ObjDestSimple>();
    }

    internal class ObjDestSimple
    {
        public string StringType { get; set; }

        public int IntType { get; set; }

        public DateTimeOffset DateType { get; set; }

        public float FloatType { get; set; }

        public decimal? DecimalType { get; set; }
    }
}
