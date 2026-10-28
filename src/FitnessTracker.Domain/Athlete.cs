
namespace FitnessTracker.Domain
{

    public class Athlete
    {
        public string Name { get; }

        private readonly List<Workout> _workouts = new List<Workout>();
        public IReadOnlyCollection<Workout> Workouts => _workouts.AsReadOnly();

        public Athlete(string name)
        {
            Name = name;
        }

        public void AddWorkout(Workout workout)
        {
            _workouts.Add(workout);
        }
    }
}
