using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraph_3
{
    public static class PlaneRepresentation
    {
        private static float _bettaIsometry = 35.26439f;

        private static float _gammaIsometry = 45f;

        private static float _bettaDimetric = 20.705f;

        private static float _gammaDimetric = 22.208f;



        public static float[,] IsometryProjection(float[,] coordiantes)
        {
            var bettaGammaRotation = BettaGammaRotation(_bettaIsometry, _gammaIsometry);

            var projection = Projection();

            var isometryProjection = MultiplyMatrix(coordiantes, MultiplyMatrix(bettaGammaRotation, projection));

            float[,] result = new float[coordiantes.GetLength(0),2];

            for (int i = 0; i < isometryProjection.GetLength(0); i++)
            {
                result[i,0] = isometryProjection[i,0];
                result[i,1] = isometryProjection[i,1];
            }

            return result;
        }

        public static float[,] DimetricProjection(float[,] coordiantes)
        {
            var bettaGammaRotation = BettaGammaRotation(_bettaDimetric, _gammaDimetric);

            var projection = Projection();

            var dimetricProjection = MultiplyMatrix(coordiantes, MultiplyMatrix(bettaGammaRotation, projection));

            float[,] result = new float[coordiantes.GetLength(0), 2];

            for (int i = 0; i < dimetricProjection.GetLength(0); i++)
            {
                result[i, 0] = dimetricProjection[i, 0];
                result[i, 1] = dimetricProjection[i, 1];
            }

            return result;
        }       

        private static float[,] BettaGammaRotation(float betta, float gamma)
        {
            return new float[,] { { MathF.Cos(gamma), MathF.Sin(betta) * MathF.Cos(gamma), -MathF.Sin(gamma) * MathF.Cos(betta), 0}
                                   , { 0, MathF.Cos(betta), MathF.Sin(betta), 0}
                                   , { MathF.Sin(gamma), -MathF.Sin(betta) * MathF.Cos(gamma), MathF.Cos(betta) * MathF.Cos(gamma), 0}
                                   , { 0.0f, 0.0f, 0.0f, 1.0f} };
        }

        private static float[,] Projection()
        {
            return new float[,] {{ 1f, 0, 0, 0 }
                                ,{ 0f, 1, 0, 0 }
                                ,{ 0f, 0, 0, 0 }
                                ,{ 0f, 0, 0, 1 } };
        }

        private static float[,] MultiplyMatrix(float[,] a, float[,] b)
        {
            float[,] result = new float[a.GetLength(0),b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }
    }
}
