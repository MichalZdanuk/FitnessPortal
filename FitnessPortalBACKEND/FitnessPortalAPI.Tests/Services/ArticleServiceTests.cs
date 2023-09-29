using AutoMapper;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Repositories;
using FitnessPortalAPI.Services;
using NSubstitute;
using Shouldly;

namespace FitnessPortalAPI.Tests.Services
{
    [TestClass]
    public class ArticleServiceTests
    {
        private IArticleRepository _articleRepository;
        private IMapper _mapper;
        private ArticleService _articleService;

        public ArticleServiceTests()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<FitnessPortalMappingProfile>()));
            _articleService = new ArticleService(_articleRepository, _mapper);
        }

        [TestMethod]
        public async Task CreateAsync_ValidInput_ShouldCallRepositoryCreateAsync()
        {
            // arrange
            var userId = 1;
            var createdDto = new CreateArticleDto()
            {
                Title= "TestTitle",
                ShortDescription = "TestDescription",
                Content = "TestContent",
                Category = "TestCategory"
            };

            // act
            await _articleService.CreateAsync(createdDto, userId);

            // assert
            await _articleRepository.Received(1).CreateAsync(Arg.Is<Article>(a => 
                a.Title == createdDto.Title &&
                a.ShortDescription == createdDto.ShortDescription &&
                a.Content == createdDto.Content &&
                a.Category == createdDto.Category &&
                a.DateOfPublication > DateTime.MinValue &&
                a.CreatedById == userId));
        }

        [TestMethod]
        public async Task GetPaginatedAsync_ValidQuery_ShouldReturnPageResult()
        {
            // arrange
            var query = new ArticleQuery()
            { 
                PageNumber = 1,
                PageSize = 10,
            };
            var articles = Enumerable.Range(1, 10).Select(i => new Article { Id = i}).ToList();
            var totalItemsCount = 10;

            _articleRepository.GetAllAsync(query.PageNumber, query.PageSize).Returns(articles);
            _articleRepository.GetTotalCountAsync().Returns(totalItemsCount);

            // act
            var result = await _articleService.GetPaginatedAsync(query);

            // assert
            result.ShouldNotBeNull();
            result.Items.Count.ShouldBe(articles.Count);
            result.TotalItemsCount.ShouldBe(totalItemsCount);
        }

        [TestMethod]
        public async Task GetByIdAsync_ValidArticleId_ShouldReturnArticleDto()
        {
            // arrange
            var articleId = 1;
            var article = new Article()
            {
                Id = articleId,
                Title = "TestTitle",
                ShortDescription = "TestDescription",
                Content = "TestContent",
                Category = "TestCategory"
            };
            _articleRepository.GetByIdAsync(articleId).Returns(article);

            // act
            var result = await _articleService.GetByIdAsync(articleId);

            // assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(articleId);
            result.Title.ShouldBe(article.Title);
            result.ShortDescription.ShouldBe(article.ShortDescription);
            result.Content.ShouldBe(article.Content);
            result.Category.ShouldBe(article.Category);
        }

        [TestMethod]
        public async Task GetByIdAsync_InvalidArticleId_ShouldThrowNotFoundException()
        {
            // arrange
            var invalidArticleId = 999;
            _articleRepository.GetByIdAsync(invalidArticleId).Returns(Task.FromResult<Article?>(null));

            // act
            async Task Act() => await _articleService.GetByIdAsync(invalidArticleId);

            // assert
            var exception = await Should.ThrowAsync<NotFoundException>(Act);
            exception.Message.ShouldBe("Article not found");

        }

        [TestMethod]
        public async Task UpdateAsync_ExistingArticle_ShouldCallRepositoryUpdateAsync()
        {
            // arrange
            var existingArticleId = 1;
            var updateDto = new UpdateArticleDto()
            {
                Title = "Updated_Title",
                ShortDescription = "Updated_Description",
                Content = "Updated_Content",
                Category = "Updated_Category"
            };
            var existingArticle = new Article()
            {
                Id = existingArticleId,
                Title = "Title",
                ShortDescription = "Description",
                Content = "Content",
                Category = "Category"
            };

            _articleRepository.GetByIdAsync(existingArticleId).Returns(existingArticle);

            // act
            await _articleService.UpdateAsync(existingArticleId, updateDto);

            // assert
            await _articleRepository.Received(1).UpdateAsync(Arg.Is<Article>(a =>
                a.Id == existingArticleId &&
                a.Title == updateDto.Title &&
                a.ShortDescription == updateDto.ShortDescription &&
                a.Content == updateDto.Content &&
                a.Category == updateDto.Category
            ));
        }

        [TestMethod]
        public async Task UpdateAsync_NonExistingArticle_ShouldThrowNotFoundException()
        {
            // arrange
            var invalidArticleId = 999;
            var updateDto = new UpdateArticleDto()
            {
                Title = "Updated Title",
                ShortDescription = "Updated Description",
                Content = "Updated Content",
                Category = "Updated Category"
            };

            _articleRepository.GetByIdAsync(invalidArticleId).Returns(Task.FromResult<Article?>(null));

            // act
            async Task Act() => await _articleService.UpdateAsync(invalidArticleId, updateDto);

            // Assert
            var exception = await Should.ThrowAsync<NotFoundException>(Act);
            exception.Message.ShouldBe("Article not found");
        }

        [TestMethod]
        public async Task RemoveAsync_ExistingArticle_ShouldCallRepositoryDeleteAsync()
        {
            // Arrange
            var existingArticleId = 1;
            var existingArticle = new Article()
            {
                Id = existingArticleId,
                Title = "Title",
                ShortDescription = "Description",
                Content = "Content",
                Category = "Category"
            };

            _articleRepository.GetByIdAsync(existingArticleId).Returns(existingArticle);

            // Act
            await _articleService.RemoveAsync(existingArticleId);

            // Assert
            await _articleRepository.Received(1).DeleteAsync(existingArticleId);
        }

        [TestMethod]
        public async Task RemoveAsync_NonExistingArticle_ShouldThrowNotFoundException()
        {
            // Arrange
            var invalidArticleId = 999;

            _articleRepository.GetByIdAsync(invalidArticleId).Returns(Task.FromResult<Article?>(null));

            // Act
            async Task Act() => await _articleService.RemoveAsync(invalidArticleId);

            // Assert
            var exception = await Should.ThrowAsync<NotFoundException>(Act);
            exception.Message.ShouldBe("Article not found");
        }
    }
}
