﻿namespace PODBooking.Repositories.Models
{
    public class OrderInfo
    {
        public string FullName { get; set; }
        public string OrderId { get; set; }
        public string OrderInformation { get; set; }
        public double Amount { get; set; }

        public int BookingId { get; set; }
    }
}
