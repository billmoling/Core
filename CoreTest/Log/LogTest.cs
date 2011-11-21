using BillList.Core.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CoreTest.LogTest
{
    /// <summary>
    ///This is a test class for LogTest and is intended
    ///to contain all LogTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogTest
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
        ///A test for Log Constructor
        ///</summary>
        [TestMethod()]
        public void LogConstructorTest()
        {



            Log log = new Log("Test Message", "Test title", LogType.Normal, 0); // Initialize to an appropriate value
            Log target = new Log(log);
            Assert.AreEqual(log, target);

            log = new Log("", "Test title", LogType.Normal, 0); // Initialize to an appropriate value
            target = new Log(log);
            Assert.AreEqual(log, target);

            log = new Log("Test Message", "", LogType.Normal, 0); // Initialize to an appropriate value
            target = new Log(log);
            Assert.AreEqual(log, target);

            log = new Log("Test Message", "Test title", LogType.Normal, 2); // Initialize to an appropriate value
            target = new Log(log);
            Assert.AreEqual(log, target);
        }

        /// <summary>
        ///A test for Log Constructor
        ///</summary>
        [TestMethod()]
        public void LogConstructorTest1()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            LogType type = new LogType(); // TODO: Initialize to an appropriate value
            ushort indent = 0; // TODO: Initialize to an appropriate value
            Log target = new Log(message, title, type, indent);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CreateDebugLog
        ///</summary>
        [TestMethod()]
        public void CreateDebugLogTest()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            ushort indent = 0; // TODO: Initialize to an appropriate value
            Log expected = null; // TODO: Initialize to an appropriate value
            Log actual;
            actual = Log.CreateDebugLog(message, title, indent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateErrorLog
        ///</summary>
        [TestMethod()]
        public void CreateErrorLogTest()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            ushort indent = 0; // TODO: Initialize to an appropriate value
            Log expected = null; // TODO: Initialize to an appropriate value
            Log actual;
            actual = Log.CreateErrorLog(message, title, indent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateNormalLog
        ///</summary>
        [TestMethod()]
        public void CreateNormalLogTest()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            ushort indent = 0; // TODO: Initialize to an appropriate value
            Log expected = null; // TODO: Initialize to an appropriate value
            Log actual;
            actual = Log.CreateNormalLog(message, title, indent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateWarningLog
        ///</summary>
        [TestMethod()]
        public void CreateWarningLogTest()
        {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            ushort indent = 0; // TODO: Initialize to an appropriate value
            Log expected = null; // TODO: Initialize to an appropriate value
            Log actual;
            actual = Log.CreateWarningLog(message, title, indent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Indent
        ///</summary>
        [TestMethod()]
        public void IndentTest()
        {
            Log log = null; // TODO: Initialize to an appropriate value
            Log target = new Log(log); // TODO: Initialize to an appropriate value
            ushort expected = 0; // TODO: Initialize to an appropriate value
            ushort actual;
            target.Indent = expected;
            actual = target.Indent;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Message
        ///</summary>
        [TestMethod()]
        public void MessageTest()
        {
            Log log = null; // TODO: Initialize to an appropriate value
            Log target = new Log(log); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Message = expected;
            actual = target.Message;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Time
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void TimeTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Log_Accessor target = new Log_Accessor(param0); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.Time = expected;
            actual = target.Time;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [TestMethod()]
        public void TitleTest()
        {
            Log log = null; // TODO: Initialize to an appropriate value
            Log target = new Log(log); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            Log log = null; // TODO: Initialize to an appropriate value
            Log target = new Log(log); // TODO: Initialize to an appropriate value
            LogType expected = new LogType(); // TODO: Initialize to an appropriate value
            LogType actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
