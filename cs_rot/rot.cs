using System.Collections.Generic;
using System.Runtime.InteropServices;

// TODO - use ComTypes.IRunningObjectTablel et al,...

namespace whitty.ROT
{
    public class RunningObjectTable
    {
	#region Interop imports
	[DllImport("ole32.dll")]  
	public static extern int GetRunningObjectTable(int reserved, out UCOMIRunningObjectTable prot); 
 
	[DllImport("ole32.dll")]  
	public static extern int  CreateBindCtx(int reserved, out UCOMIBindCtx ppbc);
	#endregion

	public static List<string> GetRunningObjectTableNames()
	{
	    UCOMIRunningObjectTable runningObjectTable;   
	    GetRunningObjectTable(0, out runningObjectTable);    

	    UCOMIEnumMoniker monikers;
	    runningObjectTable.EnumRunning(out monikers);

	    int count;
	    UCOMIMoniker[] moniker = new UCOMIMoniker[1];
	    List<string> names = new List<string>();

	    monikers.Reset();          
	    while (monikers.Next(1, moniker, out count) == 0)
	    {     
		UCOMIBindCtx context;
		CreateBindCtx(0, out context);     

		string name;
		moniker[0].GetDisplayName(context, null, out name);

		names.Add(name);
	    } 
	    return names;
	}
    }
}
