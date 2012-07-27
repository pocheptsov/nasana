namespace NAsana.API.v1.Model.Utils
{
    using System;

    public class UserId
    {
        private readonly UserPredefinedId? _predefinedId;
        private readonly long? _id;

        public UserId(UserPredefinedId id)
        {
            _predefinedId = id;
        }

        public UserId(long id)
        {
            _id = id;
        }

        public override int GetHashCode()
        {
            return _id.HasValue ? _id.GetHashCode() : _predefinedId.GetHashCode();
        }

        public override string ToString()
        {
// ReSharper disable SpecifyACultureInStringConversionExplicitly
            return _id.HasValue ? _id.ToString() : _predefinedId.ToString().ToLowerInvariant();
// ReSharper restore SpecifyACultureInStringConversionExplicitly
        }

        public bool Equals(UserId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._predefinedId.Equals(_predefinedId) && other._id.Equals(_id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (UserId)) return false;
            return Equals((UserId) obj);
        }

        public static bool operator ==(UserId a, UserId b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a._id == b._id && a._predefinedId == b._predefinedId;
        }

        public static bool operator !=(UserId a, UserId b)
        {
            return !(a == b);
        }
        
        public static implicit operator UserId(long source)
        {
            return new UserId(source);
        }
        
        public static implicit operator UserId(UserPredefinedId source)
        {
            return new UserId(source);
        }
        
        public static UserId Parse(string source)
        {
            long longResult;
            if (long.TryParse(source, out longResult))
            {
                return new UserId(longResult);
            }
            
            UserPredefinedId enumResult;
            if (Enum.TryParse(source, true, out enumResult))
            {
                return new UserId(enumResult);
            }

            throw new FormatException(string.Format("Invalid user id format - '{0}'.", source));
        }
    }

    public enum UserPredefinedId
    {
        Me,
    }
}