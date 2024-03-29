﻿namespace Wa.Pizza.Infrasctructure.DTO.Basket
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    public class BasketDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime LastModified { get; set; }

        public string? ApplicationUserId { get; set; }

        public ICollection<BasketItemDTO>? BasketItems { get; set; }

    }
}
