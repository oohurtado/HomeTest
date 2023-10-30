namespace Home.Source.Common
{
    public enum AppRole
    {
        [Basic(Name = "profile_admin", Description = "Identifies person as admin")]
        PROFILE_ADMIN,

        [Basic(Name = "profile_user", Description = "Identifies person as user")]
        PROFILE_USER,

        [Basic(Name = "func_tasks", Description = "Functionality tasks")]
        FUNK_TASKS,
    }

    public enum AppResponse
    {
        [Basic(Name = "UnknownError", Description = "Unknown error, contact your administrator.")]
        UnknownError,

        [Basic(Name = "UserNotFound", Description = "Error, user not found.")]
        UserNotFound,


        [Basic(Name = "WrongCredentials", Description = "Error, wrong credentials, try again.")]
        WrongCredentials,

        [Basic(Name = "BadRequest", Description = "Error, the server did not understand the operation that was requested.")]
        BadRequest,

        [Basic(Name = "RecordNotFound_OnGet", Description = "Error, we could not find what you were looking for.")]
        RecordNotFound_OnGet,

        [Basic(Name = "RecordNotFound_OnDelete", Description = "Error, you tried to delete a record that doesn't exist.")]
        RecordNotFound_OnDelete,

        [Basic(Name = "RecordNotFound_OnUpdate", Description = "Error, you tried to update a record that doesn't exist.")]
        RecordNotFound_OnUpdate,

        [Basic(Name = "RecordDuplicated_OnInsertUpdate", Description = "Error, you tried to insert/update a duplicate record.")]
        RecordDuplicated_OnInsertUpdate,

        [Basic(Name = "RecordNotFound_OnCreate", Description = "Error, you tried to create a record with a unknown key.")]
        RecordNotFound_OnCreate,

        [Basic(Name = "RecordWithChilds_OnDelete", Description = "Error, you tried to delete a record with ties.")]        
        RecordWithTies_OnDelete,
    }

    public enum PaginatorPage
    {
        People,
        Events,
        Activities,
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BasicAttribute : Attribute
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class PageConfigurationAttribute : Attribute
    {
        public string OrderColumns { get; set; } = null!;
        public string SearchColumns { get; set; } = null!;
    }
}
