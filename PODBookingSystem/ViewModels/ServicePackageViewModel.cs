﻿namespace PODBookingSystem.ViewModels
{
    public class ServicePackageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> IncludedServices { get; set; }
    }
}
