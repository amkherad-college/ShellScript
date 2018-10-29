using System;
using ShellScript.Core.Language.Library;

namespace ShellScript.Core.Language.CompilerServices
{
    public class ConstantInfo : IEquatable<ConstantInfo>
    {
        public TypeDescriptor TypeDescriptor { get; }

        public string Name { get; }
        //public string ReName { get; }

        public string AccessName => /*ReName ??*/ Name;

        public string Value { get; }


        public ConstantInfo(TypeDescriptor typeDescriptor, string name, string value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
            TypeDescriptor = typeDescriptor;
        }

        public ConstantInfo(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConstantInfo) obj);
        }

        public bool Equals(ConstantInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }
    }
}