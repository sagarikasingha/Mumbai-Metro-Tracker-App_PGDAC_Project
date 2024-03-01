using Microsoft.EntityFrameworkCore;
namespace MyWebApp.Models;
public class SiteDbContext : DbContext
{

    public DbSet<Admin> admins {get; set ;}
    
    public DbSet<User> users {get; set ;}

    public DbSet<Station> stations {get ; set;}

    public DbSet<RouteLine> routeLines { get ; set;}
    
    public DbSet<RouteStation> routeStations { get ; set;}

    public DbSet<TimeTable> timeTables { get;  set;}

    public DbSet<Feedback> feedbacks {get ; set;}


      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseMySQL("Data Source=localhost; Database=metro; User Id=root; Password=password");
   }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasKey(u => u.Username);
        modelBuilder.Entity<User>().Property(x=>x.Username).HasColumnName("username");
        modelBuilder.Entity<User>().Property(x=>x.Name).HasColumnName("name");
        modelBuilder.Entity<User>().Property(x=>x.Email).HasColumnName("email");
        modelBuilder.Entity<User>().Property(x=>x.Password).HasColumnName("password");

        modelBuilder.Entity<Admin>().ToTable("admin");
        modelBuilder.Entity<Admin>().HasKey(a => a.Username);
        modelBuilder.Entity<Admin>().Property(x=>x.Username).HasColumnName("username");
        modelBuilder.Entity<Admin>().Property(x=>x.Password).HasColumnName("password");
        
        modelBuilder.Entity<RouteLine>().ToTable("route_lines");
        modelBuilder.Entity<RouteLine>().HasKey(u => u.RouteId);
        modelBuilder.Entity<RouteLine>().Property(x=>x.RouteId).HasColumnName("route_id");
        modelBuilder.Entity<RouteLine>().Property(x=>x.Name).HasColumnName("name");

        modelBuilder.Entity<RouteStation>().ToTable("route_stations");
        modelBuilder.Entity<RouteStation>().HasKey(u => u.Id);
        modelBuilder.Entity<RouteStation>().Property(x=>x.StationIdFrom).HasColumnName("station_id_from");
        modelBuilder.Entity<RouteStation>().Property(x=>x.StationIdTo).HasColumnName("station_id_to");
        modelBuilder.Entity<RouteStation>().Property(x=>x.Stop).HasColumnName("stop");
        modelBuilder.Entity<RouteStation>().Property(x=>x.Time).HasColumnName("time");
        
        // modelBuilder.Entity<RouteStation>()
        //     .Property(p => p.Time)
        //     .HasConversion(
        //         v => TimeSpan.FromMinutes(v), // Convert int to TimeSpan
        //         v => (int)v.TotalMinutes)    // Convert TimeSpan to int
        //         .HasColumnType("int");
                
                
        // modelBuilder.Entity<RouteStation>()
        // .Property(rs => rs.Time)
        // .HasColumnName("time")
        // .HasColumnType("int"); // Map the time property to int data type

   
        // modelBuilder.Entity<RouteStation>()
        //     .Property(p => p.Time)
        //     .HasConversion(
        //         v => TimeSpan.FromTicks(v), // Convert int to TimeSpan
        //         v => v.Ticks)               // Convert TimeSpan to long
        //     .HasColumnType("TIME");         // Set the column type to TIME in the database
  

        modelBuilder.Entity<RouteStation>().Property(x=>x.Fare).HasColumnName("fare");
        modelBuilder.Entity<RouteStation>().Property(x=>x.InterchangeStops).HasColumnName("interchange_stops");
        
        modelBuilder.Entity<Station>().ToTable("stations");
        modelBuilder.Entity<Station>().HasKey(u => u.StationId);
        modelBuilder.Entity<Station>().Property(x=>x.StationId).HasColumnName("station_id");
        modelBuilder.Entity<Station>().Property(x=>x.StationName).HasColumnName("station_name");
        modelBuilder.Entity<Station>().Property(x=>x.RouteId).HasColumnName("route_id");
        modelBuilder.Entity<Station>().Property(x=>x.PreviousStationId).HasColumnName("previous_station_id");
        modelBuilder.Entity<Station>().Property(x=>x.NextStationId).HasColumnName("next_station_id");

        modelBuilder.Entity<TimeTable>().ToTable("time_table");
        modelBuilder.Entity<TimeTable>().HasKey(u => u.Id);
        modelBuilder.Entity<TimeTable>().Property(x=>x.Id).HasColumnName("id");
        modelBuilder.Entity<TimeTable>().Property(x=>x.StationIdFrom).HasColumnName("station_id_from");
        modelBuilder.Entity<TimeTable>().Property(x=>x.StationIdTo).HasColumnName("station_id_to");
        modelBuilder.Entity<TimeTable>().Property(x=>x.FirstTrain).HasColumnName("first_train");
        modelBuilder.Entity<TimeTable>().Property(x=>x.LastTrain).HasColumnName("last_train");

        modelBuilder.Entity<Feedback>().ToTable("feedbacks");
    

    }
}


