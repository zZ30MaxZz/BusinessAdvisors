using SoftTeK.BusinessAdvisors.Dto.Users;

namespace SoftTeK.BusinessAdvisors.Web.Models
{
    public class IndexModel
    {
        public UserDto User { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
