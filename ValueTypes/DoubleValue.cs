using Speedycloud.Runtime.ValueTypes;

namespace Speedycloud.Bytecode.ValueTypes {
    public class DoubleValue : IValue {
        public ValueType Type {
            get { return ValueType.Double; }
        }

        public long Integer { get {throw new ValueException(ValueType.Double, ValueType.Integer);} }

        public double Double { get; private set; }

        public bool Boolean {
            get { throw new ValueException(ValueType.Double, ValueType.Boolean); }
        }

        public override string ToString() {
            return string.Format("(Double: {0})", Double);
        }

        public string String {
            get { throw new ValueException(ValueType.Double, ValueType.String); }
        }

        public ArrayValue Array {
            get { throw new ValueException(ValueType.Double, ValueType.Array); }
        }

        public ComplexValue Complex {
            get { throw new ValueException(ValueType.Double, ValueType.Complex); }
        }

        public DoubleValue(double value) {
            Double = value;
        }

        protected bool Equals(DoubleValue other) {
            return Double.Equals(other.Double);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DoubleValue) obj);
        }

        public override int GetHashCode() {
            return Double.GetHashCode();
        }
    }
}
