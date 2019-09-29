using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static ClrDebug.CalliInstructions;

namespace ClrDebug.Native
{
    /// <summary>
    /// Extends the <c>ICorDebugProcess</c> interface to support access
    /// to the managed heap, to provide information about garbage
    /// collection of managed objects, and to determine whether a
    /// debugger loads images from the application local native
    /// image cache.
    /// </summary>
    /// <remarks>
    /// This interface logically extends the <c>ICorDebugProcess</c>,
    /// <c>ICorDebugProcess2</c>, and <c>ICorDebugProcess3</c> interfaces.
    ///
    /// NOTE:
    ///
    /// This interface does not support being called remotely,
    /// either from another machine or from another process.
    /// </remarks>
    public unsafe class CorDebugProcess5 : Unknown
    {
        private ICorDebugProcess5Vtable** This => (ICorDebugProcess5Vtable**)DangerousGetPointer();

        /// <summary>
        /// Sets a value that determines how an application loads
        /// native images while running under a managed debugger.
        /// </summary>
        /// <param name="ePolicy">
        /// A <c>CorDebugNGenPolicy</c> constant that determines how
        /// an application loads native images while running under a
        /// managed debugger.
        /// </param>
        /// <remarks>
        /// If the policy is set successfully, the method returns
        /// <c>S_OK</c>. If <c>ePolicy</c> is outside the range of the
        /// enumerated values defined by <c>CorDebugNGenPolicy</c>, the
        /// method returns <c>E_INVALIDARG</c> and the method call has
        /// no effect. If the policy of the Native Image Generator
        /// (Ngen.exe) cannot be updated, the method returns <c>E_FAIL</c>.
        ///
        /// The <c>ICorDebugProcess5::EnableNGenPolicy</c> method can be
        /// called at any time during the lifetime of the process. The
        /// policy is in effect for any modules that are loaded after the
        /// policy is set.
        /// </remarks>
        public int EnableNGENPolicy(CorDebugNGENPolicy ePolicy)
        {
            int hResult = Calli(_this, This[0]->EnableNGENPolicy, (int)ePolicy);
            return hResult;
        }

        /// <summary>
        /// Gets an enumerator for all objects that are to be garbage-collected
        /// in a process.
        /// </summary>
        /// <param name="enumerateWeakReferences">
        /// A <c>Boolean</c> value that indicates whether weak references are also
        /// to be enumerated. If <c>enumerateWeakReferences</c> is <c>true</c>, the
        /// <c>ppEnum</c> enumerator includes both strong references and weak references.
        /// If <c>enumerateWeakReferences</c> is <c>false</c>, the enumerator includes
        /// only strong references.
        /// </param>
        /// <param name="ppEnum">
        /// A pointer to the address of an <c>ICorDebugGCReferenceEnum</c> that is an
        /// enumerator for the objects to be garbage-collected.
        /// </param>
        /// <remarks>
        /// This method provides a way to determine the full rooting chain
        /// for any managed object in a process and can be used to determine
        /// why an object is still alive.
        /// </remarks>
        public int EnumerateGCReferences(bool enumerateWeakReferences, out CorDebugStructEnum<COR_GC_REFERENCE> ppEnum)
        {
            void** pppEnum = default;
            int hResult = Calli(_this, This[0]->EnumerateGCReferences, enumerateWeakReferences.ToNativeInt(), &pppEnum);
            ppEnum = ComFactory.Create<CorDebugStructEnum<COR_GC_REFERENCE>>(pppEnum, hResult);
            return hResult;
        }

        /// <summary>
        /// Gets an enumerator for object handles in a process.
        /// </summary>
        /// <param name="types">
        /// A bitwise combination of <c>CorGCReferenceType</c> values that
        /// specifies the type of handles to include in the collection.
        /// </param>
        /// <param name="ppENum">
        /// A pointer to the address of an <c>ICorDebugGCReferenceEnum</c> that
        /// is an enumerator for the objects to be garbage-collected.
        /// </param>
        /// <remarks>
        /// <c>EnumerateHandles</c> is a helper function that supports inspection
        /// of the handle table. It is similar to the <c>ICorDebugProcess5::EnumerateGCReferences</c>
        /// method, except that rather than populating an <c>ICorDebugGCReferenceEnum</c>
        /// collection with all objects to be garbage-collected, it includes only
        /// objects that have handles from the handle table.
        ///
        /// The <c>types</c> parameter specifies the handle types to include
        /// in the collection. <c>types</c> can be any of the following three
        /// members of the <c>CorGCReferenceType</c> enumeration:
        ///
        /// - <c>CorHandleStrongOnly</c> (handles to strong references only).
        /// - <c>CorHandleWeakOnly</c> (handles to weak references only).
        /// - <c>CorHandleAll</c> (all handles).
        /// </remarks>
        public int EnumerateHandles(CorGCReferenceType types, out CorDebugStructEnum<COR_GC_REFERENCE> ppEnum)
        {
            void** pointer = default;
            int hResult = Calli(_this, This[0]->EnumerateHandles, (uint)types, &pointer);
            ppEnum = ComFactory.Create<CorDebugStructEnum<COR_GC_REFERENCE>>(pointer, hResult);
            return hResult;
        }

        /// <summary>
        /// Gets an enumerator for the objects on the managed heap.
        /// </summary>
        /// <param name="ppObject">
        /// A pointer to the address of an <c>ICorDebugHeapEnum</c> interface object
        /// that is an enumerator for the objects that reside on the managed heap.
        /// </param>
        /// <remarks>
        /// Before calling the <c>ICorDebugProcess5::EnumerateHeap</c> method,
        /// you should call the <c>ICorDebugProcess5::GetGCHeapInformation</c> method and
        /// examine the value of the <c>areGCStructuresValid</c> field of the returned
        /// <c>COR_HEAPINFO</c> object to ensure that the garbage collection heap in its
        /// current state is enumerable. In addition, the <c>ICorDebugProcess5::EnumerateHeap</c>
        /// returns <c>E_FAIL</c> if you attach too early in the lifetime of the process,
        /// before memory for the managed heap is allocated.
        ///
        /// The <c>ICorDebugHeapEnum</c> interface object is a standard enumerator
        /// derived from the <c>ICorDebugEnum</c> interface that allows you to enumerate
        /// <c>COR_HEAPOBJECT</c> objects. This method populates the <c>ICorDebugHeapEnum</c>
        /// collection object with <c>COR_HEAPOBJECT</c> instances that provide information
        /// about all objects. The collection may also include <c>COR_HEAPOBJECT</c> instances
        /// that provide information about objects that are not rooted by any object but
        /// have not yet been collected by the garbage collector.
        /// </remarks>
        public int EnumerateHeap(out CorDebugStructEnum<COR_HEAPOBJECT> ppObjects)
        {
            void** pointer = default;
            int hResult = Calli(_this, This[0]->EnumerateHeap, &pointer);
            ppObjects = ComFactory.Create<CorDebugStructEnum<COR_HEAPOBJECT>>(pointer, hResult);
            return hResult;
        }

        /// <summary>
        /// Gets an enumerator for the memory ranges of the managed heap.
        /// </summary>
        /// <param name="ppRegions">
        /// A pointer to the address of an <c>ICorDebugHeapSegmentEnum</c>
        /// interface object that is an enumerator for the ranges of memory
        /// in which objects reside in the managed heap.
        /// </param>
        /// <remarks>
        /// Before calling the <c>ICorDebugProcess5::EnumerateHeapRegions</c>
        /// method, you should call the <c>ICorDebugProcess5::GetGCHeapInformation</c>
        /// method and examine the value of the <c>areGCStructuresValid</c>
        /// field of the returned <c>COR_HEAPINFO</c> object to ensure that
        /// the garbage collection heap in its current state is enumerable.
        /// In addition, the <c>ICorDebugProcess5::EnumerateHeapRegions</c>
        /// method returns <c>E_FAIL</c> if you attach too early in the lifetime
        /// of the process, before memory regions are created.
        ///
        /// This method is guaranteed to enumerate all memory regions that may
        /// contain managed objects, but it does not guarantee that managed objects
        /// actually reside in those regions. The <c>ICorDebugHeapSegmentEnum</c>
        /// collection object may include empty or reserved memory regions.
        ///
        /// The <c>ICorDebugHeapSegmentEnum</c> interface object is a standard
        /// enumerator derived from the <c>ICorDebugEnum</c> interface that allows
        /// you to enumerate <c>COR_SEGMENT</c> objects. Each <c>COR_SEGMENT</c>
        /// object provides information about the memory range of a particular
        /// segment, along with the generation of the objects in that segment.
        /// </remarks>
        public int EnumerateHeapRegions(out CorDebugStructEnum<COR_SEGMENT> ppRegions)
        {
            void** ptr = default;
            int hResult = Calli(_this, This[0]->EnumerateHeapRegions, &ptr);
            ppRegions = ComFactory.Create<CorDebugStructEnum<COR_SEGMENT>>(ptr, hResult);
            return hResult;
        }

        /// <summary>
        /// Provides information about the layout of array types.
        /// </summary>
        /// <param name="id">
        /// A <c>COR_TYPEID</c> token that specifies the array whose
        /// layout is desired.
        /// </param>
        /// <param name="pLayout">
        /// A pointer to a <c>COR_ARRAY_LAYOUT</c> structure that contains
        /// information about the layout of the array in memory.
        /// </param>
        public int GetArrayLayout(COR_TYPEID id, out COR_ARRAY_LAYOUT pLayout)
        {
            fixed (void* ptr = &pLayout)
            {
                return Calli(_this, This[0]->GetArrayLayout, ptr);
            }
        }

        /// <summary>
        /// Provides general information about the garbage collection heap,
        /// including whether it is currently enumerable.
        /// </summary>
        /// <param name="pHeapInfo">
        /// A pointer to a <c>COR_HEAPINFO</c> value that provides general information
        /// about the garbage collection heap.
        /// </param>
        /// <remarks>
        /// The <c>ICorDebugProcess5::GetGCHeapInformation</c> method must be called
        /// before enumerating the heap or individual heap regions to ensure that the
        /// garbage collection structures in the process are currently valid. The
        /// garbage collection heap cannot be walked while a collection is in progress.
        /// Otherwise, the enumeration may capture garbage collection structures that
        /// are invalid.
        /// </remarks>
        public int GetGCHeapInformation(out COR_HEAPINFO pHeapInfo)
        {
            fixed (void* ptr = &pHeapInfo)
            {
                return Calli(_this, This[0]->GetGCHeapInformation, ptr);
            }
        }

        /// <summary>
        /// Converts an object address to an <c>ICorDebugObjectValue</c> object.
        /// </summary>
        /// <param name="addr">
        /// The object address.
        /// </param>
        /// <param name="ppObject">
        /// A pointer to the address of an <c>ICorDebugObjectValue</c> object.
        /// </param>
        /// <remarks>
        /// If <c>addr</c> does not point to a valid managed object,
        /// the <c>GetObject</c> method returns <c>E_FAIL</c>.
        /// </remarks>
        public int GetObject(ulong addr, out CorDebugObjectValue ppObject)
        {
            void** ptr = default;
            int hResult = Calli(_this, This[0]->GetObject, addr, &ptr);
            ComFactory.Create(ptr, hResult, out ppObject);
            return hResult;
        }

        /// <summary>
        /// Provides information about the fields that belong to a type.
        /// </summary>
        /// <param name="id">
        /// The identifier of the type whose field information is retrieved.
        /// </param>
        /// <param name="fields">
        /// An array of <c>COR_FIELD</c> objects that provide information about
        /// the fields that belong to the type.
        /// </param>
        /// <param name="amountWritten">
        /// A pointer to the number of <c>COR_FIELD</c> objects included in <c>fields</c>.
        /// </param>
        /// <remarks>
        /// The <c>celt</c> parameter, which specifies the number of fields whose
        /// field information the method uses to populate <c>fields</c>, should
        /// correspond to the value of the <c>COR_TYPE_LAYOUT::numFields</c> field.
        /// </remarks>
        public int GetTypeFields(COR_TYPEID id, Span<COR_FIELD> fields, out uint amountWritten)
        {
            fixed (void* pFields = fields)
            fixed (void* pAmountWritten = &amountWritten)
            {
                return Calli(
                    _this,
                    This[0]->GetTypeFields,
                    Unsafe.As<COR_TYPEID, Primitive128>(ref id),
                    fields.Length,
                    pFields,
                    pAmountWritten);
            }
        }

        /// <summary>
        /// Converts a type identifier to an <c>ICorDebugType</c> value.
        /// </summary>
        /// <param name="id">
        /// The type identifier.
        /// </param>
        /// <param name="ppType">
        /// A pointer to the address of an <c>ICorDebugType</c> object.
        /// </param>
        /// <remarks>
        /// In some cases, methods that return a type identifier may return a
        /// null <c>COR_TYPEID</c> value. If this value is passed as the <c>id</c>
        /// argument, the <c>GetTypeForTypeID</c> method will fail and return <c>E_FAIL</c>.
        /// </remarks>
        public int GetTypeForTypeID(COR_TYPEID id, out CorDebugType ppType)
        {
            void** ptr = default;
            int hResult = Calli(_this, This[0]->GetTypeForTypeID, id.ToCalliArg(), &ptr);
            ComFactory.Create(ptr, hResult, out ppType);
            return hResult;
        }

        /// <summary>
        /// Converts an object address to a <c>COR_TYPEID</c> identifier.
        /// </summary>
        /// <param name="obj">
        /// The object address.
        /// </param>
        /// <param name="pId">
        /// A pointer to the <c>COR_TYPEID</c> value that identifies the object.
        /// </param>
        public int GetTypeID(ulong obj, out COR_TYPEID pId)
        {
            fixed (void* ppId = &pId)
            {
                return Calli(_this, This[0]->GetTypeID, obj, ppId);
            }
        }

        /// <summary>
        /// Gets information about the layout of an object in memory based
        /// on its type identifier.
        /// </summary>
        /// <param name="id">
        /// A <c>COR_TYPEID</c> token that specifies the type whose layout is desired.
        /// </param>
        /// <param name="pLayout">
        /// A pointer to a <c>COR_TYPE_LAYOUT structur</c>e that contains information
        /// about the layout of the object in memory.
        /// </param>
        /// <remarks>
        /// The <c>ICorDebugProcess5::GetTypeLayout</c> method provides information
        /// about an object based on its <c>COR_TYPEID</c>, which is returned by a
        /// number of other <c>ICorDebugProcess5</c> methods. The information is provided
        /// by a <c>COR_TYPE_LAYOUT</c> structure that is populated by the method.
        /// </remarks>
        public int GetTypeLayout(COR_TYPEID id, out COR_TYPE_LAYOUT pLayout)
        {
            fixed (void* ppLayout = &pLayout)
            {
                return Calli(_this, This[0]->GetTypeLayout, id.ToCalliArg(), ppLayout);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ICorDebugProcess5Vtable
        {
            public IUnknownVtable IUnknown;

            public void* GetGCHeapInformation;

            public void* EnumerateHeap;

            public void* EnumerateHeapRegions;

            public void* GetObject;

            public void* EnumerateGCReferences;

            public void* EnumerateHandles;

            public void* GetTypeID;

            public void* GetTypeForTypeID;

            public void* GetArrayLayout;

            public void* GetTypeLayout;

            public void* GetTypeFields;

            public void* EnableNGENPolicy;
        }
    }
}
