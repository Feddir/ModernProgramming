using FitnessTracker.Domain;
using FitnessTracker.Domain.Services;

var workoutRepo = new InMemoryRepository<Workout>();

var workout = new Workout("Силове тренування");
var exercise = new Exercise("Жим лежачи");
Action<string> logger =message => Console.WriteLine( $"[{DateTime.Now:HH:mm:ss}] {message}");

for (int i = 0; i < 6; i++)
{
    exercise.AddSet(100, 10);
}
workout.AddExercise(exercise);
workoutRepo.Add(workout);

Func<Workout, double> volumeCalculator = w => w.Exercises.Sum(e => e.Sets.Sum(s => (double)s.Weight * s.Repetitions));

Predicate<Workout> isHighVolumePredicate = w => volumeCalculator(w) >= 5000;

Action<string> printAction = message => Console.WriteLine($"INFO: {message}");

var progressService = new ProgressService();
var loggingService = new LoggingService();

workout.WorkoutCompleted += progressService.OnWorkoutCompleted;
workout.WorkoutCompleted += loggingService.OnWorkoutCompleted;

workout.Start();
printAction("Тренування розпочалось...");
logger("початок тренування");
workout.Complete(); 

Console.WriteLine("\n--- Результати перевірки ---");
Console.WriteLine(workout.GetSummary());

double totalVol = volumeCalculator(workout);
printAction($"Загальний об'єм тренування: {totalVol} кг");
logger($"загальний об'єм тренування: {totalVol} кг");

if (isHighVolumePredicate(workout))
{
    printAction("Це тренування кваліфікується як High Volume (через Predicate).");
}

if (workout.IsHighVolume(volumeCalculator))
{
    printAction("Метод розширення workout.IsHighVolume() підтвердив високий об'єм.");
}