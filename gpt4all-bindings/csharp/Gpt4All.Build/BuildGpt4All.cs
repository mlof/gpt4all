using Build.Cmake;
using Cake.Frosting;

namespace Build
{
    [IsDependentOn(typeof(CleanupTask))]
    [IsDependentOn(typeof(ConfigureTask))]
    [IsDependentOn(typeof(BuildTask))]
    [IsDependentOn(typeof(CopyTask))]
    public class BuildGpt4All : FrostingTask<BuildContext>
    {
    }
}
