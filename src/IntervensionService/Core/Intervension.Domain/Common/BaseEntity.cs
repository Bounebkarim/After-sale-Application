namespace Intervension.Domain.Common;
public class BaseEntity : IEquatable<BaseEntity>
{
    public BaseEntity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
    public DateTime DateCreated { get; set; }
    public string CreatedBy { get; set; }
    public DateTime DateModified { get; set; }
    public string ModifiedBy { get; set; }

    public static bool operator ==(BaseEntity? left, BaseEntity? right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(BaseEntity? left, BaseEntity? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BaseEntity)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 7;
    }

    public bool Equals(BaseEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (other.GetType() != GetType()) return false;
        return Id == other.Id;
    }
}
