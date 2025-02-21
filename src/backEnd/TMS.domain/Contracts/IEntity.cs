namespace TMS.domain.Contracts;

public interface IEntity<TId> : IEntity
{
    TId Id { get; }
}

public interface IEntity { }