using FitnessTracker.Domain;

public static class WorkoutExtensions
{
    public static bool IsHighVolume(this Workout workout, Func<Workout, double> volumeCalculator)
    {
        return volumeCalculator(workout) >= 5000;
    }
    public static string GetSummary(this Workout workout)
    {
        return $"[Summary] Тренування: '{workout.Title}' | Статус: {workout.Status} | Вправ: {workout.Exercises.Count}";
    }
}