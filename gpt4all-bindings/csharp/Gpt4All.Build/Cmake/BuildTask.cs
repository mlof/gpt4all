using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Cmake
{
    [IsDependentOn(typeof(ConfigureTask))]
    public class BuildTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var cmakeContext = context.CmakeContext;

            var cmakePath = context.Tools.Resolve("cmake.exe");
            var buildArgumentBuilder = new ProcessArgumentBuilder();

            buildArgumentBuilder.Append(" --build " + cmakeContext.BuildPath.FullPath);
            buildArgumentBuilder.Append(" --parallel");
            buildArgumentBuilder.Append(" --config " + context.Config);

            context.ProcessRunner.Start(cmakePath,
                new ProcessSettings
                {
                    Arguments = buildArgumentBuilder.Render(),
                    WorkingDirectory = cmakeContext.CmakeProjectPath.FullPath
                }).WaitForExit();
        }
    }
}
