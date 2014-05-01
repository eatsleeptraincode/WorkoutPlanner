using System;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuPersistence;
using FubuMVC.Validation;

namespace WorkoutPlanner.Web.Athletes
{
    public class CreateAthleteEndpoint
    {
        private readonly IEntityRepository _repository;

        public CreateAthleteEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public CreateAthleteViewModel get_athlete_create(CreateAthleteViewModel request)
        {
            return request;
        }

        public FubuContinuation post_athlete_create(CreateAthleteViewModel request)
        {
            _repository.Update(new Athlete
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BirthDate = request.BirthDate
            });
            return FubuContinuation.RedirectTo(new ListAthleteViewModel());
        }
         
    }

    public class CreateAthleteViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class CreateAthleteValidation : OverridesFor<CreateAthleteViewModel>
    {
        public CreateAthleteValidation()
        {
            Property(m => m.FirstName).Required();
            Property(m => m.LastName).Required();
            Property(m => m.Gender).Required();
//            Property(m => m.PhoneNumber).Required();
//            Property(m => m.Email).Required();
            Property(m => m.BirthDate).Required();
        }
    }
}