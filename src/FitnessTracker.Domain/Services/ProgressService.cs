using FitnessTracker.Domain;

namespace FitnessTracker.Domain.Services;
public class ProgressService
{
    public void OnWorkoutCompleted(Workout workout)
    {
        Console.WriteLine($"[ProgressService]: Тренування \"{ workout.Title} \" завершено. Оновлюємо прогрес атлета!");
    }
}