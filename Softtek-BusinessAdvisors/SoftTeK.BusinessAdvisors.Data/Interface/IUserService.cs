using SoftTeK.BusinessAdvisors.Data.Entities;

namespace SoftTeK.BusinessAdvisors.Data.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(User user);
        void Update(int id, User user);
        void Delete(int id);
        User GetByEmailAndPassword(string email, string password);
    }
}
