using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugMDA : Unknown
    {
        private ICorDebugMDAVtable** This => (ICorDebugMDAVtable**)DangerousGetPointer();

        public int GetName(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        public int GetDescription(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetDescription, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        public int GetXML(ref Span<char> szName, out uint charsUsed)
        {
            fixed (void* pszName = szName)
            fixed (void* pCharsUsed = &charsUsed)
            {
                return Calli(_this, This[0]->GetXML, (uint)szName.Length, pCharsUsed, pszName);
            }
        }

        public int GetFlags(out CorDebugMDAFlags flags)
        {
            int pFlags = default;
            int result = Calli(_this, This[0]->GetFlags, &pFlags);
            flags = (CorDebugMDAFlags)pFlags;
            return result;
        }

        public int GetOSThreadId(out uint osTid) => InvokeGet(_this, This[0]->GetOSThreadId, out osTid);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugMDAVtable
        {
            public IUnknownVtable IUnknown;

            public void* GetName;

            public void* GetDescription;

            public void* GetXML;

            public void* GetFlags;

            public void* GetOSThreadId;
        }
    }
}
