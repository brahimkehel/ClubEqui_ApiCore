using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClubEqui_V_1_1.Models;

namespace ClubEqui_V_1_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeancesController : ControllerBase
    {
        private readonly ClubEquitationBDContext _context;

        public SeancesController(ClubEquitationBDContext context)
        {
            _context = context;
        }

        //GET: api/Seances/statisticsForMonitor
        [HttpGet]
        [Route("statisticsForMonitor")]
        public async Task<ActionResult> statisticsForMonitor(int id)
        {
            var req_seances_per_week = await (_context.Seances.FromSqlRaw($"SELECT * " +
                                                           $"FROM Seance s" +
                                                           $" WHERE DATEDIFF(ww, DATEADD(dd,-1,s.DateDebut), DATEADD(dd,-1,getdate())) = 0 and s.IdMoniteur=" + id).ToListAsync());

            var req_taches_per_week = await (_context.Taches.FromSqlRaw($"SELECT * " +
                                                           $"FROM Taches t" +
                                                           $" WHERE DATEDIFF(ww, DATEADD(dd,-1,t.DateDebut), DATEADD(dd,-1,getdate())) = 0 and t.UserAttached=" + id).ToListAsync());

            return Ok(new { nb_taches_per_week = req_taches_per_week.Count, nb_seances_per_week=req_seances_per_week.Count });
        }

        // GET: api/Seances/BydateNMonitor
        [HttpGet]
        [Route("BydateNMonitor")]
        public async Task<ActionResult<Seance_List>> BydateNMonitor(DateTime date, int id)
        {
            Seance_List seance_List = new Seance_List();
            seance_List.Seances = await (from t in _context.Seances
                                         where t.IdMoniteur == id && t.DateDebut.Value.Day == date.Day && t.DateDebut.Value.Month == date.Month && t.DateDebut.Value.Year == date.Year
                                         select new Seance_Helper()
                                         {
                                             IdSeance = t.IdSeance,
                                             IdMoniteur = t.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == t.IdMoniteur select u.Nom).FirstOrDefault(),
                                             IdClient = t.IdClient + "-" + (from c in _context.Clients where c.IdClient == t.IdClient select c.Nom).FirstOrDefault(),
                                             IdPayement = t.IdPayement,
                                             Commentaires = t.Commentaires,
                                             DateDebut = t.DateDebut,
                                             DureeMinutes = t.DureeMinutes,
                                             IsDone = t.IsDone
                                         }).ToListAsync();
            return seance_List;
        }

        //GET: api/Seances/ByWeekNMonitor
        [HttpGet]
        [Route("ByWeekNMonitor")]
        public async Task<ActionResult> ByWeekNMonitor(int id)
        {
            var req = await (_context.Seances.FromSqlRaw($"SELECT * " +
                                                            $"FROM Seance s" +
                                                            $" WHERE DATEDIFF(ww, DATEADD(dd,-1,s.DateDebut), DATEADD(dd,-1,getdate())) = 0 and s.IdMoniteur=" + id).ToListAsync());
            var res = (from t in req
                       select new Seance_Helper
                       {
                           IdSeance = t.IdSeance,
                           IdMoniteur = t.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == t.IdMoniteur select u.Nom).FirstOrDefault(),
                           IdClient = t.IdClient + "-" + (from c in _context.Clients where c.IdClient == t.IdClient select c.Nom).FirstOrDefault(),
                           IdPayement = t.IdPayement,
                           Commentaires = t.Commentaires,
                           DateDebut = t.DateDebut,
                           DureeMinutes = t.DureeMinutes,
                           IsDone = t.IsDone
                       });
            Seance_List seance_List = new Seance_List();
            seance_List.Seances = res.ToList();
            return Ok(seance_List);
        }

        //GET: api/Seances/statistics
        [HttpGet]
        [Route("statistics")]
        public async Task<ActionResult> statistics()
        {
            var req = await (from s in _context.Seances where s.DateDebut.Value.Day == System.DateTime.Now.Day && s.DateDebut.Value.Month == System.DateTime.Now.Month && s.DateDebut.Value.Year == System.DateTime.Now.Year select s).ToListAsync();

            return Ok(new { nb_perday=req.Count });
        }

        //GET: api/Seances/ByUser
        [HttpGet]
        [Route("ByUser")]
        public async Task<ActionResult<Seance_List>> ByUser(int id)
        {
            Seance_List seance_List = new Seance_List();
            seance_List.Seances = await (from s in _context.Seances
                                         where s.IdMoniteur==id 
                                         select new Seance_Helper()
                                         {
                                             IdSeance = s.IdSeance,
                                             IdMoniteur = s.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == s.IdMoniteur select u.Nom).FirstOrDefault(),
                                             IdClient = s.IdClient + "-" + (from c in _context.Clients where c.IdClient == s.IdClient select c.Nom).FirstOrDefault(),
                                             IdPayement = s.IdPayement,
                                             Commentaires = s.Commentaires,
                                             DateDebut = s.DateDebut,
                                             DureeMinutes = s.DureeMinutes,
                                             IsDone = s.IsDone

                                         }).ToListAsync();
            return seance_List;
        }

        // GET: api/Seances/BydateNUser
        [HttpGet]
        [Route("BydateNUser")]
        public async Task<ActionResult<Seance_List>> BydateNUser(DateTime date,int id)
        {
            Seance_List seance_List = new Seance_List();
            seance_List.Seances = await (from t in _context.Seances
                                         where t.IdClient == id && t.DateDebut.Value.Day == date.Day && t.DateDebut.Value.Month == date.Month && t.DateDebut.Value.Year == date.Year
                                         select new Seance_Helper()
                                         {
                                             IdSeance = t.IdSeance,
                                             IdMoniteur = t.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == t.IdMoniteur select u.Nom).FirstOrDefault(),
                                             IdClient = t.IdClient + "-" + (from c in _context.Clients where c.IdClient == t.IdClient select c.Nom).FirstOrDefault(),
                                             IdPayement = t.IdPayement,
                                             Commentaires = t.Commentaires,
                                             DateDebut = t.DateDebut,
                                             DureeMinutes = t.DureeMinutes,
                                             IsDone = t.IsDone
                                         }).ToListAsync();
            return seance_List;
        }

        //GET: api/Seances/ByWeekNUser
        [HttpGet]
        [Route("ByWeekNUser")]
        public async Task<ActionResult> ByWeekNUser(int id)
        {
            var req = await (_context.Seances.FromSqlRaw($"SELECT * " +
                                                            $"FROM Seance s" +
                                                            $" WHERE DATEDIFF(ww, DATEADD(dd,-1,s.DateDebut), DATEADD(dd,-1,getdate())) = 0 and s.IdClient="+id).ToListAsync());
            var res = (from t in req
                       select new Seance_Helper
                       {
                           IdSeance = t.IdSeance,
                           IdMoniteur = t.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == t.IdMoniteur select u.Nom).FirstOrDefault(),
                           IdClient = t.IdClient + "-" + (from c in _context.Clients where c.IdClient == t.IdClient select c.Nom).FirstOrDefault(),
                           IdPayement = t.IdPayement,
                           Commentaires = t.Commentaires,
                           DateDebut = t.DateDebut,
                           DureeMinutes = t.DureeMinutes,
                           IsDone = t.IsDone
                       });
            Seance_List seance_List = new Seance_List();
            seance_List.Seances =res.ToList();
            return Ok(seance_List);
        }

        //GET: api/Seances/ByWeek
        [HttpGet]
        [Route("ByWeek")]
        public async Task<ActionResult> ByWeek()
        {
            var req = await (_context.Seances.FromSqlRaw($"SELECT * " +
                                                            $"FROM Seance s" +
                                                            $" WHERE DATEDIFF(ww, DATEADD(dd,-1,s.DateDebut), DATEADD(dd,-1,getdate())) = 0").ToListAsync());
            var res = (from t in req
                       select new Seance_Helper
                       {
                           IdSeance = t.IdSeance,
                           IdMoniteur =t.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == t.IdMoniteur select u.Nom).FirstOrDefault(),
                           IdClient =t.IdClient + "-" + (from c in _context.Clients where c.IdClient == t.IdClient select c.Nom).FirstOrDefault(),
                           IdPayement = t.IdPayement,
                           Commentaires = t.Commentaires,
                           DateDebut = t.DateDebut,
                           DureeMinutes = t.DureeMinutes,
                           IsDone = t.IsDone
                       });
            Seance_List seance_List = new Seance_List();
            seance_List.Seances = res.ToList();

            return Ok(seance_List);
        }

        //GET: api/Seances/ByDay
        [HttpGet]
        [Route("ByDay")]
        public async Task<ActionResult<Seance_List>> ByDay()
        {
            Seance_List seance_List = new Seance_List();
            seance_List.Seances = await (from s in _context.Seances
                                        where s.DateDebut.Value.Day == System.DateTime.Now.Day && s.DateDebut.Value.Month == System.DateTime.Now.Month && s.DateDebut.Value.Year == System.DateTime.Now.Year
                                        select new Seance_Helper()
                                        {
                                            IdSeance = s.IdSeance,
                                            IdMoniteur = s.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == s.IdMoniteur select u.Nom).FirstOrDefault(),
                                            IdClient = s.IdClient + "-" + (from c in _context.Clients where c.IdClient == s.IdClient select c.Nom).FirstOrDefault(),
                                            IdPayement = s.IdPayement,
                                            Commentaires = s.Commentaires,
                                            DateDebut = s.DateDebut,
                                            DureeMinutes = s.DureeMinutes,
                                            IsDone = s.IsDone

                                        }).ToListAsync();
            return seance_List;
        }

        // GET: api/Seances/Bydate
        [HttpGet]
        [Route("Bydate")]
        public async Task<ActionResult<Seance_List>> Bydate(DateTime date)
        {
            Seance_List seance_List = new Seance_List();
            seance_List.Seances = await (from t in _context.Seances
                                        where t.DateDebut.Value.Day == date.Day && t.DateDebut.Value.Month == date.Month && t.DateDebut.Value.Year == date.Year
                                        select new Seance_Helper()
                                        {
                                            IdSeance = t.IdSeance,
                                            IdMoniteur =t.IdMoniteur + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == t.IdMoniteur select u.Nom).FirstOrDefault(),
                                            IdClient = t.IdClient + "-" + (from c in _context.Clients where c.IdClient == t.IdClient select c.Nom).FirstOrDefault(),
                                            IdPayement = t.IdPayement,
                                            Commentaires = t.Commentaires,
                                            DateDebut = t.DateDebut,
                                            DureeMinutes = t.DureeMinutes,
                                            IsDone = t.IsDone
                                        }).ToListAsync();
            return seance_List;
        }


        // GET: api/Seances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeances()
        {
            return await _context.Seances.ToListAsync();
        }

        // GET: api/Seances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seance>> GetSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }

        // PUT: api/Seances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeance(int id, Seance seance)
        {
            if (id != seance.IdSeance)
            {
                return BadRequest();
            }

            _context.Entry(seance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(id))
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

        // POST: api/Seances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Seance>> PostSeance([FromBody]Seance seance, int repetition = 1)
        {
            Seance seance_;
            int i = 0;
            int weekDays = 7;
            try
            {
                while (i < repetition)
                {
                    if (i == 0)
                    {
                        seance_ = new Seance();
                        seance_.IdClient = seance.IdClient;
                        seance_.IdMoniteur = seance.IdMoniteur;
                        seance_.IdPayement = seance.IdPayement;
                        seance_.IsDone = seance.IsDone;
                        seance_.DureeMinutes = seance.DureeMinutes;
                        seance_.Commentaires = seance.Commentaires;
                        seance_.DateDebut = seance.DateDebut;
                        _context.Seances.Add(seance_);
                        i++;
                    }
                    else
                    {
                        seance_ = new Seance();
                        seance_.IdClient = seance.IdClient;
                        seance_.IdMoniteur = seance.IdMoniteur;
                        seance_.IdPayement = seance.IdPayement;
                        seance_.IsDone = seance.IsDone;
                        seance_.DureeMinutes = seance.DureeMinutes;
                        seance_.Commentaires = seance.Commentaires;
                        seance_.DateDebut = seance.DateDebut.Value.AddDays(weekDays);
                        _context.Seances.Add(seance_);
                        i++;
                        weekDays += weekDays;
                    }
                    await _context.SaveChangesAsync();

                }
                if (i == repetition)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Seances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seance>> DeleteSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);
            if (seance == null)
            {
                return NotFound();
            }

            _context.Seances.Remove(seance);
            await _context.SaveChangesAsync();

            return seance;
        }

        private bool SeanceExists(int id)
        {
            return _context.Seances.Any(e => e.IdSeance == id);
        }
    }
}
