public class GameModePopup : ViewModel
{
    public StartMenu Menu;
    public void SelectWeponMess() 
    {
        Close();
        GameManager.Instance.StartGame(GameManager.GameMode.WeaponMess);
        Menu.InStartMenu = false;
    }

    public void SelectGrow()
    {
        Close();
        GameManager.Instance.StartGame(GameManager.GameMode.Grow);
        Menu.InStartMenu = false;
    }
}
