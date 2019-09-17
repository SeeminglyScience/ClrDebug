using System;
using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;
using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    public unsafe class CorDebugCode : Unknown
    {
        private ICorDebugCodeVtable** This => (ICorDebugCodeVtable**)DangerousGetPointer();

        public int IsIL(out bool bIL) => InvokeGet(_this, This[0]->IsIL, out bIL);

        public int GetFunction(out CorDebugFunction function) => InvokeGetObject(_this, This[0]->GetFunction, out function);

        public int GetAddress(out ulong start) => InvokeGet(_this, This[0]->GetAddress, out start);

        public int GetSize(out uint cBytes) => InvokeGet(_this, This[0]->GetSize, out cBytes);

        public int CreateBreakpoint(uint offset, out CorDebugFunctionBreakpoint breakpoint)
        {
            void** pBreakpoint = default;
            int result = Calli(_this, This[0]->CreateBreakpoint, offset, &pBreakpoint);
            ComFactory.Create(pBreakpoint, out breakpoint);
            return result;
        }

        public int GetCode(uint startOffset, uint endOffset, uint cBufferAlloc, ref Span<byte> buffer, out uint cBufferSize)
        {
            fixed (void* pBuffer = buffer)
            {
                uint pcBufferSize = default;
                int result = Calli(_this, This[0]->GetCode, startOffset, endOffset, cBufferAlloc, pBuffer, &pcBufferSize);
                cBufferSize = pcBufferSize;
                return result;
            }
        }

        public int GetVersionNumber(out uint nVersion) => InvokeGet(_this, This[0]->GetVersionNumber, out nVersion);

        public int GetILToNativeMapping(uint cMap, out uint pcMap, ref Span<COR_DEBUG_IL_TO_NATIVE_MAP> map)
        {
            fixed (void* pMap = map)
            {
                uint ppcMap = default;
                int result = Calli(_this, This[0]->GetILToNativeMapping, cMap, &ppcMap, pMap);
                pcMap = ppcMap;
                return result;
            }
        }

        public int GetEnCRemapSequencePoints(uint cMap, out uint pcMap, ref Span<uint> offsets)
        {
            fixed (void* pOffsets = offsets)
            {
                uint ppcMap = default;
                int result = Calli(_this, This[0]->GetEnCRemapSequencePoints, cMap, &ppcMap, pOffsets);
                pcMap = ppcMap;
                return result;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ICorDebugCodeVtable
        {
            public IUnknownVtable IUnknown;

            public void* IsIL;

            public void* GetFunction;

            public void* GetAddress;

            public void* GetSize;

            public void* CreateBreakpoint;

            public void* GetCode;

            public void* GetVersionNumber;

            public void* GetILToNativeMapping;

            public void* GetEnCRemapSequencePoints;
        }
    }
}
