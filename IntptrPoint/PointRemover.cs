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

                            int index = i - 1;
                            int index2 = i - 2;
                            if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_Y"))
                            {
                                y = true;
                                while (method.Body.Instructions[index].OpCode.ToString().ToLower().Contains("nop"))
                                {
                                    index--;
                                }
                                while (method.Body.Instructions[index - 1].OpCode.ToString().ToLower().Contains("nop"))
                                {
                                    int intersepction = index - 1;
                                    intersepction--;
                                    index = intersepction;
                                }
                                if (method.Body.Instructions[index].OpCode.ToString().ToLower().Contains("ldc.i4"))
                                {
                                    Console.WriteLine("Found LDCi4, INDEX (X)");
                                }
                            }
                            if (method.Body.Instructions[i + 3].Operand.ToString().Contains("::get_X"))
                            {
                                x = true;
                                while (method.Body.Instructions[index2].OpCode.ToString().ToLower().Contains("nop"))
                                {
                                    index2--;
                                }
                                while (method.Body.Instructions[index2 - 1].OpCode.ToString().ToLower().Contains("nop"))
                                {
                                    int intersepction = index2 - 1;
                                    intersepction--;
                                    index2 = intersepction;
                                }
                                if (method.Body.Instructions[index2].OpCode.ToString().ToLower().Contains("ldc.i4"))
                                {
                                    Console.WriteLine("Found LDCi4, INDEX2 (Y)");
                                }
                            }

                            int finalvalue = 0;
                            if (x)
                            {
                                try
                                {
                                    finalvalue = method.Body.Instructions[index].GetLdcI4Value();
                                }
                                catch
                                {
                                    if (method.Body.Instructions[index].OpCode.ToString().ToLower().Contains("m1")) {
                                        finalvalue = -1;
                                    }
                                    else if (!method.Body.Instructions[index].OpCode.ToString().Contains("ldc.i4"))
                                    {
                                        continue; //rip conv.i4
                                    }
                                    else if (method.Body.Instructions[index].OpCode.ToString().Replace("ldc.i4.i", "").Any(char.IsDigit))
                                    {
                                        Console.WriteLine(method.Body.Instructions[index].OpCode.ToString());
                                        int held = Convert.ToInt32(method.Body.Instructions[index].OpCode.ToString().Replace("ldc.i4.i", ""));
                                        switch (held)
                                        {
                                            case 0:
                                                finalvalue = 0;
                                                break;
                                            case 1:
                                                finalvalue = 1;
                                                break;
                                            case 2:
                                                finalvalue = 2;
                                                break;
                                            case 3:
                                                finalvalue = 3;
                                                break;
                                            case 4:
                                                finalvalue = 4;
                                                break;
                                            case 5:
                                                finalvalue = 5;
                                                break;
                                            case 6:
                                                finalvalue = 6;
                                                break;
                                            case 7:
                                                finalvalue = 7;
                                                break;
                                            case 8:
                                                finalvalue = 8;
                                                break;
                                        }
                                    }
                                }
                                method.Body.Instructions[i].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i + 2].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i + 3].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i - 1].OpCode = OpCodes.Nop;
                                method.Body.Instructions[index].Operand = finalvalue;

                            }
                            if (y)
                            {
                                try
                                {
                                    finalvalue = method.Body.Instructions[index2].GetLdcI4Value();
                                }
                                catch
                                {
                                    if (method.Body.Instructions[index2].OpCode.ToString().ToLower().Contains("m1"))
                                        finalvalue = -1;
                                    else if (!method.Body.Instructions[index2].OpCode.ToString().Contains("ldc.i4"))
                                    {
                                        continue;
                                    }
                                    else if (method.Body.Instructions[index2].OpCode.ToString().Replace("ldc.i4.i", "").Any(char.IsDigit))
                                    {
                                        Console.WriteLine(method.Body.Instructions[index2].OpCode.ToString());
                                        int held = Convert.ToInt32(method.Body.Instructions[index2].OpCode.ToString().Replace("ldc.i4.i", ""));
                                        switch (held)
                                        {
                                            case 0:
                                                finalvalue = 0;
                                                break;
                                            case 1:
                                                finalvalue = 1;
                                                break;
                                            case 2:
                                                finalvalue = 2;
                                                break;
                                            case 3:
                                                finalvalue = 3;
                                                break;
                                            case 4:
                                                finalvalue = 4;
                                                break;
                                            case 5:
                                                finalvalue = 5;
                                                break;
                                            case 6:
                                                finalvalue = 6;
                                                break;
                                            case 7:
                                                finalvalue = 7;
                                                break;
                                            case 8:
                                                finalvalue = 8;
                                                break;
                                        }
                                    }
                                }
                                method.Body.Instructions[i].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i + 2].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i + 3].OpCode = OpCodes.Nop;
                                method.Body.Instructions[i - 2].OpCode = OpCodes.Nop;
                                method.Body.Instructions[index2].Operand = finalvalue;
                            }
                            amount++;
                        }
                        w++;
                    }
                }

            }
        }
    }
}
