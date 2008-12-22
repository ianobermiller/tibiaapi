using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Packets
{
    public class MessageStream
    {
        byte[] _buffer;
        int _position;
        int _length;

        public MessageStream()
        {
            _buffer = new byte[64];
        }

        public MessageStream(int size)
            : this()
        {
            CheckIfNeedExpand(size);
            _length = size;
        }

        public MessageStream(byte[] buffer)
            : this(buffer, 0, buffer.Length)
        { }

        public MessageStream(byte[] buffer, int offset, int count)
            : this()
        {
            Write(buffer, offset, count);
        }

        public void WriteByte(byte value)
        {
            Write(new byte[] { value }, 0, 1);
        }

        public void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            CheckIfNeedExpand(count);
            Array.Copy(buffer, offset, _buffer, _position, count);
            _position += count;

            if (_position > _length)
                _length = _position;
        }

        private void CheckIfNeedExpand(int count)
        {
            int _l = count + _length;

            if (_l > _buffer.Length)
            {

                int __l = 128;

                while (__l < _l)
                    __l *= 2;

                byte[] _t = new byte[__l];
                Array.Copy(_buffer, _t, _buffer.Length);

                _buffer = _t;
            }
        }

        public byte ReadByte()
        {
            if (_position + 1 > _length)
                throw new Exception("Length < Position.");

            return _buffer[_position++];
        }

        public byte[] Read(int count)
        {
            if (_position + count > _length)
                throw new Exception("Length < Position.");

            byte[] _t = new byte[count];
            Array.Copy(_buffer, _position, _t, 0, count);
            _position += count;
            return _t;
        }

        public void Read(byte[] buffer, int offset, int count)
        {
            if (_position + count > _length)
                throw new Exception("Length < Position.");

            Array.Copy(_buffer, _position, buffer, 0, count);
            _position += count;
        }

        public byte PeekByte()
        {
            return _buffer[_position];
        }

        public byte[] Peek(int count)
        {
            byte[] _t = new byte[count];
            Array.Copy(_buffer, _position, _t, 0, count);
            return _t;
        }

        public void Peek(byte[] buffer, int offset, int count)
        {
            Array.Copy(_buffer, _position, buffer, 0, count);
        }

        public byte[] GetBuffer()
        {
            return _buffer;
        }

        public byte[] ToArray()
        {
            byte[] _t = new byte[_length];
            Array.Copy(_buffer, _t, _length);
            return _t;
        }

        public int Length
        {
            get { return _length; }
            set
            {
                if (value > _buffer.Length)
                    CheckIfNeedExpand(value - _buffer.Length);
                _length = value;
            }
        }

        public int Position
        {
            get { return _position; }
            set
            {
                if (value > _buffer.Length)
                    CheckIfNeedExpand(value - _buffer.Length);

                if (value > _length)
                    _length = value;

                _position = value;
            }
        }
    }
}

