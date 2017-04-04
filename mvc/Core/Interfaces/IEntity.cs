namespace Core.Interfaces
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}