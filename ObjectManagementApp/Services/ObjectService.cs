using Microsoft.EntityFrameworkCore;
using ObjectManagementApp.Data;
using ObjectManagementApp.Models;
using ObjectManagementApp.Services.Interfaces;

namespace ObjectManagementApp.Services
{
    public class ObjectService : IObjectService
    {
        private readonly ObjectManagementAppContext _context;

        public ObjectService(ObjectManagementAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create custom object.
        /// </summary>
        /// <param name="customObject"></param>
        /// <returns></returns>
        public async Task CreateAsync(CustomObject customObject)
        {
            _context.Add(customObject);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if custom object exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CustomObjectExistsAsync(int id)
        {
            return await _context.CustomObject.AnyAsync(c => c.Id == id);
        }

        /// <summary>
        /// Delete custom object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var customObject = await _context.CustomObject.FindAsync(id);
            if (customObject != null)
            {
                _context.CustomObject.Remove(customObject);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all custom objects.
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomObject>> GetAllAsync()
        {
            return await _context.CustomObject.ToListAsync();
        }

        /// <summary>
        /// Get custom object by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomObject> GetAsync(int? id)
        {
            return await _context
                .CustomObject
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Search custom object.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<List<CustomObject>> SearchAsync(string search)
        {
            var objects = from c in _context.CustomObject
                          select c;

            if (!string.IsNullOrEmpty(search))
            {
                objects = objects.Where(s => s.Name.Contains(search) || s.Description.Contains(search) || s.Type.Contains(search));
            }

            return await objects.ToListAsync();
        }

        /// <summary>
        /// Update custom object.
        /// </summary>
        /// <param name="customObject"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CustomObject customObject)
        {
            _context.Update(customObject);
            await _context.SaveChangesAsync();
        }
    }
}
