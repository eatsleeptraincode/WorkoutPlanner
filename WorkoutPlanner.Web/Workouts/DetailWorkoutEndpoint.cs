﻿using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.AutoComplete;
using Raven.Client;
using WorkoutPlanner.Web.Exercises;
using WorkoutPlanner.Web.Html;

namespace WorkoutPlanner.Web.Workouts
{
    public class DetailWorkoutEndpoint
    {
        private readonly IDocumentSession _session;

        public DetailWorkoutEndpoint(IDocumentSession session)
        {
            _session = session;
        }

        public DetailWorkoutViewModel get_workout_detail_WorkoutId(DetailWorkoutViewModel request)
        {
            var workout = _session
                .Include<Workout>(w => w.Exercises.Select(e => e.ExerciseId))
                .Load(request.WorkoutId);
            
            var exercises = workout.Exercises.Select(e => new WorkoutExerciseViewModel
            {
                Bottom = e.Bottom,
                Down = e.Down,
                Group = e.Group,
                Reps = e.Reps,
                RepScheme = e.RepScheme,
                RepPatternId = e.RepPatternId,
                Sets = e.Sets,
                Top = e.Top,
                Up = e.Up,
                Exercise = _session.Load<Exercise>(e.ExerciseId).Name,
                ExerciseId = e.ExerciseId
            }).ToList();

            exercises.Add(new WorkoutExerciseViewModel());
            
            return new DetailWorkoutViewModel
            {
                Phase = workout.Phase,
                Type = workout.Type,
                Exercises = exercises,
                WorkoutId = workout.Id
            };
        }
    }

    public class DetailWorkoutViewModel
    {
        public Guid WorkoutId { get; set; }
        public string Type { get; set; }
        public int Phase { get; set; }
        public IList<WorkoutExerciseViewModel> Exercises { get; set; }
    }

    public class WorkoutExerciseViewModel
    {
        public ExerciseGroup Group { get; set; }
        [AutoComplete(typeof(ExerciseLookup))]
        public string Exercise { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Down { get; set; }
        public int Bottom { get; set; }
        public int Up { get; set; }
        public int Top { get; set; }
        public Guid ExerciseId { get; set; }
        public RepScheme RepScheme { get; set; }
        [RepPattern]
        public Guid RepPatternId { get; set; }
    }


    public class ExerciseViewModel
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public ExerciseGroup Group { get; set; }

        public int Down { get; set; }
        public int Bottom { get; set; }
        public int Up { get; set; }
        public int Top { get; set; }
    }

}