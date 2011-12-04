using System;

namespace BigEgg.Core.Log
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
            get
            {
                return _Time;
            }
        }
        public String Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if ((value == null) || (value.Trim() == String.Empty))
                    throw new ArgumentException("Message property could not be NULL or empty.");

                _Message = value;
            }
        }
        public String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value == null)
                    _Title = String.Empty;
                else
                    _Title = value.Trim();
            }
        }
        public UInt16 Indent
        { 
            get
            { 
                return _Indent;
            }
            set
            {
                _Indent = value;
            }
        }
        public LogType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        #endregion

        #region Members
        private DateTime _Time;
        private String _Message;
        private String _Title;
        private UInt16 _Indent;
        private LogType _Type;
        #endregion

        /// <summary>
        /// Constructor of Log class
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="type">Log type: Error, Warning, Normal or Debug</param>
        /// <param name="indent">indent of the message</param>
        public Log(String message, String title = "", LogType type = LogType.Normal, UInt16 indent = 0)
        {
            if ((message == null) || (message.Trim() == String.Empty))
                throw new ArgumentNullException("message", "The message parameter could not be NULL or empty.");

            _Message = message;
            _Title = title;
            _Type = type;
            _Indent = indent;
            _Time = DateTime.Now;
        }
        /// <summary>
        /// Constructor of Log class
        /// </summary>
        /// <param name="log">A Log class which need to be copied.</param>
        public Log(Log log)
        {
            _Message = log.Message;
            _Title = log.Title;
            Type = log.Type;
            Indent = log.Indent;
            _Time = log.Time;
        }

        #region Methods
        /// <summary>
        /// Create a new message as a Error message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateErrorLog(String message, String title = "", UInt16 indent = 0)
        {
            if ((message == null) || (message.Trim() == String.Empty))
                throw new ArgumentNullException("message", "The message parameter could not be NULL or empty.");

            return new Log(message, title, LogType.Error, indent);
        }
        /// <summary>
        /// Create a new message as a Warning message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateWarningLog(String message, String title = "", UInt16 indent = 0)
        {
            if ((message == null) || (message.Trim() == String.Empty))
                throw new ArgumentNullException("message", "The message parameter could not be NULL or empty.");

            return new Log(message, title, LogType.Warning, indent);
        }
        /// <summary>
        /// Create a new message as a Normal message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateNormalLog(String message, String title = "", UInt16 indent = 0)
        {
            if ((message == null) || (message.Trim() == String.Empty))
                throw new ArgumentNullException("message", "The message parameter could not be NULL or empty.");

            return new Log(message, title, LogType.Normal, indent);
        }
        /// <summary>
        /// Create a new message as a Debug message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="title">Log title</param>
        /// <param name="indent">indent of the message</param>
        public static Log CreateDebugLog(String message, String title = "", UInt16 indent = 0)
        {
            if ((message == null) || (message.Trim() == String.Empty))
                throw new ArgumentNullException("message", "The message parameter could not be NULL or empty.");

            return new Log(message, title, LogType.Debug, indent);
        }
        #endregion
    }
}