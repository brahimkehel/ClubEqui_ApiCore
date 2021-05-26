using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClubEqui_V_1_1.Models;
using System.Globalization;

namespace ClubEqui_V_1_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TachesController : ControllerBase
    {
        private readonly ClubEquitationBDContext _context;

        public TachesController(ClubEquitationBDContext context)
        {
            _context = context;
        }

        public int GetNumberOfWeek(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            // Return the week of our adjusted day
            return (CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday));
        }

        // GET: api/Taches/ByUser
        [HttpGet]
        [Route("ByUser/{id}")]
        public async Task<ActionResult<Taches_List>> ByUser(int id)
        {
            Taches_List taches_List = new Taches_List();
            taches_List.Taches = await (from t in _context.Taches
                                        where t.UserAttached==id
                                        select new Tach_Helper()
                                        {
                                            IdTask = t.IdTask,
                                            Title = t.Title,
                                            DateDebut = t.DateDebut,
                                            DureeMinutes = t.DureeMinutes,
                                            IsDone = t.IsDone,
                                            UserAttached = t.UserAttached + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur == t.UserAttached select u.Nom).FirstOrDefault()
                                        }).ToListAsync();
            return taches_List;
        }

        // GET: api/Taches/Bydate
        [HttpGet]
        [Route("Bydate")]
        public async Task<ActionResult<Taches_List>> Bydate(DateTime date)
        {
            Taches_List taches_List = new Taches_List();
            taches_List.Taches = await (from t in _context.Taches
                                        where t.DateDebut.Value.Day == date.Day && t.DateDebut.Value.Month == date.Month && t.DateDebut.Value.Year == date.Year
                                        select new Tach_Helper()
                                        {
                                            IdTask = t.IdTask,
                                            Title = t.Title,
                                            DateDebut = t.DateDebut,
                                            DureeMinutes = t.DureeMinutes,
                                            IsDone = t.IsDone,
                                            UserAttached = t.UserAttached+"-"+(from u in _context.Utilisateurs where u.IdUtilisateur == t.UserAttached select u.Nom).FirstOrDefault()
                                        }).ToListAsync();
            return taches_List;
        }

        //GET: api/Taches/statistics
        [HttpGet]
        [Route("statistics")]
        public async Task<ActionResult> statistics()
        {
            var req1 = await (from t in _context.Taches where t.DateDebut.Value.Month == System.DateTime.Now.Month select t).ToListAsync();
            var req2 = await (from t in _context.Taches where t.DateDebut.Value.Month == System.DateTime.Now.Month && t.IsDone == true select t).ToListAsync();
            var req3 = await (from t in _context.Taches where t.DateDebut.Value.Month == System.DateTime.Now.Month && t.IsDone == false select t).ToListAsync();
            var req4 = await (from t in _context.Taches where t.DateDebut.Value.Day == System.DateTime.Now.Day && t.DateDebut.Value.Month == System.DateTime.Now.Month && t.DateDebut.Value.Year == System.DateTime.Now.Year && t.IsDone == true select t).ToListAsync();
            var req5 = await (from t in _context.Taches where t.DateDebut.Value.Day == System.DateTime.Now.Day && t.DateDebut.Value.Month == System.DateTime.Now.Month && t.DateDebut.Value.Year == System.DateTime.Now.Year && t.IsDone == false select t).ToListAsync();

            return Ok(new { nb_permonth=req1.Count(),done_permonth=req2.Count(),undone_permonth=req3.Count(),done_perday=req4.Count(),undone_perday=req5.Count() });
        }

        //GET: api/Taches/ByWeek
        [HttpGet]
        [Route("ByWeek")]
        public async Task<ActionResult> ByWeek()
        {
            var req = await (_context.Taches.FromSqlRaw($"SELECT t.IdTask,t.Title,t.DateDebut,t.DureeMinutes,t.IsDone,t.userAttached,t.description " +
                                                            $"FROM Taches t" +
                                                            $" WHERE DATEDIFF(ww, DATEADD(dd,-1,t.DateDebut), DATEADD(dd,-1,getdate())) = 0").ToListAsync());
            var res = (from t in req select new {
                idTask=t.IdTask,
                title=t.Title,
                dateDebut = t.DateDebut,
                dureeMinutes=t.DureeMinutes,
                description=t.Description,
                isDone=t.IsDone,
                userAttached=t.UserAttached+"-"+(from u in _context.Utilisateurs where u.IdUtilisateur==t.UserAttached select u.Nom).FirstOrDefault()});
       
            return Ok(res.ToList());
        }

        //GET: api/Taches/ByDay
        [HttpGet]
        [Route("ByDay")]
        public async Task<ActionResult<Taches_List>> ByDay()
        {
            Taches_List taches_List = new Taches_List();
            taches_List.Taches = await( from t in _context.Taches
                      where t.DateDebut.Value.Day == System.DateTime.Now.Day && t.DateDebut.Value.Month == System.DateTime.Now.Month && t.DateDebut.Value.Year == System.DateTime.Now.Year
                                        select new Tach_Helper()
                      {
                          IdTask=t.IdTask,
                          Title=t.Title,
                          DateDebut=t.DateDebut,
                          DureeMinutes=t.DureeMinutes,
                          IsDone=t.IsDone,
                          UserAttached= t.UserAttached + "-" + (from u in _context.Utilisateurs where u.IdUtilisateur==t.UserAttached select u.Nom).FirstOrDefault()                         
                      }).ToListAsync();
            return taches_List;
        }        

        // GET: api/Taches
        [HttpGet]
        public async Task<ActionResult<Taches_List>> GetTaches()
        {
            Taches_List taches_List = new Taches_List();
            taches_List.Taches = await (from t in _context.Taches
                                        select new Tach_Helper()
                                        {
                                            IdTask = t.IdTask,
                                            Title = t.Title,
                                            DateDebut = t.DateDebut,
                                            DureeMinutes = t.DureeMinutes,
                                            IsDone = t.IsDone,
                                            UserAttached = t.UserAttached+"-"+(from u in _context.Utilisateurs where u.IdUtilisateur == t.UserAttached select u.Nom).FirstOrDefault()
                                        }).ToListAsync();
            return taches_List;
        }

        // GET: api/Taches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tach>> GetTach(int id)
        {
            var tach = await _context.Taches.FindAsync(id);

            if (tach == null)
            {
                return NotFound();
            }

            return tach;
        }

        // PUT: api/Taches/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTach(int id, Tach tach)
        {
            if (id != tach.IdTask)
            {
                return BadRequest();
            }

            _context.Entry(tach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TachExists(id))
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

        // POST: api/Taches?2
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tach>> PostTach([FromBody]Tach tach,int repetition=1)
        {
            Tach tache_;
            int i = 0;
            int weekDays = 7;
            try
            {
                while(i<repetition)
                {
                    if(i==0)
                    {
                        tache_ = new Tach();
                        tache_.Title = tach.Title;
                        tache_.DureeMinutes = tach.DureeMinutes;
                        tache_.UserAttached = tach.UserAttached;
                        tache_.IsDone = tach.IsDone;
                        tache_.DateDebut = tach.DateDebut;
                        tache_.Description = tach.Description;
                        _context.Taches.Add(tache_);
                        i++;
                    }
                    else
                    {
                        tache_ = new Tach();
                        tache_.Title = tach.Title;
                        tache_.DureeMinutes = tach.DureeMinutes;
                        tache_.UserAttached = tach.UserAttached;
                        tache_.IsDone = tach.IsDone;
                        tache_.DateDebut = tach.DateDebut.Value.AddDays(weekDays);
                        tache_.Description = tach.Description;
                        _context.Taches.Add(tache_);
                        i++;
                        weekDays += weekDays;
                    }
                    await _context.SaveChangesAsync();
              
                }
                if(i==repetition)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // DELETE: api/Taches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tach>> DeleteTach(int id)
        {
            var tach = await _context.Taches.FindAsync(id);
            if (tach == null)
            {
                return NotFound();
            }

            _context.Taches.Remove(tach);
            await _context.SaveChangesAsync();

            return tach;
        }

        private bool TachExists(int id)
        {
            return _context.Taches.Any(e => e.IdTask == id);
        }
    }
}
