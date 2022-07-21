using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.ReccuringJobs
{
    public class ForgottenBasketJob
    {
        private readonly SMTPService _SMTPService;
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// ForgottenBasketJob constructor for DI injection
        /// </summary>
        /// <param name="sMTPService"></param>
        /// <param name="context"></param>
        public ForgottenBasketJob(SMTPService sMTPService, ApplicationDbContext context)
        {
            _SMTPService = sMTPService;
            _context = context;
        }

        public async Task Run()
        {
            var BasketsWithUsers = _context.Basket.Include(b => b.ApplicationUser).Include(b => b.BasketItems).Where(b => b.ApplicationUser != null && b.ApplicationUser.Email != null && b.BasketItems != null & b.BasketItems.Count > 0);
            if (BasketsWithUsers != null)
            {
                foreach (Basket basket in BasketsWithUsers)
                {
                    var userEmailToSendTo = basket.ApplicationUser.Email;
                    string message = "Hello, " + basket.ApplicationUser.UserName + " you didn't finish making your order.";         
                    foreach (BasketItem basketItem in basket.BasketItems)
                    {
                        message += Environment.NewLine;
                        message += basketItem.CatalogItemName + " x" + basketItem.Quantity + " " + basketItem.UnitPrice + "$"; 
                    }
                    await _SMTPService.sendMailAsync
                        (userEmailToSendTo, 
                        "You forgot to confirm your order", 
                        message);
                }
            }
        }





    }
}
