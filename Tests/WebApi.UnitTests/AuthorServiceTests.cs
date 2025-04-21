using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;
using WebApi.Services.Abstract;
using WebApi.Repositories;
using WebApi;
using Xunit;

public class AuthorServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AuthorService _service;

    public AuthorServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _service = new AuthorService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAuthorDtoList()
    {
        // Arrange
        var authors = new List<Author> {
            new Author { Id = 1, FirstName = "Ahmet", LastName = "Yılmaz", BirthDate = new DateTime(1980, 1, 1) }
        };
        var authorDtos = new List<AuthorDto> {
            new AuthorDto { Id = 1, FullName = "Ahmet Yılmaz", BirthDate = new DateTime(1980, 1, 1) }
        };

        _unitOfWorkMock.Setup(u => u.Authors.GetAllAsync()).ReturnsAsync(authors);
        _mapperMock.Setup(m => m.Map<List<AuthorDto>>(authors)).Returns(authorDtos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(authorDtos);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAuthorExists_ShouldReturnAuthorDto()
    {
        var author = new Author { Id = 1, FirstName = "Ayşe", LastName = "Demir", BirthDate = new DateTime(1990, 5, 5) };
        var dto = new AuthorDto { Id = 1, FullName = "Ayşe Demir", BirthDate = author.BirthDate };

        _unitOfWorkMock.Setup(u => u.Authors.GetByIdAsync(1)).ReturnsAsync(author);
        _mapperMock.Setup(m => m.Map<AuthorDto>(author)).Returns(dto);

        var result = await _service.GetByIdAsync(1);

        result.Should().BeEquivalentTo(dto);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAuthorNotExists_ShouldThrow()
    {
        _unitOfWorkMock.Setup(u => u.Authors.GetByIdAsync(99)).ReturnsAsync((Author)null);

        Func<Task> act = async () => await _service.GetByIdAsync(99);

        await act.Should().ThrowAsync<Exception>().WithMessage("Yazar bulunamadı.");
    }

    [Fact]
    public async Task AddAsync_ShouldAddAuthorAndCommit()
    {
        var createDto = new CreateAuthorDto
        {
            FirstName = "Ali",
            LastName = "Kaya",
            BirthDate = new DateTime(1975, 10, 10)
        };

        var author = new Author { FirstName = "Ali", LastName = "Kaya", BirthDate = createDto.BirthDate };

        _mapperMock.Setup(m => m.Map<Author>(createDto)).Returns(author);
        _unitOfWorkMock.Setup(u => u.Authors.AddAsync(author)).Returns(Task.CompletedTask);

        await _service.AddAsync(createDto);

        _unitOfWorkMock.Verify(u => u.Authors.AddAsync(author), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenAuthorHasBooks_ShouldThrow()
    {
        var author = new Author
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Author",
            BirthDate = DateTime.Now,
            Books = new List<Book> { new Book() }
        };

        _unitOfWorkMock.Setup(u => u.Authors.GetByIdAsync(1)).ReturnsAsync(author);

        Func<Task> act = async () => await _service.DeleteAsync(1);

        await act.Should().ThrowAsync<Exception>().WithMessage("Yayımlanmış kitabı olan yazar silinemez.");
    }

    [Fact]
    public async Task UpdateAsync_WhenAuthorNotFound_ShouldThrow()
    {
        _unitOfWorkMock.Setup(u => u.Authors.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Author)null);

        var dto = new UpdateAuthorDto { Id = 1, FirstName = "Yeni", LastName = "Ad", BirthDate = DateTime.Now };

        Func<Task> act = async () => await _service.UpdateAsync(dto);

        await act.Should().ThrowAsync<Exception>().WithMessage("Yazar bulunamadı.");
    }
    [Fact]
    public async Task UpdateAsync_WhenAuthorExists_ShouldUpdateAndCommit()
    {
        var existingAuthor = new Author
        {
            Id = 1,
            FirstName = "Eski",
            LastName = "Yazar",
            BirthDate = new DateTime(1985, 1, 1)
        };

        var updateDto = new UpdateAuthorDto
        {
            Id = 1,
            FirstName = "Yeni",
            LastName = "Yazar",
            BirthDate = new DateTime(1980, 5, 5)
        };

        _unitOfWorkMock.Setup(u => u.Authors.GetByIdAsync(1)).ReturnsAsync(existingAuthor);
        _mapperMock.Setup(m => m.Map(updateDto, existingAuthor)); // mapper.Map<TSource, TDestination>

        await _service.UpdateAsync(updateDto);

        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenAuthorHasNoBooks_ShouldDeleteAndCommit()
    {
        var author = new Author
        {
            Id = 2,
            FirstName = "Silinecek",
            LastName = "Yazar",
            BirthDate = DateTime.Now,
            Books = new List<Book>() // boş kitap listesi
        };

        _unitOfWorkMock.Setup(u => u.Authors.GetByIdAsync(2)).ReturnsAsync(author);

        await _service.DeleteAsync(2);

        _unitOfWorkMock.Verify(u => u.Authors.Remove(author), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }
    
}
