using System;
using CodeFuseAI_BlogCart.ViewModels;

namespace CodeFuseAI_BlogCart.Service
{
	public interface ICartService
	{
		public event Action OnChange;

		Task DecrementCart(ShoppingCart shoppingCart);

		Task IncrementCart(ShoppingCart shoppingCart);
	}
}

