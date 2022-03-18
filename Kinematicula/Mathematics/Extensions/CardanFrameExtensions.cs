namespace Kinematicula.Mathematics.Extensions
{
    public static class CardanFrameExtensions
    {
        public static Matrix44D ToMatrix44D(this CardanFrame cardanFrame)
        {
            var alphaRotation = Matrix44D.CreateRotation(new Vector3D(1.0, 0.0, 0.0), cardanFrame.AlphaAngleAxisX);
            var betaRotation = Matrix44D.CreateRotation(new Vector3D(0.0, 1.0, 0.0), cardanFrame.BetaAngleAxisY);
            var gammaRotation = Matrix44D.CreateRotation(new Vector3D(0.0, 0.0, 1.0), cardanFrame.GammaAngleAxisZ);
            var translation = Matrix44D.CreateTranslation(cardanFrame.Translation);

            var matrix = translation * gammaRotation * betaRotation * alphaRotation;

            return matrix;
        }
    }
}
