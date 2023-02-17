using SoftTeK.BusinessAdvisors.Dto.Users;

namespace SoftTeK.BusinessAdvisors.Api.Helpers
{
    public interface IBuildTokenJwt
    {
        UserDto BuildJwt(UserDto userDto);
    }
}