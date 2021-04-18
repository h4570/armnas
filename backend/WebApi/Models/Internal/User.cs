using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models.Internal
{

    public class User : IUser
    {

        public User() { }

        public User(IUser iUser)
        {
            Id = iUser.Id;
            Login = iUser.Login;
            Password = iUser.Password;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
        [Required]
        [JsonIgnore]
        [StringLength(64)]
        public string Password { get; set; }

    }

}
