using BigEgg.Core.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace CoreTest
{
    
    
    /// <summary>
    ///This is a test class for LoggerTest and is intended
    ///to contain all LoggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LoggerTest
    {
        enum LoggerTestData
        {
            ID,
            LogFilePath,
            Type,
            IsDebug,
            IsValid
        }

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


        #region Constructor Test
        /// <summary>
        /// A test for Logger Constructor
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LoggerData", DataAccessMethod.Sequential), TestMethod()]
        public void Logger_Constructor_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LoggerTestData.IsValid];
            String logFilePath;
            LoggerOutputSetting outputSetting;
            Boolean isDebugEnabled;

            Object temp = TestContext.DataRow.ItemArray[(int)LoggerTestData.LogFilePath];
            if (temp is System.DBNull)
                logFilePath = null;
            else
                logFilePath = (String)temp;

            switch ((Int32)TestContext.DataRow.ItemArray[(int)LoggerTestData.Type])
            {
                case 1:
                    outputSetting = LoggerOutputSetting.NotWriteToFileRTF;
                    break;
                case 2:
                    outputSetting = LoggerOutputSetting.Regular;
                    break;
                case 3:
                    outputSetting = LoggerOutputSetting.RTF;
                    break;
                default:
                    outputSetting = LoggerOutputSetting.NotWriteToFileRegular;
                    break;
            }

            isDebugEnabled = (Boolean)TestContext.DataRow.ItemArray[(int)LoggerTestData.IsDebug];


            if (valid)
            {
                Logger target = new Logger(outputSetting, logFilePath, isDebugEnabled);
                Assert.AreEqual(target.IsDebugEnabled, isDebugEnabled);
                Assert.AreEqual(target.LogFilePath, logFilePath);
                Assert.AreEqual(target.OutputSetting, outputSetting);

                if (isDebugEnabled)
                    switch (outputSetting)
                    {
                        case LoggerOutputSetting.NotWriteToFileRegular:
                        case LoggerOutputSetting.NotWriteToFileRTF:
                            Assert.AreEqual(target.Logs.Count, 5);
                            break;
                        case LoggerOutputSetting.Regular:
                        case LoggerOutputSetting.RTF:
                            Assert.AreEqual(target.Logs.Count, 6);
                            break;
                    }
                else
                    Assert.AreEqual(target.Logs.Count, 0);

                Assert.AreEqual(target.Indent, 0);
                Assert.AreEqual(target.TitleString, String.Empty);

                Assert.AreEqual(target.LogTimeFont, new Font("Calibri", 11, FontStyle.Italic));
                Assert.AreEqual(target.LogTitleFont, new Font("Calibri", 11, FontStyle.Bold));
                Assert.AreEqual(target.LogMessageFont, new Font("Calibri", 11, FontStyle.Regular));

                Assert.AreEqual(target.LogTimeColor, Color.FromArgb(217, 116, 43));
                Assert.AreEqual(target.LogTitleColor, Color.FromArgb(230, 180, 80));
                Assert.AreEqual(target.LogWarningMessageColor, Color.FromArgb(153, 77, 82));
                Assert.AreEqual(target.LogDebugMessageColor, Color.FromArgb(227, 230, 195));
                Assert.AreEqual(target.LogNormalMessageColor, Color.Black);
                Assert.AreEqual(target.LogErrorMessageColor, Color.Red);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Logger target = new Logger(outputSetting, logFilePath, isDebugEnabled);
                }
                catch (Exception e)
                {
                    exception = e;
                }
                Assert.IsNotNull(exception, "The expected exception was not thrown.");
                Assert.AreEqual<Type>(typeof(ArgumentNullException), exception.GetType(),
                    "The exception type was unexpected.");
            }
        }
        /// <summary>
        /// A test for the default parameters in Logger Constructor
        /// </summary>
        [TestMethod()]
        public void Logger_Constructor_DefaultTest()
        {
            Logger target = new Logger();
            Assert.IsFalse(target.IsDebugEnabled);
            Assert.AreEqual(target.LogFilePath, String.Empty);
            Assert.AreEqual(target.OutputSetting, LoggerOutputSetting.NotWriteToFileRegular);

            Assert.AreEqual(target.Logs.Count, 0);
            Assert.AreEqual(target.Indent, 0);
            Assert.AreEqual(target.TitleString, String.Empty);

            Assert.AreEqual(target.LogTimeFont, new Font("Calibri", 11, FontStyle.Italic));
            Assert.AreEqual(target.LogTitleFont, new Font("Calibri", 11, FontStyle.Bold));
            Assert.AreEqual(target.LogMessageFont, new Font("Calibri", 11, FontStyle.Regular));

            Assert.AreEqual(target.LogTimeColor, Color.FromArgb(217, 116, 43));
            Assert.AreEqual(target.LogTitleColor, Color.FromArgb(230, 180, 80));
            Assert.AreEqual(target.LogWarningMessageColor, Color.FromArgb(153, 77, 82));
            Assert.AreEqual(target.LogDebugMessageColor, Color.FromArgb(227, 230, 195));
            Assert.AreEqual(target.LogNormalMessageColor, Color.Black);
            Assert.AreEqual(target.LogErrorMessageColor, Color.Red);
        }
        #endregion

        #region Logger Method Test
        /// <summary>
        /// A test for the Addlog Methods in Logger Class
        /// </summary>
        [TestMethod()]
        public void Logger_Method_Regular_Test()
        {
            Logger target = new Logger();
            target.LogTimeFormatString = "11:11:11.1111";
            LoggerSimulator(ref target);

            List<String> expect = LogRegularStringSimulator(false);

            for (int i = 0;i<target.Logs.Count; i++)
                Assert.AreEqual(expect[i], target.Logs[i]);

            target = new Logger(isDebugEnabled: true);
            target.LogTimeFormatString = "11:11:11.1111";
            LoggerSimulator(ref target);

            expect = LogRegularStringSimulator(true);

            for (int i = 0; i < target.Logs.Count; i++)
                Assert.AreEqual(expect[i], target.Logs[i]);
        }

        /// <summary>
        /// A test for the MergeLogger Methods in Logger Class
        /// </summary>
        [TestMethod()]
        public void Logger_Method_MergeLogger_Test()
        {
            Logger target = new Logger();
            target.LogTimeFormatString = "11:11:11.1111";
            LoggerSimulator(ref target);

            Logger temp = new Logger(isDebugEnabled: true);
            target.LogTimeFormatString = "11:11:11.1111";
            LoggerSimulator(ref temp);

            List<String> expect = LogRegularStringSimulator(false);
            expect.AddRange(LogRegularStringSimulator(true));

            for (int i = 0; i < target.Logs.Count; i++)
                Assert.AreEqual(expect[i], target.Logs[i]);
        }

        /// <summary>
        /// A test for OutputSettingChange Methods in Logger Class
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LoggerData", DataAccessMethod.Sequential), TestMethod()]
        public void Logger_Method_OutputSettingChange_Test()
        {
            Logger target = new Logger();

            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LoggerTestData.IsValid];
            String logFilePath;
            LoggerOutputSetting outputSetting;

            Object temp = TestContext.DataRow.ItemArray[(int)LoggerTestData.LogFilePath];
            if (temp is System.DBNull)
                logFilePath = null;
            else
                logFilePath = (String)temp;

            switch ((Int32)TestContext.DataRow.ItemArray[(int)LoggerTestData.Type])
            {
                case 1:
                    outputSetting = LoggerOutputSetting.NotWriteToFileRTF;
                    break;
                case 2:
                    outputSetting = LoggerOutputSetting.Regular;
                    break;
                case 3:
                    outputSetting = LoggerOutputSetting.RTF;
                    break;
                default:
                    outputSetting = LoggerOutputSetting.NotWriteToFileRegular;
                    break;
            }

            if (valid)
            {
                target.OutputSettingChange(outputSetting, logFilePath);

                Assert.AreEqual(outputSetting, target.OutputSetting);

                Assert.AreEqual(logFilePath, target.LogFilePath);
            }
            else
            {
                Exception exception = null;
                try
                {
                    target.OutputSettingChange(outputSetting, logFilePath);
                }
                catch (Exception e)
                {
                    exception = e;
                }
                Assert.IsNotNull(exception, "The expected exception was not thrown.");
                Assert.AreEqual<Type>(typeof(ArgumentNullException), exception.GetType(),
                    "The exception type was unexpected.");
            }
        }

        /// <summary>
        /// Will out the Regular log file, and the RTF log file. Need manually check if the output is correct.
        /// </summary>
        [TestMethod()]
        public void Logger_Method_Output_Test()
        {
            Logger target = new Logger(LoggerOutputSetting.Regular, @"D:\Core\CoreTest\bin\Debug");
            LoggerSimulator(ref target);

            target = new Logger(LoggerOutputSetting.RTF, @"D:\Core\CoreTest\bin\Debug");
            LoggerSimulator(ref target);
        }
        #endregion

        #region Properties Test
        /// <summary>
        /// A test for Indent Property
        /// </summary>
        [TestMethod()]
        public void Logger_Property_Indent_Test()
        {
            Logger target = new Logger();
            Assert.AreEqual(0, target.Indent);

            target.IndentAdd();
            Assert.AreEqual(1, target.Indent);

            target.IndentAdd();
            Assert.AreEqual(2, target.Indent);

            target.IndentMinus();
            Assert.AreEqual(1, target.Indent);

            target.IndentAdd();
            target.ClearIndent();
            Assert.AreEqual(0, target.Indent);
        }
        /// <summary>
        /// A test for IsDebugEnabled Property
        /// </summary>
        [TestMethod()]
        public void Logger_Property_IsDebugEnabled_Test()
        {
            Logger target = new Logger();

            target.IsDebugEnabled = true;
            Assert.IsTrue(target.IsDebugEnabled);

            target.IsDebugEnabled = false;
            Assert.IsFalse(target.IsDebugEnabled);
        }
        /// <summary>
        /// A test for LogFilePath Property
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LoggerData", DataAccessMethod.Sequential), TestMethod()]
        public void Logger_Property_LogFilePath_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LoggerTestData.IsValid];
            String logFilePath;
            LoggerOutputSetting outputSetting;

            Object temp = TestContext.DataRow.ItemArray[(int)LoggerTestData.LogFilePath];
            if (temp is System.DBNull)
                logFilePath = null;
            else
                logFilePath = (String)temp;

            switch ((Int32)TestContext.DataRow.ItemArray[(int)LoggerTestData.Type])
            {
                case 1:
                    outputSetting = LoggerOutputSetting.NotWriteToFileRTF;
                    break;
                case 2:
                    outputSetting = LoggerOutputSetting.Regular;
                    break;
                case 3:
                    outputSetting = LoggerOutputSetting.RTF;
                    break;
                default:
                    outputSetting = LoggerOutputSetting.NotWriteToFileRegular;
                    break;
            }

            if (valid)
            {
                Logger target = new Logger(outputSetting, "123");

                target.LogFilePath = logFilePath;

                Assert.AreEqual(logFilePath, target.LogFilePath);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Logger target = new Logger(outputSetting, "123");

                    target.LogFilePath = logFilePath;
                }
                catch (Exception e)
                {
                    exception = e;
                }
                Assert.IsNotNull(exception, "The expected exception was not thrown.");
                Assert.AreEqual<Type>(typeof(ArgumentException), exception.GetType(),
                    "The exception type was unexpected.");
            }
        }
        /// <summary>
        /// A test for LogTimeFormatString Property
        /// </summary>
        [TestMethod()]
        public void Logger_Property_LogTimeFormatString_Test()
        {
            Logger target = new Logger();

            target.LogTimeFormatString = "HH:mm:ss";
            Assert.AreEqual("HH:mm:ss", target.LogTimeFormatString);

            Exception exception = null;
            try
            {
                target.LogTimeFormatString = "";
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception, "The expected exception was not thrown.");
            Assert.AreEqual<Type>(typeof(ArgumentException), exception.GetType(),
                "The exception type was unexpected.");
        }
        #endregion

        #region Constructor Logger
        /// <summary>
        /// A simulator used to add lots of logs for a Logger class
        /// </summary>
        /// <param name="logger">A reference of the Logger class.</param>
        public void LoggerSimulator(ref Logger logger)
        {
            logger.ClearIndent();
            logger.ClearTitle();

            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");

            logger.TitleStart("Title 1");
            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");

            logger.TitleStart("Title 2");
            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");
            logger.TitleEnd();

            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");

            logger.IndentAdd();
            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");

            logger.IndentAdd();
            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");

            logger.IndentMinus();
            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");

            logger.ClearIndent();
            logger.ClearTitle();

            logger.AddNormal("Test Line 1");
            logger.AddWarning("Test Line 2");
            logger.AddError("Test Line 3");
            logger.AddDebug("Test Line 4");
        }

        /// <summary>
        /// A simulator used to create and return the Regular Log String list which the LoggerSimulator will add into the Logger class.
        /// </summary>
        /// <param name="isDebugEnable">If the isDebugEnable is false, will not return the Debug log string.</param>
        /// <returns>the Regular Log String list which the LoggerSimulator will add into the Logger class.</returns>
        public List<String> LogRegularStringSimulator(Boolean isDebugEnable)
        {
            List<String> _logString = new List<String>();

            if (isDebugEnable)
            {
                _logString.Add("[11:11:11.1111] Debug: Logger::Logger() Start Initialize Logger Class");
                _logString.Add("[11:11:11.1111] Debug: Logger::InitializeRTFString() Start Initialize the Format of RTF String");
                _logString.Add("[11:11:11.1111] Debug: Logger::InitializeRTFString() Initialize the Format of RTF String End");
                _logString.Add("[11:11:11.1111] Debug: Logger::Logger() Initialize Logger Class End");
                _logString.Add("[11:11:11.1111] Debug: Logger::OutputThread() Close the output Thread.");
            }

            _logString.Add("[11:11:11.1111] Test Line 1");
            _logString.Add("[11:11:11.1111] Warning: Test Line 2");;
            _logString.Add("[11:11:11.1111] Error: Test Line 3");
            if (isDebugEnable)
                _logString.Add("[11:11:11.1111] Debug: Test Line 4");

            _logString.Add("[11:11:11.1111] Title 1 Test Line 1");
            _logString.Add("[11:11:11.1111] Warning: Title 1 Test Line 2"); ;
            _logString.Add("[11:11:11.1111] Error: Title 1 Test Line 3");
            if (isDebugEnable)
                _logString.Add("[11:11:11.1111] Debug: Title 1 Test Line 4");

            _logString.Add("[11:11:11.1111] Title 1::Title 2 Test Line 1");
            _logString.Add("[11:11:11.1111] Warning: Title 1::Title 2 Test Line 2"); ;
            _logString.Add("[11:11:11.1111] Error: Title 1::Title 2 Test Line 3");
            if (isDebugEnable)
                _logString.Add("[11:11:11.1111] Debug: Title 1::Title 2 Test Line 4");

            _logString.Add("[11:11:11.1111] Title 1 Test Line 1");
            _logString.Add("[11:11:11.1111] Warning: Title 1 Test Line 2"); ;
            _logString.Add("[11:11:11.1111] Error: Title 1 Test Line 3");
            if (isDebugEnable)
                _logString.Add("[11:11:11.1111] Debug: Title 1 Test Line 4");

            _logString.Add("\t[11:11:11.1111] Title 1 Test Line 1");
            _logString.Add("\t[11:11:11.1111] Warning: Title 1 Test Line 2"); ;
            _logString.Add("\t[11:11:11.1111] Error: Title 1 Test Line 3");
            if (isDebugEnable)
                _logString.Add("\t[11:11:11.1111] Debug: Title 1 Test Line 4");

            _logString.Add("\t\t[11:11:11.1111] Title 1 Test Line 1");
            _logString.Add("\t\t[11:11:11.1111] Warning: Title 1 Test Line 2"); ;
            _logString.Add("\t\t[11:11:11.1111] Error: Title 1 Test Line 3");
            if (isDebugEnable)
                _logString.Add("\t\t[11:11:11.1111] Debug: Title 1 Test Line 4");

            _logString.Add("\t[11:11:11.1111] Title 1 Test Line 1");
            _logString.Add("\t[11:11:11.1111] Warning: Title 1 Test Line 2"); ;
            _logString.Add("\t[11:11:11.1111] Error: Title 1 Test Line 3");
            if (isDebugEnable)
                _logString.Add("\t[11:11:11.1111] Debug: Title 1 Test Line 4");

            _logString.Add("[11:11:11.1111] Test Line 1");
            _logString.Add("[11:11:11.1111] Warning: Test Line 2");;
            _logString.Add("[11:11:11.1111] Error: Test Line 3");
            if (isDebugEnable)
                _logString.Add("[11:11:11.1111] Debug: Test Line 4");

            return _logString;
        }
        #endregion
    }
}
