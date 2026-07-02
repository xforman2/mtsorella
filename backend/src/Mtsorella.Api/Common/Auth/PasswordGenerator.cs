using System.Security.Cryptography;

namespace Mtsorella.Api.Common.Auth;

// Generates the temporary password an admin hands to a new member (they must change it on first login).
// Cryptographically-random over an unambiguous alphabet (no 0/O/1/l/I).
public static class PasswordGenerator
{
    private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";

    public static string Generate(int length = 12)
    {
        // ponytail: rejection-free modulo bias is negligible for a temp password over a 56-char alphabet.
        char[] chars = new char[length];
        for (int i = 0; i < length; i++)
        {
            chars[i] = Alphabet[RandomNumberGenerator.GetInt32(Alphabet.Length)];
        }

        return new string(chars);
    }
}
