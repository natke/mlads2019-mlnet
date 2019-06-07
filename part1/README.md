# MLADS 2019 ML.NET lab part 1

## Clone skeleton code

git clone https://github.com/natke/mlads2019-mlnet.git

git checkout part1-skeleton

## Edit project

Load HeartDeseaseModel into editor

## Add code to load data

    IDataView trainingDataView = mlContext.Data.LoadFromTextFile<HeartData>(TrainDataPath, hasHeader: true, separatorChar: ';');

## Create the training pipeline

    var pipeline = mlContext.Transforms.Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                .Append(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));

## Train the model

    ITransformer trainedModel = pipeline.Fit(trainingDataView);

## Evaluate the model

    IDataView testDataView = mlContext.Data.LoadFromTextFile<HeartData>(TestDataPath, hasHeader: true, separatorChar: ';');
    IDataView predictions = trainedModel.Transform(testDataView);

    var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");

## Save the model as a zip file

    mlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);

## Create a prediction engine to make single predictions

    var predictionEngine = mlContext.Model.CreatePredictionEngine<HeartData, HeartPrediction>(trainedModel);

