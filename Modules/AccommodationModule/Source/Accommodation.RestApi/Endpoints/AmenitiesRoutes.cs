namespace Accommodation.RestApi.Endpoints
{
    /// <summary>
    /// Contains the route definitions for the Amenities API endpoints.
    /// </summary>
    public static class AmenitiesRoutes
    {
        /// <summary>
        /// The base route for the amenities API.
        /// </summary>
        public const string Base = "amenities";

        /// <summary>
        /// The route for getting paged amenities.
        /// </summary>
        public const string GetPagedAmenities = "";

        /// <summary>
        /// The route for getting a specific amenity by its ID.
        /// </summary>
        public const string GetAmenityById = "/{amenityId}";

        /// <summary>
        /// The route for getting amenities for a specific entity by its parent ID.
        /// </summary>
        public const string GetEntityAmenities = "/children/{parentId}";

        /// <summary>
        /// The route for creating a new amenity.
        /// </summary>
        public const string CreateAmenity = "";

        /// <summary>
        /// The route for editing an existing amenity.
        /// </summary>
        public const string EditAmenity = "";

        /// <summary>
        /// The route for deleting an amenity by its ID.
        /// </summary>
        public const string DeleteAmenity = "/{amenityId}";

        /// <summary>
        /// The route for adding an amenity to a specific entity.
        /// </summary>
        public const string AddEntityAmenity = "/addEntityAmenity";
    }

    
}
