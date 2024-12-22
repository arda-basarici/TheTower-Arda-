using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class LifecycleListenerGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // Register for syntax notifications
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;

        // Step 1: Find the parent interface directly
        var parentInterface = compilation.GetTypeByMetadataName("Game.IGameStateListener");

        if (parentInterface == null)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor(
                    "L001",
                    "LifecycleListenerGenerator",
                    "Parent interface IGameStateListener not found",
                    "Generator",
                    DiagnosticSeverity.Warning,
                    true
                ),
                Location.None));
            return;
        }

        // Step 2: Find child interfaces that inherit from the parent
        var childInterfaces = new List<INamedTypeSymbol>();
        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            var root = syntaxTree.GetRoot();

            var interfaceDeclarations = root.DescendantNodes()
                                            .OfType<InterfaceDeclarationSyntax>();

            foreach (var interfaceDeclaration in interfaceDeclarations)
            {
                var interfaceSymbol = semanticModel.GetDeclaredSymbol(interfaceDeclaration) as INamedTypeSymbol;
                if (interfaceSymbol != null && interfaceSymbol.AllInterfaces.Contains(parentInterface))
                {
                    childInterfaces.Add(interfaceSymbol);
                }
            }
        }

        // Step 3: Generate registration code for each child interface
        var generatedCode = new StringBuilder();
        generatedCode.AppendLine("// Auto-generated code by LifecycleListenerGenerator");
        generatedCode.AppendLine("using System;");
        generatedCode.AppendLine("namespace Game");
        generatedCode.AppendLine("{");
        generatedCode.AppendLine("    public static class LifecycleManager");
        generatedCode.AppendLine("    {");
        generatedCode.AppendLine("        public static void RegisterListeners()");
        generatedCode.AppendLine("        {");

        foreach (var childInterface in childInterfaces)
        {
            generatedCode.AppendLine($"            LifecycleManager.Register(typeof({childInterface.Name}));");
        }

        generatedCode.AppendLine("        }");
        generatedCode.AppendLine("    }");
        generatedCode.AppendLine("}");

        // Step 4: Add the generated code to the context
        context.AddSource("LifecycleListener.g.cs", SourceText.From(generatedCode.ToString(), Encoding.UTF8));
    }

}
