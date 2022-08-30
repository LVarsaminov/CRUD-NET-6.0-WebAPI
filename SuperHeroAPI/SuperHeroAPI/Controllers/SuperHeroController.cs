using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found!");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var hero = await _context.SuperHeroes.FindAsync(updatedHero.Id);
            if (hero == null)
                return BadRequest("Hero not found!");
            hero.Name = updatedHero.Name;
            hero.FirstName = updatedHero.FirstName;
            hero.LastName = updatedHero.LastName;
            hero.BirthPlace = updatedHero.BirthPlace;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete]
        //public async Task<ActionResult<List<SuperHero>>> DeleteHeroes()
        //{
        //    heroes.Clear();
        //    if (heroes.Count == 0)
        //        return BadRequest("Heroes are already null");
        //    return Ok(heroes);
        //}
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            if (hero == null)
                return BadRequest("Hero not found!");
            return Ok(hero);
        }
    }
}