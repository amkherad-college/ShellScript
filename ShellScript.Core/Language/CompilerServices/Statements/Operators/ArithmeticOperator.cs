namespace ShellScript.Core.Language.CompilerServices.Statements.Operators
{
    public abstract class ArithmeticOperator : IOperator
    {
        public bool IsBlockStatement => false;
        public abstract OperatorAssociativity Associativity { get; }
        public abstract int Order { get; }
    }
}