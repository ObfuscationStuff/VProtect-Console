﻿using System;
using System.Collections.Generic;

using VProtect.Runtime.Dynamic;
using VProtect.Runtime.Variants;
using VProtect.Runtime.Execution;

namespace VProtect.Runtime.OpCodes
{
    internal class Ldc_R4 : IOpCode
    {
        public ushort ILCode
        {
            get { return Constants.Ldc_R4; }
        }

        public void Run(VMContext ctx, ref int opcodeOffset)
        {
            ctx.Push(new SingleVariant((float)ctx.Instructions[opcodeOffset].Operands[0]));

            opcodeOffset++;
        }
    }
}
