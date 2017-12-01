namespace WebApi.Dtos
{
    public class UserDto
    {
        public double Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Provider { get; set; }
        public string TokenString{ get; set;}
    }
}