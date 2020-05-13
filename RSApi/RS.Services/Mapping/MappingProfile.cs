using AutoMapper;
using RS.Domain.Models.Data;
using RS.Domain.Models.Views;

namespace RS.Services.Mapping
{
    /// <summary>
    /// The MappingProfile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile() {
            CreateMap<AppUser, AppUserModel>();
            CreateMap<AppUserModel, AppUser>();
        }
    }
}