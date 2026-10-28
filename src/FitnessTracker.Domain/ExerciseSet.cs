
namespace FitnessTracker.Domain
{
    public class ExerciseSet
    {
        public double Weight { get; init; }
        public int Repetitions { get; init; }

        public ExerciseSet(double weight, int repetitions)
        {
            if (weight < 0)
                throw new ArgumentException("Вага не може бути від'ємною.");
            if (repetitions <= 0)
                throw new ArgumentException("Кількість повторень повинна бути більшою за 0.");

            Weight = weight;
            Repetitions = repetitions;
        }
    }
}
