//using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;

namespace BlogUNAH.API.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<CategoryDto>> GetCategoriesLIstAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<bool> CreateAsync(CategoryCreateDto dto);
        Task<bool> EditAsync(CategoryEditDto dto, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
