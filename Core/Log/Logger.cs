﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace BigEgg.Core.Log
{
    public enum LoggerOutput
    {
        NotWriteToFile,
        Regular,
        RTF
    }

    public class Logger
    {
        #region Class Setting
        private const UInt16 ConstCacheLength = 100;                 //  Every 100 non-Error Logs, save the Logs to the File.
        private const UInt16 ConstCacheTimeInterval = 60000;         //  Every 1 minute, save the Logs to the File.

        //  File Name
        private readonly String LogFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_Log";
        //  private readonly String ErrorLogFileName = DateTime.Now.ToString("yyyy-MM-dd") + "_ErrorLog";

        //  Log String Setting
        private String ConstLogTimeFormatString = "HH:mm:ss.ffff";

        private readonly Color ConstLogTimeColor = Color.FromArgb(217, 116, 43);
        private readonly Color ConstLogTitleColor = Color.FromArgb(230, 180, 80);
        private readonly Color ConstLogWarningMessageColor = Color.FromArgb(153, 77, 82);
        private readonly Color ConstLogDebugMessageColor = Color.FromArgb(227, 230, 195);
        private readonly Color ConstLogNormalMessageColor = Color.Black;
        private readonly Color ConstLogErrorMessageColor = Color.Red;

        private readonly Font ConstLogTimeFont = new Font("Calibri", 11, FontStyle.Italic);
        private readonly Font ConstLogTitleFont = new Font("Calibri", 11, FontStyle.Bold);
        private readonly Font ConstLogMessageFont = new Font("Calibri", 11, FontStyle.Regular);
        #endregion
        
        #region Properties
        private Boolean _IsDebugEnabled;
        /// <summary>
        /// If IsDebugEnabled is false, LoggerRepository will not save the Debug Type log.
        /// </summary>
        public Boolean IsDebugEnabled 
        {
            get
            {
                return _IsDebugEnabled;
            }
            set
            {
                _IsDebugEnabled = value;
            }
        }

        private String _LogFilePath;
        /// <summary>
        /// The path where the logs file will saved.
        /// </summary>
        /// <exception cref="ArgumentException">LogFilePath property could not be NULL or empty.</exception>
        public String LogFilePath
        {
            get
            {
                return _LogFilePath;
            }
            set
            {
                if ((value == null) || (value.Trim() == String.Empty))
                    throw new ArgumentException("LogFilePath property could not be NULL or empty.");

                _LogFilePath = value;
            }
        }

        private LoggerOutput _OutputSetting;
        /// <summary>
        /// Get the Output Seeting of the Logger class.
        /// </summary>
        public LoggerOutput OutputSetting
        {
            get
            {
                if (_OutputSetting == null)
                    _OutputSetting = LoggerOutput.NotWriteToFile;
                return _OutputSetting;
            }
        }

        #region Color Seting
        private Color _LogTimeColor;
        /// <summary>
        /// The color setting of Log's Time when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogTimeColor
        {
            get
            {
                return _LogTimeColor;
            }
            set
            {
                _LogTimeColor = value;
            }
        }

        private Color _LogTitleColor;
        /// <summary>
        /// The color setting of Log's Title when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogTitleColor
        {
            get
            {
                return _LogTitleColor;
            }
            set
            {
                _LogTitleColor = value;
            }
        }

        private Color _LogWarningMessageColor;
        /// <summary>
        /// The color setting of Warning Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogWarningMessageColor
        {
            get
            {
                return _LogWarningMessageColor;
            }
            set
            {
                _LogWarningMessageColor = value;
            }
        }

        private Color _LogDebugMessageColor;
        /// <summary>
        /// The color setting of Debug Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogDebugMessageColor
        {
            get
            {
                return _LogDebugMessageColor;
            }
            set
            {
                _LogDebugMessageColor = value;
            }
        }

        private Color _LogNormalMessageColor;
        /// <summary>
        /// The color setting of Normal Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogNormalMessageColor
        {
            get
            {
                return _LogNormalMessageColor;
            }
            set
            {
                _LogNormalMessageColor = value;
            }
        }

        private Color _LogErrorMessageColor;
        /// <summary>
        /// The color setting of Error Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Color LogErrorMessageColor
        {
            get
            {
                return _LogErrorMessageColor;
            }
            set
            {
                _LogErrorMessageColor = value;
            }
        }
        #endregion

        #region Font Seting
        private Font _LogTimeFont;
        /// <summary>
        /// The Font setting of Log's Time when Logger use RTF file to save the logs.
        /// </summary>
        public Font LogTimeFont
        {
            get
            {
                if (_LogTimeFont == null)
                    _LogTimeFont = ConstLogTimeFont;
                return _LogTimeFont;
            }
            set
            {
                LogTimeFont = value;
            }
        }

        private Font _LogTitleFont;
        /// <summary>
        /// The Font setting of Log's Title when Logger use RTF file to save the logs.
        /// </summary>
        public Font LogTitleFont
        {
            get
            {
                if (_LogTitleFont == null)
                    _LogTitleFont = ConstLogTitleFont;
                return _LogTitleFont;
            }
            set
            {
                _LogTitleFont = value;
            }
        }

        private Font _LogMessageFont;
        /// <summary>
        /// The Font setting of Log's Message when Logger use RTF file to save the logs.
        /// </summary>
        public Font LogMessageFont
        {
            get
            {
                if (_LogMessageFont == null)
                    _LogMessageFont = ConstLogMessageFont;
                return _LogMessageFont;
            }
            set
            {
                _LogMessageFont = value;
            }
        }
        #endregion
        #endregion

        #region Members
        //  Logs' Member
        private List<Log> _Logs;
        //private List<Log> _ErrorLogs;
        private List<Log> _LogsCache;
        //private Log _ErrorLogsCache;

        //  Title's Members
        private List<String> _Title;
        private String _TitleString;

        //  Indent's member
        private UInt16 _Indent;

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
        public Logger(String logFilePath, 
            LoggerOutput outputSetting = LoggerOutput.NotWriteToFile,
            Boolean isDebugEnabled = false)
        {
            _IsDebugEnabled = isDebugEnabled;
            _OutputSetting = outputSetting;
            LogFilePath = logFilePath;
            AddLog("Start Initialize Logger Class", "Logger::Logger()", LogType.Debug);
            
            //  Initialize Logs' Member
            _Logs = new List<Log>();
            _LogsCache = new List<Log>();

            //  Initialize Title's Members
            _Title = new List<String>();
            _TitleString = String.Empty;

            //  Initialize Indent's Members
            _Indent = 0;

            InitializeRTFString();
            AddLog("Initialize Logger Class End", "Logger::Logger()", LogType.Debug);

            //  Output Thread Setting
            OutputThread();
        }

        /// <summary>
        /// Initialize the Format of RTF String 
        /// </summary>
        private void InitializeRTFString()
        {
            AddLog("Start Initialize the Format of RTF String", 
                "Logger::InitializeRTFString()", LogType.Debug);
            _LogTimeColor = ConstLogTimeColor;
            _LogTitleColor = ConstLogTitleColor;
            _LogWarningMessageColor = ConstLogWarningMessageColor;
            _LogDebugMessageColor = ConstLogDebugMessageColor;
            _LogNormalMessageColor = ConstLogNormalMessageColor;
            _LogErrorMessageColor = ConstLogErrorMessageColor;

            _LogTimeFont = ConstLogTimeFont;
            _LogTitleFont = ConstLogTitleFont;
            _LogMessageFont = ConstLogMessageFont;
            AddLog("Initialize the Format of RTF String End", 
                "Logger::InitializeRTFString()", LogType.Debug);
        }

        #region Methods
        #region Add Logs
        /// <summary>
        /// Add the customer Log into the logger.
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="logType">Log type</param>
        /// <param name="indent">Log indent</param>
        private void AddLog(String message, String title = "", LogType logType = LogType.Normal, UInt16 indent = 0)
        {
            Log tempLog = new Log(message, title, logType, indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Add the log into the logger.
        /// </summary>
        /// <param name="log">A new Log</param>
        private void AddLog(Log log)
        {
            Boolean _NeedWriteToFile = false;

            lock (lockthis)
            {
                _Logs.Add(log);
                _LogsCache.Add(log);

                if (_LogsCache.Count >= ConstCacheLength)
                    _NeedWriteToFile = true;

                if (log.Type == LogType.Error)
                    _NeedWriteToFile = true;
            }

            //  Need write the Log to log files ASAP
            if ((_NeedWriteToFile) && (_OutputSetting != LoggerOutput.NotWriteToFile))
            {
                OutputCacheLogs(null);
            }
        }

        /// <summary>
        /// Create a Log as a Error message and add it into logger
        /// </summary>
        /// <param name="message">Log message</param>
        public void AddError(String message)
        {
            Log tempLog = Log.CreateErrorLog(message, _TitleString, _Indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Create a Log as a Warning message and add it into logger
        /// </summary>
        /// <param name="message">Log message</param>
        public void AddWarning(String message)
        {
            Log tempLog = Log.CreateWarningLog(message, _TitleString, _Indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Create a Log as a Normal message and add it into logger
        /// </summary>
        /// <param name="message">Log message</param>
        public void AddNormal(String message)
        {
            Log tempLog = Log.CreateNormalLog(message, _TitleString, _Indent);
            AddLog(tempLog);
        }
        /// <summary>
        /// Create a Log as a Debug message and add it into logger
        /// </summary>
        /// <param name="message">Log message</param>
        public void AddDebug(String message)
        {
            //  Don't save the Debug type message, when IsDebugEnabled is false.
            if (!IsDebugEnabled)
                return;

            Log tempLog = Log.CreateDebugLog(message, _TitleString, _Indent);
            AddLog(tempLog);
        }
        #endregion

        #region Title Setting
        /// <summary>
        /// Start a Title, all new log message will auto add the Title
        /// </summary>
        /// <param name="title">Title</param>
        public void TitleStart(String title)
        {
            _Title.Add(title);
            ChangeTitleString();
        }
        /// <summary>
        /// Remove last Title.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">When there is no Title</exception>
        public void TitleEnd()
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

        #region Indent Setting
        /// <summary>
        /// Indent add 1, all new log message will auto add the Title
        /// </summary>
        /// <param name="title">Title</param>
        public void IndentAdd(String title)
        {
            _Indent++;
        }
        /// <summary>
        /// Indent minus 1, all new log message will auto add the Title
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">When there is no indent</exception>
        public void IndentMinus()
        {
            //  If there is no indent anymore
            if (_Indent == 0)
            {
                throw new IndexOutOfRangeException("There is no indent anymore");
            }

            //  Indent minus 1
            _Indent--;
        }
        /// <summary>
        /// Clear all the Indent
        /// </summary>
        public void ClearIndent()
        {
            _Indent = 0;
        }
        #endregion

        #region Output Setting
        /// <summary>
        /// Set the new Output Setting.
        /// </summary>
        /// <param name="newOutputSetting">The new output setting. If it is NotWriteToFile, it will stop the output Thread</param>
        public void OutputSettingChange(LoggerOutput newOutputSetting)
        {
            //  If the new Setting as same as the Current Setting, just ignore.
            if (_OutputSetting == newOutputSetting)
                return;

            //  If the Current Setting will have output. First output all the logs.
            if (_OutputSetting != LoggerOutput.NotWriteToFile)
            {
                AddLog("Output all the logs, before the Output Setting is changed.", 
                    "Logger::OutputSettingChange(LoggerOutput)", LogType.Debug);
                OutputCacheLogs(null);
            }

            //  Change the Output Setting
            _OutputSetting = newOutputSetting;
            AddLog("The Output is set to " + newOutputSetting.ToString(),
                    "Logger::OutputSettingChange(LoggerOutput)", LogType.Debug);

            //  Output Thread Setting
            OutputThread();
        }
        /// <summary>
        /// Set the output thread. If the Output Setting have the output, it will start a thread to run the OutputCacheLogs function.
        /// </summary>
        private void OutputThread()
        {
            //  If the Output Setting have the output.
            if (_OutputSetting != LoggerOutput.NotWriteToFile)
            {
                AddLog("Start a Thread to execute the output.", "Logger::OutputThread()", LogType.Debug);

                //  Create a TimerCallback, it will run OutputCacheLogs(Object)
                timerDelegate = new System.Threading.TimerCallback(OutputCacheLogs);
                //  Create the Timer Class, to set the TimerCallback will run every ConstCacheTimeInterval millisecond.
                timer = new System.Threading.Timer(timerDelegate, null, ConstCacheTimeInterval, ConstCacheTimeInterval);

                AddLog("Start a Thread to execute the output Succeed.", "Logger::OutputThread()", LogType.Debug);
            }
            else
            {
                //  Close the Thread
                timer.Dispose();
                AddLog("Close the output Thread.", "Logger::OutputThread()", LogType.Debug);
            }
        }
        /// <summary>
        /// Output the Cache logs and the Error Cache log
        /// </summary>
        /// <param name="state">Use for TimerCallback Class, will not use in the method</param>
        private void OutputCacheLogs(Object state)
        {
            //  Return if there don't have any logs to output
            if (_LogsCache.Count == 0)
                return;

            switch (_OutputSetting)
            {
                case LoggerOutput.NotWriteToFile:
                    AddLog("The Output is NotWriteToFile, don't need output.",
                        "Logger::OutputCacheLogs(state)", LogType.Debug);
                    return;
                case LoggerOutput.Regular:
                    WriteRegularCacheLogs();
                    break;
                case LoggerOutput.RTF:
                    WriteRTFCacheLogs();
                    break;
            }
        }
        #endregion

        #region Output
        #region Output String Format
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

        #region Regular Output
        /// <summary>
        /// Write the Cache logs and the Error Cache log to the Regular log file.
        /// </summary>
        private void WriteRegularCacheLogs()
        {
            //  If the Output Setting is not the LoggerOutput.Regular.
            if (_OutputSetting != LoggerOutput.Regular)
            {
                AddLog("The Output is not Regular, output have error.",
                    "Logger::WriteRegularCacheLogs()", LogType.Debug);
                return;
            }

            lock (lockthis)
            {
                AddLog("Lock the list and output the logs.",
                    "Logger::WriteRegularCacheLogs()", LogType.Debug);

                //  Create a new StreamWriter class to write files
                StreamWriter writer;

                //  When the log cache have items
                if (_LogsCache.Count != 0)
                {
                    //  When the log cache have items
                    if (_LogsCache.Count != 0)
                    {
                        writer = new StreamWriter(LogFileName + ".log", false);

                        foreach (Log _log in _LogsCache)
                            writer.WriteLine(FormatLogString(_log));
                        writer.Close();
                    }
                }

            }

            //  Clear cache logs
            _LogsCache.Clear();
        }
        #endregion

        #region RTF Output
        /// <summary>
        /// Write the Cache logs and the Error Cache log to the Regular log file.
        /// </summary>
        private void WriteRTFCacheLogs()
        {
            //  If the Output Setting is not the LoggerOutput.Regular.
            if (_OutputSetting != LoggerOutput.RTF)
            {
                AddLog("The Output is not RTF, output have error.",
                    "Logger::WriteRTFCacheLogs()", LogType.Debug);
                return;
            }

            lock (lockthis)
            {
                AddLog("Lock the list and output the logs.",
                    "Logger::WriteRTFCacheLogs()", LogType.Debug);
                //  Create a new StreamWriter class to write files
                StreamWriter writer;

                //  When the log cache have items
                if (_LogsCache.Count != 0)
                {
                    //  When the log cache have items
                    if (_LogsCache.Count != 0)
                    {
                        writer = new StreamWriter(LogFileName + ".rtf", false);

                        RichTextBox output = new RichTextBox();
                        output.Text = String.Empty;

                        foreach (Log _log in _LogsCache)
                            output.Rtf += FormatLogRTF(_log);

                        output.SaveFile(writer.BaseStream, RichTextBoxStreamType.RichText);

                        writer.Close();
                    }
                }

            }

            //  Clear cache logs
            _LogsCache.Clear();
        }
        #endregion
        #endregion

        /// <summary>
        /// Add all the logs form a Logger class
        /// </summary>
        /// <param name="source">The source Logger class </param>
        /// <param name="title">Add title to the logs in the source</param>
        /// <param name="indent">Add indent to the logs in the source</param>
        public void MergeLogger(Logger source)
        {
            if (source == null)
                throw new ArgumentNullException("source", "The merge source could not be NULL");

            //  Check if the source have logs.
            if (source._Logs.Count == 0)
            {
                AddLog("The source don't have any logs.",
                    "Logger::MergeLogger(source)", LogType.Debug);
                return;
            }

            AddLog("Start merge.",
                "Logger::MergeLogger(source)", LogType.Debug);

            //  Merge logs
            foreach (Log _logs in source._Logs)
            {
                //  Add title
                if (_logs.Title == String.Empty)
                    _logs.Title = _TitleString;
                else
                    _logs.Title = _TitleString + "::" + _logs.Title;

                //  Add indent
                _logs.Indent += _Indent;

                AddLog(_logs);
            }
        }
        #endregion
    }
}