#include "mscorlib.h"

// .mscorlib

.typedef [mscorlib]System.Guid as Guid

// .assembly extern netstandard as mscorlib
// {
//     .publickeytoken = (CC 7B 13 FF CD 2D DD 51 )
//     .ver 2:0:0:0
// }

.module ClrDebug.Native.CalliInstructions.dll
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003
.corflags 0x00000001

.assembly 'ClrDebug.Native.CalliInstructions'
{
    // 8
    .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = (
        01 00 08 00 00 00 00 00)

    // WrapNonExceptionThrows = true
    .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = (
        01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78
        63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01)

    // 1.0.0.0
    .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = (
        01 00 07 31 2E 30 2E 30 2E 30 00 00)

    // 1.0.0.0
    .custom instance void [mscorlib]System.Reflection.AssemblyInformationalVersionAttribute::.ctor(string) = (
        01 00 07 31 2E 30 2E 30 2E 30 00 00)

    // false
    .custom instance void [mscorlib]System.CLSCompliantAttribute::.ctor(bool) = (01 00 00 00 00)

    // ClrDebug
    .custom instance void [mscorlib]System.Runtime.CompilerServices.InternalsVisibleToAttribute::.ctor(string) = (
        01 00 08 43 6C 72 44 65 62 75 67 00 00
    )

    .hash algorithm 0x0000800c
    .ver 1:0:0:0
}

.namespace ClrDebug.Native
{
    .class public sequential ansi sealed beforefieldinit Primitive128 extends [mscorlib]System.ValueType
    {
        .field private initonly int64 _field1

        .field private initonly int64 _field2
    }

    .class public auto ansi sealed abstract beforefieldinit CalliInstructions extends [mscorlib]System.Object
    {
        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, native unsigned int arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, native unsigned int, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, valuetype ClrDebug.Native.Primitive128 arg0, int32 arg1, void* arg2, void* arg3) cil managed aggressiveinlining
        {
            .maxstack 6

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.1
            calli       unmanaged stdcall int32(void*, valuetype ClrDebug.Native.Primitive128, int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, valuetype ClrDebug.Native.Primitive128 arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, valuetype ClrDebug.Native.Primitive128, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, valuetype Guid arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, valuetype Guid, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot) cil managed aggressiveinlining
        {
            .maxstack 2

            ldarg.0
            ldarg.1
            calli       unmanaged stdcall int32(void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, int32 arg0) cil managed aggressiveinlining
        {
            .maxstack 3

            ldarg.0
            ldarg.2
            ldarg.1
            calli       unmanaged stdcall int32(void*, int32)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, int32 arg0, int32 arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, int32, int32)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, int32 arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, int32 arg0, void* arg1, void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, int32 arg0, void* arg1, uint32 arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, int32, void*, uint32)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, int32 arg0, void* arg1, int32 arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, int32, void*, int32)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            int32 arg0,
            void* arg1,
            unsigned int32 arg2,
            void* arg3,
            void* arg4) cil managed aggressiveinlining
        {
            .maxstack 7

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.1
            calli       unmanaged stdcall int32(void*, int32, void*, unsigned int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint64 arg0, uint64 arg1, int32 arg2, void* arg3, void* arg4) cil managed aggressiveinlining
        {
            .maxstack 7

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint64, uint64, int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, void* arg1, void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, int32 arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, int32)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, void* arg1, int32 arg2, void* arg3) cil managed aggressiveinlining
        {
            .maxstack 6

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, void* arg1, void* arg2, int32 arg3, void* arg4) cil managed aggressiveinlining
        {
            .maxstack 7

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, void* arg1, void* arg2, int32 arg3, void* arg4, void* arg5) cil managed aggressiveinlining
        {
            .maxstack 8

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, void* arg1, void* arg2, void* arg3) cil managed aggressiveinlining
        {
            .maxstack 6

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, void* arg1, void* arg2, void* arg3, void* arg4) cil managed aggressiveinlining
        {
            .maxstack 7

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            void* arg2,
            void* arg3,
            void* arg4,
            void* arg5,
            void* arg6,
            void* arg7,
            void* arg8,
            void* arg9,
            int32 arg10,
            void* arg11) cil managed aggressiveinlining
        {
            .maxstack 14

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.s     9
            ldarg.s     10
            ldarg.s     11
            ldarg.s     12
            ldarg.s     13
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, void*, void*, void*, void*, void*, void*, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            void* arg2,
            int32 arg3,
            void* arg4,
            void* arg5,
            void* arg6,
            void* arg7,
            void* arg8,
            void* arg9,
            void* arg10,
            void* arg11,
            void* arg12) cil managed aggressiveinlining
        {
            .maxstack 15

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.s     9
            ldarg.s     10
            ldarg.s     11
            ldarg.s     12
            ldarg.s     13
            ldarg.s     14
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, int32, void*, void*, void*, void*, void*, void*, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            void* arg2,
            int32 arg3,
            void* arg4,
            void* arg5,
            void* arg6) cil managed aggressiveinlining
        {
            .maxstack 9

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, int32, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            void* arg2,
            int32 arg3,
            void* arg4,
            void* arg5,
            void* arg6,
            void* arg7,
            void* arg8,
            void* arg9) cil managed aggressiveinlining
        {
            .maxstack 9

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.s     9
            ldarg.s     10
            ldarg.s     11
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, int32, void*, void*, void*, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            uint32 arg1,
            void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, uint32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            void* arg0,
            int32 arg1,
            void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            void* arg0,
            uint32 arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, uint32)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            valuetype Guid arg1,
            void* arg2,
            void* arg3) cil managed aggressiveinlining
        {
            .maxstack 6

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, valuetype Guid, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            void* arg0,
            int32 arg1,
            void* arg2,
            void* arg3) cil managed aggressiveinlining
        {
            .maxstack 6

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            int32 arg2,
            void* arg3,
            void* arg4,
            void* arg5) cil managed aggressiveinlining
        {
            .maxstack 8

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, int32, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            void* arg2,
            void* arg3,
            int32 arg4,
            void* arg5,
            void* arg6,
            void* arg7,
            void* arg9,
            void* arg10) cil managed aggressiveinlining
        {
            .maxstack 13

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.s     9
            ldarg.s     10
            ldarg.s     11
            ldarg.s     12
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, void*, int32, void*, void*, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            void* arg2,
            int32 arg3,
            void* arg4,
            void* arg5,
            void* arg6,
            void* arg7,
            void* arg9,
            void* arg10,
            void* arg11,
            void* arg12,
            void* arg13,
            void* arg14,
            int32 arg15,
            void* arg16) cil managed aggressiveinlining
        {
            .maxstack 19

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.s     9
            ldarg.s     10
            ldarg.s     11
            ldarg.s     12
            ldarg.s     13
            ldarg.s     14
            ldarg.s     15
            ldarg.s     16
            ldarg.s     17
            ldarg.s     18
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, int32, void*, void*, void*, void*, void*, void*, void*, void*, void*, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            int32 arg2,
            void* arg3) cil managed aggressiveinlining
        {
            .maxstack 6

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(
            void* this,
            void* vtableSlot,
            uint32 arg0,
            void* arg1,
            void* arg2,
            int32 arg3,
            void* arg4,
            void* arg5,
            void* arg6,
            void* arg7,
            void* arg8,
            void* arg9,
            void* arg10) cil managed aggressiveinlining
        {
            .maxstack 13

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.s     9
            ldarg.s     10
            ldarg.s     11
            ldarg.s     12
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*, int32, void*, void*, void*, void*, void*, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, int32 arg1, int32 arg2, uint32 arg3, uint32 arg4, void* arg5) cil managed aggressiveinlining
        {
            .maxstack 8

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, int32, int32, uint32, uint32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, uint32 arg1, void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, uint32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, uint32 arg1, void* arg2, int32 arg3, void* arg4) cil managed aggressiveinlining
        {
            .maxstack 7

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, uint32, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, uint32 arg1, void* arg2, void* arg3, int32 arg4, void* arg5) cil managed aggressiveinlining
        {
            .maxstack 8

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, uint32, void*, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, uint32 arg1, uint32 arg2, void* arg3, int32 arg4, void* arg5) cil managed aggressiveinlining
        {
            .maxstack 8

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, uint32, uint32, void*, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, int64 arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, int64, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint64 arg0) cil managed aggressiveinlining
        {
            .maxstack 3

            ldarg.0
            ldarg.2
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint64)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint64 arg0, int32 arg1, void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint64, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint64 arg0, int32 arg1, void* arg2, void* arg3) cil managed aggressiveinlining
        {
            .maxstack 6

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint64, int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint64 arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint64, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint64 arg0, void* arg1, void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint64, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, int32 arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, int32)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, void* arg1) cil managed aggressiveinlining
        {
            .maxstack 4

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, void* arg1, void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, int32 arg1, void* arg2) cil managed aggressiveinlining
        {
            .maxstack 5

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, int32, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, uint32 arg0, uint32 arg1, int32 arg2, void* arg3, void* arg4) cil managed aggressiveinlining
        {
            .maxstack 7

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.1
            calli       unmanaged stdcall int32(void*, uint32, uint32, int32, void*, void*)
            ret
        }

        .method public hidebysig static int32 Calli(void* this, void* vtableSlot, void* arg0, void* arg1, void* arg2, void* arg3, int32 arg4, uint32 arg5, void* arg6, void* arg7, void* arg8, void* arg9, int32 arg10, void* arg11) cil managed aggressiveinlining
        {
            .maxstack 15

            ldarg.0
            ldarg.2
            ldarg.3
            ldarg.s     4
            ldarg.s     5
            ldarg.s     6
            ldarg.s     7
            ldarg.s     8
            ldarg.s     9
            ldarg.s     10
            ldarg.s     11
            ldarg.s     12
            ldarg.s     13
            ldarg.1
            calli       unmanaged stdcall int32(void*, void*, void*, void*, void*, int32, uint32, void*, void*, void*, void*, int32, void*)
            ret
        }
    }
}
