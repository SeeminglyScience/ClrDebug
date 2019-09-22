using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugAppDomain : Unknown
    {
        private ICorDebugAppDomainVtable** This => (ICorDebugAppDomainVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the process containing the application domain.
        /// </summary>
        /// <param name="process">The object that represents the process.</param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int GetProcess(out CorDebugProcess process)
            => InvokeGetObject(_this, This[0]->GetProcess, out process);

        /// <summary>
        /// Gets an enumerator for the assemblies in the application domain.
        /// </summary>
        /// <param name="assemblies">The assemblies in the application domain.</param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int EnumerateAssemblies(out CorDebugEnum<CorDebugAssembly> assemblies)
            => InvokeGetObject(_this, This[0]->EnumerateAssemblies, out assemblies);

        /// <summary>
        /// Gets the module that corresponds to the given metadata interface.
        /// </summary>
        /// <param name="metadata">
        /// An object that is one of the Metadata interfaces.
        /// See https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/metadata/metadata-interfaces.
        /// </param>
        /// <param name="module">
        /// The module corresponding to the given metadata interface.
        /// </param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int GetModuleFromMetaDataInterface(in Unknown metadata, out CorDebugModule module)
        {
            void* pModule = default;
            int result = Calli(_this, This[0]->GetModuleFromMetaDataInterface, metadata, &pModule);
            module = ComFactory.Create<CorDebugModule>((void**)pModule);
            return result;
        }

        /// <summary>
        /// Gets an enumerator for all active breakpoints in the application domain.
        /// </summary>
        /// <param name="breakpoints">All active breakpoints in the application domain.</param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int EnumerateBreakpoints(out CorDebugEnum<CorDebugBreakpoint> breakpoints)
            => InvokeGetObject(_this, This[0]->EnumerateBreakpoints, out breakpoints);

        /// <summary>
        /// Gets an enumerator for all active steppers in the application domain.
        /// </summary>
        /// <param name="steppers">All active steppers in the application domain.</param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int EnumerateSteppers(out CorDebugEnum<CorDebugStepper> steppers)
            => InvokeGetObject(_this, This[0]->EnumerateSteppers, out steppers);

        /// <summary>
        /// Gets a value that indicates whether the debugger is attached to the application domain.
        /// </summary>
        /// <param name="bAttached">
        /// <see langword="true" /> if the debugger is attached to the application domain;
        /// otherwise, <see langword="false" />.
        /// </param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int IsAttached(out bool bAttached)
            => InvokeGet(_this, This[0]->IsAttached, out bAttached);

        /// <summary>
        /// Gets the name of the application domain.
        /// </summary>
        /// <param name="szName">
        /// Stores the name of the application domain.
        /// </param>
        /// <param name="charsUsed">
        /// The size of the name or the number of characters actually returned in <see paramref="szName" />.
        /// In query mode, this value lets the caller know how large a buffer to allocate for
        /// the name.
        /// </param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        /// <remarks>
        /// A debugger calls the this method once to get the size of a buffer needed. The
        /// debugger allocates the buffer, and then calls the method a second time to fill
        /// the buffer. The first call, to get the size of the name, is referred to as
        /// query mode.
        /// </remarks>
        public int GetName(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        /// <summary>
        /// Gets an interface pointer to the common language runtime (CLR) application domain.
        /// </summary>
        /// <param name="@object">
        /// The interface object that represents the CLR application domain.
        /// </param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int GetObject(out CorDebugValue @object) => InvokeGetObject(_this, This[0]->GetObject, out @object);

        /// <summary>Attaches the debugger to the application domain.</summary>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        /// <remarks>
        /// The debugger must be attached to the application domain to receive events and
        /// to enable debugging of the application domain.
        /// </remarks>
        public int Attach() => Calli(_this, This[0]->Attach);


        /// <summary>Gets the unique identifier of the application domain.</summary>
        /// <param name="id">The unique identifier of the application domain.</param>
        /// <returns>The HRESULT if the method fails, otherwise zero.</returns>
        public int GetID(out uint id) => InvokeGet(_this, This[0]->GetID, out id);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugAppDomainVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetProcess;

            public void* EnumerateAssemblies;

            public void* GetModuleFromMetaDataInterface;

            public void* EnumerateBreakpoints;

            public void* EnumerateSteppers;

            public void* IsAttached;

            public void* GetName;

            public void* GetObject;

            public void* Attach;

            public void* GetID;
        }
    }
}
