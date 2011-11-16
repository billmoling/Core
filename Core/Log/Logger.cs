using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

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
        /// If IsWriteLogFile is true, first Logger will save all the logs to the log files. Then automate start a Thread to write the log files every ConstCacheTimeInterval (60000) millionseconds.
        /// </summary>
        public Boolean IsWriteLogFile
        {
            get
            {
                return IsWriteLogFile;
            }
            set
            {
                IsWriteLogFile = value;

                //  if IsWriteLogFile is true, first Logger will save all the logs to the log files.
                //  Then automate start a Thread to write the log files every ConstCacheTimeInterval millionseconds.
                if (IsWriteLogFile)
                {
                    WriteAllLogs();

                    //  Create a TimerCallback, it will run WriteLogs WriteLogs
                    timerDelegate = new System.Threading.TimerCallback(WriteCacheLogs);
                    //  Create the Timmer Class
                    timer = new System.Threading.Timer(timerDelegate, null, ConstCacheTimeInterval, ConstCacheTimeInterval);
                }
            }
        }

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
        public Color LogErrorMessageColor
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
        public Font LogMessageFont
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
        private Log _ErrorLogsCache;

        //  Title's Members
        private List<String> _Title;
        private String _TitleString;

        //  Lock Object
        private Object lockthis = new Object();

        //  Timer Threading
        private System.Threading.TimerCallback timerDelegate;
        private System.Threading.Timer timer;
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
            _ErrorLogsCache = null;

            _Title = new List<String>();
            _TitleString = String.Empty;

            IsDebugEnabled = false;

            IsWriteLogFile = isWriteLogFile;
            IsRTFFile = isRTFFile;
            LogFilePath = logFilePath;
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
                    _ErrorLogsCache = new Log(log);
                    _NeedWriteToFile = true;
                }
            }

            //  Need write the Log to log files ASAP
            if ((_NeedWriteToFile) && (IsWriteLogFile))
            {
                WriteCacheLogs(null);
            }
        }
        /// <summary>
        /// Add a message as a Error message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="indent">indent of the message</param>
        public void AddError(String message, String title = "", UInt16 indent = 0)
        {
            Log tempLog = Log.CreateErrorLog(_TitleString + message, title, indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Add a message as a Warning message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="indent">indent of the message</param>
        public void AddWarning(String message, String title = "", UInt16 indent = 0)
        {
            Log tempLog = Log.CreateWarningLog(_TitleString + message, title, indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Add a message as a Normal message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="indent">indent of the message</param>
        public void AddNormal(String message, String title = "", UInt16 indent = 0)
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
        public void AddDebug(String message, String title = "", UInt16 indent = 0)
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
        /// Write the Cache logs and the Error Cache log to the log files.
        /// </summary>
        /// <param name="state">Use for TimerCallback Class, will not use in the method</param>
        private void WriteCacheLogs(Object state)
        {
            //  Check if need write the logs to files.
            if (!IsWriteLogFile)
                return;

            //  Create a new StreamWriter class to write files
            StreamWriter writer;

            lock (lockthis)
            {
                #region Trace Log
                //  When the log cache have items
                if (_LogsCache.Count != 0)
                {
                    writer = new StreamWriter(LogFileName + ".rtf", false);

                    //  Check if the output file is the RTF file.
                    if (IsRTFFile)
                    {
                        RichTextBox output = new RichTextBox();
                        output.Text = String.Empty;

                        foreach (Log _log in _LogsCache)
                            output.Rtf += FormatLogRTF(_log);

                        output.SaveFile(writer.BaseStream, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        foreach (Log _log in _LogsCache)
                            writer.WriteLine(FormatLogString(_log));
                    }
                    writer.Close();
                }
                #endregion
                #region Error Log
                //  When the error log cache have items
                if (_ErrorLogsCache != null)
                {
                    writer = new StreamWriter(ErrorLogFileName + ".rtf", true);
                    if (IsRTFFile)
                    {
                        RichTextBox output = new RichTextBox();
                        output.Text = String.Empty;

                        output.Rtf += FormatLogRTF(_ErrorLogsCache);

                        output.SaveFile(writer.BaseStream, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        writer.WriteLine(FormatLogString(_ErrorLogsCache));
                    }
                    writer.Close();
                }
                #endregion
            }

            //  Clear cache logs
            _LogsCache.Clear();
            _ErrorLogsCache = null;
        }
        /// <summary>
        /// Write all the logs and al the Error log to the log files. This will overwrite the log file.
        /// </summary>
        private void WriteAllLogs()
        {
            //  Check if need write the logs to files.
            if (!IsWriteLogFile)
                return;

            //  Create a new StreamWriter class to write files
            StreamWriter writer;

            lock (lockthis)
            {
                #region Trace Log
                //  When the log cache have items
                if (_LogsCache.Count != 0)
                {
                    writer = new StreamWriter(LogFileName + ".rtf", false);

                    //  Check if the output file is the RTF file.
                    if (IsRTFFile)
                    {
                        RichTextBox output = new RichTextBox();
                        output.Text = String.Empty;

                        foreach (Log _log in _Logs)
                            output.Rtf += FormatLogRTF(_log);

                        output.SaveFile(writer.BaseStream, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        foreach (Log _log in _Logs)
                            writer.WriteLine(FormatLogString(_log));
                    }
                    writer.Close();
                }
                #endregion
                #region Error Log
                //  When the log cache have items
                if (_ErrorLogs.Count != 0)
                {
                    writer = new StreamWriter(ErrorLogFileName + ".rtf", true);
                    if (IsRTFFile)
                    {
                        RichTextBox output = new RichTextBox();
                        output.Text = String.Empty;

                        foreach (Log _log in _ErrorLogs)
                            output.Rtf += FormatLogRTF(_log);

                        output.SaveFile(writer.BaseStream, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        foreach (Log _log in _ErrorLogs)
                            writer.WriteLine(FormatLogString(_log));
                    }
                    writer.Close();
                }
                #endregion
            }
        }
        /// <summary>
        /// Format the log information to the String.
        /// </summary>
        /// <param name="log">A log class</param>
        /// <returns>If log is null, will return String.Empty; or return the String</returns>
        private String FormatLogString(Log log)
        {
            if (log == null)
                return String.Empty;

            String _logString = String.Empty;
            UInt16 indentCount = log.Indent;

            //  Add the Indent of the log
            for (int i = 0; i < indentCount; i++)
                _logString += "\t";
            //  Log's Time
            _logString += "[" + log.Time.ToString(ConstLogTimeFormatString) + "] ";
            //  Log's Type. If the type is Normal, will not show it.
            _logString += log.Type == LogType.Normal ? String.Empty : log.Type.ToString() + ": ";
            //  Log's Title
            _logString += log.Title + " ";
            //  Log's Message
            _logString += log.Message;

            return _logString;
        }
        /// <summary>
        /// Format the log information to the RTF String.
        /// </summary>
        /// <param name="log">A log class</param>
        /// <returns>If log is null, will return String.Empty; or return the RTF String</returns>
        private String FormatLogRTF(Log log)
        {
            if (log == null)
                return String.Empty;

            RichTextBox rtfText = new RichTextBox();
            rtfText.Text = String.Empty;

            //  Set the Indent of the log
            rtfText.SelectionIndent = (int)log.Indent;
            //  Log's Time
            rtfText.SelectionColor = ConstLogTimeColor;
            rtfText.SelectionFont = ConstLogTimeFont;
            rtfText.AppendText("[" + log.Time.ToString(ConstLogTimeFormatString) + "] ");
            //  Log's Type. If the type is Normal, will not show it.
            switch (log.Type)
            {
                case LogType.Normal:
                    rtfText.SelectionColor = ConstLogNormalMessageColor;
                    break;
                case LogType.Debug:
                    rtfText.SelectionColor = ConstLogDebugMessageColor;
                    break;
                case LogType.Error:
                    rtfText.SelectionColor = ConstLogErrorMessageColor;
                    break;
                case LogType.Warning:
                    rtfText.SelectionColor = ConstLogWarningMessageColor;
                    break;
            }
            rtfText.SelectionFont = ConstLogMessageFont;
            rtfText.AppendText(log.Type == LogType.Normal ? String.Empty : log.Type.ToString() + ": ");
            //  Log's Title
            rtfText.SelectionColor = ConstLogTitleColor;
            rtfText.SelectionFont = ConstLogTitleFont;
            rtfText.AppendText(log.Title + " ");
            //  Log's Message
            switch (log.Type)
            {
                case LogType.Normal:
                    rtfText.SelectionColor = ConstLogNormalMessageColor;
                    break;
                case LogType.Debug:
                    rtfText.SelectionColor = ConstLogDebugMessageColor;
                    break;
                case LogType.Error:
                    rtfText.SelectionColor = ConstLogErrorMessageColor;
                    break;
                case LogType.Warning:
                    rtfText.SelectionColor = ConstLogWarningMessageColor;
                    break;
            }
            rtfText.SelectionFont = ConstLogMessageFont;
            rtfText.AppendText(log.Message + "\r\n");

            return rtfText.Rtf;
        }
        #endregion
        public void MergeLogger(Logger logger, String title = "", UInt16 indent = 0)
        {
        }
        #endregion
    }
}