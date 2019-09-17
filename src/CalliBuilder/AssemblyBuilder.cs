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
            new[] { typeof(Guid), typeof(void*) },
            new[] { typeof(int) },
            new[] { typeof(int), typeof(int) },
            new[] { typeof(int), typeof(void*) },
            new[] { typeof(int), typeof(void*), typeof(void*) },
            new[] { typeof(int), typeof(void*), typeof(uint) },
            new[] { typeof(int), typeof(void*), typeof(uint), typeof(void*), typeof(void*) },
            new[] { typeof(void*) },
            new[] { typeof(void*), typeof(void*) },
            new[] { typeof(void*), typeof(void*), typeof(void*) },
            new[] { typeof(void*), typeof(int) },
            new[] { typeof(void*), typeof(uint), typeof(void*) },
            new[] { typeof(ulong) },
            new[] { typeof(ulong), typeof(void*) },
            new[] { typeof(ulong), typeof(void*), typeof(void*) },
            new[] { typeof(uint), typeof(int) },
            new[] { typeof(uint), typeof(void*) },
            new[] { typeof(uint), typeof(void*), typeof(void*) },
            new[] { typeof(uint), typeof(int), typeof(void*) },

            new[] { typeof(uint), typeof(uint), typeof(uint), typeof(void*), typeof(void*) },
            // ICorDebug.CreateProcess
            new[]
            {
                /* lpApplicationName:    */ typeof(void*),
                /* lpCommandLine:        */ typeof(void*),
                /* lpProcessAttributes:  */ typeof(void*),
                /* lpThreadAttributes:   */ typeof(void*),
                /* bInheritHandles:      */ typeof(int),
                /* dwCreationFlags:      */ typeof(ulong),
                /* lpEnvironment:        */ typeof(void*),
                /* lpCurrentDirectory:   */ typeof(void*),
                /* lpStartupInfo:        */ typeof(void*),
                /* lpProcessInformation: */ typeof(void*),
                /* debuggingFlags:       */ typeof(int),
                /* pProcess:             */ typeof(void*)
            },
        };

        private static readonly Guid s_calliGuid =
            new Guid(0x3A4B8CAC, 0x7DE0, 0x49E6, 0x9B, 0x34, 0xB1, 0x08, 0xA4, 0x8B, 0x78, 0xB7);

        private readonly Dictionary<Type, TypeReferenceHandle> _typeRefs = new Dictionary<Type, TypeReferenceHandle>();

        private MetadataBuilder _md;

        private AssemblyReferenceHandle _netstandard;

        private MethodBodyStreamEncoder _methodBodyStream;

        private string _projectName;

        private string _assemblyName;

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

            TypeReferenceHandle systemObject = GetOrCreateTypeRef(typeof(object));

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

            _md.AddTypeDefinition(
                TypeAttributes.Abstract | TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                _md.GetOrAddString(_projectName),
                _md.GetOrAddString("CalliInstructions"),
                systemObject,
                MetadataTokens.FieldDefinitionHandle(1),
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
                case TypeCode.Object: encoder.Object(); return;
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

        private TypeReferenceHandle GetOrCreateTypeRef(Type type)
        {
            if (_typeRefs.TryGetValue(type, out TypeReferenceHandle value))
            {
                return value;
            }

            var typeRef = _md.AddTypeReference(
                _netstandard,
                _md.GetOrAddString(type.Namespace),
                _md.GetOrAddString(type.Name));

            _typeRefs.Add(type, typeRef);
            return typeRef;
        }
    }
}
