namespace MvvmInfrastructure
{
    public interface IDialogService
    {
        DialogResultEnum PresentDialog(string question, DialogOptionsEnum options);

        void ShowMessage(string message);

        string SelectFilePath();

        string SelectDirectoryPath();
    }

    public enum DialogResultEnum
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }

    public enum DialogOptionsEnum
    {
        OK,
        OKCancel,
        YesNoCancel,
        YesNo
    }
}
