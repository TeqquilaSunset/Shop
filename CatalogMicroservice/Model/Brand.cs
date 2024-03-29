﻿namespace Shop.Model
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
