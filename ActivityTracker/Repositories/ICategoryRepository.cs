using ActivityTracker.Models;
using System.Collections.Generic;

namespace ActivityTracker.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}