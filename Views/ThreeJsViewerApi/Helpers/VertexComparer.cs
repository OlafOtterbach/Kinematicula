using Kinematicula.Graphics;

namespace ThreeJsViewerApi.Helpers;

public class VertexComparer : IEqualityComparer<Vertex>
{
    public bool Equals(Vertex vertex1, Vertex vertex2)
    {
        if (vertex2 == null && vertex1 == null)
            return true;
        else if (vertex1 == null || vertex2 == null)
            return false;
        else if (vertex1.Point.Position == vertex2.Point.Position && vertex1.Normal == vertex2.Normal)
            return true;
        else
            return false;
    }

    public int GetHashCode(Vertex vertex)
    {
        int hashCode = vertex.Point.Position.GetHashCode() ^ vertex.Normal.GetHashCode();
        return hashCode.GetHashCode();
    }
}
