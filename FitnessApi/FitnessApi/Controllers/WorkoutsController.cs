using FitnessApi.Data;
using FitnessApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private FitnessDbContext _dbContext;

        public WorkoutsController(FitnessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(Workout WorkoutObj)
        {
            _dbContext.Workouts.Add(WorkoutObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetWorkout(string sort, int? pageNumber, int? pageSize)
        {
            var CurrentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            var workouts = from workout in _dbContext.Workouts
                               join customer in _dbContext.Users on workout.UserId equals customer.Id
                               select new
                               {
                                   workoutName = workout.WorkoutName,
                                   Date = workout.Date,
                               };

            switch (sort)
            {
                case "desc":
                    return Ok(workouts.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderByDescending(w => w.Date));
                case "asc":
                    return Ok(workouts.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderBy(w => w.Date));
                default:
                    return Ok(workouts.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }

        }

        // Get api/Workout/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetWorkoutDetail(int id)
        {
            var workouts = (from workout in _dbContext.Workouts
                                join customer in _dbContext.Users on workout.UserId equals customer.Id
                                where workout.Id == id
                                select new
                                {
                                    WorkoutName = workout.WorkoutName,
                                    Sets = workout.Sets,
                                    Reps = workout.Reps,
                                    Date = workout.Date,
                                }).FirstOrDefault();
            return Ok(workouts);
        }


        // DELETE api/Workout/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var workout = _dbContext.Workouts.Find(id);
            if (workout == null)
            {
                return NotFound("Record not found against this id");
            }
            else
            {
                _dbContext.Workouts.Remove(workout);
                _dbContext.SaveChanges();
                return Ok("Record deleted");
            }

        }

        // /api/workouts/FindWorkouts?UserId=1&pageNumber=1&pageSize=10
        [Authorize]
        [HttpGet("[Action]")]
        public IActionResult FindWorkouts(int UserId,string sort, int? pageNumber, int? pageSize)
        {
            var workouts = from workout in _dbContext.Workouts
                           join customer in _dbContext.Users on workout.UserId equals customer.Id
                           where workout.UserId == UserId
                         select new
                         {
                             Id = workout.Id,
                             workoutName = workout.WorkoutName,
                             Reps = workout.Reps,
                             Sets = workout.Sets,
                             Date = workout.Date,
                             UserId = workout.UserId
                         };
            var CurrentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            switch (sort)
            {
                case "desc":
                    return Ok(workouts.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderByDescending(w => w.Date));
                case "asc":
                    return Ok(workouts.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderBy(w => w.Date));
                default:
                    return Ok(workouts.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }

        }
    }
}
