using System.IO;
using System.Text.RegularExpressions;
using Build.Cmake;
using Cake.Common;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
    public partial class BuildContext : FrostingContext
    {
        public BuildContext(ICakeContext context)
            : base(context)
        {
            Config = context.Argument("configuration", "Release");

            var workingDirectory = context.Environment.WorkingDirectory;
            RepositoryRoot = GetRepositoryRoot(context, workingDirectory);
            SolutionRoot = RepositoryRoot.Combine("gpt4all-bindings").Combine("csharp");

            CmakeContext = new CmakeContext(context, RepositoryRoot, SolutionRoot);
        }

        public CmakeContext CmakeContext { get; init; }

        public string Config { get; init; }

        public DirectoryPath SolutionRoot { get; init; }

        public DirectoryPath RepositoryRoot { get; init; }

        [GeneratedRegex(@"^[a-zA-Z]:\\$")]
        private partial Regex WindowsRootPathRegex();

        private DirectoryPath GetRepositoryRoot(ICakeContext context, DirectoryPath workingDirectory)
        {
            var directory = workingDirectory;
            while (directory != null)
            {
                // break if we are at the root
                if (directory.FullPath == "/" || WindowsRootPathRegex().IsMatch(directory.FullPath))
                {
                    throw new DirectoryNotFoundException("Could not find repository root");
                }

                var gitDirectory = directory.Combine(".git");
                if (context.FileSystem.Exist(gitDirectory))
                {
                    return directory;
                }

                directory = directory.GetParent();
            }

            throw new DirectoryNotFoundException("Could not find repository root");
        }
    }
}
