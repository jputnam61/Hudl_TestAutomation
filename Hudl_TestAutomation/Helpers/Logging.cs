using System.Linq;
using NLog;
using NUnit.Framework;


namespace Hudl_TestAutomation.Helpers
{
    public class HudlLogger
    {
        private Logger _log;

        public HudlLogger()
        {
            _log = LogManager.GetLogger(TestContext.CurrentContext.Test.ClassName.Split('.').Last() + "." + TestContext.CurrentContext.Test.FullName.Split('.').Last());
        }
        public HudlLogger(string loggerName)
        {
            _log = LogManager.GetLogger(loggerName);
        }
        public void Debug(string message)
        {
            _log.Debug(message);
        }
        public void Error(string message)
        {
            _log.Error(message);
        }
        public void Fatal(string message)
        {
            _log.Fatal(message);
        }
        public void Info(string message)
        {
            _log.Info(message);
        }
        public void Warn(string message)
        {
            _log.Warn(message);
        }
    }
}
