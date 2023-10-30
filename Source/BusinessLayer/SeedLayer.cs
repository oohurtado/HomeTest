using Home.Source.Repositories;

namespace Home.Source.BusinessLayer
{
    public class SeedLayer
    {
        private readonly AspNetRepository aspNetRepository;

        public SeedLayer(AspNetRepository aspNetRepository)
        {
            this.aspNetRepository = aspNetRepository;
        }

        public async Task InitAsync()
        {
            await aspNetRepository.CreateRolesAsync();
            await aspNetRepository.DeleteRolesAsync();           
        }
    }
}
