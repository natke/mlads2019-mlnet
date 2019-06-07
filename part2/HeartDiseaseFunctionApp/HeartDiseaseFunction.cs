using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.ML;

namespace HeartDiseaseFunctionApp
{
    public class PredictHeartDisease
    {

        private readonly PredictionEnginePool<HeartData, HeartPrediction> _predictionEnginePool;

        // AnalyzeSentiment class constructor
        public PredictHeartDisease(PredictionEnginePool<HeartData, HeartPrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [FunctionName("PredictHeartDisease")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //Parse HTTP Request Body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            HeartData data = JsonConvert.DeserializeObject<HeartData>(requestBody);

            log.LogInformation("Heart Data: {0}", data);


            //Make Prediction
            HeartPrediction prediction = _predictionEnginePool.Predict(data);

            //Convert prediction to string
            string disease = Convert.ToBoolean(prediction.Prediction) ? "Positive" : "Negative";

            //Return Prediction
            return (ActionResult)new OkObjectResult(disease);
        }
    }
}
