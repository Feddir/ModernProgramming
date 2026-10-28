
using FitnessTracker.Domain;
using FitnessTracker.Domain.Interface;

var target = new ExerciseTarget("Присідання зі штангою", 10, null);

var athlete = new Athlete
{
    Name = "Олександр",
    Email = "alex.fitness@gmail.com",
    Notes = "Звернути увагу на розминку колін."
};

var workout = new Workout("День ніг");
var squat = new Exercise(target.ExerciseName);
squat.AddSet(120.0, 10);
squat.AddSet(130.0, 8);

workout.AddExercise(squat);
athlete.AddWorkout(workout);

workout.Start();
workout.Complete();

IWorkoutMetricCalculator volumeCalculator = new StrengthVolumeCalculator();
double totalVolume = volumeCalculator.Calculate(workout);

Console.WriteLine($"Атлет: {athlete.Name} ({athlete.Email})");
if (athlete.Notes != null)
{
    Console.WriteLine($"Нотатки: {athlete.Notes}");
}

Console.WriteLine($"Ціль: {target.ExerciseName} (Цільові повторення: {target.TargetRepetitions})");
Console.WriteLine($"Тренування: {workout.Title}");
Console.WriteLine($"Статус (Switch 1): {workout.GetWorkoutStatusDescription()}");
Console.WriteLine($"Загальний об'єм: {totalVolume} кг");
Console.WriteLine($"Категорія об'єму (Switch 2 + Property/Relational Pattern): {workout.EvaluateVolumeCategory(totalVolume)}");