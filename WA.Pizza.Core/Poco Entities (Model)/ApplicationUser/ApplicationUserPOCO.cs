using System;
using System.ComponentModel.DataAnnotations;
public class ApplicationUser
{
	public int id { get; set; }
	[Required]
	public string[] Addressess { get; set; }

	public string RefreshToken { get; set; }

	public DateTime RefreshTokenExpiryTime { get; set; }


}
