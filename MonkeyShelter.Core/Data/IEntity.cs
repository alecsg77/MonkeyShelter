namespace MonkeyShelter.Data
{
    public interface IEntity<out TKey>: IEntity
    {
        new TKey Id { get; }
    }

    public interface IEntity
    {
        object Id { get; }
    }
}