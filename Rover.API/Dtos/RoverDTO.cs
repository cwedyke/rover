using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rover.API.Dtos
{
    public class RoverDTO : IValidatableObject
    {
        public string GridSize { get; set; }
        public string RoverPosition { get; set; }
        public string[] Components { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // add validation for positive numeric gridsize

            // add validation for RoverPosition and Components coordinate string formatting

            // add validation for coordinates based on gridsize to make sure they arent out of bounds

            if (false)
                yield return new ValidationResult("Invalid format.");
        }
    }
}