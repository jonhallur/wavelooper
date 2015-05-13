import System;
import System.Windows.Forms;
import SoundForge;

//Run with one file open and the Region Lists visible (Menu: View, Regions List)
//Sets markers at set intervals, creating names in this format: prefix+time+suffix
//NOTE: Look for MODIFY HERE to quickly update the script with your own preferences
//NOTE: Uses an undo wrapper, so that you can undo all markers in one step

public class EntryPoint {
public function Begin(app : IScriptableApp) {

   var wnd = app.ActiveWindow;
   if (null == wnd)
   {     
     app.SetStatusText("Open a file before running this script.");      
     return;
   }

   //MODIFY HERE. 
   var step : double = 0.1; //change from every 12.3 seconds to something else
   var szPre  = "pre"; //first part of the marker name. Type "" to leave blank
   var szPost = "post"; //last part of the marker name. Type "" to leave blank


   var file : ISfFileHost = app.CurrentFile;
   var currentSelection = new SfAudioSelection(wnd);
   var ccStart : Int64 = currentSelection.ccStart;
   //var ccLength : Int64 = file.Length;
   var ccLength : Int64 = currentSelection.ccLength;
   var ccEndSelection : Int64 = ccStart + ccLength;
   // create an undo wrapper, so we don't end up with an undo for every marker that we create...
   // 
   var szUndo = "Find Best Zero Crossing";
   var idUndo : int = file.BeginUndo(szUndo);
   var first : double = file.GetSample(ccStart, 0);
   var lastSample : double = first;
   var lastKnownGood : double  = Number.MAX_VALUE;
   var lastKnownGoodPos : Int64  = null;

   for (var ccPos : Int64 = ccStart; ccPos < ccEndSelection; ccPos ++) {
      var sample : double = file.GetSample(ccPos, 0);
      if (sample >= 0.0 && lastSample < 0.0)
      {
         if(lastKnownGoodPos == null || sample < lastKnownGood) {
            lastKnownGoodPos = ccPos;
            lastKnownGood = sample;
         }
      }
      lastSample = sample;
   }
   if (lastKnownGoodPos != ccStart) {
      var mk : SfAudioMarker = new SfAudioMarker(lastKnownGoodPos);
      var name = "Best Zero crossing"
      mk.Name = name.Trim();
      file.Markers.Add(mk);   
   }
   
   // close the undo wrapper, and tell it to keep the changes.
   //
   file.EndUndo(idUndo, false);


}

public function FromSoundForge(app : IScriptableApp) {
   ForgeApp = app;
   app.SetStatusText(String.Format("Script '{0}' is running.", Script.Name));
   Begin(app);
   app.SetStatusText(String.Format("Script '{0}' is done.", Script.Name));
}
public var ForgeApp : IScriptableApp = null;
public function DPF(sz) { ForgeApp.OutputText(sz);}
public function DPF(sz,o) { ForgeApp.OutputText(System.String.Format(sz,o)); }
public function DPF(sz,o,o2) { ForgeApp.OutputText(System.String.Format(sz,o,o2)); }

public function DPF(sz,o,o2,o3) { ForgeApp.OutputText(System.String.Format(sz,o,o2,o3)); }

} // class EntryPoint
