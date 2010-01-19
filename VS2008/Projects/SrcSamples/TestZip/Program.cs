using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.IO.Compression;

namespace TestZip
{
    class Program
    {
        static void Main(string[] args)
        {
            //string sPath = @"C:\Documents and Settings\mha\Application Data\SummaSummarum\4";
            //string sFile = @"C:\Documents and Settings\mha\Dokumenter\dbData3060.sdf";
            string sFile = @"C:\Documents and Settings\mha\Application Data\SummaSummarum\4\1800.dat";
            FileStream sReader = File.OpenRead(sFile);
            byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
            sReader.Read(buff, 0, (int)sReader.Length);
            MemoryStream streamfromfile = new MemoryStream(buff);
            streamfromfile.Position = 0;

            MemoryStream streamOut = FileCompressZip(streamfromfile);
            streamOut.Position = 0;
            MemoryStream streamIn = UncompressZip(streamOut);
            //Main3();
        }
        private static MemoryStream FileCompressZip(MemoryStream streamIm)
        {
            MemoryStream streamOut = new MemoryStream();
            ZipOutputStream zipOut = new ZipOutputStream(streamOut);
            ZipEntry entry = new ZipEntry("Navn");
            entry.DateTime = DateTime.Now;
            entry.Size = streamIm.Length;
            zipOut.PutNextEntry(entry);
            zipOut.Write(streamIm.ToArray(), 0, (int)streamIm.Length);

            zipOut.Finish();
            zipOut.IsStreamOwner = false;
            zipOut.Close();
            Console.WriteLine("Done!!");
            return streamOut;
        }
        private static MemoryStream UncompressZip(MemoryStream streamIn)
        {
            ZipInputStream zipIn = new ZipInputStream(streamIn);
            ZipEntry entry;
            entry = zipIn.GetNextEntry();
            MemoryStream streamOut = new MemoryStream();
            //FileStream streamWriter = File.Create(@"C:\Temp\" + entry.Name);
            long size = entry.Size;
            byte[] data = new byte[size];
            while (true)
            {
                size = zipIn.Read(data, 0, data.Length);
                if (size > 0) streamOut.Write(data, 0, (int)size);
                else break;
            }
            streamOut.Flush();
            Console.WriteLine("Done!!");
            return streamOut;

        }

        private static void CompressZip(string sPath)
        {
            ZipOutputStream zipOut = new ZipOutputStream(File.Create(@"C:\Temp\test.zip"));
            foreach (string fName in Directory.GetFiles(sPath))
            {
                FileInfo fi = new FileInfo(fName);
                ZipEntry entry = new ZipEntry(fi.Name);
                FileStream sReader = File.OpenRead(fName);
                byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
                sReader.Read(buff, 0, (int)sReader.Length);
                entry.DateTime = fi.LastWriteTime;
                entry.Size = sReader.Length;
                sReader.Close();
                zipOut.PutNextEntry(entry);
                zipOut.Write(buff, 0, buff.Length);
            }
            zipOut.Finish();
            zipOut.Close();
            Console.WriteLine("Done!!");
        }

        private static void UncompressZip(string sFile)
        {
            ZipInputStream zipIn = new ZipInputStream(File.OpenRead(sFile));
            ZipEntry entry;
            while ((entry = zipIn.GetNextEntry()) != null)
            {
                FileStream streamWriter = File.Create(@"C:\Temp\" + entry.Name);
                long size = entry.Size;
                byte[] data = new byte[size];
                while (true)
                {
                    size = zipIn.Read(data, 0, data.Length);
                    if (size > 0) streamWriter.Write(data, 0, (int)size);
                    else break;
                }
                streamWriter.Close();
            }
            Console.WriteLine("Done!!");
        }

        static void Main2()
        {
            /*
            int count;
            //GetByteData function to get Byte data like if you fetch Image column data from sqlserver or somewhere.
            byte[] byteArray = getByteData();
            MemoryStream memStream = new MemoryStream(ByteArray);
            // Write the second string to the stream, byte by byte.
            count = 0;
            // Write the stream properties to the console.
            Console.WriteLine(
            "Capacity = {0}, Length = {1}, Position = {2}\n",
            memStream.Capacity.ToString(),
            memStream.Length.ToString(),
            memStream.Position.ToString());
            // Set the position to the beginning of the stream.
            memStream.Seek(0, SeekOrigin.Begin);
            // Read the first 20 bytes from the stream.
            byteArray = new byte[memStream.Length];
            count = memStream.Read(byteArray, 0, 20);
            // Read the remaining bytes, byte by byte.
            while (count < memStream.Length)
            {
                byteArray[count++] = Convert.ToByte(memStream.ReadByte());
            }
            // Decode the byte array into a char array 
            // and write it to the console.
            charArray = new char[uniEncoding.GetCharCount(byteArray, 0, count)];
            uniEncoding.GetDecoder().GetChars(byteArray, 0, count, charArray, 0);
            Console.WriteLine(charArray);
        */
        }
        public static void Main3()
        {
            // Path to directory of files to compress and decompress.
            //string dirpath = @"C:\Documents and Settings\mha\Dokumenter\dbData3060.sdf";
            string dirpath = @"C:\Documents and Settings\mha\Application Data\SummaSummarum\4\1800.dat";
            FileInfo fi = new FileInfo(dirpath);
            //DirectoryInfo di = new DirectoryInfo(dirpath);

            // Compress the directory's files.
            //foreach (FileInfo fi in di.GetFiles())
            //{
            Compress(fi);
            //}

            // Decompress all *.cmp files in the directory.
            //foreach (FileInfo fi in di.GetFiles("*.cmp"))
            //{
            //    Decompress(fi);
            //}


        }

        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and already compressed files.
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".cmp")
                {
                    // Create the compressed file.
                    using (FileStream outFile =
                            File.Create(fi.FullName + ".cmp"))
                    {
                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            byte[] buffer = new byte[4096];
                            int numRead;
                            while ((numRead = inFile.Read(buffer,
                                    0, buffer.Length)) != 0)
                            {
                                Compress.Write(buffer, 0, numRead);
                            }
                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }

        public static void Decompress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, 
                // for example "doc" from report.doc.cmp.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length
                        - fi.Extension.Length);

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (DeflateStream Decompress = new DeflateStream(inFile,
                        CompressionMode.Decompress))
                    {
                        //Copy the decompression stream into the output file.
                        byte[] buffer = new byte[4096];
                        int numRead;
                        while ((numRead =
                            Decompress.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            outFile.Write(buffer, 0, numRead);
                        }
                        Console.WriteLine("Decompressed: {0}", fi.Name);
                    }
                }
            }
        }

    }
}
