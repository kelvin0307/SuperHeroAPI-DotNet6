using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Model;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await this.context.SuperHeroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            this.context.SuperHeroes.Add(hero);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await this.context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var dbHero = await this.context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }

            this.context.SuperHeroes.Remove(dbHero);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }
    }
}
