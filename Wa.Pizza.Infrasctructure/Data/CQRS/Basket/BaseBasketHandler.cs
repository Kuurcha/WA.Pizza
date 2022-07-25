
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
        public BaseBasketHandler(ApplicationDbContext _ctx)
        {
            context = _ctx;
        }

    }
}