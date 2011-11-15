using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Drawing;

namespace BillList.Core.Log
{
    public class Logger
    {

        #region Class Setting
        private const UInt16 ConstCacheLength = 100;                 //  Every 100 non-Error Logs, save the Logs to the File.
        private const UInt16 ConstCacheTimeInterval = 60000;         //  Every 1 minute, save the Logs to the File.

        //  File Name
        private String LogFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_Log";
        private String ErrorLogFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_ErrorLog";

        //  Log String Setting
        private String ConstLogTimeFormatString = "HH:mm:ss.ffff";

        private const Color ConstLogTimeColor = Color.FromArgb(217, 116, 43);
        private const Color ConstLogTitleColor = Color.FromArgb(230, 180, 80);
        private const Color ConstLogWarningMessageColor = Color.FromArgb(153, 77, 82);
        private const Color ConstLogDebugMessageColor = Color.FromArgb(227, 230, 195);
        private const Color ConstLogNormalMessageColor = Color.Black;
        private const Color ConstLogErrorMessageColor = Color.Red;

        private const Font ConstLogTimeFont = new Font("Calibri", 11, FontStyle.Italic);
        private const Font ConstLogTitleFont = new Font("Calibri", 11, FontStyle.Bold);
        private const Font ConstLogMessageFont = new Font("Calibri", 11, FontStyle.Regular);
        #endregion
        
        #region Properties
        /// <summary>
        /// If IsDebugEnabled is false, LoggerRepository will not save the Debug Type log.
        /// </summary>
        public Boolean IsDebugEnabled { get; set; }

        /// <summary>
        /// The path where the logs file will saved.
        /// </summary>
        public String LogFilePath
        {
            get
            {
                return LogFilePath;
            }
            set
            {
                if ((value == null) || (value == String.Empty))
                    throw (new NullReferenceException("Message Property could not be empty."));

                LogFilePath = value;
            }
        }
        /// <summary>
        /// If IsWriteLogFile is true, Logger will save the logs to a file.
        /// </summary>
        public Boolean IsWriteLogFile { get; set; }

        /// <summary>
        /// If IsRTFFile is true, Log file will be saved to a rtf file with a color Log.
        /// </summary>
        public Boolean IsRTFFile { get; set; }
        /// <summary>
        /// The color setting of Log's Time when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogTimeColor
        {
            get
            {
                if (LogTimeColor == null)
                    LogTimeColor = ConstLogTimeColor;
                return LogTimeColor;
            }
            set
            {
                LogTimeColor = value;
            }
        }
        /// <summary>
        /// The color setting of Log's Title when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogTitleColor
        {
            get
            {
                if (LogTitleColor == null)
                    LogTitleColor = ConstLogWarningMessageColor;
                return LogTitleColor;
            }
            set
            {
                LogTitleColor = value;
            }
        }
        /// <summary>
        /// The color setting of Warning Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogWarningMessageColor
        {
            get
            {
                if (LogWarningMessageColor == null)
                    LogWarningMessageColor = ConstLogWarningMessageColor;
                return LogWarningMessageColor;
            }
            set
            {
                LogWarningMessageColor = value;
            }
        }
        /// <summary>
        /// The color setting of Debug Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogDebugMessageColor
        {
            get
            {
                if (LogDebugMessageColor == null)
                    LogDebugMessageColor = ConstLogDebugMessageColor;
                return LogDebugMessageColor;
            }
            set
            {
                LogDebugMessageColor = value;
            }
        }
        /// <summary>
        /// The color setting of Normal Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogNormalMessageColor
        {
            get
            {
                if (LogNormalMessageColor == null)
                    LogNormalMessageColor = ConstLogNormalMessageColor;
                return LogNormalMessageColor;
            }
            set
            {
                LogNormalMessageColor = value;
            }
        }
        /// <summary>
        /// The color setting of Error Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Font LogErrorMessageColor
        {
            get
            {
                if (LogErrorMessageColor == null)
                    LogErrorMessageColor = ConstLogErrorMessageColor;
                return LogErrorMessageColor;
            }
            set
            {
                LogErrorMessageColor = value;
            }
        }

        /// <summary>
        /// The Font setting of Log's Time when Logger use RTF file to save the logs.
        /// </summary>
        public Font LogTimeFont
        {
            get
            {
                if (LogTimeFont == null)
                    LogTimeFont = ConstLogTimeFont;
                return LogTimeFont;
            }
            set
            {
                LogTimeFont = value;
            }
        }
        /// <summary>
        /// The Font setting of Log's Title when Logger use RTF file to save the logs.
        /// </summary>
        public Font LogTitleFont
        {
            get
            {
                if (LogTitleFont == null)
                    LogTitleFont = ConstLogTitleFont;
                return LogTitleFont;
            }
            set
            {
                LogTitleFont = value;
            }
        }
        /// <summary>
        /// The Font setting of Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogMessageFont
        {
            get
            {
                if (LogMessageFont == null)
                    LogMessageFont = ConstLogMessageFont;
                return LogMessageFont;
            }
            set
            {
                LogMessageFont = value;
            }
        }
        #endregion

        #region Members
        //  Logs' Member
        private List<Log> _Logs;
        private List<Log> _ErrorLogs;
        private List<Log> _LogsCache;

        //  Title's Members
        private List<String> _Title;
        private String _TitleString;

        //  Lock Object
        private Object lockthis = new Object();

        //  Timer Threading
        private TimerCallback timerDelegate;
        private Timer timer;
        #endregion

        /// <summary>
        /// Constructor of the LoggerRepository Class.
        /// </summary>
        /// <param name="isWriteLogFile">Set if the logs need to save to a file</param>
        /// <param name="logFilePath">the Path of log files</param>
        public Logger(Boolean isWriteLogFile, String logFilePath, Boolean isRTFFile = true)
        {
            _Logs = new List<Log>();
            _ErrorLogs = new List<Log>();
            _LogsCache = new List<Log>();

            _Title = new List<String>();
            _TitleString = String.Empty;

            IsDebugEnabled = false;

            IsWriteLogFile = isWriteLogFile;
            IsRTFFile = isRTFFile;
            LogFilePath = logFilePath;

            if (IsWriteLogFile)
            {
                //  Start a Thread to write the log files every ConstCacheTimeInterval ms.
                //  Create a TimerCallback, it will run WriteLogs WriteLogs
                timerDelegate = new TimerCallback(WriteLogs);
                //  Create the Timmer Class
                timer = new Timer(timerDelegate, null, ConstCacheTimeInterval, ConstCacheTimeInterval);
            }
        }

        #region Methods
        #region Add Logs
        private void AddLog(Log log)
        {
            Boolean _NeedWriteToFile = false;

            lock (lockthis)
            {
                if (log.Type != LogType.Error)
                {
                    _Logs.Add(log);
                    _LogsCache.Add(log);

                    if (_LogsCache.Count >= ConstCacheLength)
                        _NeedWriteToFile = true;
                }
                else
                {
                    _ErrorLogs.Add(log);
                    _NeedWriteToFile = true;
                }
            }

            //  Need write the Log to log files ASAP
            if ((_NeedWriteToFile) && (IsWriteLogFile))
            {
                WriteLogs(null);
            }
        }
        /// <summary>
        /// Add a message as a Error message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="indent">indent of the message</param>
        public void AddError(String message, String title, UInt32 indent = 0)
        {
            Log tempLog = Log.CreateErrorLog(_TitleString + message, title, indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Add a message as a Warning message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="indent">indent of the message</param>
        public void AddWarning(String message, String title, UInt32 indent = 0)
        {
            Log tempLog = Log.CreateWarningLog(_TitleString + message, title, indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Add a message as a Normal message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="indent">indent of the message</param>
        public void AddNormal(String message, String title, UInt32 indent = 0)
        {
            Log tempLog = Log.CreateNormalLog(_TitleString + message, title, indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Add a message as a Debug message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public void AddDebug(String message, String title, UInt32 indent = 0)
        {
            //  Don't save the Info type message
            if (!IsDebugEnabled)
                return;

            Log tempLog = Log.CreateDebugLog(_TitleString + message, title, indent);
            AddLog(tempLog);
        }
        #endregion
        #region Title Setting
        /// <summary>
        /// Start a Title, all new log message will auto add the Title
        /// </summary>
        /// <param name="title">Title</param>
        public void StartTitle(String title)
        {
            _Title.Add(title);
            ChangeTitleString();
        }
        /// <summary>
        /// Remove last Title.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">When there is no Title</exception>
        public void EndTitle()
        {
            //  If there is no Title anymore
            if (_Title.Count == 0)
            {
                throw new IndexOutOfRangeException("There is no Title anymore");
            }

            //  Remove the last Title
            _Title.RemoveAt(_Title.Count - 1);
            ChangeTitleString();
        }
        /// <summary>
        /// Clear all the Title
        /// </summary>
        public void ClearTitle()
        {
            //  If there is no Title anymore
            if (_Title.Count == 0)
                return;

            _Title.RemoveRange(0, _Title.Count);
            ChangeTitleString();
        }
        /// <summary>
        /// Update the "Show String" of the Title
        /// </summary>
        private void ChangeTitleString()
        {
            _TitleString = String.Empty;

            //  If there is no Title anymore
            if (_Title.Count == 0)
                return;

            _TitleString = _Title[0];
            int count = _Title.Count;
            for (int i = 1; i < count; i++)
                _TitleString += "::" + _Title[i];
        }
        #endregion
        #region Write to File
        /// <summary>
        /// Write the logs to the log files.
        /// </summary>
        /// <param name="state">Use for TimerCallback Class, will not use in the method</param>
        private void WriteLogs(Object state)
        {
            //  private String LogFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_Log";
            //  private String ErrorLogFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_ErrorLog";
            //  private const String ConstLogTimeFormatString = "HH:mm:ss.ffff";

            //  Create a new StreamWriter class to write files
            StreamWriter writer = new StreamWriter(LogFileName, true);

            lock (lockthis)
            {
            }
        }
        #endregion
        #endregion
    }
}