using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace whitty.ROT
{
    public class RunningObjectTable
    {
	#region Interop imports
	[DllImport("ole32.dll")]
	public static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

	[DllImport("ole32.dll")]
	public static extern int  CreateBindCtx(int reserved, out IBindCtx ppbc);
	#endregion

	public static List<string> GetRunningObjectTableNames()
	{
	    IRunningObjectTable runningObjectTable;
	    GetRunningObjectTable(0, out runningObjectTable);

	    IEnumMoniker monikers;
	    runningObjectTable.EnumRunning(out monikers);

	    IMoniker[] moniker = new IMoniker[1];
	    List<string> names = new List<string>();

	    monikers.Reset();

	    int result = 0;
	    while (result == 0)
	    {
		unsafe
		{
		    int count;
		    System.IntPtr pCount = (System.IntPtr)(&count);
		    result = monikers.Next(1, moniker, pCount);
		}
		if (result != 0)
		    break;

		IBindCtx context;
		CreateBindCtx(0, out context);

		string name;
		moniker[0].GetDisplayName(context, null, out name);

		names.Add(name);
	    }
	    return names;
	}
    }
}
