namespace UnitTest.Helpers
{
    using API.Helpers;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class UtilityTest
    {
        [Fact]
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

        [Fact]
        public void TestIsNullOrDefaultFalse()
        {
            //Arrange
            string testDefault = "test";
            int testInt = 123;
            DateTime testDateTime = DateTime.Now;
            decimal tesDecimal = decimal.MaxValue;
            List<string> tetListString = new List<string>();
            List<int> testListInt = new List<int>() { 123 };

            //Act
            bool isNullOrDefaultString = Utility.IsNullOrDefault(testDefault);
            bool isNullOrDefaultInt = Utility.IsNullOrDefault(testInt);
            bool isNullOrDefaultDateTime = Utility.IsNullOrDefault(testDateTime);
            bool isNullOrDefaultDateDecimal = Utility.IsNullOrDefault(tesDecimal);
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
