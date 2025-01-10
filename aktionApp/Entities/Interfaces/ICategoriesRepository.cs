namespace aktionApp.Entities.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetAllCategoriesAsync();
        Task<Categories?> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Categories category);
        Task UpdateCategoryAsync(Categories category);
        Task DeleteCategoryAsync(int categoryId);
    }
}
