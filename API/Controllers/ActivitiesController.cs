using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ActivitiesController : BaseAPIController
    {
        private readonly DataContext _context;
        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        /* Error: 'DbSet<Activities>' does not contain a definition for 'ToListAsync' and no accessible extension 
        method 'ToListAsync' accepting a first argument of type 'DbSet<Activities>' could be found.
        Solution: Add Microsoft.EntityFrameworkCore namespace */
        [HttpGet] // api/Activities/GetActivities
        public async Task<ActionResult<List<Activities>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] // api/Activities/GetActivities/requiredID
        public async Task<ActionResult<Activities>> GetActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id);
        }
    }
}