using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speedycloud.Bytecode;
using Speedycloud.Bytecode.ValueTypes;

namespace BytecodeTests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void Dump() {
            var consts = new List<IValue> {
                new IntValue(3),
                new DoubleValue(2.5),
                new BooleanValue(true),
                new StringValue("Hello, world!")
            };
            var instructions = new List<Opcode> {
                new Opcode(Instruction.LOAD_CONST, 0),
                new Opcode(Instruction.LOAD_CONST, 1),
                new Opcode(Instruction.BINARY_ADD),
                new Opcode(Instruction.CALL_FUNCTION, 0, 1)
            };

            var serialiser = new BytecodeSerialiser();

            Assert.AreEqual(
@"03
12.5
2True
3""Hello, world!""

26 0
26 1
2
21 0 1
".Trim(), serialiser.Dump(instructions, consts).Trim());
        }

        [TestMethod]
        public void Load() {
            var str = @"03
12.5
2True
3""Hello, world!""

26 0
26 1
2
21 0 1
";
            var serialiser = new BytecodeSerialiser();
            var result = serialiser.Load(str);

            var consts = new List<IValue> {
                new IntValue(3),
                new DoubleValue(2.5),
                new BooleanValue(true),
                new StringValue("Hello, world!")
            };
            var instructions = new List<Opcode> {
                new Opcode(Instruction.LOAD_CONST, 0),
                new Opcode(Instruction.LOAD_CONST, 1),
                new Opcode(Instruction.BINARY_ADD),
                new Opcode(Instruction.CALL_FUNCTION, 0, 1)
            };

            Assert.IsTrue(result.Item1.SequenceEqual(instructions));
            Assert.IsTrue(result.Item2.SequenceEqual(consts));
        }
    }
}
