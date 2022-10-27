namespace FormiculaDemonstration.Robot.Graphics;

using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using System;
using System.Collections.Generic;

public class Robot : Body
{
    private record AxisDescription(Func<double> Getter, Action<double> Setter, double Mininum, double Maximum);

    private Dictionary<int, RotationAxisConstraint> _axes = new Dictionary<int, RotationAxisConstraint>();

    public void AddAxis(RotationAxisConstraint axisConstraint)
    {
        int axisNumber = _axes.Keys.Count + 1;
        _axes[axisNumber] = axisConstraint;
    }

    public double GetAxisAngle(int axisNumber)
        => _axes.ContainsKey(axisNumber) ? _axes[axisNumber].Angle : double.NaN;

    public void SetAxisAngle(int axisNumber, double axisAngle)
    {
        if (_axes.ContainsKey(axisNumber))
        {
            axisAngle = axisAngle < _axes[axisNumber].MinimumAngle ? _axes[axisNumber].MinimumAngle : axisAngle > _axes[axisNumber].MaximumAngle ? _axes[axisNumber].MaximumAngle : axisAngle;
            _axes[axisNumber].Angle = axisAngle;
        }
    }

    public Matrix44D AdapterToolCenterFrame => Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 5), new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0));
}
