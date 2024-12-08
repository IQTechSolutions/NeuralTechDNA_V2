namespace Accommodation.RestApi.Endpoints
{
    /// <summary>
    /// Contains the route definitions for the Booking API endpoints.
    /// </summary>
    public class BookingRoutes
    {
        /// <summary>
        /// The base route for the bookings API.
        /// </summary>
        public const string Base = "bookings";

        /// <summary>
        /// The route for retrieving the total count of bookings.
        /// **Endpoint:** GET api/bookings/count
        /// </summary>
        public const string Count = "count";

        /// <summary>
        /// The route for retrieving a paged list of bookings.
        /// **Endpoint:** GET api/bookings
        /// </summary>
        public const string PagedBookings = "";

        /// <summary>
        /// The route for retrieving booking details by booking ID.
        /// **Endpoint:** GET api/bookings/details/{bookingId}
        /// </summary>
        public const string Details = "details/{bookingId}";

        /// <summary>
        /// The route for retrieving bookings associated with a specific order number.
        /// **Endpoint:** GET api/bookings/bookingList/{orderNr}
        /// </summary>
        public const string BookingList = "bookingList/{orderNr}";

        /// <summary>
        /// The route for completing an order and its associated bookings.
        /// **Endpoint:** POST api/bookings/complete/{orderNr}
        /// </summary>
        public const string CompleteOrder = "complete/{orderNr}";

        /// <summary>
        /// The route for creating a new booking.
        /// **Endpoint:** PUT api/bookings
        /// </summary>
        public const string CreateBooking = ""; // Root route

        /// <summary>
        /// The route for cancelling a booking.
        /// **Endpoint:** POST api/bookings/cancel
        /// </summary>
        public const string Cancel = "cancel";

        /// <summary>
        /// The route for retrieving order details by order number.
        /// **Endpoint:** GET api/bookings/order/{orderNr}
        /// </summary>
        public const string Order = "order/{orderNr}";

        /// <summary>
        /// The route for creating a new order.
        /// **Endpoint:** PUT api/bookings/createOrder
        /// </summary>
        public const string CreateOrder = "createOrder";
    }
}
