using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Cmake
{
    [IsDependentOn(typeof(BuildTask))]
    public class CopyTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var cmakeContext = context.CmakeContext;

            var dllDirectoryPath = cmakeContext.BuildPath.Combine("bin").Combine(context.Config);

            foreach (var dll in context.GetFiles(dllDirectoryPath.Combine("*.dll").FullPath))
            {
                context.CopyFileToDirectory(dll, cmakeContext.OutputPath);
            }
        }
    }
}
