namespace FitnessPortalAPI.DAL.Repositories;
public class ArticleRepository(FitnessPortalDbContext dbContext)
		: IArticleRepository
{
	public async Task<int> CreateAsync(Article article)
	{
		dbContext.Articles.Add(article);
		await dbContext.SaveChangesAsync();
		return article.Id;
	}

	public async Task<List<Article>> GetAllAsync(int pageNumber, int pageSize)
	{
		return await dbContext.Articles
						.Include(a => a.CreatedBy)
						.OrderByDescending(a => a.DateOfPublication)
						.Skip(pageSize * (pageNumber - 1))
						.Take(pageSize)
						.ToListAsync();
	}

	public async Task<Article?> GetByIdAsync(int id)
	{
		return await dbContext.Articles
			.Include(a => a.CreatedBy)
			.FirstOrDefaultAsync(a => a.Id == id);
	}

	public async Task UpdateAsync(Article article)
	{
		dbContext.Entry(article).State = EntityState.Modified;
		await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var article = await GetByIdAsync(id);
		if (article != null)
		{
			dbContext.Articles.Remove(article);
			await dbContext.SaveChangesAsync();
		}
	}

	public async Task<int> GetTotalCountAsync()
	{
		return await dbContext.Articles.CountAsync();
	}
}
