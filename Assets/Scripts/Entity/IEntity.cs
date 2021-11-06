public interface IEntity 
{
    void TakeDamage(int amount);

    int Health { get; protected set; }

}
