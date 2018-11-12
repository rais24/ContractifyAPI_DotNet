using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contractify_API.Models
{
    public class Users
    {
        private readonly MongoHelper<Users> _user;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyId { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

        public Users() { _user = new MongoHelper<Users>(); } 

        public string Create(Users user)
        {
            user.CreatedDate = DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm tt");
            string msg = "User Already Exist";

            if (!IsUserExist(user.Email))
            {
                var result = _user.Collection.Save(user);

                if(result.DocumentsAffected > 0)
                {
                    msg = "User Created Successfully";
                }
                else
                {
                    msg = "Some Error Occured";
                }
            }

            return msg;
        }

        public bool IsUserExist(string email)
        {
            var query = Query<Users>.EQ(x => x.Email,email);

            var result = _user.Collection.FindOne(query);

            if(result != null)
            {
                return true;
            }

            return false;
        }

        public bool Update(Users user)
        {
            bool isUpdated = false;

            Users oldUser = GetUserById(user.UserId);

            var query = Query<Users>.EQ(x => x.UserId, user.UserId);

            if (string.IsNullOrEmpty(user.Password))
            {
                user.Password = oldUser.Password;
            }

            Users newUser = new Users
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                Contact = user.Contact,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = oldUser.CreatedDate,
                UpdatedDate = DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm tt")
            };

            var replacement = Update<Users>.Replace(newUser);

            var result = _user.Collection.Update(query, replacement);

            if(result.DocumentsAffected > 0)
            {
                isUpdated = true;
            }
            return isUpdated;
        }

        public Users GetUserById(string userId)
        {
            var query = Query<Users>.EQ(x => x.UserId, userId);

            return _user.Collection.FindOne(query);
        }
    }
}