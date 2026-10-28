

namespace FitnessTracker.Domain.Interface
{
    public interface IWorkoutMetricCalculator
    {
        double Calculate(Workout workout);
    }
}
