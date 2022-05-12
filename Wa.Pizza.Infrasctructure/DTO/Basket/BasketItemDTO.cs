using System.ComponentModel.DataAnnotations;
using WA.Pizza.Core.CatalogType;

namespace Wa.Pizza.Infrasctructure.DTO.Basket
{
    public class BasketItemDTO
    {
		public int Id { get; set; }
		[Required]
		public string CatalogItemName { get; set; }
		[Required]
		public decimal UnitPrice { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public int BasketId { get; set; }
		[Required]
		public CatalogType? CatalogType { get; set; }
		[Required]
		public int CatalogItemId { get; set; }

		public override bool Equals(object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				BasketItemDTO arrivedObject = (BasketItemDTO)obj;
				return (arrivedObject.Id == this.Id && arrivedObject.BasketId == this.BasketId);
			}
		}
		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ this.CatalogItemName.GetHashCode() ^ this.UnitPrice.GetHashCode() ^ this.Quantity.GetHashCode();
		}

	}
}
