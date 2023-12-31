﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace MLFoodInteligence_WebApi
{
    public partial class MLModel
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName:@"idcomida",inputColumnName:@"idcomida")      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName:@"idusuario",inputColumnName:@"idusuario"))      
                                    .Append(mlContext.Recommendation().Trainers.MatrixFactorization(new MatrixFactorizationTrainer.Options(){LabelColumnName=@"rating",MatrixColumnIndexColumnName=@"idusuario",MatrixRowIndexColumnName=@"idcomida",ApproximationRank=10,LearningRate=0.063832041196217,NumberOfIterations=305,Quiet=true}));

            return pipeline;
        }
    }
}
