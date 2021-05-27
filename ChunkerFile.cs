using System;
using System.Collections.Generic;
using System.Text;
using ManagedBass;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using RiffParserModule;

namespace OMTChunker
{
    public class WavChunk
    {
        int chunkID;
        int chunkSize;
        int chunkData;

    }

    public class ChunkerFile
    {
        RiffParser parser = new RiffParser();

        public void GetMetadata(string filePath)
        {
            parser.OpenFile(filePath);
            Trace.WriteLine("Opening file");
            int length = parser.DataSize;

            RiffParser.ProcessChunkElement chunkProc = new RiffParser.ProcessChunkElement(ProcessCartChunk);
            RiffParser.ProcessListElement listProc = new RiffParser.ProcessListElement(ProcessCartList);
            
            while (length > 0)
            {
                try
                {
                    if (false == parser.ReadElement(ref length, chunkProc, listProc)) break;
                }
                catch (RiffParserModule.RiffParserException)
                {
                    Trace.WriteLine("Problem with File");
                    break;
                }
            }

            parser.CloseFile();

        }

        public void ProcessCartChunk(RiffParser rp, int fourCCtype, int unpaddedLength, int paddedLength)
        {
            if (RiffParser.FromFourCC(fourCCtype).ToLower() == "cart")
            {
                Byte[] ba = new byte[paddedLength];
                rp.ReadData(ba, 0, paddedLength);
                StringBuilder sb = new StringBuilder(unpaddedLength);
                for (int i = 0; i < unpaddedLength; i++)
                {
                    if (0 != ba[i]) sb.AppendLine(((char)ba[i]).ToString());
                }
                Trace.WriteLine(sb.ToString());
            }
            else
            {
                Trace.WriteLine(RiffParser.FromFourCC(fourCCtype));
                rp.SkipData(paddedLength);
            }
        }

        public void ProcessCartList(RiffParser rp, int FourCCType, int length)
        {
            rp.SkipData(length);
        }

    }
}
