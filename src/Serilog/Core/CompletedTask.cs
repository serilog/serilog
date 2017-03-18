using System.Threading.Tasks;

namespace Serilog.Core
{
    class CompletedTask
    {
        public static readonly Task Instance = Task.FromResult((object) null);
    }
}
