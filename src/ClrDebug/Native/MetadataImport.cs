using System;
using System.Runtime.InteropServices;

using static ClrDebug.Native.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Provides methods for importing and manipulating existing metadata from
    /// a portable executable (PE) file or other source, such as a type library
    /// or a stand-alone, run-time metadata binary.
    /// </summary>
    /// <remarks>
    /// The design of the <c>IMetaDataImport</c> interface is intended primarily
    /// to be used by tools and services that will be importing type information
    /// (for example, development tools) or managing deployed components (for
    /// example, resolution/activation services). The methods in <c>IMetaDataImport</c>
    /// fall into the following task categories:
    ///
    /// - Enumerating collections of items in the metadata scope.
    ///
    /// - Finding an item that has a specific set of characteristics.
    ///
    /// - Getting properties of a specified item.
    ///
    /// - The Get methods are specifically designed to return single-valued properties
    ///   of a metadata item. When the property is a reference to another item, a token
    ///   for that item is returned. Any pointer input type can be NULL to indicate that
    ///   the particular value is not being requested. To obtain properties that are
    ///   essentially collection objects (for example, the collection of interfaces that
    ///   a class implements), use the enumeration methods.
    /// </remarks>
    public unsafe class MetaDataImport : Unknown
    {
        private Vtable** This => (Vtable**)DangerousGetPointer();

        /// <summary>
        /// Closes the enumerator that is identified by the specified handle.
        /// </summary>
        /// <param name="hEnum">
        /// The handle for the enumerator to close.
        /// </param>
        /// <remarks>
        /// The handle specified by <see paramref="hEnum" /> is obtained from a previous
        /// <c>Enum</c>Name call (for example, <c>IMetaDataImport::EnumTypeDefs</c>).
        /// </remarks>
        public void CloseEnum(EnumHandle hEnum)
        {
            Calli(_this, This[0]->CloseEnum, hEnum.ToPointer());
        }

        /// <summary>
        /// Gets the number of elements in the enumeration that was retrieved
        /// by the specified enumerator.
        /// </summary>
        /// <param name="hEnum">
        /// The handle for the enumerator.
        /// </param>
        /// <param name="ulCount">
        /// The number of elements enumerated.
        /// </param>
        /// <remarks>
        /// The handle specified by <see paramref="hEnum" /> is obtained from
        /// a previous <c>EnumName</c> call (for example, <c>IMetaDataImport::EnumTypeDefs</c>).
        /// </remarks>
        public int CountEnum(EnumHandle hEnum, out uint ulCount)
        {
            fixed (void* pulCount = &ulCount)
            {
                return Calli(_this, This[0]->CountEnum, hEnum.ToPointer(), pulCount);
            }
        }

        /// <summary>
        /// Enumerates custom attribute-definition tokens associated with the
        /// specified type or member.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the returned enumerator.
        /// </param>
        /// <param name="tk">
        /// A token for the scope of the enumeration, or zero for all custom attributes.
        /// </param>
        /// <param name="tkType">
        /// A token for the constructor of the type of the attributes to be enumerated,
        /// or <c>null</c> for all types.
        /// </param>
        /// <param name="rCustomAttributes">
        /// An array of custom attribute tokens.
        /// </param>
        /// <param name="pcCustomAttributes">
        /// The actual number of token values returned in <see paramref="rCustomAttributes" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumCustomAttributes</c> returned successfully.
        ///
        /// - <c>S_FALSE</c>: There are no custom attributes to enumerate. In that
        ///   case, <see paramref="pcCustomAttributes" /> is zero.
        /// </returns>
        public int EnumCustomAttributes(
            ref EnumHandle phEnum,
            uint tk,
            uint tkType,
            Span<uint> rCustomAttributes,
            out uint pcCustomAttributes)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prCustomAttributes = rCustomAttributes)
            fixed (void* ppcCustomAttributes = &pcCustomAttributes)
            {
                return Calli(
                    _this,
                    This[0]->EnumCustomAttributes,
                    pphEnum,
                    tk,
                    tkType,
                    prCustomAttributes,
                    rCustomAttributes.Length,
                    ppcCustomAttributes);
            }
        }

        /// <summary>
        /// Enumerates event definition tokens for the specified TypeDef token.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator.
        /// </param>
        /// <param name="td">
        /// The <c>TypeDef</c> token whose event definitions are to be enumerated.
        /// </param>
        /// <param name="rEvents">
        /// The array of returned events.
        /// </param>
        /// <param name="pcEvents">
        /// The actual number of events returned in <see paramref="rEvents".
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumEvents</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no events to enumerate. In that case,
        ///   <see cref="pcEvents" /> is zero.
        /// </returns>
        public int EnumEvents(ref EnumHandle phEnum, uint td, Span<uint> rEvents, out uint pcEvents)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prEvents = rEvents)
            fixed (void* ppcEvents = &pcEvents)
            {
                return Calli(
                    _this,
                    This[0]->EnumEvents,
                    pphEnum,
                    td,
                    prEvents,
                    rEvents.Length,
                    ppcEvents);
            }
        }

        /// <summary>
        /// Enumerates <c>FieldDef</c> tokens for the type referenced by the specified
        /// <c>TypeDef</c> token.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator.
        /// </param>
        /// <param name="cl">
        /// The <c>TypeDef</c> token of the class whose fields are to be enumerated.
        /// </param>
        /// <param name="rFields">
        /// The list of <c>FieldDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The actual number of <c>FieldDef</c> tokens returned in <see paramref="rFields" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumFields</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no fields to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        public int EnumFields(ref EnumHandle phEnum, uint cl, Span<uint> rFields, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prFields = rFields)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumFields,
                    pphEnum,
                    cl,
                    prFields,
                    rFields.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>FieldDef</c> tokens of the specified type with the specified name.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator.
        /// </param>
        /// <param name="cl">
        /// The token of the type whose fields are to be enumerated.
        /// </param>
        /// <param name="szName">
        /// The field name that limits the scope of the enumeration.
        /// </param>
        /// <param name="rFields">
        /// Array used to store the <c>FieldDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The actual number of <c>FieldDef</c> tokens returned in
        /// <see paramref="rFields" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumFieldsWithName</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no fields to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        /// <remarks>
        /// Unlike <c>IMetaDataImport::EnumFields</c>, <c>EnumFieldsWithName</c> discards
        /// all field tokens that do not have the specified name.
        /// </remarks>
        public int EnumFieldsWithName(
            ref EnumHandle phEnum,
            uint cl,
            ReadOnlySpan<char> szName,
            Span<uint> rFields,
            out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* pszName = szName)
            fixed (void* prFields = rFields)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumFieldsWithName,
                    pphEnum,
                    cl,
                    pszName,
                    prFields,
                    rFields.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates all interfaces implemented by the specified <c>TypeDef</c>.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator.
        /// </param>
        /// <param name="td">
        /// The token of the <c>TypeDef</c> whose <c>MethodDef</c> tokens representing
        /// interface implementations are to be enumerated.
        /// </param>
        /// <param name="rImpls">
        /// The array used to store the <c>MethodDef</c> tokens.
        /// </param>
        /// <param name="pcImpls">
        /// The actual number of tokens returned in <see paramref="rImpls" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumInterfaceImpls</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no MethodDef tokens to enumerate. In that case,
        ///   <see paramref="pcImpls" /> is set to zero.
        /// </returns>
        /// <remarks>
        /// The enumeration returns a collection of <c>mdInterfaceImpl</c> tokens
        /// for each interface implemented by the specified <c>TypeDef</c>. Interface
        /// tokens are returned in the order the interfaces were specified (through
        /// <c>DefineTypeDef</c> or <c>SetTypeDefProps</c>). Properties of the returned
        /// <c>mdInterfaceImpl</c> tokens can be queried using <c>GetInterfaceImplProps</c>.
        /// </remarks>
        public int EnumInterfaceImpls(ref EnumHandle phEnum, uint td, Span<uint> rImpls, out uint pcImpls)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prImpls = rImpls)
            fixed (void* ppcImpls = &pcImpls)
            {
                return Calli(
                    _this,
                    This[0]->EnumInterfaceImpls,
                    pphEnum,
                    td,
                    prImpls,
                    rImpls.Length,
                    ppcImpls);
            }
        }

        /// <summary>
        /// Enumerates <c>MemberRef</c> tokens representing members of the specified type.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator.
        /// </param>
        /// <param name="tkParent">
        /// A <c>TypeDef</c>, <c>TypeRef</c>, <c>MethodDef</c>, or <c>ModuleRef</c> token
        /// for the type whose members are to be enumerated.
        /// </param>
        /// <param name="rMemberRefs">
        /// The array used to store <c>MemberRef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The actual number of MemberRef tokens returned in <see paramref="rMemberRefs" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumMemberRefs</c> returned successfully.|
        /// - <c>S_FALSE</c>: There are no <c>MemberRef</c> tokens to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is to zero.
        /// </returns>
        public int EnumMemberRefs(ref EnumHandle phEnum, uint tkParent, Span<uint> rMemberRefs, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prMemberRefs = rMemberRefs)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumMemberRefs,
                    pphEnum,
                    tkParent,
                    prMemberRefs,
                    rMemberRefs.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>MemberDef</c> tokens representing members of the specified type.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator.
        /// </param>
        /// <param name="cl">
        /// A <c>TypeDef</c> token representing the type whose members are to be enumerated.
        /// </param>
        /// <param name="rMembers">
        /// The array used to hold the <c>MemberDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The actual number of <c>MemberDef</c> tokens returned in <see paramref="rMembers" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumMembers</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no <c>MemberDef</c> tokens to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        /// <remarks>
        /// When enumerating collections of members for a class, <c>EnumMembers</c> returns
        /// only members (fields and methods, but not properties or events) defined directly
        /// on the class. It does not return any members that the class inherits, even if the
        /// class provides an implementation for those inherited members. To enumerate
        /// inherited members, the caller must explicitly walk the inheritance chain. Note
        /// that the rules for the inheritance chain may vary depending on the language or
        /// compiler that emitted the original metadata.
        ///
        /// Properties and events are not enumerated by <c>EnumMembers</c>. To enumerate those,
        /// use <c>EnumProperties</c> or <c>EnumEvents</c>.
        /// </remarks>
        public int EnumMembers(ref EnumHandle phEnum, uint cl, Span<uint> rMembers, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prMembers = rMembers)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumMembers,
                    pphEnum,
                    cl,
                    prMembers,
                    rMembers.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>MemberDef</c> tokens representing members of the specified
        /// type with the specified name.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator.
        /// </param>
        /// <param name="cl">
        /// A <c>TypeDef</c> token representing the type with members to enumerate.
        /// </param>
        /// <param name="szName">
        /// The member name that limits the scope of the enumerator.
        /// </param>
        /// <param name="rMembers">
        /// The array used to store the <c>MemberDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The actual number of <c>MemberDef</c> tokens returned in <see paramref="rMembers" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumTypeDefs</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no <c>MemberDef</c> tokens to enumerate. In that
        /// case, <see paramref="pcTokens" /> is zero.
        /// </returns>
        /// <remarks>
        /// This method enumerates fields and methods, but not properties or events. Unlike IMetaDataImport::EnumMembers, <c>EnumMembersWithName</c> discards all field and member tokens that do not have the specified name.
        /// </remarks>
        public int EnumMembersWithName(
            ref EnumHandle phEnum,
            uint cl,
            Span<char> szName,
            Span<uint> rMembers,
            out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* pszName = szName)
            fixed (void* prMembers = rMembers)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumMembersWithName,
                    pphEnum,
                    cl,
                    pszName,
                    prMembers,
                    rMembers.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>MethodBody</c> and <c>MethodDeclaration</c> tokens representing
        /// methods of the specified type.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first call of
        /// this method.
        /// </param>
        /// <param name="td">
        /// A <c>TypeDef</c> token for the type whose method implementations to enumerate.
        /// </param>
        /// <param name="rMethodBody">
        /// The array to store the <c>MethodBody</c> tokens.
        /// </param>
        /// <param name="rMethodDecl">
        /// The array to store the <c>MethodDeclaration</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The actual number of methods returned in <see paramref="rMethodBody" /> and <see paramref="rMethodDecl" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumMethodImpls</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no method tokens to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        public int EnumMethodImpls(ref EnumHandle phEnum, uint td, Span<uint> rMethodBody, Span<uint> rMethodDecl, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prMethodBody = rMethodBody)
            fixed (void* prMethodDecl = rMethodDecl)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumMethodImpls,
                    pphEnum,
                    td,
                    prMethodBody,
                    prMethodDecl,
                    rMethodBody.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>MethodDef</c> tokens representing methods of the
        /// specified type.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="cl">
        /// A <c>TypeDef</c> token representing the type with the methods to enumerate.
        /// </param>
        /// <param name="rMethods">
        /// The array to store the <c>MethodDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The number of MethodDef tokens returned in <see paramref="rMethods" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumMethods</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no <c>MethodDef</c> tokens to enumerate. In that
        ///   case, <see paramref="pcTokens" /> is zero.
        /// </returns>
        public int EnumMethods(ref EnumHandle phEnum, uint cl, Span<uint> rMethods, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prMethods = rMethods)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumMethods,
                    pphEnum,
                    cl,
                    prMethods,
                    rMethods.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates the properties and the property-change events to which the
        /// specified method is related.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="mb">
        /// A <c>MethodDef</c> token that limits the scope of the enumeration.
        /// </param>
        /// <param name="rEventProp">
        /// The array used to store the events or properties.
        /// </param>
        /// <param name="pcEventProp">
        /// The number of events or properties returned in <see paramref="rEventProp" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumMethodSemantics</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no events or properties to enumerate. In that case,
        ///   <see paramref="pcEventProp" /> is zero.
        /// </returns>
        /// <remarks>
        /// Many common language runtime types define Property<c>Changed</c> events and
        /// <c>OnPropertyChanged</c> methods related to their properties. For example,
        /// the <c>System.Windows.Forms.Control</c> type defines a <c>System.Windows.Forms.Control.Font</c>
        /// property, a <c>System.Windows.Forms.Control.FontChanged</c> event, and an
        /// <c>System.Windows.Forms.Control.OnFontChanged</c> method. The set accessor
        /// method of the <c>System.Windows.Forms.Control.Font</c> property calls
        /// <c>System.Windows.Forms.Control.OnFontChanged</c> method, which in turn raises
        /// the <c>System.Windows.Forms.Control.FontChanged</c> event. You would call
        /// <c>EnumMethodSemantics</c> using the MethodDef for <c>System.Windows.Forms.Control.OnFontChanged</c>
        /// to get references to the <c>System.Windows.Forms.Control.Font</c> property and
        /// the <c>System.Windows.Forms.Control.FontChanged</c> event.
        /// </remarks>
        public int EnumMethodSemantics(ref EnumHandle phEnum, uint mb, Span<uint> rEventProp, out uint pcEventProp)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prEventProp = rEventProp)
            fixed (void* ppcEventProp = &pcEventProp)
            {
                return Calli(
                    _this,
                    This[0]->EnumMethodSemantics,
                    pphEnum,
                    mb,
                    prEventProp,
                    rEventProp.Length,
                    ppcEventProp);
            }
        }

        /// <summary>
        /// Enumerates methods that have the specified name and that are defined by
        /// the type referenced by the specified <c>TypeDef</c> token.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first call of this method.
        /// </param>
        /// <param name="cl">
        /// A <c>TypeDef</c> token representing the type whose methods to enumerate.
        /// </param>
        /// <param name="szName">
        /// The name that limits the scope of the enumeration.
        /// </param>
        /// <param name="rMethods">
        /// The array used to store the <c>MethodDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The number of <c>MethodDef</c> tokens returned in <see paramref="rMethods" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumMethodsWithName</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        /// <remarks>
        /// This method enumerates fields and methods, but not properties or events.
        /// Unlike <c>IMetaDataImport::EnumMethods</c>, <c>EnumMethodsWithName</c> discards
        /// all method tokens that do not have the specified name.
        /// </remarks>
        public int EnumMethodsWithName(ref EnumHandle phEnum, uint cl, Span<char> szName, Span<uint> rMethods, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* pszName = szName)
            fixed (void* prMethods = rMethods)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumMethodsWithName,
                    pphEnum,
                    cl,
                    pszName,
                    prMethods,
                    rMethods.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>ModuleRef</c> tokens that represent imported modules.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first call of this method.
        /// </param>
        /// <param name="rModuleRefs">
        /// The array used to store the <c>ModuleRef</c> tokens.
        /// </param>
        /// <param name="pcModuleRefs">
        /// The number of <c>ModuleRef</c> tokens returned in <see paramref="rModuleRefs" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumModuleRefs</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcModuleRefs" /> is zero.
        /// </returns>
        public int EnumModuleRefs(ref EnumHandle phEnum, Span<uint> rModuleRefs, out uint pcModuleRefs)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prModuleRefs = rModuleRefs)
            fixed (void* ppcModuleRefs = &pcModuleRefs)
            {
                return Calli(
                    _this,
                    This[0]->EnumModuleRefs,
                    pphEnum,
                    prModuleRefs,
                    rModuleRefs.Length,
                    ppcModuleRefs);
            }
        }

        /// <summary>
        /// Enumerates <c>ParamDef</c> tokens representing the parameters of the method
        /// referenced by the specified <c>MethodDef</c> token.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first call of this method.
        /// </param>
        /// <param name="mb">
        /// A <c>MethodDef</c> token representing the method with the parameters to enumerate.
        /// </param>
        /// <param name="rParams">
        /// The array used to store the <c>ParamDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The number of <c>ParamDef</c> tokens returned in <see paramref="rParams" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumParams</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        public int EnumParams(ref EnumHandle phEnum, uint mb, Span<uint> rParams, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prParams = rParams)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumParams,
                    pphEnum,
                    mb,
                    prParams,
                    rParams.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates permissions for the objects in a specified metadata scope.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="tk">
        /// A metadata token that limits the scope of the search, or <c>NULL</c> to search
        /// the widest scope possible.
        /// </param>
        /// <param name="dwActions">
        /// Flags representing the <c>System.Security.Permissions.SecurityAction</c> values
        /// to include in <see paramref="rPermission" />, or zero to return all actions.
        /// </param>
        /// <param name="rPermission">
        /// The array used to store the Permission tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The number of Permission tokens returned in <see paramref="rPermission" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumPermissionSets</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        public int EnumPermissionSets(ref EnumHandle phEnum, uint tk, uint dwActions, Span<uint> rPermission, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prPermission = rPermission)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumPermissionSets,
                    pphEnum,
                    tk,
                    dwActions,
                    prPermission,
                    rPermission.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>PropertyDef</c> tokens representing the properties of the type
        /// referenced by the specified <c>TypeDef</c> token.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first call of this method.
        /// </param>
        /// <param name="td">
        /// A <c>TypeDef</c> token representing the type with properties to enumerate.
        /// </param>
        /// <param name="rProperties">
        /// The array used to store the <c>PropertyDef</c> tokens.
        /// </param>
        /// <param name="pcProperties">
        /// The number of <c>PropertyDef</c> tokens returned in <see paramref="rProperties" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumProperties</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcProperties" /> is zero.
        /// </returns>
        public int EnumProperties(ref EnumHandle phEnum, uint td, Span<uint> rProperties, out uint pcProperties)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prProperties = rProperties)
            fixed (void* ppcProperties = &pcProperties)
            {
                return Calli(
                    _this,
                    This[0]->EnumProperties,
                    pphEnum,
                    td,
                    prProperties,
                    rProperties.Length,
                    ppcProperties);
            }
        }

        /// <summary>
        /// Enumerates Signature tokens representing stand-alone signatures
        /// in the current scope.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="rSignatures">
        /// The array used to store the <c>Signature</c> tokens.
        /// </param>
        /// <param name="pcSignatures">
        /// The number of <c>Signature</c> tokens returned in <see paramref="rSignatures" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumSignatures</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcSignatures" /> is zero.
        /// </returns>
        /// <remarks>
        /// The Signature tokens are created by the <c>IMetaDataEmit::GetTokenFromSig</c>
        /// method.
        /// </remarks>
        public int EnumSignatures(ref EnumHandle phEnum, Span<uint> rSignatures, out uint pcSignatures)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prSignatures = rSignatures)
            fixed (void* ppcSignatures = &pcSignatures)
            {
                return Calli(
                    _this,
                    This[0]->EnumSignatures,
                    pphEnum,
                    prSignatures,
                    rSignatures.Length,
                    ppcSignatures);
            }
        }

        /// <summary>
        /// Enumerates <c>TypeDef</c> tokens representing all types within the current scope.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the new enumerator. This must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="rTypeDefs">
        /// The array used to store the <c>TypeDef</c> tokens.
        /// </param>
        /// <param name="pcTypeDefs">
        /// The number of <c>TypeDef</c> tokens returned in <see paramref="rTypeDefs" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumTypeDefs</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcTypeDefs" /> is zero.
        /// </returns>
        /// <remarks>
        /// The <c>TypeDef</c> token represents a type such as a class or an interface,
        /// as well as any type added via an extensibility mechanism.
        /// </remarks>
        public int EnumTypeDefs(ref EnumHandle phEnum, Span<uint> rTypeDefs, out uint pcTypeDefs)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prTypeDefs = rTypeDefs)
            fixed (void* ppcTypeDefs = &pcTypeDefs)
            {
                return Calli(
                    _this,
                    This[0]->EnumTypeDefs,
                    pphEnum,
                    prTypeDefs,
                    rTypeDefs.Length,
                    ppcTypeDefs);
            }
        }

        /// <summary>
        /// Enumerates <c>TypeRef</c> tokens defined in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first call of this method.
        /// </param>
        /// <param name="rTypeRefs">
        /// The array used to store the <c>TypeRef</c> tokens.
        /// </param>
        /// <param name="pcTypeRefs">
        /// A pointer to the number of <c>TypeRef</c> tokens returned in <see paramref="rTypeRefs" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumTypeRefs</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcTypeRefs" /> is zero.
        /// </returns>
        /// <remarks>
        /// A <c>TypeRef</c> token represents a reference to a type.
        /// </remarks>
        public int EnumTypeRefs(ref EnumHandle phEnum, Span<uint> rTypeRefs, out uint pcTypeRefs)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prTypeRefs = rTypeRefs)
            fixed (void* ppcTypeRefs = &pcTypeRefs)
            {
                return Calli(
                    _this,
                    This[0]->EnumTypeRefs,
                    pphEnum,
                    prTypeRefs,
                    rTypeRefs.Length,
                    ppcTypeRefs);
            }
        }

        /// <summary>
        /// Enumerates <c>TypeSpec</c> tokens defined in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This value must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="rTypeSpecs">
        /// The array used to store the <c>TypeSpec</c> tokens.
        /// </param>
        /// <param name="pcTypeSpecs">
        /// The number of <c>TypeSpec</c> tokens returned in <see paramref="rTypeSpecs" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumTypeSpecs</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcTypeSpecs" /> is zero.
        /// </returns>
        /// <remarks>
        /// The TypeSpec tokens are created by the IMetaDataEmit::GetTokenFromTypeSpec method.
        /// </remarks>
        public int EnumTypeSpecs(ref EnumHandle phEnum, Span<uint> rTypeSpecs, out uint pcTypeSpecs)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prTypeSpecs = rTypeSpecs)
            fixed (void* ppcTypeSpecs = &pcTypeSpecs)
            {
                return Calli(
                    _this,
                    This[0]->EnumTypeSpecs,
                    pphEnum,
                    prTypeSpecs,
                    rTypeSpecs.Length,
                    ppcTypeSpecs);
            }
        }

        /// <summary>
        /// Enumerates <c>MemberDef</c> tokens representing the unresolved methods
        /// in the current metadata scope.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="rMethods">
        /// The array used to store the <c>MemberDef</c> tokens.
        /// </param>
        /// <param name="pcTokens">
        /// The number of <c>MemberDef</c> tokens returned in <see paramref="rMethods" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumUnresolvedMethods</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcTokens" /> is zero.
        /// </returns>
        /// <remarks>
        /// An unresolved method is one that has been declared but not implemented.
        /// A method is included in the enumeration if the method is marked
        /// <c>miForwardRef</c> and either <c>mdPinvokeImpl</c> or <c>miRuntime</c>
        /// is set to zero. In other words, an unresolved method is a class method
        /// that is marked <c>miForwardRef</c> but which is not implemented in unmanaged
        /// code (reached via PInvoke) nor implemented internally by the runtime itself
        ///
        /// The enumeration excludes all methods that are defined either at module scope
        /// (globals) or in interfaces or abstract classes.
        /// </remarks>
        public int EnumUnresolvedMethods(ref EnumHandle phEnum, Span<uint> rMethods, out uint pcTokens)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prMethods = rMethods)
            fixed (void* ppcTokens = &pcTokens)
            {
                return Calli(
                    _this,
                    This[0]->EnumUnresolvedMethods,
                    pphEnum,
                    prMethods,
                    rMethods.Length,
                    ppcTokens);
            }
        }

        /// <summary>
        /// Enumerates <c>String</c> tokens representing hard-coded strings in
        /// the current metadata scope.
        /// </summary>
        /// <param name="phEnum">
        /// A pointer to the enumerator. This must be <c>NULL</c> for the first
        /// call of this method.
        /// </param>
        /// <param name="rStrings">
        /// The array used to store the <c>String</c> tokens.
        /// </param>
        /// <param name="pcStrings">
        /// The number of <c>String</c> tokens returned in <see paramref="rStrings" />.
        /// </param>
        /// <returns>
        /// - <c>S_OK</c>: <c>EnumUserStrings</c> returned successfully.
        /// - <c>S_FALSE</c>: There are no tokens to enumerate. In that case,
        ///   <see paramref="pcStrings" /> is zero.
        /// </returns>
        /// <remarks>
        /// The <c>String</c> tokens are created by the <c>IMetaDataEmit::DefineUserString</c>
        /// method. This method is designed to be used by a metadata browser
        /// rather than by a compiler.
        /// </remarks>
        public int EnumUserStrings(ref EnumHandle phEnum, Span<uint> rStrings, out uint pcStrings)
        {
            fixed (void* pphEnum = &phEnum)
            fixed (void* prStrings = rStrings)
            fixed (void* ppcStrings = &pcStrings)
            {
                return Calli(
                    _this,
                    This[0]->EnumUserStrings,
                    pphEnum,
                    prStrings,
                    rStrings.Length,
                    ppcStrings);
            }
        }

        /// <summary>
        /// Gets a pointer to the <c>FieldDef</c> token for the field that
        /// is enclosed by the specified <c>System.Type</c> and that has the
        /// specified name and metadata signature.
        /// </summary>
        /// <param name="td">
        /// The <c>TypeDef</c> token for the class or interface that encloses
        /// the field to search for. If this value is <c>mdTokenNil</c>, the
        /// lookup is done for a global variable.
        /// </param>
        /// <param name="szName">
        /// The name of the field to search for.
        /// </param>
        /// <param name="pvSigBlob">
        /// A pointer to the binary metadata signature of the field.
        /// </param>
        /// <param name="pmb">
        /// A pointer to the matching <c>FieldDef</c> token.
        /// </param>
        /// <remarks>
        /// You specify the field using its enclosing class or interface (<see paramref="td" />),
        /// its name (<see paramref="szName" />), and optionally its signature (<see paramref="pvSigBlob" />).
        ///
        /// The signature passed to <c>FindField</c> must have been generated in the
        /// current scope, because signatures are bound to a particular scope. A
        /// signature can embed a token that identifies the enclosing class or value
        /// type. (The token is an index into the local <c>TypeDef</c> table). You cannot
        /// build a run-time signature outside the context of the current scope and use
        /// that signature as input to <c>FindField</c>.
        ///
        /// <c>FindField</c> finds only fields that were defined directly in the class
        /// or interface; it does not find inherited fields.
        /// </remarks>
        public int FindField(
            uint td,
            ReadOnlySpan<char> szName,
            ReadOnlySpan<byte> pvSigBlob,
            out uint pmb)
        {
            fixed (void* pszName = szName)
            fixed (void* ppvSigBlob = pvSigBlob)
            fixed (void* ppmb = &pmb)
            {
                return Calli(
                    _this,
                    This[0]->FindField,
                    td,
                    pszName,
                    ppvSigBlob,
                    pvSigBlob.Length,
                    ppmb);
            }
        }

        /// <summary>
        /// Gets a pointer to the <c>MemberDef</c> token for field or method that is
        /// enclosed by the specified <c>System.Type</c> and that has the specified
        /// name and metadata signature.
        /// </summary>
        /// <param name="td">
        /// The <c>TypeDef</c> token for the class or interface that encloses the member
        /// to search for. If this value is <c>mdTokenNil</c>, the lookup is done for a
        /// global-variable or global-function.
        /// </param>
        /// <param name="szName">
        /// The name of the member to search for.
        /// </param>
        /// <param name="pvSigBlob">
        /// A pointer to the binary metadata signature of the member.
        /// </param>
        /// <param name="pmb">
        /// A pointer to the matching <c>MemberDef</c> token.
        /// </param>
        /// <remarks>
        /// You specify the member using its enclosing class or interface (<see paramref="td" />),
        /// its name (<see paramref="szName" />), and optionally its signature (<see paramref="pvSigBlob" />).
        /// There might be multiple members with the same name in a class or interface.
        /// In that case, pass the member's signature to find the unique match.
        ///
        /// The signature passed to <c>FindMember</c> must have been generated in the
        /// current scope, because signatures are bound to a particular scope. A
        /// signature can embed a token that identifies the enclosing class or value
        /// type. The token is an index into the local <c>TypeDef</c> table. You cannot
        /// build a run-time signature outside the context of the current scope and use
        /// that signature as input to input to <c>FindMember</c>.
        ///
        /// <c>FindMember</c> finds only members that were defined directly in the class
        /// or interface; it does not find inherited members.
        ///
        /// NOTE:
        ///
        /// <c>FindMember</c> is a helper method. It calls <c>IMetaDataImport::FindMethod</c>;
        /// if that call does not find a match, <c>FindMember</c> then calls
        /// <c>IMetaDataImport::FindField</c>.
        /// </remarks>
        public int FindMember(uint td, ReadOnlySpan<char> szName, ReadOnlySpan<byte> pvSigBlob, out uint pmb)
        {
            fixed (void* pszName = szName)
            fixed (void* ppvSigBlob = pvSigBlob)
            fixed (void* ppmb = &pmb)
            {
                return Calli(
                    _this,
                    This[0]->FindMember,
                    td,
                    pszName,
                    ppvSigBlob,
                    pvSigBlob.Length,
                    ppmb);
            }
        }

        /// <summary>
        /// Gets a pointer to the <c>MemberRef</c> token for the member reference
        /// that is enclosed by the specified <c>System.Type</c> and that has the
        /// specified name and metadata signature.
        /// </summary>
        /// <param name="td">
        /// The <c>TypeRef</c> token for the class or interface that encloses the
        /// member reference to search for. If this value is <c>mdTokenNil</c>, the
        /// lookup is done for a global variable or a global-function reference.
        /// </param>
        /// <param name="szName">
        /// The name of the member reference to search for.
        /// </param>
        /// <param name="pvSigBlob">
        /// A pointer to the binary metadata signature of the member reference.
        /// </param>
        /// <param name="pmr">
        /// A pointer to the matching <c>MemberRef</c> token.
        /// </param>
        /// <remarks>
        /// You specify the member using its enclosing class or interface (<see paramref="td" />),
        /// its name (<see paramref="szName" />), and optionally its signature (<see paramref="pvSigBlob" />).
        ///
        /// The signature passed to <c>FindMemberRef</c> must have been generated in
        /// the current scope, because signatures are bound to a particular scope.
        /// A signature can embed a token that identifies the enclosing class or value
        /// type. The token is an index into the local <c>TypeDef</c> table. You cannot
        /// build a run-time signature outside the context of the current scope and use
        /// that signature as input to <c>FindMemberRef</c>.
        ///
        /// <c>FindMemberRef</c> finds only member references that were defined directly
        /// in the class or interface; it does not find inherited member references.
        /// </remarks>
        public int FindMemberRef(uint td, ReadOnlySpan<char> szName, ReadOnlySpan<byte> pvSigBlob, out uint pmr)
        {
            fixed (void* pszName = szName)
            fixed (void* ppvSigBlob = pvSigBlob)
            fixed (void* ppmr = &pmr)
            {
                return Calli(
                    _this,
                    This[0]->FindMemberRef,
                    td,
                    pszName,
                    ppvSigBlob,
                    pvSigBlob.Length,
                    ppmr);
            }
        }

        /// <summary>
        /// Gets a pointer to the <c>MethodDef</c> token for the method that is
        /// enclosed by the specified System.Type and that has the specified name
        /// and metadata signature.
        /// </summary>
        /// <param name="td">
        /// The <c>mdTypeDef</c> token for the type (a class or interface) that
        /// encloses the member to search for. If this value is <c>mdTokenNil</c>,
        /// then the lookup is done for a global function.
        /// </param>
        /// <param name="szName">
        /// The name of the method to search for.
        /// </param>
        /// <param name="pvSigBlob">
        /// A pointer to the binary metadata signature of the method.
        /// </param>
        /// <param name="pmb">
        /// A pointer to the matching <c>MethodDef</c> token.
        /// </param>
        /// <remarks>
        /// You specify the method using its enclosing class or interface (<see paramref="td" />),
        /// its name (<see paramref="szName" />), and optionally its signature (<see paramref="pvSigBlob" />).
        /// There might be multiple methods with the same name in a class or interface.
        /// In that case, pass the method's signature to find the unique match.
        ///
        /// The signature passed to <c>FindMethod</c> must have been generated in the
        /// current scope, because signatures are bound to a particular scope. A signature
        /// can embed a token that identifies the enclosing class or value type. The token
        /// is an index into the local <c>TypeDef</c> table. You cannot build a run-time
        /// signature outside the context of the current scope and use that signature as
        /// input to input to <c>FindMethod</c>.
        ///
        /// <c>FindMethod</c> finds only methods that were defined directly in the class or
        /// interface; it does not find inherited methods.
        /// </remarks>
        public int FindMethod(uint td, ReadOnlySpan<char> szName, ReadOnlySpan<byte> pvSigBlob, out uint pmb)
        {
            fixed (void* pszName = szName)
            fixed (void* ppvSigBlob = pvSigBlob)
            fixed (void* ppmb = &pmb)
            {
                return Calli(_this, This[0]->FindMethod, td, pszName, ppvSigBlob, pvSigBlob.Length, ppmb);
            }
        }

        /// <summary>
        /// Gets a pointer to the <c>TypeDef</c> metadata token for the <c>System.Type</c>
        /// with the specified name.
        /// </summary>
        /// <param name="szTypeDef">
        /// The name of the type for which to get the <c>TypeDef</c> token.
        /// </param>
        /// <param name="tkEnclosingClass">
        /// A <c>TypeDef</c> or <c>TypeRef</c> token representing the enclosing class.
        /// If the type to find is not a nested class, set this value to <c>NULL</c>.
        /// </param>
        /// <param name="ptd">
        /// A pointer to the matching <c>TypeDef</c> token.
        /// </param>
        public int FindTypeDefByName(ReadOnlySpan<char> szTypeDef, uint tkEnclosingClass, out uint ptd)
        {
            fixed (void* pszTypeDef = szTypeDef)
            fixed (void* pptd = &ptd)
            {
                return Calli(_this, This[0]->FindTypeDefByName, pszTypeDef, tkEnclosingClass, pptd);
            }
        }

        /// <summary>
        /// Gets a pointer to the <c>TypeRef</c> token for the <c>System.Type</c> reference
        /// that is in the specified scope and that has the specified name.
        /// </summary>
        /// <param name="tkResolutionScope">
        /// A <c>ModuleRef</c>, <c>AssemblyRef</c>, or <c>TypeRef</c> token that specifies
        /// the module, assembly, or type, respectively, in which the type reference is defined.
        /// </param>
        /// <param name="szName">
        /// The name of the type reference to search for.
        /// </param>
        /// <param name="ptr">
        /// A pointer to the matching <c>TypeRef</c> token.
        /// </param>
        public int FindTypeRef(uint tkResolutionScope, ReadOnlySpan<char> szName, out uint ptr)
        {
            fixed (void* pszName = szName)
            fixed (void* pptr = &ptr)
            {
                return Calli(_this, This[0]->FindTypeRef, tkResolutionScope, pszName, pptr);
            }
        }

        /// <summary>
        /// Gets layout information for the class referenced by the specified
        /// <c>TypeDef</c> token.
        /// </summary>
        /// <param name="td">
        /// The <c>TypeDef</c> token for the class with the layout to return.
        /// </param>
        /// <param name="pdwPackSize">
        /// One of the values 1, 2, 4, 8, or 16, representing the pack size of the class.
        /// </param>
        /// <param name="rFieldOffset">
        /// An array of <c>COR_FIELD_OFFSET</c> values.
        /// </param>
        /// <param name="pcFieldOffset">
        /// The number of elements returned in <see paramref="rFieldOffset" />.
        /// </param>
        /// <param name="pulClassSize">
        /// The size in bytes of the class represented by <see paramref="td" />.
        /// </param>
        public int GetClassLayout(
            uint td,
            out uint pdwPackSize,
            Span<COR_FIELD_OFFSET> rFieldOffset,
            out uint pcFieldOffset,
            out uint pulClassSize)
        {
            fixed (void* ppdwPackSize = &pdwPackSize)
            fixed (void* prFieldOffset = rFieldOffset)
            fixed (void* ppcFieldOffset = &pcFieldOffset)
            fixed (void* ppulClassSize = &pulClassSize)
            {
                return Calli(
                    _this,
                    This[0]->GetClassLayout,
                    td,
                    ppdwPackSize,
                    prFieldOffset,
                    rFieldOffset.Length,
                    ppcFieldOffset,
                    ppulClassSize);
            }
        }

        /// <summary>
        /// Gets the custom attribute, given its name and owner.
        /// </summary>
        /// <param name="tkObj">
        /// A metadata token representing the object that owns the custom attribute.
        /// </param>
        /// <param name="szName">
        /// The name of the custom attribute.
        /// </param>
        /// <param name="ppData">
        /// A pointer to an array of data that is the value of the custom attribute.
        /// </param>
        /// <param name="pcbData">
        /// The size in bytes of the data returned in <see paramref="ppData" />.
        /// </param>
        /// <remarks>
        /// It is legal to define multiple custom attributes for the same owner;
        /// they may even have the same name. However, <c>GetCustomAttributeByName</c>
        /// returns only one instance. (<c>GetCustomAttributeByName</c> returns the
        /// first instance that it encounters.) To find all instances of a custom
        /// attribute, call the <c>IMetaDataImport::EnumCustomAttributes</c> method.
        /// </remarks>
        public int GetCustomAttributeByName(uint tkObj, ReadOnlySpan<char> szName, out IntPtr ppData, out uint pcbData)
        {
            fixed (void* pszName = szName)
            fixed (void* pppData = &ppData)
            fixed (void* ppcbData = &pcbData)
            {
                return Calli(_this, This[0]->GetCustomAttributeByName, tkObj, pszName, pppData, ppcbData);
            }
        }

        /// <summary>
        /// Gets the value of the custom attribute, given its metadata token.
        /// </summary>
        /// <param name="cv">
        /// A metadata token that represents the custom attribute to be retrieved.
        /// </param>
        /// <param name="ptkObj">
        /// A metadata token representing the object that the custom attribute
        /// modifies. This value can be any type of metadata token except <c>mdCustomAttribute</c>.
        /// </param>
        /// <param name="ptkType">
        /// An <c>mdMethodDef</c> or <c>mdMemberRef</c> metadata token representing
        /// the <c>System.Type</c> of the returned custom attribute.
        /// </param>
        /// <param name="ppBlob">
        /// A pointer to an array of data that is the value of the custom attribute.
        /// </param>
        /// <param name="pcbSize">
        /// The size in bytes of the data returned in <see paramref="ppBlob" />.
        /// </param>
        /// <remarks>
        /// A custom attribute is stored as an array of data, the format which
        /// is understood by the metadata engine.
        /// </remarks>
        public int GetCustomAttributeProps(
            uint cv,
            Span<uint> ptkObj,
            Span<uint> ptkType,
            Span<byte> ppBlob,
            out uint pcbSize)
        {
            fixed (void* pptkObj = ptkObj)
            fixed (void* pptkType = ptkType)
            fixed (void* pppBlob = ppBlob)
            fixed (void* ppcbSize = &pcbSize)
            {
                return Calli(
                    _this,
                    This[0]->GetCustomAttributeProps,
                    cv,
                    pptkObj,
                    pptkType,
                    pppBlob,
                    ppcbSize);
            }
        }

        /// <summary>
        /// Gets metadata information for the event represented by the specified
        /// event token, including the declaring type, the add and remove methods
        /// for delegates, and any flags and other associated data.
        /// </summary>
        /// <param name="ev">
        /// The event metadata token representing the event to get metadata for.
        /// </param>
        /// <param name="pClass">
        /// A pointer to the <c>TypeDef</c> token representing the class that declares the event.
        /// </param>
        /// <param name="szEvent">
        /// The name of the event referenced by <see paramref="ev" />.
        /// </param>
        /// <param name="pchEvent">
        /// The requested length in wide characters of <see paramref="szEvent" />.
        /// </param>
        /// <param name="pdwEventFlags">
        /// The returned length in wide characters of <see paramref="szEvent" />.
        /// </param>
        /// <param name="ptkEventType">
        /// A pointer to a <c>TypeRef</c> or <c>TypeDef</c> metadata token representing
        /// the <c>System.Delegate</c> type of the event.
        /// </param>
        /// <param name="pmdAddOn">
        /// A pointer to the metadata token representing the method that adds handlers
        /// for the event.
        /// </param>
        /// <param name="pmdRemoveOn">
        /// A pointer to the metadata token representing the method that removes handlers
        /// for the event.
        /// </param>
        /// <param name="pmdFire">
        /// A pointer to the metadata token representing the method that raises the event.
        /// </param>
        /// <param name="rmdOtherMethod">
        /// An array of token pointers to other methods associated with the event.
        /// </param>
        /// <param name="pcOtherMethod">
        /// The number of tokens returned in <see paramref="rmdOtherMethod" />.
        /// </param>
        public int GetEventProps(
            uint ev,
            out uint pClass,
            Span<char> szEvent,
            out uint pchEvent,
            out uint pdwEventFlags,
            out uint ptkEventType,
            out uint pmdAddOn,
            out uint pmdRemoveOn,
            out uint pmdFire,
            Span<uint> rmdOtherMethod,
            out uint pcOtherMethod)
        {
            fixed (void* ppClass = &pClass)
            fixed (void* pszEvent = szEvent)
            fixed (void* ppchEvent = &pchEvent)
            fixed (void* ppdwEventFlags = &pdwEventFlags)
            fixed (void* pptkEventType = &ptkEventType)
            fixed (void* ppmdAddOn = &pmdAddOn)
            fixed (void* ppmdRemoveOn = &pmdRemoveOn)
            fixed (void* ppmdFire = &pmdFire)
            fixed (void* prmdOtherMethod = rmdOtherMethod)
            fixed (void* ppcOtherMethod = &pcOtherMethod)
            {
                return Calli(
                    _this,
                    This[0]->GetEventProps,
                    ev,
                    ppClass,
                    pszEvent,
                    ppchEvent,
                    ppdwEventFlags,
                    pptkEventType,
                    ppmdAddOn,
                    ppmdRemoveOn,
                    ppmdFire,
                    prmdOtherMethod,
                    rmdOtherMethod.Length,
                    ppcOtherMethod);
            }
        }

        /// <summary>
        /// Gets a pointer to the native, unmanaged type of the field represented
        /// by the specified field metadata token.
        /// </summary>
        /// <param name="tk">
        /// The metadata token that represents the field to get interop marshaling
        /// information for.
        /// </param>
        /// <param name="ppvNativeType">
        /// A pointer to the metadata signature of the field's native type.
        /// </param>
        public int GetFieldMarshal(uint tk, out ReadOnlySpan<byte> ppvNativeType)
        {
            byte* buffer = default;
            int size = default;
            int hResult = Calli(_this, This[0]->GetFieldMarshal, tk, &buffer, &size);
            if (hResult != HResult.S_OK || buffer == null || size <= 0)
            {
                ppvNativeType = default;
                return hResult;
            }

            ppvNativeType = new Span<byte>(buffer, size);
            return hResult;
        }

        /// <summary>
        /// Gets metadata associated with the field referenced by the specified
        /// <c>FieldDef</c> token.
        /// </summary>
        /// <param name="mb">
        /// A <c>FieldDef</c> token that represents the field to get associated metadata for.
        /// </param>
        /// <param name="pClass">
        /// A pointer to a <c>TypeDef</c> token that represents the type of the class that
        /// the field belongs to.
        /// </param>
        /// <param name="szField">
        /// The name of the field.
        /// </param>
        /// <param name="pchField">
        /// The actual size of the returned buffer.
        /// </param>
        /// <param name="pdwAttr">
        /// Flags associated with the field's metadata.
        /// </param>
        /// <param name="ppvSigBlob">
        /// A pointer to the binary metadata value that describes the field.
        /// </param>
        /// <param name="pcbSigBlob">
        /// The size in bytes of <see paramref="ppvSigBlob" />.
        /// </param>
        /// <param name="pdwCPlusTypeFlag">
        /// A flag that specifies the value type of the field.
        /// </param>
        /// <param name="ppValue">
        /// A constant value for the field.
        /// </param>
        /// <param name="pcchValue">
        /// The size in chars of <see paramref="ppValue" />, or zero if no string exists.
        /// </param>
        public int GetFieldProps(
            uint mb,
            out uint pClass,
            Span<char> szField,
            out uint pchField,
            out uint pdwAttr,
            out ReadOnlySpan<byte> ppvSigBlob,
            out uint pdwCPlusTypeFlag,
            out ConstantValue ppValue)
        {
            byte* pppvSigBlob = default;
            int sigSize = default;

            byte* pppValue = default;
            uint valueSize = default;

            fixed (void* ppClass = &pClass)
            fixed (void* pszField = szField)
            fixed (void* ppchField = &pchField)
            fixed (void* ppdwAttr = &pdwAttr)
            fixed (void* ppdwCPlusTypeFlag = &pdwCPlusTypeFlag)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetFieldProps,
                    mb,
                    ppClass,
                    pszField,
                    szField.Length,
                    ppchField,
                    ppdwAttr,
                    &pppvSigBlob,
                    &sigSize,
                    ppdwCPlusTypeFlag,
                    &pppValue,
                    &valueSize);

                if (hResult != HResult.S_OK)
                {
                    ppValue = default;
                    ppvSigBlob = default;
                    return hResult;
                }

                ppvSigBlob = pppvSigBlob != null && sigSize > 0
                    ? new ReadOnlySpan<byte>(pppvSigBlob, sigSize)
                    : default;

                ppValue = new ConstantValue(pppValue, pdwCPlusTypeFlag, valueSize);
                return hResult;
            }
        }

        /// <summary>
        /// Gets a pointer to the metadata tokens for the System.Type that implements
        /// the specified method, and for the interface that declares that method.
        /// </summary>
        /// <param name="iiImpl">
        /// The metadata token representing the method to return the class and interface
        /// tokens for.
        /// </param>
        /// <param name="pClass">
        /// The metadata token representing the class that implements the method.
        /// </param>
        /// <param name="ptkIface">
        /// The metadata token representing the interface that defines the implemented method.
        /// </param>
        /// <remarks>
        /// You obtain the value for <see paramref="iImpl" /> by calling the
        /// <c>EnumInterfaceImpls</c> method.
        ///
        /// For example, suppose that a class has an <c>mdTypeDef</c> token value
        /// of <c>0x02000007</c> and that it implements three interfaces whose types
        /// have tokens:
        ///
        /// - <c>0x02000003</c> (<c>TypeDef</c>)
        /// - <c>0x0100000A</c> (<c>TypeRef</c>)
        /// - <c>0x0200001C</c> (<c>TypeDef</c>)
        ///
        /// Conceptually, this information is stored into an interface implementation table as:
        ///
        /// | Row number | Class token | Interface token |
        /// |------------|-------------|-----------------|
        /// | 4          |             |                 |
        /// | 5          | 02000007    | 02000003        |
        /// | 6          | 02000007    | 0100000A        |
        /// | 7          |             |                 |
        /// | 8          | 02000007    | 0200001C        |
        ///
        /// Recall, the token is a 4-byte value:
        ///
        /// - The lower 3 bytes hold the row number, or RID.
        /// - The upper byte holds the token type ? 0x09 for <c>mdtInterfaceImpl</c>.
        ///
        /// <c>GetInterfaceImplProps</c> returns the information held in the row whose
        /// token you provide in the <see paramref="iImpl" /> argument.
        /// </remarks>
        public int GetInterfaceImplProps(uint iiImpl, out uint pClass, out uint ptkIface)
        {
            fixed (void* ppClass = &pClass)
            fixed (void* pptkIface = &ptkIface)
            {
                return Calli(_this, This[0]->GetInterfaceImplProps, iiImpl, ppClass, pptkIface);
            }
        }

        /// <summary>
        /// Gets information stored in the metadata for a specified member definition,
        /// including the name, binary signature, and relative virtual address, of the
        /// <c>System.Type</c> member referenced by the specified metadata token. This
        /// is a simple helper method: if <see paramref="mb" /> is a <c>MethodDef</c>,
        /// then <c>GetMethodProps</c> is called; if <see paramref="mb" /> is a <c>FieldDef</c>,
        /// then <c>GetFieldProps</c> is called. See these other methods for details.
        /// </summary>
        /// <param name="mb">
        /// The token that references the member to get the associated metadata for.
        /// </param>
        /// <param name="pClass">
        /// A pointer to the metadata token that represents the class of the member.
        /// </param>
        /// <param name="szMember">
        /// The name of the member.
        /// </param>
        /// <param name="pchMember">
        /// The size in wide characters of the returned name.
        /// </param>
        /// <param name="pdwAttr">
        /// Any flag values applied to the member.
        /// </param>
        /// <param name="ppvSigBlob">
        /// A pointer to the binary metadata signature of the member.
        /// </param>
        /// <param name="pcbSigBlob">
        /// The size in bytes of <see paramref="ppvSigBlob" />.
        /// </param>
        /// <param name="pulCodeRVA">
        /// A pointer to the relative virtual address of the member.
        /// </param>
        /// <param name="pdwImplFlags">
        /// Any method implementation flags associated with the member.
        /// </param>
        /// <param name="pdwCPlusTypeFlag">
        /// A flag that marks a <c>System.ValueType</c>. It is one of the
        /// <c>ELEMENT_TYPE_*</c> values.
        /// </param>
        /// <param name="ppValue">
        /// A constant string value returned by this member.
        /// </param>
        /// <param name="pcchValue">
        /// The size in characters of <see paramref="ppValue" />, or zero if <c>ppValue</c>
        /// does not hold a string.
        /// </param>
        public int GetMemberProps(
            uint mb,
            out uint pClass,
            Span<char> szMember,
            out uint pchMember,
            out uint pdwAttr,
            out ReadOnlySpan<byte> ppvSigBlob,
            out uint pulCodeRVA,
            out uint pdwImplFlags,
            out uint pdwCPlusTypeFlag,
            out ConstantValue ppValue)
        {
            byte* sig = default;
            byte* value = default;
            int sigLength = default;
            uint valueLength = default;

            fixed (void* ppClass = &pClass)
            fixed (void* pszMember = szMember)
            fixed (void* ppchMember = &pchMember)
            fixed (void* ppdwAttr = &pdwAttr)
            fixed (void* ppulCodeRVA = &pulCodeRVA)
            fixed (void* ppdwImplFlags = &pdwImplFlags)
            fixed (void* ppdwCPlusTypeFlag = &pdwCPlusTypeFlag)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetMemberProps,
                    mb,
                    ppClass,
                    pszMember,
                    szMember.Length,
                    ppchMember,
                    ppdwAttr,
                    &sig,
                    &sigLength,
                    ppulCodeRVA,
                    ppdwImplFlags,
                    ppdwCPlusTypeFlag,
                    &value,
                    &valueLength);

                if (hResult != HResult.S_OK)
                {
                    ppvSigBlob = default;
                    ppValue = default;
                    return hResult;
                }

                ppvSigBlob = sig != null && sigLength > 0
                    ? new ReadOnlySpan<byte>(sig, sigLength)
                    : default;

                ppValue = new ConstantValue(value, pdwCPlusTypeFlag, valueLength);
                return hResult;
            }
        }

        /// <summary>
        /// Gets metadata associated with the member referenced by the specified token.
        /// </summary>
        /// <param name="mr">
        /// The <c>MemberRef</c> token to return associated metadata for.
        /// </param>
        /// <param name="ptk">
        /// A <c>TypeDef</c> or <c>TypeRef</c>, or <c>TypeSpec</c> token that represents
        /// the class that declares the member, or a <c>ModuleRef</c> token that represents
        /// the module class that declares the member, or a <c>MethodDef</c> that represents
        /// the member.
        /// </param>
        /// <param name="szMember">
        /// A string buffer for the member's name.
        /// </param>
        /// <param name="pchMember">
        /// The returned size in wide characters of <see paramref="szMember" />.
        /// </param>
        /// <param name="ppvSibBlob">
        /// A pointer to the binary metadata signature for the member.
        /// </param>
        public int GetMemberRefProps(
            uint mr,
            out uint ptk,
            Span<char> szMember,
            out uint pchMember,
            out ReadOnlySpan<byte> ppvSigBlob)
        {
            byte* sig = default;
            int sigLength = default;

            fixed (void* pptk = &ptk)
            fixed (void* pszMember = szMember)
            fixed (void* ppchMember = &pchMember)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetMemberRefProps,
                    mr,
                    pptk,
                    pszMember,
                    szMember.Length,
                    ppchMember,
                    &sig,
                    &sigLength);

                if (hResult != HResult.S_OK || sig == null || sigLength <= 0)
                {
                    ppvSigBlob = default;
                    return hResult;
                }

                ppvSigBlob = new ReadOnlySpan<byte>(sig, sigLength);
                return hResult;
            }
        }

        /// <summary>
        /// Gets the metadata associated with the method referenced by the specified
        /// <c>MethodDef</c> token.
        /// </summary>
        /// <param name="mb">
        /// The <c>MethodDef</c> token that represents the method to return metadata for.
        /// </param>
        /// <param name="pClass">
        /// A pointer to a <c>TypeDef</c> token that represents the type that implements
        /// the method.
        /// </param>
        /// <param name="szMethod">
        /// A pointer to a buffer that has the method's name.
        /// </param>
        /// <param name="pchMethod">
        /// A pointer to the size in wide characters of <see paramref="szMethod" />, or
        /// in the case of truncation, the actual number of wide characters in the method name.
        /// </param>
        /// <param name="pdwAttr">
        /// A pointer to any flags associated with the method.
        /// </param>
        /// <param name="ppvSigBlob">
        /// A pointer to the binary metadata signature of the method.
        /// </param>
        /// <param name="pulCodeRVA">
        /// A pointer to the relative virtual address of the method.
        /// </param>
        /// <param name="pdwImplFlags">
        /// A pointer to any implementation flags for the method.
        /// </param>
        public int GetMethodProps(
            uint mb,
            out uint pClass,
            Span<char> szMethod,
            out uint pchMethod,
            out uint pdwAttr,
            out ReadOnlySpan<byte> ppvSigBlob,
            out uint pulCodeRVA,
            out uint pdwImplFlags)
        {
            byte* sig = default;
            int sigLength = default;

            fixed (void* ppClass = &pClass)
            fixed (void* pszMethod = szMethod)
            fixed (void* ppchMethod = &pchMethod)
            fixed (void* ppdwAttr = &pdwAttr)
            fixed (void* ppulCodeRVA = &pulCodeRVA)
            fixed (void* ppdwImplFlags = &pdwImplFlags)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetMethodProps,
                    mb,
                    ppClass,
                    pszMethod,
                    szMethod.Length,
                    ppchMethod,
                    ppdwAttr,
                    &sig,
                    &sigLength,
                    ppulCodeRVA,
                    ppdwImplFlags);

                if (hResult == HResult.S_OK || sig == null || sigLength <= 0)
                {
                    ppvSigBlob = default;
                    return hResult;
                }

                ppvSigBlob = new ReadOnlySpan<byte>(sig, sigLength);
                return hResult;
            }
        }

        /// <summary>
        /// Gets flags indicating the relationship between the method referenced by
        /// the specified <c>MethodDef</c> token and the paired property and event
        /// referenced by the specified <c>EventProp</c> token.
        /// </summary>
        /// <param name="mb">
        /// A <c>MethodDef</c> token representing the method to get the semantic role
        /// information for.
        /// </param>
        /// <param name="tkEventProp">
        /// A token representing the paired property and event for which to get the
        /// method's role.
        /// </param>
        /// <param name="pdwSemanticsFlags">
        /// A pointer to the associated semantics flags. This value is a bitmask from
        /// the <c>CorMethodSemanticsAttr</c> enumeration.
        /// </param>
        /// <remarks>
        /// The <c>IMetaDataEmit::DefineProperty</c> method sets a method's semantics
        /// flags.
        /// </remarks>
        public int GetMethodSemantics(uint mb, uint tkEventProp, out uint pdwSemanticsFlags)
        {
            fixed (void* ppdwSemanticsFlags = &pdwSemanticsFlags)
            {
                return Calli(_this, This[0]->GetMethodSemantics, mb, tkEventProp, ppdwSemanticsFlags);
            }
        }

        /// <summary>
        /// Gets a metadata token for the module referenced in the current
        /// metadata scope.
        /// </summary>
        /// <param name="pmd">
        /// A pointer to the token representing the module referenced in the
        /// current metadata scope.
        /// </param>
        public int GetModuleFromScope(out uint pmd)
        {
            fixed (void* ppmd = &pmd)
            {
                return Calli(_this, This[0]->GetModuleFromScope, ppmd);
            }
        }

        /// <summary>
        /// Gets the name of the module referenced by the specified metadata token.
        /// </summary>
        /// <param name="mur">
        /// The <c>ModuleRef</c> metadata token that references the module to get
        /// metadata information for.
        /// </param>
        /// <param name="szName">
        /// A buffer to hold the module name.
        /// </param>
        /// <param name="pchName">
        /// The returned size of <see paramref="szName" /> in wide characters.
        /// </param>
        public int GetModuleRefProps(uint mur, Span<char> szName, out uint pchName)
        {
            fixed (void* pszName = szName)
            fixed (void* ppchName = &pchName)
            {
                return Calli(
                    _this,
                    This[0]->GetModuleRefProps,
                    mur,
                    pszName,
                    szName.Length,
                    ppchName);
            }
        }

        /// <summary>
        /// Gets the UTF-8 name of the object referenced by the specified metadata token.
        /// This method is obsolete.
        /// </summary>
        /// <param name="tk">
        /// The token representing the object to return the name for.
        /// </param>
        /// <param name="pszUtf8NamePtr">
        /// A pointer to the UTF-8 object name in the heap.
        /// </param>
        /// <remarks>
        /// <c>GetNameFromToken</c> is obsolete. As an alternative, call a method to get the
        /// properties of the particular type of token required, such as <c>GetFieldProps</c>
        /// for a field or <c>GetMethodProps</c> for a method.
        /// </remarks>
        [Obsolete("Use GetFieldProps or GetMethodProps instead.", error: true)]
        public int GetNameFromToken(uint tk, Span<char> pszUtf8NamePtr)
        {
            return HResult.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the native calling convention for the method that is represented
        /// by the specified signature pointer.
        /// </summary>
        /// <param name="pvSig">
        /// A pointer to the metadata signature of the method to return the calling
        /// convention for.
        /// </param>
        /// <param name="pCallConv">
        /// A pointer to the native calling convention.
        /// </param>
        public int GetNativeCallConvFromSig(ReadOnlySpan<byte> pvSig, out uint pCallConv)
        {
            fixed (void* ppvSig = pvSig)
            fixed (void* ppCallConv = &pCallConv)
            {
                return Calli(_this, This[0]->GetNativeCallConvFromSig, ppvSig, pvSig.Length, ppCallConv);
            }
        }

        /// <summary>
        /// Gets the <c>TypeDef</c> token for the parent <c>System.Type</c> of
        /// the specified nested type.
        /// </summary>
        /// <param name="tdNestedClass">
        /// A <c>TypeDef</c> token representing the <c>System.Type</c> to return the
        /// parent class token for.
        /// </param>
        /// <param name="ptdEnclosingClass">
        /// A pointer to the <c>TypeDef</c> token for the <c>System.Type</c> that
        /// <see paramref="tdNestedClass" /> is nested in.
        /// </param>
        public int GetNestedClassProps(uint tdNestedClass, out uint ptdEnclosingClass)
        {
            fixed (void* pptdEnclosingClass = &ptdEnclosingClass)
            {
                return Calli(_this, This[0]->GetNestedClassProps, tdNestedClass, pptdEnclosingClass);
            }
        }

        /// <summary>
        /// Gets the token that represents a specified parameter of the method
        /// represented by the specified <c>MethodDef</c> token.
        /// </summary>
        /// <param name="md">
        /// A token that represents the method to return the parameter token for.
        /// </param>
        /// <param name="ulParamSeq">
        /// The ordinal position in the parameter list where the requested parameter
        /// occurs. Parameters are numbered starting from one, with the method's return
        /// value in position zero.
        /// </param>
        /// <param name="ppd">
        /// A pointer to a <c>ParamDef</c> token that represents the requested parameter.
        /// </param>
        public int GetParamForMethodIndex(uint md, uint ulParamSeq, out uint ppd)
        {
            fixed (void* pppd = &ppd)
            {
                return Calli(_this, This[0]->GetParamForMethodIndex, md, ulParamSeq, pppd);
            }
        }

        /// <summary>
        /// Gets metadata values for the parameter referenced by the specified
        /// <c>ParamDef</c> token.
        /// </summary>
        /// <param name="tk">
        /// A <c>ParamDef</c> token that represents the parameter to return
        /// metadata for.
        /// </param>
        /// <param name="pmd">
        /// A pointer to a <c>MethodDef</c> token representing the method that
        /// takes the parameter.
        /// </param>
        /// <param name="pulSequence">
        /// The ordinal position of the parameter in the method argument list.
        /// </param>
        /// <param name="szName">
        /// A buffer to hold the name of the parameter.
        /// </param>
        /// <param name="pchName">
        /// The returned size in wide characters of <see paramref="szName" />.
        /// </param>
        /// <param name="pdwAttr">
        /// A pointer to any attribute flags associated with the parameter.
        /// This is a bitmask of <c>CorParamAttr</c> values.
        /// </param>
        /// <param name="pdwCPlusTypeFlag">
        /// A pointer to a flag specifying that the parameter is a <c>System.ValueType</c>.
        /// </param>
        /// <param name="ppValue">
        /// A pointer to a constant string returned by the parameter.
        /// </param>
        /// <remarks>
        /// The sequence values in <c>pulSequence</c> begin with 1 for parameters.
        /// A return value has a sequence number of 0.
        /// </remarks>
        public int GetParamProps(
            uint tk,
            out uint pmd,
            out uint pulSequence,
            Span<char> szName,
            out uint pchName,
            out uint pdwAttr,
            out uint pdwCPlusTypeFlag,
            out ConstantValue ppValue)
        {
            byte* value = default;
            uint valueLength = default;
            fixed (void* ppmd = &pmd)
            fixed (void* ppulSequence = &pulSequence)
            fixed (void* pszName = szName)
            fixed (void* ppchName = &pchName)
            fixed (void* ppdwAttr = &pdwAttr)
            fixed (void* ppdwCPlusTypeFlag = &pdwCPlusTypeFlag)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetParamProps,
                    tk,
                    ppmd,
                    ppulSequence,
                    pszName,
                    szName.Length,
                    ppchName,
                    ppdwAttr,
                    ppdwCPlusTypeFlag,
                    &value,
                    &valueLength);

                ppValue = new ConstantValue(value, pdwCPlusTypeFlag, valueLength);
                return hResult;
            }
        }

        /// <summary>
        /// Gets the metadata associated with the <c>System.Security.PermissionSet</c>
        /// represented by the specified <c>Permission</c> token.
        /// </summary>
        /// <param name="pm">
        /// The <c>Permission</c> metadata token that represents the permission set
        /// to get the metadata properties for.
        /// </param>
        /// <param name="pdwAction">
        /// A pointer to the permission set.
        /// </param>
        /// <param name="ppvPermission">
        /// A pointer to the binary metadata signature of the permission set.
        /// </param>
        public int GetPermissionSetProps(uint pm, out uint pdwAction, out ReadOnlySpan<byte> ppvPermission)
        {
            byte* sig = default;
            int sigLength = default;
            fixed (void* ppdwAction = &pdwAction)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetPermissionSetProps,
                    pm,
                    ppdwAction,
                    &sig,
                    &sigLength);

                if (hResult != HResult.S_OK || sig == null || sigLength <= 0)
                {
                    ppvPermission = default;
                    return hResult;
                }

                ppvPermission = new ReadOnlySpan<byte>(sig, sigLength);
                return hResult;
            }
        }

        /// <summary>
        /// Gets a <c>ModuleRef</c> token to represent the target assembly of
        /// a PInvoke call.
        /// </summary>
        /// <param name="tk">
        /// A <c>FieldDef</c> or <c>MethodDef</c> token to get the PInvoke
        /// mapping metadata for.
        /// </param>
        /// <param name="pdwMappingFlags">
        /// A pointer to flags used for mapping. This value is a bitmask from
        /// the <c>CorPinvokeMap</c> enumeration.
        /// </param>
        /// <param name="szImportName">
        /// The name of the unmanaged target DLL.
        /// </param>
        /// <param name="pchImportName">
        /// The number of wide characters returned in <see paramref="szImportName" />.
        /// </param>
        /// <param name="pmrImportDLL">
        /// A pointer to a <c>ModuleRef</c> token that represents the unmanaged
        /// target object library.
        /// </param>
        public int GetPinvokeMap(
            uint tk,
            out uint pdwMappingFlags,
            Span<char> szImportName,
            out uint pchImportName,
            out uint pmrImportDLL)
        {
            fixed (void* ppdwMappingFlags = &pdwMappingFlags)
            fixed (void* pszImportName = szImportName)
            fixed (void* ppchImportName = &pchImportName)
            fixed (void* ppmrImportDLL = &pmrImportDLL)
            {
                return Calli(
                    _this,
                    This[0]->GetPinvokeMap,
                    tk,
                    ppdwMappingFlags,
                    pszImportName,
                    szImportName.Length,
                    ppchImportName,
                    ppmrImportDLL);
            }
        }

        /// <summary>
        /// Gets the metadata for the property represented by the specified token.
        /// </summary>
        /// <param name="prop">
        /// A token that represents the property to return metadata for.
        /// </param>
        /// <param name="pClass">
        /// A pointer to the <c>TypeDef</c> token that represents the type that
        /// implements the property.
        /// </param>
        /// <param name="szProperty">
        /// A buffer to hold the property name.
        /// </param>
        /// <param name="pchProperty">
        /// The number of wide characters returned in <see paramref="szProperty" />.
        /// </param>
        /// <param name="pdwPropFlags">
        /// A pointer to any attribute flags applied to the property. This value is a
        /// bitmask from the <c>CorPropertyAttr</c> enumeration.
        /// </param>
        /// <param name="ppvSig">
        /// A pointer to the metadata signature of the property.
        /// </param>
        /// <param name="ppDefaultValue">
        /// A pointer to the bytes that store the default value for this property.
        /// </param>
        /// <param name="pmdSetter">
        /// A pointer to the <c>MethodDef</c> token that represents the set accessor
        /// method for the property.
        /// </param>
        /// <param name="pmdGetter">
        /// A pointer to the <c>MethodDef</c> token that represents the get accessor
        /// method for the property.
        /// </param>
        /// <param name="rmdOtherMethod">
        /// An array of <c>MethodDef</c> tokens that represent other methods associated
        /// with the property.
        /// </param>
        /// <param name="pcOtherMethod">
        /// The number of <c>MethodDef</c> tokens returned in <see paramref="rmdOtherMethod" />.
        /// </param>
        public int GetPropertyProps(
            uint prop,
            out uint pClass,
            Span<char> szProperty,
            out uint pchProperty,
            out uint pdwPropFlags,
            out ReadOnlySpan<byte> ppvSig,
            out ConstantValue ppDefaultValue,
            out uint pmdSetter,
            out uint pmdGetter,
            Span<uint> rmdOtherMethod,
            out uint pcOtherMethod)
        {
            void* sig = default;
            int sigLength = default;
            byte* value = default;
            CorElementType valueType = default;
            uint valueLength = default;
            fixed (void* ppClass = &pClass)
            fixed (void* pszProperty = szProperty)
            fixed (void* ppchProperty = &pchProperty)
            fixed (void* ppdwPropFlags = &pdwPropFlags)
            fixed (void* ppmdSetter = &pmdSetter)
            fixed (void* ppmdGetter = &pmdGetter)
            fixed (void* prmdOtherMethod = rmdOtherMethod)
            fixed (void* ppcOtherMethod = &pcOtherMethod)
            {
                int hResult = Calli(
                    _this,
                    This[0]->GetPropertyProps,
                    prop,
                    ppClass,
                    pszProperty,
                    szProperty.Length,
                    ppchProperty,
                    ppdwPropFlags,
                    &sig,
                    &sigLength,
                    &valueType,
                    &value,
                    &valueLength,
                    ppmdSetter,
                    ppmdGetter,
                    prmdOtherMethod,
                    rmdOtherMethod.Length,
                    ppcOtherMethod);

                ppvSig = hResult == HResult.S_OK && sig != null && sigLength > 0
                    ? new ReadOnlySpan<byte>(sig, sigLength)
                    : default;

                ppDefaultValue = new ConstantValue(value, valueType, valueLength);
                return hResult;

            }
        }

        /// <summary>
        /// Gets the relative virtual address (RVA) and the implementation flags of
        /// the method or field represented by the specified token.
        /// </summary>
        /// <param name="tk">
        /// A <c>MethodDef</c> or <c>FieldDef</c> metadata token that represents the
        /// code object to return the RVA for. If the token is a <c>FieldDef</c>, the
        /// field must be a global variable.
        /// </param>
        /// <param name="pulCodeRVA">
        /// A pointer to the relative virtual address of the code object represented
        /// by the token.
        /// </param>
        /// <param name="pdwImplFlags">
        /// A pointer to the implementation flags for the method. This value is a
        /// bitmask from the <c>CorMethodImpl</c> enumeration. The value of
        /// <c>pdwImplFlags</c> is valid only if <c>tk</c> is a <c>MethodDef</c> token.
        /// </param>
        public int GetRVA(uint tk, out uint pulCodeRVA, out uint pdwImplFlags)
        {
            fixed (void* ppulCodeRVA = &pulCodeRVA)
            fixed (void* ppdwImplFlags = &pdwImplFlags)
            {
                return Calli(_this, This[0]->GetRVA, tk, ppulCodeRVA, ppdwImplFlags);
            }
        }

        /// <summary>
        /// Gets the name and optionally the version identifier of the assembly or
        /// module in the current metadata scope.
        /// </summary>
        /// <param name="szName">
        /// A buffer for the assembly or module name.
        /// </param>
        /// <param name="pchName">
        /// The number of wide characters returned in <see paramref="szName" />.
        /// </param>
        /// <param name="pmvid">
        /// A pointer to a GUID that uniquely identifies the version of the assembly
        /// or module.
        /// </param>
        /// <remarks>
        /// The <c>IMetaDataEmit::SetModuleProps</c> method is used to set these properties.
        /// </remarks>
        public int GetScopeProps(Span<char> szName, out uint pchName, out Guid pmvid)
        {
            fixed (void* pszName = szName)
            fixed (void* ppchName = &pchName)
            fixed (void* ppmvid = &pmvid)
            {
                return Calli(
                    _this,
                    This[0]->GetScopeProps,
                    pszName,
                    szName.Length,
                    ppchName,
                    ppmvid);
            }
        }

        /// <summary>
        /// Gets the binary metadata signature associated with the specified token.
        /// </summary>
        /// <param name="mdSig">
        /// The token to return the binary metadata signature for.
        /// </param>
        /// <param name="ppvSig">
        /// A pointer to the returned metadata signature.
        /// </param>
        /// <param name="pcbSig">
        /// The size in bytes of the binary metadata signature.
        /// </param>
        public int GetSigFromToken(uint mdSig, out ReadOnlySpan<byte> ppvSig)
        {
            void* sig = default;
            int sigLength = default;
            int hResult = Calli(_this, This[0]->GetSigFromToken, mdSig, &sig, &sigLength);
            ppvSig = hResult == HResult.S_OK && sig != null && sigLength > 0
                ? new ReadOnlySpan<byte>(sig, sigLength)
                : default;

            return hResult;
        }

        /// <summary>
        /// Returns metadata information for the <c>System.Type</c> represented
        /// by the specified <c>TypeDef</c> token.
        /// </summary>
        /// <param name="td">
        /// The <c>TypeDef</c> token that represents the type to return metadata for.
        /// </param>
        /// <param name="szTypeDef">
        /// A buffer containing the type name.
        /// </param>
        /// <param name="pchTypeDef">
        /// The number of wide characters returned in <see paramref="szTypeDef" />.
        /// </param>
        /// <param name="pdwTypeDefFlags">
        /// A pointer to any flags that modify the type definition. This value is a
        /// bitmask from the <c>CorTypeAttr</c> enumeration.
        /// </param>
        /// <param name="ptkExtends">
        /// A <c>TypeDef</c> or <c>TypeRef</c> metadata token that represents the base
        /// type of the requested type.
        /// </param>
        public int GetTypeDefProps(
            uint td,
            Span<char> szTypeDef,
            out uint pchTypeDef,
            out uint pdwTypeDefFlags,
            out uint ptkExtends)
        {
            fixed (void* pszTypeDef = szTypeDef)
            fixed (void* ppchTypeDef = &pchTypeDef)
            fixed (void* ppdwTypeDefFlags = &pdwTypeDefFlags)
            fixed (void* pptkExtends = &ptkExtends)
            {
                return Calli(
                    _this,
                    This[0]->GetTypeDefProps,
                    td,
                    pszTypeDef,
                    szTypeDef.Length,
                    ppchTypeDef,
                    ppdwTypeDefFlags,
                    pptkExtends);
            }
        }

        /// <summary>
        /// Gets the metadata associated with the <c>System.Type</c> referenced by the
        /// specified <c>TypeRef</c> token.
        /// </summary>
        /// <param name="tr">
        /// The <c>TypeRef</c> token that represents the type to return metadata for.
        /// </param>
        /// <param name="ptkResolutionScope">
        /// A pointer to the scope in which the reference is made. This value is an
        /// <c>AssemblyRef</c> or <c>ModuleRef</c> token.
        /// </param>
        /// <param name="szName">
        /// A buffer containing the type name.
        /// </param>
        /// <param name="pchName">
        /// The returned size in wide characters of <see paramref="szName" />.
        /// </param>
        public int GetTypeRefProps(uint tr, out uint ptkResolutionScope, Span<char> szName, out uint pchName)
        {
            fixed (void* pptkResolutionScope = &ptkResolutionScope)
            fixed (void* pszName = szName)
            fixed (void* ppchName = &pchName)
            {
                return Calli(
                    _this,
                    This[0]->GetTypeRefProps,
                    tr,
                    pptkResolutionScope,
                    pszName,
                    szName.Length,
                    ppchName);
            }
        }

        /// <summary>
        /// Gets the binary metadata signature of the type specification represented
        /// by the specified token.
        /// </summary>
        /// <param name="typespec">
        /// The <c>TypeSpec</c> token associated with the requested metadata signature.
        /// </param>
        /// <param name="ppvSig">
        /// A pointer to the binary metadata signature.
        /// </param>
        /// <param name="pcbSig">
        /// The size, in bytes, of the metadata signature.
        /// </param>
        /// <returns>
        /// An <c>HRESULT</c> that indicates success or failure. Failures can be tested
        /// with the <c>FAILED</c> macro.
        /// </returns>
        public int GetTypeSpecFromToken(uint typespec, out ReadOnlySpan<byte> ppvSig)
        {
            void* sig = default;
            int sigLength = default;
            int hResult = Calli(_this, This[0]->GetTypeSpecFromToken, typespec, &sig, &sigLength);
            ppvSig = hResult == HResult.S_OK && sig != null && sigLength > 0
                ? new ReadOnlySpan<byte>(sig, sigLength)
                : default;

            return hResult;
        }

        /// <summary>
        /// Gets the literal string represented by the specified metadata token.
        /// </summary>
        /// <param name="stk">
        /// The <c>String</c> token to return the associated string for.
        /// </param>
        /// <param name="szString">
        /// A copy of the requested string.
        /// </param>
        /// <param name="pchString">
        /// The size in wide characters of the returned <see paramref="szString" />.
        /// </param>
        public int GetUserString(uint stk, Span<char> szString, out uint pchString)
        {
            fixed (void* pszString = szString)
            fixed (void* ppchString = &pchString)
            {
                return Calli(
                    _this,
                    This[0]->GetUserString,
                    stk,
                    pszString,
                    szString.Length,
                    ppchString);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the field, method, or type represented by
        /// the specified metadata token has global scope.
        /// </summary>
        /// <param name="pd">
        /// A metadata token that represents a type, field, or method.
        /// </param>
        /// <param name="pbGlobal">
        /// 1 if the object has global scope; otherwise, 0 (zero).
        /// </param>
        public int IsGlobal(uint pd, out int pbGlobal)
        {
            fixed (void* ppbGlobal = &pbGlobal)
            {
                return Calli(_this, This[0]->IsGlobal, pd, pbGlobal);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified token holds a valid reference
        /// to a code object.
        /// </summary>
        /// <param name="tk">
        /// The token to check the reference validity for.
        /// </param>
        /// <returns>
        /// <c>true</c> if <see paramref="tk" /> is a valid metadata token within the
        /// current scope. Otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidToken(uint tk)
        {
            return Calli(_this, This[0]->IsValidToken, tk).FromNativeBool();
        }

        /// <summary>
        /// Resets the specified enumerator to the specified position.
        /// </summary>
        /// <param name="hEnum">
        /// The enumerator to reset.
        /// </param>
        /// <param name="ulPos">
        /// The new position at which to place the enumerator.
        /// </param>
        public int ResetEnum(EnumHandle hEnum, uint ulPos)
        {
            return Calli(_this, This[0]->ResetEnum, hEnum.ToPointer(), ulPos);
        }

        /// <summary>
        /// Resolves a <c>System.Type</c> reference represented by the specified
        /// <c>TypeRef</c> token.
        /// </summary>
        /// <param name="tr">
        /// The <c>TypeRef</c> metadata token to return the referenced type information for.
        /// </param>
        /// <param name="riid">
        /// The IID of the interface to return in <see paramref="ppIScope" />. Typically,
        /// this would be <c>IID_IMetaDataImport</c>.
        /// </param>
        /// <param name="ppIScope">
        /// An interface to the module scope in which the referenced type is defined.
        /// </param>
        /// <param name="ptd">
        /// A pointer to a <c>TypeDef</c> token that represents the referenced type.
        /// </param>
        /// <remarks>
        /// IMPORTANT:
        ///
        /// Do not use this method if multiple application domains are loaded. The
        /// method does not respect application domain boundaries. If multiple versions of
        /// an assembly are loaded, and they contain the same type with the same namespace,
        /// the method returns the module scope of the first type it finds.
        ///
        /// The <c>ResolveTypeRef</c> method searches for the type definition in other modules.
        /// If the type definition is found, <c>ResolveTypeRef</c> returns an interface to
        /// that module scope as well as the <c>TypeDef</c> token for the type.
        ///
        /// If the type reference to be resolved has a resolution scope of AssemblyRef,
        /// the <c>ResolveTypeRef</c> method searches for a match only in the metadata
        /// scopes that have already been opened with calls to either the <c>IMetaDataDispenser::OpenScope</c>
        /// method or the <c>IMetaDataDispenser::OpenScopeOnMemory</c> method. This is because
        /// <c>ResolveTypeRef</c> cannot determine from only the <c>AssemblyRef</c> scope where
        /// on disk or in the global assembly cache the assembly is stored.
        /// </remarks>
        public int ResolveTypeRef(uint tr, Guid riid, out Unknown ppIScope, out uint ptd)
        {
            void* pppIScope = default;
            fixed (void* pptd = &ptd)
            {
                int hResult = Calli(_this, This[0]->ResolveTypeRef, tr, riid, &pppIScope, pptd);
                ComFactory.Create(pppIScope, hResult, out ppIScope);
                return hResult;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private new struct Vtable
        {
            public Unknown.Vtable IUnknown;

            public void* CloseEnum;

            public void* CountEnum;

            public void* EnumCustomAttributes;

            public void* EnumEvents;

            public void* EnumFields;

            public void* EnumFieldsWithName;

            public void* EnumInterfaceImpls;

            public void* EnumMemberRefs;

            public void* EnumMembers;

            public void* EnumMembersWithName;

            public void* EnumMethodImpls;

            public void* EnumMethods;

            public void* EnumMethodSemantics;

            public void* EnumMethodsWithName;

            public void* EnumModuleRefs;

            public void* EnumParams;

            public void* EnumPermissionSets;

            public void* EnumProperties;

            public void* EnumSignatures;

            public void* EnumTypeDefs;

            public void* EnumTypeRefs;

            public void* EnumTypeSpecs;

            public void* EnumUnresolvedMethods;

            public void* EnumUserStrings;

            public void* FindField;

            public void* FindMember;

            public void* FindMemberRef;

            public void* FindMethod;

            public void* FindTypeDefByName;

            public void* FindTypeRef;

            public void* GetClassLayout;

            public void* GetCustomAttributeByName;

            public void* GetCustomAttributeProps;

            public void* GetEventProps;

            public void* GetFieldMarshal;

            public void* GetFieldProps;

            public void* GetInterfaceImplProps;

            public void* GetMemberProps;

            public void* GetMemberRefProps;

            public void* GetMethodProps;

            public void* GetMethodSemantics;

            public void* GetModuleFromScope;

            public void* GetModuleRefProps;

            public void* GetNameFromToken;

            public void* GetNativeCallConvFromSig;

            public void* GetNestedClassProps;

            public void* GetParamForMethodIndex;

            public void* GetParamProps;

            public void* GetPermissionSetProps;

            public void* GetPinvokeMap;

            public void* GetPropertyProps;

            public void* GetRVA;

            public void* GetScopeProps;

            public void* GetSigFromToken;

            public void* GetTypeDefProps;

            public void* GetTypeRefProps;

            public void* GetTypeSpecFromToken;

            public void* GetUserString;

            public void* IsGlobal;

            public void* IsValidToken;

            public void* ResetEnum;

            public void* ResolveTypeRef;
        }
    }
}
