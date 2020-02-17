using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Detectify.ServerDetection.API.Entities
{

    [BsonIgnoreExtraElements]
    public class User
    {

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }


        public string FullName
        {
            get
            {
                return $"{this.FirstName } { this.LastName}";
            }
        }

        public User GetUserWithoutPassword()
        {
            return new User()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                UserName = this.UserName
            };
        }

    }
}
