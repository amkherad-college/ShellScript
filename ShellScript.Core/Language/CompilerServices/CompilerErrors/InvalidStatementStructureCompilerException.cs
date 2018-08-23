using System;
using ShellScript.Core.Language.CompilerServices.Statements;

namespace ShellScript.Core.Language.CompilerServices.CompilerErrors
{
    public class InvalidStatementStructureCompilerException : CompilerException
    {
        public InvalidStatementStructureCompilerException(IStatement statement, StatementInfo info)
            : base(CreateMessage(statement, info))
        {
        }
        
        public InvalidStatementStructureCompilerException(string message) : base(message)
        {
        }

        public InvalidStatementStructureCompilerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidStatementStructureCompilerException(Exception innerException) : base(innerException)
        {
        }

        public static string CreateMessage(IStatement statement, StatementInfo info)
        {
            return
                $"Invalid statement of type '{statement.GetType().Name}' was found in '{info.FilePath}' at {info.LineNumber}:{info.ColumnNumber}.";
        }

        public static string CreateMessage(IStatement statement)
        {
            return
                $"Invalid statement of type '{statement.GetType().Name}' was found";
        }
    }
}