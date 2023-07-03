using System.Runtime.InteropServices;
using Worker.Enum;

namespace Worker
{
    public static class FunctionsWindows
    {
        [DllImport("kernel32.dll")]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public static void ModifyStateWindowsToNotHibern()
        {
            SetThreadExecutionState(
                EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }
    }
}