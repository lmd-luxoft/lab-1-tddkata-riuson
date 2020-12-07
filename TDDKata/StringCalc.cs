using System;
using System.Collections.Generic;

namespace TDDKata {
    internal class StringCalc {
        internal int Calculate(string expression) {
            // 1 2 + 3 + 4 + 5 10 * 7 1 - + + 
            // 66
            if (string.IsNullOrWhiteSpace(expression)) {
                throw new StringCalcException("Expression is empty!");
            }

            var items = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var stack = new Stack<int>();

            foreach (var item in items) {
                if (item.Contains(".") || item.Contains(",")) {
                    throw new StringCalcException("Invalid operand (float)!");
                }

                if (int.TryParse(item, out var operand)) {
                    if (operand < 0) {
                        throw new StringCalcException("Negative operand!");
                    }

                    stack.Push(operand);
                    continue;
                }

                if (stack.Count < 2) {
                    throw new StringCalcException("Invalid expression!");
                }

                switch (item) {
                    case "+": {
                        var operand2 = stack.Pop();
                        var operand1 = stack.Pop();
                        var result = operand1 + operand2;
                        stack.Push(result);
                        break;
                    }
                    case "-": {
                        var operand2 = stack.Pop();
                        var operand1 = stack.Pop();
                        var result = operand1 - operand2;
                        stack.Push(result);
                        break;
                    }
                    case "*": {
                        var operand2 = stack.Pop();
                        var operand1 = stack.Pop();
                        var result = operand1 * operand2;
                        stack.Push(result);
                        break;
                    }
                    case "/": {
                        var operand2 = stack.Pop();
                        var operand1 = stack.Pop();
                        var result = operand1 / operand2;
                        stack.Push(result);
                        break;
                    }

                    default: {
                        throw new StringCalcException("Invalid expression!");
                    }
                }
            }

            if (stack.Count != 1) {
                throw new StringCalcException("Invalid expression!");
            }

            return stack.Pop();
        }

        //private IItem CreateItem(string value) {
        //    if (int.TryParse(value, out var integerValue)) {

        //    }
        //}
    }

    //internal interface IItem {
    //    string StringValue { get; }
    //    ItemType ItemType { get; }
    //}

    //internal enum ItemType {
    //    Operand,
    //    Operation
    //}

    //internal class Operand : IItem {

    //}
}
