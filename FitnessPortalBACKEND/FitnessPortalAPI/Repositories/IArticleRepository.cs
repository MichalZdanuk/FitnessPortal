namespace FitnessPortalAPI.Repositories
{
	public interface IArticleRepository
    {
        Task<int> CreateAsync(Article article);
        Task<List<Article>> GetAllAsync(int pageNumber, int pageSize);
        Task<Article?> GetByIdAsync(int id);
        Task UpdateAsync(Article article);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}
