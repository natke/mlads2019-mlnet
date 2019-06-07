# MLADS 2019 ML.NET lab part 2

## Checkout the skeleton code

git checkout part2-skeleton

## Copy the model from part1

cp part1/HeartDiseaseModel/models/HeartClassification.zip part2/HeartDiseaseFunctionApp/models/HeartClassification.zip

## Open the project in Visual Studio

### Add prediction engine pool

Edit Startup.cs

    builder.Services.AddPredictionEnginePool<HeartData, HeartPrediction>()
        .FromFile("models/HeartClassification.zip");

### Add code to make a prediction to the function

Edit HeartDiseaseFunction.cs

### Parse HTTP Request Body

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    HeartData data = JsonConvert.DeserializeObject<HeartData>(requestBody);

### Make a prediction

    HeartPrediction prediction = _predictionEnginePool.Predict(data);

### Convert the prediction to a string

    string disease = Convert.ToBoolean(prediction.Prediction) ? "Positive" : "Negative";

### Return the result

    return (ActionResult)new OkObjectResult(disease);

## Run the function

Debug > Start without debugging

## Call the function

    curl -s -d "@data/heart-data.json" -X POST http://localhost:7071/api/PredictHeartDisease
