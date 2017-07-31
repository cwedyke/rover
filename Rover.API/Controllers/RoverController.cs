using System;
using System.Collections.Generic;
using System.Web.Http;
using Rover.API.Dtos;
using Rover.Models;
using Rover.Common;
using Rover.Services;

namespace Rover.API.Controllers
{
    public class RoverController : ApiController
    {
        public IRoverService RoverService { get; }

        /// <summary>
        /// Default constr that inits Rover.Services.RoverService().
        /// </summary>
        public RoverController()
        {
            RoverService = new Rover.Services.RoverService();
        }

        /// <summary>
        /// Optional constr service declaration to allow Dependancy Injection.
        /// </summary>
        public RoverController(IRoverService roverService)
        {
            RoverService = roverService;
        }

        /// <summary>
        /// Execute rover per arguments.
        /// </summary>
        /// <returns>Returns a list of the rovers movements.</returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]RoverDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // convert dto to model
            var comps = new List<RoverModel.Coordinate>();
            foreach (string coord in dto.Components)
            {
                comps.Add(RoverModel.ToCoordinate(coord));
            }

            var model = new RoverModel()
            {
                GridSize = Convert.ToInt32(dto.GridSize),
                RoverPosition = RoverModel.ToCoordinate(dto.RoverPosition),
                Components = comps
            };
            // end convert to model
            

            var roverMovements =  RoverService.ProcessRover(model);

            return Ok(roverMovements);
        }



    }
}
