using Cake.Core;
using Cake.Core.IO;

namespace Build.Cmake
{
    public class CmakeContext
    {
        public CmakeContext(ICakeContext context, DirectoryPath repositoryRoot, DirectoryPath solutionRoot)
        {
            RuntimePath = solutionRoot.Combine("runtimes");
            CmakeProjectPath = RuntimePath.Combine("win-x64").Combine("msvc");
            BuildPath = RuntimePath.Combine("win-x64").Combine("msvc").Combine("build");
            SourcePath = repositoryRoot.Combine("gpt4all-backend");
            OutputPath = RuntimePath.Combine("win-x64");
        }

        public DirectoryPath OutputPath { get; set; }

        public DirectoryPath SourcePath { get; set; }

        public DirectoryPath BuildPath { get; set; }

        public DirectoryPath CmakeProjectPath { get; set; }

        public DirectoryPath RuntimePath { get; set; }
    }
}
