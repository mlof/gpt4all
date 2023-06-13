using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Cmake
{
    [IsDependentOn(typeof(CleanupTask))]
    public class ConfigureTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var cmakeContext = context.CmakeContext;

            var cmakePath = context.Tools.Resolve("cmake.exe");
            var argumentBuilder = new ProcessArgumentBuilder();
            argumentBuilder.Append(" -G \"Visual Studio 17 2022\"");
            argumentBuilder.Append(" -A x64");
            argumentBuilder.Append(" -S " + cmakeContext.SourcePath.FullPath);
            argumentBuilder.Append(" -B " + cmakeContext.BuildPath.FullPath);

            context.ProcessRunner.Start(cmakePath,
                    new ProcessSettings
                    {
                        Arguments = argumentBuilder.Render(),
                        WorkingDirectory = cmakeContext.CmakeProjectPath.FullPath
                    })
                .WaitForExit();
        }
    }
}
