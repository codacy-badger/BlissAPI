namespace UnitTest.Helpers
{
    using API.Helpers;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class MapperTest
    {
        [Theory]
        [InlineData("test", 999999999.99, 4.5f, 321123)]
        [InlineData("test2", 888888.9977, 2.7, 1)]
        [InlineData("test3", -777888.456321, -45654.654, -200)]
        public void TestMapperBetweenObjectsSimple(string stringType, decimal decimalType, float floatType, int intType)
        {
            //Arrange
            ObjFromSimple objFromSimple = new ObjFromSimple
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
            Assert.Equal(objDestSimple.StringType, objFromSimple.StringType);
            Assert.Equal(objDestSimple.DateType, objFromSimple.DateType);
            Assert.Equal(objDestSimple.DecimalType, objFromSimple.DecimalType);
            Assert.Equal(objDestSimple.FloatType, objFromSimple.FloatType);
            Assert.Equal(objDestSimple.IntType, objFromSimple.IntType);
        }

        [Theory]
        [InlineData("test", 999999999.99, 4.5f, 321123)]
        [InlineData("test2", 888888.9977, 2.7, 1)]
        [InlineData("test3", -777888.456321, -45654.654, -200)]
        public void TestMapperBetweenObjectsComplex(string stringType, decimal decimalType, float floatType, int intType)
        {
            //Arrange
            ObjFromComplex objFromComplex = new ObjFromComplex
            {
                StringType = stringType,
                DateType = DateTimeOffset.Now,
                DecimalType = decimalType,
                FloatType = floatType,
                IntType = intType
            };

            ObjFromSimple objFromSimple = new ObjFromSimple
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

            Assert.Equal(objDestComplex.StringType, objFromComplex.StringType);
            Assert.Equal(objDestComplex.DateType, objFromComplex.DateType);
            Assert.Equal(objDestComplex.DecimalType, objFromComplex.DecimalType);
            Assert.Equal(objDestComplex.FloatType, objFromComplex.FloatType);
            Assert.Equal(objDestComplex.IntType, objFromComplex.IntType);

            Assert.Equal(objDestComplex.ObjSimple.StringType, objFromComplex.ObjSimple.StringType);
            Assert.Equal(objDestComplex.ObjSimple.DateType, objFromComplex.ObjSimple.DateType);
            Assert.Equal(objDestComplex.ObjSimple.DecimalType, objFromComplex.ObjSimple.DecimalType);
            Assert.Equal(objDestComplex.ObjSimple.FloatType, objFromComplex.ObjSimple.FloatType);
            Assert.Equal(objDestComplex.ObjSimple.IntType, objFromComplex.ObjSimple.IntType);

            for (int i = 0; i < objDestComplex.ListObjSimples.Count; i++)
            {
                Assert.Equal(objDestComplex.ListObjSimples[i].StringType, objFromComplex.ListObjSimples[i].StringType);
                Assert.Equal(objDestComplex.ListObjSimples[i].DateType, objFromComplex.ListObjSimples[i].DateType);
                Assert.Equal(objDestComplex.ListObjSimples[i].DecimalType, objFromComplex.ListObjSimples[i].DecimalType);
                Assert.Equal(objDestComplex.ListObjSimples[i].FloatType, objFromComplex.ListObjSimples[i].FloatType);
                Assert.Equal(objDestComplex.ListObjSimples[i].IntType, objFromComplex.ListObjSimples[i].IntType);
            }
        }

        [Theory]
        [InlineData("test", 999999999.99, 4.5f, 321123)]
        [InlineData("test2", 888888.9977, 2.7, 1)]
        [InlineData("test3", -777888.456321, -45654.654, -200)]
        public void TestMapperBetweenComplexObjectsNotOverride(string stringType, decimal decimalType, float floatType, int intType)
        {
            //Arrange
            ObjFromComplex objFromComplex = new ObjFromComplex
            {
                StringType = stringType,
                DateType = DateTimeOffset.Now,
                DecimalType = decimalType,
                FloatType = floatType,
                IntType = intType
            };

            ObjFromSimple objFromSimple = new ObjFromSimple
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
            ObjDestComplex objDestComplex = new ObjDestComplex
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
            Assert.Equal(objDestComplex.StringType, objFromComplex.StringType);
            Assert.Equal(objDestComplex.DateType, DateTimeOffset.MaxValue);
            Assert.Equal(objDestComplex.DecimalType, decimal.MaxValue);
            Assert.Equal(objDestComplex.FloatType, objFromComplex.FloatType);
            Assert.Equal(123, objDestComplex.IntType);

            Assert.Equal("NotOverride", objDestComplex.ObjSimple.StringType);
            Assert.Equal(objDestComplex.ObjSimple.DateType, testDateTimeOffset);
            Assert.Equal(objDestComplex.ObjSimple.DecimalType, decimal.MinusOne);
            Assert.Equal(objDestComplex.ObjSimple.FloatType, objFromComplex.ObjSimple.FloatType);
            Assert.Equal(objDestComplex.ObjSimple.IntType, objFromComplex.ObjSimple.IntType);

            for (int i = 0; i < objDestComplex.ListObjSimples.Count; i++)
            {
                Assert.Equal("NotOverride2", objDestComplex.ListObjSimples[i].StringType);
                Assert.Equal(objDestComplex.ListObjSimples[i].DateType, objFromComplex.ListObjSimples[i].DateType);
                Assert.Equal(objDestComplex.ListObjSimples[i].DecimalType, objFromComplex.ListObjSimples[i].DecimalType);
                Assert.Equal(objDestComplex.ListObjSimples[i].FloatType, float.MaxValue);
                Assert.Equal(321, objDestComplex.ListObjSimples[i].IntType);
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
