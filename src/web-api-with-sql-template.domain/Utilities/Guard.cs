using System;

namespace WebApiWithSqlTemplate.Domain.Utilities
{
    public static class Guard
    {
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