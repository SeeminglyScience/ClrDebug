using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents an assembly.
    /// </summary>
    public unsafe class CorDebugAssembly : Unknown
    {
        private ICorDebugAssemblyVtable** This => (ICorDebugAssemblyVtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the process in which this instance is running.
        /// </summary>
        public int GetProcess(out CorDebugProcess process)
            => InvokeGetObject(_this, This[0]->GetProcess, out process);

        /// <summary>
        /// Gets the application domain that contains this instance.
        /// </summary>
        public int GetAppDomain(out CorDebugAppDomain appDomain)
            => InvokeGetObject(_this, This[0]->GetAppDomain, out appDomain);

        /// <summary>
        /// Gets the modules contained in this instance.
        /// </summary>
        public int EnumerateModules(out CorDebugEnum<CorDebugModule> modules)
            => InvokeGetObject(_this, This[0]->EnumerateModules, out modules);

        [Obsolete("This method is not implemented in the current version of the .NET Framework.")]
        public int GetCodeBase(Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetCodeBase, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        /// <summary>
        /// Gets the name of the assembly that this ICorDebugAssembly instance represents.
        /// </summary>
        /// <remarks>The name returned is the full path and file name of the assembly.</remarks>
        public int GetName(Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugAssemblyVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetProcess;

            public void* GetAppDomain;

            public void* EnumerateModules;

            public void* GetCodeBase;

            public void* GetName;
        }
    }
}
