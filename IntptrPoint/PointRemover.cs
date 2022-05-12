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
                        IList<Instruction> instr = method.Body.Instructions;
                        for (int i = 0; i < instr.Count; i++)
                        {
                            // CODE: new Point(1676, 3352).Y 
                            // A POINT MUTATION IN DNSPY LOOKS LIKE:

                            //19  0047    ldc.i4    0x68C 
                            //20  004C    ldc.i4    0xD18
                            //21  0051    newobj    instance void [System.Drawing]System.Drawing.Point::.ctor(int32, int32)
                            //22  0056    stloc.s   V_69(69)
                            //23  0058    ldloca.s  V_69(69)
                            //24  005A    call    instance int32[System.Drawing]System.Drawing.Point::get_Y()


                            // 2 instructions before Point::.ctor (constructor) is X value, 1 instruction before is Y value

                            // 3 instructions ahead is .Y /.X  get_X obviously means .X   get_Y obviously means .Y

                            // Here, we will try getting point via instruction 21 (the newobj opcode). If this exists, it is possible to check if there is point mutation

                            try
                            {
                                if (instr[i + 3].OpCode != OpCodes.Call || instr[i].OpCode != OpCodes.Newobj || !instr[i].Operand.ToString().Contains("Point::.ctor") || !instr[i + 3].Operand.ToString().Contains("::get_") || !instr[i + 1].OpCode.Name.StartsWith("stloc") || !instr[i + 2].OpCode.Name.StartsWith("ldloca")) continue;
                            }
                            catch
                            {
                                continue; // If instruction is 0, 1, or 2, this will stop the OutOfRangeExeption. (skip)
                            }

                            if (instr[i + 3].Operand.ToString().Contains("::get_X")) // IF .X
                            {
                                if (!instr[i - 1].OpCode.ToString().Contains("ldc.i4"))
                                    continue; //Opcode has to be an LDCi4 instruction
                                instr[i].OpCode = OpCodes.Nop; //removes newobj
                                instr[i + 1].OpCode = OpCodes.Nop; // removes stloc
                                instr[i + 2].OpCode = OpCodes.Nop; // removes ldloca
                                instr[i + 3].OpCode = OpCodes.Nop; // removes .X
                                instr[i - 1].OpCode = OpCodes.Nop; // Because looks for .X, we can remove the Y Value stuff so only X value remains

                            }
                            if (instr[i + 3].Operand.ToString().Contains("::get_Y"))
                            {
                                if (!instr[i - 2].OpCode.ToString().Contains("ldc.i4"))
                                    continue;
                                instr[i].OpCode = OpCodes.Nop;
                                instr[i + 1].OpCode = OpCodes.Nop;
                                instr[i + 2].OpCode = OpCodes.Nop;
                                instr[i + 3].OpCode = OpCodes.Nop;
                                instr[i - 2].OpCode = OpCodes.Nop;
                            }
                            amount++;
                        }
                }

            }
        }
    }
}
