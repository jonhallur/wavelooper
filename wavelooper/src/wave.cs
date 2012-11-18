using System;
using System.IO;

namespace ambient.wavelooper
{
    public class WaveFile
    {
	private int chunkID;
	private int fileSize;
	private int riffType;
	int fmtID;
	int fmtSize;
	int fmtCode;
	int channels;
	int sampleRate;
	int fmtAvgBPS;
	int fmtBlockAlign;
	int bitDepth;
	byte[] fmtExtra;
	int fmtExtraSize;
	int dataID;
	int dataSize;
	byte[] dataArray;
	

	string fileName;
	public bool returnTrue()
	{
		return true;
	}	
	public bool canOpenFile(string fullPath)
	{
		this.fileName = fullPath;
		BinaryReader br = new BinaryReader(File.Open(fullPath, FileMode.Open));
		this.canReadHeader(ref br);
		return true;
	}
	public bool canReadHeader(ref BinaryReader br)
	{
		this.chunkID = br.ReadInt32();
		this.fileSize = br.ReadInt32();
                this.riffType = br.ReadInt32();
                this.fmtID = br.ReadInt32();
                this.fmtCode = br.ReadInt16();
                this.channels = br.ReadInt16();
                this.sampleRate = br.ReadInt32();
                this.fmtAvgBPS = br.ReadInt32();
                this.fmtBlockAlign = br.ReadInt16();
                this.bitDepth = br.ReadInt16();

		if (this.fmtSize == 18)
		{
			this.fmtExtraSize = br.ReadInt16();
			this.fmtExtra = br.ReadBytes(this.fmtExtraSize);
		}

		this.dataID = br.ReadInt32();
		this.dataSize = br.ReadInt32();
		
		this.dataArray = br.ReadBytes(this.dataSize);

		return true;
	}
    }
}
