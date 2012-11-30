using System;
using NUnit.Framework;
using ambient.wavelooper;

namespace ambient.wavelooper.testing
{
	[TestFixture]
	public class waveOpenTest
	{
		WaveFile wav = new WaveFile();

		[SetUp]
		public void TestFileOpen()
		{
			Assert.IsTrue(this.wav.canOpenFile("gtr.wav"));
			//Assert.IsTrue(wav.canReadHeader());
		//	Assert.AreEqual(1,1);
		}

		[Test]
                public void TestReadHeader()
                {
                        Assert.IsTrue(this.wav.canReadHeader());
                //      Assert.AreEqual(1,1);
                }

		[TearDown]
		public void TestCloseFile()
		{
			Assert.IsTrue(this.wav.closeFile());
		}

	}
}
