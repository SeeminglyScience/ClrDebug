namespace ClrDebug.Native
{
    /// <summary>
    /// Provides constants of common HRESULT codes.
    /// </summary>
    public static class HResult
    {
        /// <summary>Operation successful.</summary>
        public const int S_OK = 0;

        public const int S_FALSE = 1;

        /// <summary>Not implemented.</summary>
        public const int E_NOTIMPL = unchecked((int)0x80004001);

        /// <summary>No such interface supported.</summary>
        public const int E_NOINTERFACE = unchecked((int)0x80004002);

        /// <summary>Pointer that is not valid.</summary>
        public const int E_POINTER = unchecked((int)0x80004003);

        /// <summary>Operation aborted.</summary>
        public const int E_ABORT = unchecked((int)0x80004004);

        /// <summary>Unspecified failure.</summary>
        public const int E_FAIL = unchecked((int)0x80004005);

        /// <summary>Unexpected failure.</summary>
        public const int E_UNEXPECTED = unchecked((int)0x8000FFFF);

        /// <summary>General access denied error.</summary>
        public const int E_ACCESSDENIED = unchecked((int)0x80070005);

        /// <summary>Handle that is not valid.</summary>
        public const int E_HANDLE = unchecked((int)0x80070006);

        /// <summary>Failed to allocate necessary memory.</summary>
        public const int E_OUTOFMEMORY = unchecked((int)0x8007000E);

        /// <summary>One or more arguments are not valid.</summary>
        public const int E_INVALIDARG = unchecked((int)0x80070057);

        /// <summary>STATUS: Data value was truncated.</summary>
        public const int CLDB_S_TRUNCATION = 0x00131106;

        /// <summary>Attempt to define an object that already exists in valid scenerios.</summary>
        public const int META_S_DUPLICATE = 0x00131197;

        /// <summary>Attempt to SetIP not at a sequence point sequence point.</summary>
        public const int CORDBG_S_BAD_START_SEQUENCE_POINT = 0x0013130b;

        /// <summary>Attempt to SetIP when not going to a sequence point. If both this and CORDBG_E_BAD_START_SEQUENCE_POINT are true, only CORDBG_E_BAD_START_SEQUENCE_POINT will be reported.</summary>
        public const int CORDBG_S_BAD_END_SEQUENCE_POINT = 0x0013130c;

        /// <summary>Some Func evals will lack a return value,</summary>
        public const int CORDBG_S_FUNC_EVAL_HAS_NO_RESULT = 0x00131316;

        /// <summary>The Debugging API doesn't support dereferencing void pointers.</summary>
        public const int CORDBG_S_VALUE_POINTS_TO_VOID = 0x00131317;

        /// <summary>The func eval completed, but was aborted.</summary>
        public const int CORDBG_S_FUNC_EVAL_ABORTED = 0x00131319;

        /// <summary>The stack walk has reached the end of the stack.  There are no more frames to walk.</summary>
        public const int CORDBG_S_AT_END_OF_STACK = 0x00131324;

        /// <summary>Not all bits specified were successfully applied</summary>
        public const int CORDBG_S_NOT_ALL_BITS_SET = 0x00131c13;

        /// <summary>cvtres.exe not found.</summary>
        public const int CEE_E_CVTRES_NOT_FOUND = unchecked((int)0x80131001);

        /// <summary>Type has been unloaded.</summary>
        public const int COR_E_TYPEUNLOADED = unchecked((int)0x80131013);

        /// <summary>Attempted to access an unloaded appdomain.</summary>
        public const int COR_E_APPDOMAINUNLOADED = unchecked((int)0x80131014);

        /// <summary>Error while unloading appdomain.</summary>
        public const int COR_E_CANNOTUNLOADAPPDOMAIN = unchecked((int)0x80131015);

        /// <summary>Assembly is still being loaded.</summary>
        public const int MSEE_E_ASSEMBLYLOADINPROGRESS = unchecked((int)0x80131016);

        /// <summary>The module was expected to contain an assembly manifest.</summary>
        public const int COR_E_ASSEMBLYEXPECTED = unchecked((int)0x80131018);

        /// <summary>Attempt to load an unverifiable executable with fixups (IAT with more than 2 sections or a TLS section.)</summary>
        public const int COR_E_FIXUPSINEXE = unchecked((int)0x80131019);

        /// <summary>This assembly is built by a runtime newer than the currently loaded runtime and cannot be loaded.</summary>
        public const int COR_E_NEWER_RUNTIME = unchecked((int)0x8013101b);

        /// <summary>The module cannot be loaded because only single file assemblies are supported.</summary>
        public const int COR_E_MULTIMODULEASSEMBLIESDIALLOWED = unchecked((int)0x8013101e);

        /// <summary>Host detected a deadlock on a blocking operation.</summary>
        public const int HOST_E_DEADLOCK = unchecked((int)0x80131020);

        /// <summary>Invalid operation.</summary>
        public const int HOST_E_INVALIDOPERATION = unchecked((int)0x80131022);

        /// <summary>CLR has been disabled due to unrecoverable error.</summary>
        public const int HOST_E_CLRNOTAVAILABLE = unchecked((int)0x80131023);

        /// <summary>Process exited due to ThreadAbort escalation.</summary>
        public const int HOST_E_EXITPROCESS_THREADABORT = unchecked((int)0x80131027);

        /// <summary>Process exited due to AD Unload escalation.</summary>
        public const int HOST_E_EXITPROCESS_ADUNLOAD = unchecked((int)0x80131028);

        /// <summary>Process exited due to Timeout escalation.</summary>
        public const int HOST_E_EXITPROCESS_TIMEOUT = unchecked((int)0x80131029);

        /// <summary>Process exited due to OutOfMemory escalation.</summary>
        public const int HOST_E_EXITPROCESS_OUTOFMEMORY = unchecked((int)0x8013102a);

        /// <summary>The check of the module's hash failed.</summary>
        public const int COR_E_MODULE_HASH_CHECK_FAILED = unchecked((int)0x80131039);

        /// <summary>The located assembly's manifest definition does not match the assembly reference.</summary>
        public const int FUSION_E_REF_DEF_MISMATCH = unchecked((int)0x80131040);

        /// <summary>The private assembly was located outside the appbase directory.</summary>
        public const int FUSION_E_INVALID_PRIVATE_ASM_LOCATION = unchecked((int)0x80131041);

        /// <summary>A module specified in the manifest was not found.</summary>
        public const int FUSION_E_ASM_MODULE_MISSING = unchecked((int)0x80131042);

        /// <summary>A strongly-named assembly is required.</summary>
        public const int FUSION_E_PRIVATE_ASM_DISALLOWED = unchecked((int)0x80131044);

        /// <summary>Strong name signature could not be verified.  The assembly may have been tampered with, or it was delay signed but not fully signed with the correct private key.</summary>
        public const int FUSION_E_SIGNATURE_CHECK_FAILED = unchecked((int)0x80131045);

        /// <summary>The given assembly name or codebase was invalid.</summary>
        public const int FUSION_E_INVALID_NAME = unchecked((int)0x80131047);

        /// <summary>HTTP download of assemblies has been disabled for this appdomain.</summary>
        public const int FUSION_E_CODE_DOWNLOAD_DISABLED = unchecked((int)0x80131048);

        /// <summary>Assembly in host store has a different signature than assembly in GAC.</summary>
        public const int FUSION_E_HOST_GAC_ASM_MISMATCH = unchecked((int)0x80131050);

        /// <summary>LoadFrom(), LoadFile(), Load(byte[]) and LoadModule() have been disabled by the host.</summary>
        public const int FUSION_E_LOADFROM_BLOCKED = unchecked((int)0x80131051);

        /// <summary>Failed to add file to AppDomain cache.</summary>
        public const int FUSION_E_CACHEFILE_FAILED = unchecked((int)0x80131052);

        /// <summary>The requested assembly version conflicts with what is already bound in the app domain or specified in the manifest.</summary>
        public const int FUSION_E_APP_DOMAIN_LOCKED = unchecked((int)0x80131053);

        /// <summary>The requested assembly name was neither found in the GAC nor in the manifest or the manifest's specified location is wrong.</summary>
        public const int FUSION_E_CONFIGURATION_ERROR = unchecked((int)0x80131054);

        /// <summary>Unexpected error while parsing the specified manifest.</summary>
        public const int FUSION_E_MANIFEST_PARSE_ERROR = unchecked((int)0x80131055);

        /// <summary>Reference assemblies should not be loaded for execution.  They can only be loaded in the Reflection-only loader context.</summary>
        public const int COR_E_LOADING_REFERENCE_ASSEMBLY = unchecked((int)0x80131058);

        /// <summary>The native image could not be loaded, because it was generated for use by a different version of the runtime.</summary>
        public const int COR_E_NI_AND_RUNTIME_VERSION_MISMATCH = unchecked((int)0x80131059);

        /// <summary>Contract Windows Runtime assemblies cannot be loaded for execution.  Make sure your application only contains non-contract Windows Runtime assemblies.</summary>
        public const int COR_E_LOADING_WINMD_REFERENCE_ASSEMBLY = unchecked((int)0x80131069);

        /// <summary>Ambiguous implementation found.</summary>
        public const int COR_E_AMBIGUOUSIMPLEMENTATION = unchecked((int)0x8013106A);

        /// <summary>Error occurred during a read.</summary>
        public const int CLDB_E_FILE_BADREAD = unchecked((int)0x80131100);

        /// <summary>Error occurred during a write.</summary>
        public const int CLDB_E_FILE_BADWRITE = unchecked((int)0x80131101);

        /// <summary>Old version error.</summary>
        public const int CLDB_E_FILE_OLDVER = unchecked((int)0x80131107);

        /// <summary>Create of shared memory failed.  A memory mapping of the same name already exists.</summary>
        public const int CLDB_E_SMDUPLICATE = unchecked((int)0x8013110a);

        /// <summary>No .CLB data in the memory or stream.</summary>
        public const int CLDB_E_NO_DATA = unchecked((int)0x8013110b);

        /// <summary>Importing scope is not compatible with the emitting scope.</summary>
        public const int CLDB_E_INCOMPATIBLE = unchecked((int)0x8013110d);

        /// <summary>File is corrupt.</summary>
        public const int CLDB_E_FILE_CORRUPT = unchecked((int)0x8013110e);

        /// <summary>Cannot open a incrementally build scope for full update.</summary>
        public const int CLDB_E_BADUPDATEMODE = unchecked((int)0x80131110);

        /// <summary>Index not found.</summary>
        public const int CLDB_E_INDEX_NOTFOUND = unchecked((int)0x80131124);

        /// <summary>Record not found on lookup.</summary>
        public const int CLDB_E_RECORD_NOTFOUND = unchecked((int)0x80131130);

        /// <summary>Record is emitted out of order.</summary>
        public const int CLDB_E_RECORD_OUTOFORDER = unchecked((int)0x80131135);

        /// <summary>A blob or string was too big.</summary>
        public const int CLDB_E_TOO_BIG = unchecked((int)0x80131154);

        /// <summary>A token of the wrong type passed to a metadata function.</summary>
        public const int META_E_INVALID_TOKEN_TYPE = unchecked((int)0x8013115f);

        /// <summary>Typelib export: Type library is not registered.</summary>
        public const int TLBX_E_LIBNOTREGISTERED = unchecked((int)0x80131165);

        /// <summary>Merge: Inconsistency in meta data import scope.</summary>
        public const int META_E_BADMETADATA = unchecked((int)0x8013118a);

        /// <summary>Bad binary signature.</summary>
        public const int META_E_BAD_SIGNATURE = unchecked((int)0x80131192);

        /// <summary>Bad input parameters.</summary>
        public const int META_E_BAD_INPUT_PARAMETER = unchecked((int)0x80131193);

        /// <summary>Cannot resolve typeref.</summary>
        public const int META_E_CANNOTRESOLVETYPEREF = unchecked((int)0x80131196);

        /// <summary>No logical space left to create more user strings.</summary>
        public const int META_E_STRINGSPACE_FULL = unchecked((int)0x80131198);

        /// <summary>Unmark all has been called already.</summary>
        public const int META_E_HAS_UNMARKALL = unchecked((int)0x8013119a);

        /// <summary>Must call UnmarkAll first before marking.</summary>
        public const int META_E_MUST_CALL_UNMARKALL = unchecked((int)0x8013119b);

        /// <summary>Known custom attribute on invalid target.</summary>
        public const int META_E_CA_INVALID_TARGET = unchecked((int)0x801311c0);

        /// <summary>Known custom attribute had invalid value.</summary>
        public const int META_E_CA_INVALID_VALUE = unchecked((int)0x801311c1);

        /// <summary>Known custom attribute blob has bad format.</summary>
        public const int META_E_CA_INVALID_BLOB = unchecked((int)0x801311c2);

        /// <summary>Known custom attribute blob has repeated named argument.</summary>
        public const int META_E_CA_REPEATED_ARG = unchecked((int)0x801311c3);

        /// <summary>Known custom attribute named argument not recognized.</summary>
        public const int META_E_CA_UNKNOWN_ARGUMENT = unchecked((int)0x801311c4);

        /// <summary>Known attribute parser found unexpected type.</summary>
        public const int META_E_CA_UNEXPECTED_TYPE = unchecked((int)0x801311c7);

        /// <summary>Known attribute parser only handles fields, not properties.</summary>
        public const int META_E_CA_INVALID_ARGTYPE = unchecked((int)0x801311c8);

        /// <summary>Known attribute parser found an argument that is invalid for the object it is applied to.</summary>
        public const int META_E_CA_INVALID_ARG_FOR_TYPE = unchecked((int)0x801311c9);

        /// <summary>The format of the UUID was invalid.</summary>
        public const int META_E_CA_INVALID_UUID = unchecked((int)0x801311ca);

        /// <summary>The MarshalAs attribute has fields set that are not valid for the specified unmanaged type.</summary>
        public const int META_E_CA_INVALID_MARSHALAS_FIELDS = unchecked((int)0x801311cb);

        /// <summary>The specified unmanaged type is only valid on fields.</summary>
        public const int META_E_CA_NT_FIELDONLY = unchecked((int)0x801311cc);

        /// <summary>The parameter index cannot be negative.</summary>
        public const int META_E_CA_NEGATIVE_PARAMINDEX = unchecked((int)0x801311cd);

        /// <summary>The constant size cannot be negative.</summary>
        public const int META_E_CA_NEGATIVE_CONSTSIZE = unchecked((int)0x801311cf);

        /// <summary>A fixed string requires a size.</summary>
        public const int META_E_CA_FIXEDSTR_SIZE_REQUIRED = unchecked((int)0x801311d0);

        /// <summary>A custom marshaler requires the custom marshaler type.</summary>
        public const int META_E_CA_CUSTMARSH_TYPE_REQUIRED = unchecked((int)0x801311d1);

        /// <summary>SaveDelta was called without being in EnC mode.</summary>
        public const int META_E_NOT_IN_ENC_MODE = unchecked((int)0x801311d4);

        /// <summary>InternalsVisibleTo can't have a version, culture, or processor architecture.</summary>
        public const int META_E_CA_BAD_FRIENDS_ARGS = unchecked((int)0x801311e5);

        /// <summary>Strong-name signed assemblies can only grant friend access to strong name-signed assemblies</summary>
        public const int META_E_CA_FRIENDS_SN_REQUIRED = unchecked((int)0x801311e6);

        /// <summary>Rid is out of range.</summary>
        public const int VLDTR_E_RID_OUTOFRANGE = unchecked((int)0x80131203);

        /// <summary>String offset is invalid.</summary>
        public const int VLDTR_E_STRING_INVALID = unchecked((int)0x80131206);

        /// <summary>GUID offset is invalid.</summary>
        public const int VLDTR_E_GUID_INVALID = unchecked((int)0x80131207);

        /// <summary>Blob offset if invalid.</summary>
        public const int VLDTR_E_BLOB_INVALID = unchecked((int)0x80131208);

        /// <summary>MemberRef has invalid calling convention.</summary>
        public const int VLDTR_E_MR_BADCALLINGCONV = unchecked((int)0x80131224);

        /// <summary>Signature specified is zero-sized.</summary>
        public const int VLDTR_E_SIGNULL = unchecked((int)0x80131237);

        /// <summary>Method signature has invalid calling convention.</summary>
        public const int VLDTR_E_MD_BADCALLINGCONV = unchecked((int)0x80131239);

        /// <summary>Method is marked static but has HASTHIS/EXPLICITTHIS set on the calling convention.</summary>
        public const int VLDTR_E_MD_THISSTATIC = unchecked((int)0x8013123a);

        /// <summary>Method is not marked static but is not HASTHIS or EXPLICITTHIS.</summary>
        public const int VLDTR_E_MD_NOTTHISNOTSTATIC = unchecked((int)0x8013123b);

        /// <summary>Method signature is missing the argument count.</summary>
        public const int VLDTR_E_MD_NOARGCNT = unchecked((int)0x8013123c);

        /// <summary>Signature missing element type.</summary>
        public const int VLDTR_E_SIG_MISSELTYPE = unchecked((int)0x8013123d);

        /// <summary>Signature missing token.</summary>
        public const int VLDTR_E_SIG_MISSTKN = unchecked((int)0x8013123e);

        /// <summary>Signature has bad token.</summary>
        public const int VLDTR_E_SIG_TKNBAD = unchecked((int)0x8013123f);

        /// <summary>Signature is missing function pointer.</summary>
        public const int VLDTR_E_SIG_MISSFPTR = unchecked((int)0x80131240);

        /// <summary>Signature has function pointer missing argument count.</summary>
        public const int VLDTR_E_SIG_MISSFPTRARGCNT = unchecked((int)0x80131241);

        /// <summary>Signature is missing rank specification.</summary>
        public const int VLDTR_E_SIG_MISSRANK = unchecked((int)0x80131242);

        /// <summary>Signature is missing count of sized dimensions.</summary>
        public const int VLDTR_E_SIG_MISSNSIZE = unchecked((int)0x80131243);

        /// <summary>Signature is missing size of dimension.</summary>
        public const int VLDTR_E_SIG_MISSSIZE = unchecked((int)0x80131244);

        /// <summary>Signature is missing count of lower bounds.</summary>
        public const int VLDTR_E_SIG_MISSNLBND = unchecked((int)0x80131245);

        /// <summary>Signature is missing a lower bound.</summary>
        public const int VLDTR_E_SIG_MISSLBND = unchecked((int)0x80131246);

        /// <summary>Signature has bad element type.</summary>
        public const int VLDTR_E_SIG_BADELTYPE = unchecked((int)0x80131247);

        /// <summary>TypeDef not nested has encloser.</summary>
        public const int VLDTR_E_TD_ENCLNOTNESTED = unchecked((int)0x80131256);

        /// <summary>Field or method is PInvoke but is not marked Static.</summary>
        public const int VLDTR_E_FMD_PINVOKENOTSTATIC = unchecked((int)0x80131277);

        /// <summary>E_T_SENTINEL in MethodDef signature.</summary>
        public const int VLDTR_E_SIG_SENTINMETHODDEF = unchecked((int)0x801312df);

        /// <summary>E_T_SENTINEL <=> VARARG.</summary>
        public const int VLDTR_E_SIG_SENTMUSTVARARG = unchecked((int)0x801312e0);

        /// <summary>Multiple E_T_SENTINELs.</summary>
        public const int VLDTR_E_SIG_MULTSENTINELS = unchecked((int)0x801312e1);

        /// <summary>Signature missing argument.</summary>
        public const int VLDTR_E_SIG_MISSARG = unchecked((int)0x801312e3);

        /// <summary>Field of ByRef type.</summary>
        public const int VLDTR_E_SIG_BYREFINFIELD = unchecked((int)0x801312e4);

        /// <summary>Unrecoverable API error.</summary>
        public const int CORDBG_E_UNRECOVERABLE_ERROR = unchecked((int)0x80131300);

        /// <summary>Process was terminated.</summary>
        public const int CORDBG_E_PROCESS_TERMINATED = unchecked((int)0x80131301);

        /// <summary>Process not synchronized.</summary>
        public const int CORDBG_E_PROCESS_NOT_SYNCHRONIZED = unchecked((int)0x80131302);

        /// <summary>A class is not loaded.</summary>
        public const int CORDBG_E_CLASS_NOT_LOADED = unchecked((int)0x80131303);

        /// <summary>An IL variable is not available at the current native IP.</summary>
        public const int CORDBG_E_IL_VAR_NOT_AVAILABLE = unchecked((int)0x80131304);

        /// <summary>A reference value was found to be bad during dereferencing.</summary>
        public const int CORDBG_E_BAD_REFERENCE_VALUE = unchecked((int)0x80131305);

        /// <summary>A field in a class is not available, because the runtime optimized it away.</summary>
        public const int CORDBG_E_FIELD_NOT_AVAILABLE = unchecked((int)0x80131306);

        /// <summary>'Native-frame-only' operation on non-native frame.</summary>
        public const int CORDBG_E_NON_NATIVE_FRAME = unchecked((int)0x80131307);

        /// <summary>The code is currently unavailable.</summary>
        public const int CORDBG_E_CODE_NOT_AVAILABLE = unchecked((int)0x80131309);

        /// <summary>Attempt to get a ICorDebugFunction for a function that is not IL.</summary>
        public const int CORDBG_E_FUNCTION_NOT_IL = unchecked((int)0x8013130a);

        /// <summary>SetIP is not possible because SetIP would move EIP from outside of an exception handling finally clause to a point inside of one.</summary>
        public const int CORDBG_E_CANT_SET_IP_INTO_FINALLY = unchecked((int)0x8013130e);

        /// <summary>SetIP is not possible because it would move EIP from within an exception handling finally clause to a point outside of one.</summary>
        public const int CORDBG_E_CANT_SET_IP_OUT_OF_FINALLY = unchecked((int)0x8013130f);

        /// <summary>SetIP is not possible, because SetIP would move EIP from outside of an exception handling catch clause to a point inside of one.</summary>
        public const int CORDBG_E_CANT_SET_IP_INTO_CATCH = unchecked((int)0x80131310);

        /// <summary>SetIP cannot be done on any frame except the leaf frame.</summary>
        public const int CORDBG_E_SET_IP_NOT_ALLOWED_ON_NONLEAF_FRAME = unchecked((int)0x80131311);

        /// <summary>SetIP is not allowed.</summary>
        public const int CORDBG_E_SET_IP_IMPOSSIBLE = unchecked((int)0x80131312);

        /// <summary>Func eval cannot work. Bad starting point.</summary>
        public const int CORDBG_E_FUNC_EVAL_BAD_START_POINT = unchecked((int)0x80131313);

        /// <summary>This object value is no longer valid.</summary>
        public const int CORDBG_E_INVALID_OBJECT = unchecked((int)0x80131314);

        /// <summary>CordbEval::GetResult called before func eval has finished.</summary>
        public const int CORDBG_E_FUNC_EVAL_NOT_COMPLETE = unchecked((int)0x80131315);

        /// <summary>A static variable is not available because it has not been initialized yet.</summary>
        public const int CORDBG_E_STATIC_VAR_NOT_AVAILABLE = unchecked((int)0x8013131a);

        /// <summary>SetIP cannot leave or enter a filter.</summary>
        public const int CORDBG_E_CANT_SETIP_INTO_OR_OUT_OF_FILTER = unchecked((int)0x8013131c);

        /// <summary>JIT settings for ZAP modules cannot be changed.</summary>
        public const int CORDBG_E_CANT_CHANGE_JIT_SETTING_FOR_ZAP_MODULE = unchecked((int)0x8013131d);

        /// <summary>SetIP is not possible because it would move EIP from within a finally clause to a point outside of one on this platforms.</summary>
        public const int CORDBG_E_CANT_SET_IP_OUT_OF_FINALLY_ON_WIN64 = unchecked((int)0x8013131e);

        /// <summary>SetIP is not possible because it would move EIP from within a catch clause to a point outside of one on this platforms.</summary>
        public const int CORDBG_E_CANT_SET_IP_OUT_OF_CATCH_ON_WIN64 = unchecked((int)0x8013131f);

        /// <summary>Cannot use JMC on this code (likely wrong JIT settings).</summary>
        public const int CORDBG_E_CANT_SET_TO_JMC = unchecked((int)0x80131323);

        /// <summary>Internal frame markers have no associated context.</summary>
        public const int CORDBG_E_NO_CONTEXT_FOR_INTERNAL_FRAME = unchecked((int)0x80131325);

        /// <summary>The current frame is not a child frame.</summary>
        public const int CORDBG_E_NOT_CHILD_FRAME = unchecked((int)0x80131326);

        /// <summary>The provided CONTEXT does not match the specified thread.</summary>
        public const int CORDBG_E_NON_MATCHING_CONTEXT = unchecked((int)0x80131327);

        /// <summary>The stackwalker is now past the end of stack.  No information is available.</summary>
        public const int CORDBG_E_PAST_END_OF_STACK = unchecked((int)0x80131328);

        /// <summary>The state of the thread is invalid.</summary>
        public const int CORDBG_E_BAD_THREAD_STATE = unchecked((int)0x8013132d);

        /// <summary>This process has already been attached.</summary>
        public const int CORDBG_E_DEBUGGER_ALREADY_ATTACHED = unchecked((int)0x8013132e);

        /// <summary>Returned from a call to Continue that was not matched with a stopping event.</summary>
        public const int CORDBG_E_SUPERFLOUS_CONTINUE = unchecked((int)0x8013132f);

        /// <summary>Cannot perfrom SetValue on non-leaf frames.</summary>
        public const int CORDBG_E_SET_VALUE_NOT_ALLOWED_ON_NONLEAF_FRAME = unchecked((int)0x80131330);

        /// <summary>Tried to do Edit and Continue on a module that was not started in Edit and Continue mode.</summary>
        public const int CORDBG_E_ENC_MODULE_NOT_ENC_ENABLED = unchecked((int)0x80131332);

        /// <summary>SetIP cannot be done on any exception.</summary>
        public const int CORDBG_E_SET_IP_NOT_ALLOWED_ON_EXCEPTION = unchecked((int)0x80131333);

        /// <summary>The 'variable' does not exist because it is a literal optimized away by the compiler.</summary>
        public const int CORDBG_E_VARIABLE_IS_ACTUALLY_LITERAL = unchecked((int)0x80131334);

        /// <summary>Process has been detached.</summary>
        public const int CORDBG_E_PROCESS_DETACHED = unchecked((int)0x80131335);

        /// <summary>Adding a field to a value or layout class is prohibited.</summary>
        public const int CORDBG_E_ENC_CANT_ADD_FIELD_TO_VALUE_OR_LAYOUT_CLASS = unchecked((int)0x80131338);

        /// <summary>GetStaticFieldValue called on a non-static field.</summary>
        public const int CORDBG_E_FIELD_NOT_STATIC = unchecked((int)0x8013133b);

        /// <summary>Returned if someone tries to call GetStaticFieldValue on a non-instance field.</summary>
        public const int CORDBG_E_FIELD_NOT_INSTANCE = unchecked((int)0x8013133c);

        /// <summary>The JIT is unable to update the method.</summary>
        public const int CORDBG_E_ENC_JIT_CANT_UPDATE = unchecked((int)0x8013133f);

        /// <summary>Internal Runtime Error while doing Edit-and-Continue.</summary>
        public const int CORDBG_E_ENC_INTERNAL_ERROR = unchecked((int)0x80131341);

        /// <summary>The field was added via Edit and Continue after the class was loaded.</summary>
        public const int CORDBG_E_ENC_HANGING_FIELD = unchecked((int)0x80131342);

        /// <summary>Module not loaded.</summary>
        public const int CORDBG_E_MODULE_NOT_LOADED = unchecked((int)0x80131343);

        /// <summary>Cannot set a breakpoint here.</summary>
        public const int CORDBG_E_UNABLE_TO_SET_BREAKPOINT = unchecked((int)0x80131345);

        /// <summary>Debugging is not possible due to an incompatibility within the CLR implementation.</summary>
        public const int CORDBG_E_DEBUGGING_NOT_POSSIBLE = unchecked((int)0x80131346);

        /// <summary>A kernel debugger is enabled on the system.  User-mode debugging will trap to the kernel debugger.</summary>
        public const int CORDBG_E_KERNEL_DEBUGGER_ENABLED = unchecked((int)0x80131347);

        /// <summary>A kernel debugger is present on the system.  User-mode debugging will trap to the kernel debugger.</summary>
        public const int CORDBG_E_KERNEL_DEBUGGER_PRESENT = unchecked((int)0x80131348);

        /// <summary>The debugger's protocol is incompatible with the debuggee.</summary>
        public const int CORDBG_E_INCOMPATIBLE_PROTOCOL = unchecked((int)0x8013134b);

        /// <summary>The debugger can only handle a finite number of debuggees.</summary>
        public const int CORDBG_E_TOO_MANY_PROCESSES = unchecked((int)0x8013134c);

        /// <summary>Interop debugging is not supported.</summary>
        public const int CORDBG_E_INTEROP_NOT_SUPPORTED = unchecked((int)0x8013134d);

        /// <summary>Cannot call RemapFunction until have received RemapBreakpoint.</summary>
        public const int CORDBG_E_NO_REMAP_BREAKPIONT = unchecked((int)0x8013134e);

        /// <summary>Object is in a zombie state.</summary>
        public const int CORDBG_E_OBJECT_NEUTERED = unchecked((int)0x8013134f);

        /// <summary>Function not yet compiled.</summary>
        public const int CORPROF_E_FUNCTION_NOT_COMPILED = unchecked((int)0x80131350);

        /// <summary>The ID is not fully loaded/defined yet.</summary>
        public const int CORPROF_E_DATAINCOMPLETE = unchecked((int)0x80131351);

        /// <summary>The Method has no associated IL.</summary>
        public const int CORPROF_E_FUNCTION_NOT_IL = unchecked((int)0x80131354);

        /// <summary>The thread has never run managed code before.</summary>
        public const int CORPROF_E_NOT_MANAGED_THREAD = unchecked((int)0x80131355);

        /// <summary>The function may only be called during profiler initialization.</summary>
        public const int CORPROF_E_CALL_ONLY_FROM_INIT = unchecked((int)0x80131356);

        /// <summary>Requested information is not yet available.</summary>
        public const int CORPROF_E_NOT_YET_AVAILABLE = unchecked((int)0x8013135b);

        /// <summary>The given type is a generic and cannot be used with this method.</summary>
        public const int CORPROF_E_TYPE_IS_PARAMETERIZED = unchecked((int)0x8013135c);

        /// <summary>The given function is a generic and cannot be used with this method.</summary>
        public const int CORPROF_E_FUNCTION_IS_PARAMETERIZED = unchecked((int)0x8013135d);

        /// <summary>A profiler tried to walk the stack of an invalid thread</summary>
        public const int CORPROF_E_STACKSNAPSHOT_INVALID_TGT_THREAD = unchecked((int)0x8013135e);

        /// <summary>A profiler can not walk a thread that is currently executing unmanaged code</summary>
        public const int CORPROF_E_STACKSNAPSHOT_UNMANAGED_CTX = unchecked((int)0x8013135f);

        /// <summary>A stackwalk at this point may cause dead locks or data corruption</summary>
        public const int CORPROF_E_STACKSNAPSHOT_UNSAFE = unchecked((int)0x80131360);

        /// <summary>Stackwalking callback requested the walk to abort</summary>
        public const int CORPROF_E_STACKSNAPSHOT_ABORTED = unchecked((int)0x80131361);

        /// <summary>Returned when asked for the address of a static that is a literal.</summary>
        public const int CORPROF_E_LITERALS_HAVE_NO_ADDRESS = unchecked((int)0x80131362);

        /// <summary>A call was made at an unsupported time.  Examples include illegally calling a profiling API method asynchronously, calling a method that might trigger a GC at an unsafe time, and calling a method at a time that could cause locks to be taken out of order.</summary>
        public const int CORPROF_E_UNSUPPORTED_CALL_SEQUENCE = unchecked((int)0x80131363);

        /// <summary>A legal asynchronous call was made at an unsafe time (e.g., CLR locks are held)</summary>
        public const int CORPROF_E_ASYNCHRONOUS_UNSAFE = unchecked((int)0x80131364);

        /// <summary>The specified ClassID cannot be inspected by this function because it is an array</summary>
        public const int CORPROF_E_CLASSID_IS_ARRAY = unchecked((int)0x80131365);

        /// <summary>The specified ClassID is a non-array composite type (e.g., ref) and cannot be inspected</summary>
        public const int CORPROF_E_CLASSID_IS_COMPOSITE = unchecked((int)0x80131366);

        /// <summary>The profiler's call into the CLR is disallowed because the profiler is attempting to detach.</summary>
        public const int CORPROF_E_PROFILER_DETACHING = unchecked((int)0x80131367);

        /// <summary>The profiler does not support attaching to a live process.</summary>
        public const int CORPROF_E_PROFILER_NOT_ATTACHABLE = unchecked((int)0x80131368);

        /// <summary>The message sent on the profiling API attach pipe is in an unrecognized format.</summary>
        public const int CORPROF_E_UNRECOGNIZED_PIPE_MSG_FORMAT = unchecked((int)0x80131369);

        /// <summary>The request to attach a profiler was denied because a profiler is already loaded.</summary>
        public const int CORPROF_E_PROFILER_ALREADY_ACTIVE = unchecked((int)0x8013136A);

        /// <summary>Unable to request a profiler attach because the target profilee's runtime is of a version incompatible with the current process calling AttachProfiler().</summary>
        public const int CORPROF_E_PROFILEE_INCOMPATIBLE_WITH_TRIGGER = unchecked((int)0x8013136B);

        /// <summary>AttachProfiler() encountered an error while communicating on the pipe to the target profilee.  This is often caused by a target profilee that is shutting down or killed while AttachProfiler() is reading or writing the pipe.</summary>
        public const int CORPROF_E_IPC_FAILED = unchecked((int)0x8013136C);

        /// <summary>AttachProfiler() was unable to find a profilee with the specified process ID.</summary>
        public const int CORPROF_E_PROFILEE_PROCESS_NOT_FOUND = unchecked((int)0x8013136D);

        /// <summary>Profiler must implement ICorProfilerCallback3 interface for this call to be supported.</summary>
        public const int CORPROF_E_CALLBACK3_REQUIRED = unchecked((int)0x8013136E);

        /// <summary>This call was attempted by a profiler that attached to the process after startup, but this call is only supported by profilers that are loaded into the process on startup.</summary>
        public const int CORPROF_E_UNSUPPORTED_FOR_ATTACHING_PROFILER = unchecked((int)0x8013136F);

        /// <summary>Detach is impossible because the profiler has either instrumented IL or inserted enter/leave hooks. Detach was not attempted; the profiler is still fully attached.</summary>
        public const int CORPROF_E_IRREVERSIBLE_INSTRUMENTATION_PRESENT = unchecked((int)0x80131370);

        /// <summary>The profiler called a function that cannot complete because the CLR is not yet fully initialized.  The profiler may try again once the CLR has fully started.</summary>
        public const int CORPROF_E_RUNTIME_UNINITIALIZED = unchecked((int)0x80131371);

        /// <summary>Detach is impossible because immutable flags were set by the profiler at startup. Detach was not attempted; the profiler is still fully attached.</summary>
        public const int CORPROF_E_IMMUTABLE_FLAGS_SET = unchecked((int)0x80131372);

        /// <summary>The profiler called a function that cannot complete because the profiler is not yet fully initialized.</summary>
        public const int CORPROF_E_PROFILER_NOT_YET_INITIALIZED = unchecked((int)0x80131373);

        /// <summary>The profiler called a function that first requires additional flags to be set in the event mask.  This HRESULT may also indicate that the profiler called a function that first requires that some of the flags currently set in the event mask be reset.</summary>
        public const int CORPROF_E_INCONSISTENT_WITH_FLAGS = unchecked((int)0x80131374);

        /// <summary>The profiler has requested that the CLR instance not load the profiler into this process.</summary>
        public const int CORPROF_E_PROFILER_CANCEL_ACTIVATION = unchecked((int)0x80131375);

        /// <summary>Concurrent GC mode is enabled, which prevents use of COR_PRF_MONITOR_GC</summary>
        public const int CORPROF_E_CONCURRENT_GC_NOT_PROFILABLE = unchecked((int)0x80131376);

        /// <summary>This functionality requires CoreCLR debugging to be enabled.</summary>
        public const int CORPROF_E_DEBUGGING_DISABLED = unchecked((int)0x80131378);

        /// <summary>Timed out on waiting for concurrent GC to finish during attach.</summary>
        public const int CORPROF_E_TIMEOUT_WAITING_FOR_CONCURRENT_GC = unchecked((int)0x80131379);

        /// <summary>The specified module was dynamically generated (e.g., via Reflection.Emit API), and is thus not supported by this API method.</summary>
        public const int CORPROF_E_MODULE_IS_DYNAMIC = unchecked((int)0x8013137A);

        /// <summary>Profiler must implement ICorProfilerCallback4 interface for this call to be supported.</summary>
        public const int CORPROF_E_CALLBACK4_REQUIRED = unchecked((int)0x8013137B);

        /// <summary>This call is not supported unless ReJIT is first enabled during initialization by setting COR_PRF_ENABLE_REJIT via SetEventMask.</summary>
        public const int CORPROF_E_REJIT_NOT_ENABLED = unchecked((int)0x8013137C);

        /// <summary>The specified function is instantiated into a collectible assembly, and is thus not supported by this API method.</summary>
        public const int CORPROF_E_FUNCTION_IS_COLLECTIBLE = unchecked((int)0x8013137E);

        /// <summary>Profiler must implement ICorProfilerCallback6 interface for this call to be supported.</summary>
        public const int CORPROF_E_CALLBACK6_REQUIRED = unchecked((int)0x80131380);

        /// <summary>Profiler must implement ICorProfilerCallback7 interface for this call to be supported.</summary>
        public const int CORPROF_E_CALLBACK7_REQUIRED = unchecked((int)0x80131382);

        /// <summary>The runtime's tracking of inlined methods for ReJIT is not enabled.</summary>
        public const int CORPROF_E_REJIT_INLINING_DISABLED = unchecked((int)0x80131383);

        /// <summary>The runtime was unable to decode the Header or Payload.</summary>
        public const int CORDIAGIPC_E_BAD_ENCODING = unchecked((int)0x80131384);

        /// <summary>The specified CommandSet or CommandId is unknown.</summary>
        public const int CORDIAGIPC_E_UNKNOWN_COMMAND = unchecked((int)0x80131385);

        /// <summary>The magic version of Diagnostics IPC is unknown.</summary>
        public const int CORDIAGIPC_E_UNKNOWN_MAGIC = unchecked((int)0x80131386);

        /// <summary>An unknown error occurred in the Diagnpostics IPC Server.</summary>
        public const int CORDIAGIPC_E_UNKNOWN_ERROR = unchecked((int)0x80131387);

        /// <summary>The runtime cannot be suspened since a suspension is already in progress.</summary>
        public const int CORPROF_E_SUSPENSION_IN_PROGRESS = unchecked((int)0x80131388);

        /// <summary>Loading this assembly would produce a different grant set from other instances.</summary>
        public const int SECURITY_E_INCOMPATIBLE_SHARE = unchecked((int)0x80131401);

        /// <summary>Unverifiable code failed policy check.</summary>
        public const int SECURITY_E_UNVERIFIABLE = unchecked((int)0x80131402);

        /// <summary>Assembly already loaded without additional security evidence.</summary>
        public const int SECURITY_E_INCOMPATIBLE_EVIDENCE = unchecked((int)0x80131403);

        /// <summary>PolicyException thrown.</summary>
        public const int CORSEC_E_POLICY_EXCEPTION = unchecked((int)0x80131416);

        /// <summary>Failed to grant minimum permission requests.</summary>
        public const int CORSEC_E_MIN_GRANT_FAIL = unchecked((int)0x80131417);

        /// <summary>Failed to grant permission to execute.</summary>
        public const int CORSEC_E_NO_EXEC_PERM = unchecked((int)0x80131418);

        /// <summary>XML Syntax error.</summary>
        public const int CORSEC_E_XMLSYNTAX = unchecked((int)0x80131419);

        /// <summary>Strong name validation failed.</summary>
        public const int CORSEC_E_INVALID_STRONGNAME = unchecked((int)0x8013141a);

        /// <summary>Assembly is not strong named.</summary>
        public const int CORSEC_E_MISSING_STRONGNAME = unchecked((int)0x8013141b);

        /// <summary>Invalid assembly file format.</summary>
        public const int CORSEC_E_INVALID_IMAGE_FORMAT = unchecked((int)0x8013141d);

        /// <summary>Invalid assembly public key.</summary>
        public const int CORSEC_E_INVALID_PUBLICKEY = unchecked((int)0x8013141e);

        /// <summary>Signature size mismatch.</summary>
        public const int CORSEC_E_SIGNATURE_MISMATCH = unchecked((int)0x80131420);

        /// <summary>Failure during Cryptographic operation.</summary>
        public const int CORSEC_E_CRYPTO = unchecked((int)0x80131430);

        /// <summary>Unexpected Cryptographic operation.</summary>
        public const int CORSEC_E_CRYPTO_UNEX_OPER = unchecked((int)0x80131431);

        /// <summary>Invalid security action code.</summary>
        public const int CORSECATTR_E_BAD_ACTION = unchecked((int)0x80131442);

        /// <summary>General Exception</summary>
        public const int COR_E_EXCEPTION = unchecked((int)0x80131500);

        /// <summary>System.Exception</summary>
        public const int COR_E_SYSTEM = unchecked((int)0x80131501);

        /// <summary>An argument was out of its legal range.</summary>
        public const int COR_E_ARGUMENTOUTOFRANGE = unchecked((int)0x80131502);

        /// <summary>Attempted to store an object of the wrong type in an array.</summary>
        public const int COR_E_ARRAYTYPEMISMATCH = unchecked((int)0x80131503);

        /// <summary>Attempted to marshal an object across a context boundary.</summary>
        public const int COR_E_CONTEXTMARSHAL = unchecked((int)0x80131504);

        /// <summary>Operation timed out.</summary>
        public const int COR_E_TIMEOUT = unchecked((int)0x80131505);

        /// <summary>Internal CLR error.</summary>
        public const int COR_E_EXECUTIONENGINE = unchecked((int)0x80131506);

        /// <summary>Access to this field is denied.</summary>
        public const int COR_E_FIELDACCESS = unchecked((int)0x80131507);

        /// <summary>Array subscript out of range.</summary>
        public const int COR_E_INDEXOUTOFRANGE = unchecked((int)0x80131508);

        /// <summary>An operation is not legal in the current state.</summary>
        public const int COR_E_INVALIDOPERATION = unchecked((int)0x80131509);

        /// <summary>An error relating to security occurred.</summary>
        public const int COR_E_SECURITY = unchecked((int)0x8013150a);

        /// <summary>An error relating to serialization occurred.</summary>
        public const int COR_E_SERIALIZATION = unchecked((int)0x8013150c);

        /// <summary>A verification failure has occurred.</summary>
        public const int COR_E_VERIFICATION = unchecked((int)0x8013150d);

        /// <summary>Access to this method is denied.</summary>
        public const int COR_E_METHODACCESS = unchecked((int)0x80131510);

        /// <summary>Field does not exist.</summary>
        public const int COR_E_MISSINGFIELD = unchecked((int)0x80131511);

        /// <summary>Member does not exist.</summary>
        public const int COR_E_MISSINGMEMBER = unchecked((int)0x80131512);

        /// <summary>Method does not exist.</summary>
        public const int COR_E_MISSINGMETHOD = unchecked((int)0x80131513);

        /// <summary>Attempt to combine delegates that are not multicast.</summary>
        public const int COR_E_MULTICASTNOTSUPPORTED = unchecked((int)0x80131514);

        /// <summary>Operation is not supported.</summary>
        public const int COR_E_NOTSUPPORTED = unchecked((int)0x80131515);

        /// <summary>Arithmetic, casting or conversion operation overflowed or underflowed.</summary>
        public const int COR_E_OVERFLOW = unchecked((int)0x80131516);

        /// <summary>An array has the wrong number of dimensions for a particular operation.</summary>
        public const int COR_E_RANK = unchecked((int)0x80131517);

        /// <summary>This operation must be called from a synchronized block.</summary>
        public const int COR_E_SYNCHRONIZATIONLOCK = unchecked((int)0x80131518);

        /// <summary>Thread was interrupted from a waiting state.</summary>
        public const int COR_E_THREADINTERRUPTED = unchecked((int)0x80131519);

        /// <summary>Access to this member is denied.</summary>
        public const int COR_E_MEMBERACCESS = unchecked((int)0x8013151a);

        /// <summary>Thread is in an invalid state for this operation.</summary>
        public const int COR_E_THREADSTATE = unchecked((int)0x80131520);

        /// <summary>Thread is stopping.</summary>
        public const int COR_E_THREADSTOP = unchecked((int)0x80131521);

        /// <summary>Could not find or load a type.</summary>
        public const int COR_E_TYPELOAD = unchecked((int)0x80131522);

        /// <summary>Could not find the specified DllImport entrypoint.</summary>
        public const int COR_E_ENTRYPOINTNOTFOUND = unchecked((int)0x80131523);

        /// <summary>Could not find the specified DllImport Dll.</summary>
        public const int COR_E_DLLNOTFOUND = unchecked((int)0x80131524);

        /// <summary>Indicate that a user thread fails to start.</summary>
        public const int COR_E_THREADSTART = unchecked((int)0x80131525);

        /// <summary>An invalid __ComObject has been used.</summary>
        public const int COR_E_INVALIDCOMOBJECT = unchecked((int)0x80131527);

        /// <summary>Not a Number.</summary>
        public const int COR_E_NOTFINITENUMBER = unchecked((int)0x80131528);

        /// <summary>An object appears more than once in the wait objects array.</summary>
        public const int COR_E_DUPLICATEWAITOBJECT = unchecked((int)0x80131529);

        /// <summary>Reached maximum count for semaphore.</summary>
        public const int COR_E_SEMAPHOREFULL = unchecked((int)0x8013152b);

        /// <summary>No semaphore of the given name exists.</summary>
        public const int COR_E_WAITHANDLECANNOTBEOPENED = unchecked((int)0x8013152c);

        /// <summary>The wait completed due to an abandoned mutex.</summary>
        public const int COR_E_ABANDONEDMUTEX = unchecked((int)0x8013152d);

        /// <summary>Thread has aborted.</summary>
        public const int COR_E_THREADABORTED = unchecked((int)0x80131530);

        /// <summary>OLE Variant has an invalid type.</summary>
        public const int COR_E_INVALIDOLEVARIANTTYPE = unchecked((int)0x80131531);

        /// <summary>An expected resource in the assembly manifest was missing.</summary>
        public const int COR_E_MISSINGMANIFESTRESOURCE = unchecked((int)0x80131532);

        /// <summary>A mismatch has occurred between the runtime type of the array and the sub type recorded in the metadata.</summary>
        public const int COR_E_SAFEARRAYTYPEMISMATCH = unchecked((int)0x80131533);

        /// <summary>Uncaught exception during type initialization.</summary>
        public const int COR_E_TYPEINITIALIZATION = unchecked((int)0x80131534);

        /// <summary>Invalid marshaling directives.</summary>
        public const int COR_E_MARSHALDIRECTIVE = unchecked((int)0x80131535);

        /// <summary>An expected satellite assembly containing the ultimate fallback resources for a given culture was not found or could not be loaded.</summary>
        public const int COR_E_MISSINGSATELLITEASSEMBLY = unchecked((int)0x80131536);

        /// <summary>The format of one argument does not meet the contract of the method.</summary>
        public const int COR_E_FORMAT = unchecked((int)0x80131537);

        /// <summary>A mismatch has occurred between the runtime rank of the array and the rank recorded in the metadata.</summary>
        public const int COR_E_SAFEARRAYRANKMISMATCH = unchecked((int)0x80131538);

        /// <summary>Operation is not supported on this platform.</summary>
        public const int COR_E_PLATFORMNOTSUPPORTED = unchecked((int)0x80131539);

        /// <summary>Invalid IL or CLR metadata.</summary>
        public const int COR_E_INVALIDPROGRAM = unchecked((int)0x8013153a);

        /// <summary>The operation was cancelled.</summary>
        public const int COR_E_OPERATIONCANCELED = unchecked((int)0x8013153b);

        /// <summary>Not enough memory was available for an operation.</summary>
        public const int COR_E_INSUFFICIENTMEMORY = unchecked((int)0x8013153d);

        /// <summary>An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.</summary>
        public const int COR_E_RUNTIMEWRAPPED = unchecked((int)0x8013153e);

        /// <summary>A datatype misalignment was detected in a load or store instruction.</summary>
        public const int COR_E_DATAMISALIGNED = unchecked((int)0x80131541);

        /// <summary>A managed code contract (ie, precondition, postcondition, invariant, or assert) failed.</summary>
        public const int COR_E_CODECONTRACTFAILED = unchecked((int)0x80131542);

        /// <summary>Access to this type is denied.</summary>
        public const int COR_E_TYPEACCESS = unchecked((int)0x80131543);

        /// <summary>Fail to access a CCW because the corresponding managed object is already collected.</summary>
        public const int COR_E_ACCESSING_CCW = unchecked((int)0x80131544);

        /// <summary>The given key was not present in the dictionary.</summary>
        public const int COR_E_KEYNOTFOUND = unchecked((int)0x80131577);

        /// <summary>Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space.</summary>
        public const int COR_E_INSUFFICIENTEXECUTIONSTACK = unchecked((int)0x80131578);

        /// <summary>Application exception</summary>
        public const int COR_E_APPLICATION = unchecked((int)0x80131600);

        /// <summary>The given filter criteria does not match the filter content.</summary>
        public const int COR_E_INVALIDFILTERCRITERIA = unchecked((int)0x80131601);

        /// <summary>Could not find or load a specific class that was requested through Reflection.</summary>
        public const int COR_E_REFLECTIONTYPELOAD = unchecked((int)0x80131602);

        /// <summary>Attempt to invoke non-static method with a null Object.</summary>
        public const int COR_E_TARGET = unchecked((int)0x80131603);

        /// <summary>Uncaught exception thrown by method called through Reflection.</summary>
        public const int COR_E_TARGETINVOCATION = unchecked((int)0x80131604);

        /// <summary>Custom attribute has invalid format.</summary>
        public const int COR_E_CUSTOMATTRIBUTEFORMAT = unchecked((int)0x80131605);

        /// <summary>Error during managed I/O.</summary>
        public const int COR_E_IO = unchecked((int)0x80131620);

        /// <summary>Could not find or load a specific file.</summary>
        public const int COR_E_FILELOAD = unchecked((int)0x80131621);

        /// <summary>The object has already been disposed.</summary>
        public const int COR_E_OBJECTDISPOSED = unchecked((int)0x80131622);

        /// <summary>Runtime operation halted by call to System.Environment.FailFast().</summary>
        public const int COR_E_FAILFAST = unchecked((int)0x80131623);

        /// <summary>The host has forbidden this operation.</summary>
        public const int COR_E_HOSTPROTECTION = unchecked((int)0x80131640);

        /// <summary>Attempted to call into managed code when executing inside a low level extensibility point.</summary>
        public const int COR_E_ILLEGAL_REENTRANCY = unchecked((int)0x80131641);

        /// <summary>Failed to load the runtime.</summary>
        public const int CLR_E_SHIM_RUNTIMELOAD = unchecked((int)0x80131700);

        /// <summary>A runtime has already been bound for legacy activation policy use.</summary>
        public const int CLR_E_SHIM_LEGACYRUNTIMEALREADYBOUND = unchecked((int)0x80131704);

        /// <summary>[field sig]</summary>
        public const int VER_E_FIELD_SIG = unchecked((int)0x80131815);

        /// <summary>Method parent has circular class type parameter constraints.</summary>
        public const int VER_E_CIRCULAR_VAR_CONSTRAINTS = unchecked((int)0x801318ce);

        /// <summary>Method has circular method type parameter constraints.</summary>
        public const int VER_E_CIRCULAR_MVAR_CONSTRAINTS = unchecked((int)0x801318cf);

        /// <summary>Illegal 'void' in signature.</summary>
        public const int VLDTR_E_SIG_BADVOID = unchecked((int)0x80131b24);

        /// <summary>GenericParam is a method type parameter and must be non-variant.</summary>
        public const int VLDTR_E_GP_ILLEGAL_VARIANT_MVAR = unchecked((int)0x80131b2d);

        /// <summary>Thread is not scheduled. Thus we may not have OSThreadId, handle, or context.</summary>
        public const int CORDBG_E_THREAD_NOT_SCHEDULED = unchecked((int)0x80131c00);

        /// <summary>Handle has been disposed.</summary>
        public const int CORDBG_E_HANDLE_HAS_BEEN_DISPOSED = unchecked((int)0x80131c01);

        /// <summary>Cannot intercept this exception.</summary>
        public const int CORDBG_E_NONINTERCEPTABLE_EXCEPTION = unchecked((int)0x80131c02);

        /// <summary>The intercept frame for this exception has already been set.</summary>
        public const int CORDBG_E_INTERCEPT_FRAME_ALREADY_SET = unchecked((int)0x80131c04);

        /// <summary>There is no native patch at the given address.</summary>
        public const int CORDBG_E_NO_NATIVE_PATCH_AT_ADDR = unchecked((int)0x80131c05);

        /// <summary>This API is only allowed when interop debugging.</summary>
        public const int CORDBG_E_MUST_BE_INTEROP_DEBUGGING = unchecked((int)0x80131c06);

        /// <summary>There is already a native patch at the address.</summary>
        public const int CORDBG_E_NATIVE_PATCH_ALREADY_AT_ADDR = unchecked((int)0x80131c07);

        /// <summary>A wait timed out, likely an indication of deadlock.</summary>
        public const int CORDBG_E_TIMEOUT = unchecked((int)0x80131c08);

        /// <summary>Cannot use the API on this thread.</summary>
        public const int CORDBG_E_CANT_CALL_ON_THIS_THREAD = unchecked((int)0x80131c09);

        /// <summary>Method was not JIT'd in EnC mode.</summary>
        public const int CORDBG_E_ENC_INFOLESS_METHOD = unchecked((int)0x80131c0a);

        /// <summary>Method is in a callable handler/filter. Cannot increase stack.</summary>
        public const int CORDBG_E_ENC_IN_FUNCLET = unchecked((int)0x80131c0c);

        /// <summary>Attempt to perform unsupported edit.</summary>
        public const int CORDBG_E_ENC_EDIT_NOT_SUPPORTED = unchecked((int)0x80131c0e);

        /// <summary>The LS is not in a good spot to perform the requested operation.</summary>
        public const int CORDBG_E_NOTREADY = unchecked((int)0x80131c10);

        /// <summary>We failed to resolve assembly given an AssemblyRef token. Assembly may be not loaded yet or not a valid token.</summary>
        public const int CORDBG_E_CANNOT_RESOLVE_ASSEMBLY = unchecked((int)0x80131c11);

        /// <summary>Must be in context of LoadModule callback to perform requested operation.</summary>
        public const int CORDBG_E_MUST_BE_IN_LOAD_MODULE = unchecked((int)0x80131c12);

        /// <summary>Requested operation cannot be performed during an attach operation.</summary>
        public const int CORDBG_E_CANNOT_BE_ON_ATTACH = unchecked((int)0x80131c13);

        /// <summary>NGEN must be supported to perform the requested operation.</summary>
        public const int CORDBG_E_NGEN_NOT_SUPPORTED = unchecked((int)0x80131c14);

        /// <summary>Trying to shutdown out of order.</summary>
        public const int CORDBG_E_ILLEGAL_SHUTDOWN_ORDER = unchecked((int)0x80131c15);

        /// <summary>Debugging fiber mode managed process is not supported.</summary>
        public const int CORDBG_E_CANNOT_DEBUG_FIBER_PROCESS = unchecked((int)0x80131c16);

        /// <summary>Must be in context of CreateProcess callback to perform requested operation.</summary>
        public const int CORDBG_E_MUST_BE_IN_CREATE_PROCESS = unchecked((int)0x80131c17);

        /// <summary>All outstanding func-evals have not completed, detaching is not allowed at this time.</summary>
        public const int CORDBG_E_DETACH_FAILED_OUTSTANDING_EVALS = unchecked((int)0x80131c18);

        /// <summary>All outstanding steppers have not been closed, detaching is not allowed at this time.</summary>
        public const int CORDBG_E_DETACH_FAILED_OUTSTANDING_STEPPERS = unchecked((int)0x80131c19);

        /// <summary>Cannot have an ICorDebugStepper do a native step-out.</summary>
        public const int CORDBG_E_CANT_INTEROP_STEP_OUT = unchecked((int)0x80131c20);

        /// <summary>All outstanding breakpoints have not been closed, detaching is not allowed at this time.</summary>
        public const int CORDBG_E_DETACH_FAILED_OUTSTANDING_BREAKPOINTS = unchecked((int)0x80131c21);

        /// <summary>The operation is illegal because of a stack overflow.</summary>
        public const int CORDBG_E_ILLEGAL_IN_STACK_OVERFLOW = unchecked((int)0x80131c22);

        /// <summary>The operation failed because it is a GC unsafe point.</summary>
        public const int CORDBG_E_ILLEGAL_AT_GC_UNSAFE_POINT = unchecked((int)0x80131c23);

        /// <summary>The operation failed because the thread is in the prolog.</summary>
        public const int CORDBG_E_ILLEGAL_IN_PROLOG = unchecked((int)0x80131c24);

        /// <summary>The operation failed because the thread is in native code.</summary>
        public const int CORDBG_E_ILLEGAL_IN_NATIVE_CODE = unchecked((int)0x80131c25);

        /// <summary>The operation failed because the thread is in optimized code.</summary>
        public const int CORDBG_E_ILLEGAL_IN_OPTIMIZED_CODE = unchecked((int)0x80131c26);

        /// <summary>A supplied object or type belongs to the wrong AppDomain.</summary>
        public const int CORDBG_E_APPDOMAIN_MISMATCH = unchecked((int)0x80131c28);

        /// <summary>The thread's context is not available.</summary>
        public const int CORDBG_E_CONTEXT_UNVAILABLE = unchecked((int)0x80131c29);

        /// <summary>The operation failed because debuggee and debugger are on incompatible platforms.</summary>
        public const int CORDBG_E_UNCOMPATIBLE_PLATFORMS = unchecked((int)0x80131c30);

        /// <summary>The operation failed because the debugging has been disabled</summary>
        public const int CORDBG_E_DEBUGGING_DISABLED = unchecked((int)0x80131c31);

        /// <summary>Detach is illegal after an Edit and Continue on a module.</summary>
        public const int CORDBG_E_DETACH_FAILED_ON_ENC = unchecked((int)0x80131c32);

        /// <summary>Cannot intercept the current exception at the specified frame.</summary>
        public const int CORDBG_E_CURRENT_EXCEPTION_IS_OUTSIDE_CURRENT_EXECUTION_SCOPE = unchecked((int)0x80131c33);

        /// <summary>The debugger helper thread cannot obtain the locks it needs to perform this operation.</summary>
        public const int CORDBG_E_HELPER_MAY_DEADLOCK = unchecked((int)0x80131c34);

        /// <summary>The operation failed because the debugger could not get the metadata.</summary>
        public const int CORDBG_E_MISSING_METADATA = unchecked((int)0x80131c35);

        /// <summary>The debuggee is in a corrupt state.</summary>
        public const int CORDBG_E_TARGET_INCONSISTENT = unchecked((int)0x80131c36);

        /// <summary>Detach failed because there are outstanding resources in the target.</summary>
        public const int CORDBG_E_DETACH_FAILED_OUTSTANDING_TARGET_RESOURCES = unchecked((int)0x80131c37);

        /// <summary>The debuggee is read-only.</summary>
        public const int CORDBG_E_TARGET_READONLY = unchecked((int)0x80131c38);

        /// <summary>The version of clr.dll in the target does not match the one mscordacwks.dll was built for.</summary>
        public const int CORDBG_E_MISMATCHED_CORWKS_AND_DACWKS_DLLS = unchecked((int)0x80131c39);

        /// <summary>Symbols are not supplied for modules loaded from disk.</summary>
        public const int CORDBG_E_MODULE_LOADED_FROM_DISK = unchecked((int)0x80131c3a);

        /// <summary>The application did not supply symbols when it loaded or created this module, or they are not yet available.</summary>
        public const int CORDBG_E_SYMBOLS_NOT_AVAILABLE = unchecked((int)0x80131c3b);

        /// <summary>A debug component is not installed.</summary>
        public const int CORDBG_E_DEBUG_COMPONENT_MISSING = unchecked((int)0x80131c3c);

        /// <summary>The ICLRDebuggingLibraryProvider callback returned an error or did not provide a valid handle.</summary>
        public const int CORDBG_E_LIBRARY_PROVIDER_ERROR = unchecked((int)0x80131c43);

        /// <summary>The module at the base address indicated was not recognized as a CLR</summary>
        public const int CORDBG_E_NOT_CLR = unchecked((int)0x80131c44);

        /// <summary>The provided data target does not implement the required interfaces for this version of the runtime</summary>
        public const int CORDBG_E_MISSING_DATA_TARGET_INTERFACE = unchecked((int)0x80131c45);

        /// <summary>This debugging model is unsupported by the specified runtime</summary>
        public const int CORDBG_E_UNSUPPORTED_DEBUGGING_MODEL = unchecked((int)0x80131c46);

        /// <summary>The debugger is not designed to support the version of the CLR the debuggee is using.</summary>
        public const int CORDBG_E_UNSUPPORTED_FORWARD_COMPAT = unchecked((int)0x80131c47);

        /// <summary>The version struct has an unrecognized value for wStructVersion</summary>
        public const int CORDBG_E_UNSUPPORTED_VERSION_STRUCT = unchecked((int)0x80131c48);

        /// <summary>A call into a ReadVirtual implementation returned failure</summary>
        public const int CORDBG_E_READVIRTUAL_FAILURE = unchecked((int)0x80131c49);

        /// <summary>The Debugging API doesn't support dereferencing function pointers.</summary>
        public const int CORDBG_E_VALUE_POINTS_TO_FUNCTION = unchecked((int)0x80131c4a);

        /// <summary>The address provided does not point to a valid managed object.</summary>
        public const int CORDBG_E_CORRUPT_OBJECT = unchecked((int)0x80131c4b);

        /// <summary>The GC heap structures are not in a valid state for traversal.</summary>
        public const int CORDBG_E_GC_STRUCTURES_INVALID = unchecked((int)0x80131c4c);

        /// <summary>The specified IL offset or opcode is not supported for this operation.</summary>
        public const int CORDBG_E_INVALID_OPCODE = unchecked((int)0x80131c4d);

        /// <summary>The specified action is unsupported by this version of the runtime.</summary>
        public const int CORDBG_E_UNSUPPORTED = unchecked((int)0x80131c4e);

        /// <summary>The debuggee memory space does not have the expected debugging export table.</summary>
        public const int CORDBG_E_MISSING_DEBUGGER_EXPORTS = unchecked((int)0x80131c4f);

        /// <summary>Failure when calling a data target method.</summary>
        public const int CORDBG_E_DATA_TARGET_ERROR = unchecked((int)0x80131c61);

        /// <summary>Couldn't find a native image.</summary>
        public const int CORDBG_E_NO_IMAGE_AVAILABLE = unchecked((int)0x80131c64);

        /// <summary>The delegate contains a delegate currently not supported by the API.</summary>
        public const int CORDBG_E_UNSUPPORTED_DELEGATE = unchecked((int)0x80131c68);

        /// <summary>File is PE32+.</summary>
        public const int PEFMT_E_64BIT = unchecked((int)0x80131d02);

        /// <summary>File is PE32</summary>
        public const int PEFMT_E_32BIT = unchecked((int)0x80131d0b);

        /// <summary>NGen cannot proceed because Mscorlib.dll does not have a native image</summary>
        public const int NGEN_E_SYS_ASM_NI_MISSING = unchecked((int)0x80131f06);

        /// <summary>The bound assembly has a version that is lower than that of the request.</summary>
        public const int CLR_E_BIND_ASSEMBLY_VERSION_TOO_LOW = unchecked((int)0x80132000);

        /// <summary>The assembly version has a public key token that does not match that of the request.</summary>
        public const int CLR_E_BIND_ASSEMBLY_PUBLIC_KEY_MISMATCH = unchecked((int)0x80132001);

        /// <summary>The requested image was not found or is unavailable.</summary>
        public const int CLR_E_BIND_IMAGE_UNAVAILABLE = unchecked((int)0x80132002);

        /// <summary>The provided identity format is not recognized.</summary>
        public const int CLR_E_BIND_UNRECOGNIZED_IDENTITY_FORMAT = unchecked((int)0x80132003);

        /// <summary>A binding for the specified assembly name was not found.</summary>
        public const int CLR_E_BIND_ASSEMBLY_NOT_FOUND = unchecked((int)0x80132004);

        /// <summary>A binding for the specified type name was not found.</summary>
        public const int CLR_E_BIND_TYPE_NOT_FOUND = unchecked((int)0x80132005);

        /// <summary>Could not use native image because Mscorlib.dll is missing a native image</summary>
        public const int CLR_E_BIND_SYS_ASM_NI_MISSING = unchecked((int)0x80132006);

        /// <summary>Native image was generated in a different trust level than present at runtime</summary>
        public const int CLR_E_BIND_NI_SECURITY_FAILURE = unchecked((int)0x80132007);

        /// <summary>Native image identity mismatch with respect to its dependencies</summary>
        public const int CLR_E_BIND_NI_DEP_IDENTITY_MISMATCH = unchecked((int)0x80132008);

        /// <summary>Failfast due to an OOM during a GC</summary>
        public const int CLR_E_GC_OOM = unchecked((int)0x80132009);

        /// <summary>GCHeapAffinitizeMask or GCHeapAffinitizeRanges didn't specify any CPUs the current process is affinitized to.</summary>
        public const int CLR_E_GC_BAD_AFFINITY_CONFIG = unchecked((int)0x8013200A);

        /// <summary>GCHeapAffinitizeRanges configuration string has invalid format.</summary>
        public const int CLR_E_GC_BAD_AFFINITY_CONFIG_FORMAT = unchecked((int)0x8013200B);

        /// <summary>Cannot compile using the PartialNgen flag because no IBC data was found.</summary>
        public const int CLR_E_CROSSGEN_NO_IBC_DATA_FOUND = unchecked((int)0x8013200C);

        /// <summary>Access is denied.</summary>
        public const int COR_E_UNAUTHORIZEDACCESS = unchecked((int)0x80070005);

        /// <summary>An argument does not meet the contract of the method.</summary>
        public const int COR_E_ARGUMENT = unchecked((int)0x80070057);

        /// <summary>Indicates a bad cast condition</summary>
        public const int COR_E_INVALIDCAST = unchecked((int)0x80004002);

        /// <summary>The EE thows this exception when no more memory is avaible to continue execution</summary>
        public const int COR_E_OUTOFMEMORY = unchecked((int)0x8007000E);

        /// <summary>Dereferencing a null reference. In general class libraries should not throw this</summary>
        public const int COR_E_NULLREFERENCE = unchecked((int)0x80004003);

        /// <summary>Overflow or underflow in mathematical operations.</summary>
        public const int COR_E_ARITHMETIC = unchecked((int)0x80070216);

        /// <summary>The specified path was too long.</summary>
        public const int COR_E_PATHTOOLONG = unchecked((int)0x800700CE);

        /// <summary>The system cannot find the file specified.</summary>
        public const int COR_E_FILENOTFOUND = unchecked((int)0x80070002);

        /// <summary>Attempted to read past the end of the stream.</summary>
        public const int COR_E_ENDOFSTREAM = unchecked((int)0x80070026);

        /// <summary>The specified path couldn't be found.</summary>
        public const int COR_E_DIRECTORYNOTFOUND = unchecked((int)0x80070003);

        /// <summary>Is raised by the EE when the execution stack overflows as it is attempting to ex</summary>
        public const int COR_E_STACKOVERFLOW = unchecked((int)0x800703E9);

        /// <summary>While late binding to a method via reflection, could not resolve between</summary>
        public const int COR_E_AMBIGUOUSMATCH = unchecked((int)0x8000211D);

        /// <summary>There was a mismatch between number of arguments provided and the number expected</summary>
        public const int COR_E_TARGETPARAMCOUNT = unchecked((int)0x8002000E);

        /// <summary>Attempted to divide a number by zero.</summary>
        public const int COR_E_DIVIDEBYZERO = unchecked((int)0x80020012);

        /// <summary>The format of a DLL or executable being loaded is invalid.</summary>
        public const int COR_E_BADIMAGEFORMAT = unchecked((int)0x8007000B);
    }
}
