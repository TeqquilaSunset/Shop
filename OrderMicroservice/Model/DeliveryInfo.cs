﻿namespace OrderMicroservice.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class DeliveryInfo
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
