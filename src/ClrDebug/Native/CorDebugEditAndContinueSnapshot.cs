using System;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    [Obsolete("This class is obsolete.")]
    public unsafe class CorDebugEditAndContinueSnapshot : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        [Obsolete("This method is obsolete.", error: true)]
        public int CopyMetaData(void* stream, out Guid mvid)
        {
            Guid pMvid = default;
            int result = Calli(_this, This[0]->CopyMetaData, stream, &pMvid);
            mvid = pMvid;
            return result;
        }

        [Obsolete("This method is obsolete.", error: true)]
        public int GetMvid(out Guid mvid)
        {
            Guid pMvid = default;
            int result = Calli(_this, This[0]->GetMvid, &pMvid);
            mvid = pMvid;
            return result;
        }

        [Obsolete("This method is obsolete.", error: true)]
        public int GetRoDataRVA(out uint roDataRVA) => InvokeGet(_this, This[0]->GetRoDataRVA, out roDataRVA);

        [Obsolete("This method is obsolete.", error: true)]
        public int GetRwDataRVA(out uint rwDataRVA) => InvokeGet(_this, This[0]->GetRwDataRVA, out rwDataRVA);

        [Obsolete("This method is obsolete.", error: true)]
        public int SetPEBytes(in void* stream) => Calli(_this, This[0]->SetPEBytes, stream);

        [Obsolete("This method is obsolete.", error: true)]
        public int SetILMap(uint mdFunction, in ReadOnlySpan<COR_IL_MAP> map)
        {
            fixed (void* pMap = map)
            {
                return Calli(_this, This[0]->SetILMap, mdFunction, map.Length, pMap);
            }
        }

        [Obsolete("This method is obsolete.", error: true)]
        public int SetPESymbolBytes(void* stream) => Calli(_this, This[0]->SetPESymbolBytes, stream);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* CopyMetaData;

            public void* GetMvid;

            public void* GetRoDataRVA;

            public void* GetRwDataRVA;

            public void* SetPEBytes;

            public void* SetILMap;

            public void* SetPESymbolBytes;
        }
    }
}
