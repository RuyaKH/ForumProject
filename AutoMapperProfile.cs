using AutoMapper;
using ForumProject.Models;

namespace ForumProject
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ForumModel, ForumViewModel>();
            CreateMap<ForumViewModel, ForumModel>();
        }
    }
}
