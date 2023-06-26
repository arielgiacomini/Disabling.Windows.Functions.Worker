using System.Runtime.InteropServices;

namespace DisableFunctionsWindows
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Execute()
        {

        }
    }
}