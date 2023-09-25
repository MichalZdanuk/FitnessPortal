using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly FitnessPortalDbContext _dbContext;
        public ArticleRepository(FitnessPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Article article)
        {
            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();
            return article.Id;
        }

        public async Task<List<Article>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Articles
                            .Include(a => a.CreatedBy)
                            .OrderByDescending(a => a.DateOfPublication)
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _dbContext.Articles
                .Include(a => a.CreatedBy)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Article article)
        {
            _dbContext.Entry(article).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await GetByIdAsync(id);
            if (article != null)
            {
                _dbContext.Articles.Remove(article);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _dbContext.Articles.CountAsync();
        }
    }
}
