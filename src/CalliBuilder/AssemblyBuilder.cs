using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace CalliBuilder
{
    internal class AssemblyBuilder
    {
        // Add additional calli methods here. Do not include the @this or vtableSlot parameters
        // as they will be added automatically.
        private static readonly Type[][] s_calliMethods =
        {
            Type.EmptyTypes,
            new[] { typeof(UIntPtr), typeof(void*) },
            new[] { typeof(Primitive128), typeof(int), typeof(void*), typeof(void*) },
            new[] { typeof(Primitive128), typeof(void*) },
            new[] { typeof(Guid), typeof(void*) },
            new[] { typeof(int) },
            new[] { typeof(int), typeof(int) },
            new[] { typeof(int), typeof(void*) },
            new[] { typeof(int), typeof(void*), typeof(void*) },
            new[] { typeof(int), typeof(void*), typeof(uint) },
            new[] { typeof(int), typeof(void*), typeof(int) },
            new[] { typeof(int), typeof(void*), typeof(uint), typeof(void*), typeof(void*) },
            new[] { typeof(ulong), typeof(ulong), typeof(int), typeof(void*), typeof(void*) },
            new[] { typeof(void*) },
            new[] { typeof(void*), typeof(void*) },
            new[] { typeof(void*), typeof(void*), typeof(void*) },
            new[] { typeof(void*), typeof(int) },
            new[] { typeof(void*), typeof(uint), typeof(void*) },
            new[] { typeof(long), typeof(void*) },
            new[] { typeof(ulong) },
            new[] { typeof(ulong), typeof(int), typeof(void*) },
            new[] { typeof(ulong), typeof(int), typeof(void*), typeof(void*) },
            new[] { typeof(ulong), typeof(void*) },
            new[] { typeof(ulong), typeof(void*), typeof(void*) },
            new[] { typeof(uint), typeof(int) },
            new[] { typeof(uint), typeof(void*) },
            new[] { typeof(uint), typeof(void*), typeof(void*) },
            new[] { typeof(uint), typeof(int), typeof(void*) },

            new[] { typeof(uint), typeof(uint), typeof(int), typeof(void*), typeof(void*) },
            // ICorDebug.CreateProcess
            new[]
            {
                /* lpApplicationName:    */ typeof(void*),
                /* lpCommandLine:        */ typeof(void*),
                /* lpProcessAttributes:  */ typeof(void*),
                /* lpThreadAttributes:   */ typeof(void*),
                /* bInheritHandles:      */ typeof(int),
                /* dwCreationFlags:      */ typeof(uint),
                /* lpEnvironment:        */ typeof(void*),
                /* lpCurrentDirectory:   */ typeof(void*),
                /* lpStartupInfo:        */ typeof(void*),
                /* lpProcessInformation: */ typeof(void*),
                /* debuggingFlags:       */ typeof(int),
                /* pProcess:             */ typeof(void*)
            },
        };

        private static readonly string[] s_fwdFromStd =
        {
            "Microsoft.Win32.Primitives", "System.Collections", "System.Collections.Concurrent",
            "System.Collections.NonGeneric", "System.Collections.Specialized", "System.ComponentModel",
            "System.ComponentModel.EventBasedAsync", "System.ComponentModel.Primitives", "System.ComponentModel.TypeConverter",
            "System.Console", "System.Data.Common", "System.Diagnostics.FileVersionInfo",
            "System.Diagnostics.Process", "System.Diagnostics.StackTrace", "System.Diagnostics.TextWriterTraceListener",
            "System.Diagnostics.Tools", "System.Diagnostics.TraceSource", "System.Diagnostics.Tracing",
            "System.Drawing.Primitives", "System.IO.Compression", "System.IO.Compression.ZipFile",
            "System.IO.FileSystem", "System.IO.FileSystem.DriveInfo", "System.IO.FileSystem.Watcher",
            "System.IO.IsolatedStorage", "System.IO.MemoryMappedFiles", "System.IO.Pipes",
            "System.Linq", "System.Linq.Expressions", "System.Linq.Parallel",
            "System.Linq.Queryable", "System.Net.Http", "System.Net.HttpListener",
            "System.Net.Mail", "System.Net.NameResolution", "System.Net.NetworkInformation",
            "System.Net.Ping", "System.Net.Primitives", "System.Net.Requests",
            "System.Net.Security", "System.Net.ServicePoint", "System.Net.Sockets",
            "System.Net.WebClient", "System.Net.WebHeaderCollection", "System.Net.WebProxy",
            "System.Net.WebSockets", "System.Net.WebSockets.Client", "System.ObjectModel",
            "System.Private.CoreLib", "System.Private.DataContractSerialization", "System.Private.Uri",
            "System.Private.Xml", "System.Private.Xml.Linq", "System.Resources.Writer",
            "System.Runtime", "System.Runtime.CompilerServices.VisualC", "System.Runtime.Extensions",
            "System.Runtime.InteropServices", "System.Runtime.InteropServices.RuntimeInformation", "System.Runtime.Numerics",
            "System.Runtime.Serialization.Formatters", "System.Runtime.Serialization.Primitives", "System.Security.Claims",
            "System.Security.Cryptography.Algorithms", "System.Security.Cryptography.Csp", "System.Security.Cryptography.Encoding",
            "System.Security.Cryptography.Primitives", "System.Security.Cryptography.X509Certificates", "System.Security.Principal",
            "System.Text.RegularExpressions", "System.Threading", "System.Threading.Tasks.Parallel",
            "System.Threading.Thread", "System.Transactions.Local", "System.Web.HttpUtility",
            "System.Xml.XPath.XDocument",
        };

        private static readonly Guid s_calliGuid =
            new Guid(0x3A4B8CAC, 0x7DE0, 0x49E6, 0x9B, 0x34, 0xB1, 0x08, 0xA4, 0x8B, 0x78, 0xB7);

        private readonly Dictionary<Type, TypeReferenceHandle> _typeRefs = new Dictionary<Type, TypeReferenceHandle>();

        private readonly Dictionary<Assembly, AssemblyReferenceHandle> _assemblyRefs = new Dictionary<Assembly, AssemblyReferenceHandle>();

        private MetadataBuilder _md;

        private TypeDefinitionHandle _primitive128;

        private AssemblyReferenceHandle _netstandard;

        private MethodBodyStreamEncoder _methodBodyStream;

        private string _projectName;

        private string _assemblyName;

        private int _nextFieldRow = 1;

        public static void Create(string projectName, string path)
        {
            var builder = new AssemblyBuilder();
            builder.Create(projectName);

            foreach (Type[] methodSig in s_calliMethods)
            {
                builder.CreateCalliMethod(methodSig);
            }

            var root = new MetadataRootBuilder(builder._md);
            var peBuilder = new ManagedPEBuilder(
                PEHeaderBuilder.CreateLibraryHeader(),
                root,
                builder._methodBodyStream.Builder);

            var serialized = new BlobBuilder();
            peBuilder.Serialize(serialized);

            using var stream = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);

            stream.Write(serialized.ToArray(), 0, serialized.Count);
        }

        public void Create(string projectName)
        {
            _projectName = projectName;
            _assemblyName = projectName + ".Calli";
            _md = new MetadataBuilder();
            _methodBodyStream = new MethodBodyStreamEncoder(new BlobBuilder());

            _netstandard = _md.AddAssemblyReference(
                _md.GetOrAddString("netstandard"),
                new Version(2, 0, 0, 0),
                default,
                _md.GetOrAddBlob(new byte[] { 0xCC, 0x7B, 0x13, 0xFF, 0xCD, 0x2D, 0xDD, 0x51 }),
                default,
                default);

            EntityHandle systemObject = GetOrCreateTypeRef(typeof(object));

            var sigBlob = new BlobBuilder();
            new MethodSignatureEncoder(sigBlob, hasVarArgs: false)
                .Parameters(parameterCount: 0, rt => rt.Void(), p => {});

            MemberReferenceHandle systemObjectCtor = _md.AddMemberReference(
                systemObject,
                _md.GetOrAddString(".ctor"),
                _md.GetOrAddBlob(sigBlob));

            sigBlob.Clear();

            AssemblyDefinitionHandle assembly = _md.AddAssembly(
                _md.GetOrAddString(_assemblyName),
                new Version(1, 0, 0, 0),
                default,
                default,
                default,
                default);

            AddInternalsVisibleTo(assembly);

            ModuleDefinitionHandle module = _md.AddModule(
                generation: 0,
                _md.GetOrAddString(_assemblyName + ".dll"),
                _md.GetOrAddGuid(s_calliGuid),
                default,
                default);

            _md.AddTypeDefinition(
                default,
                default,
                _md.GetOrAddString("<Module>"),
                default,
                MetadataTokens.FieldDefinitionHandle(1),
                MetadataTokens.MethodDefinitionHandle(1));

            _primitive128 = CreatePrimitive128();

            _md.AddTypeDefinition(
                TypeAttributes.Abstract | TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                _md.GetOrAddString(_projectName),
                _md.GetOrAddString("CalliInstructions"),
                systemObject,
                MetadataTokens.FieldDefinitionHandle(_nextFieldRow),
                MetadataTokens.MethodDefinitionHandle(1));
        }

        public void AddInternalsVisibleTo(AssemblyDefinitionHandle assembly)
        {
            var internalsVisibleTo = GetOrCreateTypeRef(typeof(InternalsVisibleToAttribute));
            var encoder = new BlobEncoder(new BlobBuilder());
            encoder
                .MethodSignature(isInstanceMethod: true)
                .Parameters(
                    parameterCount: 1,
                    rt => rt.Void(),
                    p => p.AddParameter().Type().String());

            var ctor = _md.AddMemberReference(
                internalsVisibleTo,
                _md.GetOrAddString(".ctor"),
                _md.GetOrAddBlob(encoder.Builder));

            var blob = new BlobBuilder();
            new BlobEncoder(blob)
                .CustomAttributeSignature(
                    fa => fa.AddArgument().Scalar().Constant(_projectName),
                    na => na.Count(0));

            _md.AddCustomAttribute(
                assembly,
                ctor,
                _md.GetOrAddBlob(blob));
        }

        internal MethodDefinitionHandle CreateCalliMethod(params Type[] types)
        {
            var targetParams = new Type[types.Length + 1];
            targetParams[0] = typeof(void*);
            Array.Copy(types, 0, targetParams, 1, types.Length);
            StandaloneSignatureHandle calliSig = CreateCalliSig(
                SignatureCallingConvention.StdCall,
                typeof(int),
                targetParams);

            var methodSig = new BlobEncoder(new BlobBuilder());
            methodSig.MethodSignature().Parameters(
                types.Length + 2,
                rt => rt.Type().Int32(),
                p => EncodeParameters(p, types));

            var il = new InstructionEncoder(new BlobBuilder());
            il.LoadArgument(0);
            for (int i = 0; i < types.Length; i++)
            {
                il.LoadArgument(i + 2);
            }

            il.LoadArgument(1);
            il.CallIndirect(calliSig);
            il.OpCode(ILOpCode.Ret);

            int paramSequence = 1;
            var thisParam = _md.AddParameter(ParameterAttributes.None, _md.GetOrAddString("this"), paramSequence++);
            _md.AddParameter(ParameterAttributes.None, _md.GetOrAddString("vtableSlot"), paramSequence++);

            for (int i = 0; i < types.Length; i++)
            {
                _md.AddParameter(
                    ParameterAttributes.None,
                    _md.GetOrAddString($"arg{i}"),
                    paramSequence++);
            }

            return _md.AddMethodDefinition(
                MethodAttributes.Assembly | MethodAttributes.Static,
                MethodImplAttributes.AggressiveInlining,
                _md.GetOrAddString("Calli"),
                _md.GetOrAddBlob(methodSig.Builder),
                _methodBodyStream.AddMethodBody(il),
                thisParam);
        }

        private StandaloneSignatureHandle CreateCalliSig(
            SignatureCallingConvention callingConvention,
            Type returnType,
            Type[] parameterTypes)
        {
            var encoder = new BlobEncoder(new BlobBuilder());
            encoder.MethodSignature(callingConvention)
                .Parameters(
                    parameterTypes.Length,
                    rt => EncodeType(rt.Type(), returnType),
                    p => { foreach (Type type in parameterTypes) EncodeType(p.AddParameter().Type(), type); });

            return _md.AddStandaloneSignature(_md.GetOrAddBlob(encoder.Builder));
        }

        private void EncodeParameters(ParametersEncoder encoder, Type[] types)
        {
            encoder.AddParameter().Type().VoidPointer();
            encoder.AddParameter().Type().VoidPointer();
            foreach (Type type in types)
            {
                EncodeType(encoder.AddParameter().Type(), type);
            }
        }

        private void EncodeType(SignatureTypeEncoder encoder, Type type)
        {
            if (type == typeof(void*))
            {
                encoder.VoidPointer();
                return;
            }

            if (type.IsPointer)
            {
                EncodeType(encoder.Pointer(), type.GetElementType());
                return;
            }

            if (type.IsSZArray)
            {
                EncodeType(encoder.SZArray(), type.GetElementType());
            }

            if (type.IsGenericType)
            {
                Type[] genericArgs = type.GetGenericArguments();
                GenericTypeArgumentsEncoder argsEncoder = encoder.GenericInstantiation(
                    GetOrCreateTypeRef(type.GetGenericTypeDefinition()),
                    genericArgs.Length,
                    type.IsValueType);

                foreach (Type genericArg in genericArgs)
                {
                    EncodeType(argsEncoder.AddArgument(), genericArg);
                }
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean: encoder.Boolean(); return;
                case TypeCode.Char: encoder.Char(); return;
                case TypeCode.SByte: encoder.SByte(); return;
                case TypeCode.Byte: encoder.Byte(); return;
                case TypeCode.Int16: encoder.Int16(); return;
                case TypeCode.UInt16: encoder.UInt16(); return;
                case TypeCode.Int32: encoder.Int32(); return;
                case TypeCode.UInt32: encoder.UInt32(); return;
                case TypeCode.Int64: encoder.Int64(); return;
                case TypeCode.UInt64: encoder.UInt64(); return;
                case TypeCode.Single: encoder.Single(); return;
                case TypeCode.Double: encoder.Double(); return;
                case TypeCode.String: encoder.String(); return;
            }

            if (type == typeof(IntPtr))
            {
                encoder.IntPtr();
                return;
            }

            if (type == typeof(UIntPtr))
            {
                encoder.UIntPtr();
                return;
            }

            if (type == typeof(TypedReference))
            {
                encoder.PrimitiveType(PrimitiveTypeCode.TypedReference);
                return;
            }

            if (type == typeof(void))
            {
                encoder.PrimitiveType(PrimitiveTypeCode.Void);
                return;
            }

            encoder.Type(GetOrCreateTypeRef(type), type.IsValueType);
        }

        private TypeDefinitionHandle CreatePrimitive128()
        {
            var encoder = new BlobEncoder(new BlobBuilder());
            encoder.FieldSignature().UInt64();

            var ulongFieldSig = _md.GetOrAddBlob(encoder.Builder);

            var firstField = _md.AddFieldDefinition(
                FieldAttributes.Public,
                _md.GetOrAddString("Part0"),
                ulongFieldSig);

            var secondField = _md.AddFieldDefinition(
                FieldAttributes.Public,
                _md.GetOrAddString("Part1"),
                ulongFieldSig);

            var typeDef = _md.AddTypeDefinition(
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit | TypeAttributes.SequentialLayout,
                _md.GetOrAddString("ClrDebug"),
                _md.GetOrAddString("Primitive128"),
                GetOrCreateTypeRef(typeof(ValueType)),
                firstField,
                MetadataTokens.MethodDefinitionHandle(1));

            _md.AddTypeLayout(typeDef, packingSize: 8, size: 0);
            _md.AddFieldLayout(firstField, 0);
            _md.AddFieldLayout(secondField, 8);
            _nextFieldRow += 2;

            return typeDef;
        }

        private EntityHandle GetOrCreateTypeRef(Type type)
        {
            if (type == typeof(Primitive128))
            {
                return _primitive128;
            }

            if (_typeRefs.TryGetValue(type, out TypeReferenceHandle value))
            {
                return value;
            }

            var typeRef = _md.AddTypeReference(
                GetOrCreateAssemblyRef(type.Assembly),
                _md.GetOrAddString(type.Namespace),
                _md.GetOrAddString(type.Name));

            _typeRefs.Add(type, typeRef);
            return typeRef;
        }

        private AssemblyReferenceHandle GetOrCreateAssemblyRef(Assembly assembly)
        {
            if (_assemblyRefs.TryGetValue(assembly, out AssemblyReferenceHandle value))
            {
                return value;
            }

            AssemblyName assemblyName = assembly.GetName();
            if (Array.IndexOf(s_fwdFromStd, assemblyName.Name) != -1)
            {
                _assemblyRefs.Add(assembly, _netstandard);
                return _netstandard;
            }

            byte[] token = assemblyName.GetPublicKeyToken();
            var assemblyRef = _md.AddAssemblyReference(
                _md.GetOrAddString(assemblyName.Name),
                assemblyName.Version,
                assemblyName.CultureName == null ? default : _md.GetOrAddString(assemblyName.CultureName),
                token == null ? default : _md.GetOrAddBlob(token),
                ConvertFlags(assemblyName.Flags),
                default);

            _assemblyRefs.Add(assembly, assemblyRef);
            return assemblyRef;
        }

        private AssemblyFlags ConvertFlags(AssemblyNameFlags source)
        {
            AssemblyFlags flags = default;

            // Names mean very different things, but they do the same thing oddly.
            if ((source & AssemblyNameFlags.EnableJITcompileOptimizer) != 0)
            {
                flags |= AssemblyFlags.DisableJitCompileOptimizer;
            }

            if ((source & AssemblyNameFlags.EnableJITcompileTracking) != 0)
            {
                flags |= AssemblyFlags.EnableJitCompileTracking;
            }

            if ((source & AssemblyNameFlags.PublicKey) != 0)
            {
                flags |= AssemblyFlags.PublicKey;
            }

            if ((source & AssemblyNameFlags.Retargetable) != 0)
            {
                flags |= AssemblyFlags.Retargetable;
            }

            if ((source & AssemblyNameFlags.Retargetable) != 0)
            {
                flags |= AssemblyFlags.Retargetable;
            }

            return flags;
        }

        private struct Primitive128 { }
    }
}
