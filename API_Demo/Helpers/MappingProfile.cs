using AutoMapper;

namespace API_Demo_V2.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieDto, Movie>()
                 .ForMember(src => src.Poster, opt => opt.Ignore());// Ignore some proparty so the auto-mapper willn't map them
        }
    }
}
