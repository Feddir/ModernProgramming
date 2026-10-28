
using FitnessTracker.Domain.Interface;

namespace FitnessTracker.Domain
{
    public class StrengthVolumeCalculator : IWorkoutMetricCalculator
    {
        public double Calculate(Workout workout)
        {
            double totalVolume = 0;
            foreach (var exercise in workout.Exercises)
            {
                foreach (var set in exercise.Sets)
                {
                    totalVolume += set.Weight * set.Repetitions;
                }
            }
            return totalVolume;
        }
    }
}
