﻿using Microsoft.AspNetCore.Mvc;
using Tabloid.Data;
using Tabloid.Models;
using Tabloid.Repositories;

namespace Tabloid.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {
        private readonly PostTagRepository _postTagRepository;
       
        public PostTagController(ApplicationDbContext context)
        {
            _postTagRepository = new PostTagRepository(context);
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postTagRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Post(PostTag postTag)
        {
            _postTagRepository.Add(postTag);
            return CreatedAtAction("Get", new { id = postTag.Id }, postTag);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postTagRepository.Delete(id);
            return NoContent();
        }
    }
}
