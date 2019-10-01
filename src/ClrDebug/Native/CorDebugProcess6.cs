using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Logically extends the <c>ICorDebugProcess</c> interface to enable
    /// features such as decoding managed debug events that are encoded
    /// in native exception debug events and virtual module splitting.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// The interface is available with .NET Native only. Attempting to
    /// call <c>QueryInterface</c> to retrieve an interface pointer returns
    /// <c>E_NOINTERFACE</c> for ICorDebug scenarios outside of .NET Native.
    /// </remarks>
    public unsafe class CorDebugProcess6 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Decodes managed debug events that have been encapsulated in the
        /// payload of specially crafted native exception debug events.
        /// </summary>
        /// <param name="record">
        /// A pointer to a byte array from a native exception debug event that
        /// includes information about a managed debug event.
        /// </param>
        /// <param name="countBytes">
        /// The number of elements in the <c>pRecord</c> byte array.
        /// </param>
        /// <param name="format">
        /// A <c>CorDebugRecordFormat</c> enumeration member that specifies the format
        /// of the unmanaged debug event.
        /// </param>
        /// <param name="dwFlags">
        /// A bit field that depends on the target architecture and that specifies additional
        /// information about the debug event. For Windows systems, it can be a member of the
        /// <c>CorDebugDecodeEventFlagsWindows</c> enumeration.
        /// </param>
        /// <param name="dwThreadId">
        /// The operating system identifier of the thread on which the exception was thrown.
        /// </param>
        /// <param name="ppEvent">
        /// A pointer to the address of an <c>ICorDebugDebugEvent</c> object that represents
        /// a decoded managed debug event.
        /// </param>
        /// <remarks>
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int DecodeEvent(
            Span<byte> record,
            CorDebugRecordFormat format,
            uint dwFlags,
            uint dwThreadId,
            out CorDebugDebugEvent @event)
        {
            void** pEvent = default;
            fixed (void* pRecord = record)
            {
                int hResult = Calli(
                    _this,
                    This[0]->DecodeEvent,
                    pRecord,
                    record.Length,
                    (int)format,
                    dwFlags,
                    dwThreadId,
                    &pEvent);

                ComFactory.Create(pEvent, hResult, out @event);
                return hResult;
            }
        }

        /// <summary>
        /// Enables or disables virtual module splitting.
        /// </summary>
        /// <param name="enableSplitting">
        /// <c>true</c> to enable virtual module splitting; <c>false</c>
        /// to disable it.
        /// </param>
        /// <remarks>
        /// Virtual module splitting causes <c>ICorDebug</c> to recognize modules
        /// that were merged together during the build process and present them as
        /// a group of separate modules rather than a single large module. Doing
        /// this changes the behavior of various <c>ICorDebug</c> methods described below.
        ///
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        ///
        ///
        ///
        /// This method can be called and the value of <c>enableSplitting</c> can be
        /// changed at any time. It does not cause any stateful functional changes in an
        /// ICorDebug object, other than altering the behavior of the methods listed in the
        /// Virtual module splitting and the unmanaged debugging APIs section at the time
        /// they are called. Using virtual modules does incur a performance penalty when
        /// calling those methods. In addition, significant in-memory caching of the
        /// virtualized metadata may be required to correctly implement the <c>IMetaDataImport</c>
        /// APIs, and these caches may be retained even after virtual module splitting
        /// has been turned off.
        /// </remarks>
        public int EnableVirtualModuleSplitting(bool enableSplitting)
        {
            return Calli(_this, This[0]->EnableVirtualModuleSplitting, enableSplitting.ToNativeInt());
        }

        /// <summary>
        /// Gets information about the managed code at a particular code address.
        /// </summary>
        /// <param name="codeAddress">
        /// [in] A CORDB_ADDRESS value that specifies the starting address of the managed code segment.
        /// </param>
        /// <param name="ppCode">
        /// [out] A pointer to the address of an "ICorDebugCode" object that represents a segment of managed code.
        /// </param>
        /// <remarks>
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int GetCode(long codeAddress, out CorDebugCode code)
        {
            void** pCode = default;
            int hResult = Calli(_this, This[0]->GetCode, codeAddress, &pCode);
            ComFactory.Create(pCode, hResult, out code);
            return hResult;
        }

        /// <summary>
        /// Provides information on runtime exported functions to help
        /// step through managed code.
        /// </summary>
        /// <param name="pszExportName">
        /// The name of a runtime export function as written in the PE
        /// export table.
        /// </param>
        /// <param name="invokeKind">
        /// A pointer to a member of the <c>CorDebugCodeInvokeKind</c> enumeration
        /// that describes how the exported function will invoke managed code.
        /// </param>
        /// <param name="invokePurpose">
        /// A pointer to a member of the <c>CorDebugCodeInvokePurpose</c> enumeration
        /// that describes why the exported function will call managed code.
        /// </param>
        /// <returns>
        /// The method can return the values listed in the following table.
        ///
        /// - <c>S_OK</c>: The method call was successful.
        /// - <c>E_POINTER</c>: <c>pInvokeKind</c> or <c>pInvokePurpose</c> is null.
        /// - Other failing <c>HRESULT</c> values: As appropriate.
        /// </returns>
        /// <remarks>
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int GetExportStepInfo(
            ReadOnlySpan<char> szExportName,
            out CorDebugCodeInvokeKind invokeKind,
            out CorDebugCodeInvokePurpose invokePurpose)
        {
            fixed (void* pszExportName = szExportName)
            fixed (void* pInvokeKind = &invokeKind)
            fixed (void* pInvokePurpose = &invokePurpose)
            {
                return Calli(_this, This[0]->GetExportStepInfo, pszExportName, pInvokeKind, pInvokePurpose);
            }
        }

        /// <summary>
        /// Changes the internal state of the debugee so that the
        /// <c>System.Diagnostics.Debugger.IsAttached</c> method in
        /// the .NET Framework Class Library returns <c>true</c>.
        /// </summary>
        /// <param name="fIsAttached">
        /// <c>true</c> if the System.Diagnostics.Debugger.IsAttached method
        /// should indicate that a debugger is attached; <c>false</c> otherwise.
        /// </param>
        /// <returns>
        /// The method can return the values listed in the following table.
        ///
        /// - <c>S_OK</c>: The debuggee was successfully updated.
        ///
        /// - <c>CORDBG_E_MODULE_NOT_LOADED</c>: The assembly that contains the
        ///   <c>System.Diagnostics.Debugger.IsAttached</c> method is not loaded,
        ///   or some other error, such as missing metadata, is preventing it from
        ///   being recognized. This error is common and benign. You should call
        ///   the method again when additional assemblies load.
        ///
        /// - Other failing <c>HRESULT</c> values: Other values likely indicate
        ///   misbehaving debugger or compiler components.
        /// </returns>
        /// <remarks>
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int MarkDebuggerAttached(bool fIsAttached)
        {
            int hResult = Calli(_this, This[0]->MarkDebuggerAttached, fIsAttached.ToNativeInt());
            return hResult;
        }

        /// <summary>
        /// Notifies <c>ICorDebug</c> that the process is running.
        /// </summary>
        /// <param name="change">
        /// A member of the <c>ProcessStateChanged</c> enumeration
        /// </param>
        /// <remarks>
        /// The debugger calls this method to notify <c>ICorDebug</c>
        /// that the process is running.
        ///
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int ProcessStateChanged(CorDebugStateChange change)
        {
            return Calli(_this, This[0]->ProcessStateChanged, (int)change);
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* DecodeEvent;

            public void* ProcessStateChanged;

            public void* GetCode;

            public void* EnableVirtualModuleSplitting;

            public void* MarkDebuggerAttached;

            public void* GetExportStepInfo;
        }
    }
}
