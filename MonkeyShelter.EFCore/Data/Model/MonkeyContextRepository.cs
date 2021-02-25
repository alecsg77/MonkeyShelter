namespace MonkeyShelter.Data.Model
{
    public class MonkeyContextRepository : ContextRepository<Monkey, string>, IMonkeyRepository
    {
        public MonkeyContextRepository(ShelterContext context) : base(context)
        {
        }
    }
}
