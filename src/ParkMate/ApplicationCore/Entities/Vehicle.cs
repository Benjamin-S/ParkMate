using System;
using System.Collections.Generic;

namespace ParkMate.ApplicationCore.Entities
{
    public class Vehicle : BaseEntity
    {
        private Vehicle()
        {
        }

        public Vehicle(string make, string model, string colour, string registration)
        {
            Make = !string.IsNullOrWhiteSpace(make) ?
                make : throw new ArgumentNullException(nameof(make));
            Model = !string.IsNullOrWhiteSpace(model) ?
                model : throw new ArgumentNullException(nameof(model));
            Colour = !string.IsNullOrWhiteSpace(colour) ?
                colour : throw new ArgumentNullException(nameof(colour));
            Registration = !string.IsNullOrWhiteSpace(registration) ?
                registration : throw new ArgumentNullException(nameof(registration));
        }

        public string Make { get; private set; }
        public string Model { get; private set; }
        public string Colour { get; private set; }
        public string Registration { get; private set; }
    }
}

