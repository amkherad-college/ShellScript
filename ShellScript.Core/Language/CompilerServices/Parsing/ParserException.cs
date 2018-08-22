using System;

namespace ShellScript.Core.Language.CompilerServices.Parsing
{
    public class ParserException : Exception
    {
        public ParserException(string message)
            : base(message)
        {
        }
        public ParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}