namespace WebApi.Models.Internal
{

    public interface IUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
