using BigEgg.Core.Log;
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
        enum LogTestData
        {
            ID,
            Message,
            Title,
            Type,
            Indent,
            IsValid
        }
        enum LogMessageTestData
        {
            ID,
            Message,
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
        /// A test for Copy Constructor of Log class
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_CopyConstructor_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            String message, title;
            LogType logType;
            Int32 indent;

            if (valid)
            {
                Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
                if (temp is System.DBNull)
                    message = null;
                else
                    message = (String)temp;

                temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
                if (temp is System.DBNull)
                    title = null;
                else
                    title = (String)temp;

                switch ((Int32)TestContext.DataRow.ItemArray[(int)LogTestData.Type])
                {
                    case 1:
                        logType = LogType.Warning;
                        break;
                    case 2:
                        logType = LogType.Error;
                        break;
                    case 3:
                        logType = LogType.Debug;
                        break;
                    default:
                        logType = LogType.Normal;
                        break;
                }
                indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

                Log log = new Log(message, title, logType, (UInt16)indent); // Initialize to an appropriate value
                Log target = new Log(log);
                Assert.AreEqual<DateTime>(log.Time, target.Time);
                Assert.AreEqual<String>(log.Message, target.Message);
                Assert.AreEqual<String>(log.Title, target.Title);
                Assert.AreEqual<LogType>(log.Type, target.Type);
                Assert.AreEqual<UInt16>(log.Indent, target.Indent);
            }
        }
        /// <summary>
        /// A test for the default parameters in Copy Constructor of Log class
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogMessageData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_CopyConstructor_DefaultTest()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogMessageTestData.IsValid];
            String message;

            if (valid)
            {
                Object temp = TestContext.DataRow.ItemArray[(int)LogMessageTestData.Message];
                if (temp is System.DBNull)
                    message = null;
                else
                    message = (String)temp;

                Log log = new Log(message); // Initialize to an appropriate value
                Log target = new Log(log);
                Assert.AreEqual<DateTime>(log.Time, target.Time);
                Assert.AreEqual<String>(log.Message, target.Message);
                Assert.AreEqual<String>(log.Title, target.Title);
                Assert.AreEqual<LogType>(log.Type, target.Type);
                Assert.AreEqual<UInt16>(log.Indent, target.Indent);
            }
        }

        /// <summary>
        /// A test for Log Constructor
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Constructor_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            String message, title;
            LogType logType;
            Int32 indent;

            Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
            if (temp is System.DBNull)
                title = null;
            else
                title = (String)temp;

            switch ((Int32)TestContext.DataRow.ItemArray[(int)LogTestData.Type])
            {
                case 1:
                    logType = LogType.Warning;
                    break;
                case 2:
                    logType = LogType.Error;
                    break;
                case 3:
                    logType = LogType.Debug;
                    break;
                default:
                    logType = LogType.Normal;
                    break;
            }
            indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

            if (valid)
            {
                Log log = new Log(message, title, logType, (UInt16)indent);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, title);
                Assert.AreEqual<LogType>(log.Type, logType);
                Assert.AreEqual<UInt16>(log.Indent, (UInt16)indent);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = new Log(message, title, logType, (UInt16)indent);
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
        /// A test for the default parameters in Log Constructor
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogMessageData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Constructor_DefaultTest()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogMessageTestData.IsValid];
            String message;

            Object temp = TestContext.DataRow.ItemArray[(int)LogMessageTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            if (valid)
            {
                Log log = new Log(message);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, String.Empty);
                Assert.AreEqual<LogType>(log.Type, LogType.Normal);
                Assert.AreEqual<UInt16>(log.Indent, 0);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = new Log(message);
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
        #endregion

        #region Method Test
        /// <summary>
        /// A test for CreateDebugLog Method
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateDebugLog_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            String message, title;
            Int32 indent;

            Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
            if (temp is System.DBNull)
                title = null;
            else
                title = (String)temp;

            indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

            if (valid)
            {
                Log log = Log.CreateDebugLog(message, title, (UInt16)indent);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, title);
                Assert.AreEqual<LogType>(log.Type, LogType.Debug);
                Assert.AreEqual<UInt16>(log.Indent, (UInt16)indent);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateDebugLog(message, title, (UInt16)indent);
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
        /// A test for the default parameters in CreateDebugLog Method
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogMessageData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateDebugLog_DefaultTest()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogMessageTestData.IsValid];
            String message;

            Object temp = TestContext.DataRow.ItemArray[(int)LogMessageTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            if (valid)
            {
                Log log = Log.CreateDebugLog(message);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, String.Empty);
                Assert.AreEqual<LogType>(log.Type, LogType.Debug);
                Assert.AreEqual<UInt16>(log.Indent, 0);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateDebugLog(message);
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
        /// A test for CreateErrorLog Method
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateErrorLog_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            String message, title;
            Int32 indent;

            Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
            if (temp is System.DBNull)
                title = null;
            else
                title = (String)temp;

            indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

            if (valid)
            {
                Log log = Log.CreateErrorLog(message, title, (UInt16)indent);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, title);
                Assert.AreEqual<LogType>(log.Type, LogType.Error);
                Assert.AreEqual<UInt16>(log.Indent, (UInt16)indent);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateErrorLog(message, title, (UInt16)indent);
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
        /// A test for the default parameters in CreateErrorLog Method
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogMessageData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateErrorLog_DefaultTest()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogMessageTestData.IsValid];
            String message;

            Object temp = TestContext.DataRow.ItemArray[(int)LogMessageTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            if (valid)
            {
                Log log = Log.CreateErrorLog(message);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, String.Empty);
                Assert.AreEqual<LogType>(log.Type, LogType.Error);
                Assert.AreEqual<UInt16>(log.Indent, 0);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateErrorLog(message);
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
        /// A test for CreateNormalLog Method
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateNormalLog_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            String message, title;
            Int32 indent;

            Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
            if (temp is System.DBNull)
                title = null;
            else
                title = (String)temp;

            indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

            if (valid)
            {
                Log log = Log.CreateNormalLog(message, title, (UInt16)indent);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, title);
                Assert.AreEqual<LogType>(log.Type, LogType.Normal);
                Assert.AreEqual<UInt16>(log.Indent, (UInt16)indent);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateNormalLog(message, title, (UInt16)indent);
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
        /// A test for the default parameters in CreateNormalLog Method
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogMessageData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateNormalLog_DefaultTest()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogMessageTestData.IsValid];
            String message;

            Object temp = TestContext.DataRow.ItemArray[(int)LogMessageTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            if (valid)
            {
                Log log = Log.CreateNormalLog(message);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, String.Empty);
                Assert.AreEqual<LogType>(log.Type, LogType.Normal);
                Assert.AreEqual<UInt16>(log.Indent, 0);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateNormalLog(message);
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
        /// A test for CreateWarningLog Method
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb", 
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateWarningLog_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            String message, title;
            Int32 indent;

            Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
            if (temp is System.DBNull)
                title = null;
            else
                title = (String)temp;

            indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

            if (valid)
            {
                Log log = Log.CreateWarningLog(message, title, (UInt16)indent);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, title);
                Assert.AreEqual<LogType>(log.Type, LogType.Warning);
                Assert.AreEqual<UInt16>(log.Indent, (UInt16)indent);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateWarningLog(message, title, (UInt16)indent);
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
        /// A test for the default parameters in CreateWarningLog Method
        /// </summary>
        [DataSource("System.Data.OleDb", 
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogMessageData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Method_CreateWarningLog_DefaultTest()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogMessageTestData.IsValid];
            String message;

            Object temp = TestContext.DataRow.ItemArray[(int)LogMessageTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            if (valid)
            {
                Log log = Log.CreateWarningLog(message);
                Assert.AreEqual<String>(log.Message, message);
                Assert.AreEqual<String>(log.Title, String.Empty);
                Assert.AreEqual<LogType>(log.Type, LogType.Warning);
                Assert.AreEqual<UInt16>(log.Indent, 0);
            }
            else
            {
                Exception exception = null;
                try
                {
                    Log log = Log.CreateWarningLog(message);
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
        #endregion

        #region Properties Test
        /// <summary>
        /// A test for Indent Property
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Property_Indent_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            if (valid)
            {
                String message, title;
                LogType logType;
                Int32 indent;

                Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
                if (temp is System.DBNull)
                    message = null;
                else
                    message = (String)temp;

                temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
                if (temp is System.DBNull)
                    title = null;
                else
                    title = (String)temp;

                switch ((Int32)TestContext.DataRow.ItemArray[(int)LogTestData.Type])
                {
                    case 1:
                        logType = LogType.Warning;
                        break;
                    case 2:
                        logType = LogType.Error;
                        break;
                    case 3:
                        logType = LogType.Debug;
                        break;
                    default:
                        logType = LogType.Normal;
                        break;
                }
                indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

                Log log = new Log(message, title, logType, (UInt16)indent);

                log.Indent = 5;
                Assert.AreEqual<UInt16>(log.Indent, 5);
            }
        }

        /// <summary>
        /// A test for Message Property
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogMessageData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Property_Message_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogMessageTestData.IsValid];
            String message;

            Object temp = TestContext.DataRow.ItemArray[(int)LogMessageTestData.Message];
            if (temp is System.DBNull)
                message = null;
            else
                message = (String)temp;

            Log log = new Log("Test message", "Test title", LogType.Normal, 0);

            if (valid)
            {
                log.Message = message;
                Assert.AreEqual<String>(log.Message, message);
            }
            else
            {
                Exception exception = null;
                try
                {
                    log.Message = message;
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
        /// A test for Title Property
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Property_Title_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            if (valid)
            {
                String message, title;
                LogType logType;
                Int32 indent;

                Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
                if (temp is System.DBNull)
                    message = null;
                else
                    message = (String)temp;

                temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
                if (temp is System.DBNull)
                    title = null;
                else
                    title = (String)temp;

                switch ((Int32)TestContext.DataRow.ItemArray[(int)LogTestData.Type])
                {
                    case 1:
                        logType = LogType.Warning;
                        break;
                    case 2:
                        logType = LogType.Error;
                        break;
                    case 3:
                        logType = LogType.Debug;
                        break;
                    default:
                        logType = LogType.Normal;
                        break;
                }
                indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

                Log log = new Log(message, title, logType, (UInt16)indent);

                log.Title = "123";
                Assert.AreEqual<String>(log.Title, "123");
            }
        }

        /// <summary>
        /// A test for Type Property
        /// </summary>
        [DataSource("System.Data.OleDb",
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Core\\CoreTest\\TestData.accdb",
            "LogData", DataAccessMethod.Sequential), TestMethod()]
        public void Log_Property_Type_Test()
        {
            Boolean valid = (Boolean)TestContext.DataRow.ItemArray[(int)LogTestData.IsValid];
            if (valid)
            {
                String message, title;
                LogType logType;
                Int32 indent;

                Object temp = TestContext.DataRow.ItemArray[(int)LogTestData.Message];
                if (temp is System.DBNull)
                    message = null;
                else
                    message = (String)temp;

                temp = TestContext.DataRow.ItemArray[(int)LogTestData.Title];
                if (temp is System.DBNull)
                    title = null;
                else
                    title = (String)temp;

                switch ((Int32)TestContext.DataRow.ItemArray[(int)LogTestData.Type])
                {
                    case 1:
                        logType = LogType.Warning;
                        break;
                    case 2:
                        logType = LogType.Error;
                        break;
                    case 3:
                        logType = LogType.Debug;
                        break;
                    default:
                        logType = LogType.Normal;
                        break;
                }
                indent = (Int32)(TestContext.DataRow.ItemArray[(int)LogTestData.Indent]);

                Log log = new Log(message, title, logType, (UInt16)indent);

                log.Type = LogType.Normal;
                Assert.AreEqual<LogType>(log.Type, LogType.Normal);
            }
        }
        #endregion
    }
}