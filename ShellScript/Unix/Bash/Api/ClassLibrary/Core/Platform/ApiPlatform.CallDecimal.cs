using ShellScript.Core.Language.CompilerServices.Statements;
using ShellScript.Core.Language.CompilerServices.Transpiling.ExpressionBuilders;
using ShellScript.Core.Language.Library;
using ShellScript.Unix.Bash.Api.ClassLibrary.Base;

namespace ShellScript.Unix.Bash.Api.ClassLibrary.Core.Platform
{
    public partial class ApiPlatform
    {
        public class CallDecimal : Call
        {
            public override string Name => nameof(CallDecimal);
            public override string Summary { get; }
            public override DataTypes DataType => DataTypes.Decimal;

            
        }
    }
}