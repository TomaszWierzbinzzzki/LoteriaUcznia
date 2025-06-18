namespace WheelOfDoom.Views;

public partial class AddClass : ContentPage
{
	public AddClass()
	{
		InitializeComponent();
	}

    private void AddNewClass(object sender, EventArgs e)
    {
		string newClassName = EntryClassName.Text;

		try
		{
			if (newClassName != string.Empty && newClassName != null && !File.Exists(ClassList.DATA_PATH + "\\" + newClassName + ".txt"))
			{
                File.Create(ClassList.DATA_PATH + "/" + newClassName + ".txt").Dispose();
            }
			else
			{
				throw new Exception("ClassNameEmptyString");
            }

            DisplayAlert("Success", "New class has been created!", "OK");
        }
		catch { DisplayAlert("Error", "Name of the class is either empty or class with that name already exist.", "OK"); }
    }
}