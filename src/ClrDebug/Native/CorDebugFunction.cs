using System.Runtime.InteropServices;

using static ClrDebug.UnsafeOps;

namespace ClrDebug.Native
{
    /// <summary>
    /// Represents a managed function or method.
    /// </summary>
    /// <remarks>
    /// This class does not represent a function with generic type parameters. For example, an
    /// instance would represent <see cref="System.Func{TResult}" /> but not <see cref="System.Func{string}" />.
    /// Call <c>ICorDebugILFrame2::EnumerateTypeParameters</c> to get the generic type parameters.
    ///
    /// The relationship between a method's metadata token, <c>mdMethodDef</c>, and a method's
    /// <see cref="CorDebugFunction" /> object is dependent upon whether Edit and Continue is
    /// allowed on the function:
    ///
    /// If Edit and Continue is not allowed on the function, a one-to-one relationship exists
    /// between the <see cref="CorDebugFunction" /> object and the <c>mdMethodDef</c> token. That
    /// is, the function has one <see cref="CorDebugFunction" /> object and one mdMethodDef token.
    ///
    /// If Edit and Continue is allowed on the function, a many-to-one relationship exists between
    /// the <see cref="CorDebugFunction" /> object and the <c>mdMethodDef</c> token. That is, the
    /// function may have many instances of <see cref="CorDebugFunction" />, one for each version
    /// of the function, but only one <c>mdMethodDef</c> token.
    /// </remarks>
    public unsafe class CorDebugFunction : Unknown
    {
        internal CorDebugFunction()
        {
        }

        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Gets the module in which this function is defined.
        /// </summary>
        public int GetModule(out CorDebugModule module) => InvokeGetObject(_this, This[0]->GetModule, out module);

        /// <summary>
        /// Gets an <see cref="CorDebugClass" /> object that represents the class this
        /// function is a member of, or <see langword="null" /> if the function is not a
        /// member of a class.
        /// </summary>
        public int GetClass(out CorDebugClass @class) => InvokeGetObject(_this, This[0]->GetClass, out @class);

        /// <summary>
        /// Gets the metadata token for this function.
        /// </summary>
        public int GetToken(out uint methodDef) => InvokeGet(_this, This[0]->GetToken, out methodDef);

        /// <summary>
        /// Gets the <see cref="CorDebugCode" /> instance that represents the Microsoft
        /// intermediate language (MSIL) code associated with this object, or <see langword="null" />
        /// if the function was not compiled into MSIL.
        /// </summary>
        /// <remarks>
        /// If Edit and Continue has been allowed on this function, the <see cref="GetILCode(out CorDebugCode)" />
        /// method will get the MSIL code corresponding to this function's edited version
        /// of the code in the common language runtime (CLR).
        /// </remarks>
        public int GetILCode(out CorDebugCode code) => InvokeGetObject(_this, This[0]->GetILCode, out code);

        /// <summary>
        /// Gets the native code for the function that is represented by this object, or <see langword="null" />
        /// if this function is Microsoft intermediate language (MSIL) that has not been
        /// just-in-time (JIT) compiled.
        /// </summary>
        /// <remarks>
        /// If the function that is represented by this object has been JIT-compiled more
        /// than once, as in the case of generic types, then this method returns a random
        /// native code object.
        /// </remarks>
        public int GetNativeCode(out CorDebugCode code) => InvokeGetObject(_this, This[0]->GetNativeCode, out code);

        /// <summary>
        /// Creates a breakpoint at the beginning of this function.
        /// </summary>
        public int CreateBreakpoint(out CorDebugFunctionBreakpoint breakpoint)
            => InvokeGetObject(_this, This[0]->CreateBreakpoint, out breakpoint);

        /// <summary>
        /// Gets the metadata token for the local variable signature of the function that
        /// is represented by this object or <c>mdSignatureNil</c> (0x11000000) if the function
        /// has no local variables.
        /// </summary>
        public int GetLocalVarSigToken(out uint mdSig) => InvokeGet(_this, This[0]->GetLocalVarSigToken, out mdSig);

        /// <summary>
        /// Gets the version number of the latest edit made to the function represented
        /// by this object.
        /// </summary>
        /// <remarks>
        /// The version number of the latest edit made to this function may be greater than the
        /// version number of the function itself. Use either the <c>ICorDebugFunction2::GetVersionNumber</c>
        /// method or the <see cref="CorDebugCode.GetVersionNumber(out uint)" /> method to
        /// retrieve the version number of the function.
        /// </remarks>
        public int GetCurrentVersionNumber(out uint nCurrentVersion)
            => InvokeGet(_this, This[0]->GetCurrentVersionNumber, out nCurrentVersion);

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* GetModule;

            public void* GetClass;

            public void* GetToken;

            public void* GetILCode;

            public void* GetNativeCode;

            public void* CreateBreakpoint;

            public void* GetLocalVarSigToken;

            public void* GetCurrentVersionNumber;
        }
    }
}
