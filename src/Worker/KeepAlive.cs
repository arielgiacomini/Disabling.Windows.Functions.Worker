using System.Runtime.InteropServices;

namespace KeepAlive
{
    public static class PowerUtilities
    {
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        private static readonly AutoResetEvent _event = new(false);

        public static void PreventPowerSave(int contador)
        {
            Console.WriteLine($"Passou aqui {contador}");
            _ = (new TaskFactory()).StartNew(() =>
            {
                _ = SetThreadExecutionState(
                    EXECUTION_STATE.ES_CONTINUOUS
                    | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                    | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                _event.WaitOne();
            },
                TaskCreationOptions.LongRunning);
        }

        public static void Shutdown()
        {
            _event.Set();
        }
    }
}