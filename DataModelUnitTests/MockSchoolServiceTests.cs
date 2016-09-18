using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DataModelUnitTests
{
    /// <summary>
    /// Summary description for MockSchoolService
    /// </summary>
    [TestClass]
    public class MockSchoolServiceTests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void TestCountries()
        {
            MockSchoolService service = new MockSchoolService();
            Assert.IsTrue(service.GetCountries().SequenceEqual(new List<string> { "India", "Indonesia" }));
        }

        [TestMethod]
        public void TestStates()
        {
            MockSchoolService service = new MockSchoolService();
            Assert.IsTrue(service.GetStates("India").SequenceEqual(new List<string> { "MH" , "TN"}));
        }

        [TestMethod]
        public void TestCities()
        {
            MockSchoolService service = new MockSchoolService();
            Assert.IsTrue(service.GetCities("India", "MH").SequenceEqual(new List<string> { "Mumbai" }));
        }
    }
}
