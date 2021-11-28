using System.Text;

namespace JobRunner
{
    public class LogTextProducer
    {
        public void AppendMessage(StringBuilder s, string message)
        {
            s.Append(message);
            s.AppendLine();
            s.AppendLine();
        }

        public void AppendCloseMessage(StringBuilder s)
        {
            s.Append("The application will close.");
            s.AppendLine();
            s.AppendLine();
            s.AppendLine("To be able to edit the job list, start JobRunner as administrator.");
        }
    }
}