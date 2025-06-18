using LoteriaUcznia.Models;

namespace LoteriaUcznia.Views;

public partial class ClassDetails : ContentPage
{
	Class _Class;


	public ClassDetails(Models.Class _class)
	{
		InitializeComponent();
		this._Class = _class;

        UpdateClass();

    }

    private void AddStudent(object sender, EventArgs e)
    {
        if (StudentNameEntry.Text != null && StudentNameEntry.Text != string.Empty && !StudentNameEntry.Text.Contains(";")) 
        {
            string fileContent = File.ReadAllText(ClassList.DATA_PATH + "\\" + this._Class.ClassName + ".txt");

            fileContent += ";" + StudentNameEntry.Text;
            fileContent = fileContent.Replace(";;", ";");
            if (fileContent.StartsWith(';'))
            {
                fileContent = fileContent.Substring(1);
            }
            File.WriteAllText(ClassList.DATA_PATH + "\\" + this._Class.ClassName + ".txt",fileContent);
            this._Class.Students.Add(new Student(StudentNameEntry.Text));
            UpdateClass();
            DisplayAlert("Success", "New student has been added!", "OK");
        }
        else { DisplayAlert("Error", "Name of the student is incorrect.", "OK"); }   
    }

    private void DeleteStudent(object sender, EventArgs e)
    {
        if (StudentIdEntry.Text != null && StudentIdEntry.Text != string.Empty)
        {
            string fileContent = File.ReadAllText(ClassList.DATA_PATH + "\\" + this._Class.ClassName + ".txt");

            try
            {
                int Id = int.Parse(StudentIdEntry.Text);
                if (Id <= 0 || Id > this._Class.Students.Count)
                { throw new Exception("incorrectStudentId"); }

                string studentname = this._Class.Students[Id-1].Name;
                fileContent = fileContent.Replace(studentname, "");
                fileContent = fileContent.Replace(";;", ";");
                if(fileContent.StartsWith(';'))
                {
                    fileContent = fileContent.Substring(1);
                }
                File.WriteAllText(ClassList.DATA_PATH + "\\" + this._Class.ClassName + ".txt", fileContent);
                this._Class.Students.RemoveAt(Id - 1);
                UpdateClass();
                DisplayAlert("Success", "Student has been deleted!", "OK");
            }
            catch { DisplayAlert("Error", "ID of the student is incorrect.", "OK"); }
            
            
        }
        else { DisplayAlert("Error", "ID of the student is empty.", "OK"); }
    }

    private void Draw(object sender, EventArgs e)
    {
        Random random = new Random();
        if (this._Class.Students.Count <= 0)
        {
            DisplayAlert("Error", "No students in class.", "OK");
        }
        else
        {
            int boundA = 0;
            int boundB = this._Class.Students.Count;
            int _random = random.Next(boundA, boundB);

            string drawnName = this._Class.Students[_random].Name + " (id: " + (_random + 1) +")";

            DisplayAlert("Drawn!", drawnName, "OK");
        }
    }

    private async void DeleteClass(object sender, EventArgs e)
    {
        File.Delete(ClassList.DATA_PATH + "\\" + this._Class.ClassName + ".txt");
        await Navigation.PopAsync();
    }

    private void UpdateClass()
    {
        string studentsList = "Students:\n";

        try
        {
            if (this._Class.Students[0].Name == "")
            {
                this._Class.Students.RemoveAt(0);
            }
        }
        catch { }
        
        
        if (this._Class.Students.Count > 0)
        {
            int id = 1;
            foreach (Student student in this._Class.Students)
            {
                studentsList += "\n" + student.Name + " (id: " + id + "),";
                id++;
            }

            StudentList.Text = studentsList;
        }
        else
        {
            StudentList.Text = "Students:\nclass is empty";
        }
    }

}