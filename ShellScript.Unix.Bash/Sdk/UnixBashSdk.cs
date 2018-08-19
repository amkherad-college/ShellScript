using ShellScript.Core.Language.Sdk;

namespace ShellScript.Unix.Bash.Sdk
{
    public class UnixBashSdk : ISdk
    {
        public string Name => "Bash";
        public string OutputFileExtension => "sh";


        public bool TryGetClass(string className, out ISdkClass result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetGeneralFunction(string functionName, out ISdkFunc result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetGeneralVariable(string variableName, out ISdkVariable result)
        {
            throw new System.NotImplementedException();
        }
    }
}