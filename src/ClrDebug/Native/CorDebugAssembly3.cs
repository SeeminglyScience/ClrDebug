using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Logically extends the <c>ICorDebugAssembly</c> interface to provide support
    /// for container assemblies and their contained assemblies.
    /// </summary>
    /// <remarks>
    /// NOTE:
    ///
    /// The interface is available with .NET Native only. Attempting to call
    /// <c>QueryInterface</c> to retrieve an interface pointer returns
    /// <c>E_NOINTERFACE</c> for <c>ICorDebug</c> scenarios outside of .NET Native.
    /// </remarks>
    public unsafe class CorDebugAssembly3 : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets an enumerator for the assemblies contained in this assembly.
        /// </summary>
        /// <param name="assemblies">
        /// A pointer to the address of an <c>ICorDebugAssemblyEnum</c> interface
        /// object that is the enumerator.
        /// </param>
        /// <returns>
        /// <c>S_OK</c> if this <c>ICorDebugAssembly3</c> object is a container;
        /// otherwise, <c>S_FALSE</c>, and the enumeration is empty.
        /// </returns>
        /// <remarks>
        /// Symbols are needed to enumerate the contained assemblies. If they aren't
        /// present, the method returns <c>S_FALSE</c>, and no valid enumerator is provided.
        ///
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int EnumerateContainedAssemblies(out CorDebugComEnum<CorDebugAssembly> assemblies)
            => InvokeGetObject(_this, This[0]->EnumerateContainedAssemblies, out assemblies);

        /// <summary>
        /// Returns the container assembly of this <c>ICorDebugAssembly3</c> object.
        /// </summary>
        /// <param name="assembly">
        /// A pointer to the address of an <c>ICorDebugAssembly</c> object that represents
        /// the container assembly, or <c>null</c> if the method call fails.
        /// </param>
        /// <returns>
        /// <c>S_OK</c> if the method call succeeds; otherwise, <c>S_FALSE</c>, and
        /// <c>ppAssembly</c> is null.
        /// </returns>
        /// <remarks>
        /// If this assembly has been merged with others inside a single container assembly,
        /// this method returns the container assembly. For more information and terminology,
        /// see the <c>ICorDebugProcess6::EnableVirtualModuleSplitting</c> topic.
        ///
        /// NOTE:
        ///
        /// This method is available with .NET Native only.
        /// </remarks>
        public int GetContainerAssembly(out CorDebugAssembly assembly)
            => InvokeGetObject(_this, This[0]->GetContainerAssembly, out assembly);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetContainerAssembly;

            public void* EnumerateContainedAssemblies;
        }
    }
}
