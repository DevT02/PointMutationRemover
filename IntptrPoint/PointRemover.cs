using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntptrPoint
{
    class PointRemover
    {
        public static int amount = 0;
        public static void Clean()
        {
            foreach (TypeDef types in Program.module.GetTypes())
            {
                foreach (MethodDef method in types.Methods)
                {
                    if (!method.HasBody) continue;
                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        int AHHAHA = i;
                        bool x = false;
                        bool y = false;
                        if (method.Body.Instructions[i].OpCode != OpCodes.Newobj) continue;
                        if (!method.Body.Instructions[i].Operand.ToString().Contains("Point::.ctor")) continue;
                        if (method.Body.Instructions[i + 3].OpCode != OpCodes.Call) continue;
                        if (!method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_")) continue;
                        if (!method.Body.Instructions[i + 1].OpCode.ToString().ToLower().Contains("stloc")) continue;
                        if (!method.Body.Instructions[i + 2].OpCode.ToString().ToLower().Contains("ldloca")) continue;
                        if (!method.Body.Instructions[i - 2].OpCode.ToString().ToLower().Contains("ldc.i4")) continue; // 
                        if (!method.Body.Instructions[i - 1].OpCode.ToString().ToLower().Contains("ldc.i4")) continue;
                        if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_X"))
                        {
                            x = true;
                        }
                        if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_Y"))
                        {
                            y = true;
                        }
                        var valY = method.Body.Instructions[i - 1].GetLdcI4Value();
                        var valX = method.Body.Instructions[i - 2].GetLdcI4Value();
                        int finalvalue = 0;
                        if (y)
                            finalvalue = valY;
                        if (x)
                            finalvalue = valX;
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i - 1);
                        method.Body.Instructions[i - 2].Operand = finalvalue;
                        amount++;
                    }
                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        int AHHAHA = i;
                        bool x = false;
                        bool y = false;
                        if (method.Body.Instructions[i].OpCode != OpCodes.Newobj) continue;
                        if (!method.Body.Instructions[i].Operand.ToString().Contains("Point::.ctor")) continue;
                        if (method.Body.Instructions[i + 3].OpCode != OpCodes.Call) continue;
                        if (!method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_")) continue;
                        if (!method.Body.Instructions[i + 1].OpCode.ToString().ToLower().Contains("stloc")) continue;
                        if (!method.Body.Instructions[i + 2].OpCode.ToString().ToLower().Contains("ldloca")) continue;
                        if (!method.Body.Instructions[i - 2].OpCode.ToString().ToLower().Contains("ldc.i4")) continue; // 
                        if (!method.Body.Instructions[i - 1].OpCode.ToString().ToLower().Contains("ldc.i4")) continue;
                        if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_X"))
                        {
                            x = true;
                        }
                        if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_Y"))
                        {
                            y = true;
                        }
                        var valY = method.Body.Instructions[i - 1].GetLdcI4Value();
                        var valX = method.Body.Instructions[i - 2].GetLdcI4Value();
                        int finalvalue = 0;
                        if (y)
                            finalvalue = valY;
                        if (x)
                            finalvalue = valX;
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i);
                        method.Body.Instructions.RemoveAt(i - 1);
                        method.Body.Instructions[i - 2].Operand = finalvalue;
                        amount++;
                    }
                }
            }
        }
    }
}
