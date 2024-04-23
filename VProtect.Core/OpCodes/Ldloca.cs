﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dnlib.DotNet;
using dnlib.DotNet.Emit;

using VProtect.Core.VM;
using VProtect.Core.Utilites;

namespace VProtect.Core.OpCodes
{
    internal class Ldloca : IOpCode
    {
        public OpCode Code
        {
            get { return dnlib.DotNet.Emit.OpCodes.Ldloca; }
        }

        public void Run(VMContext ctx, dynamic operand)
        {
            ctx.Instructions.Add(new VInstruction(VCode.Ldloca, ctx.SelectedMethod.Body.Variables.IndexOf((Local)operand), MDTokenResolver.Resolve(ctx, ((Local)operand).Type)));
        }
    }
}