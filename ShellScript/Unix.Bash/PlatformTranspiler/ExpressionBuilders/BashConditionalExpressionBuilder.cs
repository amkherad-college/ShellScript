using System;
using System.Reflection.Metadata;
using ShellScript.Core.Language.CompilerServices.Statements;
using ShellScript.Core.Language.CompilerServices.Statements.Operators;
using ShellScript.Core.Language.CompilerServices.Transpiling;
using ShellScript.Core.Language.CompilerServices.Transpiling.ExpressionBuilders;
using ShellScript.Core.Language.Library;

namespace ShellScript.Unix.Bash.PlatformTranspiler.ExpressionBuilders
{
    public class BashConditionalExpressionBuilder : BashDefaultExpressionBuilder
    {
        public new static BashConditionalExpressionBuilder Instance { get; } = new BashConditionalExpressionBuilder();
        
        public override bool ShouldBePinnedToFloatingPointVariable(Context context, Scope scope,
            DataTypes dataType, IStatement template)
        {
            if (template is ConstantValueStatement)
                return false;
            if (template is VariableAccessStatement)
                return false;
            if (template is FunctionCallStatement)
                return false;

            return dataType.IsNumericOrFloat();
        }

        public override bool ShouldBePinnedToFloatingPointVariable(Context context, Scope scope,
            DataTypes left, IStatement leftTemplate, DataTypes right, IStatement rightTemplate)
        {
            if (leftTemplate is ConstantValueStatement)
                return false;
            if (leftTemplate is VariableAccessStatement)
                return false;
            if (leftTemplate is FunctionCallStatement)
                return false;

            return left.IsNumericOrFloat();
        }
        
        public override string FormatLogicalExpression(Context context, Scope scope, DataTypes leftDataType, string left, IOperator op,
            DataTypes rightDataType, string right, IStatement template)
        {
            if (!(template is LogicalEvaluationStatement logicalEvaluationStatement))
                throw new InvalidOperationException();

            string opStr;

            if (!leftDataType.IsString() && !rightDataType.IsString())
            {
                switch (op)
                {
                    case EqualOperator _:
                    {
                        opStr = "-eq";
                        break;
                    }
                    case NotEqualOperator _:
                    {
                        opStr = "-ne";
                        break;
                    }
                    case GreaterOperator _:
                    {
                        opStr = "-gt";
                        break;
                    }
                    case GreaterEqualOperator _:
                    {
                        opStr = "-ge";
                        break;
                    }
                    case LessOperator _:
                    {
                        opStr = "-lt";
                        break;
                    }
                    case LessEqualOperator _:
                    {
                        opStr = "-le";
                        break;
                    }
                    default:
                        opStr = op.ToString();
                        break;
                }
            }
            else
            {
                opStr = op.ToString();
            }

            left = _createStringExpression(left, logicalEvaluationStatement.Left);
            right = _createStringExpression(right, logicalEvaluationStatement.Right);
            
            return $"[ {left} {opStr} {right} ]";
        }

        private string _createStringExpression(string exp, IStatement statement)
        {
            if (statement is ConstantValueStatement _)
            {
                return exp;
            }
            
            if (statement is VariableAccessStatement _)
            {
                return exp;
            }

            return $"$(({exp}))";
        }
    }
}