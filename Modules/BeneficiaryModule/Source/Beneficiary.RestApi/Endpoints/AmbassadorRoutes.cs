namespace Accomodation.Base.Endpoints
{
    /// <summary>
    /// Defines routes for Ambassador-related API endpoints.
    /// Adjust these constants as necessary to match your API style.
    /// </summary>
    public static class AmbassadorRoutes
    {
        public const string Base = "api/ambassadors";

        public const string GetAll = Base;
        public const string GetPaged = Base + "/paged";
        public const string GetById = Base + "/{id}";
        public const string Create = Base;
        public const string Update = Base + "/{id}";
        public const string Delete = Base + "/{id}";
    }
}