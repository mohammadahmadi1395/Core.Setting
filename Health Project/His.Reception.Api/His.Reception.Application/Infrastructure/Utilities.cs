using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace His.Reception.Application.Infrastructure
{
    public class Utilities
    {
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
