using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods for debugging application domains.
    /// </summary>
    public unsafe class CorDebugAppDomain : CorDebugController
    {
        private ICorDebugAppDomainVtable** This => (ICorDebugAppDomainVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the process containing the application domain.
        /// </summary>
        public int GetProcess(out CorDebugProcess process)
            => InvokeGetObject(_this, This[0]->GetProcess, out process);

        /// <summary>
        /// Gets all of the assemblies in the application domain.
        /// </summary>
        public int EnumerateAssemblies(out CorDebugComEnum<CorDebugAssembly> assemblies)
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
        public int GetModuleFromMetaDataInterface(Unknown metadata, out CorDebugModule module)
        {
            void* pModule = default;
            using var pMetadata = metadata?.AcquirePointer();
            int result = Calli(_this, This[0]->GetModuleFromMetaDataInterface, pMetadata, &pModule);
            module = ComFactory.Create<CorDebugModule>(pModule);
            return result;
        }

        /// <summary>
        /// Gets all active breakpoints in the application domain.
        /// </summary>
        public int EnumerateBreakpoints(out CorDebugComEnum<CorDebugBreakpoint> breakpoints)
            => InvokeGetObject(_this, This[0]->EnumerateBreakpoints, out breakpoints);

        /// <summary>
        /// Gets all active steppers in the application domain.
        /// </summary>
        public int EnumerateSteppers(out CorDebugComEnum<CorDebugStepper> steppers)
            => InvokeGetObject(_this, This[0]->EnumerateSteppers, out steppers);

        /// <summary>
        /// Gets a value that indicates whether the debugger is attached to the application domain.
        /// </summary>
        /// <param name="bAttached">
        /// <see langword="true" /> if the debugger is attached to the application domain;
        /// otherwise, <see langword="false" />.
        /// </param>
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
        /// <remarks>
        /// A debugger calls the this method once to get the size of a buffer needed. The
        /// debugger allocates the buffer, and then calls the method a second time to fill
        /// the buffer. The first call, to get the size of the name, is referred to as
        /// query mode.
        /// </remarks>
        public int GetName(Span<char> szName, out uint charsUsed)
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
        public int GetObject(out CorDebugValue @object) => InvokeGetObject(_this, This[0]->GetObject, out @object);

        /// <summary>Attaches the debugger to the application domain.</summary>
        /// <remarks>
        /// The debugger must be attached to the application domain to receive events and
        /// to enable debugging of the application domain.
        /// </remarks>
        public int Attach() => Calli(_this, This[0]->Attach);


        /// <summary>Gets the unique identifier of the application domain.</summary>
        public int GetID(out uint id) => InvokeGet(_this, This[0]->GetID, out id);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugAppDomainVtable
        {
            public ICorDebugControllerVtable ICorDebugController;

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
