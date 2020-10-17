namespace TimManager.LogViewer.SearchLog
{
    public interface IBuilderLogItem
    {
        LogItem GetLogItem(string line);
    }
}