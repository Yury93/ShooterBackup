
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 0;
        public int enemyDied = 0;// Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];
        public int[] weaponsFirst = new int[]{ 0, -1, -1, -1 };
        public int[] weaponsSecond = new int[] { 0, -1, -1, -1 ,-1};
        public int selectedFirstWeapon = 0;
        public int selectedSecondWeapon = 0;
        public int[] levels = new int[] { 0, -1, -1};
        public int selectedLevel = 0;
        public float ValueSlider = -1;
        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
