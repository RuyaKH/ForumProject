using ForumProject.Models;
using Microsoft.PowerBI.Api.Models;

namespace ForumProject.Services
{
    public interface IForumService
    {
        Task<ServiceResponse<ForumViewModel>> GetForumItemsAsync(Adventurer? user, string searchString);
        Task<ServiceResponse<ForumModel>> GetDetailsAsync(Adventurer? user, int? id);
        Task<ServiceResponse<ForumModel>> CreateThreadAsync(Adventurer? user, ForumModel thread);
        Task<ServiceResponse<ForumModel>> EditThreadAsync(Adventurer? user, int? id, ForumModel thread);
        Task<ServiceResponse<ForumModel>> DeleteThreadAsync(Adventurer? user, int? id);
    }
}
