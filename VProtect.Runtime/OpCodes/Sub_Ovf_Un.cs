﻿using System;
using System.Collections.Generic;

using VProtect.Runtime.Dynamic;
using VProtect.Runtime.Execution;

namespace VProtect.Runtime.OpCodes
{
    internal class Sub_Ovf_Un : IOpCode
    {
        public ushort ILCode
        {
            get { return Constants.Sub_Ovf_Un; }
        }

        public void Run(VMContext ctx, ref int opcodeOffset)
        {
            var value = ctx.Pop();
            var value1 = ctx.Pop();

            ctx.Push(ctx.Sub(value, value1, true, true));

            opcodeOffset++;
        }
    }
}
