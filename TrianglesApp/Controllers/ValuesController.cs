using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TrianglesApp.Controllers
{
    public class ValuesController : ApiController
    {

        [HttpGet]
        [Route("api/GetTriangleCoordinates")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Object GetTriangleCoordinates(int row, int col)
        {
            var x1 = 0;
            var y1 = 0;
            var sideLength = 10;  // 10 pix
            var secondHalf = false; // is the the 2nd triagle in square ?

            // first find the square itself (top)
            y1 = (row - 1) * sideLength;

            if ((col % 2) == 0) // even column
            {
                secondHalf = true;
                x1 = ((col / 2) * sideLength);
            }
            else
            {
                x1 = ((col - 1) * sideLength) / 2;
            }

            var returnObj = new
            {
                V2 = new {
                       xVal = !secondHalf ? x1 : x1 - sideLength,
                       yVal = y1
                },
                V1 = new  {
                      xVal = !secondHalf ? x1 : x1 ,
                      yVal =  !secondHalf ? y1 + sideLength : y1
                },
                V3 = new  {
                   xVal =  !secondHalf ? x1 + sideLength : x1,
                   yVal =  y1 + sideLength
                }
             };


            return returnObj;
        }

        [HttpGet]
        [Route("api/GetTriangleRowColumn")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Object GetTriangleRowColumn(int V1x, int V1y, int V2x, int V2y, int V3x, int V3y)
        {
            var sideLength = 10;  // 10 pix
            var row = 0;
            var col = 0;

            // find row
            var maxY = Math.Max(V2y, V1y);
            maxY = Math.Max(maxY, V3y);
            row = (maxY / sideLength);

            // determine which column
            var maxX = Math.Max(V2x, V1x);
            maxX = Math.Max(maxX, V3x);
            col = (maxX / sideLength);
            // adjust for double triangles in a square
            col += (V1x / sideLength);

            var returnObj = new
            {
                row = row,
                col = col
            };
            return returnObj;
        }
        }
}
