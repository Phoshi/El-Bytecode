using System.Linq;
using Speedycloud.Runtime.ValueTypes;

namespace Speedycloud.Bytecode.ValueTypes {
    public class StringValue : IValue {
        public ValueType Type { get {return ValueType.String;} }
        public long Integer { get { throw new ValueException(ValueType.String, ValueType.Integer); } }
        public double Double { get { throw new ValueException(ValueType.String, ValueType.Double); } }
        public bool Boolean { get { throw new ValueException(ValueType.String, ValueType.Boolean); } }
        public string String { get; private set; }
        public ArrayValue Array { get { return new ArrayValue(String.Select<char, IValue>(c=>new IntValue(c)).ToArray());} }
        public ComplexValue Complex { get { return new ComplexValue(new IValue[]{new IntValue(String.Length)});} }

        public StringValue(string str) {
            String = str;
        }

        public override string ToString() {
            return string.Format("(String {0})", String);
        }

        protected bool Equals(StringValue other) {
            return string.Equals(String, other.String);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StringValue) obj);
        }

        public override int GetHashCode() {
            return (String != null ? String.GetHashCode() : 0);
        }
    }
}
