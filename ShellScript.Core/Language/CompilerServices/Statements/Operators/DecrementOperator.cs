namespace ShellScript.Core.Language.CompilerServices.Statements.Operators
{
    public class DecrementOperator : ArithmeticOperator
    {
        public override int Order => 65;
        public override OperatorAssociativity Associativity => OperatorAssociativity.ContextChangeable;
    }
}