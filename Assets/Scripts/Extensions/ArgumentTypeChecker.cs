using System;

namespace Extensions
{
    public static class ArgumentTypeChecker
    {
        public static void Check<T>(object obj, out T result)
        {
            if (obj is not T argument) 
                throw new ArgumentException("Invalid argument type", nameof(obj));
            
            result = argument;
        }
    }
}
