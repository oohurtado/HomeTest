using Home.Source.Common;
using Home.Source.Database;
using Home.Source.Extensions;
using Home.Source.Helpers;
using Microsoft.Data.SqlClient;

namespace Home.Source.Repositories
{
    public class BaseRepository
    {
        protected readonly DatabaseContext context;

        public BaseRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<BaseResponse> SaveChangesAsync()
        {
            var request = new BaseResponse();

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BaseResponseHelper.GetExceptionError(ex);
            }

            return request;
        }
    }
}
