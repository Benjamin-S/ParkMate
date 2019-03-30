using System;
using System.Collections.Generic;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class ParkingSpaceDescription : ValueObject
    {
        private ParkingSpaceDescription()
        {
        }

        public ParkingSpaceDescription(
            string title, 
            string description, 
            string imageUrl)
        {
            Title = !string.IsNullOrWhiteSpace(title) ? 
                title : throw new ArgumentNullException(nameof(title));

            Description = !string.IsNullOrWhiteSpace(description) ? 
                description : throw new ArgumentNullException(nameof(description));

            ImageURL = !string.IsNullOrWhiteSpace(imageUrl) ? 
                imageUrl : throw new ArgumentNullException(nameof(imageUrl));
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageURL { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return Description;
            yield return ImageURL;
        }
    }
}
