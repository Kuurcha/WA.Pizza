using FluentValidation.Results;
using Mapster;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.Validators;

namespace Wa.Pizza.Infrasctructure.Data.CQRS.Basket
{
    public class BaseBasketHandler
    {
        internal readonly ApplicationDbContext context;
        internal readonly BasketItemValidator basketItemValidator;
        public BaseBasketHandler(ApplicationDbContext _ctx, BasketItemValidator _basketItemValidator)
        {
            context = _ctx;
            basketItemValidator = _basketItemValidator;

        }
        internal async Task<bool> validateDTOAsync(BasketItemDTO basketItemDTO)
        {

            FluentValidation.Results.ValidationResult results = await basketItemValidator.ValidateAsync(basketItemDTO.Adapt<BasketItem>());
            List<ValidationFailure> failures = results.Errors;

            if (failures.Count > 0)
            {
                string errorString = "";
                foreach (ValidationFailure failure in failures)
                {
                    errorString += System.Environment.NewLine + failure.ErrorMessage;
                }
                throw new WrongDataFormatException(errorString);
            }
            return results.IsValid;
        }
    }
}
