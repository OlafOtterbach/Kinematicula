//using Kinematicula.Kinematics.UniversalSolving.Sequences;
//using Kinematicula.Mathematics;
//using Kinematicula.Mathematics.Extensions;
////using Graphics.Robotics;
//using System;
//using System.Linq;
//using Xunit;

//namespace Kinematicula.Kinematics.UniversalSolving
//{
//    public class InverseSolverHotColdRobotTest
//    {
//        // 25,1, 25,1, 15,1, 15,1, 25,1, 25,1 | Delta 0,100 Error 99,31369235062616
//        // 100.79380583406501
//        [Fact]
//        public void TestA()
//        {
//            // Act
//            var alpha1 = 30.0.DegToRad();
//            var alpha2 = 30.0.DegToRad();
//            var alpha3 = 10.0.DegToRad();
//            var alpha4 = 20.0.DegToRad();
//            var alpha5 = 30.0.DegToRad();
//            var alpha6 = 30.0.DegToRad();
//            var targetPose = GetPose(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

//            var sequencer = new AxisSequencer();
//            var axes = new Axis[]
//            {
//                new Axis(AxisType.RotationAxis, 25.1),
//                new Axis(AxisType.RotationAxis, 25.1),
//                new Axis(AxisType.RotationAxis, 14.9),
//                new Axis(AxisType.RotationAxis, 15.1),
//                new Axis(AxisType.RotationAxis, 25.1),
//                new Axis(AxisType.RotationAxis, 25.1),
//            };
//            double[] errors = new double[6];

//            var pose = GetPose(axes[0].Value.RadToDeg().DegToRad(), axes[1].Value.DegToRad(), axes[2].Value.DegToRad(), axes[3].Value.DegToRad(), axes[4].Value.DegToRad(), axes[5].Value.DegToRad());
//            errors[0] = Math.Abs(pose.AlphaAngleAxisX.RadToDeg() - targetPose.AlphaAngleAxisX.RadToDeg());
//            errors[1] = Math.Abs(pose.BetaAngleAxisY.RadToDeg() - targetPose.BetaAngleAxisY.RadToDeg());
//            errors[2] = Math.Abs(pose.GammaAngleAxisZ.RadToDeg() - targetPose.GammaAngleAxisZ.RadToDeg());
//            errors[3] = Math.Abs(pose.Offset.X - targetPose.Offset.X);
//            errors[4] = Math.Abs(pose.Offset.Y - targetPose.Offset.Y);
//            errors[5] = Math.Abs(pose.Offset.Z - targetPose.Offset.Z);
//            var error = errors.Max();
//        }



//        /*
//         // Six axes robot
//         //
//         //                   n42
//         //                 e7--|==
//         //      n31  n32  /n41
//         //    e4---e5---e6
//         //   / n2
//         //   e3
//         //   | n1
//         // e1,e2
//         */

//        [Fact]
//        public void Test1()
//        {
//            // Act
//            var alpha1 = 30.0.DegToRad();
//            var alpha2 = 30.0.DegToRad();
//            var alpha3 = 10.0.DegToRad();
//            var alpha4 = 20.0.DegToRad();
//            var alpha5 = 30.0.DegToRad();
//            var alpha6 = 30.0.DegToRad();
//            var targetPose = GetPose(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);
//            var targetText = "30, 30, 10, 20, 30, 30";

//            var sequencer = new AxisSequencer();
//            var axes = new Axis[]
//            {
//                new Axis(AxisType.RotationAxis, 25.0),
//                new Axis(AxisType.RotationAxis, 25.0),
//                new Axis(AxisType.RotationAxis, 15.0),
//                new Axis(AxisType.RotationAxis, 15.0),
//                new Axis(AxisType.RotationAxis, 25.0),
//                new Axis(AxisType.RotationAxis, 25.0),
//            };
//            double[] errors = new double[6];

//            var pose = GetPose(axes[0].Value.DegToRad(), axes[1].Value.DegToRad(), axes[2].Value.DegToRad(), axes[3].Value.DegToRad(), axes[4].Value.DegToRad(), axes[5].Value.DegToRad());
//            errors[0] = Math.Abs(pose.AlphaAngleAxisX.RadToDeg() - targetPose.AlphaAngleAxisX.RadToDeg());
//            errors[1] = Math.Abs(pose.BetaAngleAxisY.RadToDeg() - targetPose.BetaAngleAxisY.RadToDeg());
//            errors[2] = Math.Abs(pose.GammaAngleAxisZ.RadToDeg() - targetPose.GammaAngleAxisZ.RadToDeg());
//            errors[3] = Math.Abs(pose.Offset.X - targetPose.Offset.X);
//            errors[4] = Math.Abs(pose.Offset.Y - targetPose.Offset.Y);
//            errors[5] = Math.Abs(pose.Offset.Z - targetPose.Offset.Z);
//            var error = errors.Sum();

//            var deltaAngle = 0.1;
//            Axis[] bestSeq = axes;
//            double bestError = error;

//            Axis[] globalSeq = axes;
//            double globalError = error;

//            var text = "";
//            for (var i = 0; i < 100; i++)
//            {
//                var sequences = sequencer.GetSequences(axes, deltaAngle, 0.0);
//                foreach (var seq in sequences)
//                {
//                    pose = GetPose(seq[0].Value.DegToRad(), seq[1].Value.DegToRad(), seq[2].Value.DegToRad(), seq[3].Value.DegToRad(), seq[4].Value.DegToRad(), seq[5].Value.DegToRad());
//                    errors[0] = Math.Abs(pose.AlphaAngleAxisX.RadToDeg() - targetPose.AlphaAngleAxisX.RadToDeg());
//                    errors[1] = Math.Abs(pose.BetaAngleAxisY.RadToDeg() - targetPose.BetaAngleAxisY.RadToDeg());
//                    errors[2] = Math.Abs(pose.GammaAngleAxisZ.RadToDeg() - targetPose.GammaAngleAxisZ.RadToDeg());
//                    errors[3] = Math.Abs(pose.Offset.X - targetPose.Offset.X);
//                    errors[4] = Math.Abs(pose.Offset.Y - targetPose.Offset.Y);
//                    errors[5] = Math.Abs(pose.Offset.Z - targetPose.Offset.Z);
//                    error = errors.Sum();
//                    if (error < bestError)
//                    {
//                        bestError = error;
//                        bestSeq = seq;
//                    }
//                }
//                text += targetText + "\n";
//                text += $"{bestSeq[0].Value}, {bestSeq[1].Value}, {bestSeq[2].Value}, {bestSeq[3].Value}, {bestSeq[4].Value}, {bestSeq[5].Value} | Delta {deltaAngle:F3} Error {bestError}\n";
//                text += targetPose.ToString();
//                text += "\n";

//                if (bestError < globalError)
//                {
//                    globalError = bestError;
//                    globalSeq = bestSeq;
//                }
//                else
//                {
//                    deltaAngle /= 2.0;
//                }
//                axes = bestSeq;
//            }
//        }

//        [Fact]
//        public void Test3()
//        {
//            var targetText = "30.00, 30.00, 10.00, 20.00, 30.00, 30.00";
//            var targetPose = GetPoseDegree(30, 30, 10, 20, 30, 30);
//            var startPose = GetDeltaPoseDegree(targetPose, 5, 5, 5, 5, 5, 5);
//            var startError = GetError(startPose, targetPose);
//            var alpha1 = 30.0.DegToRad();
//            var alpha2 = 30.0.DegToRad();
//            var alpha3 = 10.0.DegToRad();
//            var alpha4 = 20.0.DegToRad();
//            var alpha5 = 30.0.DegToRad();
//            var alpha6 = 30.0.DegToRad();
//            bool isOk = RobotService.GetAxesForTransformation(startPose.ToMatrix44D(), ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);
//            alpha1 = alpha1.RadToDeg();
//            alpha2 = alpha2.RadToDeg();
//            alpha3 = alpha3.RadToDeg();
//            alpha4 = alpha4.RadToDeg();
//            alpha5 = alpha5.RadToDeg();
//            alpha6 = alpha6.RadToDeg();
//            var sequencer = new AxisSequencer();
//            var axes = new Axis[]
//            {
//                new Axis(AxisType.RotationAxis, alpha1),
//                new Axis(AxisType.RotationAxis, alpha2),
//                new Axis(AxisType.RotationAxis, alpha3),
//                new Axis(AxisType.RotationAxis, alpha4),
//                new Axis(AxisType.RotationAxis, alpha5),
//                new Axis(AxisType.RotationAxis, alpha6),
//            };

//            var deltaAngle = 0.1;
//            Axis[] bestSeq = axes;
//            double bestError = startError;

//            Axis[] globalSeq = axes;
//            double globalError = startError;

//            var text = "";
//            for (var i = 0; i < 100; i++)
//            {
//                var sequences = sequencer.GetSequences(axes, deltaAngle, 0.0);
//                foreach (var seq in sequences)
//                {
//                    var pose = GetPose(seq[0].Value.DegToRad(), seq[1].Value.DegToRad(), seq[2].Value.DegToRad(), seq[3].Value.DegToRad(), seq[4].Value.DegToRad(), seq[5].Value.DegToRad());
//                    var error = GetError(pose, targetPose);
//                    if (error < bestError)
//                    {
//                        bestError = error;
//                        bestSeq = seq;
//                    }
//                }
//                text += targetText + "\n";
//                text += $"{bestSeq[0].Value:F2}, {bestSeq[1].Value:F2}, {bestSeq[2].Value:F2}, {bestSeq[3].Value:F2}, {bestSeq[4].Value:F2}, {bestSeq[5].Value:F2} | Delta {deltaAngle:F3} Error {bestError}\n";
//                text += "\n";

//                if (bestError < globalError)
//                {
//                    globalError = bestError;
//                    globalSeq = bestSeq;
//                }
//                else
//                {
//                    deltaAngle /= 2.0;
//                }
//                axes = bestSeq;
//            }
//        }


//        private double GetError(CardanFrame pose, CardanFrame targetPose)
//        {
//            var errors = new double[6];
//            errors[0] = Math.Abs(pose.AlphaAngleAxisX.RadToDeg() - targetPose.AlphaAngleAxisX.RadToDeg());
//            errors[1] = Math.Abs(pose.BetaAngleAxisY.RadToDeg() - targetPose.BetaAngleAxisY.RadToDeg());
//            errors[2] = Math.Abs(pose.GammaAngleAxisZ.RadToDeg() - targetPose.GammaAngleAxisZ.RadToDeg());
//            errors[3] = Math.Abs(pose.Offset.X - targetPose.Offset.X);
//            errors[4] = Math.Abs(pose.Offset.Y - targetPose.Offset.Y);
//            errors[5] = Math.Abs(pose.Offset.Z - targetPose.Offset.Z);
//            var error = errors.Sum();
//            return error;
//        }

//        private CardanFrame GetPoseDegree(double alpha1, double alpha2, double alpha3, double alpha4, double alpha5, double alpha6)
//        {
//            return GetPose(alpha1.DegToRad(), alpha2.DegToRad(), alpha3.DegToRad(), alpha4.DegToRad(), alpha5.DegToRad(), alpha6.DegToRad());
//        }

//        private CardanFrame GetPose(double alpha1, double alpha2, double alpha3, double alpha4, double alpha5, double alpha6)
//        {
//            var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);
//            var pose = mat.ToCardanFrame();
//            return pose;
//        }

//        private CardanFrame GetDeltaPoseDegree(CardanFrame pose, double xDelta, double yDelta, double zDelta, double alphaDelta, double betaDelta, double gammaDelta)
//        {
//            alphaDelta = alphaDelta.DegToRad();
//            betaDelta = betaDelta.DegToRad();
//            gammaDelta = gammaDelta.DegToRad();
//            return new CardanFrame(new Position3D(pose.Offset.X + xDelta, pose.Offset.Y + yDelta, pose.Offset.Z + zDelta), pose.AlphaAngleAxisX + alphaDelta, pose.BetaAngleAxisY + betaDelta, pose.GammaAngleAxisZ + gammaDelta);
//        }





//        [Fact]
//        public void Test4()
//        {
//            var sourceAlpha1 = 00.0.DegToRad();
//            var sourceAlpha2 = 30.0.DegToRad();
//            var sourceAlpha3 = 30.0.DegToRad();
//            var sourceAlpha4 = 00.0.DegToRad();
//            var sourceAlpha5 = 30.0.DegToRad();
//            var sourceAlpha6 = 00.0.DegToRad();
//            var sourceFrame = RobotService.GetTransformation(sourceAlpha1, sourceAlpha2, sourceAlpha3, sourceAlpha4, sourceAlpha5, sourceAlpha6);
//            var targetFrame = sourceFrame * Matrix44D.CreateTranslation(new Vector3D(1, 0, 0));
//            {
//                var targetAlpha1 = 00.0.DegToRad();
//                var targetAlpha2 = 30.0.DegToRad();
//                var targetAlpha3 = 30.0.DegToRad();
//                var targetAlpha4 = 00.0.DegToRad();
//                var targetAlpha5 = 30.0.DegToRad();
//                var targetAlpha6 = 00.0.DegToRad();
//                var res = RobotService.GetAxesForTransformation(targetFrame, ref targetAlpha1, ref targetAlpha2, ref targetAlpha3, ref targetAlpha4, ref targetAlpha5, ref targetAlpha6);
//                targetAlpha1 = targetAlpha1.RadToDeg();
//                targetAlpha2 = targetAlpha2.RadToDeg();
//                targetAlpha3 = targetAlpha3.RadToDeg();
//                targetAlpha4 = targetAlpha4.RadToDeg();
//                targetAlpha5 = targetAlpha5.RadToDeg();
//                targetAlpha6 = targetAlpha6.RadToDeg();
//            }

//            var sequencer = new AxisSequencer();
//            var axes = new Axis[]
//            {
//                new Axis(AxisType.RotationAxis, sourceAlpha1),
//                new Axis(AxisType.RotationAxis, sourceAlpha2),
//                new Axis(AxisType.RotationAxis, sourceAlpha3),
//                new Axis(AxisType.RotationAxis, sourceAlpha4),
//                new Axis(AxisType.RotationAxis, sourceAlpha5),
//                new Axis(AxisType.RotationAxis, sourceAlpha6),
//            };

//            var deltaAngle = 1.0.DegToRad();
//            Axis[] bestSeq = axes;
//            Matrix44D bestFrame = sourceFrame;
//            var error = GetError(bestFrame, targetFrame);

//            Axis[] globalSeq = axes;
//            Matrix44D globalFrame = bestFrame;

//            var targetText = "0.00, 30.00, 30.00, 00.00, 30.00, 00.00";

//            var text = "";
//            while (!error.EqualsTo(0.0, 0.001))
//            {
//                var sequences = sequencer.GetSequences(axes, deltaAngle, 0.0).ToList(); ;
//                foreach (var seq in sequences)
//                {
//                    var alpha1 = seq[0].Value;
//                    var alpha2 = seq[1].Value;
//                    var alpha3 = seq[2].Value;
//                    var alpha4 = seq[3].Value;
//                    var alpha5 = seq[4].Value;
//                    var alpha6 = seq[5].Value;
//                    var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);
//                    alpha1 = alpha1.RadToDeg();
//                    alpha2 = alpha2.RadToDeg();
//                    alpha3 = alpha3.RadToDeg();
//                    alpha4 = alpha4.RadToDeg();
//                    alpha5 = alpha5.RadToDeg();
//                    alpha6 = alpha6.RadToDeg();
//                    if (IsBetter(targetFrame, bestFrame, mat))
//                    {
//                        bestFrame = mat;
//                        bestSeq = seq;
//                        error = GetError(targetFrame, bestFrame);

//                        text += targetText + "\n";
//                        text += $"{bestSeq[0].Value.RadToDeg():F4}, {bestSeq[1].Value.RadToDeg():F4}, {bestSeq[2].Value.RadToDeg():F4}, {bestSeq[3].Value.RadToDeg():F4}, {bestSeq[4].Value.RadToDeg():F4}, {bestSeq[5].Value.RadToDeg():F4} | Delta {deltaAngle.RadToDeg():F8} Error {error}\n";
//                        text += "\n";
//                    }
//                }

//                if (IsBetter(targetFrame, globalFrame, bestFrame))
//                {
//                    globalFrame = bestFrame;
//                    globalSeq = bestSeq;
//                    // deltaAngle /= 2.0;
//                }
//                else
//                {
//                    deltaAngle /= 2.0;
//                    var delta = deltaAngle.RadToDeg();
//                    text += "reduce\n";
//                }
//                axes = bestSeq;
//            }
//        }

//        private bool IsBetter(Matrix44D startFrame, Matrix44D currentFrame, Matrix44D newFrame)
//        {
//            return IsBetter(startFrame.ToCardanFrame(), currentFrame.ToCardanFrame(), newFrame.ToCardanFrame());
//        }

//        private bool IsBetter(CardanFrame targetFrame, CardanFrame currentFrame, CardanFrame newFrame)
//        {
//            var newDistantOffsetX = targetFrame.Offset.X.DistantLengthTo(newFrame.Offset.X);
//            var newDistantOffsetY = targetFrame.Offset.Y.DistantLengthTo(newFrame.Offset.Y);
//            var newDistantOffsetZ = targetFrame.Offset.Z.DistantLengthTo(newFrame.Offset.Z);
//            var newDistantAlpha = targetFrame.AlphaAngleAxisX.DistantAngleTo(newFrame.AlphaAngleAxisX);
//            var newDistantBeta = targetFrame.BetaAngleAxisY.DistantAngleTo(newFrame.BetaAngleAxisY);
//            var newDistantGamma = targetFrame.GammaAngleAxisZ.DistantAngleTo(newFrame.AlphaAngleAxisX);

//            var currentDistantOffsetX = targetFrame.Offset.X.DistantLengthTo(currentFrame.Offset.X);
//            var currentDistantOffsetY = targetFrame.Offset.Y.DistantLengthTo(currentFrame.Offset.Y);
//            var currentDistantOffsetZ = targetFrame.Offset.Z.DistantLengthTo(currentFrame.Offset.Z);
//            var currentDistantAlpha = targetFrame.AlphaAngleAxisX.DistantAngleTo(currentFrame.AlphaAngleAxisX);
//            var currentDistantBeta = targetFrame.BetaAngleAxisY.DistantAngleTo(currentFrame.BetaAngleAxisY);
//            var currentDistantGamma = targetFrame.GammaAngleAxisZ.DistantAngleTo(currentFrame.AlphaAngleAxisX);

//            var isLessOrEqualOffsetX = newDistantOffsetX <= currentDistantOffsetX;
//            var isLessOrEqualOffsetY = newDistantOffsetY <= currentDistantOffsetY;
//            var isLessOrEqualOffsetZ = newDistantOffsetZ <= currentDistantOffsetZ;
//            var isLessOrEqualAlpha = newDistantAlpha <= currentDistantAlpha;
//            var isLessOrEqualBeta = newDistantBeta <= currentDistantBeta;
//            var isLessOrEqualGamma = newDistantGamma <= currentDistantGamma;

//            var isLessOffsetX = newDistantOffsetX < currentDistantOffsetX;
//            var isLessOffsetY = newDistantOffsetY < currentDistantOffsetY;
//            var isLessOffsetZ = newDistantOffsetZ < currentDistantOffsetZ;
//            var isLessAlpha = newDistantAlpha < currentDistantAlpha;
//            var isLessBeta = newDistantBeta < currentDistantBeta;
//            var isLessGamma = newDistantGamma < currentDistantGamma;


//            var result = isLessOrEqualOffsetX &&
//                         isLessOrEqualOffsetY &&
//                         isLessOrEqualOffsetZ &&
//                         isLessOrEqualAlpha &&
//                         isLessOrEqualBeta &&
//                         isLessOrEqualGamma &&
//                         (
//                             isLessOffsetX ||
//                             isLessOffsetY ||
//                             isLessOffsetZ ||
//                             isLessAlpha ||
//                             isLessBeta ||
//                             isLessGamma);

//            return result;
//        }

//        private double GetError(Matrix44D current, Matrix44D target)
//        {
//            var result =
//                Math.Max(target.A11.DistantLengthTo(current.A11),
//                Math.Max(target.A12.DistantLengthTo(current.A12),
//                Math.Max(target.A13.DistantLengthTo(current.A13),
//                Math.Max(target.A14.DistantLengthTo(current.A14),
//                Math.Max(target.A21.DistantLengthTo(current.A21),
//                Math.Max(target.A22.DistantLengthTo(current.A22),
//                Math.Max(target.A23.DistantLengthTo(current.A23),
//                Math.Max(target.A24.DistantLengthTo(current.A24),
//                Math.Max(target.A31.DistantLengthTo(current.A31),
//                Math.Max(target.A32.DistantLengthTo(current.A32),
//                Math.Max(target.A33.DistantLengthTo(current.A33),
//                Math.Max(target.A34.DistantLengthTo(current.A34),
//                Math.Max(target.A41.DistantLengthTo(current.A41),
//                Math.Max(target.A42.DistantLengthTo(current.A42),
//                Math.Max(target.A43.DistantLengthTo(current.A43),
//                         target.A44.DistantLengthTo(current.A44))))))))))))))));

//            return result;
//        }
//    }
//}