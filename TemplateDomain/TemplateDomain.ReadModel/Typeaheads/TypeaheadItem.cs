namespace TemplateDomain.ReadModel;

public class TypeaheadItem
{
    public string Id { get; set; }
    public string Value { get; set; }
    public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
}
