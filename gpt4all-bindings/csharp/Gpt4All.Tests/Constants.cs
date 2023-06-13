using System.IO;
using Microsoft.Extensions.Configuration;

namespace Gpt4All.Tests
{
    public static class Constants
    {
        public static readonly string MODELS_BASE_DIR;
        public static readonly string LLAMA_MODEL_PATH = Path.Join(MODELS_BASE_DIR, "ggml-gpt4all-l13b-snoozy.bin");
        public static readonly string GPTJ_MODEL_PATH = Path.Join(MODELS_BASE_DIR, "ggml-gpt4all-j-v1.3-groovy.bin");
        public static readonly string MPT_MODEL_PATH = Path.Join(MODELS_BASE_DIR, "ggml-mpt-7b-chat.bin");

        static Constants()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.local.json", true, true)
                .Build();

            MODELS_BASE_DIR = configuration["MODELS_BASE_DIR"] ?? throw new InvalidOperationException();
        }
    }
}
