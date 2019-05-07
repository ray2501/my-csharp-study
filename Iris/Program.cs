using Microsoft.ML;
using Microsoft.ML.Data;
using System;

namespace Iris
{
    class Program
    {
        // STEP 1: Define your data structures
        // IrisData is used to provide training data, and as
        // input for prediction operations
        // - First 4 properties are inputs/features used to predict the label
        // - Label is what you are predicting, and is only set when training
        public class IrisData
        {

            [LoadColumn(0)]
            public float SepalLength;

            [LoadColumn(1)]
            public float SepalWidth;

            [LoadColumn(2)]
            public float PetalLength;

            [LoadColumn(3)]
            public float PetalWidth;

            [LoadColumn(4)]
            public string Label;
        }

        public class ClusterPrediction
        {
            [ColumnName("PredictedLabel")]
            public uint PredictedClusterId;

            [ColumnName("Score")]
            public float[] Distances;
        }

        static void Main(string[] args)
        {
            // STEP 2: Create a ML.NET environment
            var mlContext = new MLContext();

            // If working in Visual Studio, make sure the 'Copy to Output Directory'
            // property of iris-data.txt is set to 'Copy always'
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<IrisData>(path: "iris-data.txt", hasHeader: false, separatorChar: ',');

            // STEP 3: Transform your data and add a learner

            string featuresColumnName = "Features";
            var pipeline = mlContext.Transforms
                .Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 3));

            // STEP 4: Train your model based on the data set
            var model = pipeline.Fit(trainingDataView);

            // STEP 5: Use your model to make a prediction
            // You can change these numbers to test different predictions
            var predictor = mlContext.Model.CreatePredictionEngine<IrisData, ClusterPrediction>(model);
            var prediction = predictor.Predict(
                new IrisData()
                {
                    SepalLength = 3.3f,
                    SepalWidth = 2.6f,
                    PetalLength = 1.2f,
                    PetalWidth = 1.1f,
                });

            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
        }
    }
}
