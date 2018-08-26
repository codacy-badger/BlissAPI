namespace UnitTestAPI.Helpers
{
    using System;
    using System.Collections.Generic;

    using API.Helpers;

    using NUnit.Framework;

    public class UtilityTest
    {
        [Test]
        public void TestIsNullOrDefaultTrue()
        {
            //Arrange
            const int TEST_INT = default(int);
            DateTime testDateTime = default(DateTime);
            const decimal TES_DECIMAL = default(decimal);

            //Act
            bool isNullOrDefaultString = Utility.IsNullOrDefault((string)null);
            bool isNullOrDefaultInt = Utility.IsNullOrDefault(TEST_INT);
            bool isNullOrDefaultDateTime = Utility.IsNullOrDefault(testDateTime);
            bool isNullOrDefaultDateDecimal = Utility.IsNullOrDefault(TES_DECIMAL);
            bool isNullOrDefaultDateListString = Utility.IsNullOrDefault((List<string>)null);
            bool isNullOrDefaultDateListInt = Utility.IsNullOrDefault((List<int>)null);

            //Assert
            Assert.True(isNullOrDefaultString);
            Assert.True(isNullOrDefaultInt);
            Assert.True(isNullOrDefaultDateTime);
            Assert.True(isNullOrDefaultDateDecimal);
            Assert.True(isNullOrDefaultDateListString);
            Assert.True(isNullOrDefaultDateListInt);
        }

        [Test]
        public void TestIsNullOrDefaultFalse()
        {
            //Arrange
            const string TEST_DEFAULT = "test";
            const int TEST_INT = 123;
            DateTime testDateTime = DateTime.Now;
            const decimal TES_DECIMAL = decimal.MaxValue;
            List<string> tetListString = new List<string>();
            List<int> testListInt = new List<int>() { 123 };

            //Act
            bool isNullOrDefaultString = Utility.IsNullOrDefault(TEST_DEFAULT);
            bool isNullOrDefaultInt = Utility.IsNullOrDefault(TEST_INT);
            bool isNullOrDefaultDateTime = Utility.IsNullOrDefault(testDateTime);
            bool isNullOrDefaultDateDecimal = Utility.IsNullOrDefault(TES_DECIMAL);
            bool isNullOrDefaultDateListString = Utility.IsNullOrDefault(tetListString);
            bool isNullOrDefaultDateListInt = Utility.IsNullOrDefault(testListInt);

            //Assert
            Assert.False(isNullOrDefaultString);
            Assert.False(isNullOrDefaultInt);
            Assert.False(isNullOrDefaultDateTime);
            Assert.False(isNullOrDefaultDateDecimal);
            Assert.False(isNullOrDefaultDateListString);
            Assert.False(isNullOrDefaultDateListInt);
        }
    }
}
