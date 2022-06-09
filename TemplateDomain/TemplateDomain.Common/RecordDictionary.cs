using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateDomain.Common
{
    /*Dictionary of vaule objects. Equality is based on value object semantics*/
    public class RecordDictionary<TKey, TVal> : Dictionary<TKey, TVal>, IEquatable<RecordDictionary<TKey, TVal>>
    {
        public override bool Equals(object obj)
        {
            return obj is { } && (ReferenceEquals(this, obj) || obj is RecordDictionary<TKey, TVal> coll && Equals(coll));
        }

        public bool Equals(RecordDictionary<TKey, TVal> other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Count == other.Count && !this.Except(other).Any();
        }

        public static bool operator ==(RecordDictionary<TKey, TVal> left, RecordDictionary<TKey, TVal> right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(RecordDictionary<TKey, TVal> left, RecordDictionary<TKey, TVal> right)
        {
            return !EqualOperator(left, right);
        }

        static bool EqualOperator(RecordDictionary<TKey, TVal> left, RecordDictionary<TKey, TVal> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                return false;

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                foreach (var kv in this)
                {
                    result = (result * 397) ^ kv.Key.GetHashCode();
                    result = (result * 397) ^ kv.Value.GetHashCode();
                }
                return result;
            }
        }
    }
}
