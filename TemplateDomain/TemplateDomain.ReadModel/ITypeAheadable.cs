namespace TemplateDomain.ReadModel;

public interface ITypeAheadable
{
    RefEx CovertToTypeaheadItem();
    RefEx CovertToTypeaheadItem(string lng);
}
