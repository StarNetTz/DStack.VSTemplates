using System.Threading.Tasks;

namespace TemplateDomain.ReadModel
{
    public interface IQueryById
    {
        Task<T> GetById<T>(string id);
    }
}