namespace ThreeJsViewerApi.Helpers;

using ThreeJsViewerApi.GraphicsModel;

public class VertexComparer : IEqualityComparer<VertexTjs>
{
    public bool Equals(VertexTjs vertex1, VertexTjs vertex2)
    {
        if (vertex2 == null && vertex1 == null)
            return true;
        else if (vertex1 == null || vertex2 == null)
            return false;
        else if (    vertex1.Position == vertex2.Position
                  && vertex1.Normal == vertex2.Normal
                  && vertex1.Color.Alpha == vertex2.Color.Alpha
                  && vertex1.Color.Red == vertex2.Color.Red
                  && vertex1.Color.Green == vertex2.Color.Green
                  && vertex1.Color.Blue == vertex2.Color.Blue)
            return true;
        else
            return false;
    }

    public int GetHashCode(VertexTjs vertex)
    {
        int hashCode =    vertex.Position.GetHashCode()
                        ^ vertex.Normal.GetHashCode()
                        ^ vertex.Color.Alpha.GetHashCode()
                        ^ vertex.Color.Red.GetHashCode()
                        ^ vertex.Color.Green.GetHashCode()
                        ^ vertex.Color.Blue.GetHashCode()
        ;
        return hashCode.GetHashCode();
    }
}
