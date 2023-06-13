using System.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Tool;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using ClangSharp;

namespace Build
{
    [TaskName("Default")]
    [IsDependentOn(typeof(GenerateBindingsTask))]
    //[IsDependentOn(typeof(BuildGpt4All))]
    public class DefaultTask : FrostingTask<BuildContext>
    {
    }

    public class GenerateBindingsTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.Log.Information("Generating bindings...");

            var clangSharpPInvokeGeneratorPath = context.Tools.Resolve("ClangSharpPInvokeGenerator.exe");

            ArgumentException.ThrowIfNullOrEmpty(clangSharpPInvokeGeneratorPath.FullPath);
            var arguments = new ProcessArgumentBuilder();

            arguments.Append(" --config latest-codegen");
            arguments.Append(" --config exclude-fnptr-codegen");
            arguments.Append(" --config generate-helper-types");
            arguments.Append(" --with-access-specifier *=Public");
            arguments.Append(" --include-directory " +
                                          context.RepositoryRoot.Combine("gpt4all-backend").FullPath);
            arguments.Append(" --file " + context.RepositoryRoot.Combine("gpt4all-backend")
                .CombineWithFilePath("llmodel_c.h").FullPath);
            arguments.Append(" --libraryPath libllmodel");
            arguments.Append(" --remap sbyte*=IntPtr void*=IntPtr");
            arguments.Append(" --namespace Gpt4All.Bindings");
            arguments.Append(" --methodClassName NativeMethods");
            arguments.Append(" --output " +
                                          context.SolutionRoot.Combine("bindings")
                                              .CombineWithFilePath("NativeMethods.cs"));
            arguments.Append(" --output-mode CSharp");

            context.ProcessRunner.Start(clangSharpPInvokeGeneratorPath,
                new ProcessSettings()
                {
                    WorkingDirectory = context.SolutionRoot.Combine("Gpt4All"),
                    Arguments = arguments.Render()
                }
            );
        }
    }
}
