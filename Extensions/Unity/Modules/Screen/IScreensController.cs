namespace Build1.PostMVC.Extensions.Unity.Modules.Screen
{
    public interface IScreensController
    {
        public bool HasShownScreens { get; }

        void Show(Screen screen);
        void Show(Screen screen, ScreenBehavior behavior);
        
        void Hide(Screen screen);
        void HideAll();

        bool ScreenIsActive(Screen screen);
    }
}