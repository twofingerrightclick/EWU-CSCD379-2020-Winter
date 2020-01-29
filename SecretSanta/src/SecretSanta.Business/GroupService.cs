using AutoMapper;
using SecretSanta.Data;


namespace SecretSanta.Business
{
    public class GroupService : EntityService<Group>, IEntityService<Group>
    {

        public GroupService(ApplicationDbContext applicationDbContext, IMapper mapper) :
       base(applicationDbContext, mapper)
        {


        }




    }
}
