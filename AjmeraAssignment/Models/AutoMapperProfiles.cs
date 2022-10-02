using AjmeraAssignment.Models.DTO;
using AutoMapper;

namespace AjmeraAssignment.Models
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, BookDTO>();
        }
    }
}
