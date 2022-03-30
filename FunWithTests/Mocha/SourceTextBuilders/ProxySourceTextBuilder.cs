using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mocha.Helpers;

namespace Mocha.SourceTextBuilders
{
    internal static class ProxySourceTextBuilder
    {
        public static string GetContent(InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            var interfaceName = interfaceDeclarationSyntax.Identifier.Text;
            var fullInterfaceName = GetInterfaceFullName(interfaceDeclarationSyntax);

            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("");
            sb.AppendLine("namespace Mocha");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {interfaceName}Proxy : {fullInterfaceName}");
            sb.AppendLine("    {");
            sb.AppendLine("        private readonly Dictionary<string, Func<object>> _methodInterceptors;");
            sb.AppendLine("");
            sb.AppendLine($"        public {interfaceName}Proxy(Dictionary<string, Func<object>> methodInterceptors)");
            sb.AppendLine("        {");
            sb.AppendLine("            _methodInterceptors = methodInterceptors;");
            sb.AppendLine("        }");

            foreach (var member in interfaceDeclarationSyntax.Members)
            {
                AppendMember(sb, member);
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void AppendMember(StringBuilder sb, MemberDeclarationSyntax member)
        {
            sb.AppendLine("");
            if (member is MethodDeclarationSyntax methodDeclarationSyntax)
            {
                sb.AppendLine($"        public {methodDeclarationSyntax.ToString().Trim(';')}");
                sb.AppendLine("        {");
                sb.AppendLine($"            var result = _methodInterceptors[nameof({methodDeclarationSyntax.Identifier.Text})].Invoke();");
                sb.AppendLine("");
                if (methodDeclarationSyntax.ReturnType.ToString() != "void")
                {
                    sb.AppendLine($"            return ({methodDeclarationSyntax.ReturnType})result;");
                }
                sb.AppendLine("        }");
            }
            sb.AppendLine("");
        }

        private static string GetInterfaceFullName(InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            if (!SyntaxNodeHelper.TryGetParentSyntax(interfaceDeclarationSyntax, out NamespaceDeclarationSyntax namespaceDeclarationSyntax))
            {
                return interfaceDeclarationSyntax.Identifier.Text;
            }

            return $"{namespaceDeclarationSyntax.Name}.{interfaceDeclarationSyntax.Identifier.Text}";
        }
    }
}
