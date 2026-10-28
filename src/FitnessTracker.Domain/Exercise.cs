
namespace FitnessTracker.Domain
{
    public class Exercise
    {
        public string Name { get; }

        private readonly List<ExerciseSet> _sets = new List<ExerciseSet>();
        public IReadOnlyCollection<ExerciseSet> Sets => _sets.AsReadOnly();

        public Exercise(string name)
        {
            Name = name;
        }

        public void AddSet(double weight, int repetitions)
        {
            var set = new ExerciseSet(weight, repetitions);
            _sets.Add(set);
        }

        public void RemoveSet(ExerciseSet set)
        {
            _sets.Remove(set);
        }
    }

}
