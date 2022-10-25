using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilites.Exstensions
{
    public static class StringExtensions
    {
        public static bool IsValidEmail (this string email, out string localPart, out string domain)
        {
            if (email.IsValidEmail( ))
            {
                localPart = email.Split("@")[0];
                domain = email.Split("@")[1];
                return true;
            }
            else
            {
                localPart = "";
                domain = "";
                return false;
            }
        }

        public static bool IsValidEmail (this string email)
        {

            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper (Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping( );

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static (byte[ ] hash, byte[ ] salt) CreatePasswordHash (this string password)
        {
            using var hmac = new HMACSHA512( );
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }

        public static bool VerifyPassword (this string password, byte[ ] passwordHash, byte[ ] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        public static string GetCheckSum (this string data) => GetCheckSum(Encoding.UTF8.GetBytes(data));

        public static string GetCheckSum (this byte[ ] data)
        {
            using var sha1 = SHA1.Create( );
            return string.Concat(
                sha1.ComputeHash(data)
                .Select(x => x.ToString("X2")));
        }

    }
}
