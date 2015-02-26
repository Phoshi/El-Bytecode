using Speedycloud.Runtime.ValueTypes;

namespace Speedycloud.Bytecode.ValueTypes {
    public class BooleanValue : IValue {
        public ValueType Type { get { return ValueType.Boolean; } }
        public long Integer {
            get { throw new ValueException(ValueType.Boolean, ValueType.Integer); }
        }
        public double Double { get { throw new ValueException(ValueType.Boolean, ValueType.Double); } }
        public bool Boolean { get; private set; }
        public string String { get { throw new ValueException(ValueType.Boolean, ValueType.String); } }

        public override string ToString() {
            return string.Format("(Boolean: {0})", Boolean);
        }

        public ArrayValue Array { get { throw new ValueException(ValueType.Boolean, ValueType.Array); } }
        public ComplexValue Complex { get { throw new ValueException(ValueType.Boolean, ValueType.Complex); } }

        public BooleanValue(bool value) {
            Boolean = value;
        }

        protected bool Equals(BooleanValue other) {
            return Boolean.Equals(other.Boolean);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BooleanValue) obj);
        }

        public override int GetHashCode() {
            return Boolean.GetHashCode();
        }
    }
}
