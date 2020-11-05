namespace Logger
{
    public interface ILogManager
    {
        void Error(string log);
        void Fatal(string log);
        void Info(string log);
    }
}