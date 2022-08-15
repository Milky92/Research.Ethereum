namespace Research.Eth.Persistence;

public abstract class EntityBase<TK>
{
    public TK Id { get; }
    public DateTime Created { get; } = DateTime.UtcNow;
    public DateTime Updated { get; set; }

    public abstract void GenerateKey();
}