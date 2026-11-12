using FitnessTracker.Domain;

namespace FitnessTracker.Domain.Services;
public class LoggingService
{
    public void OnWorkoutCompleted(Workout workout)
    {
        Console.WriteLine($"[LoggingService]: Лог — тренування \"{ workout.Title}\" успішно зафіксовано в системі о {DateTime.Now:T}.");
    }
}
