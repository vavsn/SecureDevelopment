using CardStorageService.Data;

namespace CardStorageService.Services
{
    public interface ICardRepositoryService : IRepository<Card, string>
    {
        IList<Card> GetByCardId(string id);
    }
}
