using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Tibia
{
    /// <summary>
    /// Helper methods for reading memory.
    /// </summary>
    public static class Memory
    {
        public const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
        public const uint PROCESS_VM_READ = 0x0010;
        public const uint PROCESS_VM_WRITE = 0x0020;
        public const uint PROCESS_VM_OPERATION = 0x0008;

        public const uint PAGE_EXECUTE_READWRITE = 0x40;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, 
            IntPtr dwSize, uint flNewProtect, ref uint lpflOldProtect);
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowText(IntPtr hWnd, string newTitle);

        /// <summary>
        /// Read a specified number of bytes from a process.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <param name="bytesToRead"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(Process process, long address, uint bytesToRead)
        {
            IntPtr pHandle;
            IntPtr ptrBytesRead;
            byte[] buffer = new byte[bytesToRead];

            // Use the pid to get a Process Handle
            pHandle = OpenProcess(PROCESS_VM_READ, 0, (uint)process.Id);

            ReadProcessMemory(pHandle, new IntPtr(address), buffer, bytesToRead, out ptrBytesRead);

            CloseHandle(pHandle);

            return buffer;
        }

        /// <summary>
        /// Read an integer from the process (actually a short because it is only 4 bytes)
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static int ReadInt(Process process, long address)
        {
            return BitConverter.ToInt32(ReadBytes(process, address, 4), 0);
        }

        /// <summary>
        /// Read a byte from memory.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static byte ReadByte(Process process, long address)
        {
            return ReadBytes(process, address, 1)[0];
        }

        /// <summary>
        /// Read a string from memmory. Splits at 00 and returns first section to avoid junk.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string ReadString(Process process, long address)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            String str = enc.GetString(ReadBytes(process, address, 255)).Split(new Char())[0];
            return str;
        }

        /// <summary>
        /// Write a specified number of bytes to a process.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static bool WriteBytes(Process process, long address, byte[] bytes, uint length)
        {
            IntPtr handle;
            IntPtr bytesWritten;
            int result;

            // Use the pid to get a Process Handle
            handle = OpenProcess(PROCESS_VM_WRITE | PROCESS_VM_OPERATION, 0, (uint)process.Id);

            // Write to memory
            result = WriteProcessMemory(handle, new IntPtr(address), bytes, length, out bytesWritten);

            // Close the process handle
            CloseHandle(handle);

            return (result != 0);
        }

        /// <summary>
        /// Write an integer to memory.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteInt(Process process, long address, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return WriteBytes(process, address, bytes, 4);
        }

        /// <summary>
        /// Write a byte to memory.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteByte(Process process, long address, byte value)
        {
            byte[] bytes = { value };
            return WriteBytes(process, address, bytes, 1);
        }

        /// <summary>
        /// Write a string to memory.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool WriteString(Process process, long address, string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] bytes = enc.GetBytes(str);
            return WriteBytes(process, address, bytes, (uint)bytes.Length);
        }

        /// <summary>
        /// Set the RSA key. Different from WriteString because must overcome protection.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        /// <param name="newKey"></param>
        /// <returns></returns>
        public static bool WriteRSA(Process process, long address, string newKey)
        {
            IntPtr handle;
            IntPtr bytesWritten;
            int result;
            uint oldProtection = 0;

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] bytes = enc.GetBytes(newKey);

            // Use the pid to get a Process Handle
            handle = OpenProcess(PROCESS_ALL_ACCESS, 0, (uint)process.Id);

            // Make it so we can write to the memory block
            VirtualProtectEx(handle, new IntPtr(address), new IntPtr(bytes.Length), PAGE_EXECUTE_READWRITE, ref oldProtection);

            // Write to memory
            result = WriteProcessMemory(handle, new IntPtr(address), bytes, (uint)bytes.Length, out bytesWritten);

            // Put the protection back on the memory block
            VirtualProtectEx(handle, new IntPtr(address), new IntPtr(bytes.Length), oldProtection, ref oldProtection);

            // Close the process handle
            CloseHandle(handle);

            return (result != 0);
        }
    }
}
