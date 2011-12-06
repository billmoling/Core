using BigEgg.Core.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace CoreTest
{
    
    
    /// <summary>
    ///This is a test class for LoggerTest and is intended
    ///to contain all LoggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LoggerTest
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
        ///A test for Logger Constructor
        ///</summary>
        [TestMethod()]
        public void LoggerConstructorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddDebug
        ///</summary>
        [TestMethod()]
        public void AddDebugTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.AddDebug(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddError
        ///</summary>
        [TestMethod()]
        public void AddErrorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.AddError(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddLog
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void AddLogTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            Log log = null; // TODO: Initialize to an appropriate value
            target.AddLog(log);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddLog
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void AddLogTest1()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            LogType logType = new LogType(); // TODO: Initialize to an appropriate value
            ushort indent = 0; // TODO: Initialize to an appropriate value
            target.AddLog(message, title, logType, indent);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddNormal
        ///</summary>
        [TestMethod()]
        public void AddNormalTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.AddNormal(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddWarning
        ///</summary>
        [TestMethod()]
        public void AddWarningTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.AddWarning(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ChangeTitleString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void ChangeTitleStringTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            target.ChangeTitleString();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ClearIndent
        ///</summary>
        [TestMethod()]
        public void ClearIndentTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            target.ClearIndent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ClearTitle
        ///</summary>
        [TestMethod()]
        public void ClearTitleTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            target.ClearTitle();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for FormatLogRTF
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void FormatLogRTFTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            Log log = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FormatLogRTF(log);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FormatLogString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void FormatLogStringTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            Log log = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FormatLogString(log);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IndentAdd
        ///</summary>
        [TestMethod()]
        public void IndentAddTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            target.IndentAdd(title);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IndentMinus
        ///</summary>
        [TestMethod()]
        public void IndentMinusTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            target.IndentMinus();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeRTFString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void InitializeRTFStringTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            target.InitializeRTFString();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MergeLogger
        ///</summary>
        [TestMethod()]
        public void MergeLoggerTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Logger source = null; // TODO: Initialize to an appropriate value
            target.MergeLogger(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OutputCacheLogs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void OutputCacheLogsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            object state = null; // TODO: Initialize to an appropriate value
            target.OutputCacheLogs(state);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OutputSettingChange
        ///</summary>
        [TestMethod()]
        public void OutputSettingChangeTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            LoggerOutput newOutputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            target.OutputSettingChange(newOutputSetting);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OutputThread
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void OutputThreadTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            target.OutputThread();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TitleEnd
        ///</summary>
        [TestMethod()]
        public void TitleEndTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            target.TitleEnd();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TitleStart
        ///</summary>
        [TestMethod()]
        public void TitleStartTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            target.TitleStart(title);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteRTFCacheLogs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void WriteRTFCacheLogsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            target.WriteRTFCacheLogs();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WriteRegularCacheLogs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Core.dll")]
        public void WriteRegularCacheLogsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Logger_Accessor target = new Logger_Accessor(param0); // TODO: Initialize to an appropriate value
            target.WriteRegularCacheLogs();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsDebugEnabled
        ///</summary>
        [TestMethod()]
        public void IsDebugEnabledTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsDebugEnabled = expected;
            actual = target.IsDebugEnabled;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogDebugMessageColor
        ///</summary>
        [TestMethod()]
        public void LogDebugMessageColorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            target.LogDebugMessageColor = expected;
            actual = target.LogDebugMessageColor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogErrorMessageColor
        ///</summary>
        [TestMethod()]
        public void LogErrorMessageColorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            target.LogErrorMessageColor = expected;
            actual = target.LogErrorMessageColor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogFilePath
        ///</summary>
        [TestMethod()]
        public void LogFilePathTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.LogFilePath = expected;
            actual = target.LogFilePath;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogMessageFont
        ///</summary>
        [TestMethod()]
        public void LogMessageFontTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Font expected = null; // TODO: Initialize to an appropriate value
            Font actual;
            target.LogMessageFont = expected;
            actual = target.LogMessageFont;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogNormalMessageColor
        ///</summary>
        [TestMethod()]
        public void LogNormalMessageColorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            target.LogNormalMessageColor = expected;
            actual = target.LogNormalMessageColor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogTimeColor
        ///</summary>
        [TestMethod()]
        public void LogTimeColorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            target.LogTimeColor = expected;
            actual = target.LogTimeColor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogTimeFont
        ///</summary>
        [TestMethod()]
        public void LogTimeFontTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Font expected = null; // TODO: Initialize to an appropriate value
            Font actual;
            target.LogTimeFont = expected;
            actual = target.LogTimeFont;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogTitleColor
        ///</summary>
        [TestMethod()]
        public void LogTitleColorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            target.LogTitleColor = expected;
            actual = target.LogTitleColor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogTitleFont
        ///</summary>
        [TestMethod()]
        public void LogTitleFontTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Font expected = null; // TODO: Initialize to an appropriate value
            Font actual;
            target.LogTitleFont = expected;
            actual = target.LogTitleFont;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogWarningMessageColor
        ///</summary>
        [TestMethod()]
        public void LogWarningMessageColorTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            target.LogWarningMessageColor = expected;
            actual = target.LogWarningMessageColor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OutputSetting
        ///</summary>
        [TestMethod()]
        public void OutputSettingTest()
        {
            string logFilePath = string.Empty; // TODO: Initialize to an appropriate value
            LoggerOutput outputSetting = new LoggerOutput(); // TODO: Initialize to an appropriate value
            bool isDebugEnabled = false; // TODO: Initialize to an appropriate value
            Logger target = new Logger(logFilePath, outputSetting, isDebugEnabled); // TODO: Initialize to an appropriate value
            LoggerOutput actual;
            actual = target.OutputSetting;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
