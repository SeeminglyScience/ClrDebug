using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Exposes the monitor lock properties of objects. This interface extends
    /// the <c>ICorDebugHeapValue</c> and <c>ICorDebugHeapValue2</c> interfaces.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugHeapValue3 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Provides an ordered list of threads that are queued on the event
        /// that is associated with a monitor lock.
        /// </summary>
        /// <param name="threadEnum">
        /// The <c>ICorDebugThreadEnum</c> enumerator that provides the ordered
        /// list of threads.
        /// </param>
        /// <returns>
        /// This method returns the following specific HRESULTs as well as
        /// HRESULT errors that indicate method failure.
        ///
        /// - <c>S_OK</c>: The list is not empty.
        ///
        /// - <c>S_FALSE</c>: The list is empty.
        /// </returns>
        /// <remarks>
        /// The first thread in the list is the first thread that is released by the
        /// next call to <c>System.Threading.Monitor.Pulse(System.Object)</c>. The
        /// next thread in the list is released on the following call, and so on.
        ///
        /// If the list is not empty, this method returns <c>S_OK</c>. If the list is
        /// empty, the method returns <c>S_FALSE</c>; in this case, the enumeration is
        /// still valid, although it is empty.
        ///
        /// In either case, the enumeration interface is usable only for the duration
        /// of the current synchronized state. However, the thread's interfaces
        /// dispensed from it are valid until the thread exits.
        ///
        /// If <see paramref="ppThreadEnum" /> is not a valid pointer, the result is
        /// undefined.
        ///
        /// If an error occurs such that it cannot be determined which, if any, threads
        /// are waiting for the monitor, the method returns an HRESULT that indicates
        /// failure.
        /// </remarks>
        public int GetMonitorEventWaitList(out CorDebugComEnum<CorDebugThread> threadEnum)
            => InvokeGetObject(_this, This[0]->GetMonitorEventWaitList, out threadEnum);

        /// <summary>
        /// Returns the managed thread that owns the monitor lock on this object.
        /// </summary>
        /// <param name="thread">
        /// The managed thread that owns the monitor lock on this object.
        /// </param>
        /// <param name="acquisitionCount">
        /// The number of times this thread would have to release the lock before
        /// it returns to being unowned.
        /// </param>
        /// <returns>
        /// This method returns the following specific HRESULTs as well as
        /// HRESULT errors that indicate method failure.
        ///
        /// - <c>S_OK</c>: The method completed successfully.
        ///
        /// - <c>S_FALSE</c>: No managed thread owns the monitor lock on this object.
        /// </returns>
        /// <remarks>
        /// If a managed thread owns the monitor lock on this object:
        ///
        /// - The method returns <c>S_OK</c>.
        /// - The thread object is valid until the thread exits.
        ///
        /// If no managed thread owns the monitor lock on this object,
        /// <see paramref="ppThread" /> and <see paramref="pAcquisitionCount" /> are
        /// unchanged, and the method returns <c>S_FALSE</c>.
        ///
        /// If <see paramref="ppThread" /> or <see paramref="pAcquisitionCount" /> is not
        /// a valid pointer, the result is undefined.
        ///
        /// If an error occurs such that it cannot be determined which, if any, thread
        /// owns the monitor lock on this object, the method returns an HRESULT that
        /// indicates failure.
        /// </remarks>
        public int GetThreadOwningMonitorLock(out CorDebugThread thread, out int acquisitionCount)
        {
            void** pThread = default;
            fixed (void* pAcquisitionCount = &acquisitionCount)
            {
                int hResult = Calli(_this, This[0]->GetThreadOwningMonitorLock, &pThread, pAcquisitionCount);
                ComFactory.Create(pThread, hResult, out thread);
                return hResult;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetThreadOwningMonitorLock;

            public void* GetMonitorEventWaitList;
        }
    }
}
