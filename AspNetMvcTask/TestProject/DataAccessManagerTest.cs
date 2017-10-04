using MvcApp.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for DataAccessManagerTest and is intended
    ///to contain all DataAccessManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataAccessManagerTest
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for DataAccessManager Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\Andrey Kukalev\\AspNetMvcTask\\MvcApp", "/")]
        [UrlToTest("http://localhost:1287/")]
        public void DataAccessManagerConstructorTest()
        {
            string connectionStr = string.Empty; // TODO: Initialize to an appropriate value
            DataAccessManager target = new DataAccessManager(connectionStr);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddImage
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\Andrey Kukalev\\AspNetMvcTask\\MvcApp", "/")]
        [UrlToTest("http://localhost:1287/")]
        public void AddImageTest()
        {
            string connectionStr = string.Empty; // TODO: Initialize to an appropriate value
            DataAccessManager target = new DataAccessManager(connectionStr); // TODO: Initialize to an appropriate value
            Employee employee = null; // TODO: Initialize to an appropriate value
            target.AddEmployee(employee);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ReadEmployees
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\Andrey Kukalev\\AspNetMvcTask\\MvcApp", "/")]
        [UrlToTest("http://localhost:1287/")]
        public void ReadEmployeesTest()
        {
            string connectionStr = string.Empty; // TODO: Initialize to an appropriate value
            DataAccessManager target = new DataAccessManager(connectionStr); // TODO: Initialize to an appropriate value
            List<Employee> expected = null; // TODO: Initialize to an appropriate value
            List<Employee> actual;
            actual = target.ReadEmployees();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
