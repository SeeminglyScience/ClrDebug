using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;
using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a common language runtime (CLR) module, which is either an executable file
    /// or a dynamic-link library (DLL).
    /// </summary>
    public unsafe class CorDebugModule : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        public bool TryGetMetadataImport(out MetaDataImport metadata)
        {
            if (GetMetaDataInterface(CorGuids.IMetadataImport, out Unknown unknown) != HResult.S_OK)
            {
                unknown?.Dispose();
                metadata = null;
                return false;
            }

            bool result = unknown.TryGetInterface(CorGuids.IMetadataImport, out metadata);
            unknown?.Dispose();
            return result;
        }

        /// <summary>
        /// Gets the containing process of this module.
        /// </summary>
        public int GetProcess(out CorDebugProcess process) => InvokeGetObject(_this, This[0]->GetProcess, out process);

        /// <summary>
        /// Gets the base address of the module.
        /// </summary>
        /// <remarks>
        /// If the module is a native image (that is, if the module was produced by the native
        /// image generator, NGen.exe), its base address will be zero.
        /// </remarks>
        public int GetBaseAddress(out ulong address) => InvokeGet(_this, This[0]->GetBaseAddress, out address);

        /// <summary>
        /// Gets the containing assembly for this module.
        /// </summary>
        public int GetAssembly(out CorDebugAssembly assembly) => InvokeGetObject(_this, This[0]->GetAssembly, out assembly);

        /// <summary>
        /// Gets the file name of the module.
        /// </summary>
        /// <param name="szName">The destination buffer.</param>
        /// <param name="amountWritten">
        /// The amount of characters written to <see paramref="szName" />.
        /// </param>
        /// <remarks>
        /// This method returns <see cref="HResult.S_OK" /> if the module's file name matches
        /// the name on disk. Otherwise, it returns an <see cref="HResult.S_FALSE" /> if the name is
        /// fabricated, such as for a dynamic or in-memory module.
        /// </remarks>
        public int GetName(Span<char> szName, out uint amountWritten)
        {
            fixed (void* pCharsUsed = &amountWritten)
            fixed (void* pSzName = szName)
            {
                return Calli(_this, This[0]->GetName, (uint)szName.Length, pCharsUsed, pSzName);
            }
        }

        /// <summary>
        /// Controls whether the just-in-time (JIT) compiler preserves
        /// debugging information for methods within this module.
        /// </summary>
        /// <param name="bTrackJITInfo">
        /// Indicates whether the JIT compiler should preserve mapping
        /// information between the Microsoft intermediate language (MSIL)
        /// version and the JIT-compiled version of each method in this module.
        /// </param>
        /// <param name="bAllowJitOpts">
        /// Indicates whether the JIT compiler should generate code with
        /// certain JIT-specific optimizations for debugging.
        /// </param>
        /// <remarks>
        /// JIT debugging is enabled by default for all modules that are
        /// loaded when the debugger is active. Programmatically enabling
        /// or disabling the settings overrides global settings.
        /// </remarks>
        public int EnableJITDebugging(bool bTrackJITInfo, bool bAllowJitOpts)
        {
            return Calli(_this, This[0]->EnableJITDebugging, bTrackJITInfo.ToNativeInt(), bAllowJitOpts.ToNativeInt());
        }

        /// <summary>
        /// Controls whether the <c>ICorDebugManagedCallback::LoadClass</c> and
        /// <c>ICorDebugManagedCallback::UnloadClass</c> callbacks are called
        /// for this module.
        /// </summary>
        /// <param name="bClassLoadCallbacks">
        /// Indicates whether the common language runtime (CLR) should call the
        /// <c>ICorDebugManagedCallback::LoadClass</c> and <c>ICorDebugManagedCallback::UnloadClass</c>
        /// methods when their associated events occur.
        ///
        /// The default value is <see langword="false" /> for non-dynamic modules.
        /// The value is always <see langword="true" /> for dynamic modules and
        /// cannot be changed.
        /// </param>
        /// <remarks>
        /// The <c>ICorDebugManagedCallback::LoadClass</c> and <c>ICorDebugManagedCallback::UnloadClass</c>
        /// callbacks are always enabled for dynamic modules and cannot be disabled.
        /// </remarks>
        public int EnableClassLoadCallbacks(bool bClassLoadCallbacks)
        {
            return Calli(_this, This[0]->EnableClassLoadCallbacks, bClassLoadCallbacks.ToNativeInt());
        }

        /// <summary>
        /// Gets the function that is specified by the metadata token.
        /// </summary>
        /// <param name="methodDef">
        /// A <c>mdMethodDef</c> metadata token that references the
        /// function's metadata.
        /// </param>
        /// <param name="function">
        /// The <see cref="CorDebugFunction" /> that represents the function.
        /// </param>
        /// <remarks>
        /// This method returns a <c>CORDBG_E_FUNCTION_NOT_IL</c>(0x8013130A) HRESULT
        /// if the value passed in <c>methodDef</c> does not refer to a Microsoft
        /// intermediate language (MSIL) method.
        /// </remarks>
        public int GetFunctionFromToken(uint methodDef, out CorDebugFunction function)
        {
            void** pFunction = default;
            int result = Calli(_this, This[0]->GetFunctionFromToken, methodDef, &pFunction);
            ComFactory.Create(pFunction, out function);
            return result;
        }

        [Obsolete("This method has not been implemented in the current version of the .NET Framework.")]
        public int GetFunctionFromRVA(ulong rva, out CorDebugFunction function)
        {
            void** pFunction = default;
            int result = Calli(_this, This[0]->GetFunctionFromRVA, rva, &pFunction);
            ComFactory.Create(pFunction, out function);
            return result;
        }

        /// <summary>
        /// Gets the class specified by the metadata token.
        /// </summary>
        public int GetClassFromToken(uint methodDef, out CorDebugClass @class)
        {
            void** pClass = default;
            int result = Calli(_this, This[0]->GetClassFromToken, methodDef, &pClass);
            ComFactory.Create(pClass, out @class);
            return result;
        }

        [Obsolete("This method has not been implemented in the current version of the .NET Framework.")]
        public int CreateBreakpoint(out CorDebugModuleBreakpoint breakpoint)
            => InvokeGetObject(_this, This[0]->CreateBreakpoint, out breakpoint);

        [Obsolete("Deprecated.")]
        public int GetEditAndContinueSnapshot(out CorDebugEditAndContinueSnapshot editAndContinueSnapshot)
            => InvokeGetObject(_this, This[0]->GetEditAndContinueSnapshot, out editAndContinueSnapshot);

        /// <summary>
        /// Gets a metadata interface object that can be used to examine the
        /// metadata for the module.
        /// </summary>
        /// <param name="riid">
        /// The reference ID that specifies the metadata interface.
        /// </param>
        /// <param name="obj">
        /// An object that is one of the metadata interfaces
        /// (see https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/metadata/metadata-interfaces).
        /// </param>
        /// <remarks>
        /// The debugger can use this method to make a copy of the original
        /// metadata for a module, which it must do in order to edit that
        /// module. The debugger calls this to get an <c>IMetaDataEmit</c>
        /// interface object for the module, then calls <c>IMetaDataEmit::SaveToMemory</c>
        /// to save a copy of the module's metadata to memory.
        /// </remarks>
        public int GetMetaDataInterface(Guid riid, out Unknown obj)
        {
            void** pObj = default;
            int result = Calli(_this, This[0]->GetMetaDataInterface, &riid, &pObj);
            ComFactory.Create(pObj, out obj);
            return result;
        }

        /// <summary>
        /// Gets the token for the table entry for this module.
        /// </summary>
        /// <remarks>
        /// The token can be passed to the <c>IMetaDataImport</c>,
        /// <c>IMetaDataImport2</c>, and <c>IMetaDataAssemblyImport</c>
        /// metadata import interfaces.
        /// </remarks>
        public int GetToken(out uint token) => InvokeGet(_this, This[0]->GetToken, out token);

        /// <summary>
        /// Gets a value that indicates whether this module is dynamic.
        /// </summary>
        /// <remarks>
        /// A dynamic module can add new classes and delete existing
        /// classes even after the module has been loaded. The
        /// <c>ICorDebugManagedCallback::LoadClass</c> and
        /// <c>ICorDebugManagedCallback::UnloadClass</c> callbacks
        /// inform the debugger when a class has been added or deleted.
        /// </remarks>
        public int IsDynamic(out bool dynamic) => InvokeGet(_this, This[0]->IsDynamic, out dynamic);

        /// <summary>
        /// Gets the value of the specified global variable.
        /// </summary>
        /// <param name="fieldDef">
        /// An <c>mdFieldDef</c> token that references the metadata
        /// describing the global variable.
        /// </param>
        /// <param name="value">The value of the specified global variable.</param>
        public int GetGlobalVariableValue(uint fieldDef, out CorDebugValue value)
        {
            void** pValue = default;
            int result = Calli(_this, This[0]->GetGlobalVariableValue, fieldDef, &pValue);
            ComFactory.Create(pValue, out value);
            return result;
        }

        /// <summary>
        /// Gets the size, in bytes, of the module.
        /// </summary>
        /// <remarks>
        /// If the module was produced
        /// from a native imagine generator (e.g. NGen.exe or crossgen), then
        /// the size of the module will be zero.
        /// </remarks>
        public int GetSize(out uint cBytes) => InvokeGet(_this, This[0]->GetSize, out cBytes);

        /// <summary>
        /// Gets a value that indicates whether this module exists only in memory.
        /// </summary>
        /// <remarks>
        /// The common language runtime (CLR) supports the loading of modules from
        /// raw streams of bytes. Such modules are called in-memory modules and do
        /// not exist on disk.
        /// </remarks>
        public int IsInMemory(out bool inMemory) => InvokeGet(_this, This[0]->IsInMemory, out inMemory);

        [StructLayout(LayoutKind.Sequential)]
        private new unsafe struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetProcess;

            public void* GetBaseAddress;

            public void* GetAssembly;

            public void* GetName;

            public void* EnableJITDebugging;

            public void* EnableClassLoadCallbacks;

            public void* GetFunctionFromToken;

            public void* GetFunctionFromRVA;

            public void* GetClassFromToken;

            public void* CreateBreakpoint;

            public void* GetEditAndContinueSnapshot;

            public void* GetMetaDataInterface;

            public void* GetToken;

            public void* IsDynamic;

            public void* GetGlobalVariableValue;

            public void* GetSize;

            public void* IsInMemory;
        }
    }
}
