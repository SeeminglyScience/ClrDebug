using System;
using System.Text;

namespace ClrDebug.Native
{
    public readonly ref struct ConstantValue
    {
        private readonly unsafe byte* _ptr;

        private readonly CorElementType _type;

        private readonly uint _count;

        internal unsafe ConstantValue(byte* ptr, uint type, uint count)
        {
            _ptr = ptr;
            _type = (CorElementType)unchecked((int)type);
            _count = count;
        }

        internal unsafe ConstantValue(byte* ptr, CorElementType type, uint count)
        {
            _ptr = ptr;
            _type = type;
            _count = count;
        }

        public unsafe bool TryReadBoolean(out bool value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_BOOLEAN)
            {
                value = default;
                return false;
            }

            value = ReadBoolean();
            return true;
        }

        public unsafe bool TryReadChar(out char value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_CHAR)
            {
                value = default;
                return false;
            }

            value = ReadChar();
            return true;
        }

        public unsafe bool TryReadSByte(out sbyte value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_I)
            {
                value = default;
                return false;
            }

            value = ReadSByte();
            return true;
        }

        public unsafe bool TryReadInt16(out short value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_I2)
            {
                value = default;
                return false;
            }

            value = ReadInt16();
            return true;
        }

        public unsafe bool TryReadInt32(out int value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_I4)
            {
                value = default;
                return false;
            }

            value = ReadInt32();
            return true;
        }

        public unsafe bool TryReadInt64(out long value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_I8)
            {
                value = default;
                return false;
            }

            value = ReadInt64();
            return true;
        }

        public unsafe bool TryReadByte(out byte value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_U1)
            {
                value = default;
                return false;
            }

            value = ReadByte();
            return true;
        }

        public unsafe bool TryReadUInt16(out ushort value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_U2)
            {
                value = default;
                return false;
            }

            value = ReadUInt16();
            return true;
        }

        public unsafe bool TryReadUInt32(out uint value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_U4)
            {
                value = default;
                return false;
            }

            value = ReadUInt32();
            return true;
        }

        public unsafe bool TryReadUInt64(out ulong value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_U8)
            {
                value = default;
                return false;
            }

            value = ReadUInt64();
            return true;
        }

        public unsafe bool TryReadString(out string value)
        {
            if (_ptr == null || _type != CorElementType.ELEMENT_TYPE_STRING)
            {
                value = default;
                return false;
            }

            value = ReadString();
            return true;
        }

        public unsafe object Read()
        {
            if (_ptr == default)
            {
                return null;
            }

            return _type switch
            {
                CorElementType.ELEMENT_TYPE_BOOLEAN => ReadBoolean(),
                CorElementType.ELEMENT_TYPE_CHAR => ReadChar(),
                CorElementType.ELEMENT_TYPE_CLASS => null,
                CorElementType.ELEMENT_TYPE_I1 => ReadSByte(),
                CorElementType.ELEMENT_TYPE_I2 => ReadInt16(),
                CorElementType.ELEMENT_TYPE_I4 => ReadInt32(),
                CorElementType.ELEMENT_TYPE_I8 => ReadInt64(),
                CorElementType.ELEMENT_TYPE_OBJECT => null,
                CorElementType.ELEMENT_TYPE_U1 => ReadByte(),
                CorElementType.ELEMENT_TYPE_U2 => ReadUInt16(),
                CorElementType.ELEMENT_TYPE_U4 => ReadUInt32(),
                CorElementType.ELEMENT_TYPE_U8 => ReadUInt64(),
                CorElementType.ELEMENT_TYPE_STRING => ReadString(),
                _ => throw new ArgumentException(nameof(_type)),
            };
        }

        private unsafe bool ReadBoolean() => *_ptr == 1;

        private unsafe char ReadChar() => *(char*)_ptr;

        private unsafe sbyte ReadSByte() => *(sbyte*)_ptr;

        private unsafe short ReadInt16() => *(short*)_ptr;

        private unsafe int ReadInt32() => *(int*)_ptr;

        private unsafe long ReadInt64() => *(long*)_ptr;

        private unsafe byte ReadByte() => *_ptr;

        private unsafe ushort ReadUInt16() => *(ushort*)_ptr;

        private unsafe uint ReadUInt32() => *(uint*)_ptr;

        private unsafe ulong ReadUInt64() => *(ulong*)_ptr;

        private unsafe string ReadString()
        {
            if (_count == 0)
            {
                return string.Empty;
            }

            return new string((sbyte*)_ptr, 0, unchecked((int)_count), Encoding.ASCII);
        }

    }
}
