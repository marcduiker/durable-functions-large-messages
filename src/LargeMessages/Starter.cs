using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace LargeMessages
{
    public class Starter
    {
        [FunctionName(nameof(Starter))]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "start/{orchestrationName}")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClientBase orchestrationClient,
            string orchestrationName,

            ILogger log)
        {
            dynamic orchestrationInput = await req.Content.ReadAsAsync<object>();

            // Start a new orchestration and let Durable Functions generate the instance id.
            var instanceId = await orchestrationClient.StartNewAsync(
                    orchestrationName,
                    orchestrationInput);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'...");

            return orchestrationClient.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
