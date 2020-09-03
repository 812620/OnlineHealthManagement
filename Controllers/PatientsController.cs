using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHealthManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;


namespace OnlineHealthManagement.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {


        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PatientsController));
        private readonly OnlineHMContext _context;
        
        public PatientsController(OnlineHMContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]

        public dynamic GetPatient()
        {
            _log4net.Info("PatientsController HttpGet ");
            var query =
             (
                 from patient in _context.Patient
                 select new
                 {
                     patient.PatientId,
                     patient.Name,
                     patient.Age,
                     patient.Height,
                     patient.Weight,
                     patient.Sex,
                     patient.Address,
                     patient.PhoneNumber,
                     patient.Date
                 }
             ).ToList();
            return query;
        }

        /*public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            return await _context.Patient.ToListAsync();
        }*/

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _context.Patient.Add(patient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.PatientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatient", new { id = patient.PatientId }, patient);
        }

        // DELETE: api/Patients/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.PatientId == id);
        }
    }
}
