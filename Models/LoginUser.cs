using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    

public class LoginUser
{
    // No other fields!
    [Required]
    public string Email {get; set;}
    [Required]
    public string Password { get; set; }
}
