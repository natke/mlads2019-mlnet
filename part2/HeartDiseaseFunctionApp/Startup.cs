using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.ML;
using HeartDiseaseFunctionApp;

[assembly: WebJobsStartup(typeof(Startup))]
namespace HeartDiseaseFunctionApp
{
    class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddPredictionEnginePool<HeartData, HeartPrediction>()
                .FromFile("models/HeartClassification.zip");
        }
    }
}
