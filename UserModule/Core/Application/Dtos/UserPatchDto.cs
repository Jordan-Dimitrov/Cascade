using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserPatchDto
    {
        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters")]
        public string Username { get; set; }
    }
}
