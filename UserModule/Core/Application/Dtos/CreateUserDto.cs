namespace Users.Application.Dtos
{
    public class CreateUserDto
    {
        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters")]
        public string Username { get; set; }
        //[Required]
        //[RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[@#$%^&+=]).{8,}$")]
        public string Password { get; set; }
    }
}
