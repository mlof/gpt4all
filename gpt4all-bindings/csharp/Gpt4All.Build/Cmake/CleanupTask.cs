using Cake.Common.IO;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Cmake
{
    public class CleanupTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var cmakeContext = context.CmakeContext;
            if (context.FileSystem.Exist(cmakeContext.RuntimePath))
            {
                context.DeleteDirectory(cmakeContext.RuntimePath,
                    new EnsureDirectoryDoesNotExistSettings { Recursive = true, Force = true });
            }

            context.EnsureDirectoryExists(cmakeContext.CmakeProjectPath);
        }
    }
}
