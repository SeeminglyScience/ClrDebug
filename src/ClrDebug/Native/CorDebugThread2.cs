using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Serves as a logical extension to the <see cref="CorDebugThread" /> interface.
    /// </summary>
    public unsafe class CorDebugThread2 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets information about the active function in each of
        /// this thread's frames.
        /// </summary>
        /// <param name="amountWritten">
        /// The amount of functions written to <see paramref="functions" />. The number
        /// of objects is equal to the number of managed frames on the stack.
        /// </param>
        /// <param name="functions">
        /// The destination buffer. Each item contains information about the active
        /// functions in this thread's frames.
        ///
        /// The first element will be used for the leaf frame, and so on back to the
        /// root of the stack.
        /// </param>
        public int GetActiveFunctions(out uint amountWritten, Span<COR_ACTIVE_FUNCTION> functions)
        {
            fixed (void* pAmountWritten = &amountWritten)
            fixed (void* pFunctions = functions)
            {
                return Calli(
                    _this,
                    This[0]->GetActiveFunctions,
                    functions.Length,
                    &pAmountWritten,
                    pFunctions);
            }
        }

        /// <summary>
        /// Gets the connection identifier for this object.
        /// </summary>
        /// <remarks>
        /// This method returns zero in the <see paramref="dwConnectionId" /> parameter,
        /// if this thread is not part of a connection.
        ///
        /// If this thread is connected to an instance of Microsoft SQL Server 2005
        /// Analysis Services (SSAS), the CONNID maps to a server process identifier (SPID).
        /// </remarks>
        public int GetConnectionID(uint dwConnectionId) => InvokeGet(_this, This[0]->GetConnectionID, out dwConnectionId);

        /// <summary>
        /// Gets the identifier of the task running on this thread.
        /// </summary>
        /// <remarks>
        /// A task can only be running on the thread if the thread is associated with
        /// a connection. <see cref="GetTaskID(out ulong)" /> returns zero in <see paramref="taskId" />
        /// if the thread is not associated with a connection.
        /// </remarks>
        public int GetTaskID(out ulong taskId) => InvokeGet(_this, This[0]->GetTaskID, out taskId);

        /// <summary>
        /// Gets the operating system thread identifier for this thread.
        /// </summary>
        public int GetVolatileOSThreadID(out uint dwTid) => InvokeGet(_this, This[0]->GetVolatileOSThreadID, out dwTid);

        /// <summary>
        /// Allows a debugger to intercept the current exception on this thread.
        /// </summary>
        /// <remarks>
        /// This method can be called between an exception callback (<c>ICorDebugManagedCallback::Exception</c>
        /// or <c>ICorDebugManagedCallback2::Exception</c>) and the associated call to <see cref="CorDebugController.Continue(bool)" />.
        /// </remarks>
        public int InterceptCurrentException(CorDebugFrame frame)
        {
            using var pFrame = frame?.AcquirePointer();
            return Calli(_this, This[0]->InterceptCurrentException, pFrame);
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetActiveFunctions;

            public void* GetConnectionID;

            public void* GetTaskID;

            public void* GetVolatileOSThreadID;

            public void* InterceptCurrentException;
        }
    }
}
