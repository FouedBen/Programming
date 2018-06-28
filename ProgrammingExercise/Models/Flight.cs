using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace ProgrammingExercise.Models
{
    public class Airoport
    {
        [Key]
        public int IdAirport { get; set; }

        [Display(Name = "Name")]  
        public string Name { get; set; }

        public DbGeography Location { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }


    }

    public class Flight
    {
        [Key]
        public int IdFlight { get; set; }

        [Required]
        [Display(Name = "Departure Airport ")]
        public int IdDepartureAirport { get; set; }
     
        public virtual Airoport AiroportDeparture { get; set; }
        [Required]
        [Display(Name = "Destination Airport")]
        public int IdDestinationAirport { get; set; }
       
        public virtual Airoport AiroportDestination { get; set; }
        [Display(Name = "Distance KM")]
        public double? Distance { get; set; }
      
        [Required]
        [Display(Name = "Flight Time (min)")]
        public int? FlightTime { get; set; }
        [Display(Name = "Fuel L")]
        public double? Fuel { get; set; }
    }
    
            public class V_ListFlights
    {
        [Key]
        public int IdFlight { get; set; }

        [Display(Name = "Departure Airport ")]
        public string DepartAiroport { get; set; }

        
        [Display(Name = "Destination Airport")]
        public string DestinationAirport { get; set; }

       
        public double? Distance { get; set; }

        [Required]
        [Display(Name = "Flight Time (min)")]
        public int? FlightTime { get; set; }
        [Display(Name = "Fuel L")]
        public double? Fuel { get; set; }
    }
    public class FlightContext : DbContext
    {
        public FlightContext() : base("name=DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<FlightContext>(null);
            base.OnModelCreating(modelBuilder);



        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airoport> Airoport { get; set; }
        public DbSet<V_ListFlights> V_ListFlights { get; set; }
    }

}