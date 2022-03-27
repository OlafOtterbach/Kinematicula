using Kinematicula.LogicViewing;
using Kinematicula.Mathematics;

namespace Kinematicula.Graphics
{
    public class CameraBody : Body
    {
        private Camera _camera;

        public CameraBody()
        {
            _camera = new Camera();
        }

        public Camera Camera
        { 
            get; set; }

        protected override Matrix44D OnFrameChange(Matrix44D currentFrame, Matrix44D newFrame)
        {


            return newFrame;
        }
    }
}
