public class CreditsView : ViewModel
{
    public StartMenu Menu;
    public override void Close()
    {
        base.Close();
        Menu.SetStartMenuStuff(true);
    }
}
