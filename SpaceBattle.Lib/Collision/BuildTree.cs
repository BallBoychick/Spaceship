using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Models;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;



// namespace MLNetDemo
// {
//     class Program
//     {
//         //Don't forget to build in X64 mode (ML.Net requiers it)

//         static void Main(string[] args)
//         {
//             //Create the category list with the mapping of the Id and the Name of the Category
//             var categoryList = new List<Category>
//             {
//                 new Category{Id=1, Name="sdw"},
//                 new Category{Id=2, Name="rent"},
//                 new Category{Id=3, Name="cigs"},
//                 new Category{Id=4, Name="resto"}
//             };

//             //2.54 is not a known value but the model should predict the "sdw" category
//             //because the model will be trained with 1.7, 2.2 and 3.0 a values for "sdw" category
//             var valueToPredict = 2.54f;

//             var predictedCategoryId = PredictCategoryFromCost(valueToPredict);
//             //Find the category by its Id
//             var predictedCategory = categoryList.FirstOrDefault(x => x.Id == predictedCategoryId);

//             Console.WriteLine($"\r\nThe cost of {valueToPredict}$ gives a Category prediction of {predictedCategory.Name}.");
//             Console.WriteLine("\r\nPress any key...");
//             Console.ReadKey();
//         }

//         private static float PredictCategoryFromCost(float costToPredict)
//         {
//             var predictedCategoryId = 0;

//             var model = CreateModel();
//             TestModel(model);

//             //Save the model on disk to reuse later
//             model.WriteAsync("model.zip");

//             //Here is how to load this model later instead re-training it every time:
//             //var loadedModel = PredictionModel.ReadAsync<ExpenseData, CategoryPrediction>("model.zip").Result;

//             var expenseToPredict = new ExpenseData { Cost = costToPredict };
//             var prediction = model.Predict(expenseToPredict);

//             predictedCategoryId = (int)Math.Round(prediction.Category);

//             return predictedCategoryId;
//         }

//         private static PredictionModel<ExpenseData, CategoryPrediction> CreateModel()
//         {
//             var dataFileName = "expenses.csv";

//             var pipeline = new LearningPipeline
//             {
//                 new TextLoader(dataFileName).CreateFrom<ExpenseData>(useHeader: true, separator: ','),
//                 new ColumnConcatenator("Features", "Cost"),
//                 new GeneralizedAdditiveModelRegressor()
//             };

//             var model = pipeline.Train<ExpenseData, CategoryPrediction>();
//             return model;
//         }

//         private static void TestModel(PredictionModel model)
//         {
//             var testData = new TextLoader("expenses_tests.csv").CreateFrom<ExpenseData>(useHeader: true, separator: ',');

//             var evaluator = new RegressionEvaluator();
//             var metrics = evaluator.Evaluate(model, testData);

//             //Let's check the precision of our model
//             Console.WriteLine($"RMS: {metrics.Rms}");
//             Console.WriteLine($"R^2: {metrics.RSquared}"); //The closer to 1, the best the model has been trained
//         }
//     }
// }