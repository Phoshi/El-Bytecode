using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Speedycloud.Bytecode.ValueTypes;
using ValueType = Speedycloud.Bytecode.ValueTypes.ValueType;

namespace Speedycloud.Bytecode {
    public class BytecodeSerialiser {
        public string Dump(IEnumerable<Opcode> instructionStream, IEnumerable<IValue> consts) {
            var serialised = new StringBuilder();
            foreach (var @const in consts) {
                switch (@const.Type) {
                    case ValueType.Integer:
                        serialised.AppendLine("0" + @const.Integer);
                        break;
                    case ValueType.Double:
                        serialised.AppendLine("1" + @const.Double);
                        break;
                    case ValueType.Boolean:
                        serialised.AppendLine("2" + @const.Boolean);
                        break;
                    case ValueType.String:
                        serialised.AppendLine("3\"" + @const.String + "\"");
                        break;
                } 
            }
            serialised.AppendLine();

            foreach (var instruction in instructionStream) {
                var id = ((int) instruction.Instruction).ToString();
                var args = instruction.OpArgs.Select(a=>a.ToString());
                serialised.AppendLine(string.Join(" ", new[] {id}.Concat(args)));
            }

            return serialised.ToString();
        }

        public Tuple<IEnumerable<Opcode>, IEnumerable<IValue>> Load(string input) {
            var isConstSection = true;
            var consts = new List<IValue>();
            var instructions = new List<Opcode>();

            var reader = new StringReader(input);

            string line;
            while ((line = reader.ReadLine()) != null) {
                if (isConstSection) {
                    if (line == "") {
                        isConstSection = false;
                        continue;
                    }
                    if (line.StartsWith("0")) {
                        var integer = Int64.Parse(line.Substring(1));
                        consts.Add(new IntValue(integer));
                    }
                    else if (line.StartsWith("1")) {
                        var doubleNumber = Double.Parse(line.Substring(1));
                        consts.Add(new DoubleValue(doubleNumber));
                    }
                    else if (line.StartsWith("2")) {
                        var boolean = Boolean.Parse(line.Substring(1));
                        consts.Add(new BooleanValue(boolean));
                    }
                    else if (line.StartsWith("3")) {
                        var str = line.Substring(2, line.Length - 3);
                        consts.Add(new StringValue(str));
                    }
                }
                else {
                    var components = line.Split(' ').Select(Int32.Parse).ToList();
                    var instruction = (Instruction) components.First();

                    var opcode = new Opcode(instruction, components.Skip(1).ToArray());
                    instructions.Add(opcode);
                }
            }

            return Tuple.Create((IEnumerable<Opcode>)instructions, (IEnumerable<IValue>)consts);
        }
    }
}
