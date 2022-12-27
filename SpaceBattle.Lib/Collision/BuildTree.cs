// using System.IO;
// using System.Collections.Generic;
// using Hwdtech;
// namespace SpaceBattle.Lib;


// public class CSVReader: ICommand
// {
//     private string spliter;
//     private string fileway;
//     public CSVReader(string fileway, string spliter="; ")
//     {
//         this.fileway = fileway;
//         this.spliter = spliter;
//     }

//     public void execute()
//     {
//         using (var reader = new StreamReader(fileway))
//         {
//             IList<string> listHEADERS = reader.ReadLine().Split("; ");
//             List<Dictionary<string, double>> table = new List<Dictionary<string, double>>();
            
//             while (!reader.EndOfStream)
//             {
//                 var values = reader.ReadLine().Split("; ");
//                 table.Add(new Dictionary<string, double>(listHEADERS.Zip(values, (k, v) => new KeyValuePair<string, double>(k, Convert.ToDouble(v)))));
//             }
//         }
//     }
// }

// using System;
// using Microsoft.ML;
// using Microsoft.ML.Data;
// using System.IO;
// using System.Collections.Generic;
// namespace SpaceBattle.Lib.Classes;


// public class CSVReader: ICommand
// {
//     private string spliter;
//     private string fileway;
//     public CSVReader(string fileway, string spliter="; ")
//     {
//         this.fileway = fileway;
//         this.spliter = spliter;
//     }

//     public void execute()
//     {
//         using (var reader = new StreamReader(fileway))
//         {
//             IList<string> listHEADERS = reader.ReadLine().Split("; ");
//             List<Dictionary<string, double>> table = new List<Dictionary<string, double>>();
            
//             while (!reader.EndOfStream)
//             {
//                 var values = reader.ReadLine().Split("; ");
//                 table.Add(new Dictionary<string, double>(listHEADERS.Zip(values, (k, v) => new KeyValuePair<string, double>(k, Convert.ToDouble(v)))));
//             }
//         }
//     }
// }

// using System;
// using Microsoft.ML;
// using Microsoft.ML.Data;

//    class Program
//    {
//        public class HouseData
//        {
//            public float Size { get; set; }
//            public float Price { get; set; }
//        }

//        public class Prediction
//        {
//            [ColumnName("Score")]
//            public float Price { get; set; }
//        }

//        static void Main(string[] args)
//        {
//            MLContext mlContext = new MLContext();

//            // 1. Import or create training data
//            HouseData[] houseData = {
//                new HouseData() { Size = 1.1F, Price = 1.2F },
//                new HouseData() { Size = 1.9F, Price = 2.3F },
//                new HouseData() { Size = 2.8F, Price = 3.0F },
//                new HouseData() { Size = 3.4F, Price = 3.7F } };
//            IDataView trainingData = mlContext.Data.LoadFromEnumerable(houseData);

//            // 2. Specify data preparation and model training pipeline
//            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
//                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

//            // 3. Train model
//            var model = pipeline.Fit(trainingData);

//            // 4. Make a prediction
//            var size = new HouseData() { Size = 2.5F };
//            var price = mlContext.Model.CreatePredictionEngine<HouseData, Prediction>(model).Predict(size);

//            Console.WriteLine($"Predicted price for size: {size.Size*1000} sq ft= {price.Price*100:C}k");

//            // Predicted price for size: 2500 sq ft= $261.98k
//        }
//    }




//Step 1. Create an ML Context
// var ctx = new MLContext();

// //Step 2. Read in the input data from a text file for model training
// IDataView trainingData = ctx.Data
//     .LoadFromTextFile<ModelInput>(dataPath, hasHeader: true);

// //Step 3. Build your data processing and training pipeline
// var pipeline = ctx.Transforms.Text
//     .FeaturizeText("Features", nameof(SentimentIssue.Text))
//     .Append(ctx.BinaryClassification.Trainers
//         .LbfgsLogisticRegression("Label", "Features"));

// //Step 4. Train your model
// ITransformer trainedModel = pipeline.Fit(trainingData);

// //Step 5. Make predictions using your trained model
// var predictionEngine = ctx.Model
//     .CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);

// var sampleStatement = new ModelInput() { Text = "This is a horrible movie" };

// var prediction = predictionEngine.Predict(sampleStatement);

// using MyMLApp;
// // Add input data
// var sampleData = new SentimentModel.ModelInput()
// {
//     Col0 = "This restaurant was wonderful."
// };

// // Load model and predict output of sample data
// var result = SentimentModel.Predict(sampleData);

// // If Prediction is 1, sentiment is "Positive"; otherwise, sentiment is "Negative"
// var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";
// Console.WriteLine($"Text: {sampleData.Col0}\nSentiment: {sentiment}");