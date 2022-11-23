using Kinematicula.Graphics.Memento;
using Kinematicula.Mathematics;

namespace Kinematicula.Graphics
{
    public class Body : IGraphics, IMementoCreator
    {
        private Matrix44D _frame;
        private Body _parent;
        private List<Body> _children;
        private List<ISensor> _sensors;

        public Body()
        {
            Id = Guid.NewGuid();
            _frame = Matrix44D.Identity;
            Constraints = new List<Constraint>();
            _parent = null;
            _children = new List<Body>();
            Faces = new Face[0];
            Edges = new Edge[0];
            Points = new Point3D[0];
            _sensors = new List<ISensor>();
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public Matrix44D Frame
        {
            get
            {
                return _frame;
            }

            set
            {
                var newFrame = OnFrameChange(_frame, value);
                _frame = newFrame;

                if(Name == "first socket" && Math.Abs(value.Offset.X + 250.0) > 0.01)
                {
                    var a = 0;
                    a = a + 1;
                }

                if (Name == "first shifter" && value.Offset.X < -351)
                {
                    var a = 0;
                    a = a + 1;
                }

                if (Name == "floor" && (Frame.Offset.X != 0.0 || Frame.Offset.Y != 0.0 || Frame.Offset.Z != 0.0))
                {
                    var a = 0;
                    a = a + 1;
                }
            }
        }

        public IEnumerable<ISensor> Sensors => _sensors;

        public void AddSensor(ISensor sensor) => _sensors.Add(sensor);

        public List<Constraint> Constraints { get; }

        public Point3D[] Points { get; set; }
        public Face[] Faces { get; set; }
        public Edge[] Edges { get; set; }

        public Body Parent => _parent;

        public IEnumerable<Body> Children => _children;

        public void AddChild(Body child)
        {
            if (child != null && child != this)
            {
                child._parent = this;
                if (!_children.Contains(child))
                {
                    _children.Add(child);
                }
            }
        }

        public void AddChildren(params Body[] children)
        {
            if (children != null)
            {
                foreach (var child in children)
                {
                    AddChild(child);
                }
            }
        }

        public Color Color { get; set; }

        public virtual IMemento GetMemento()
        {
            return new BodyMemento(this);
        }

        protected virtual Matrix44D OnFrameChange(Matrix44D currentFrame, Matrix44D newFrame) => newFrame;
    }



    public class BodyMemento : IMemento
    {
        private readonly Body _body;
        private readonly Matrix44D _frame;

        public BodyMemento(Body body)
        {
            _body = body;
            _frame = body.Frame;
        }

        public void Restore()
        {
            _body.Frame = _frame;
        }
    }
}