
using FitnessTracker.Domain.Interface;

namespace FitnessTracker.Domain
{
    public class RepetitionCalculator : IWorkoutMetricCalculator
    {
        public double Calculate(Workout workout)
        {
            int totalReps = 0;
            foreach (var exercise in workout.Exercises)
            {
                foreach (var set in exercise.Sets)
                {
                    totalReps += set.Repetitions;
                }
            }
            return totalReps;
        }
    }
}
