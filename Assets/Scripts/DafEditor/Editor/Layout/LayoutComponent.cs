namespace DafEditor.Editor.Layout
{
    public class LayoutComponent
    {
        protected GameEditorWindow gameEditorWindow;
        protected bool isInitialized;
        
        public void Construct(GameEditorWindow gameEditorWindow)
        {
            this.gameEditorWindow = gameEditorWindow;
            isInitialized = true;
        }
    }
}