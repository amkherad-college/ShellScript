namespace ShellScript.Core.Language.CompilerServices.Statements.Operators
{
    public class NegativeNumberOperator : ArithmeticOperator
    {
        public override StatementInfo Info { get; }
        public override OperatorAssociativity Associativity => OperatorAssociativity.RightToLeft;
        public override int Order => 1000;
        
        
        public NegativeNumberOperator(StatementInfo info)
        {
            Info = info;
        }
    }
}