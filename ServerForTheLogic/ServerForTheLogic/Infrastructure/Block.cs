using Newtonsoft.Json;
using ServerForTheLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForTheLogic.Infrastructure
{
    /// <summary>
    /// Block is a layer of abstraction used for city development, each
    /// block consists of 1 type of building, and a grid of 8x4 grid cells.
    /// This object allows for easier city expansion and zoning.
    /// <para>Written by: Chandu Dissanayake 2017-11-13</para>
    /// <para>Last modified by Connor Goudie 2017-11-14</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Block
    {
        //width of the landplot array in terms of grid cells
        public const int BLOCK_WIDTH = 4;

        //height of the landplot array in terms of grid cells
        public const int BLOCK_LENGTH = 8;

        //starting point of the landplot in terms of world grid
        [JsonProperty]
        public Point StartPoint { get; }

        //blocks adjacent to the current one
        public List<Block> Adjacents { get; }

        //[JsonProperty]
        //2d array of cells that contain locations
        public Location[,] LandPlot;

        //type of buildings a block will hold
        [JsonProperty]
        public BlockType Type { get;  set; }

        /// <summary>
        /// Constructs a new Block object from the passed start point
        /// </summary>
        /// <para>Written by Connor Goudie 2017-10-14</para>
        /// <para>Last modified by Connor Goudie 2017-11-14</para>
        /// <param name="StartPoint"></param>
        public Block(Point StartPoint)
        {
            if (!StartPoint.Equals(null))
                this.StartPoint = StartPoint;
            else
                throw new InvalidOperationException("must pass in a start point");

            LandPlot = new Location[BLOCK_WIDTH, BLOCK_LENGTH];
            Adjacents = new List<Block>();
            Type = BlockType.Empty;
        }

        /// <summary>
        /// Prints the block object in a formatted manner
        /// </summary>
        /// <para>Written by Connor Goudie 2017-10-14</para>
        /// <para>Last modified by Connor Goudie 2017-11-14</para>
        /// <returns></returns>
        public override string ToString()
        {
            return "Start point:" + StartPoint.ToString() + " Block type:" + Type.ToString();
        }
    }

    /// <summary>
    /// Type of block, this is used as a way to zone each block and ensure only 1 type of
    /// building exists in each.
    /// <para>Written by Connor Goudie 2017-10-14</para>
    /// </summary>
    public enum BlockType { Residential, Industrial, Commercial, Empty }
}
