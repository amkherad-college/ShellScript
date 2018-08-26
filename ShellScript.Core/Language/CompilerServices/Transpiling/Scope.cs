using System.Collections.Generic;
using ShellScript.Core.Language.Sdk;

namespace ShellScript.Core.Language.CompilerServices.Transpiling
{
    public class Scope
    {
        public Context Context { get; }
        public Scope Parent { get; }

        public bool IsRootScope => Parent == null;

        private readonly HashSet<string> _identifiers;
        private readonly HashSet<ConstantInfo> _constants;
        private readonly HashSet<VariableInfo> _variables;


        public Scope(Context context)
        {
            Context = context;

            _identifiers = new HashSet<string>();
            _variables = new HashSet<VariableInfo>();
        }

        public Scope(Context context, Scope parent)
        {
            Context = context;
            Parent = parent;

            _identifiers = new HashSet<string>();
            _variables = new HashSet<VariableInfo>();
        }

        public Scope Fork()
        {
            return new Scope(Context, this);
        }


        public bool IsVariableExists(string name)
        {
            var that = this;
            do
            {
                if (that._variables.Contains(new VariableInfo(DataTypes.Variant, name)))
                {
                    return true;
                }

                if (that._identifiers.Contains(name))
                {
                    return true;
                }
                
            } while ((that = that.Parent) != null);

            return false;
        }

        public string ReserveNewVariable(DataTypes dataType, string nameHint)
        {
            var counter = 1;
            var varName = nameHint;
            while (IsVariableExists(varName))
            {
                varName = nameHint + counter++;
            }

            _identifiers.Add(varName);
            _variables.Add(new VariableInfo(dataType, varName));

            return varName;
        }

        public string ReserveNewVariableIfNotExists(DataTypes dataType, string nameHint)
        {
            if (IsVariableExists(nameHint))
            {
                return nameHint;
            }

            _identifiers.Add(nameHint);
            _variables.Add(new VariableInfo(dataType, nameHint));

            return nameHint;
        }

        public bool TryGetVariableInfo(string variableName, out VariableInfo variableInfo)
        {
            var varInfo = new VariableInfo(DataTypes.Variant, variableName);

            var that = this;
            do
            {
                if (that._variables.TryGetValue(varInfo, out variableInfo))
                {
                    return true;
                }
                
            } while ((that = that.Parent) != null);

            return false;
        }

        public bool TryGetConstantInfo(string constantName, out ConstantInfo constantInfo)
        {
            var varInfo = new ConstantInfo(DataTypes.Variant, constantName);

            var that = this;
            do
            {
                if (that._constants.TryGetValue(varInfo, out constantInfo))
                {
                    return true;
                }
                
            } while ((that = that.Parent) != null);

            return false;
        }
    }
}