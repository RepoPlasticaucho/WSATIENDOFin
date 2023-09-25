using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WsAtiendo.SRI
{
    public class EncryptionAlgorithm
    {
        // Algoritmo de Encriptacion necesita texto plano y la clave 
        public static string Encrypt(string plainText, string key)
        {
            //creacion de variables en bytes
            byte[] encryptedBytes;
            byte[] ivBytes;

            //Encriptacion con la libreria AES
            using (Aes aes = Aes.Create())
            {
                // Generacion de la clave valida
                aes.Key = GetValidKey(key, aes.KeySize / 8);
                aes.GenerateIV();
                ivBytes = aes.IV;

                // Creacion de la encriptacion bajo la salt
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                //Genera un arreglo en Bytes para encriptar y manter el texto plano como llego
                byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
                // Transformacion de bloques de 0 hasta lo largo de la encriptacion realizada anterior
                encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }

            //Encriptacion en bytes
            byte[] encryptedResult = new byte[ivBytes.Length + encryptedBytes.Length];
            Buffer.BlockCopy(ivBytes, 0, encryptedResult, 0, ivBytes.Length);
            Buffer.BlockCopy(encryptedBytes, 0, encryptedResult, ivBytes.Length, encryptedBytes.Length);

            //Devuelve en base 64 la encriptacion
            return Convert.ToBase64String(encryptedResult);
        }

        public static string Decrypt(string encryptedText, string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] ivBytes = new byte[16];
            byte[] encryptedData = new byte[encryptedBytes.Length - 16];

            Buffer.BlockCopy(encryptedBytes, 0, ivBytes, 0, 16);
            Buffer.BlockCopy(encryptedBytes, 16, encryptedData, 0, encryptedBytes.Length - 16);

            byte[] decryptedBytes;

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetValidKey(key, aes.KeySize / 8);
                aes.IV = ivBytes;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            }

            return Encoding.Unicode.GetString(decryptedBytes);
        }

        private static byte[] GetValidKey(string key, int keySize)
        {
            byte[] keyBytes = Encoding.Unicode.GetBytes(key);

            if (keyBytes.Length != keySize)
            {
                Array.Resize(ref keyBytes, keySize);
            }

            return keyBytes;
        }
    }
}