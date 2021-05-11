using System;

namespace WebApiWithSqlTemplate.Domain.Utilities
{
    public static class Guard
    {
        public static void IsNotNull<T>(T value, string nameof)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof);
            }
        }
        
        public static void IsNotDefault<T>(T value, string nameof)
        {
            if (value.Equals(default(T)))
            {
                throw new ArgumentException("Value cannot be default.", nameof);
            }
        }
        
        public static void IsNotNullOrWhiteSpace(string value, string nameof)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("String cannot be null, empty or whitespace.", nameof);
            }
        }
        
        public static void IsNotLongerThan(string value, int maxCharLength, string nameof)
        {
            if (value.Length > maxCharLength)
            {
                throw new ArgumentException("String must be no more than 150 characters.", nameof);
            }
        }
    }
}