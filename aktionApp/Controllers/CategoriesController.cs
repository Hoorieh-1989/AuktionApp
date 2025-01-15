using aktionApp.Entities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuktionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoryRepository;

        //Konstruktor för att injicera beroende
        public CategoriesController(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //Hämta alla kategorier
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            //Hämtar kategorilistan från databasen
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        //Hämta en specifik kategori med ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Kategorin hittades inte.");
            }
            return Ok(category);
        }

        //Skapa en ny kategori (endast tillåtet för auktoriserade användare)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                return BadRequest("Kategorinamn är obligatoriskt.");
            }

            await _categoryRepository.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        //Uppdatera en befintlig kategori
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Kategori-ID matchar inte.");
            }

            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Kategorin hittades inte.");
            }

            existingCategory.Name = category.Name;

            await _categoryRepository.UpdateCategoryAsync(existingCategory);
            return NoContent(); //Returnerar 204 om uppdatering lyckades
        }

        //Ta bort en kategori (endast tillåtet för auktoriserade användare)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Kategorin hittades inte.");
            }

            await _categoryRepository.DeleteCategoryAsync(category);
            return NoContent();
        }
    }
}
