namespace TrebleSketch_AIE_Platformer
{
    class SceneClass
    {

        protected enum UIMode
        {
            MainMenu,
            SettingsMenu,
            LoadMenu,
            StoryStage,
            BattleStage,
            InGameUIGone
        }

        public int SceneID;
        public string SceneName;

        // List of Tiles rendered :3
        // protected List<SceneObjects> sceneTiles;

        public void Draw()
        {

        }
    }
}
