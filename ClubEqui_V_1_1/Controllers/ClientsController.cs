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
    public class ClientsController : ControllerBase
    {
        private readonly ClubEquitationBDContext _context;

        public ClientsController(ClubEquitationBDContext context)
        {
            _context = context;
        }

        // POST: api/clients/SeConnecter
        [HttpPost]
        [Route("SeConnecter")]
        public async Task<ActionResult<Client_List>> SeConnecter([FromBody] Client client)
        {
            if (client.Email != null || client.MotPasse != null)
            {
                Client_List list = new Client_List();
                var user = await (from u in _context.Clients
                                  where
                                  u.Email == client.Email && u.MotPasse == client.MotPasse
                                  select u).FirstOrDefaultAsync();
                if (user != null)
                {
                    list.Clients.Add(user);
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

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<Client_List>> GetClients()
        {
            Client_List list = new Client_List();
            list.Clients = await _context.Clients.ToListAsync();
            return list;
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client_List>> GetClient(int id)
        {
            Client_List list = new Client_List();
            list.Clients[0] = await _context.Clients.FindAsync(id);

            if (list.Clients == null)
            {
                return NotFound();
            }

            return list;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.IdClient)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.IdClient == id);
        }
    }
}
