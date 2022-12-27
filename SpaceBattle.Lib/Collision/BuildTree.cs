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
//    using Microsoft.ML;
//    using Microsoft.ML.Data;

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
//    }
// using System;
// using System.Collections.Generic;
// namespace DecisionTree
// {
//   class DecisionTreeProgram
//   {
//     static void Main(string[] args)
//     {
//       Console.WriteLine("Decision tree demo ");

//       double[][] dataX = new double[30][];
//       dataX[0] = new double[] { 5.1, 3.5, 1.4, 0.2 };
//       dataX[1] = new double[] { 4.9, 3.0, 1.4, 0.2 };
//       dataX[2] = new double[] { 4.7, 3.2, 1.3, 0.2 };
//       dataX[3] = new double[] { 4.6, 3.1, 1.5, 0.2 };
//       dataX[4] = new double[] { 5.0, 3.6, 1.4, 0.2 };
//       dataX[5] = new double[] { 5.4, 3.9, 1.7, 0.4 };
//       dataX[6] = new double[] { 4.6, 3.4, 1.4, 0.3 };
//       dataX[7] = new double[] { 5.0, 3.4, 1.5, 0.2 };
//       dataX[8] = new double[] { 4.4, 2.9, 1.4, 0.2 };
//       dataX[9] = new double[] { 4.9, 3.1, 1.5, 0.1 };

//       dataX[10] = new double[] { 7.0, 3.2, 4.7, 1.4 };
//       dataX[11] = new double[] { 6.4, 3.2, 4.5, 1.5 };
//       dataX[12] = new double[] { 6.9, 3.1, 4.9, 1.5 };
//       dataX[13] = new double[] { 5.5, 2.3, 4.0, 1.3 };
//       dataX[14] = new double[] { 6.5, 2.8, 4.6, 1.5 };
//       dataX[15] = new double[] { 5.7, 2.8, 4.5, 1.3 };
//       dataX[16] = new double[] { 6.3, 3.3, 4.7, 1.6 };
//       dataX[17] = new double[] { 4.9, 2.4, 3.3, 1.0 };
//       dataX[18] = new double[] { 6.6, 2.9, 4.6, 1.3 };
//       dataX[19] = new double[] { 5.2, 2.7, 3.9, 1.4 };

//       dataX[20] = new double[] { 6.3, 3.3, 6.0, 2.5 };
//       dataX[21] = new double[] { 5.8, 2.7, 5.1, 1.9 };  
//       dataX[22] = new double[] { 7.1, 3.0, 5.9, 2.1 };
//       dataX[23] = new double[] { 6.3, 2.9, 5.6, 1.8 };
//       dataX[24] = new double[] { 6.5, 3.0, 5.8, 2.2 };
//       dataX[25] = new double[] { 7.6, 3.0, 6.6, 2.1 };
//       dataX[26] = new double[] { 4.9, 2.5, 4.5, 1.7 };
//       dataX[27] = new double[] { 7.3, 2.9, 6.3, 1.8 };
//       dataX[28] = new double[] { 6.7, 2.5, 5.8, 1.8 };
//       dataX[29] = new double[] { 7.2, 3.6, 6.1, 2.5 };

//       int[] dataY = 
//         new int[30] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
//                       1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
//                       2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

//       Console.WriteLine("Iris 30-item subset: ");
//       Console.WriteLine("5.1, 3.5, 1.4, 0.2 -> 0");
//       Console.WriteLine("7.0, 3.2, 4.7, 1.4 -> 1");
//       Console.WriteLine("6.3, 3.3, 6.0, 2.5 -> 2");
//       Console.WriteLine(" . . . ");

//       Console.WriteLine("Build 7-node 3-class tree");
//       DecisionTree dt = new DecisionTree(7, 3);
//       dt.BuildTree(dataX, dataY);

//       // Console.WriteLine("Tree is: ");
//       // dt.Show();  // show all nodes in tree

//       Console.WriteLine("Done. Nodes 0 and 4 are:");
//       dt.ShowNode(0);
//       dt.ShowNode(4);

//       Console.WriteLine("Computing accuracy:");
//       double acc = dt.Accuracy(dataX, dataY);
//       Console.WriteLine("Classification accuracy = " +
//         acc.ToString("F4"));

//       double[] x = new double[] {6.0, 2.0, 3.0, 4.0};
//       Console.Write("Predicting class for");
//       Console.WriteLine(" (6.0, 2.0, 3.0, 4.0)");
//       int predClass = dt.Predict(x, verbose: true);

//       Console.WriteLine("End demo ");
//       Console.ReadLine();
//     } // Main
//   } // Program class

//   class DecisionTree
//   {
//     public int numNodes;
//     public int numClasses;
//     public List<Node> tree;

//     public DecisionTree(int numNodes,
//       int numClasses) { . . }
    
//     public void BuildTree(double[][] dataX,
//       int[] dataY) { . . }
    
//     public void Show() { . . }
    
//     public void ShowNode(int nodeID) { . . }
    
//     public int Predict(double[] x,
//       bool verbose) { . . }
    
//     public double Accuracy(double[][] dataX,
//       int[] dataY) { . . }
    
//     private static SplitInfo GetSplitInfo(
//       double[][] dataX, int[] dataY, List<int> rows,
//       int numClasses) { . . }
    
//     private static double Impurity(int[] dataY,
//       List<int> rows, int numClasses) { . . }
    
//     private static double MeanImpurity(int[] dataY,
//       List<int> rows1, List<int> rows2,
//       int numClasses) { . . }
    
//     private static int[] ComputeClassCts(int[] dataY,
//       List<int> rows, int numClasses) { . . }
    
//     private static int ArgMax(int[] classCts) { . . }
    
//     // ----------

//     public class Node
//     {
//       public int nodeID;
//       public List<int> rows;
//       public int splitCol;
//       public double splitVal;
//       public int[] classCounts;
//       public int predictedClass;
//     }

//     public class SplitInfo  // helper
//     {
//       public int splitCol;
//       public double splitVal;
//       public List<int> lessRows;
//       public List<int> greaterRows;
//     }

//     // ----------

//   } // DecisionTree class
// } // ns

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