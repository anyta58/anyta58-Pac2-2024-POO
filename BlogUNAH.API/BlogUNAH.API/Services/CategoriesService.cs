using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace BlogUNAH.API.Services
{
    public class CategoriesService : ICategoriesService
    {
        public readonly string _JSON_FILE;

        public CategoriesService() 
        {
            _JSON_FILE = "SeedData/categories.json";
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            var categoriesDto = await ReadCategoriesFromFileAsync();

            //dto.Id = Guid.NewGuid();

            var categoryDtos = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
            };


            categoriesDto.Add(categoryDtos);

            var categories = categoriesDto.Select(x => new CategoryEntity 
            {
                Id = x.Id,
                Name = x.Name,
                Description= x.Description,
            }).ToList();

            await WriteCategoriesToFileAsync(categories);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFileAsync();
            var categoryToDelete = categoriesDto.FirstOrDefault(x => x.Id == id);

            if (categoryToDelete is null)
            {
                return false;
            }

            categoriesDto.Remove(categoryToDelete);

            var categories = categoriesDto.Select(x => new CategoryEntity
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();


            await WriteCategoriesToFileAsync(categories);

            return true;
        }

        public async Task<bool> EditAsync( CategoryEditDto dto, Guid id)
        {
            var categoriesDto = await ReadCategoriesFromFileAsync();

            var existingCategory = categoriesDto
                .FirstOrDefault(category => category.Id == id);
            if (existingCategory is null) 
            {
                return false;
            }

            //existingCategory.Name = dto.Name;
            //existingCategory.Description = dto.Description;
            //TODO: recordar las  categoriaas y actualizar la categoria correspondientes de la lista 

            for (int i = 0; i < categoriesDto.Count; i++)
            {
                if (categoriesDto[i].Id == id)
                {
                    categoriesDto[i].Name = dto.Name;
                    categoriesDto[i].Description = dto.Description;
                }
            }

            var categories = categoriesDto.Select(x => new CategoryEntity
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            await WriteCategoriesToFileAsync(categories);
            return true;
        }

        public Task<bool> EditAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDto>> GetCategoriesLIstAsync()
        {
            return await ReadCategoriesFromFileAsync();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var categories = await ReadCategoriesFromFileAsync();
            // CategoryDto category = categories.FirstOrDefault(c => c.Id)
            return categories.FirstOrDefault(c => c.Id == id);
        }

        private async Task<List<CategoryDto>> ReadCategoriesFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {
                return new List<CategoryDto>();
            }

            var json = await File.ReadAllTextAsync(_JSON_FILE);

            var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(json);

            var dtos = categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            return dtos;
        }

        private async Task WriteCategoriesToFileAsync(List<CategoryEntity> categories)
        {
            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            if(File.Exists(_JSON_FILE))
            {
               await File.WriteAllTextAsync(_JSON_FILE, json);
            }
        }
    }
}
