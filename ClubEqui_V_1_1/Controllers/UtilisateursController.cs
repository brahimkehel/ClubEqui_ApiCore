using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClubEqui_V_1_1.Models;
using Microsoft.AspNetCore.Cors;

namespace ClubEqui_V_1_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class UtilisateursController : ControllerBase
    {
        private readonly ClubEquitationBDContext _context;

        public UtilisateursController(ClubEquitationBDContext context)
        {
            _context = context;
        }

        // POST: api/Utilisateurs/SeConnecter
        [HttpPost]
        [Route("SeConnecter")]
        public async Task<ActionResult<Utilisateur_List>> SeConnecter([FromBody]Utilisateur utilisateur)
        {
            if(utilisateur.Email != null || utilisateur.MotPasse != null)
            {
                Utilisateur_List list = new Utilisateur_List();
                var user =await (from u in _context.Utilisateurs
                                    where
                                    u.Email == utilisateur.Email && u.MotPasse == utilisateur.MotPasse
                            select u).FirstOrDefaultAsync();
                if(user!=null)
                {
                    list.Utilisateurs.Add(user);
                    return list;
                }
                else
                {
                    return BadRequest("Bad cridentiels");
                }
            }
            else
            {
                return BadRequest("Login denied");
            }
            
        }

        // GET: api/Utilisateurs/
        [Route("GetUtilisateursParType/{type}")]
        [HttpGet]
        public async Task<ActionResult<Utilisateur_List>> GetUtilisateurs(int type)
        {
            Utilisateur_List utilisateur_List;
            if (type.Equals(1))
            {
                utilisateur_List = new Utilisateur_List();
                utilisateur_List.Utilisateurs=await _context.Utilisateurs.Where(u => u.TypeUtilsateur == "salarie").ToListAsync();
                return utilisateur_List; 
            }
            else if (type.Equals(2))
            {
                utilisateur_List = new Utilisateur_List();
                utilisateur_List.Utilisateurs = await _context.Utilisateurs.Where(u => u.TypeUtilsateur == "administrateur").ToListAsync();
                return utilisateur_List;
            }
            else
            {
                return BadRequest("type not found");
            }
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.IdUtilisateur)
            {
                return BadRequest();
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.IdUtilisateur }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Utilisateur>> DeleteUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return utilisateur;
        }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.IdUtilisateur == id);
        }
    }
}
