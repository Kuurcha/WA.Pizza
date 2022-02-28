using WA.Pizza.Core.CatalogType;
using System.ComponentModel.DataAnnotations;
public class CatalogItem
{
    public int id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
        
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public CatalogType CatalogType { get; set; }
}
