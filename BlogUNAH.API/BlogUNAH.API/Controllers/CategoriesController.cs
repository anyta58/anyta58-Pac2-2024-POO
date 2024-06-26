using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlogUNAH.API.Controllers
{
    [ApiController]
    [Route("api/categories")]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public List<CategoryEntity> _categories { get; set; }

        public CategoriesController(ICategoriesService categoriesService) 
        {  
            this._categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoriesService.GetCategoriesLIstAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get( Guid id)
        {
            var category = await _categoriesService.GetCategoryByIdAsync(id);

            if (category == null) 
            {
                return NotFound(new { Message = $"No se encontro la categoria: {id}" });
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryCreateDto category) 
        {
            //bool categoryExist = _categories
            //    .Any(x => x.Name.ToUpper().Trim().Contains(category.Name.ToUpper()));

            //if (!categoryExist)
            //{
            //    return BadRequest("La categoria ya esta registrada.");
            //}

            //category.Id = Guid.NewGuid();

            //_categories.Add(category);
            ////validar que no se pueda ingresar una categoria con el mismo nombre.
            //return Created();

            await _categoriesService.CreateAsync(category);

            return StatusCode(201);
        }

        [HttpPut("{id}")] //se usa por que para actualizar completamente el recurso pach para modificar parcialmente.
        public async Task<ActionResult> Edit(CategoryEditDto category, Guid id) 
        {
            var result = await _categoriesService.EditAsync(category, id);

            if (!result) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(Guid id)
        {
            var categoryResponse = await _categoriesService.GetCategoryByIdAsync(id);

            if (categoryResponse is null)
            {
                return NotFound();
            }

            await _categoriesService.DeleteAsync(id);

            return Ok();
        }

    }
}
