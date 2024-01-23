using ObjectManagementApp.Models;

namespace ObjectManagementApp.Services.Interfaces
{
    public interface IObjectService
    {
        Task<List<CustomObject>> GetAllAsync();
        Task<CustomObject> GetAsync(int? id);
        Task<List<CustomObject>> SearchAsync(string search);
        Task<bool> CustomObjectExistsAsync(int id);
        Task CreateAsync(CustomObject customObject);
        Task UpdateAsync(CustomObject customObject);
        Task DeleteAsync(int id);
    }
}
