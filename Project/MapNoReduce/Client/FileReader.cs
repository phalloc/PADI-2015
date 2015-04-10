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
            _reader = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read);
            encoding = new UTF8Encoding(true);
        }

        public string fetchSplitFromFile(long begin, long end)
        {
            long beginLine;
            long endLine;
            if(begin > _reader.Length || begin > end || end > _reader.Length || begin < 0){
                throw new InvalidAccessException(begin, end, _reader.Length);
            }

            try
            {

                if (begin != 0)
                {

                    //Logger.LogInfo("(" + begin + "," + end + ")" +"Going in guys!");

                    //verifica se o split dividiu a linha perfeitamente, ALTERNATIVE
                    _reader.Seek(begin - 1, SeekOrigin.Begin);
                    byte[] singleByte = new byte[1];

                    _reader.Read(singleByte, 0, 1);

                    if (encoding.GetString(singleByte) == "\n")
                    {
                        //Logger.LogInfo("(" + begin + "," + end + ")" + "Found \\n in the beginning!");

                        beginLine = begin;
                    }
                    else
                    {
                        //Logger.LogInfo("(" + begin + "," + end + ")" + "No \\n in the beginning!");
                        beginLine = seekBeginLine(begin, end);
                    }

                    //Logger.LogInfo("(" + begin + "," + end + ")" + "with begin line: " + beginLine);

                    //beginLine = seekBeginLine(begin, end);

                }
                else
                {
                    //Logger.LogInfo("(" + begin + "," + end + ")" + "Begin hipster!");
                    beginLine = 0;
                    _reader.Seek(0, SeekOrigin.Begin);

                }

                endLine = seekNewLine(end);

                //Logger.LogInfo("(" + begin + "," + end + ")" + "with end line: " + beginLine);

                //Logger.LogInfo("(" + begin + "," + end + ")"  + " Reading from " + beginLine + " to " + endLine);
                string split = readString(beginLine, endLine);


                return split;
            }
            catch (EmptySplitException e)
            {
                //Logger.LogInfo("(" + begin + "," + end + ")" + e.ToString());
                return "";
            }
        }

        private long seekNewLine(long end)
        {
            if (end == getFileSize())
            {
                return end;
            }

            //alternative
            _reader.Seek(end, SeekOrigin.Begin);
            //_reader.Seek(end, SeekOrigin.Begin);
            //FIXME: Should be optimized
            byte[] singleByte = new byte[1];
            while (_reader.Read(singleByte, 0, 1) > 0)
            {
                if (_reader.Position == getFileSize() || encoding.GetString(singleByte) == "\n")
                {
                    return _reader.Position;
                }
            }

            throw new EmptySplitException(_reader.Position);
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

                if (encoding.GetString(singleByte) == "\n")
                {
                    return _reader.Position;
                }
                
                if (_reader.Position >= end)
                {
                    throw new EmptySplitException(_reader.Position);
                }

            }

            throw new EmptySplitException(_reader.Position);
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


    }


   
}
