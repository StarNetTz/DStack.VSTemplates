namespace TemplateDomain.ReadModel;
public class Organization : ITypeAheadable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }

    public TypeaheadItem CovertToTypeaheadItem()
    {

        return new TypeaheadItem
        {
            Id = Starnet.Common.Utils.IdUtils.ToInt64(Id).ToString(),
            Value = Name
        };
    }

    public TypeaheadItem CovertToTypeaheadItem(string lng)
    {
        return CovertToTypeaheadItem();
    }
}
