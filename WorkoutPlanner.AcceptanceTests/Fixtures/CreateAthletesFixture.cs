using Serenity.Fixtures;
using StoryTeller.Engine;
using WorkoutPlanner.Web.Athletes;

namespace WorkoutPlanner.AcceptanceTests.Fixtures
{
    public class CreateAthletesFixture : ScreenFixture<CreateAthleteViewModel>
    {
        public CreateAthletesFixture()
        {
            Title = "Create Athlete";
            EditableElementsForAllImmediateProperties();
        }

        [FormatAs("Go to create athlete screen")]
        public void OpenCreateAthleteScreen()
        {
            Navigation.NavigateTo(new CreateAthleteViewModel());
        }
    }
}