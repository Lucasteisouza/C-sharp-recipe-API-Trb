using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    // 1 - Sua aplicação deve ter o endpoint GET /recipe
    //Read
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(this._service.GetRecipes());   
    }

    // 2 - Sua aplicação deve ter o endpoint GET /recipe/:name
    //Read
    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {                
        var currentRecipe = this._service.GetRecipe(name);
        if (currentRecipe == null)
        {
            return NotFound();
        }
        return Ok(currentRecipe);
    }

    // 3 - Sua aplicação deve ter o endpoint POST /recipe
    [HttpPost]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        this._service.AddRecipe(recipe);
        return CreatedAtRoute("GetRecipe", new { name = recipe.Name }, recipe);
    }

    // 4 - Sua aplicação deve ter o endpoint PUT /recipe
    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {
        var currentRecipe = this._service.GetRecipe(name);
        if (currentRecipe == null || name != recipe.Name.ToLower())
        {
            return BadRequest();
        }
        this._service.UpdateRecipe(recipe);
        return NoContent();
    }

    // 5 - Sua aplicação deve ter o endpoint DEL /recipe
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var currentRecipe = this._service.GetRecipe(name);
        if (currentRecipe == null)
        {
            return NotFound();
        }
        this._service.DeleteRecipe(name);
        return NoContent();
    }    
}
