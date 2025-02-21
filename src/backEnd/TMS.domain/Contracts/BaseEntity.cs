namespace TMS.domain.Contracts;

public abstract class BaseEntity<TId>: IEntity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTimeOffset Date_Created { get; set; } = DateTimeOffset.UtcNow;
}

public abstract class BaseEntity : BaseEntity<int>
{
    protected BaseEntity() => Id = default!;
}

