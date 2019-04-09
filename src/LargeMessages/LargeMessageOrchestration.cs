using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LargeMessages
{
    public class LargeMessageOrchestration
    {
        [FunctionName(nameof(LargeMessageOrchestration))]
        public async Task<string> Run(
            [OrchestrationTrigger]DurableOrchestrationContextBase context,
            ILogger log)
        {
            var nrOfChars = context.GetInput<int>();

            var result = await context.CallActivityAsync<string>(
                nameof(LargeMessageActivity),
                nrOfChars);

            return result;
        }
    }
}
