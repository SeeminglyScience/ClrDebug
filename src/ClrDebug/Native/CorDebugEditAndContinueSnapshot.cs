using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugEditAndContinueSnapshot : Unknown
    {
        private ICorDebugEditAndContinueSnapshotVtable** This => (ICorDebugEditAndContinueSnapshotVtable**)DangerousGetPointer();

        public int CopyMetaData(void* stream, out Guid mvid)
        {
            Guid pMvid = default;
            int result = Calli(_this, This[0]->CopyMetaData, stream, &pMvid);
            mvid = pMvid;
            return result;
        }

        public int GetMvid(out Guid mvid)
        {
            Guid pMvid = default;
            int result = Calli(_this, This[0]->GetMvid, &pMvid);
            mvid = pMvid;
            return result;
        }

        public int GetRoDataRVA(out uint roDataRVA) => InvokeGet(_this, This[0]->GetRoDataRVA, out roDataRVA);

        public int GetRwDataRVA(out uint rwDataRVA) => InvokeGet(_this, This[0]->GetRwDataRVA, out rwDataRVA);

        public int SetPEBytes(in void* stream) => Calli(_this, This[0]->SetPEBytes, stream);

        public int SetILMap(uint mdFunction, in ReadOnlySpan<COR_IL_MAP> map)
        {
            fixed (void* pMap = map)
            {
                return Calli(_this, This[0]->SetILMap, mdFunction, map.Length, pMap);
            }
        }

        public int SetPESymbolBytes(void* stream) => Calli(_this, This[0]->SetPESymbolBytes, stream);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugEditAndContinueSnapshotVtable
        {
            public IUnknownVtable IUnknown;

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
