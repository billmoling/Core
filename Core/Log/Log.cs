using System;

namespace BillList.Core.Log
{
    public enum LogType
    {
        Error,
        Warning,
        Normal,
        Debug
    }

    public class Log
    {
        #region Properties
        public DateTime Time
        {
            get;
            private set;
        }
        public String Message
        {
            get
            {
                return Message;
            }
            set
            {
                if ((value == null) || (value == String.Empty))
                    throw (new NullReferenceException("Message Property could not be empty."));

                Message = value;
            }
        }
        public String Title
        {
            get
            {
                return Title;
            }
            set
            {
                if (value == null)
                    Title = String.Empty;
                else
                    Title = value;
            }
        }
        public UInt16 Indent { get; set; }
        public LogType Type { get; set; }
        #endregion

        /// <summary>
        /// Constructor of Log class
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="type">Log type: Error, Warning, Normal or Debug</param>
        /// <param name="indent">indent of the message</param>
        public Log(String message, String title, LogType type = LogType.Normal, UInt16 indent = 0)
        {
            Message = message;
            Title = title;
            Type = type;
            Indent = indent;
            Time = DateTime.Now;
        }
        /// <summary>
        /// Constructor of Log class
        /// </summary>
        /// <param name="log">A Log class which need to be copied.</param>
        public Log(Log log)
        {
            Message = log.Message;
            Title = log.Title;
            Type = log.Type;
            Indent = log.Indent;
            Time = log.Time;
        }

        #region Methods
        /// <summary>
        /// Create a new message as a Error message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateErrorLog(String message, String title, UInt16 indent = 0)
        {
            return new Log(message, title, LogType.Error, indent);
        }
        /// <summary>
        /// Create a new message as a Warning message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateWarningLog(String message, String title, UInt16 indent = 0)
        {
            return new Log(message, title, LogType.Warning, indent);
        }
        /// <summary>
        /// Create a new message as a Normal message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateNormalLog(String message, String title, UInt16 indent = 0)
        {
            return new Log(message, title, LogType.Normal, indent);
        }
        /// <summary>
        /// Create a new message as a Debug message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateDebugLog(String message, String title, UInt16 indent = 0)
        {
            return new Log(message, title, LogType.Debug, indent);
        }
        #endregion
    }
}