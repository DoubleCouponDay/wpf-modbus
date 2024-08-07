﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace wpf_modbus
{
    public class JustWorksBridge : IModbusBridge
    {
        byte[] coils;
        byte[] registers;

        public JustWorksBridge(byte inputCoil, byte inputRegister)
        {
            coils = [inputCoil];
            registers = [inputRegister];
        }

        public void Connect(string address)
        {

        }

        public void Dispose()
        {
            
        }

        public bool IsConnected()
        {
            return true;
        }

        public Span<byte> ReadCoils(int startingAddress, int quantity)
        {
            return new Span<byte>(coils);
        }

        public Span<T> ReadHoldingRegisters<T>(int startingAddress, int count) where T : unmanaged
        {
            var asSpan = registers.AsSpan();
            ReadOnlySpan<T> readonlySpan = MemoryMarshal.Cast<byte, T>(asSpan);
            var output = new Span<T>();
            readonlySpan.CopyTo(output);
            return output;
        }

        public void WriteSingleCoil(int startingAddress, bool value)
        {
            if (value) {
                coils[0] |= (byte)(1 << startingAddress);
            }
            
            else
            {
                coils[0] |= (byte)(0 << startingAddress);
            }
        }

        public void WriteSingleRegister(int registerAddress, short value)
        {
            registers[0] = (byte)value;
        }
    }
}
