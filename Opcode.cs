using System.Collections.Generic;
using System.Linq;

namespace Speedycloud.Bytecode {
    public class Opcode {
        public static Opcode Of(Instruction instruction) {
            return new Opcode(instruction);
        }
        public Instruction Instruction { get; internal set; }
        public IReadOnlyList<int> OpArgs { get { return opargs.AsReadOnly(); } }
        private readonly List<int> opargs;

        public Opcode(Instruction instruction, params int[] args) {
            this.Instruction = instruction;
            this.opargs = new List<int>(args);
        }

        protected bool Equals(Opcode other) {
            return opargs.SequenceEqual(other.opargs) && Instruction == other.Instruction;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Opcode) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((opargs != null ? opargs.GetHashCode() : 0)*397) ^ (int) Instruction;
            }
        }

        public override string ToString() {
            return string.Format("{0} {1}", Instruction, string.Join(" ", opargs));
        }
    }
}
