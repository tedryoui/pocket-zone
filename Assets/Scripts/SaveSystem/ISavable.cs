namespace DefaultNamespace.SaveSystem
{
    public interface ISavable
    {
        public void SaveIntoObject(SaveObject saveObject);
        public void LoadFromObject(SaveObject saveObject);
        public void LoadDefaults();
    }
}