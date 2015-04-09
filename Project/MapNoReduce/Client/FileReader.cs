using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace PADIMapNoReduce
{
    public class FileReader
    {
        private string _path;
        private FileStream _reader;
        UTF8Encoding encoding;

        public FileReader(string path)
        {
            _path = path;
            _reader = new FileStream(_path, FileMode.Open);
            encoding = new UTF8Encoding(true);
        }

        public string fetchSplitFromFile(long begin, long end)
        {
            long beginLine;
            long endLine;
            if(begin > _reader.Length || begin > end || end > _reader.Length || begin < 0){
                throw new InvalidAccessException(begin, end, _reader.Length);
            }



            if (begin != 0)
            {

                //verifica se o split dividiu a linha perfeitamente
                _reader.Seek(begin - 1, SeekOrigin.Begin);
                byte[] singleByte = new byte[1];

                _reader.Read(singleByte, 0, 1);

                if (encoding.GetString(singleByte) == "\n")
                {
                    beginLine = begin;
                }
                else
                {
                    beginLine = seekBeginLine(begin, end);
                }

            }
            else
            {
                beginLine = begin;
                _reader.Seek(begin, SeekOrigin.Begin);

            }

            endLine = seekNewLine(end);


            string split = readString(beginLine, endLine);


            return split;
        }

        private long seekNewLine(long end)
        {
            if (_reader.Position == getFileSize())
            {
                return _reader.Position;
            }
            _reader.Seek(end - 1, SeekOrigin.Begin);
            //FIXME: Should be optimized
            byte[] singleByte = new byte[1];
            while (_reader.Read(singleByte, 0, 1) > 0)
            {
                if (_reader.Position == getFileSize() || encoding.GetString(singleByte) == "\n")
                {
                    return _reader.Position;
                }
            }

            throw new EmptySplitException();
        }

        private string readString(long begin, long end)
        {

            _reader.Seek(begin, SeekOrigin.Begin);

            string split = "";

            int readBufferLength = 12;
            byte[] readBuffer = new byte[readBufferLength];



            int nBytesToRead = getBytesToRead(end, readBufferLength);

            while (nBytesToRead > 0)
            {
                //Last iteration will have trash if it isn't cleaned
                if (nBytesToRead < readBufferLength)
                    Array.Clear(readBuffer, 0, readBufferLength);

                _reader.Read(readBuffer, 0, nBytesToRead);

                split += encoding.GetString(readBuffer);
                nBytesToRead = getBytesToRead(end, readBufferLength);



            }
            return split;
        }

        private long seekBeginLine(long start, long end)
        {

            _reader.Seek(start, SeekOrigin.Begin);
            //FIXME: Should be optimized
            byte[] singleByte = new byte[1];
            while (_reader.Read(singleByte, 0, 1) > 0)
            {
                if (_reader.Position >= end)
                {
                    throw new EmptySplitException();
                }

                if (encoding.GetString(singleByte) == "\n")
                {
                    return _reader.Position;
                }
            }

            throw new EmptySplitException();
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

            string result = fs.fetchSplitFromFile(55, 102);
            Console.WriteLine("Got: " + result);
            Console.ReadLine();

        }

    }


   
}
