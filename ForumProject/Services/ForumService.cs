using AutoMapper;
using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ForumProject.Services
{
    public class ForumService : IForumService
    {
        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;
        //private readonly UserManager<Adventurer> _userManager;

        public ForumService(ForumDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ForumModel>> CreateThreadAsync(ForumModel thread)
        {
            var response = new ServiceResponse<ForumModel>();

            //if (user == null)
            //{
            //    response.Success = false;
            //    response.Message = "No User Found";
            //    return response;
            //}

            var threadItem = _mapper.Map<ForumModel>(thread);
            //movieItem.AppUser = user;
            _context.Add(threadItem);
            await _context.SaveChangesAsync();
            response.Data = thread;
            return response;
        }

        public async Task<ServiceResponse<ForumModel>> DeleteThreadAsync(int? id)
        {
            var response = new ServiceResponse<ForumModel>();

            if(_context.Threads == null)
            {
                response.Success = false;
                response.Message = "Entitiy set is null";
                return response;
            }
            //if (user == null)
            //{
            //    response.Success = false;
            //    response.Message = "Not Found";
            //    return response;
            //}
            //if (!await UserIsOwner(user, id))
            //{
            //    response.Success = false;
            //    return response;
            //}

            var thread = await _context.Threads.FindAsync(id);
            if(thread != null)
            {
                _context.Threads.Remove(thread);
            }
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ServiceResponse<ForumModel>> EditThreadAsync(int? id, ForumModel thread)
        {
            var response = new ServiceResponse<ForumModel>();

            //if (user == null)
            //{
            //    response.Success = false;
            //    response.Message = "Not Found";
            //    return response;
            //}
            //if (!await UserIsOwner(user, id))
            //{
            //    response.Success = false;
            //    return response;
            //}

            try
            {
                var threadItem = _mapper.Map<ForumModel>(thread);
                //movieItem.UserAppId = user.Id;
                _context.Update(threadItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                response.Success = false;
                if (!await ThreadExists(thread.Id))
                {
                    return response;
                }
                else
                {
                    throw;
                }
            }
            return response;
        }

        private async Task<bool> ThreadExists(int id)
        {
            return await _context.Threads!.AnyAsync(e => e.Id == id);
        }

        public async Task<ServiceResponse<ForumModel>> GetDetailsAsync(int? id)
        {
            var response = new ServiceResponse<ForumModel>();
            if (id == null || _context.Threads == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }

            var threadItems = await _context.Threads
                .FirstOrDefaultAsync(t => t.Id == id);

            if(threadItems == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            response.Data = _mapper.Map<ForumModel>(threadItems);
            return response;
        }

        public async Task<ServiceResponse<ForumViewModel>> GetForumItemsAsync(string searchString)
        {
            var response = new ServiceResponse<ForumViewModel>();
            bool isNull = _context.Threads.IsNullOrEmpty();

            if(isNull)
            {
                response.Success = false;
                response.Message = "Context is null";
                return response;
            }
            //if (user == null)
            //{
            //    response.Success = false;
            //    response.Message = "No User Found";
            //    return response;
            //}

            var threads = from t in _context.Threads
                          select t;

            if(!string.IsNullOrEmpty(searchString))
            {
                threads = threads.Where(s => s.Title!.Contains(searchString));

            }

            response.Data = new ForumViewModel
            {
                Threads = await threads.ToListAsync()
            };
            return response;
        }
    }
}
