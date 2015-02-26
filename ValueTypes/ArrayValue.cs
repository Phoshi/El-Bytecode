using System.Collections.Generic;
using System.Linq;
using Speedycloud.Runtime.ValueTypes;

namespace Speedycloud.Bytecode.ValueTypes {
    public class ArrayValue : IValue {
        public IReadOnlyList<IValue> Contents { get { return new List<IValue>(arr).AsReadOnly();} }
        private readonly IValue[] arr;

        public ArrayValue(IValue[] arr) {
            this.arr = arr;
        }

        public ArrayValue Update(long pos, IValue val) {
            var newArr = new List<IValue>(arr);
            newArr[(int) pos] = val;
            return new ArrayValue(newArr.ToArray());
        }

        public ValueType Type { get {return ValueType.Array;} }
        public long Integer { get { throw new ValueException(ValueType.Array, ValueType.Integer); } }
        public double Double { get { throw new ValueException(ValueType.Array, ValueType.Double); } }
        public bool Boolean { get { throw new ValueException(ValueType.Array, ValueType.Boolean); } }

        public override string ToString() {
            return string.Format("(Array {0})", string.Join(", ", arr.Select(x=>x.ToString())));
        }

        public string String {
            get {
                if (arr.Any(elem=>elem.Type != ValueType.Integer))
                    throw new ValueException(ValueType.Array, ValueType.String);
                return arr.Select(elem => (char) elem.Integer).ToString();
            }
        }

        public ArrayValue Array { get { return this; } }
        public ComplexValue Complex { get { return new ComplexValue(new IValue[]{new IntValue(arr.Length)});} }
    }
}