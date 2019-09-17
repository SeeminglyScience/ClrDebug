using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugProcess : CorDebugController
    {
        private ICorDebugProcessVtable** This => (ICorDebugProcessVtable**)DangerousGetPointer();

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugProcessVtable
        {
            public ICorDebugControllerVtable ICorDebugController;
        }
    }
}
