using System.Collections.Generic;
using Rover.Models;

namespace Rover.Common
{
    public interface IRoverService
    {
        IEnumerable<string> ProcessRover(RoverModel model);
    }
}
