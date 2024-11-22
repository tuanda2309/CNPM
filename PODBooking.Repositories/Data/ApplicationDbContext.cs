using Microsoft.EntityFrameworkCore;
using PODBooking.Repositories.Models;
using PODBooking.Repositories.Models.Momo;
using PODBookingSystem.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<OrderInfo> OrderInfos { get; set; }
    public DbSet<MomoInfoModel> momoInfoModels { get; set; }
    public DbSet<Bank> Banks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Định nghĩa các khóa chính
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Room>().HasKey(r => r.RoomId);
        modelBuilder.Entity<Booking>().HasKey(b => b.BookingId);
        modelBuilder.Entity<Payment>().HasKey(p => p.PaymentId);
        modelBuilder.Entity<Notification>().HasKey(n => n.NotificationId);
        modelBuilder.Entity<OrderInfo>().HasKey(o => o.OrderId);
        modelBuilder.Entity<MomoInfoModel>().HasKey(m => m.Id);
        modelBuilder.Entity<Bank>().HasKey(b => b.BookingId);


        // Thiết lập các mối quan hệ khóa ngoại
        modelBuilder.Entity<Booking>()
            .HasOne<Room>()
            .WithMany()
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.NoAction); 

        modelBuilder.Entity<Booking>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.NoAction); 

        modelBuilder.Entity<Payment>()
            .HasOne<Booking>()
            .WithMany()
            .HasForeignKey(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderInfo>()
            .HasOne<Booking>() 
            .WithMany()
            .HasForeignKey(o => o.BookingId) 
            .OnDelete(DeleteBehavior.Cascade); 
            
        modelBuilder.Entity<MomoInfoModel>()
            .HasOne<OrderInfo>()
            .WithMany()
            .HasForeignKey(m => m.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Bank>()
            .HasOne<Room>()
            .WithMany()
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.NoAction); 

        modelBuilder.Entity<Bank>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
