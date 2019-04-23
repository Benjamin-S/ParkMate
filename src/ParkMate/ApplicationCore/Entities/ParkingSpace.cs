using System;
using System.Collections.Generic;
using System.Linq;
using ParkMate.ApplicationCore.Exceptions;
using ParkMate.ApplicationCore.Util;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationCore.Entities
{
    public class ParkingSpace : BaseEntity
    {
        private ParkingSpace()
        {
        }

        public ParkingSpace(
            string ownerId,
            ParkingSpaceDescription description,
            Address address,
            SpaceAvailability availability,
            BookingRate bookingRate)
        {
            OwnerId = !string.IsNullOrWhiteSpace(ownerId) ? ownerId :
                throw new ArgumentNullException(nameof(ownerId));
            Description = description ?? 
                throw new ArgumentNullException(nameof(description));
            Address = address ?? 
                throw new ArgumentNullException(nameof(address));
            Availability = availability ?? 
                throw new ArgumentNullException(nameof(availability)); 
            BookingRate = bookingRate ?? 
                throw new ArgumentNullException(nameof(bookingRate));
        }

        public string OwnerId { get; private set; }
        public ParkingSpaceDescription Description { get; private set; }
        public Address Address { get; private set; }
        public SpaceAvailability Availability { get; private set; }
        public BookingRate BookingRate { get; private set; }
        public List<Booking> Bookings { get; private set; } = new List<Booking>();

        public void AddBooking(Booking booking)
        {
            Bookings.Add(booking);
        }

        public void UpdateAddress(Address address)
        {
            Address = address ?? 
                throw new ArgumentNullException(nameof(address));
        }

        public void UpdateDescription(ParkingSpaceDescription description)
        {
            Description = description ??
                throw new ArgumentNullException(nameof(description));
        }

        public void UpdateBookingRate(BookingRate bookingRate)
        {
            if (bookingRate == null)
            {
                throw new ArgumentNullException(nameof(bookingRate));
            }
            BookingRate.UpdateFrom(bookingRate);
        }

        public void SetVisibility(bool isListed)
        {
            Availability.SetVisible(isListed);
        }

        public bool IsAvailable(BookingInfo bookingPeriod)
        {
            return Availability.IsAvailable(bookingPeriod) && 
                    !Overlaps(bookingPeriod);
        }

        bool Overlaps(BookingInfo bookingPeriod)
        {
            foreach (var booking in Bookings)
            {
                if (booking.BookingInfo.Overlaps(bookingPeriod))
                {
                    return true;
                }
            }
            return false;
        }
        
        public void AddBookingToSchedule(Booking booking)
        {
            if (Overlaps(booking.BookingInfo))
            {
                throw new AlreadyBookedException(booking.BookingInfo);
            }
            Bookings.Add(booking);
        }
    }
}
