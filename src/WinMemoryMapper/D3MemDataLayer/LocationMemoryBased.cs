using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using WinMemoryMapper;

namespace D3MemDataLayer
{
    public enum Quad : uint
    {
        Quad1 = 1,
        Quad2 = 2,
        Quad3 = 3,
        Quad4 = 4,
    }

    public class LocationMemoryBased : MemoryContainerBase, ILocation
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float z { get; private set; }

        public LocationMemoryBased(float x, float y, float z)
        {
            this.Init(x, y, z);
        }

        public LocationMemoryBased(IMemoryMapper mapper)
        {
            this.Mapper = mapper;
        }

        public void InitLocationStack(uint startAddr)
        {
            var x = this.Mapper.Read<float>(startAddr + 0x000);
            var y = this.Mapper.Read<float>(startAddr + 0x004);
            var z = this.Mapper.Read<float>(startAddr + 0x008);
            this.Init(x, y, z);
        }

        public void InitLocationPtr(uint ptrAddr)
        {
            var startAddr = this.Mapper.Read<uint>(ptrAddr);
            var x = this.Mapper.Read<float>(startAddr + 0x000);
            var y = this.Mapper.Read<float>(startAddr + 0x004);
            var z = this.Mapper.Read<float>(startAddr + 0x008);
            this.Init(x, y, z);
        }

        public void Init(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double DistanceTo(LocationMemoryBased other)
        {
            var xd = other.x - this.x;
	        var yd = other.y - this.y;
	        var zd = other.z - this.z;
            return Math.Sqrt(xd * xd + yd * yd + zd * zd);
        }

        public LocationMemoryBased ApplyTransform(Matrix transform)
        {
            var points = new PointF[1];
            points[0] = new PointF(this.x, this.y);
            transform.TransformPoints(points);
            return new LocationMemoryBased(points[0].X, points[0].Y, this.z);
        }

        /// <summary>
        /// Agnles the from360.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public double AngleFrom360(LocationMemoryBased other)
        {
            var angleRaw = Math.Abs(this.AngleFrom(other));
            var quad = this.QuadrantFrom(other);
            double result = 0;
            if (quad == Quad.Quad1)
            {
                result = angleRaw;
            }
            else if (quad == Quad.Quad2)
            {
                result = 180 - angleRaw;
            }
            else if (quad == Quad.Quad3)
            {
                result = 180 + angleRaw;
            }
            else if (quad == Quad.Quad4)
            {
                result = 360 - angleRaw;
            }
            return result;
        }

        /// <summary>
        /// Angles from. In Degrees
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public double AngleFrom(LocationMemoryBased other)
        {
            var radians = Math.Atan((this.y - other.y) / (this.x - other.x));
            return radians * (180 / Math.PI);
        }

        /// <summary>
        /// Quadrants from.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public Quad QuadrantFrom(LocationMemoryBased other)
        {
            if (other.y >= this.y && other.x <= this.x)
            {
                return Quad.Quad2;
            } 
            else if (other.y >= this.y && other.x >= this.x) 
            {
                return Quad.Quad1;
            }
            else if (other.y <= this.y && other.x <= this.x)
            {
                return Quad.Quad3;
            }
            else if (other.y <= this.y && other.x >= this.x)
            {
                return Quad.Quad4;
            }

            return 0;
        }

        float ILocation.x
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        float ILocation.y
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        float ILocation.z
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
