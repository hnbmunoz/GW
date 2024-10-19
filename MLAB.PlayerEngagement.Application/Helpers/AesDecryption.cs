using System.Security.Cryptography;
using System.Text;

namespace MLAB.PlayerEngagement.Application.Helpers
{
    public sealed class AesDecryption
    {
        private AesDecryption()
            : base()
        {
        }

        /// <summary>
        /// Overloaded.  Rountine for Rijndale decryption to base 64 string.
        /// </summary>
        /// <param name="strIn">Input string</param>
        /// <param name="strPwd">Encrypt / Decrypt password.</param>
        /// <param name="strBase64Salt">Base 64 string salt.</param>
        /// <returns>Encrypted string / decrypted string.</returns>
        /// <remarks></remarks>
        /// <history>
        ///     07 Oct 2005, Fai
        ///     - Created
        /// </history>
        public static string AesDecryptedFromBase64(
            string strIn,
            string strPwd,
            string strBase64Salt)
        {
            string strRtn = string.Empty;
            try
            {
                strRtn = AesDecryptedFromBase64(
                    strIn,
                    strPwd,
                    Convert.FromBase64String(strBase64Salt));
            }
            catch (Exception ex)
            {
                strRtn = string.Empty;
                throw new ArgumentException("Not Found" + ex.Message, nameof(strIn));
            }
            return strRtn;

        }





        /// <summary>
        /// Decrypt the base 64 string of Rijindael encrypted cipher data.
        /// </summary>
        /// <param name="strIn">Input base 64 string of Rijindael encrypted cipher data.</param>
        /// <param name="strPwd">Decrypted password.</param>
        /// <param name="salt">Decrypted salt.</param>
        /// <returns>Decrypted string value.</returns>
        /// <remarks></remarks>
        /// <history>
        ///     10 Oct 2005, Fai
        ///     - Created
        /// </history>
        public static string AesDecryptedFromBase64(string strIn,
            string strPwd,
            byte[] salt)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(strIn);

                // Then, we need to turn the password into Key and IV 
                // We are using salt to make it harder to guess our key
                // using a dictionary attack - 
                // trying to guess a password by enumerating all possible words. 
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(strPwd, salt);


                // Now get the key/IV and do the decryption using
                // the function that accepts byte arrays. 
                // Using PasswordDeriveBytes object we are first
                // getting 32 bytes for the Key 
                // (the default Rijndael key length is 256bit = 32bytes)
                // and then 16 bytes for the IV. 
                // IV should always be the block size, which is by
                // default 16 bytes (128 bit) for Rijndael. 
                // If you are using DES/TripleDES/RC2 the block size is
                // 8 bytes and so should be the IV size. 
                // You can also read KeySize/BlockSize properties off
                // the algorithm to find out the sizes. 
                byte[] decryptedData = AesDecryptedFromByte(cipherBytes,
                    pdb.GetBytes(32), pdb.GetBytes(16));

                // Now we need to turn the resulting byte array into a string. 
                // A common mistake would be to use an Encoding class for that.
                // It does not work 
                // because not all byte values can be represented by characters. 
                // We are going to be using Base64 encoding that is 
                // designed exactly for what we are trying to do. 
                return Encoding.UTF8.GetString(decryptedData);



            }
            catch (Exception ex)
            {
                throw new ArgumentException("Not Found" + ex.Message, nameof(strIn));
            }

        }

        /// <summary>
        /// Decrypt the cipher data to byte array content.
        /// </summary>
        /// <param name="cipherData">Cipher data for decrypting.</param>
        /// <param name="Key">Key value.</param>
        /// <param name="IV">IV value.</param>
        /// <returns>Decrypted byte array content.</returns>
        /// <remarks></remarks>
        /// <history>
        ///     10 Oct 2005, Fai
        ///     - Created
        /// </history>
        public static byte[] AesDecryptedFromByte(byte[] cipherData,
            byte[] Key,
            byte[] IV)
        {
            try
            {
                // Create a MemoryStream that is going to accept the
                // decrypted bytes 
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create a symmetric algorithm. 
                    // We are going to use Rijndael because it is strong and
                    // available on all platforms. 
                    // You can use other algorithms, to do so substitute the next
                    // line with something like 
                    using (Aes alg = Aes.Create())
                    {
                        // Now set the key and the IV. 
                        // We need the IV (Initialization Vector) because the algorithm
                        // is operating in its default 
                        // mode called CBC (Cipher Block Chaining). The IV is XORed with
                        // the first block (8 byte) 
                        // of the data after it is decrypted, and then each decrypted
                        // block is XORed with the previous 
                        // cipher block. This is done to make encryption more secure. 
                        // There is also a mode called ECB which does not need an IV,
                        // but it is much less secure. 
                        alg.Key = Key;
                        alg.IV = IV;
                        alg.Padding = PaddingMode.PKCS7;
                        alg.Mode = CipherMode.CBC;

                        // Create a CryptoStream through which we are going to be
                        // pumping our data. 
                        // CryptoStreamMode.Write means that we are going to be
                        // writing data to the stream 
                        // and the output will be written in the MemoryStream
                        // we have provided. 
                        using (CryptoStream cs = new CryptoStream(ms,
                            alg.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            // Write the data and make it do the decryption 
                            cs.Write(cipherData, 0, cipherData.Length);
                            cs.FlushFinalBlock();
                            // Close the crypto stream (or do FlushFinalBlock). 
                            // This will tell it that we have done our decryption
                            // and there is no more data coming in, 
                            // and it is now a good time to remove the padding
                            // and finalize the decryption process. 
                            cs.Close();

                            // Now get the decrypted data from the MemoryStream. 
                            // Some people make a mistake of using GetBuffer() here,
                            // which is not the right way. 
                            byte[] decryptedData = ms.ToArray();
                            return decryptedData;
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Not Found" + ex.Message, nameof(cipherData));
            }

        }
    }
}
