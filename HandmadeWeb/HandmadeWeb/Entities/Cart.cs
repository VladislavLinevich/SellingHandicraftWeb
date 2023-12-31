﻿namespace HandmadeWeb.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
