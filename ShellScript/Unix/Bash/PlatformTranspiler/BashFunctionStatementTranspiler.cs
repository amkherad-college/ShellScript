using System;
using System.Globalization;
using System.IO;
using ShellScript.Core.Language.Compiler;
using ShellScript.Core.Language.Compiler.CompilerErrors;
using ShellScript.Core.Language.Compiler.Statements;
using ShellScript.Core.Language.Compiler.Transpiling;
using ShellScript.Core.Language.Compiler.Transpiling.BaseImplementations;

namespace ShellScript.Unix.Bash.PlatformTranspiler
{
    public class BashFunctionStatementTranspiler : FunctionStatementTranspilerBase
    {
        public override void WriteInline(Context context, Scope scope, TextWriter writer, TextWriter metaWriter,
            TextWriter nonInlinePartWriter, IStatement statement)
        {
            throw new NotSupportedException();
        }

        public override void WriteBlock(Context context, Scope scope, TextWriter writer, TextWriter metaWriter,
            IStatement statement)
        {
            if (!(statement is FunctionStatement funcDefStt)) throw new InvalidOperationException();

            var functionName = funcDefStt.Name;

            if (scope.IsIdentifierExists(functionName))
            {
                throw new IdentifierNameExistsCompilerException(functionName, funcDefStt.Info);
            }

            var funcScope = scope.BeginNewScope(ScopeType.MethodRoot);
            IStatement inlinedStatement = null;

            funcScope.SetConfig(c => c.ExplicitEchoStream, context.Flags.DefaultExplicitEchoStream);

            if (context.Flags.UseComments && context.Flags.CommentParameterInfos)
            {
                BashTranspilerHelpers.WriteComment(writer, $"! {funcDefStt.TypeDescriptor} {functionName}");
            }

            if (funcDefStt.Parameters != null && funcDefStt.Parameters.Length > 0)
            {
                for (var i = 0; i < funcDefStt.Parameters.Length; i++)
                {
                    var param = funcDefStt.Parameters[i];
                    var paramMappedName = (i + 1).ToString(CultureInfo.InvariantCulture);
                    funcScope.ReserveNewParameter(param.TypeDescriptor, param.Name, paramMappedName);

                    if (context.Flags.UseComments && context.Flags.CommentParameterInfos)
                    {
                        BashTranspilerHelpers.WriteComment(writer,
                            $"\\param ${paramMappedName} {param.TypeDescriptor} - {param.Name}");
                    }
                }
            }

            writer.WriteLine($"function {functionName}() {{");

            BashBlockStatementTranspiler.WriteBlockStatement(context, funcScope, writer, metaWriter,
                funcDefStt.Statement, ScopeType.MethodRoot, false, typeof(ReturnStatement));
            TryGetInlinedStatement(context, funcScope, funcDefStt, out inlinedStatement);


            var func = new FunctionInfo(funcDefStt.TypeDescriptor, functionName, null, null, funcDefStt.IsParams,
                funcDefStt.Parameters, inlinedStatement);

            scope.ReserveNewFunction(func);

            writer.WriteLine("}");

            scope.IncrementStatements();
        }
    }
}