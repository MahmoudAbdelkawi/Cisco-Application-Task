namespace CiscoApplication.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetAtomicValues();

        // functionality to compare two value objects
        public bool AreAtomicValuesEqual(ValueObject obj)
        {
            return GetAtomicValues().SequenceEqual(obj.GetAtomicValues());
        }
        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && AreAtomicValuesEqual(other);
        }

        public bool Equals(ValueObject? other)
        {
            return other is not null && AreAtomicValuesEqual(other);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Aggregate(
                default(int),
                HashCode.Combine);
        }
    }
}
