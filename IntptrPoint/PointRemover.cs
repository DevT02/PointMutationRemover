using de4dot.blocks;
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
                    int w = 0;
                    while (w < method.Body.Instructions.Count)
                    {
                        for (int i = 0; i < method.Body.Instructions.Count; i++)
                        {
                            bool x = false;
                            bool y = false;
                            if (method.Body.Instructions[i].OpCode != OpCodes.Newobj) continue;
                            if (!method.Body.Instructions[i].Operand.ToString().Contains("Point::.ctor")) continue;
                            if (method.Body.Instructions[i + 3].OpCode != OpCodes.Call) continue;
                            if (!method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_")) continue;
                            if (!method.Body.Instructions[i + 1].OpCode.ToString().ToLower().Contains("stloc")) continue;
                            if (!method.Body.Instructions[i + 2].OpCode.ToString().ToLower().Contains("ldloca")) continue;
                            if (!method.Body.Instructions[i - 2].OpCode.ToString().ToLower().Contains("ldc.i4")) continue; // 
                            if (!method.Body.Instructions[i - 1].OpCode.ToString().ToLower().Contains("ldelem.i")) continue;
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i - 4);
                            amount++;
                        }
                        for (int i = 0; i < method.Body.Instructions.Count; i++)
                        {
                            bool x = false;
                            bool y = false;
                            if (method.Body.Instructions[i].OpCode != OpCodes.Newobj) continue;
                            if (!method.Body.Instructions[i].Operand.ToString().Contains("Point::.ctor")) continue;
                            if (method.Body.Instructions[i + 3].OpCode != OpCodes.Call) continue;
                            if (!method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_")) continue;
                            if (!method.Body.Instructions[i + 1].OpCode.ToString().ToLower().Contains("stloc")) continue;
                            if (!method.Body.Instructions[i + 2].OpCode.ToString().ToLower().Contains("ldloca")) continue;
                            if (!method.Body.Instructions[i - 2].OpCode.ToString().ToLower().Contains("ldelem.i")) continue; // 
                            if (!method.Body.Instructions[i - 1].OpCode.ToString().ToLower().Contains("ldc.i4")) continue;
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i);
                            method.Body.Instructions.RemoveAt(i - 1);
                            amount++;
                        }
                        for (int i = 0; i < method.Body.Instructions.Count; i++)
                        {
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
                                Console.WriteLine("GotX");
                                x = true;
                            }
                            if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_Y"))
                            {
                                Console.WriteLine("GotY");
                                y = true;
                            }
                            //var valY = 0;
                            //var valX = 0;
                            //try
                            //{
                            //    valY = method.Body.Instructions[i - 1].GetLdcI4Value();
                            //    valX = method.Body.Instructions[i - 2].GetLdcI4Value();
                            //}
                            //catch
                            //{
                            //    continue;
                            //}
                            //int finalvalue = 0;
                            if (y)
                            {
                                //finalvalue = valY;
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i - 2);
                                //     method.Body.Instructions[i - 1].Operand = finalvalue;
                            }
                            if (x)
                            {
                                //finalvalue = valX;
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i);
                                method.Body.Instructions.RemoveAt(i - 1);
                                //   method.Body.Instructions[i - 2].Operand = finalvalue;
                            }
                            amount++;
                        }
                        for (int i = 0; i < method.Body.Instructions.Count; i++)
                        {
                            //int AHHAHA = i;
                            //int AHHAHA2 = i;
                            bool x = false;
                            bool y = false;

                            bool XisinafuckedupPosition = false;
                            bool YisinafuckedupPosition = false;

                            if (method.Body.Instructions[i].OpCode != OpCodes.Newobj) continue;
                            if (!method.Body.Instructions[i].Operand.ToString().Contains("Point::.ctor")) continue;
                            if (method.Body.Instructions[i + 3].OpCode != OpCodes.Call) continue;
                            if (!method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_")) continue;
                            if (!method.Body.Instructions[i + 1].OpCode.ToString().ToLower().Contains("stloc")) continue;
                            if (!method.Body.Instructions[i + 2].OpCode.ToString().ToLower().Contains("ldloca")) continue;

                            int index = i - 1;
                            while (method.Body.Instructions[index].OpCode.ToString().ToLower().Contains("nop"))
                            {
                                index--;
                                YisinafuckedupPosition = true;
                            }
                            if (method.Body.Instructions[index].OpCode.ToString().ToLower().Contains("nop"))
                            {
                                throw new NotSupportedException();
                            }
                            if (!method.Body.Instructions[index].OpCode.ToString().ToLower().Contains("ldc.i4")) continue;
                            int index2 = i - 2;
                            while (method.Body.Instructions[index2].OpCode.ToString().ToLower().Contains("nop"))
                            {
                                index2--;
                                XisinafuckedupPosition = true;
                            }
                            if (method.Body.Instructions[index2].OpCode.ToString().ToLower().Contains("nop"))
                            {
                                throw new NotSupportedException();
                            }
                            if (!method.Body.Instructions[index2].OpCode.ToString().ToLower().Contains("ldc.i4")) continue;
                            if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_X"))
                            {
                                x = true;
                            }
                            if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_Y"))
                            {
                                y = true;
                            }
                            var valY = 0;
                            var valX = 0;
                            int finalvalue = 0;
                            if (y)
                                finalvalue = valY;
                            if (x)
                                finalvalue = valX;
                            method.Body.Instructions[i].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 2].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 3].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
                            if (YisinafuckedupPosition)
                                method.Body.Instructions[index2].Operand = finalvalue;
                            if (XisinafuckedupPosition)
                                method.Body.Instructions[index].Operand = finalvalue;
                            amount++;
                        }
                        w++;
                    }
                }

            }
        }
    }
}
