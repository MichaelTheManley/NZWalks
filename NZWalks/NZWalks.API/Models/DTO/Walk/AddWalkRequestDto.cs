﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.Walk
{
    public class AddWalkRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters.")]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
