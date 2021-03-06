namespace ShellScript.Core.Language.Compiler.Statements.Operators
{
    public abstract class ArithmeticOperator : IOperator
    {
        public bool CanBeEmbedded => false;
        public abstract StatementInfo Info { get; }
        public abstract OperatorAssociativity Associativity { get; }
        public abstract int Order { get; }
        
        public IStatement[] TraversableChildren => StatementHelpers.EmptyStatements;
    }
}