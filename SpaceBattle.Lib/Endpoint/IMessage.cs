public interface IMessage
{
    public string Type { get; }
    public string GameID { get; }
    public string GameItemID { get; }
    public IDictionary<string, object> Properties { get; }
}
