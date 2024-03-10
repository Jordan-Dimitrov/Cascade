namespace Users.Application.Dtos
{
    public class UserPatchDto
    {
        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters")]
        public string Username { get; set; }
    }
}
