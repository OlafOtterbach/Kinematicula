# Kinematicula

Simple handling of simple very graphics and kinematics .NET Projects.

## Graphics


#### Body
The body is a container for the graphics of the body.
It is a volume body

```c#
Guid Id { get; }
```
The unique id of the body.
```c#
string Name { get; set; }
```
The optional name of the body.

```c#
Body Parent { get; }
```
The parent of the body. It is not a relative positioning relation ship.
It is only for structure of a body hierarchy.

```c#
IEnumerable<Body> Children
```
The children of the body. It is not a relative positioning relation ship.
It is only for structure of a body hierarchy.

```c#
void AddChild(Body child)
```
Adds a child body to a body.

```c#
void AddChildren(params Body[] children)
```
Adds children bodies to a body.

```c#
Matrix44D Frame { get; set; }
```
The position and orientation of the body.
The graphics is positioned relative to this frame.
The frame itself is always absolute positioned to the world origin (0,0,0).

```c#
        public Point3D[] Points { get; set; }
        public Face[] Faces { get; set; }
        public Edge[] Edges { get; set; }
```

The points are all edge points of the graphics. The are used fpr triangles and edges.
The faces are the surfaces that form a volume body.
The edges represent a wire frame for contours.

```c#
Color Color { get; set; }
```
The color of the body. it is the color of all faces and edges without a color.

```c#
IEnumerable<ISensor> Sensors { get; }
```
The list of sensors for touching the body.

```c#
void AddSensor(ISensor sensor)
```
Adding a sensor for touching the body.

```c#
List<Constraint> Constraints { get; }
```
List of kinematic constraints to connect two bodies by a axis or a fixed connection or othe connections.


####Face
A face is a set of coherent triangles to form a surface.
```c#
Triangle[] Triangles { get; set; }
```
Triangles that are forming the face.

```c#
Color Color { get; set; }
```
The color of the face. it is the color of all triangles and edges without a color.

```c#
bool HasBorder { get; set; }
```
Sets a border around the face.

```c#
bool HasFacets { get; set; }
```
All coherent triangles in one plane get a border.


####Triangle
A triangle is defined by three vertices and coedges that connecting the vertices.

```c#
Face ParentFace { get; set; }
```
The parent face the triangle is owned by.

```c#
Vertex P1 { get; set; }
```
The first edge point of the triangle.

```c#
Vertex P2 { get; set; }
```
The second edge point of the triangle.

```c#
Vertex P3 { get; set; }
```
The third edge point of the triangle.

```c#
Vector3D Normal { get; set; }
```
The normal of the triangle calculated in counter clock wise direction through the edge points.

```c#
CoEdge[] CoEdges { get; set; }
```
The coedges connecting the points. 
Neighbour triangles share the same edge of their coedges.

#### Vertex
A vertex is a combination of a position point and a normal.
The normal is used for color gradient between faces.

```c#
Point3D Point { get; set; }
```
Position point of vertex.

```c#
Vector3D Normal { get; set; }
```
Normal of the vertex.

#### Point3D
A point is a position in space and ca shared by mor than one vertex.
```c#
Position3D Position { get; set; }
```
Position of the point in space.

#### Coedge
Coedge is a edge between of a triangle that has a neighbour coedge of a othe triangle.
They share the same points of different vertices of the triangles.

```c#
Triangle ParentTriangle {get; set;}
```
Triangle the coedge is owned by.

```c#
 Point3D Start { get; set; }
```
Start of the coedge.

```c#
Point3D End { get; set; }
```
End of the coedge.

#### Edge
```c#
Body Parent { get; set; }
```
The parent body of the edge.
```c#
Point3D Start { get; }
```
```c#
Point3D End { get; }
```
```c#
CoEdge First { get; set; }
```
First coedge of the first neigbour triangle of the edge between two triangles.
```c#
CoEdge Second { get; set; }
```
Second coedge of the second neigbour triangle of the edge between two triangles. If there is only one triangle it is a outside edge with only one coedge.
```c#
Color Color { get; set; }
```
Color of the edge.

#### World
Body with a frame that is allways the identity.

#### Scene
The scene is managing the scenery of bodies.
All bodies of the scene are holded.
```c#
List<Body> Bodies { get; }
```
List of bodies of the scene.

```c#
void AddBody(Body body)
```
Adding body all children bodies of the body to the scene.

```c#
World World { get;}
```
If the word is part of the scene then it gets the world.



## Creators
For creating primitive bodies static creators can be used.

```c#
Body Cube.Create(double size)
```
Creates a cube with the given size.

```c#
Body Cubeoid.Create(double width, double height, double depth)
```
Creates a cuboid with given width, height and depth.

```c#
Body Cylinder.Create(int segments, double radius, double height)
```
Creates a cylinder body with given height and radius. The segments are
the count of edges of the polygon floorplan for the cylinder.

```c#
Body Sphere.Create(int circleSegments, double radius)
```
Creates a sphere body with given radius and count of segments
of the polygon body. More segments results in a more smooth sphere.

```c#
Body Floor.Create(int count, double size)
```
Creates a square floor body like a chess play ground of count fields and the size of every field.

```c#
Body Oval.Create(
            int segments1,
            int segments2,
            double radius1,
            double radius2,
            bool hasBorder1,
            bool hasBorder2,
            bool facetted1,
            bool facetted2,
            double length,
            double depth,
            Matrix44D originFrame)
```
Creates a oval body. 
segments are the count of edges for the floorplan of the two half circles of the oval. The radius1 and radius2 for the two half circles
The parametes hasBorder sets if there is a edge between the linear face and the half circle face.
The facetted parameters sets if there ar edges on the radius faces.
The length is the distance between the center points of the half circles.
And the depth is the distance between bottom side and upper side of the oval.

```c#
Body RotationBody.Create(
    int circleSegments,
    double[][] shapeSections,
    bool[] borderFlags,
    bool[] facetsFlags)
```
Creates a rotation body of a 2D shape rotated around the Z axis.
The circle segmentss describe the count of edges of the rotation circle.
The shape segments is a list of 2D points the forms the shape for rotation.
The parametes hasBorder sets if there is a edge between the linear face and the half circle face.
The facetted parameters sets if there ar edges on the radius faces.




##Sensors
Sensors are elements for interacting with the bodies of a scene. By Adding a sensor to a body the body can touched and moved in the way of the sensor.
The idear is taken from the 3D description language of VRML.


#### ISensor
The base interface of the sensors. The event source is only a string and can has the content of "Right mouse button with control key" for describing the trigger of the event the sensor should react on.


#### LinearSensor
**Konstruktor**
```c#
LinearSensor(Vector3D axis, Position3D offset, string eventSource)
```
Linear sensor supports moving the body on an axis.
Parameter *axis* is the move direction.
The parameter *offset* is the offset of the axis.
And the *eventSource* that triggers the sensor moving.
There are also constructors with less parameters. The removed parameters are set with default parameters in this case.

**Methods and properties**
```c#
Vector3D Axis { get; }
```
Move axis of the linear sensor.
```c#
Position3D Offset { get; }
```
The offset of the move direction relativ to the body.
```c#
string EventSource { get; }
```
A string like "Right mouse button with control key" from the client that
matches the sensor event source triggers the sensor.


#### PlaneSensor
**Konstruktor**
```c#
PlaneSensor(Vector3D planeNormal, Position3D planeOffset, string eventSource)
```
Plane sensor supports moving the body on an plane like a chess figure on the playground.
Parameter *planeNormal* is normal of the plane.
The parameter *planeOffset* is the offset of plane relative to body.
And the *eventSource* that triggers the sensor moving.
There are also constructors with less parameters. The removed parameters are set with default parameters in this case.

**Methods and properties**
```c#
Vector3D PlaneNormal { get; }
```
Plane normal of the plane of the plane sensor.
```c#
Position3D PlaneOffset { get; }
```
Plane offset of the plane of the plane sensor.
```c#
string EventSource { get; }
```
A string like "Right mouse button with control key" from the client that
matches the sensor event source triggers the sensor.


#### CylinderSensor
**Konstruktor**
```c#
CylinderSensor(Vector3D axis, Position3D offset, string eventSource)
```
Cylinder sensor supports rotating the body around an axis.
Parameter *axis* is the move rotation.
The parameter *offset* is the offset of the axis.
And the *eventSource* that triggers the sensor moving.
There are also constructors with less parameters. The removed parameters are set with default parameters in this case.

**Methods and properties**
```c#
Vector3D Axis { get; }
```
Move axis of the cylinder sensor.
```c#
Position3D Offset { get; }
```
The offset of the rotation axis relativ to the body.
```c#
string EventSource { get; }
```
A string like "Right mouse button with control key" from the client that
matches the sensor event source triggers the sensor.


#### SphereSensor
**Konstruktor**
```c#
SphereSensor(Position3D sphereOffset, string eventSource)
```
sphere sensor supports rotating the body free around an offset.
The parameter *phereOffset* is the offset of rotating sphere.
And the *eventSource* that triggers the sensor moving.
There are also constructors with less parameters. The removed parameters are set with default parameters in this case.

**Methods and properties**
```c#
Position3D sphereOffset { get; }
```
The offset of the rotation relativ to the body.
```c#
string EventSource { get; }
```
A string like "Right mouse button with control key" from the client that
matches the sensor event source triggers the sensor.