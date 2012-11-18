using System;
using NUnit.Framework;
using ambient.wavelooper;

namespace ambient.wavelooper.testing
{
	[TestFixture]
	public class waveOpenTest
	{
		public void TestFileOpen()
		{
			WaveFile wav = new WaveFile();
			Assert.IsTrue(wav.canOpenFile("gtr.wav"));
		}
	}
}
