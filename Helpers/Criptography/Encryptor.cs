// CommonLibrary - library for common usage.
// CommonLibrary - библиотека с общо предназначение.

using System;
using System.IO;
using System.ComponentModel;
using CommonLibrary.Attributes;

namespace CommonLibrary.Helpers.Criptography
{
    /// <summary>
    /// 
    /// EN:
    ///   Provides set of static methods for encrypting and decrypting 
    ///   file content with a key. The encryptor encrypts a file by iterating
    ///   over all symbols of the file content and apply XOR (^) bitwise operation
    ///   with the key with each of them. The decrypting operation is the same as enctypting.
    /// 
    /// BG:
    ///   Предоставя набор от статични методи за криптиране и декриптиране на съдържание на файл с ключ.
    ///   Крипторът шифрира файл, като обхожда всички символи от съдържанието на файла и
    ///   прилага побитова операция XOR (^) (побитово изключващо ИЛИ) с ключа върху всеки от тях. 
    ///   Операцията по декриптиране е същата като при криптиране.
    /// 
    /// </summary>
    [Author("Tsvetelin Marinov")]
    [Description("File encryptor")]
    [Usage("Used to encrypt and decrypt file content")]
    public static class Encryptor
    {
        //
        // The key for the encryptiong.
        //
        // Ключа за шифроване.
        //
        private const ushort Key = 666;


        /// <summary>
        /// 
        /// EN:
        ///   Encrypts the content of the file.
        ///   
        /// BG:
        ///   Шифрира указания файл.
        /// 
        /// </summary>
        /// 
        /// <param name="filePath">
        ///  EN: The directory of the file in the system.
        ///  BG: Директорията на файла в системата.
        /// </param>
        public static void Encrypt(string filePath)
            => EncryptFile(filePath);

        /// <summary>
        /// 
        /// EN:
        ///   Decripts a file, encrypted with the same encryptor - the current data type.
        ///   
        /// BG:
        ///   Дешифрова файл, шифриран с текущия криптор.
        /// 
        /// </summary>
        /// 
        /// <param name="filePath">
        ///  ЕN: The directory of the file in the system.
        ///  BG: Директорията на файла в системата.
        /// </param>
        public static void Decrypt(string filePath)
            => DecryptFile(filePath);


        // Encrytps a file
        private static void EncryptFile(string directory)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(directory);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(directory);

            string fileName = directory.Substring(directory.LastIndexOf("\\") + 1);
            string newFilePath = directory.Substring(0, directory.Length - fileName.Length);

            newFilePath += $"secret_{fileName}";

            using FileStream inputStream = new(directory, FileMode.Open);
            using FileStream outputStream = new(newFilePath, FileMode.Create);

            while (true)
            {
                byte[] buffer = new byte[4096];
                int readedBytes = inputStream.Read(buffer);

                if (readedBytes == 0)
                {
                    break;
                }

                for (int i = 0; i < readedBytes; ++i)
                {
                    buffer[i] = (byte)(buffer[i] ^ Key);
                }

                outputStream.Write(buffer, 0 , readedBytes);
            }
        }

        // Decrypts a file
        private static void DecryptFile(string directory)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(directory);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(directory);

            string fileName = directory.Substring(directory.LastIndexOf(@"\") + 1);
            string originalFileName = fileName.Substring(fileName.IndexOf("_") + 1);
          
            string newPath = directory.Replace(fileName, originalFileName);

            using FileStream inputStream = new(directory, FileMode.Open);
            using FileStream outputStream = new(newPath, FileMode.Create);

            while (true)
            {
                byte[] buffer = new byte[4096];
                int readed = inputStream.Read(buffer);

                if (readed == 0)
                {
                    break;
                }

                for (int i = 0; i < readed; i++)
                {
                    buffer[i] = (byte)(buffer[i] ^ Key);
                }

                outputStream.Write(buffer, 0, readed);
            }
        }
    }
}