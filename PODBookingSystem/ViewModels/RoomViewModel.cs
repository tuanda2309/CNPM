namespace PODBookingSystem.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string? ImageUrl { get; set; }


        public double Price { get; set; }


        public string Address { get; set; }

        public string OwnerName { get; set; }

        public DateTime DatePosted { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
