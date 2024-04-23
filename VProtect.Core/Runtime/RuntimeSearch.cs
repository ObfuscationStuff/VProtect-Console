﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;

using dnlib.DotNet;
using dnlib.DotNet.MD;

using AsmResolver.DotNet;

using VProtect.Core.VM;
using VProtect.Core.Utilites;

namespace VProtect.Core.Runtime
{
    internal class RuntimeSearch: IDisposable
    {
        public ModuleDefinition CorLib_AsmRes
        {
            get;
            private set;
        }

        public ModuleDefMD CorLib_dnlib
        {
            get;
            private set;
        }

        #region Runtime Events
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region VMData
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public TypeDef VMData;
        public MethodDef VMData_Ctor;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region Entry
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public TypeDef Entry;
        public MethodDef Entry_Execute;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region Constants
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public TypeDef Constants;
        public MethodDef Constants_StaticCtor;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        public RuntimeSearch(VMContext ctx)
        {
            var corlib = ModuleDefinition.FromBytes(ctx.GetRawModuleBytes);
            CorLib_AsmRes = corlib.MetadataResolver.AssemblyResolver.Resolve(corlib.CorLibTypeFactory.CorLibScope.GetAssembly()).ManifestModule;
            CorLib_dnlib = ModuleDefMD.Load(CorLib_AsmRes.FilePath);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            VMData = ctx.RTModule.Find(RTMap.VMData, true);
            VMData_Ctor = VMData.FindConstructors().ToList()[0];

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            Entry = ctx.RTModule.Find(RTMap.Entry, true);
            Entry_Execute = Entry.FindMethod(RTMap.Entry_Execute);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Constants = ctx.RTModule.Find(RTMap.Constants, true);
            Constants_StaticCtor = Constants.FindOrCreateStaticConstructor();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        public void Dispose()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Entry = null;
            Entry_Execute = null;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}
