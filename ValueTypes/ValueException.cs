using System;
using ValueType = Speedycloud.Bytecode.ValueTypes.ValueType;

namespace Speedycloud.Runtime.ValueTypes {
    internal class ValueException : Exception {
        public ValueException(ValueType expected, ValueType actual) : 
            base(string.Format("Incorrect value type found! Expected {0} but got {1}", expected, actual)) {}
    }
}