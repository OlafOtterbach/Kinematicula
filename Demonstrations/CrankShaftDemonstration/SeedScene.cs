using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Graphics.Extensions;
using CrankShaftDemonstration.PistonMotor;

namespace CylinderDemonstration
{
    public static class SeedScene
    {
        public static Scene CreateAndPopulateScene()
        {
            var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver());

            // floor
            var floor = Floor.Create(10, 100);
            floor.Name = "Floor";
            scene.AddBody(floor);
            var fixedToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));


            var motor = MotorCreator.Create();
            scene.AddBody(motor);
            var fixedToFloorConstraint = new FixedConstraint(new Anchor(floor, Matrix44D.Identity), new Anchor(motor, Matrix44D.Identity));


            var camera = new Camera()
            {
                Name = "CameraOne",
                NearPlane = 1.0,
                Target = new Position3D(),
            };
            camera.SetCamera(10.0, 10.0, 1500.0);
            camera.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 200)) * camera.Frame;
            scene.AddBody(camera);

            scene.InitScene();
            return scene;
        }
    }
}