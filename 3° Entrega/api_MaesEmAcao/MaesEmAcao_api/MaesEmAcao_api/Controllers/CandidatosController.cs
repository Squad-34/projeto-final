﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaesEmAcao_api.Context;
using MaesEmAcao_api.Models;

namespace MaesEmAcao_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private readonly DataContext _context;

        public CandidatosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Candidatos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidato>>> GetCandidatos()
        {
          if (_context.Candidatos == null)
          {
              return NotFound();
          }
            return await _context.Candidatos.ToListAsync();
        }

        // GET: api/Candidatos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
          if (_context.Candidatos == null)
          {
              return NotFound();
          }
            var candidato = await _context.Candidatos.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            return candidato;
        }

        // PUT: api/Candidatos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato(int id, Candidato candidato)
        {
            if (id != candidato.id)
            {
                return BadRequest();
            }

            _context.Entry(candidato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatoExists(id))
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

        // POST: api/Candidatos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Candidato>> PostCandidato(Candidato candidato)
        {
          if (_context.Candidatos == null)
          {
              return Problem("Entity set 'DataContext.Candidatos'  is null.");
          }
            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidato", new { id = candidato.id }, candidato);
        }

        // DELETE: api/Candidatos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidato(int id)
        {
            if (_context.Candidatos == null)
            {
                return NotFound();
            }
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidatos.Remove(candidato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidatoExists(int id)
        {
            return (_context.Candidatos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
