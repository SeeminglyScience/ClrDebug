using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Defines the base interface from which all <c>ICorDebug</c>
    /// debug events derive.
    /// </summary>
    /// <remarks>
    /// The following interfaces are derived from the <c>ICorDebugDebugEvent</c>
    /// interface:
    ///
    /// - <c>ICorDebugExceptionDebugEvent</c>
    /// - <c>ICorDebugModuleDebugEvent</c>
    ///
    /// NOTE:
    ///
    /// The interface is available with .NET Native only. Attempting to
    /// call <c>QueryInterface</c> to retrieve an interface pointer returns
    /// <c>E_NOINTERFACE</c> for <c>ICorDebug</c> scenarios outside of .NET Native.
    /// </remarks>
    public unsafe class CorDebugDebugEvent : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Indicates what kind of event this <c>ICorDebugDebugEvent</c> object
        /// represents.
        /// </summary>
        /// <param name="debugEventKind">
        /// A pointer to a <c>CorDebugDebugEventKind</c> enumeration member that
        /// indicates the type of event.
        /// </param>
        /// <remarks>
        /// Based on the value of <c>pDebugEventKind</c>, you can call
        /// <c>QueryInterface</c> to get a more precise debug event interface that
        /// has additional data.
        ///
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int GetEventKind(out CorDebugDebugEventKind debugEventKind)
        {
            fixed (void* pDebugEventKind = &debugEventKind)
            {
                return Calli(_this, This[0]->GetEventKind, pDebugEventKind);
            }
        }

        /// <summary>
        /// Gets the thread on which the event occurred.
        /// </summary>
        /// <param name="ppThread">
        /// A pointer to the address of an <c>ICorDebugThread</c> object that
        /// represents the thread on which the event occurred.
        /// </param>
        /// <remarks>
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int GetThread(out CorDebugThread thread)
            => InvokeGetObject(_this, This[0]->GetThread, out thread);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetEventKind;

            public void* GetThread;
        }
    }
}
