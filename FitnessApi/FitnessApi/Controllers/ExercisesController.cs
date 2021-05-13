using FitnessApi.Data;
using FitnessApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
            private FitnessDbContext _dbContext;

            public ExercisesController(FitnessDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            // Get api/Exercises/
            [Authorize]
            [HttpGet]
            public IActionResult GetExercise(string sort, int? pageNumber, int? pageSize)
            {
                var CurrentPageNumber = pageNumber ?? 1;
                var currentPageSize = pageSize ?? 5;
                var exercises = from exercise in _dbContext.Exercises
                                select new
                                {
                                    Id = exercise.Id,
                                    ExerciseName = exercise.ExerciseName,
                                    Difficulty = exercise.Difficulty,
                                    ImageUrl = exercise.ImageUrl
                                };

                switch (sort)
                {
                    case "desc":
                        return Ok(exercises.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderByDescending(e => e.ExerciseName));
                    case "asc":
                        return Ok(exercises.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderBy(e => e.ExerciseName));
                    default:
                        return Ok(exercises.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize));
                }

            }


            // Get api/Exercises/5
            [Authorize]
            [HttpGet("{Id}")]
            public IActionResult GetExerciseDetail(int Id)
            {
                var exercise = _dbContext.Exercises.Find(Id);
                if (exercise == null)
                {
                    return NotFound();
                }
                return Ok(exercise);
            }



            //POST api/exercises
            [Authorize(Roles = "Admin")]
            [HttpPost]
            public IActionResult AddExercise([FromForm] Exercise exercise)
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");
                if (exercise.Image != null)
                {
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    exercise.Image.CopyTo(fileStream);
                }
                exercise.ImageUrl = filePath.Remove(0, 7);
                _dbContext.Exercises.Add(exercise);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);

            }



        // PUT api/<exercisesController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Exercise exercise)
        {
            var exer = _dbContext.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");
                if (exercise.Image != null)
                {
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    exercise.Image.CopyTo(fileStream);
                    exer.ImageUrl = filePath.Remove(0, 7);
                }
                exer.ExerciseName = exercise.ExerciseName;
                exer.Description = exercise.Description;
                exer.Difficulty = exercise.Difficulty;
                exer.TrailorUrl = exercise.TrailorUrl;
                exer.ImageUrl = exercise.ImageUrl;

                _dbContext.SaveChanges();
                return Ok("Record updated successfully");
            }

        }


        // DELETE api/<exercisesController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
            {
                return NotFound("Record not found against this id");
            }
            else
            {
                _dbContext.Exercises.Remove(exercise);
                _dbContext.SaveChanges();
                return Ok("Record deleted");
            }

        }

        // api/exercises/FindExercises?exerciseName=MissionImpossible
        [Authorize]
        [HttpGet("[Action]")]
        public IActionResult FindExercises(string exerciseName)
        {
            var exercises = from exercise in _dbContext.Exercises
                         where exercise.ExerciseName.StartsWith(exerciseName)
                         select new
                         {
                             ExerciseName = exercise.ExerciseName,
                             ImageUrl = exercise.ImageUrl
                         };
            return Ok(exercises);
        }

    }
}
