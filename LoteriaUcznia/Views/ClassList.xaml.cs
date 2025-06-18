

using LoteriaUcznia.Models;

namespace LoteriaUcznia.Views;

public partial class ClassList : ContentPage
{
	public static string DATA_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WheelOfDoom";
    public static List<Class> classes = new List<Class>();

	public ClassList()
	{
		InitializeComponent();

        if (!Directory.Exists(DATA_PATH))
        {
            Directory.CreateDirectory(DATA_PATH);
        }

        LoadData();
    }

    private void LoadData()
    {
        string[] fileNames = Directory.GetFiles(DATA_PATH);

        if (fileNames.Length > 0)
        {
            ClassListLabel.Text = "Select class:";
            foreach (string element in fileNames)
            {
                List<Student> studentList = new List<Student>();
                string fileRaw = File.ReadAllText(element);
                string[] students = fileRaw.Split(';');

                foreach (string student in students)
                {
                    studentList.Add(new Student(student));
                }

                string[] classNameSplit = element.Split("\\");
                string finalClassName = classNameSplit[classNameSplit.Length - 1];
                finalClassName = finalClassName.Replace(".txt", "");
                classes.Add(new Class(finalClassName, studentList));
            }

            foreach (Class element in classes)
            {
                Button classButton = new Button
                {
                    Text = element.ClassName,
                    HorizontalOptions = LayoutOptions.Fill,
                    Margin = new Thickness(0, 5),
                    StyleClass = new List<string> { "button-secondary" }
                };

                classButton.Clicked += (sender, e) =>
                {
                    Navigation.PushAsync(new ClassDetails(element));
                };

                ClassListContainer.Children.Add(classButton);
            }
        }
        else
        {
            ClassListLabel.Text = "Select class:\nclass list is empty";
        }
        

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        classes.Clear();
        ClassListContainer.Children.Clear();
        LoadData();
    }

    private async void AddClass(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddClass());
    }
}