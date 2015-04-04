using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Client
{
    class FileReader
    {
        private string _path;
        private FileStream _reader;


        public FileReader(string path)
        {
            _path = path;
            _reader = new FileStream(_path, FileMode.Open);

            long fileSize = _reader.Length;




        }

        public string fetchSplitFromFile(long begin, long end)
        {

            //FIXME: Tratamento Excepção
            /*if(begin > _reader.length || begin > end || end > _reader.Length || begin < 0){

            }*/

            Console.WriteLine("FileSize: " + _reader.Length);

            _reader.Seek(begin, SeekOrigin.Begin);

            string split = "";

            int readBufferLength = 12;
            byte[] readBuffer = new byte[readBufferLength];
            UTF8Encoding encoding = new UTF8Encoding(true);


            int nBytesToRead = getBytesToRead(end, readBufferLength);

            while (nBytesToRead > 0)
            {
                //Last iteration will have trash if it isn't cleaned
                if (nBytesToRead < readBufferLength)
                    Array.Clear(readBuffer, 0, readBufferLength);

                _reader.Read(readBuffer, 0, nBytesToRead);
                Console.WriteLine("My position: " + _reader.Position);

                split += encoding.GetString(readBuffer);
                nBytesToRead = getBytesToRead(end, readBufferLength);



            }



            return split;
        }

        private int getBytesToRead(long end, int readBufferLength)
        {
            int nBytesToRead;
            if (readBufferLength + _reader.Position < end)
            {
                nBytesToRead = readBufferLength;
            }
            else
            {
                long endDelta = end - _reader.Position;
                nBytesToRead = unchecked((int)endDelta);
            }

            return nBytesToRead;
        }

        public long getFileSize()
        {
            return _reader.Length;
        }


        public long getSplitSize(int nSplits)
        {

            return _reader.Length / nSplits;

        }

        public void closeReader()
        {
            _reader.Close();
        }

        //main for testing purposes
        public static void Main(string[] args)
        {

            string path = "D:\\Valchier\\Desktop\\exampleMap.txt";

            Console.WriteLine("Reading from: " + path);
            FileReader fs = new FileReader(path);

            Console.WriteLine("Each split will have " + fs.getSplitSize(3) + " bytes");

            string result = fs.fetchSplitFromFile(85, 102);
            Console.WriteLine("Got: " + result);
            Console.ReadLine();

        }

    }


   
}
