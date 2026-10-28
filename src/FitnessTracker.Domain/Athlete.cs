
namespace FitnessTracker.Domain
{

    public class Athlete
    {
        public required string Name { get; init; }
        public required string Email { get; init; }

        public string? Notes { get; init; }

        private readonly List<Workout> _workouts = new();
        public IReadOnlyCollection<Workout> Workouts => _workouts.AsReadOnly();

        public void AddWorkout(Workout workout)
        {
            _workouts.Add(workout);
        }
    }
}
