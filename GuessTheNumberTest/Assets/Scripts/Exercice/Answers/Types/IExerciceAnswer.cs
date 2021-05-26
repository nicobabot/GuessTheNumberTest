public interface IExerciceAnswer
{
    bool HasEndExercice { get; } 
    void StartExerciceAnswer(Exercice exercice, OptionsController optionsController);
    void ChoseNumber(int chosenNumber, int index);
}
