using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods to retrieve information about the managed
    /// representations of Windows Runtime types currently loaded in
    /// an application domain. This interface is an extension of the
    /// <c>ICorDebugAppDomain</c> and <c>ICorDebugAppDomain2</c> interfaces.
    /// </summary>
    /// <remarks>
    /// This interface is meant to be used by a debugger in conjunction
    /// with a function evaluation call to <c>System.Runtime.InteropServices.Marshal.GetInspectableIIDs(System.Object)</c>.
    /// When the method retrieves the interface identifiers supported by a
    /// Windows Runtime server object, the debugger may use the methods defined
    /// in this interface to map them to managed types that correspond to those
    /// interfaces.
    ///
    /// To retrieve an instance of this interface, run <c>QueryInterface</c> on
    /// an instance of the ICorDebugAppDomain or ICorDebugAppDomain2 interface.
    ///
    /// NOTE:
    ///
    /// This interface does not support being called remotely, either cross-machine or cross-process.
    /// </remarks>
    public unsafe class CorDebugAppDomain3 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets an enumerator for all cached Windows Runtime types.
        /// </summary>
        /// <param name="guidToTypeEnum">
        /// A pointer to an <c>ICorDebugGuidToTypeEnum</c> interface object that
        /// can enumerate the managed representations of Windows Runtime types
        /// currently loaded in the application domain.
        /// </param>
        public int GetCachedWinRTTypes(out CorDebugStructEnum<CorDebugGuidToTypeMapping> guidToTypeEnum)
            => InvokeGetObject(_this, This[0]->GetCachedWinRTTypes, out guidToTypeEnum);

        /// <summary>
        /// Gets an enumerator for cached Windows Runtime types in an application
        /// domain based on their interface identifiers.
        /// </summary>
        /// <param name="cReqTypes">
        /// The number of required types.
        /// </param>
        /// <param name="iidsToResolve">
        /// A pointer to an array that contains the interface identifiers
        /// corresponding to the managed representations of the Windows Runtime
        /// types to be retrieved.
        /// </param>
        /// <param name="typesEnum">
        /// A pointer to the address of an <c>ICorDebugTypeEnum</c> interface object
        /// that allows enumeration of the cached managed representations of the
        /// Windows Runtime types retrieved, based on the interface identifiers in
        /// <c>iidsToResolve</c>.
        /// </param>
        /// <remarks>
        /// If the method fails to retrieve information for a specific interface
        /// identifier, the corresponding entry in the <c>ICorDebugTypeEnum</c>
        /// collection will have a type of <c>ELEMENT_TYPE_END</c> for errors due
        /// to data retrieval issues, or <c>ELEMENT_TYPE_VOID</c> for unknown
        /// interface identifiers.
        /// </remarks>
        public int GetCachedWinRTTypesForIIDs(
            Span<Guid> iidsToResolve,
            out CorDebugComEnum<CorDebugType> typesEnum)
        {
            void** pTypesEnum = default;
            fixed (void* piidsToResolve = iidsToResolve)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetCachedWinRTTypesForIIDs,
                    iidsToResolve.Length,
                    piidsToResolve,
                    &pTypesEnum);

                ComFactory.Create(pTypesEnum, hResult, out typesEnum);
                return hResult;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetCachedWinRTTypesForIIDs;

            public void* GetCachedWinRTTypes;
        }
    }
}
