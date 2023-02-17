﻿using SoftTeK.BusinessAdvisors.Data.Entities;
using SoftTeK.BusinessAdvisors.Data.Interface;

namespace SoftTeK.BusinessAdvisors.Data.Repository
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Create(User model)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new Exception("Usuario con email '" + model.Email + "' ya existe");

            model.PasswordHash = EnryptString(model.Password!);

            _context.Users.Add(model);
            _context.SaveChanges();
        }

        public void Update(int id, User model)
        {
            var user = getUser(id);

            if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
                throw new Exception("Usuario con email '" + model.Email + "' ya existe");

            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = EnryptString(model.Password!);

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}
