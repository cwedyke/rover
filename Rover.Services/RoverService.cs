using System;
using System.Collections.Generic;
using Rover.Common;
using Rover.Models;

namespace Rover.Services
{
    public class RoverService : IRoverService
    {

        /// <summary>
        /// A set of numbered components are placed on a grid, a rover is placed on a random grid
        /// coordinate and has to pick up the numbered components in ascending order(starting with 1).
        /// Once the last component has been picked up the solution is complete.
        /// </summary>
        /// <returns>Returns a list of movements performed by the rover.</returns>
        public IEnumerable<string> ProcessRover(RoverModel model)
        {
            var movementHistory = new List<string>();
            
            foreach (RoverModel.Coordinate componentPosition in model.Components)
            {
                var availableCoords = GetAllCoords(model.GridSize, model.RoverPosition);

                while (!model.RoverPosition.Equals(componentPosition)) // seek until landing on component
                {
                    RoverModel.Coordinate nextMove = GetNextMove(model.RoverPosition, availableCoords, model.GridSize);

                    movementHistory.Add(SetDirection(nextMove, model.RoverPosition));

                    if (availableCoords.Contains(nextMove))
                        availableCoords.Remove(nextMove);
                    
                    model.RoverPosition = nextMove;
                }

                // landed on component so add pickup to movement
                movementHistory.Add("P");
            }

            return movementHistory;
        }

        /// <summary>
        /// Gets the rovers next cardinal movement coordinates.
        /// </summary>
        private RoverModel.Coordinate GetNextMove(RoverModel.Coordinate roverPosition, List<RoverModel.Coordinate> tempList, int gridSize)
        {
            RoverModel.Coordinate nextMove = new RoverModel.Coordinate(-1, -1);
            RoverModel.Coordinate targetCoordinate = new RoverModel.Coordinate(-1,-1);

            // seek out closest unused coordinate
            var distanceList = new List<Tuple<int, RoverModel.Coordinate>>();
            foreach (RoverModel.Coordinate coord in tempList)
            {
                int distance = Math.Abs((coord.Longitude - roverPosition.Longitude)) + Math.Abs((coord.Latitude - roverPosition.Latitude));

                distanceList.Add(new Tuple<int, RoverModel.Coordinate>(distance, coord));
            }

            int closetDistance = gridSize * gridSize;
            foreach (Tuple<int, RoverModel.Coordinate> t in distanceList)
            {
                if (t.Item1 < closetDistance)
                {
                    closetDistance = t.Item1;
                    targetCoordinate = t.Item2;
                }
            }


            // select next move towards targetCoordinate
            if (targetCoordinate.Latitude > roverPosition.Latitude)
                nextMove = new RoverModel.Coordinate(roverPosition.Longitude, roverPosition.Latitude + 1);
            else if (targetCoordinate.Latitude < roverPosition.Latitude)
                nextMove = new RoverModel.Coordinate(roverPosition.Longitude, roverPosition.Latitude - 1);
            else if (targetCoordinate.Longitude > roverPosition.Longitude)
                nextMove = new RoverModel.Coordinate(roverPosition.Longitude + 1, roverPosition.Latitude);
            else if (targetCoordinate.Longitude < roverPosition.Longitude)
                nextMove = new RoverModel.Coordinate(roverPosition.Longitude - 1, roverPosition.Latitude);

            return nextMove;
        }

        /// <summary>
        /// Gets a list of all coordinates created based on grid size minus the rover position.
        /// </summary>
        private List<RoverModel.Coordinate> GetAllCoords(int gridSize, RoverModel.Coordinate roverPosition)
        {
            var list = new List<RoverModel.Coordinate>();

            for (int i = 0; i < gridSize; i++)
            {
                for (int n = 0; n < gridSize; ++n)
                    list.Add(new RoverModel.Coordinate(i, n));
            }

            list.Remove(roverPosition);

            return list;
        }

        /// <summary>
        /// Set direction the rover moved.
        /// </summary>
        private string SetDirection(RoverModel.Coordinate tempMove, RoverModel.Coordinate roverPosition)
        {
            string val = string.Empty;

            if (tempMove.Latitude != roverPosition.Latitude)
            {
                if (tempMove.Latitude > roverPosition.Latitude)
                    val = "N";
                else
                    val = "S";
            }
            else
            {
                if (tempMove.Longitude > roverPosition.Longitude)
                    val = "W";
                else
                    val = "E";
            }

            return val;
        }

    }
}
