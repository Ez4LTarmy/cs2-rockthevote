namespace cs2_rockthevote.Contracts
{
    public interface IPluginDependency<TPlugin, TConfig>
    {
        public virtual void OnMapStart(string mapName) { }
        public virtual void OnConfigParsed(TConfig config) { }
        public virtual void OnLoad(TPlugin plugin) { }
        public virtual void Clear() { }
    }
}
