using System.Runtime.CompilerServices;

namespace MalfunctionTrackerUI
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();

            builder.Services.AddSingleton<IDBConnection, DBConnection>();
            builder.Services.AddSingleton<ICategoryData, MongoCategoryData>();
            builder.Services.AddSingleton<IStatusData, MongoStatusData>();
            builder.Services.AddSingleton<IMalfunctionData, MongoMalfunctionData>();
            builder.Services.AddSingleton<IUserData, MongoUserData>(); //Nemamo nikakve zapise u pojedinog korisnika zato singl.
        }
    }
}
