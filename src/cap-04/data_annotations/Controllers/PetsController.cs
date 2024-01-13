using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace data_annotations.Controllers;

[ApiController]
[Route("[controller]")]
public class PetsController(ILogger<PetsController> logger) : ControllerBase
{

    [HttpPost]
    public IActionResult Post(PetModel pet)
    {
        return Ok(pet);
    }
}

public class PetModel
{
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required]
    [AllowedValues("Beagle", "Pitbull", "Boxer")]
    [DeniedValues("Chihuahua")]
    public required string Breed { get; set; }

    [Base64String]
    public string? Photo { get; set; }   
}