using System;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuPersistence;
using FubuMVC.Validation;

namespace WorkoutPlanner.Web.Exercises
{
    public class CreateExerciseEndpoint
    {
        private readonly IEntityRepository _repository;

        public CreateExerciseEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public CreateExerciseViewModel get_exercise_create(CreateExerciseViewModel request)
        {
            return request;
        }

        public FubuContinuation post_exercise_create(CreateExerciseViewModel request)
        {
            _repository.Update(new Exercise {Id = Guid.NewGuid(), Name = request.Name });
            return FubuContinuation.RedirectTo(new ListExerciseViewModel());
        }
         
    }

    public class CreateExerciseViewModel
    {
        public string Name { get; set; }
    }

    public class CreateExerciseValidation : OverridesFor<CreateExerciseViewModel>
    {
        public CreateExerciseValidation()
        {
            Property(m => m.Name).Required();
        }
    }
}