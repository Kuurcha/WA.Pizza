using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Model.AuthenticateController;
using Wa.Pizza.Core.Configuration;

[Table(nameof(ApplicationUser))]
[EntityTypeConfiguration(typeof(ApplicationUserConfiguration))]
public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
{
	public ICollection<Adress>? Adresses { get; set; }

	public Basket Basket { get; set; }

	public ICollection<Order>? Orders { get; set; }

	public RefreshToken refreshToken { get; set; }


}
