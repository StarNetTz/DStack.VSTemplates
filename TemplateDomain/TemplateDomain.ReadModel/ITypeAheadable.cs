namespace TemplateDomain.ReadModel
{
    public interface ITypeAheadable
    {
        TypeaheadItem CovertToTypeaheadItem();
        TypeaheadItem CovertToTypeaheadItem(string lng);
    }
}
