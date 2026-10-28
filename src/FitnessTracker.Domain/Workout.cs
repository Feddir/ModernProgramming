
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Domain
{
    public class Workout
    {
        public string Title { get; }
        public WorkoutStatus Status { get; private set; } = WorkoutStatus.Planned;

        private readonly List<Exercise> _exercises = new List<Exercise>();
        public IReadOnlyCollection<Exercise> Exercises => _exercises.AsReadOnly();

        public Workout(string title)
        {
            Title = title;
        }

        public void AddExercise(Exercise exercise)
        {
            if (Status == WorkoutStatus.Completed || Status == WorkoutStatus.Cancelled)
                throw new InvalidOperationException("Неможливо додати вправу до завершеного чи скасованого тренування.");

            _exercises.Add(exercise);
        }

        public void Start()
        {
            if (Status == WorkoutStatus.Completed || Status == WorkoutStatus.Cancelled)
                throw new InvalidOperationException("Неможливо розпочати завершене або скасоване тренування.");

            Status = WorkoutStatus.InProgress;
        }

        public void Complete()
        {
            if (Status != WorkoutStatus.InProgress)
                throw new InvalidOperationException("Можна завершити лише тренування, яке зараз виконується.");

            Status = WorkoutStatus.Completed;
        }

        public void Cancel()
        {
            if (Status == WorkoutStatus.Completed)
                throw new InvalidOperationException("Неможливо скасувати вже завершене тренування.");

            Status = WorkoutStatus.Cancelled;
        }
    }

}
