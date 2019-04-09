using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LargeMessages
{
    public class LargeMessageActivity
    {
        [FunctionName(nameof(LargeMessageActivity))]
        public async Task<string> Run(
            [ActivityTrigger] int nrOfChars,
            ILogger logger)
        {
            return await Task.FromResult(new string('X', nrOfChars));
        }
    }
}
