using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDKata {
    public class StringCalc {
        private readonly Dictionary<string, IOperation> _operations;

        internal StringCalc(IEnumerable<IOperation> operations) {
            this._operations = operations.ToDictionary(x => x.Symbol);
        }

        internal int Calculate(string expression) {
            var stack = new Stack<int>();

            foreach (var item in this.EnumerateTokens(expression)) {
                if (this._operations.TryGetValue(item, out var operation)) {
                    if (stack.Count >= operation.OperandsCount) {
                        operation.Process(stack);
                        continue;
                    }
                }

                if (int.TryParse(item, out var operand)) {
                    stack.Push(operand);
                    continue;
                }

                throw new StringCalcException("Unknown token!");
            }

            if (stack.Count != 1) {
                throw new StringCalcException("Invalid expression!");
            }

            return stack.Pop();
        }

        internal IEnumerable<string> EnumerateTokens(string expression) {
            if (string.IsNullOrWhiteSpace(expression)) {
                throw new StringCalcException("Expression is empty!");
            }

            var items = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return items;
        }
    }

    public static class CalculatorFactory {
        public static StringCalc Create() {
            return new StringCalc(new IOperation[] {
                new AddOperation(),
                new SubtractOperation(),
                new MultiplyOperation(),
                new DivideOperation()
            });
        }
    }

    internal interface IOperation {
        string Symbol { get; }
        int OperandsCount { get; }
        void Process(Stack<int> stack);
    }

    internal class AddOperation : IOperation {
        public string Symbol => "+";
        public int OperandsCount => 2;

        public void Process(Stack<int> stack) {
            var operand2 = stack.Pop();
            var operand1 = stack.Pop();
            stack.Push(operand1 + operand2);
        }
    }

    internal class SubtractOperation : IOperation {
        public string Symbol => "-";
        public int OperandsCount => 2;

        public void Process(Stack<int> stack) {
            var operand2 = stack.Pop();
            var operand1 = stack.Pop();
            stack.Push(operand1 - operand2);
        }
    }

    internal class MultiplyOperation : IOperation {
        public string Symbol => "*";
        public int OperandsCount => 2;

        public void Process(Stack<int> stack) {
            var operand2 = stack.Pop();
            var operand1 = stack.Pop();
            stack.Push(operand1 * operand2);
        }
    }

    internal class DivideOperation : IOperation {
        public string Symbol => "/";
        public int OperandsCount => 2;

        public void Process(Stack<int> stack) {
            var operand2 = stack.Pop();
            var operand1 = stack.Pop();

            if (operand2 == 0) {
                throw new StringCalcException("Division by zero!");
            }

            stack.Push(operand1 / operand2);
        }
    }
}
