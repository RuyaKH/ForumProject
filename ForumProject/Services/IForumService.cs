using ForumProject.Models;

namespace ForumProject.Services
{
    public interface IForumService
    {
        Task<ServiceResponse<ForumViewModel>> GetForumItemsAsync(string searchString);
        Task<ServiceResponse<ForumModel>> GetDetailsAsync(int? id);
        Task<ServiceResponse<ForumModel>> CreateThreadAsync(ForumModel thread);
        Task<ServiceResponse<ForumModel>> EditThreadAsync(int? id, ForumModel thread);
        Task<ServiceResponse<ForumModel>> DeleteThreadAsync(int? id);
    }
}
