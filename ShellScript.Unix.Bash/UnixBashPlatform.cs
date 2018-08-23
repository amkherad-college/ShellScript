using ShellScript.Core.Language;
using ShellScript.Core.Language.CompilerServices.Transpiling;
using ShellScript.Core.Language.Sdk;
using ShellScript.Unix.Bash.Sdk;

namespace ShellScript.Unix.Bash
{
    public class UnixBashPlatform : IPlatform
    {
        public string Name => "Unix-Bash";

        public string[] CompilerConstants { get; } =
        {
            "unix",
            "bash",
        };

        public ISdk Sdk { get; } = new UnixBashSdk();

        public IPlatformStatementTranspiler[] Transpilers { get; } =
        {
            //new IfElseTranspiler()
        };
    }
}