using System;
using ShellScript.Core.Language.CompilerServices.Statements;
using ShellScript.Core.Language.Library;

namespace ShellScript.Core.Language.CompilerServices.CompilerErrors
{
    public class InvalidOperatorForTypeCompilerException : CompilerException
    {
        public InvalidOperatorForTypeCompilerException(Type operatorType, DataTypes dataType, StatementInfo statementInfo)
            : base(CreateMessage(operatorType, dataType), statementInfo)
        {
        }
        public InvalidOperatorForTypeCompilerException(Type operatorType, DataTypes a, DataTypes b, StatementInfo statementInfo)
            : base(CreateMessage(operatorType, a, b), statementInfo)
        {
        }

        public InvalidOperatorForTypeCompilerException(string message, StatementInfo statementInfo)
            : base(message, statementInfo)
        {
        }
        
//        public InvalidOperatorForTypeCompilerException(string message)
//            : base(message)
//        {
//        }
//
//        public InvalidOperatorForTypeCompilerException(string message, Exception innerException) : base(message, innerException)
//        {
//        }
//
//        public InvalidOperatorForTypeCompilerException(Exception innerException) : base(innerException)
//        {
//        }

        public static string CreateMessage(Type operatorType, DataTypes a, DataTypes b)
        {
            return $"Invalid operator '{operatorType.Name}' on variables of type '{a}' and '{b}'.";
        }

        public static string CreateMessage(Type operatorType, DataTypes dataType)
        {
            return $"Invalid operator '{operatorType.Name}' on variable of type '{dataType}'.";
        }
        
        public static string CreateMessageForConstant(Type operatorType)
        {
            return $"Invalid operator '{operatorType.Name}' on constant value.";
        }
    }
}