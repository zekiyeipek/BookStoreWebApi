using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Repositories;
using WebApi.Services.Abstract;
public class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AuthorDto>> GetAllAsync()
    {
        var authors = await _unitOfWork.Authors.GetAllAsync();
        return _mapper.Map<List<AuthorDto>>(authors);
    }

    public async Task<AuthorDto> GetByIdAsync(int id)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);
        if (author == null)
            throw new Exception("Yazar bulunamadı.");

        return _mapper.Map<AuthorDto>(author);
    }

    public async Task AddAsync(CreateAuthorDto dto)
    {
        var author = _mapper.Map<Author>(dto);
        await _unitOfWork.Authors.AddAsync(author);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(UpdateAuthorDto dto)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(dto.Id);
        if (author == null)
            throw new Exception("Yazar bulunamadı.");

        _mapper.Map(dto, author);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id);
        if (author == null)
            throw new Exception("Yazar bulunamadı.");

        if (author.Books.Any())
            throw new Exception("Yayımlanmış kitabı olan yazar silinemez.");

        _unitOfWork.Authors.Remove(author);
        await _unitOfWork.CommitAsync();
    }
}
